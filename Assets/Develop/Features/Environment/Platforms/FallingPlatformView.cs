using UnityEngine;

namespace Develop.Features.Environment
{
    public class FallingPlatformView : MonoBehaviour
    {
        [SerializeField] private FallingPlatform _platform;
        [SerializeField] private float _shakeAmount = 0.05f;
        [SerializeField] private Color _warningColor = Color.red;

        private Vector2 _originalPosition;
        private SpriteRenderer[] _childSpriteRenderers;
        private Color[] _originalColors;

        private void Awake()
        {
            _originalPosition = transform.localPosition;
            
            _childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _originalColors = new Color[_childSpriteRenderers.Length];

            for (int i = 0; i < _childSpriteRenderers.Length; i++)
                _originalColors[i] = _childSpriteRenderers[i].color;
        }

        private void OnEnable()
        {
            _platform.StabilityChanged += OnStabilityChanged;
            _platform.Collapsed += OnCollapsed;
        }
        private void OnDisable()
        {
            _platform.StabilityChanged -= OnStabilityChanged;
            _platform.Collapsed -= OnCollapsed;
        }

        private void OnCollapsed() => transform.localPosition = _originalPosition;

        private void OnStabilityChanged(float progress)
        {
            float lerpProgress = 1f - progress;

            float currentShake = _shakeAmount * lerpProgress;
            
            float x = Random.Range(-1f,1f) * currentShake;
            float y = Random.Range(-1f, 1f) * currentShake;

            transform.localPosition = _originalPosition + new Vector2(x, y);

            for (int i = 0; i < _childSpriteRenderers.Length; i++)
                _childSpriteRenderers[i].color = Color.Lerp(_originalColors[i], _warningColor, lerpProgress);
        }
    }
}