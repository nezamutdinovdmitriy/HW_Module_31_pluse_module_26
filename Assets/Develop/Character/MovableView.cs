using Interface;
using UnityEngine;
using Utils;

namespace Characters
{
    public class MovableView : MonoBehaviour, IInitializable
    {
        private readonly int _velocityXKey = Animator.StringToHash("VelocityX");

        [SerializeField] private Animator _animator;

        private IMovable _movable;

        private bool _isInit;

        public void Initialize()
        {
            _movable = GetComponentInParent<IMovable>();

            _isInit = true;
        }

        private void Update()
        {
            if (_isInit == false)
                return;

            _animator.SetFloat(_velocityXKey, Mathf.Abs(_movable.Velocity.x));
        }
    }
}

