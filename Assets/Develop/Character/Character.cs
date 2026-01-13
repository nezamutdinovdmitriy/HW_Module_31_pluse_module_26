using Controllers;
using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Characters
{
    public class Character : MonoBehaviour
    {
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

        private Mover _mover;
        private Rotator _rotator;
        private Jumper _jumper;

        private PlayerJumpController _jumpController;
        private PlayerMovementController _movementController;

        private EnvironmentSensor _sensor;

        private float _wallJumpTimer;

        public Vector2 Velocity => _rigidbody.velocity;
        public bool IsSlideWall => _sensor.IsTouchingWall(out int direction);
        public bool IsGrounded => _sensor.IsGrounded;

        private void Awake()
        {
            _mover = new Mover(_rigidbody, _moveSpeed);
            _rotator = new Rotator(transform);
            _jumper = new Jumper(_rigidbody, _yVelocityForJump, _gravityModifier, _wallSlideSpeed, _wallJumpForce, _wallJumpDuration);

            _sensor = new EnvironmentSensor(_groundChecker, _ceilChecker, _leftWallChecker, _rightWallChecker);

            _jumpController = new PlayerJumpController();
            _movementController = new PlayerMovementController();

            _jumpController.Enable();
            _movementController.Enable();
        }

        private void Update()
        {
            _jumpController.Update(Time.deltaTime);
            _movementController.Update(Time.deltaTime);

            if (_wallJumpTimer <= 0)
            {
                _mover.SetInputDirection(_movementController.InputDirection);
                _rotator.SetInputDirection(_movementController.InputDirection);

                _mover.Update();
                _rotator.Update();
            }
            else
            {
                _wallJumpTimer -= Time.deltaTime;
            }

            _jumper.Update(_jumpController.IsJumpPressed, _sensor, Time.deltaTime, out bool wasWallJump);

            if (wasWallJump)
            {
                _wallJumpTimer = _wallJumpDuration;
                _rotator.SetInputDirection(new Vector2(_rigidbody.velocity.x, 0));
                _rotator.Update();
            }

            CheckFall();
        }

        private void CheckFall()
        {
            if (_levelBounds.OverlapPoint(transform.position) == false)
            {
                Debug.Log("Defeat!");

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
    }
}