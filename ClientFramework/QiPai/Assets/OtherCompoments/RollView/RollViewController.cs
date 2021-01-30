using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollViewController : MonoBehaviour
{
	public const float X_OFFSET = 266;
	public const float Y_OFFSET = 70;
	public const float MAX_CHANGE_SCALE = 0.2f;
	public const float MOVE_TIME = 0.5f;
	public const float MAX_CHANGE_ALPHA = 55;

	private List<RollItem> _rollItems;
	private int _currentRollItemIndex = 0;
	//private int _tempIndex;
	private float _dragPercent;

	private List<Vector3> _positions = new List<Vector3>();


	private void InitDragCheck()
	{
		DragCheck dragCheck = transform.parent.Find("Image_DragArea").GetComponent<DragCheck>();

		if (!dragCheck)
		{
			dragCheck = transform.parent.Find("Image_DragArea").gameObject.AddComponent<DragCheck>();
		}
		dragCheck.OnDragEnd = OnDragEnd;
		dragCheck.OnDragEvent = OnDrag;
	}

	public void Init()
	{
		InitDragCheck();
		InitRollItem();
		InitPositions();
		SetRollItemPostion();
		if (_rollItems != null)
		{
			for (int i = 0; i < _rollItems.Count; i++)
			{
				_rollItems[i].Init();
			}
		}
	}

	public void Roll(EDragDirection direction)
	{
		if (direction == EDragDirection.None)
		{
			return;
		}
		if (direction == EDragDirection.LeftToRight)
		{
			_currentRollItemIndex = GetIndex(_currentRollItemIndex - 1);
		}
		if (direction == EDragDirection.RightToLeft)
		{
			_currentRollItemIndex = GetIndex(_currentRollItemIndex + 1);
		}
		StopAllCoroutines();
		SetRollItemPostion(0);
		StartCoroutine(SetRollItemTransform());
	}

	private void InitRollItem()
	{
		_rollItems = new List<RollItem>();
		int index = 0;
		for (int i = 0; i < transform.childCount; i++)
		{
			if (!transform.GetChild(i).gameObject.activeSelf)
			{
				continue;
			}
			RollItem rollItem = transform.GetChild(i).GetComponent<RollItem>();
			if (rollItem == null)
			{
				rollItem = transform.GetChild(i).gameObject.AddComponent<RollItem>();
			}
			rollItem.Index = index;
			index++;
			_rollItems.Add(rollItem);
		}
	}

	private void SetRollItemPostion(float percent = 1f)
	{
		float originPercent = percent;
		percent = Mathf.Abs(percent);
		for (int i = 0; i < _rollItems.Count; i++)
		{
			int index = _rollItems[i].Index;
			index = index - _currentRollItemIndex;
			if (index < 0)
			{
				index += _rollItems.Count;
			}
			Vector3 pos = GetPostion(index);
			if (originPercent != 1 && originPercent != 0)
			{
				_rollItems[i].SetPostion(pos, GetPostion(index + GetZhengFu(originPercent)), percent);
			}
			else
			{
				_rollItems[i].SetPostion(pos, percent);
			}
		}
		SetRollItemSibling();
	}

	private void OnDrag(float percent)
	{
		if (_dragPercent == 0 && percent != 0)
		{
			_currentRollItemIndex += GetZhengFu(percent);
		}
		if (_dragPercent > 0 && _dragPercent + percent < 0)
		{
			_currentRollItemIndex++;
		}
		if (_dragPercent < 0 && _dragPercent + percent > 0)
		{
			_currentRollItemIndex--;
		}

		_dragPercent += percent;
		if (Mathf.Abs(_dragPercent) >= 1)
		{
			_currentRollItemIndex += GetZhengFu(_dragPercent) * (int)Mathf.Abs(_dragPercent);
			//_tempIndex = _currentRollItemIndex;
			_dragPercent -= GetZhengFu(_dragPercent) * (int)Mathf.Abs(_dragPercent);
		}
		_currentRollItemIndex = GetIndex(_currentRollItemIndex);
		//_tempIndex = GetIndex(_tempIndex);
		SetRollItemPostion(_dragPercent);
	}

	private void OnDragEnd()
	{
		//if (Mathf.Abs(_dragPercent) < 0.3f) {
		//    _currentRollItemIndex = _tempIndex;
		//}
		StopAllCoroutines();
		StartCoroutine(SetRollItemTransform());
		_dragPercent = 0;
		//_tempIndex = _currentRollItemIndex;
	}

	private int GetIndex(int index)
	{
		if (index >= _rollItems.Count)
		{
			return 0;
		}
		if (index < 0)
		{
			return _rollItems.Count + index;
		}
		return index;
	}

	private void SortRollItemsByPosY()
	{
		for (int i = 0; i < _rollItems.Count; i++)
		{
			for (int j = i; j < _rollItems.Count; j++)
			{
				if (_rollItems[i].transform.localPosition.y > _rollItems[j].transform.localPosition.y)
				{
					RollItem temp = _rollItems[i];
					_rollItems[i] = _rollItems[j];
					_rollItems[j] = temp;
				}
			}
		}
	}

	private void SetRollItemSibling()
	{
		SortRollItemsByPosY();
		for (int i = 0; i < _rollItems.Count; i++)
		{
			_rollItems[i].transform.SetAsFirstSibling();
		}
	}

	private int GetZhengFu(float num)
	{
		if (num >= 0)
		{
			return 1;
		}
		return -1;
	}

	private void InitPositions()
	{
		int count = _rollItems.Count;
		float cellAngle = 360f / count;
		float angle = -90;
		for (int i = 0; i < count; i++)
		{
			float x = X_OFFSET * Mathf.Cos(angle / 180 * Mathf.PI);
			float y = Y_OFFSET * Mathf.Sin(angle / 180 * Mathf.PI);
			_positions.Add(new Vector3(x, y, 1));
			angle += cellAngle;
		}
	}

	private Vector3 GetPostion(int index)
	{
		if (_positions.Count == 0)
		{
			return Vector3.zero;
		}
		if (index >= _positions.Count)
		{
			return _positions[0];
		}
		if (index < 0)
		{
			return _positions[_positions.Count - 1];
		}
		return _positions[index];
	}



	private IEnumerator SetRollItemTransform()
	{
		float timer = Mathf.Abs(_dragPercent * MOVE_TIME);
		while (timer < MOVE_TIME)
		{
			timer += Time.deltaTime;
			for (int i = 0; i < _rollItems.Count; i++)
			{
				_rollItems[i].UpdateRollItemTransform(timer / MOVE_TIME);
			}
			SetRollItemSibling();
			yield return null;
		}
	}
}

public class RollItem : MonoBehaviour
{
	public int Index;
	public Vector3 TargetPos;
	private Vector3 _oldPos;
	private Image _icon;

	private Image Icon
	{
		get { return _icon ?? (_icon = GetComponent<Image>()); }
	}

	public void Init()
	{
		_oldPos = transform.localPosition;
		TargetPos = transform.localPosition;
	}

	public void SetPostion(Vector3 targetPos, float percent)
	{
		SetTargetPos(targetPos);
		UpdateRollItemTransform(percent);
	}

	public void SetPostion(Vector3 targetPos, Vector3 oldPos, float percent)
	{
		TargetPos = targetPos;
		_oldPos = oldPos;
		UpdateRollItemTransform(percent);
	}

	public void UpdateRollItemTransform(float percent)
	{
		transform.localPosition = GetTargetPos(percent);
		transform.localScale = GetScaleByPos();
		SetImageAlpha();
	}

	private void SetTargetPos(Vector3 targetPos)
	{
		if (targetPos == TargetPos)
		{
			return;
		}
		_oldPos = TargetPos;
		TargetPos = targetPos;
	}

	private void SetImageAlpha()
	{
		Color color = Icon.color;
		color.a = GetAlphaByPos(transform.localPosition);
		Icon.color = color;
	}

	public Vector3 GetTargetPos(float percent)
	{
		return Vector3.Slerp(_oldPos, TargetPos, percent);
		return Vector3.Lerp(transform.localPosition, TargetPos, percent);
	}

	private Vector3 GetScaleByPos()
	{
		float scale = 1 - RollViewController.MAX_CHANGE_SCALE * (transform.localPosition.y + RollViewController.Y_OFFSET) / (RollViewController.Y_OFFSET * 2);
		scale = Mathf.Clamp(scale, 1 - RollViewController.MAX_CHANGE_SCALE, 1);
		return Vector3.one * scale;
	}

	private float GetAlphaByPos(Vector3 pos)
	{
		return 1 - (RollViewController.MAX_CHANGE_ALPHA * (pos.y + RollViewController.Y_OFFSET) / (RollViewController.Y_OFFSET * 2)) / 255f;
	}

}
