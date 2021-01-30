using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public enum UGUIFollowHorizontal : int
{
    None = 0,
    Size = 1
}

public enum UGUIFollowVertical : int
{
    None = 0,
    Size = 1
}

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class UGUIFollowSize : MonoBehaviour {
    public UGUIFollowHorizontal horizontalFollow;
    public UGUIFollowVertical verticalFollow;
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

    void Update ()
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

            if (horizontalFollow != UGUIFollowHorizontal.None)
            {
                
                selfRectTransform.sizeDelta = new Vector2(targetRectTransform.rect.width + offsetX, selfRectTransform.sizeDelta.y);
            }

            if (verticalFollow != UGUIFollowVertical.None)
            {
                selfRectTransform.sizeDelta = new Vector2(selfRectTransform.sizeDelta.x, targetRectTransform.rect.height + offsetY);
            }
        }
    }
}

