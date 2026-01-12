using Characters;
using Controllers;

namespace Factories
{
    public class ControllersFactory
    {
        public RigidbodyMovementController CreateRigidbodyMovementController(
            RigidbodyCharacter character
            )
        {
            return new RigidbodyMovementController(character);
        }

        public RigidbodyJumpController CreateRigidbodyJumpController(
            RigidbodyCharacter character,
            float gravityModifier
            )
        {
            return new RigidbodyJumpController(character, gravityModifier);
        }

        public CompositeController CreateCompositeController(
            params Controller[] controllers)
        {
            return new CompositeController(controllers);
        }

        public CompositeController CreateMainHeroController(
            RigidbodyCharacter character,
            float gravityModifier)
        {
            return new CompositeController(
                CreateRigidbodyMovementController(character),
                CreateRigidbodyJumpController(character, gravityModifier));
        }
    }
}

