using UnityEngine;

public class DirectionalMover
{
    private Rigidbody2D _rigidbody;
    private float _moveSpeed;

    private Vector2 _currentDirection;

    public DirectionalMover(Rigidbody2D rigidbody, float moveSpeed)
    {
        _rigidbody = rigidbody;
        _moveSpeed = moveSpeed;
    }

    public float HorizontalVelocity { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    public void SetInputDirection(Vector2 direction) => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        HorizontalVelocity = _currentDirection.x * _moveSpeed;

        CurrentVelocity = _rigidbody.velocity;

        _rigidbody.velocity = new Vector2(HorizontalVelocity, CurrentVelocity.y);
    }
}
