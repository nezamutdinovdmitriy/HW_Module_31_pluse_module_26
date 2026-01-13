using Characters;
using UnityEngine;

namespace Controllers
{
    public class RigidbodyJumpController : Controller
    {
        private readonly RigidbodyCharacter _character;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _gravityModifier;

        private bool _jumpPressed;

        private Vector2 _velocity;

        public RigidbodyJumpController(RigidbodyCharacter character, float gravityModifier)
        {
            _character = character;

            _rigidbody = character.GetComponent<Rigidbody2D>();
            _gravityModifier = gravityModifier;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            _jumpPressed = Input.GetKeyDown(KeyCode.Space);

            Vector2 currentVelocity = _rigidbody.velocity;

            _velocity = currentVelocity;

            HandleGravity(deltaTime);

            HandleJump();

            HandleCeil();

            _rigidbody.velocity = _velocity;
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

        private void HandleGravity(float deltaTime)
        {
            if (_character.IsGrounded() && _velocity.y <= 0)
                _velocity.y = 0;
            else
                _velocity.y -= _gravityModifier * deltaTime;
        }
    }
}

