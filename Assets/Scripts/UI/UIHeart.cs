using UnityEngine;

public class UIHeart : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(true);
    }

    public void Create()
    {
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
