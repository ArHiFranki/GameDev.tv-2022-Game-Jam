using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private SpeedController speedController;

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.Translate(Vector3.left * speedController.CurrentSpeed * Time.deltaTime);
    }

    public void InitSpeedController(SpeedController speedController)
    {
        this.speedController = speedController;
    }
}
