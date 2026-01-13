using Interface;
using UnityEngine;
using Utils;

namespace Characters
{
    public class RigidbodyCharacter : MonoBehaviour, IMovable, IJumper, IRotatable
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;

        [SerializeField] private ObstacleChecker _groundChecker;
        [SerializeField] private ObstacleChecker _ceilChecker;

        private DirectionalMover _mover;
        private DirectionalRotator _rotator;

        public Transform Transform => transform;
        
        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
        public Vector2 CurrentVelocity => _rigidbody.velocity;

        public void Initialize()
        {
            _mover = new DirectionalMover(_rigidbody, _moveSpeed);
            _rotator = new DirectionalRotator(transform);

            foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
                initializable.Initialize();
        }

        public bool IsGrounded() => _groundChecker.IsTouches();
        public bool IsCeilinged() => _ceilChecker.IsTouches();

        private void Update()
        {
            float xInput = Input.GetAxisRaw("Horizontal");

            _rigidbody.velocity = new Vector2(xInput, CurrentVelocity.y);

            _mover.SetInputDirection(CurrentVelocity);
            _rotator.SetInputDirection(CurrentVelocity);

            _mover.Update(Time.deltaTime);
            _rotator.Update(Time.deltaTime);
        }
    }
}

