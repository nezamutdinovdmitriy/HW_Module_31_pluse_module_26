using Characters;
using UnityEngine;
using Controllers;
using Factories;
using Utils;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;

    private ControllersFactory _controllersFactory;

    private ControllerUpdateService _controllerUpdateService;

    private void Awake()
    {
        _controllersFactory = new ControllersFactory();
        _controllerUpdateService = new ControllerUpdateService();

        CompositeController mainHeroController = _controllersFactory.CreateMainHeroController(_character, _character);

        mainHeroController.Enable();

        _controllerUpdateService.Add(mainHeroController);
    }

    private void Update()
    {
        _controllerUpdateService?.Update(Time.deltaTime);
    }
}
