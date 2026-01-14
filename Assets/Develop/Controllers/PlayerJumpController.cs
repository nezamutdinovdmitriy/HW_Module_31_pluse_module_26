using UnityEngine;
using Interfaces;

namespace Controllers
{
    public class PlayerJumpController : Controller
    {        
        private const KeyCode JumpKey = KeyCode.Space;

        private IJumpable _jumpable;

        public PlayerJumpController(IJumpable jumpable)
        {
            _jumpable = jumpable;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetKeyDown(JumpKey))
                _jumpable.Jump();
        }
    }
}
