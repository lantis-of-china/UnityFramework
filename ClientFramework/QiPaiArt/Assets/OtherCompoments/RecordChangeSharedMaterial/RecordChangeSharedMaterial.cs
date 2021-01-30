using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordChangeSharedMaterial : MonoBehaviour
{

	public Material[] sharedMaterials;
	// Use this for initialization
	void Awake()
	{
		MeshRenderer mr = GetComponent<MeshRenderer>();
		if (mr != null)
		{
			sharedMaterials = mr.sharedMaterials;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (sharedMaterials != null && sharedMaterials.Length > 0)
		{
			MeshRenderer mr = GetComponent<MeshRenderer>();
			if (mr != null)
			{
				mr.sharedMaterials = sharedMaterials;
			}
		}
	}

	void LateUpdate()
	{
		Update();
	}
}
