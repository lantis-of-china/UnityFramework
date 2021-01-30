using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AssetsItemLoader
{
    public static Dictionary<string, Dictionary<string,AssetsParkManager>> parksMap = new Dictionary<string, Dictionary<string,AssetsParkManager>>();

    public static void LoadParkWithNames(string projectName,string[] itemNames)
    {
        for(int i = 0;i < itemNames.Length;++i)
        {
            LoadParkWithName(projectName, itemNames[i]);
        }
    }

    /// <summary>
    /// 加载包和名字
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="itemName"></param>
    public static void LoadParkWithName(string projectName, string itemName)
    {
        Dictionary<string,AssetsParkManager> curParn = null;
        if (parksMap.ContainsKey(projectName))
        {
            curParn = parksMap[projectName];
        }
        else
        {
            curParn = new Dictionary<string,AssetsParkManager>();
            parksMap.Add(projectName, curParn);
        }

        if (!curParn.ContainsKey(itemName))
        {
            AssetsParkManager itemPark = new AssetsParkManager();
            curParn.Add(itemName, itemPark);
            itemPark.LoadParkPrefab(projectName, itemName, ePathType.Items);
        }
    }

    /// <summary>
    /// 获取预制体
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public static GameObject GetPrefab(string projectName, string itemName)
    {
        Dictionary<string, AssetsParkManager> curParn = null;

        if (parksMap.ContainsKey(projectName))
        {
            curParn = parksMap[projectName];

            if(curParn.ContainsKey(itemName))
            {
                AssetsData assetsInfo = curParn[itemName].GetAssetInfo();

                return GameObject.Instantiate(assetsInfo.assetsData as GameObject);
            }
        }

        return null;
    }

    public static void Dispose(string projectName)
    {
        Dictionary<string, AssetsParkManager> curParn = null;

        if (parksMap.ContainsKey(projectName))
        {
            curParn = parksMap[projectName];

            foreach (var kv in curParn)
            {
                kv.Value.Dispose();
            }

            parksMap.Remove(projectName);
        }
    }
}

