using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
