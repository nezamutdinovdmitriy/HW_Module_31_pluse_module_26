using UnityEngine;

public class Jumper
{
    private readonly Rigidbody2D _rigidbody;
    private readonly float _jumpForce;

    private Vector2 _velocity;

    public Jumper(Rigidbody2D rigidbody, float jumpForce)
    {
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
    }

    //public void Update()
    //{
    //    Vector2 currentVelocity = _rigidbody.velocity;

    //    _velocity = currentVelocity;

    //    //HandleJump();

    //    HandleCeil();

    //    _rigidbody.velocity = _velocity;
    //}

    public void Jump()
    {
        Vector2 currentVelocity = _rigidbody.velocity;

        _velocity = currentVelocity;

        _velocity.y = _jumpForce;

        _rigidbody.velocity = _velocity;
    }

    //private void HandleCeil()
    //{
    //    if (_jumper.IsCeilinged())
    //        _velocity.y = Mathf.Min(0, _velocity.y);
    //}

    //private void HandleJump()
    //{
    //    if (_jumpPressed && _jumper.IsGrounded())
    //        _velocity.y = _jumper.JumpForce;
    //}
}
