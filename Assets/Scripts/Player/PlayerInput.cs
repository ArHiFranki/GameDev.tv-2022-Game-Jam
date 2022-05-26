using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMoveController))]
[RequireComponent(typeof(PlayerFireController))]
public class PlayerInput : MonoBehaviour
{
    private Vector2 moveInput;
    private PlayerMoveController moveController;
    private PlayerFireController fireController;

    private void Awake()
    {
        moveController = GetComponent<PlayerMoveController>();
        fireController = GetComponent<PlayerFireController>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveController.SetMoveInput(moveInput);
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            fireController.Fire();
        }
    }
}
