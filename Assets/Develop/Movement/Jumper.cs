using System;
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

        private float _wallJumpSpeed;

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

        public void ApplyGravity(bool isGrounded, float deltaTime)
        {
            Vector2 velocity = _rigidbody.velocity;

            if (isGrounded && velocity.y <= 0)
                velocity.y = 0;
            else
                velocity.y -= _gravityModifier * deltaTime;

            _rigidbody.velocity = velocity;
        }

        public void Update(bool isJumpPressed, EnvironmentSensor sensor, float deltaTime, out bool wasWallJump)
        {
            wasWallJump = false;

            bool isGrounded = sensor.IsGrounded;
            bool isTouchingWall = sensor.IsTouchingWall(out int wallDirection);

            ApplyGravity(isGrounded, deltaTime);

            if (isGrounded == false && isTouchingWall && _rigidbody.velocity.y < 0)
                ApplyYVelocity(Mathf.Max(_rigidbody.velocity.y, -_wallSlideSpeed));

            if (isJumpPressed)
            {
                if (isGrounded)
                    ApplyYVelocity(_jumpForce);
                else if (isTouchingWall)
                {
                    _rigidbody.velocity = new Vector2(-wallDirection * _wallJumpForce.x, _wallJumpForce.y);
                    wasWallJump = true;
                }
            }

            if(sensor.IsCeiling)
                StopVerticalVelocity();
        }

        public void HandleWallSlide(bool isGrounded, bool isTouchingWall)
        {
            if (isGrounded == false && isTouchingWall && _rigidbody.velocity.y < 0)
                ApplyYVelocity(Mathf.Max(_rigidbody.velocity.y, -_wallSlideSpeed));
        }

        public void StopVerticalVelocity() => ApplyYVelocity(Mathf.Min(0, _rigidbody.velocity.y));

        private void ApplyYVelocity(float yValue)
        {
            Vector2 velocity = _rigidbody.velocity;

            velocity.y = yValue;

            _rigidbody.velocity = velocity;
        }
    }
}
