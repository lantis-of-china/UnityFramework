using UnityEngine;
using System.Collections;
using System.Xml;


/// <summary>
/// 配置基本数据
/// </summary>
public class ConfigDataBase
{
    public string configName;
    public  ConfigDataBase(string projectName,string _configName)
    {
        configName = _configName;

        FrameWorkDrvice.ConfigDataManagerInstance.RegistConfig(projectName, _configName, this);
    }

    /// <summary>
    /// 释放配置
    /// </summary>
    public virtual void ReleseData()
    {
        FrameWorkDrvice.ConfigDataManagerInstance.RemoveData(configName);
    }

    /// <summary>
    /// 创建一个基本数据
    /// </summary>
    /// <param name="_key"></param>
    public virtual void CreateData(string _key)
    {

    }

    /// <summary>
    /// 添加属性
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_value"></param>
    public virtual void AppendAttribute(string _key,string _name,string _value)
    {

    }
}

/// <summary>
/// 配置表管理
/// </summary>
public class ConfigDataManager
{
    /// <summary>
    /// 创建一个配置表map
    /// </summary>
    public System.Collections.Generic.Dictionary<string, ConfigDataBase> configMap = new System.Collections.Generic.Dictionary<string, ConfigDataBase>();
    
    
    /// <summary>
    /// 加载配置表
    /// </summary>
    public void LoaeConfig()
    {

    }


    /// <summary>
    /// 配置表注册
    /// </summary>
    /// <param name="_configName"></param>
    /// <param name="_configInstance"></param>
    public void RegistConfig(string projectName,string _configName,ConfigDataBase _configInstance)
    {
        if(!configMap.ContainsKey(_configName))
        {
            configMap.Add(_configName, _configInstance);

            LoadConfig(projectName,_configName);
        }
        else
        {
            DebugLoger.LogError("配置表重复加载 表名 " + _configName);
        }
    }
    

    /// <summary>
    /// 进行资源加载
    /// </summary>
    /// <param name="_configName"></param>
    public void LoadConfig(string projectName,string _configName)
    {
        ///加载配置表
        string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithTypeFromWWW(projectName,_configName, ePathType.ConfigPathType);

        AssetsInfor assetsInforLoad = new AssetsInfor();
        assetsInforLoad.assetsPath = _path;
        assetsInforLoad.assetsName = _configName;
        assetsInforLoad.OnLoadFinishCall = LoadCompleted;
        assetsInforLoad.assetsLoadType = eAssetsLoadType.String;

        FrameWorkDrvice.AssetsManageInstance.AddLoadImmediate(assetsInforLoad);
    }


    /// <summary>
    /// 通过配置表名获取表
    /// </summary>
    /// <param name="_configName"></param>
    /// <returns></returns>
    public ConfigDataBase GetConfigDataBaseFromConfigName(string _configName)
    {
        foreach (var config in configMap.Keys)
        {
            if(config == _configName)
            {
                return configMap[config];
            }
        }

        return null;
    }

    /// <summary>
    /// 加载回调
    /// </summary>
    /// <param name="assetInfor"></param>
    /// <param name="assetsLoadType"></param>
    private void LoadCompleted(AssetsData assetInfor, eAssetsLoadType assetsLoadType)
    {
        string _loadString = assetInfor.assetsInfor.assetsString;
        string _configName = assetInfor.assetsInfor.assetsName;

        ConfigDataBase _dataBase = GetConfigDataBaseFromConfigName(_configName);

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(_loadString);

        XmlNode node = xmlDoc.SelectSingleNode("template");

        XmlNodeList nodeList = node.FirstChild.ChildNodes;

        string key = "";

        string nKey;

        foreach (XmlElement xe in nodeList)
        {
            key = xe.GetAttribute("Key");

            nKey = key;

            _dataBase.CreateData(nKey);

            for (int i = 0; i < xe.Attributes.Count; i++)
            {
                XmlAttribute attr = xe.Attributes[i];

                _dataBase.AppendAttribute(nKey, attr.Name, attr.Value);
            }
        }
    }


    private void GetConfigData(string _configName)
    {

    }

    /// <summary>
    /// 移除数据
    /// </summary>
    /// <param name="removeItem"></param>
    public void RemoveData(ConfigDataBase removeItem)
    {
        if(configMap.ContainsValue(removeItem))
        {
            foreach(var kv in configMap)
            {
                if(kv.Value == removeItem)
                {
                    configMap.Remove(kv.Key);
                }
            }
        }
    }

    /// <summary>
    /// 移除数据
    /// </summary>
    /// <param name="configName"></param>
    public void RemoveData(string configName)
    {
        if (configMap.ContainsKey(configName))
        {
            configMap.Remove(configName);
        }
    }
}
