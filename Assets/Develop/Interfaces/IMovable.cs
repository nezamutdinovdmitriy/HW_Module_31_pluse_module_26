using UnityEngine;

namespace Interface
{
    public interface IMovable
    {
        public float MoveSpeed { get; }
        public Vector2 Velocity { get; }
    }
}

