using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private SpeedController mySpeedController;

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.Translate(Vector3.left * mySpeedController.CurrentSpeed * Time.deltaTime);
    }

    public void InitSpeedController(SpeedController speedController)
    {
        mySpeedController = speedController;
    }
}
