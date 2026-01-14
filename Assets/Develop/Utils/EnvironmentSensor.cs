namespace Develop.Utils
{
    public class EnvironmentSensor
    {
        private readonly ObstacleChecker _ground, _ceil, _leftWall, _rightWall;

        public EnvironmentSensor(
            ObstacleChecker ground,
            ObstacleChecker ceil,
            ObstacleChecker leftWall,
            ObstacleChecker rightWall)
        {
            _ground = ground;
            _ceil = ceil;
            _leftWall = leftWall;
            _rightWall = rightWall;
        }

        public bool IsGrounded => _ground.IsTouches();
        public bool IsCeiling => _ceil.IsTouches();
        public bool IsTouchingWall(out int direction)
        {
            if (_rightWall.IsTouches())
            {
                direction = 1;
                return true;
            }

            if (_leftWall.IsTouches())
            {
                direction = -1;
                return true;
            }

            direction = 0;
            return false;
        }
    }
}
