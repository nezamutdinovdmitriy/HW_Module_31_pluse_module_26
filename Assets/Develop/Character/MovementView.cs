using UnityEngine;

namespace Develop.Characters
{
    public class MovementView : MonoBehaviour
    {
        private readonly int _velocityXKey = Animator.StringToHash("VelocityX");

        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _animator.SetFloat(_velocityXKey, Mathf.Abs(_character.Velocity.x));
        }
    }
}
