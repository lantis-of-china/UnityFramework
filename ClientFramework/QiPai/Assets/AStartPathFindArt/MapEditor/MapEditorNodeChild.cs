using UnityEngine;
using System.Collections;

public class MapEditorNodeChild : MonoBehaviour 
{
    public MapEditorNode ParentRoot;

   void Awake()
    {
        MeshRenderer rd = gameObject.GetComponent<MeshRenderer>();

        if (rd != null)
        {
            rd.sharedMaterial = MapEditorNode.Mat_4;
        }
    }
}
