using Characters;
using UnityEngine;

namespace Controllers
{
    public class RigidbodyJumpController : Controller
    {
        private readonly RigidbodyCharacter _character;
        private readonly Rigidbody2D _rigidbody;


        private bool _jumpPressed;

        private Vector2 _velocity;

        public RigidbodyJumpController(RigidbodyCharacter character)
        {
            _character = character;

            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            _jumpPressed = Input.GetKeyDown(KeyCode.Space);

            Vector2 currentVelocity = _rigidbody.velocity;

            _velocity = currentVelocity;

            HandleJump();

            HandleCeil();

            //_character.Velocity = _velocity;
        }

        private void HandleCeil()
        {
            if (_character.IsCeilinged())
                _velocity.y = Mathf.Min(0, _velocity.y);
        }

        private void HandleJump()
        {
            if (_jumpPressed && _character.IsGrounded())
                _velocity.y = _character.JumpForce;
        }
    }
}

