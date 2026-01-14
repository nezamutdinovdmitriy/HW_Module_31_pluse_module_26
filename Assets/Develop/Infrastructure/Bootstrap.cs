using Develop.Characters;
using Develop.Controllers;
using Develop.Factories;
using UnityEngine;
using Develop.Utils;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Character _enemy;
    [SerializeField] private Transform[] _patrolPoints;

    private ControllersFactory _controllersFactory;

    private ControllerUpdateService _controllerUpdateService;

    private void Awake()
    {
        _controllersFactory = new ControllersFactory();
        _controllerUpdateService = new ControllerUpdateService();

        CompositeController mainHeroController = _controllersFactory.CreateMainHeroController(_character, _character);

        PatrolController patrolController = new PatrolController(_enemy, 0.5f, _patrolPoints);

        mainHeroController.Enable();
        patrolController.Enable();

        _controllerUpdateService.Add(mainHeroController);
        _controllerUpdateService.Add(patrolController);
    }

    private void Update()
    {
        _controllerUpdateService?.Update(Time.deltaTime);
    }
}
