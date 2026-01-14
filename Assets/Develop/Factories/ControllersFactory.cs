using Controllers;
using Interfaces;

namespace Factories
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

        public CompositeController CreateMainHeroController(IMovable movable, IJumpable jumpable)
        {
            return new CompositeController(CreatePlayerMovementController(movable), CreatePlayerJumpController(jumpable));
        }
    }
}
