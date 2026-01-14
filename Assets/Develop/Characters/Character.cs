using Develop.Interfaces;
using Develop.Movement;
using UnityEngine;
using Develop.Utils;
using System;

namespace Develop.Characters
{
    public class Character : MonoBehaviour, IMovable, IJumpable, ITransformable, IKillable
    {
        public event Action Died;

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

        private Mover _mover;
        private Rotator _rotator;
        private Jumper _jumper;
        private EnvironmentSensor _sensor;

        private bool _isDied;

        public Vector2 Velocity => _rigidbody.velocity;
        public bool IsSlideWall => _sensor.IsTouchingWall(out int direction);
        public bool IsGrounded => _sensor.IsGrounded;
        public bool IsInputLocked => _jumper.IsInputLocked;

        public Transform Transform => transform;
        
        public bool IsDied => _isDied;

        private void Awake()
        {
            _mover = new Mover(_rigidbody, _moveSpeed);
            _rotator = new Rotator(transform);
            _jumper = new Jumper(_rigidbody, _yVelocityForJump, _gravityModifier, _wallSlideSpeed, _wallJumpForce, _wallJumpDuration);
            _sensor = new EnvironmentSensor(_groundChecker, _ceilChecker, _leftWallChecker, _rightWallChecker);
        }

        private void Update()
        {
            if (_isDied)
                return;

            _jumper.Update(_sensor, _rotator, Time.deltaTime);

            if (IsInputLocked == false)
                _mover.Update();

            _rotator.Update();
        }

        public void Move(Vector2 direction)
        {
            _mover.SetInputDirection(direction);
            _rotator.SetInputDirection(direction);
        }

        public void Jump() => _jumper.Jump(_sensor, _rotator);

        public void Kill()
        {
            if (_isDied == true)
                return;

            _isDied = true;

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;

            Died?.Invoke();
        }
    }
}