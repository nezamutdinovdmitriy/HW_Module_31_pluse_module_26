using UnityEngine;

namespace Characters
{
    public class CharacterView : MonoBehaviour
    {
        private readonly int _velocityXKey = Animator.StringToHash("VelocityX");
        private readonly int _isGroundedKey = Animator.StringToHash("IsGrounded");
        private readonly int _isWallSlideKey = Animator.StringToHash("IsWallSlide");

        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _animator.SetFloat(_velocityXKey, Mathf.Abs(_character.Velocity.x));
            _animator.SetBool(_isGroundedKey, _character.IsGrounded);
            _animator.SetBool(_isWallSlideKey, _character.IsSlideWall);
        }
    }
}

