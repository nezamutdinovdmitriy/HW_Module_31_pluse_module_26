using UnityEngine;

namespace Interface
{
    public interface IRotatable
    {
        public Quaternion GetRotationFrom(Vector2 velocity);
    }
}