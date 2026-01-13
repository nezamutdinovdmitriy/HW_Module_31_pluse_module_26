using UnityEngine;

namespace Controllers
{
    public class PlayerJumpController : Controller
    {        
        private const KeyCode JumpKey = KeyCode.Space;

        public bool IsJumpPressed { get; private set; }

        protected override void UpdateLogic(float deltaTime)
        {
            IsJumpPressed = Input.GetKeyDown(JumpKey);
        }
    }
}
