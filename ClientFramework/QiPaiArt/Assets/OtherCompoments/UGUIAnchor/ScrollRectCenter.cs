using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ScrollDir
{
    Horizontal,
    Vertical
}

public class ScrollRectCenter : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    public ScrollDir Dir = ScrollDir.Horizontal;

    /// <summary>
    /// 是否正在居中
    /// </summary>
    private bool _isCentering = false;

    /// <summary>
    /// 居中过程移动速度
    /// </summary>
    public float MoveToCenterSpeed = 10f;

    private ScrollRect _scrollView;

    private Transform _content;

    private List<float> _childrenPos = new List<float>();
    private float _targetPos;

    /// <summary>
    /// 当前中心child索引
    /// </summary>
    private int _curCenterChildIndex = -1;

    /// <summary>
    /// 当前中心ChildItem
    /// </summary>
    public GameObject CurCenterChildItem
    {
        get
        {
            GameObject centerChild = null;
            if (_content != null && _curCenterChildIndex >= 0 && _curCenterChildIndex < _content.childCount)
            {
                centerChild = _content.GetChild(_curCenterChildIndex).gameObject;
            }
            return centerChild;
        }
    }

    void Awake()
    {
        _scrollView = GetComponent<ScrollRect>();
        if (_scrollView == null)
        {
            Debug.LogError("ScrollRect is null");
            return;
        }
        _content = _scrollView.content;

        LayoutGroup layoutGroup = null;
        layoutGroup = _content.GetComponent<LayoutGroup>();

        if (layoutGroup == null)
        {
            Debug.LogError("LayoutGroup component is null");
        }
        _scrollView.movementType = ScrollRect.MovementType.Unrestricted;
        float spacing = 0f;
        //根据dir计算坐标，Horizontal：存x，Vertical：存y
        switch (Dir)
        {
            case ScrollDir.Horizontal:
                if (layoutGroup is HorizontalLayoutGroup)
                {
                    float childPosX = _scrollView.GetComponent<RectTransform>().rect.width * 0.5f - GetChildItemWidth(0) * 0.5f;
                    float halfValue = _scrollView.GetComponent<RectTransform>().rect.width * 0.25f;
                    spacing = (layoutGroup as HorizontalLayoutGroup).spacing;
                    _childrenPos.Add(childPosX);
                    for (int i = 1; i < _content.childCount; i++)
                    {
                        childPosX -= GetChildItemWidth(i) * 0.5f + GetChildItemWidth(i - 1) * 0.5f + spacing + halfValue;
                        _childrenPos.Add(childPosX);
                    }
                }
                else if (layoutGroup is GridLayoutGroup)
                {
                    GridLayoutGroup grid = layoutGroup as GridLayoutGroup;
                    float childPosX = _scrollView.GetComponent<RectTransform>().rect.width * 0.5f - grid.cellSize.x * 0.5f;
                    _childrenPos.Add(childPosX);
                    for (int i = 0; i < _content.childCount - 1; i++)
                    {
                        childPosX -= grid.cellSize.x + grid.spacing.x;
                        _childrenPos.Add(childPosX);
                    }
                }
                else
                {
                    Debug.LogError("Horizontal ScrollView is using VerticalLayoutGroup");
                }
                break;
            case ScrollDir.Vertical:
                if (layoutGroup is VerticalLayoutGroup)
                {
                    float childPosY = -_scrollView.GetComponent<RectTransform>().rect.height * 0.5f + GetChildItemHeight(0) * 0.5f;
                    spacing = (layoutGroup as VerticalLayoutGroup).spacing;
                    _childrenPos.Add(childPosY);
                    for (int i = 1; i < _content.childCount; i++)
                    {
                        childPosY += GetChildItemHeight(i) * 0.5f + GetChildItemHeight(i - 1) * 0.5f + spacing;
                        _childrenPos.Add(childPosY);
                    }
                }
                else if (layoutGroup is GridLayoutGroup)
                {
                    GridLayoutGroup grid = layoutGroup as GridLayoutGroup;
                    float childPosY = -_scrollView.GetComponent<RectTransform>().rect.height * 0.5f + grid.cellSize.y * 0.5f;
                    _childrenPos.Add(childPosY);
                    for (int i = 1; i < _content.childCount; i++)
                    {
                        childPosY += grid.cellSize.y + grid.spacing.y;
                        _childrenPos.Add(childPosY);
                    }
                }
                else
                {
                    Debug.LogError("Vertical ScrollView is using HorizontalLayoutGroup");
                }
                break;
        }
    }

    private float GetChildItemWidth(int index)
    {
        return (_content.GetChild(index) as RectTransform).sizeDelta.x;
    }

    private float GetChildItemHeight(int index)
    {
        return (_content.GetChild(index) as RectTransform).sizeDelta.y;
    }

    void Start()
    {

    }

    void Update()
    {
        if (_isCentering)
        {
            Vector3 v = _content.localPosition;
            switch (Dir)
            {
                case ScrollDir.Horizontal:
                    v.x = Mathf.Lerp(_content.localPosition.x, _targetPos, MoveToCenterSpeed * Time.deltaTime);
                    _content.localPosition = v;
                    if (Math.Abs(_content.localPosition.x - _targetPos) < 0.01f)
                    {
                        _isCentering = false;
                    }
                    break;
                case ScrollDir.Vertical:
                    v.y = Mathf.Lerp(_content.localPosition.y, _targetPos, MoveToCenterSpeed * Time.deltaTime);
                    _content.localPosition = v;
                    if (Math.Abs(_content.localPosition.y - _targetPos) < 0.01f)
                    {
                        _isCentering = false;
                    }
                    break;
            }


        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        switch (Dir)
        {
            case ScrollDir.Horizontal:
                _targetPos = FindClosestChildPos(_content.localPosition.x, out _curCenterChildIndex);
                break;
            case ScrollDir.Vertical:
                _targetPos = FindClosestChildPos(_content.localPosition.y, out _curCenterChildIndex);
                break;
        }

        for (var i = 0; i < _content.childCount; ++i)
        {
            _content.GetChild(i).localScale = 0.8f * Vector3.one;
        }
        var target = _content.GetChild(_curCenterChildIndex);
        target.localScale = 1.2f * Vector3.one;

        _isCentering = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isCentering = false;
        _curCenterChildIndex = -1;
    }

    private float FindClosestChildPos(float currentPos, out int curCenterChildIndex)
    {
        float closest = 0;
        float distance = Mathf.Infinity;
        curCenterChildIndex = -1;
        for (int i = 0; i < _childrenPos.Count; i++)
        {
            float p = _childrenPos[i];
            float d = Mathf.Abs(p - currentPos);
            if (d < distance)
            {
                distance = d;
                closest = p;
                curCenterChildIndex = i;
            }
        }
        return closest;
    }
}