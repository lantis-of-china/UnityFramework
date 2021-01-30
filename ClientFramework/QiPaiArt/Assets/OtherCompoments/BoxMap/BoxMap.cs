using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxMap : MonoBehaviour
{
    public bool startInit = true;
    [SerializeField]
    public List<BoxMapContidion> boxMapContidions = new List<BoxMapContidion>();
    public float height = 10.0f;
    public int xCount = 100;
    public int yCount = 100;
    public float scale = 1.0f;
    public float relief = 15.0f;
    [SerializeField]
    public List<BoxMapListWrap> boxMapNodes = new List<BoxMapListWrap>();

#if UNITY_EDITOR
    void OnValidate()
    {
        Init();
        UpAll();
    }
#endif

    void Awake()
    {
        Init();
    }

    void Init()
    {
        if (startInit)
        {
            startInit = false;
            Clear();
            Spawn();
        }
    }

    void Clear()
    {
        for (var x = 0; x < boxMapNodes.Count; ++x)
        {
            for (var y = 0; y < boxMapNodes[x].datas.Count; ++y)
            {
                boxMapNodes[x].datas[y].Destroy();
            }
        }

        boxMapNodes.Clear();
    }

    void Spawn()
    {
        for (var x = 0; x < xCount; ++x)
        {
            boxMapNodes.Add(new BoxMapListWrap());

            for (var y = 0; y < yCount; ++y)
            {
                    var boxNode = new BoxMapNode()
                    {
                        x = x,
                        y = y,                       
                    };

                    boxMapNodes[x].datas.Add(boxNode);                
            }
        }

        for (var x = 0; x < xCount; ++x)
        {
            for (var y = 0; y < yCount; ++y)
            {
                boxMapNodes[x].datas[y].Load(gameObject, boxMapContidions, height,scale, relief);
            }
        }
    }

    void UpAll()
    {
        for (var x = 0; x < boxMapNodes.Count; ++x)
        {
            for (var y = 0; y < boxMapNodes[x].datas.Count; ++y)
            {
                boxMapNodes[x].datas[y].SetNotices(boxMapContidions, height, scale, relief);
            }
        }
    }

    public BoxMapNode GetBoxMapNode(float x, float y)
    {
        var xpos = x / scale;
        var ypos = x / scale;

        var xIndex = (int)Mathf.Floor(xpos);
        var yIndex = (int)Mathf.Floor(ypos);

        if (xIndex < boxMapNodes.Count && yIndex < boxMapNodes[xIndex].datas.Count)
        {
            return boxMapNodes[xIndex].datas[yIndex];
        }

        return null;
    }
}

[Serializable]
public class BoxMapListWrap
{
    public List<BoxMapNode> datas = new List<BoxMapNode>();
}

[Serializable]
public class BoxMapContidion
{
    public float min;
    public float max;
    public float offsetY;
    public GameObject prefab;
}

[Serializable]
public class BoxMapNode
{
    public int x;
    public int y;
    public float notices;
    public GameObject node;
    public BoxMapContidion boxMapContidion;

    public void Load(GameObject parent, List<BoxMapContidion> boxMapContidions, float height, float scale,float relief)
    {
        if (boxMapContidion == null)
        {
            notices = Mathf.PerlinNoise(x / relief, y / relief);
            boxMapContidion = FindBoxMapContidion(boxMapContidions);
        }

        if (boxMapContidion != null)
        {
            node = GameObject.Instantiate(boxMapContidion.prefab);
            node.name = $"node_{x}_{y}";
            node.transform.SetParent(parent.transform);
            SetNotices(boxMapContidions,height,scale,relief);
        }
    }

    public void SetNotices(List<BoxMapContidion> boxMapContidions, float height, float scale, float relief)
    {
        if (boxMapContidion == null)
        {
            boxMapContidion = FindBoxMapContidion(boxMapContidions);
        }

        if (boxMapContidion != null)
        {
            notices = Mathf.PerlinNoise(x / relief, y / relief);
            node.transform.localScale = Vector3.one * scale;
            node.transform.localPosition = new Vector3(x * scale, (notices * height + boxMapContidion.offsetY) * scale, y * scale);
        }
    }

    public BoxMapContidion FindBoxMapContidion(List<BoxMapContidion> boxMapContidions)
    {
        for (var i = 0; i < boxMapContidions.Count; ++i)
        {
            if (boxMapContidions[i].min < notices && notices <= boxMapContidions[i].max)
            {
                return boxMapContidions[i];
            }
        }

        return null;
    }

    public void Destroy()
    {
        if (node != null)
        {
            GameObject.Destroy(node);
        }
    }
}
