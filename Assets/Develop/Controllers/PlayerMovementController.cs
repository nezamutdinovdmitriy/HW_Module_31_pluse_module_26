using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : Controller
    {
        private const string HorizontalAxisName = "Horizontal";

        public Vector2 InputDirection { get; private set; }

        protected override void UpdateLogic(float deltaTime)
        {
            float xInput = Input.GetAxisRaw(HorizontalAxisName);
            InputDirection = new Vector2(xInput, 0);
        }
    }
}
