using System.Collections;
using UnityEngine;

namespace Develop.Characters
{
    public class DeathView : MonoBehaviour
    {
        private readonly int _isDiedKey = Animator.StringToHash("IsDied");

        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _deathDuration;

        private void OnEnable() => _character.Died += Death;
        private void OnDisable() => _character.Died -= Death;

        private void Death() => StartCoroutine(DeathProcess());

        private IEnumerator DeathProcess()
        {
            _animator.SetBool(_isDiedKey, _character.IsDied);
            while(_deathDuration > 0)
            {
                _deathDuration -= Time.deltaTime;
                yield return null;
            }

            Destroy(_character.gameObject);
        }
    }
}