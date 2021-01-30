using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUColorGrayGroup : MonoBehaviour
{
    [SerializeField]
    private bool isGray;
    [SerializeField]
    private List<UGUIImageColorGray> colorGrayList;


#if UNITY_EDITOR
    void Reset()
    {
    }

    void OnValidate()
    {
        SetColorGray(isGray);
    }
#endif

    public void SetColorGray(bool setGray)
    {
        for (var i = 0; i < colorGrayList.Count; ++i)
        {
            if (colorGrayList[i] != null)
            {
                colorGrayList[i].SetColor(setGray);
            }
        }
    }
}
