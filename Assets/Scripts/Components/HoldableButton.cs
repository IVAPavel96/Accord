using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldableButton : Button//MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onHold = new UnityEvent();
    public UnityEvent onRelease = new UnityEvent();

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        onHold.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onRelease.Invoke();
    }
}