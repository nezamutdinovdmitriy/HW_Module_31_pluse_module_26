using Interface;
using UnityEngine;
using Utils;

namespace Characters
{
    public class JumpView : MonoBehaviour, IInitializable
    {
        private readonly int _isGroundedKey = Animator.StringToHash("IsGrounded");

        [SerializeField] private Animator _animator;

        private IJumper _jumper;

        private bool _isInit;

        public void Initialize()
        {
            _jumper = GetComponentInParent<IJumper>();

            _isInit = true;
        }

        private void Update()
        {
            if (_isInit == false)
                return;

            _animator.SetBool(_isGroundedKey, _jumper.IsGrounded());
        }
    }
}

