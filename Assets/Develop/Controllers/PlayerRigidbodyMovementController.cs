using Characters;
using UnityEngine;

namespace Controllers
{
    public class PlayerRigidbodyMovementController : Controller
    {
        private const string HorizontalAxisName = "Horizontal";

        private readonly RigidbodyCharacter _character;

        public PlayerRigidbodyMovementController(RigidbodyCharacter character)
        {
            _character = character;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            float xInput = Input.GetAxisRaw(HorizontalAxisName);

            Vector2 inputDirection = new Vector2(xInput, _character.Velocity.y);

            _character.SetMoveDirection(inputDirection);
            _character.SetRotationDirection(inputDirection);
        }
    }
}

