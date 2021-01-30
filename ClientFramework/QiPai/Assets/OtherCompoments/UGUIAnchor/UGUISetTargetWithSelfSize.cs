using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public enum UGUISetHorizontal : int
{
    None = 0,
    Size = 1
}

public enum UGUISetVertical : int
{
    None = 0,
    Size = 1
}

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class UGUISetTargetWithSelfSize : MonoBehaviour {
    public UGUISetHorizontal horizontalFollow;
    public UGUISetVertical verticalFollow;
    public RectTransform targetRectTransform;
    private RectTransform selfRectTransform;
    public float offsetX = 5.0f;
    public float offsetY = 5.0f;

    public bool isInit;

#if UNITY_EDITOR

    void Reset()
    {
    }


    void OnValidate()
    {
        if (!enabled)
        {
            return;
        }

        if (!isInit)
        {
            InitSelfRectTransform();
        }
    }

#endif

    public bool InitSelfRectTransform()
    {
        if (selfRectTransform == null)
        {
            selfRectTransform = transform.GetComponent<RectTransform>();
        }

        if (targetRectTransform == null || selfRectTransform == null)
        {
            return false;
        }

        if (targetRectTransform == selfRectTransform)
        {
            targetRectTransform = null;
            return false;
        }

        if (!isInit)
        {
            isInit = true;
        }

        return isInit;
    }

    private void Awake()
    {
    }

    public void Update ()
    {
        if (!enabled)
        {
            return;
        }

        if (InitSelfRectTransform())
        {
            if (targetRectTransform == null || selfRectTransform == null)
            {
                return;
            }

            if (targetRectTransform == selfRectTransform)
            {
                targetRectTransform = null;
                return;
            }

            if (horizontalFollow != UGUISetHorizontal.None)
            {
                targetRectTransform.sizeDelta = new Vector2(selfRectTransform.sizeDelta.x + offsetX, selfRectTransform.sizeDelta.y);
            }

            if (verticalFollow != UGUISetVertical.None)
            {
                targetRectTransform.sizeDelta = new Vector2(selfRectTransform.sizeDelta.x, selfRectTransform.sizeDelta.y + offsetY);
            }
        }
    }
}

