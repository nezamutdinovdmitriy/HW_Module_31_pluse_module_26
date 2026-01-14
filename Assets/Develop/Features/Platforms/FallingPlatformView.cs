using System.Collections;
using UnityEngine;

namespace Features.Platforms
{
    public class FallingPlatformView : MonoBehaviour
    {
        [SerializeField] private FallingPlatform _platform;
        [SerializeField] private float _shakeAmount = 0.05f;
        [SerializeField] private Color _warningColor = Color.red;

        private Vector2 _originalPosition;
        private SpriteRenderer[] _childSpriteRenderers;
        private Color[] _originalColors;

        private void OnEnable() => _platform.Collapsed += StartShake;
        private void OnDisable() => _platform.Collapsed -= StartShake;

        private void Awake()
        {
            _childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _originalColors = new Color[_childSpriteRenderers.Length];

            for (int i = 0; i < _childSpriteRenderers.Length; i++)
                _originalColors[i] = _childSpriteRenderers[i].color;
        }

        private void StartShake(float duration)
        {
            _originalPosition = transform.localPosition;
            StartCoroutine(DestroyProcess(duration));
        }

        private IEnumerator DestroyProcess(float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float progress = elapsed / duration;

                float currentShake = _shakeAmount * progress;

                float x = Random.Range(-1f,1f) * currentShake;
                float y = Random.Range(-1f, 1f) * currentShake;

                transform.localPosition = _originalPosition + new Vector2(x, y);

                for (int i = 0; i < _childSpriteRenderers.Length; i++)
                    _childSpriteRenderers[i].color = Color.Lerp(_originalColors[i], _warningColor, progress);

                yield return null;
            }

            transform.localPosition = _originalPosition;
        }
    }
}