using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private Vector2 playerMoveInput;

    private void Update()
    {
        PlayerMove();
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        playerMoveInput = moveInput;
    }

    private void PlayerMove()
    {
        Vector3 deltaPosition = playerMoveInput * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + deltaPosition;
        newPosition.y = Mathf.Clamp(newPosition.y, minHeight, maxHeight);

        transform.position = newPosition;
    }
}
