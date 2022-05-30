using UnityEngine;

public class Castle : MonoBehaviour
{
    private SpeedController mySpeedController;
    private PlayerMoveController playerMoveController;

    private bool canMove = true;

    private void OnEnable()
    {
        playerMoveController = FindObjectOfType<PlayerMoveController>();
    }

    private void Update()
    {
        if (canMove)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        transform.Translate(Vector3.left * mySpeedController.CurrentSpeed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CastleStopTrigger castleStopTrigger))
        {
            canMove = false;
            mySpeedController.SetCurrentSpeed(0);
            playerMoveController.SetDefaultMoveAnimationSpeed(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TriggerWinEvent();
        }
    }

    public void InitSpeedController(SpeedController speedController)
    {
        mySpeedController = speedController;
    }
}
