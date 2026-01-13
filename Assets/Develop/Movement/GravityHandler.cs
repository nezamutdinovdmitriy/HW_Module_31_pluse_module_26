using UnityEngine;

public class GravityHandler
{
    private readonly Rigidbody2D _rigidbody;
    private readonly IGravityTarget _gravityTarget;
    private readonly float _gravityModifier;
    

    public GravityHandler(Rigidbody2D rigidbody, IGravityTarget gravityTarget, float gravityModifier)
    {
        _rigidbody = rigidbody;
        _gravityTarget = gravityTarget;
        _gravityModifier = gravityModifier;
    }

    public void Update()
    {
        ApplyGravity(_gravityModifier);
    }

    public void ApplyGravity(float gravityModifier)
    {
        Vector2 currentVelocity = _rigidbody.velocity;

        if (_gravityTarget.IsGrounded() && currentVelocity.y <= 0)
            currentVelocity.y = 0;
        else
            currentVelocity.y -= gravityModifier * Time.deltaTime;

        _rigidbody.velocity = currentVelocity;
    }
}
