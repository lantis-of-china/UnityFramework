using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrderLayerSet : MonoBehaviour {
	public int sortingOrder;
	public string sortingLayerName;
	
	/// <summary>
	/// 设置顺序
	/// </summary>
	/// <param name="order"></param>
	public void SetSortingOrder(int order)
	{
		sortingOrder = order;
		RefenceOrder();
	}

	/// <summary>
	/// 刷新排序
	/// </summary>
	public void RefenceOrder()
	{
		Renderer render = gameObject.GetComponent<Renderer>();
		if (render != null)
		{
			render.sortingOrder = sortingOrder;
			if (!string.IsNullOrEmpty(sortingLayerName))
			{
				render.sortingLayerName = sortingLayerName;
			}
		}
	}

	// Use this for initialization
	void Awake ()
	{
		RefenceOrder();
	}	
}
