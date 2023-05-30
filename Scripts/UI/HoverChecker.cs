using UnityEngine;
using UnityEngine.EventSystems;

public class HoverChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool Hovered { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hovered = false;
    }
}
