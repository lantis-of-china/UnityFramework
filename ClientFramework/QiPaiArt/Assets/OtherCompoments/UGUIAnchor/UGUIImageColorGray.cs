using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUIImageColorGray : MonoBehaviour
{
    public bool gray;
    public Color generalColor = Color.black;
    public Image image;

#if UNITY_EDITOR

    void Reset()
    {
    }

    void OnValidate()
    {
        SetColor(gray);
    }

#endif
    public bool Gray
    {
        get
        {
            return gray;
        }

        set
        {
            if (gray != value)
            {
                SetColor(value);
            }

            gray = value;            
        }
    }

    public void SetColor(bool setValue)
    {
        if (setValue)
        {
            generalColor = image.color;
            image.color = Color.black;
        }
        else
        {
            image.color = generalColor;
        }
    }
}
