using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public enum UGUIAnchorTypeHorizontal : int
{
    忽略 = 0,
    左_左 = 1,
    右_右 = 2,
    左_右 = 3,
    右_左 = 4,
}

public enum UGUIAnchorTypeVertical : int
{
    忽略 = 0,
    顶_顶 = 1,
    底_底 = 2,
    底_顶 = 3,
    顶_底 = 4
}

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class UGUIAnchor : MonoBehaviour {
    public UGUIAnchorTypeHorizontal horizontalAlignment;
    public UGUIAnchorTypeVertical verticalAlignment;
    public RectTransform targetRectTransform;
    private RectTransform selfRectTransform;

    public bool isInit;
    [SerializeField]
    private Vector3 recordSelfPosition;
    [SerializeField]
    private Rect recordTargetRect;
    [SerializeField]
    private Vector2 recordSelfRect;
    [SerializeField]
    private float recordOffsetX;
    [SerializeField]
    private float recordOffsetY;

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
            recordSelfPosition = selfRectTransform.transform.localPosition;
            recordTargetRect.width = targetRectTransform.rect.width;
            recordTargetRect.height = targetRectTransform.rect.height;
            recordTargetRect.position = targetRectTransform.transform.localPosition;

            recordSelfRect.x = selfRectTransform.rect.width;
            recordSelfRect.y = selfRectTransform.rect.height;

            recordOffsetX = selfRectTransform.transform.localPosition.x - targetRectTransform.transform.localPosition.x;
            recordOffsetY = selfRectTransform.transform.localPosition.y - targetRectTransform.transform.localPosition.y;
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

            if (horizontalAlignment != UGUIAnchorTypeHorizontal.忽略)
            {
                var recordTargetHalf = recordTargetRect.width * targetRectTransform.pivot.x;
                var currentHalf = targetRectTransform.rect.width * targetRectTransform.pivot.x;
                var offsetX = targetRectTransform.localPosition.x - recordTargetRect.position.x;

                if (horizontalAlignment == UGUIAnchorTypeHorizontal.左_左)
                {
                    recordTargetHalf = recordTargetRect.width * targetRectTransform.pivot.x;
                    currentHalf = targetRectTransform.rect.width * targetRectTransform.pivot.x;
                    var changet = recordTargetHalf - currentHalf;

                    var recordSelfRate = recordSelfRect.x * selfRectTransform.pivot.x;
                    var currentSelfRate = selfRectTransform.rect.width * selfRectTransform.pivot.x;
                    var selfChange = recordSelfRate + currentSelfRate;

                    offsetX += changet + selfChange;

                    transform.localPosition = new Vector3(offsetX, transform.localPosition.y, selfRectTransform.localPosition.z) + new Vector3(recordOffsetX, 0, 0);
                }
                else if (horizontalAlignment == UGUIAnchorTypeHorizontal.右_右)
                {
                    recordTargetHalf = recordTargetRect.width * (1 - targetRectTransform.pivot.x);
                    currentHalf = targetRectTransform.rect.width * (1 - targetRectTransform.pivot.x);
                    var changet = currentHalf - recordTargetHalf;

                    var recordSelfRate = recordSelfRect.x * (1 - selfRectTransform.pivot.x);
                    var currentSelfRate = selfRectTransform.rect.width * (1 - selfRectTransform.pivot.x);
                    var selfChange = recordSelfRate - currentSelfRate;

                    offsetX += changet + selfChange;
                    transform.localPosition = new Vector3(offsetX, transform.localPosition.y, selfRectTransform.localPosition.z) + new Vector3(recordOffsetX, 0, 0);
                }
                else if (horizontalAlignment == UGUIAnchorTypeHorizontal.左_右)
                {
                    recordTargetHalf = recordTargetRect.width * (1 - targetRectTransform.pivot.x);
                    currentHalf = targetRectTransform.rect.width * (1 - targetRectTransform.pivot.x);
                    var changet = currentHalf - recordTargetHalf;

                    var recordSelfRate = recordSelfRect.x * selfRectTransform.pivot.x;
                    var currentSelfRate = selfRectTransform.rect.width * selfRectTransform.pivot.x;
                    var selfChange = recordSelfRate + currentSelfRate;

                    offsetX += changet + selfChange;

                    transform.localPosition = new Vector3(offsetX, transform.localPosition.y, selfRectTransform.localPosition.z) + new Vector3(recordOffsetX, 0, 0);
                }
                else if (horizontalAlignment == UGUIAnchorTypeHorizontal.右_左)
                {
                    recordTargetHalf = recordTargetRect.width * targetRectTransform.pivot.x;
                    currentHalf = targetRectTransform.rect.width * targetRectTransform.pivot.x;
                    var changet = recordTargetHalf - currentHalf;

                    var recordSelfRate = recordSelfRect.x * (1 - selfRectTransform.pivot.x);
                    var currentSelfRate = selfRectTransform.rect.width * (1 - selfRectTransform.pivot.x);
                    var selfChange = recordSelfRate - currentSelfRate;

                    offsetX += changet + selfChange;

                    transform.localPosition = new Vector3(offsetX, transform.localPosition.y, selfRectTransform.localPosition.z) + new Vector3(recordOffsetX, 0, 0);
                }
            }


            if (verticalAlignment != UGUIAnchorTypeVertical.忽略)
            {
                var offsetY = targetRectTransform.localPosition.y - recordTargetRect.position.y;

                if (verticalAlignment == UGUIAnchorTypeVertical.顶_顶)
                {
                    var recordTargetHalf = recordTargetRect.height * (1 - targetRectTransform.pivot.y);
                    var currentHalf = targetRectTransform.rect.height * (1 - targetRectTransform.pivot.y);
                    var changet = recordTargetHalf + currentHalf;

                    var recordSelfRate = recordSelfRect.y * (1 - selfRectTransform.pivot.y);
                    var currentSelfRate = selfRectTransform.rect.height * (1 - selfRectTransform.pivot.y);
                    var selfChange = recordSelfRate - currentSelfRate;

                    offsetY += changet + selfChange;

                    transform.localPosition = new Vector3(transform.localPosition.x, offsetY, selfRectTransform.localPosition.z) + new Vector3(0, recordOffsetY, 0);
                }
                else if (verticalAlignment == UGUIAnchorTypeVertical.底_底)
                {
                    var recordTargetHalf = recordTargetRect.height * targetRectTransform.pivot.y;
                    var currentHalf = targetRectTransform.rect.height * targetRectTransform.pivot.y;
                    var changet = recordTargetHalf - currentHalf;

                    var recordSelfRate = recordSelfRect.y * selfRectTransform.pivot.y;
                    var currentSelfRate = selfRectTransform.rect.height * selfRectTransform.pivot.y;
                    var selfChange = recordSelfRate + currentSelfRate;

                    offsetY += changet + selfChange;

                    transform.localPosition = new Vector3(transform.localPosition.x, offsetY, selfRectTransform.localPosition.z) + new Vector3(0, recordOffsetY, 0);
                }
                else if (verticalAlignment == UGUIAnchorTypeVertical.顶_底)
                {
                    var recordTargetHalf = recordTargetRect.height * targetRectTransform.pivot.y;
                    var currentHalf = targetRectTransform.rect.height * targetRectTransform.pivot.y;
                    var changet = recordTargetHalf - currentHalf;

                    var recordSelfRate = recordSelfRect.y * (1 - selfRectTransform.pivot.y);
                    var currentSelfRate = selfRectTransform.rect.height * (1 - selfRectTransform.pivot.y);
                    var selfChange = recordSelfRate - currentSelfRate;

                    offsetY += changet + selfChange;

                    transform.localPosition = new Vector3(transform.localPosition.x, offsetY, selfRectTransform.localPosition.z) + new Vector3(0, recordOffsetY, 0);
                }
                else if (verticalAlignment == UGUIAnchorTypeVertical.底_顶)
                {
                    var recordTargetHalf = recordTargetRect.height * (1 - targetRectTransform.pivot.y);
                    var currentHalf = targetRectTransform.rect.height * (1 - targetRectTransform.pivot.y);
                    var changet = recordTargetHalf + currentHalf;

                    var recordSelfRate = recordSelfRect.y * selfRectTransform.pivot.y;
                    var currentSelfRate = selfRectTransform.rect.height * selfRectTransform.pivot.y;
                    var selfChange = recordSelfRate + currentSelfRate;

                    offsetY += changet + selfChange;

                    transform.localPosition = new Vector3(transform.localPosition.x, offsetY, selfRectTransform.localPosition.z) + new Vector3(0, recordOffsetY, 0);
                }
            }
        }
    }

    private Vector3 LocalPositionToPosition(Transform parent, Vector3 localPosition)
    {
        var parentPosition = Vector3.one;
        
        if (parent != null)
        {
            parentPosition = new Vector3(parent.localScale.x * localPosition.x, parent.localScale.y * localPosition.y, parent.localScale.z * localPosition.z);
        }
        
        if (parent.parent != null)
        {
            return LocalPositionToPosition(parent.parent, parentPosition);
        }

        return parentPosition;
    }
}

