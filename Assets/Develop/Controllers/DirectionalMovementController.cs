using UnityEngine;

public class DirectionalMovementController : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    [SerializeField] private ObstacleChecker _groundChecker;
    [SerializeField] private float _yVelocityForJump;
    [SerializeField] private float _gravity;

    private Vector2 _velocity;
    private bool _jumpPressed; 

    private void Update()
    {
        float xInput = Input.GetAxisRaw(HorizontalAxisName);

        _jumpPressed = Input.GetKeyDown(KeyCode.Space);
        
        float horizontalVelocity = _speed * xInput;

        _velocity = new Vector2(horizontalVelocity, _velocity.y);

        HandleGravity();
         
        HandleJump();

        _rigidbody.velocity = _velocity;
    }

    private void HandleJump()
    {
        if(_jumpPressed && _groundChecker.IsTouches())
            _velocity.y = _yVelocityForJump;
    }

    private void HandleGravity()
    {
        if (_groundChecker.IsTouches() && _velocity.y <= 0)
            _velocity.y = 0;
        else
            _velocity.y -= _gravity * Time.deltaTime;
    }
}
