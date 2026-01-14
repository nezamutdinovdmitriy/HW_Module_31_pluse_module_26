using Develop.Characters;
using System;
using System.Collections;
using UnityEngine;

namespace Develop.Features.Environment
{
    public class FallingPlatform : MonoBehaviour
    {
        public event Action<float> Collapsed;

        [SerializeField] private float _fallDelay = 1.5f;

        private bool _isProcessing;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_isProcessing == false && collision.gameObject.GetComponent<Character>())
                StartCoroutine(ProcessFall());
        }

        private IEnumerator ProcessFall()
        {
            _isProcessing = true;

            Collapsed?.Invoke(_fallDelay);

            yield return new WaitForSeconds(_fallDelay);

            Destroy(gameObject);
        }
    }
}
