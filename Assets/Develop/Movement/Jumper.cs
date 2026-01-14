using UnityEngine;
using Utils;

namespace Movement
{
    public class Jumper
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _jumpForce;
        private readonly float _gravityModifier;

        private readonly float _wallSlideSpeed;
        private readonly Vector2 _wallJumpForce;
        private readonly float _wallJumpDuration;

        private float _wallJumpTimer;

        public bool IsInputLocked => _wallJumpTimer > 0;

        public Jumper(
            Rigidbody2D rigidbody,
            float jumpForce,
            float gravityModifier,
            float wallSlideSpeed,
            Vector2 wallJumpForce,
            float wallJumpDuration)
        {
            _rigidbody = rigidbody;
            _jumpForce = jumpForce;
            _gravityModifier = gravityModifier;
            _wallSlideSpeed = wallSlideSpeed;
            _wallJumpForce = wallJumpForce;
            _wallJumpDuration = wallJumpDuration;
        }

        public void Update(EnvironmentSensor sensor, float deltaTime)
        {
            if(_wallJumpTimer > 0)
                _wallJumpTimer -= deltaTime;

            bool isGrounded = sensor.IsGrounded;
            bool isTouchingWall = sensor.IsTouchingWall(out int wallDirection);
            Vector2 velocity = _rigidbody.velocity;

            if (isGrounded == false || velocity.y > 0.01f)
                velocity.y -= _gravityModifier * deltaTime;
            else if(isGrounded && velocity.y < 0)
                velocity.y = 0;

            if (isGrounded == false && isTouchingWall && velocity.y < 0)
                velocity.y = Mathf.Max(velocity.y, -_wallSlideSpeed);

            if(sensor.IsCeiling && velocity.y > 0)
                velocity.y = 0;

            _rigidbody.velocity = velocity;
        }

        public void Jump(EnvironmentSensor sensor, Rotator rotator)
        {
            if (sensor.IsGrounded)
            {
                ApplyYVelocity(_jumpForce);
            }
            else if(sensor.IsTouchingWall(out int wallDirection))
            {
                _rigidbody.velocity = new Vector2(-wallDirection * _wallJumpForce.x, _wallJumpForce.y);
                _wallJumpTimer = _wallJumpDuration;

                rotator.SetInputDirection(new Vector2(_rigidbody.velocity.x, 0));
                rotator.Update();
            }
        }

        private void ApplyYVelocity(float yValue)
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = yValue;
            _rigidbody.velocity = velocity;
        }
    }
}
