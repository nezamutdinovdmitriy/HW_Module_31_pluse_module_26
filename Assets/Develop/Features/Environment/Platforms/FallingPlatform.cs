using Develop.Characters;
using System;
using UnityEngine;

namespace Develop.Features.Environment
{
    public class FallingPlatform : MonoBehaviour
    {
        public event Action Collapsed;
        public event Action<float> StabilityChanged;

        [SerializeField] private float _maxStability = 100f;
        [SerializeField] private float _wearSpeed = 50f;

        private float _currentStability;
        private bool _isCollapsed;

        private void Awake()
        {
            _currentStability = _maxStability;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_isCollapsed)
                return;

            if (collision.gameObject.GetComponent<Character>())
            {
                _currentStability -= _wearSpeed * Time.deltaTime;

                float progress = Mathf.Clamp01(_currentStability / _maxStability);
                
                StabilityChanged?.Invoke(progress);

                if(_currentStability <= 0)
                {
                    Collapsed?.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
}