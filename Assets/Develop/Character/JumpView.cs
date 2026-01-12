using Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters
{
    public class JumpView : MonoBehaviour, IInitializable
    {
        private readonly int _velocityYKey = Animator.StringToHash("VelocityY");

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

            _animator.SetFloat(_velocityYKey, _jumper.CurrentVerticalVelocity);
        }
    }
}

