using UnityEngine;
using Utils;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        private const string HorizontalAxisName = "Horizontal";

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private ObstacleChecker _groundChecker;
        [SerializeField] private ObstacleChecker _ceilChecker;
        [SerializeField] private ObstacleChecker _leftWallChecker;
        [SerializeField] private ObstacleChecker _rightWallChecker;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _yVelocityForJump;
        [SerializeField] private float _gravityModifier;

        [SerializeField] private float _wallSlideSpeed = 2f;
        [SerializeField] private Vector2 _wallJumpForce = new Vector2(10f, 12f);
        [SerializeField] private float _wallJumpDuration = 0.2f;

        [SerializeField] private PolygonCollider2D _levelBounds;

        private Vector2 _velocity;
        private bool _jumpPressed;
        private float _wallJumpTimer;

        public Vector2 Velocity => _rigidbody.velocity;
        public bool IsSlideWall => IsTouchingWall(out int wallDirection);

        private Quaternion TurnRight => Quaternion.identity;
        private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

        private void Update()
        {
            if (_wallJumpTimer > 0)
                _wallJumpTimer -= Time.deltaTime;

            float xInput = Input.GetAxisRaw(HorizontalAxisName);
            _jumpPressed = Input.GetKeyDown(KeyCode.Space);

            if (_wallJumpTimer <= 0)
                _velocity.x = _moveSpeed * xInput;

            HandleGravity();
            HandleWallSlide();
            HandleJump();
            HandleCeil();

            _rigidbody.velocity = _velocity;

            if (_wallJumpTimer <= 0 && Mathf.Abs(_velocity.x) > 0.01f)
                transform.rotation = GetRotationFrom(_velocity);

            CheckFall();
        }

        public bool IsGrounded() => _groundChecker.IsTouches();

        private void CheckFall()
        {
            if (_levelBounds.OverlapPoint(transform.position) == false)
            {
                Debug.Log("Defeat!");
            }
        }

        private void HandleWallSlide()
        {
            if (IsGrounded() == false && IsTouchingWall(out _) && _velocity.y < 0)
            {
                _velocity.y = Mathf.Max(_velocity.y, -_wallSlideSpeed);
            }
        }

        private void HandleCeil()
        {
            if (_ceilChecker.IsTouches())
                _velocity.y = Mathf.Min(0, _velocity.y);
        }

        private void HandleJump()
        {
            if (_jumpPressed == false)
                return;

            if (IsGrounded())
            {
                _velocity.y = _yVelocityForJump;
            }
            else if (IsTouchingWall(out int wallDirection))
            {
                _wallJumpTimer = _wallJumpDuration;
                _velocity.x = -wallDirection * _wallJumpForce.x;
                _velocity.y = _wallJumpForce.y;

                transform.rotation = _velocity.x > 0 ? TurnRight : TurnLeft;
            }

            if (_jumpPressed && _groundChecker.IsTouches())
            {
                _velocity.y = _yVelocityForJump;
            }
        }

        private bool IsTouchingWall(out int wallDirection)
        {
            if (_rightWallChecker.IsTouches())
            {
                wallDirection = 1;
                return true;
            }
            if (_leftWallChecker.IsTouches())
            {
                wallDirection = -1;
                return true;
            }

            wallDirection = 0;
            return false;
        }

        private void HandleGravity()
        {
            if (_groundChecker.IsTouches() && _velocity.y <= 0)
                _velocity.y = 0;
            else
                _velocity.y -= _gravityModifier * Time.deltaTime;
        }

        private Quaternion GetRotationFrom(Vector2 velocity)
        {
            if (velocity.x > 0)
                return TurnRight;

            if (velocity.x < 0)
                return TurnLeft;

            return transform.rotation;
        }
    }
}

