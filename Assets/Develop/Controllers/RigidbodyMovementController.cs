using Characters;
using UnityEngine;

namespace Controllers
{
    public class RigidbodyMovementController : Controller
    {
        private const string HorizontalAxisName = "Horizontal";

        private readonly RigidbodyCharacter _character;
        private readonly Rigidbody2D _rigidbody;

        public RigidbodyMovementController(RigidbodyCharacter character)
        {
            _character = character;

            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            float xInput = Input.GetAxisRaw(HorizontalAxisName);

            float horizontalVelocity = _character.MoveSpeed * xInput;

            Vector2 currentVelocity = _rigidbody.velocity;

            _rigidbody.velocity = new Vector2(horizontalVelocity, currentVelocity.y);
        }
    }
}

