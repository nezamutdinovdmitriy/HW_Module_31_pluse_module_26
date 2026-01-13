using UnityEngine;
using Utils;

namespace Characters
{
    public class RigidbodyCharacter : MonoBehaviour
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

        public void Initialize()
        {
            _mover = new DirectionalMover(_rigidbody, _moveSpeed, _acceleration);
            _rotator = new DirectionalRotator(transform);
        }

        private void Update()
        {
            _mover.Update();
            _rotator.Update();
        }

        public void SetMoveDirection(Vector2 inputDirection) => _mover.SetInputDirection(inputDirection);
        public void SetRotationDirection(Vector2 inputDirection) => _rotator.SetInputDirection(inputDirection);
    }
}

