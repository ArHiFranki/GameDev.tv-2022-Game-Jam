using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxWidth;
    [SerializeField] private float minWidth;
    [SerializeField] private float moveForwardAnimationSpeed = 1.5f;
    [SerializeField] private float moveBackwardAnimationSpeed = 0.8f;
    [SerializeField] private Animator playerAnimator;

    private Vector2 playerMoveInput;
    private Vector3 deltaPosition;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private float defaultMaxHeightValue;
    private float defaultMinHeightValue;
    private float defaultMaxWidthValue;
    private float defaultMinWidthValue;
    private const string moveAnimationSpeedValue = "moveSpeed";

    public float MaxHeight => maxHeight;
    public float MinHeight => minHeight;
    public float MaxWidth => maxWidth;
    public float MinWidth => minWidth;

    private void Start()
    {
        defaultMaxHeightValue = maxHeight;
        defaultMinHeightValue = minHeight;
        defaultMaxWidthValue = maxWidth;
        defaultMinWidthValue = minWidth;
    }

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
        oldPosition = transform.position;
        deltaPosition = playerMoveInput * moveSpeed * Time.deltaTime;
        newPosition = transform.position + deltaPosition;

        newPosition.x = Mathf.Clamp(newPosition.x, minWidth, maxWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minHeight, maxHeight);

        SetMoveSpeedAnimation();
        transform.position = newPosition;
    }

    private void SetMoveSpeedAnimation()
    {
        if (newPosition.x < oldPosition.x)
        {
            playerAnimator.SetFloat(moveAnimationSpeedValue, moveBackwardAnimationSpeed);
        }
        else if (newPosition.x == oldPosition.x)
        {
            playerAnimator.SetFloat(moveAnimationSpeedValue, 1f);
        }
        else
        {
            playerAnimator.SetFloat(moveAnimationSpeedValue, moveForwardAnimationSpeed);
        }
    }

    public void SetBorders(float maxHeightValue, float minHeightValue, float maxWidthValue, float minWidthValue)
    {
        maxHeight = maxHeightValue;
        minHeight = minHeightValue;
        maxWidth = maxWidthValue;
        minWidth = minWidthValue;
    }

    public void ResetBorders()
    {
        maxHeight = defaultMaxHeightValue;
        minHeight = defaultMinHeightValue;
        maxWidth = defaultMaxWidthValue;
        minWidth = defaultMinWidthValue;
    }
}
