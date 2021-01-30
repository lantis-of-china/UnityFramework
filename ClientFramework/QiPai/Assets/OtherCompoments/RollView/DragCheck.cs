using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EDragDirection
{
	None,
	LeftToRight,
	RightToLeft,
}

public class DragCheck : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerClickHandler
{
	public System.Action OnDragEnd;
	public System.Action<float> OnDragEvent;

	private const float MIN_DRAG_DIATANCE = 500;
	private const float MAX_DRAG_PERCENT = 0.999f;

	private Vector3 _pointerDownPos;
	private bool _isPointerDown;

	private float _dragedDistance;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		_isPointerDown = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{

		if (Input.touchCount > 1)
		{
			return;
		}
		if (!_isPointerDown)
		{
			return;
		}
		_isPointerDown = false;
		if (OnDragEnd != null)
		{
			OnDragEnd();
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		float lastDis = _dragedDistance;
		_dragedDistance += eventData.delta.x;
		_dragedDistance = Mathf.Min(_dragedDistance, MIN_DRAG_DIATANCE * MAX_DRAG_PERCENT);
		_dragedDistance = Mathf.Max(_dragedDistance, -MIN_DRAG_DIATANCE * MAX_DRAG_PERCENT);
		if (_dragedDistance == lastDis)
		{
			return;
		}
		if (OnDragEvent != null)
		{
			OnDragEvent((lastDis - _dragedDistance) / MIN_DRAG_DIATANCE);
		}
	}

	//把事件透下去
	public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
		where T : IEventSystemHandler
	{
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(data, results);
		GameObject current = data.pointerCurrentRaycast.gameObject;
		for (int i = 0; i < results.Count; i++)
		{
			if (current != results[i].gameObject)
			{
				ExecuteEvents.Execute(results[i].gameObject, data, function);
				break;
				//RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		_pointerDownPos = eventData.position;
		_dragedDistance = 0;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		if (Vector3.Distance(_pointerDownPos, eventData.position) < 10)
		{
			//PassEvent(eventData, ExecuteEvents.submitHandler);
			PassEvent(eventData, ExecuteEvents.pointerClickHandler);
		}
	}
}
