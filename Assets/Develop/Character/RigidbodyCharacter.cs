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

        public float CurrentHorizontalVelocity => _rigidbody.velocity.x;
        public float CurrentVerticalVelocity => _rigidbody.velocity.y;

        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;

        public ObstacleChecker GroundChecker => _groundChecker;
        public ObstacleChecker CeilChecker => _ceilChecker;

        private Quaternion TurnRight => Quaternion.identity;
        private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

        public void Initialize()
        {
            foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
                initializable.Initialize();
        }

        private void Update()
        {
            transform.rotation = GetRotationFrom(_rigidbody.velocity);
        }

        public Quaternion GetRotationFrom(Vector2 velocity)
        {
            if (velocity.x > 0)
                return TurnRight;

            if (velocity.x < 0)
                return TurnLeft;

            return transform.rotation;
        }

        public bool IsGrounded() => _groundChecker.IsTouches();
        public bool IsCeilinged() => _ceilChecker.IsTouches();
    }
}

