using UnityEngine;
using Interfaces;

namespace Controllers
{
    public class PlayerMovementController : Controller
    {
        private const string HorizontalAxisName = "Horizontal";
        private IMovable _movable;

        public PlayerMovementController(IMovable movable)
        {
            _movable = movable;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if(_movable.IsInputLocked == false)
            {
                float xInput = Input.GetAxisRaw(HorizontalAxisName);
                _movable.Move(new Vector2(xInput, 0));
            }
        }
    }
}
