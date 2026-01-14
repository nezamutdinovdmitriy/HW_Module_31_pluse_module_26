using Characters;
using UnityEngine;
using Controllers;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;

    private PlayerMovementController _movementController;
    private PlayerJumpController _jumpController;

    private void Awake()
    {
        _movementController = new PlayerMovementController(_character);
        _jumpController = new PlayerJumpController(_character);

        _movementController.Enable();
        _jumpController.Enable();
    }

    private void Update()
    {
        _movementController.Update(Time.deltaTime);
        _jumpController.Update(Time.deltaTime);
    }
}
