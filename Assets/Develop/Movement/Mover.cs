using UnityEngine;

namespace Movement
{
    public class Mover
    {
        private readonly Rigidbody2D _rigidbody;
        private float _moveSpeed;

        private Vector2 _currentDirection;

        public Mover(Rigidbody2D rigidbody, float moveSpeed)
        {
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;
        }

        public Vector2 Velocity => _rigidbody.velocity;

        public void SetInputDirection(Vector2 horizontalInput) => _currentDirection = horizontalInput;

        public void Update(bool isInputLocked)
        {
            if (isInputLocked)
                return;

            Vector2 velocity = _rigidbody.velocity;

            velocity.x = _currentDirection.x * _moveSpeed;

            _rigidbody.velocity = velocity;
        }
    }
}