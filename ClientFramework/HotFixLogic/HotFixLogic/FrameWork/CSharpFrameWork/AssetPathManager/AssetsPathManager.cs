﻿using UnityEngine;
using System.Collections;
using CherishExe;


// 资源目录类型 //
public enum ePathType
{  
    ConfigPathType=0,
    WorldPathType=1,
    UIPathType=2,
    FontPathType=3,
    CharacterMoudle=4,
    CharacterMatral=5,
    CharacterTexture=6,
    CharacterAnimatorController=7,
    CharacterAnimationClip=8,
    SkillCallEffect=9,
    SkillReadyEffect=10,
    HitEffect=11,
    BufReadyEffect=12,
    BufStateEffect=13,
    FullSenceEffect=14,
    EffectAudio=15,
    SenceAudio=16,
    NpcAudio=17,
    HudType = 18,
    ThingIcon = 19,
    Audio = 20,
    Items = 21,
    FindPath = 22
}



public class AssetsPathManager
{
    /// <summary>
    /// 获取 WWW 加载的时候使用文件协议
    /// </summary>
    /// <returns></returns>
    public string GetFileProtocol()
    {
        string _fileSystem = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            _fileSystem = "file:///";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _fileSystem = "file://";
        }
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			_fileSystem = "";
		}
		else
        {
            _fileSystem = "file://";
        }

        return _fileSystem;
    }

    /// <summary>
    /// 工程内部文件夹结构目录
    /// </summary>
    /// <returns></returns>
    public string GetProjectPathNode()
    {
        string _projectPath = "";

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            _projectPath = Application.dataPath + "/../../" + "QiPaiArt/Assets/StreamingAssets";
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            _projectPath = Application.dataPath + "/../../../" + "QiPaiArt/Assets/StreamingAssets";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            _projectPath= Application.streamingAssetsPath;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _projectPath= Application.streamingAssetsPath;
        }
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			_projectPath = "http://127.0.0.1/";
		}

		return _projectPath.Replace("\\","/");
    }

    /// <summary>
    /// 获取外部路径
    /// </summary>
    /// <returns></returns>
    public string GetExternPathNode()
    {
        return Application.persistentDataPath.Replace("\\","/");
    }

    /// <summary>
    /// 获取平台对应的资源目录名
    /// </summary>
    /// <returns></returns>
    public static string GetPlatform()
    {        
        string _platformNodeName = "";

        if(CherishExeAssetsPathManager.GetPlatformType() == RuntimePlatform.Android)
        {
            _platformNodeName="/AndroidAssets/";
        }
        else if(CherishExeAssetsPathManager.GetPlatformType() == RuntimePlatform.IPhonePlayer)
        {
            _platformNodeName="/IosAssets/";
        }
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			_platformNodeName = "/WebGLAssets/";
		}
		else
        {
            if(CherishExeAssetsPathManager.GetPlatformType() == RuntimePlatform.WindowsEditor)
            _platformNodeName = "/WindowsAssets/";
        }

        return _platformNodeName;
    }

    /// <summary>
    /// 获取局部路径 对应资源类型
    /// </summary>
    /// <param name="_assetsType"></param>
    /// <returns></returns>
    public string GetAssetLocalPathWithAssetsType(ePathType _assetsType)
    {
        string _localPath = "";

        switch (_assetsType)
        {
            case ePathType.ConfigPathType:
                _localPath = "Config/";
                break;
            case ePathType.UIPathType:
                _localPath = "UI/";
                break;
            case ePathType.FontPathType:
                _localPath = "Font/";
                break;
            case ePathType.WorldPathType:
                _localPath = "Sences/";
                break;
            case ePathType.CharacterMoudle:
                _localPath = "CharacterMoudle/";
                break;
            case ePathType.CharacterMatral:
                _localPath = "CharacterMaterials/";
                break;
            case ePathType.CharacterTexture:
                _localPath = "CharacterTexture/";
                break;
            case ePathType.CharacterAnimatorController:
                _localPath = "AnimatorController/";
                break;
            case ePathType.CharacterAnimationClip:
                _localPath = "CharacterAnimationClip/";
                break;
            case ePathType.SkillReadyEffect:
                _localPath = "SkillReadyEffect/";
                break;
            case ePathType.SkillCallEffect:
                _localPath = "SkillCallEffect/";
                break;
            case ePathType.HitEffect:
                _localPath = "HitEffect/";
                break;
            case ePathType.FullSenceEffect:
                _localPath = "FullSenceEffect/";
                break;
            case ePathType.BufReadyEffect:
                _localPath = "BufReadyEffect/";
                break;
            case ePathType.BufStateEffect:
                _localPath = "BufStateEffect/";
                break;
            case ePathType.HudType:
                _localPath = "HUD/";
                break;
            case ePathType.ThingIcon:
                _localPath = "ThingIcons/";
                break;
            case ePathType.Audio:
                _localPath = "Sound/";
                break;
            case ePathType.Items:
                _localPath = "Items/";
                break;
            case ePathType.FindPath:
                _localPath = "FindPath/";
                break;
            default:
                break;
        }

        return _localPath;
    }
    
    /// <summary>
    /// 获取后缀名
    /// </summary>
    /// <param name="_assetsType"></param>
    /// <returns></returns>
    public string GetAssetsSuffix(ePathType _assetsType)
    {
        string _suffix = ".data";

        switch (_assetsType)
        {
            case ePathType.FontPathType:
                _suffix = ".data";
                break;
            case ePathType.UIPathType:
                _suffix = ".data";
                break;
            case ePathType.ConfigPathType:
                _suffix = ".xml";
                break;
            case ePathType.FindPath:
                _suffix = ".AS";
                break;
        }

        return _suffix;
    }
    
    /// <summary>
    /// 通过www加载文件
    /// </summary>
    /// <param name="_fileName"></param>
    /// <param name="_assetsType"></param>
    /// <returns></returns>
    public string GetFilePathWithTypeFromWWW(string projectName, string _fileName, ePathType _assetsType)
    {
        string _filePath = GetExternPathNode() + GetPlatform() + projectName + "/" + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);

        if (System.IO.File.Exists(_filePath))
        {
            _filePath = GetFileProtocol() + _filePath;

            return _filePath;
        }
        else
        {
            _filePath = GetProjectPathNode() + GetPlatform() + projectName + "/" + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);
            
            if(Application.platform != RuntimePlatform.Android)
            {
                _filePath = GetFileProtocol() + _filePath;
            }           

            return _filePath;
        }
    }
    
    /// <summary>
    /// 加载文件路径
    /// </summary>
    /// <param name="_fileName"></param>
    /// <param name="_assetsType"></param>
    /// <returns></returns>
    public string GetFilePathWithType(string projectName,string _fileName, ePathType _assetsType)
    {
        string _filePath = GetExternPathNode() + GetPlatform() + projectName + "/" + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);
        if (System.IO.File.Exists(_filePath))
        {
            return _filePath;
        }
        else
        {
            _filePath = GetProjectPathNode() + GetPlatform() + projectName + "/" + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);
            if (!System.IO.File.Exists(_filePath))
            {
                Debug.LogError("资源不存在 " + _filePath);
            }

            return _filePath;
        }
    }
}