using UnityEngine;

namespace Develop.Interfaces
{
    public interface IMovable : ITransformable
    {
        public void Move(Vector2 direction);
        public bool IsInputLocked { get; }
    }
}