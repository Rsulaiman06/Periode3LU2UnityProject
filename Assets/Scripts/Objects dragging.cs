using UnityEngine;
using UnityEngine.EventSystems;

public class Objectsdragging : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public MoveObjects moveObjects;
    private RectTransform rectTransform; // UI-element position handling
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveObjects.panel.SetActive(false); // Panel verdwijnt
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveObjects.panel.SetActive(true); // Panel komt terug
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
