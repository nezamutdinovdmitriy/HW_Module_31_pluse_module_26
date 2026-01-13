using UnityEngine;

public class DirectionalMover
{
    private readonly Rigidbody2D _rigidbody;
    private readonly float _moveSpeed;

    private Vector2 _currentDirection;

    public DirectionalMover(Rigidbody2D rigidbody, float moveSpeed, float acceleration)
    {
        _rigidbody = rigidbody;
        _moveSpeed = moveSpeed;
    }

    public float HorizontalVelocity { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    public void SetInputDirection(Vector2 direction) => _currentDirection = direction;

    public void Update()
    {
        HorizontalVelocity = _currentDirection.x * _moveSpeed;

        CurrentVelocity = _rigidbody.velocity;

        _rigidbody.velocity = new Vector2(HorizontalVelocity, CurrentVelocity.y);
    }
}
