using UnityEngine;

namespace Interface
{
    public interface IMovable
    {
        public float CurrentHorizontalVelocity { get; }

        public bool IsGrounded();
    }
}

