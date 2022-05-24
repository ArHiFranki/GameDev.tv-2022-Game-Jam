using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private SpeedController speedController;

    private RawImage image;
    private float imageUVPositionX;

    private void Awake()
    {
        image = GetComponent<RawImage>();
    }

    private void Start()
    {
        imageUVPositionX = image.uvRect.x;
    }

    private void Update()
    {
        ScrollImage();
    }

    private void ScrollImage()
    {
        imageUVPositionX += speedController.CurrentSpeed * scrollSpeed * Time.deltaTime;

        if (imageUVPositionX > 1)
        {
            imageUVPositionX = 0;
        }

        image.uvRect = new Rect(imageUVPositionX, 0, image.uvRect.width, image.uvRect.height);
    }
}
