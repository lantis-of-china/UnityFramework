using UnityEngine;
using System.Collections;
using System.IO;

namespace CherishExe
{

    // 资源目录类型 //
    public enum ePathType
    {
        None = -1,
        ConfigPathType = 0,
        WorldPathType = 1,
        UIPathType = 2,
        FontPathType = 3,
        CharacterMoudle = 4,
        CharacterMatral = 5,
        CharacterTexture = 6,
        CharacterAnimatorController = 7,
        CharacterAnimationClip = 8,
        SkillCallEffect = 9,
        SkillReadyEffect = 10,
        HitEffect = 11,
        BufReadyEffect = 12,
        BufStateEffect = 13,
        FullSenceEffect = 14,
        EffectAudio = 15,
        SenceAudio = 16,
        NpcAudio = 17,
        HudType = 18,
        ThingIcon = 19,
        Audio = 20,
        Dll = 21,
        Pdb = 22,
        Mdb = 23
    }



    public class CherishExeAssetsPathManager
    {
        /// <summary>
        /// 获取 WWW 加载的时候使用文件协议
        /// </summary>
        /// <returns></returns>
        public static string GetFileProtocol()
        {
            string _fileSystem = "";
#if (UNITY_ANDROID&&!UNITY_EDITOR)
        _fileSystem="jar:file://";
#elif (UNITY_IPHONE&&!UNITY_EDITOR)
        _fileSystem="";
#elif (UNITY_ANDROID&&UNITY_EDITOR)
            _fileSystem = "file:///";
#elif (UNITY_IPHONE&&UNITY_EDITOR)
		_fileSystem="file://";
#elif (UNITY_EDITOR)
        _fileSystem = "file://";
#else
        _fileSystem = "file://";
#endif
            return _fileSystem;
        }

        /// <summary>
        /// 工程内部文件夹结构目录
        /// </summary>
        /// <returns></returns>
        public static string GetProjectPathNode()
        {
            string _projectPath = "";

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
                {
                    _projectPath = Application.dataPath + "/StreamingAssets";
                }
                else
                {
                    _projectPath = Application.dataPath + "/StreamingAssets";
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
                {
                    _projectPath = Application.dataPath + "/../../../QiPai/Assets/StreamingAssets";
                }
                else
                {
                    _projectPath = Application.dataPath + "/../../../QiPai/Assets/StreamingAssets";
                }
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
                {
                    _projectPath = Application.dataPath + "/StreamingAssets";
                }
                else
                {
                    _projectPath = Application.dataPath + "/StreamingAssets";
                }
            }
            else if (Application.platform == RuntimePlatform.OSXPlayer)
            {
                if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
                {
                    _projectPath = Application.dataPath + "/StreamingAssets";
                }
                else
                {
                    _projectPath = Application.dataPath + "/StreamingAssets";
                }
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                _projectPath = Application.streamingAssetsPath;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _projectPath = Application.streamingAssetsPath;
            }

            return _projectPath.Replace("\\", "/");
        }

        /// <summary>
        /// 获取外部路径
        /// </summary>
        /// <returns></returns>
        public static string GetExternPathNode()
        {
            return Application.persistentDataPath.Replace("\\", "/");
        }

        /// <summary>
        /// 获取平台对应的资源目录名
        /// </summary>
        /// <returns></returns>
        public static string GetPlatform()
        {
            string _platformNodeName = "";
#if (UNITY_ANDROID)
            _platformNodeName = "/AndroidAssets/";
#elif(UNITY_IPHONE)
        _platformNodeName="/IosAssets/";
#else
        _platformNodeName = "/WindowsAssets/";
#endif

            return _platformNodeName;
        }


        public static RuntimePlatform GetPlatformType()
        {            
#if (UNITY_ANDROID)            
            return RuntimePlatform.Android;
#elif (UNITY_IPHONE)
        return RuntimePlatform.IPhonePlayer;
#else
        return RuntimePlatform.WindowsEditor;
#endif
        }

        /// <summary>
        /// 获取局部路径 对应资源类型
        /// </summary>
        /// <param name="_assetsType"></param>
        /// <returns></returns>
        public static string GetAssetLocalPathWithAssetsType(ePathType _assetsType)
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
        public static string GetAssetsSuffix(ePathType _assetsType)
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
                case ePathType.Dll:
                    if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
                    {
                        _suffix = ".data";
                    }
                    else
                    {
                        _suffix = ".data";
                    }
                    break;
                case ePathType.Pdb:
                    _suffix = ".pdb";
                    break;
                case ePathType.Mdb:
                    _suffix = ".mdb";
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
        public static string GetFilePathWithTypeFromWWW(string _fileName, ePathType _assetsType)
        {
            string _filePath = GetExternPathNode() + GetPlatform() + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);

            if (System.IO.File.Exists(_filePath))
            {
                _filePath = GetFileProtocol() + _filePath;

                return _filePath;
            }
            else
            {
                _filePath = GetProjectPathNode() + GetPlatform() + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);

                if (!System.IO.File.Exists(_filePath))
                {
                    Debug.LogError("资源不存在 " + _filePath);
                }

                _filePath = GetFileProtocol() + _filePath;

                return _filePath;
            }
        }

        /// <summary>
        /// 加载文件路径
        /// </summary>
        /// <param name="_fileName"></param>
        /// <param name="_assetsType"></param>
        /// <returns></returns>
        public static string GetFilePathWithType(string _fileName, ePathType _assetsType)
        {
            string _filePath = GetExternPathNode() + GetPlatform() + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);

            if (System.IO.File.Exists(_filePath))
            {
                return _filePath;
            }
            else
            {
                _filePath = GetProjectPathNode() + GetPlatform() + GetAssetLocalPathWithAssetsType(_assetsType) + _fileName + GetAssetsSuffix(_assetsType);

                if (!System.IO.File.Exists(_filePath))
                {
                    Debug.LogError("资源不存在 " + _filePath);
                }

                return _filePath;
            }
        }

        /// <summary>
        /// 获取资源存储根目录
        /// </summary>
        /// <returns></returns>
        public static byte[] GetAssetsRootFileData(string fileName,ePathType pathType)
        {
            string _filePath = GetExternPathNode() + "/" + fileName + GetAssetsSuffix(pathType);

            if (System.IO.File.Exists(_filePath))
            {
                Debug.Log(_filePath);
                byte[] addinStream = null;
                using (System.IO.FileStream fileStream = System.IO.File.OpenRead(_filePath))
                {
                    addinStream = new byte[fileStream.Length];
                    fileStream.Read(addinStream, 0, addinStream.Length);
                };
                return addinStream;
            }
            else
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                _filePath = fileName + GetAssetsSuffix(pathType);
                byte[] addinStream = CherishUtility.GetFileData(_filePath,_filePath);
                return addinStream;
#else
                _filePath = GetProjectPathNode() + "/" + fileName + GetAssetsSuffix(pathType);
                Debug.Log(_filePath);
				byte[] addinStream = null;

                if (System.IO.File.Exists(_filePath))
                {
                    using (System.IO.FileStream fileStream = System.IO.File.OpenRead(_filePath))
                    {
                        addinStream = new byte[fileStream.Length];
                        fileStream.Read(addinStream, 0, addinStream.Length);
                    };
                }

                return addinStream;
            #endif
            }
        }
    }

}
