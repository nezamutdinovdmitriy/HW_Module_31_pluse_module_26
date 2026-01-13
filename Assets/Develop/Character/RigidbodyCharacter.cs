using Interface;
using UnityEngine;
using Utils;

namespace Characters
{
    public class RigidbodyCharacter : MonoBehaviour, IMovable, IJumper, IRotatable, IGravityTarget
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityModifier;

        [SerializeField] private ObstacleChecker _groundChecker;
        [SerializeField] private ObstacleChecker _ceilChecker;

        private DirectionalMover _mover;
        private DirectionalRotator _rotator;
        private GravityHandler _gravity;
        private Jumper _jumper;

        public Transform Transform => transform;
        
        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
        public Vector2 Velocity => _rigidbody.velocity;

        public void Initialize()
        {
            _mover = new DirectionalMover(_rigidbody, _moveSpeed, _acceleration);
            _rotator = new DirectionalRotator(transform);
            _gravity = new GravityHandler(_rigidbody, this, _gravityModifier);
            _jumper = new Jumper(_rigidbody, _jumpForce);

            foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
                initializable.Initialize();
        }

        private void Update()
        {
            _mover.Update();
            _rotator.Update();
            _gravity.Update();
        }

        public void SetMoveDirection(Vector2 inputDirection) => _mover.SetInputDirection(inputDirection);
        public void SetRotationDirection(Vector2 inputDirection) => _rotator.SetInputDirection(inputDirection);

        public bool IsGrounded() => _groundChecker.IsTouches();
        public bool IsCeilinged() => _ceilChecker.IsTouches();
    }
}

