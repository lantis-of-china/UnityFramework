using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkSpriteData : MonoBehaviour
{
	[SerializeField]
	public List<string> keys = new List<string>();
	[SerializeField]
	public List<Image> values = new List<Image>();

	public void Clear()
	{
		keys.Clear();
		values.Clear();
		keys = new List<string>();
		values = new List<Image>();
	}

	public bool ContantHas(string key)
	{
		for (var i = 0; i < keys.Count; ++i)
		{
			if (keys[i] == key)
			{
				return true;
			}
		}

		return false;
	}

	public void AddNewData(string key, Image sprite)
	{
		if (ContantHas(key))
		{
			Debug.LogError("存在重复的Sprite添加");
			return;
		}

		keys.Add(key);
		values.Add(sprite);
	}

	public Image GetData(string key)
	{
		for (var i = 0; i < keys.Count; ++i)
		{
			if (keys[i] == key)
			{
				return values[i];
			}
		}

		return null;
	}
}
