using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChouPaiArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler,IEndDragHandler {
    public System.Action<int, Vector2> BeginDragEvent;
    public System.Action<int, Vector2> PointerUpEvent;
    public System.Action<int, Vector2> DragEvent;

    public void OnPointerUp(PointerEventData eventData) {
        if (Input.touchCount > 2) {
            return;
        }
     
        if (PointerUpEvent != null) {
            PointerUpEvent(ConvertPointerId(eventData.pointerId), eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (Input.touchCount > 2) {
            return;
        }
        if (DragEvent != null) {
            DragEvent(ConvertPointerId(eventData.pointerId), eventData.delta);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Input.touchCount > 2) {
            return;
        }
        if (BeginDragEvent != null) {
            BeginDragEvent(ConvertPointerId(eventData.pointerId), eventData.position);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
    }

    public void OnEndDrag(PointerEventData eventData) {

    }

    private int ConvertPointerId(int pointerId) {
        if (pointerId < 0) {
            return 0;
        }
        return pointerId;
    }
}
