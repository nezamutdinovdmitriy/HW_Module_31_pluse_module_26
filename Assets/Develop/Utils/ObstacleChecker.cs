using UnityEngine;

namespace Utils
{
    public class ObstacleChecker : MonoBehaviour
    {
        private const float offsetBoundY = 0.01f; 

        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _distanceToCheck;

        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private Vector2 _direction;

        public bool IsTouches()
        {
            Bounds bounds = _collider.bounds;

            Vector2 origin = new Vector2(bounds.center.x, bounds.min.y + offsetBoundY);

            Vector2 size = new Vector2(bounds.size.x * 0.9f, offsetBoundY * 2f);

            return Physics2D.CapsuleCast(origin, size, CapsuleDirection2D.Horizontal, 0, _direction, _distanceToCheck, _mask).collider != null;
        }  
    }
}