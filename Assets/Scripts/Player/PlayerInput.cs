using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMoveController))]
[RequireComponent(typeof(PlayerJumpController))]
[RequireComponent(typeof(PlayerFireController))]
public class PlayerInput : MonoBehaviour
{
    private Vector2 moveInput;
    private PlayerMoveController moveController;
    private PlayerJumpController jumpController;
    private PlayerFireController fireController;

    private void Awake()
    {
        moveController = GetComponent<PlayerMoveController>();
        jumpController = GetComponent<PlayerJumpController>();
        fireController = GetComponent<PlayerFireController>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveController.SetMoveInput(moveInput);
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Jump Pressed");
        }
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Fire Pressed");
        }
    }
}
