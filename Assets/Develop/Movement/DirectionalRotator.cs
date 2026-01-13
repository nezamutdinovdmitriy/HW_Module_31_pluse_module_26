using UnityEngine;

public class DirectionalRotator
{
    private readonly Transform _transform;
    private Vector2 _currentDirection;

    public DirectionalRotator(Transform transform)
    {
        _transform = transform;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    private Quaternion TurnRight => Quaternion.identity;
    private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

    public void SetInputDirection(Vector2 direction) => _currentDirection = direction;

    public void Update()
    {
        _transform.rotation = GetRotationFrom(_currentDirection);
    }

    private Quaternion GetRotationFrom(Vector2 direction)
    {
        if (direction.x > 0)
            return TurnRight;

        if (direction.x < 0)
            return TurnLeft;

        return _transform.rotation;
    }
}
