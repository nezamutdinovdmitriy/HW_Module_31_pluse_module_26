using UnityEngine;

namespace Utils
{
    public class ObstacleChecker : MonoBehaviour
    {
        private const float GroundContactOffset = 0.01f; 
        private const float GroundProbeWidthFactor = 0.9f; 

        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _distanceToCheck;

        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private Vector2 _direction;

        private Vector2 _origin;
        private Vector2 _size;

        //private RaycastHit2D[] _hits = new RaycastHit2D[3];

        public bool IsTouches()
        {
            Bounds bounds = _collider.bounds;

            _origin.x = bounds.center.x;
            _origin.y = bounds.min.y + GroundContactOffset;

            _size.x = bounds.size.x * GroundProbeWidthFactor;
            _size.y = GroundContactOffset * 2f;

            return Physics2D.CapsuleCast(_origin, _size, CapsuleDirection2D.Horizontal, 0, _direction, _distanceToCheck, _mask).collider != null;

            //int count = Physics2D.CapsuleCastNonAlloc(_origin, _size, CapsuleDirection2D.Horizontal, 0, _direction, _hits, _distanceToCheck, _mask);

            //return count > 2;
        }  
    }
}