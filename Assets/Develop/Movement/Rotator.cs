using UnityEngine;

namespace Develop.Movement
{
    public class Rotator
    {
        private readonly Transform _transform;
        private readonly Quaternion _turnRight = Quaternion.identity;
        private readonly Quaternion _turnLeft = Quaternion.Euler(0, 180, 0);

        private Vector2 _currentDirection;

        public Rotator(Transform transform) => _transform = transform;

        public void SetInputDirection(Vector2 horizontalInput) => _currentDirection = horizontalInput;

        public void Update()
        {
            if (Mathf.Abs(_currentDirection.x) > 0.01f)
                _transform.rotation = _currentDirection.x > 0 ? _turnRight : _turnLeft;
        }
    }
}
