using Develop.Characters;
using Develop.Controllers;
using Develop.Factories;
using Develop.Utils;
using UnityEngine;

namespace Develop.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Character _enemy;
        [SerializeField] private Transform[] _patrolPoints;

        [SerializeField] private Collider2D _levelBounds;

        private ControllersFactory _controllersFactory;

        private ControllerUpdateService _controllerUpdateService;

        private GameMode _gameMode;

        private void Awake()
        {
            _controllersFactory = new ControllersFactory();
            _controllerUpdateService = new ControllerUpdateService();

            CompositeController mainHeroController = _controllersFactory.CreateMainHeroController(_character, _character);
            PatrolController patrolController = _controllersFactory.CreatePatrolController(_enemy, 0.5f, _patrolPoints);

            mainHeroController.Enable();
            patrolController.Enable();

            _controllerUpdateService.Add(mainHeroController);
            _controllerUpdateService.Add(patrolController);

            _gameMode = new GameMode(_character, _levelBounds, this);
        }

        private void Update()
        {
            _controllerUpdateService?.Update(Time.deltaTime);
            _gameMode?.Update(Time.deltaTime);
        }
    }
}