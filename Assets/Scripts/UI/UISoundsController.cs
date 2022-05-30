using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundsController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] SoundController soundController;

    public void OnPointerDown(PointerEventData eventData)
    {
        soundController.PlayOnMouseClickUISound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        soundController.PlayOnMouseOverUISound();
    }
}
