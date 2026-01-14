using Controllers;
using System.Collections.Generic;

namespace Utils
{
    public class ControllerUpdateService
    {
        private List<Controller> _controllers = new List<Controller>();

        public void Add(Controller controller) => _controllers.Add(controller);

        public void Update(float deltaTime)
        {
            foreach (Controller controller in _controllers)
                controller.Update(deltaTime);
        }
    }
}