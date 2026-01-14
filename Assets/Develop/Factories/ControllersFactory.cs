using Develop.Controllers;
using Develop.Interfaces;
using UnityEngine;

namespace Develop.Factories
{
    public class ControllersFactory
    {
        public PlayerJumpController CreatePlayerJumpController(IJumpable jumpable)
        {
            return new PlayerJumpController(jumpable);
        }

        public PlayerMovementController CreatePlayerMovementController(IMovable movable)
        {
            return new PlayerMovementController(movable);
        }

        public CompositeController CreateCompositeController(Controller[] controllers)
        {
            return new CompositeController(controllers);
        }

        public PatrolController CreatePatrolController(
            IMovable movable, 
            float minDistanceToTarget, 
            Transform[] patrolPoints)
        {
            return new PatrolController(movable, minDistanceToTarget, patrolPoints);
        }

        public CompositeController CreateMainHeroController(
            IMovable movable, 
            IJumpable jumpable)
        {
            return new CompositeController(
                CreatePlayerMovementController(movable), 
                CreatePlayerJumpController(jumpable));
        }
    }
}