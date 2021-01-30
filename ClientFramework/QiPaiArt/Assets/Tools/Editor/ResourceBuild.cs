using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ResourceBuild
{
	/// <summary>
	/// 获取当前编译平台
	/// </summary>
	/// <returns></returns>
	static public BuildTarget GetBuildTarget ()
	{       
#if UNITY_ANDROID
		return BuildTarget.Android;

#elif UNITY_IPHONE
        return BuildTarget.iOS;
#elif UNITY_WEBGL
		return BuildTarget.WebGL;
#else
        return BuildTarget.StandaloneWindows;

#endif
	}

	[MenuItem ("Assets/BuildTool/MakeHotFix")]
	static void MakeHotFix ()
	{
		//平台
		string platformToPath = "/Platform_Windows";

		string platformAssetsPath = MakeHotFixBuild.PlatformFloder ();
		string hotfixConfigPath = Application.dataPath + "\\..\\../HotFixConfig";
		string versionPath = hotfixConfigPath + platformToPath + "/Version.txt";
		string resourceVersionPath = hotfixConfigPath + platformToPath + "/ResourceVersion.txt";
		string assetRootPath = Application.streamingAssetsPath;

		MakeHotFixBuild.SpawnMD5UpVersionFile (versionPath, resourceVersionPath, assetRootPath, hotfixConfigPath, platformToPath, platformAssetsPath, false, true);
	}

	[MenuItem ("Assets/BuildTool/清除NaN")]
	static void ClearnNan ()
	{
		string abFileName = "";
		// 选择的要保存的对象  
		Object[] selection = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);

		for (short loop = 0; loop < selection.Length; ++loop) {
			Object selectionObj = selection [loop];
			UnityEngine.Transform[] _textComp = (selectionObj as GameObject).GetComponentsInChildren<Transform> (true);
			for (short lood = 0; lood < _textComp.Length; ++lood) {
				Vector3 point = _textComp [lood].localPosition;
				if (float.IsNaN (point.x)) {
					point.x = 1;
					UnityEngine.Debug.Log ("清除[" + _textComp [lood].name + "]的NAN");
				}
				if (float.IsNaN (point.y)) {
					point.y = 1;
					UnityEngine.Debug.Log ("清除[" + _textComp [lood].name + "]的NAN");
				}
				if (float.IsNaN (point.z)) {
					point.z = 1;
					UnityEngine.Debug.Log ("清除[" + _textComp [lood].name + "]的NAN");
				}
				_textComp [lood].localPosition = point;
			}
		}
	}

	//在Unity编辑器中添加菜单
	[MenuItem ("Assets/Build AssetBundle From Selection")]
	static void ExportResourceRGB2 ()
	{
		// 打开保存面板，获得用户选择的路径
		string path = EditorUtility.SaveFilePanel ("Save Resource", "", "New Resource", "data");
		if (path.Length != 0) {
			// 选择的要保存的对象
			Object[] selection = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
			//打包
			BuildPipeline.BuildAssetBundle (Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
		}
	}
   

	[MenuItem("Assets/BuildTool/加密/加密")]
	static void CompressAssets ()
	{
		Object[] selection = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		for (short loop = 0; loop < selection.Length; loop++) {
			string savePath = "";
			Object selectionObj = selection [loop];
			string assetsPath = AssetDatabase.GetAssetPath (selectionObj);
			assetsPath = Application.dataPath + "\\../" + assetsPath;
            
			try
            {
				
				byte[] bufs = File.ReadAllBytes (assetsPath);
				File.Delete (assetsPath);

				bufs = CompressEncryption.Encryption(bufs);

				File.WriteAllBytes (assetsPath, bufs);
                Debug.Log("压缩完成 " + assetsPath);
            } catch (System.Exception e)
            {
				Debug.LogError ("压缩过程出现一个问题 " + e.ToString ());
			}
		}
	}

    [MenuItem("Assets/BuildTool/加密/解密")]
    static void UnCompressAssets()
    {
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        for (short loop = 0; loop < selection.Length; loop++)
        {
            string savePath = "";
            Object selectionObj = selection[loop];
            string assetsPath = AssetDatabase.GetAssetPath(selectionObj);
            assetsPath = Application.dataPath + "\\../" + assetsPath;

            try
            {

                byte[] bufs = File.ReadAllBytes(assetsPath);
                File.Delete(assetsPath);

                bufs = CompressEncryption.UnEncryption(bufs);

                File.WriteAllBytes(assetsPath, bufs);
                Debug.Log("解压压完成 " + assetsPath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("解压过程出现一个问题 " + e.ToString());
            }
        }
    }

    /// <summary>
    /// 拷贝目录下文件
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="assetsPath"></param>
    /// <param name="sum"></param>
    /// <param name="searchPattern"></param>
    public static void CopyToAll(string projectName, string assetsPath, string sum, string searchPattern)
    {
        string sumParent = "GamePrefab";

        string projectPath = Application.dataPath.Replace("\\", "/"); ;

        string streamingPath = Application.streamingAssetsPath.Replace("\\", "/"); ;

        int assetsFilePos = projectPath.LastIndexOf("/");

        projectPath = projectPath.Substring(0, assetsFilePos + 1);

        assetsPath = assetsPath.Replace("\\", "/");

        string[] filePaths = GetFiles($"{ assetsPath }/{sumParent}/{ sum }", searchPattern);

        for (var i = 0; i < filePaths.Length; ++i)
        {
            string filePath = filePaths[i];

            filePath = filePath.Replace("\\", "/");

            string fileName = System.IO.Path.GetFileName(filePath);

            string[] folderArray = filePath.Split('/');

            string upFloder = folderArray[folderArray.Length - 2];

            string savePath = streamingPath + GetPlatform() + projectName + "/" + upFloder;

            savePath = savePath.Substring(projectPath.Length);

            string newProjectPath = projectPath.Replace("/Assets", "");

            savePath = newProjectPath + savePath;

            string sourceAssetsPath = $"{assetsPath}/{sumParent}/{sum}";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            savePath += "/" + fileName;

            sourceAssetsPath += "/" + fileName;

            File.Copy(sourceAssetsPath, savePath, true);
        }
    }

    /// <summary>
    /// 导出单个AB
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="assetsPath"></param>
    /// <param name="sum"></param>
    /// <param name="searchPattern"></param>
    public static void ExporeAssetBundle(string projectName, string assetsPath, string sum, string searchPattern)
    {
        string sumParent = "GamePrefab";

        string projectPath = Application.dataPath.Replace("\\", "/"); ;

        string streamingPath = Application.streamingAssetsPath.Replace("\\", "/"); ;

        int assetsFilePos = projectPath.LastIndexOf("/");

        projectPath = projectPath.Substring(0, assetsFilePos + 1);

        assetsPath = assetsPath.Replace("\\", "/");

        string[] filePaths = GetFiles($"{ assetsPath }/{sumParent}/{ sum }", searchPattern);

        for (var i = 0; i < filePaths.Length; ++i)
        {
            string filePath = filePaths[i];

            filePath = filePath.Replace("\\", "/");

            var targetObject = AssetDatabase.LoadAssetAtPath<Object>(filePath);

            var textFontDic = ClearFont(targetObject);

            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);

            string[] folderArray = filePath.Split('/');

            string upFloder = folderArray[folderArray.Length - 2];

            string savePath = streamingPath + GetPlatform() + projectName + "/" + upFloder;

            savePath = savePath.Substring(projectPath.Length);

            string newProjectPath = projectPath.Replace("/Assets", "");

            savePath = newProjectPath + savePath;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            filePath = filePath.Substring(newProjectPath.Length);

            AssetBundleBuild[] assetBundle = new AssetBundleBuild[1];

            assetBundle[0].assetBundleName = fileName + ".data";

            assetBundle[0].assetNames = new string[] { filePath };

            savePath = savePath.Substring(newProjectPath.Length);

            //打包  
            BuildPipeline.BuildAssetBundles(savePath, assetBundle, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, GetBuildTarget());

            Debug.Log("导出资源: " + savePath + "/" + assetBundle[0].assetBundleName);

            if (textFontDic != null)
            {
                SetFont(targetObject, textFontDic);
            }

            assetBundle = null;
        }
    }

    /// <summary>
    /// 导出全部资源到1个AssetBundle
    /// </summary>
    public static void ExporeAssetBundleAllToOne(string projectName, string assetsPath, string sum, string searchPattern)
    {
        string sumParent = "GamePrefab";

        string projectPath = Application.dataPath.Replace("\\", "/"); ;

        string streamingPath = Application.streamingAssetsPath.Replace("\\", "/"); ;

        int assetsFilePos = projectPath.LastIndexOf("/");

        projectPath = projectPath.Substring(0, assetsFilePos + 1);

        assetsPath = assetsPath.Replace("\\", "/");

        string[] filePaths = GetFiles($"{ assetsPath }/{sumParent}/{ sum }", searchPattern);

        AssetBundleBuild[] assetBundle = new AssetBundleBuild[1];

        List<string> outFilePathList = new List<string>();

        Dictionary<Object, Dictionary<Text, Font>> objectTextFontMaps = new Dictionary<Object, Dictionary<Text, Font>>();

        for (var i = 0; i < filePaths.Length; ++i)
        {
            filePaths[i] = filePaths[i].Replace("\\", "/");

            string filePath = (filePaths[i].Replace("\\", "/")).Substring(projectPath.Replace("/Assets", "").Length);

            var targetObject = AssetDatabase.LoadAssetAtPath<Object>(filePath);

            var textFontDic = ClearFont(targetObject);

            if (textFontDic != null)
            {
                objectTextFontMaps.Add(targetObject, textFontDic);
            }

            outFilePathList.Add(filePath);
        }

        assetBundle[0].assetBundleName = $"{projectName}_{sum}".ToLower() + ".data";

        assetBundle[0].assetNames = outFilePathList.ToArray();

        string savePath = streamingPath + GetPlatform() + projectName + "/" + sum;

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        savePath = savePath.Substring(projectPath.Replace("/Assets", "").Length);

        //打包  
        BuildPipeline.BuildAssetBundles(savePath, assetBundle, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, GetBuildTarget());

        Debug.Log("导出资源: " + savePath + "/" + assetBundle[0].assetBundleName);

        for (var i = 0; i < objectTextFontMaps.Count; ++i)
        {
            var targetFontSet = objectTextFontMaps.ElementAt(i);

            SetFont(targetFontSet.Key, targetFontSet.Value);
        }

        assetBundle = null;
    }

    public static string[] GetFiles(string path,string searchPattern)
    {
        List<string> filePathList = new List<string>();

        var searchArray = searchPattern.Split('|');

        for (var i = 0; i < searchArray.Length; ++i)
        {
            if (string.IsNullOrEmpty(searchArray[i]))
            {
                continue;
            }

            List<string> filePaths = new List<string>();
            GetFile(path, filePaths, searchArray[i]);

            for (var f = 0; f < filePaths.Count; ++f)
            {
                if (!filePathList.Contains(filePaths[f]))
                {
                    filePathList.Add(filePaths[f]);
                }
            }
        }

        return filePathList.ToArray();
    }

    public static List<string> GetFile(string path, List<string> fileList,string searchPattern)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] fil = dir.GetFiles(searchPattern);
        DirectoryInfo[] dii = dir.GetDirectories();

        foreach (FileInfo f in fil)
        {
            long size = f.Length;
            fileList.Add(f.FullName);
        }

        foreach (DirectoryInfo d in dii)
        {
            GetFile(d.FullName, fileList, searchPattern);
        }

        return fileList;
    }

    /// <summary>
    /// 获取平台对应的资源目录名
    /// </summary>
    /// <returns></returns>
    public static string GetPlatform ()
	{
		string _platformNodeName = "";
#if (UNITY_ANDROID)
		_platformNodeName = "/AndroidAssets/";
#elif (UNITY_IPHONE)
        _platformNodeName="/IosAssets/";
#elif (UNITY_WEBGL)
		_platformNodeName= "/WebGLAssets/";
#else
        _platformNodeName = "/WindowsAssets/";
#endif

		return _platformNodeName;
	}

    public static Dictionary<Text, Font> ClearFont(Object clearObject)
    {
        var gameObject = clearObject as GameObject;

        Dictionary<Text, Font> textFontDictionary = new Dictionary<Text, Font>();

        if (gameObject != null)
        {
            UnityEngine.UI.Text[] textComp = gameObject.GetComponentsInChildren<UnityEngine.UI.Text>(true);

            for (short lood = 0; lood < textComp.Length; ++lood)
            {
                UnityEngine.UI.Text text = textComp[lood];

                var fontSet = text.GetComponent<FontBindSet>();

                if (fontSet == null)
                {
                    fontSet = text.gameObject.AddComponent<FontBindSet>();
                }

                if (text.font == null)
                {
                    fontSet.fontName = "default";
                }
                else
                {
                    textFontDictionary.Add(text, text.font);

                    fontSet.fontName = text.font.name;

                    text.font = null;
                }
            }
        }
        return textFontDictionary;
    }

    public static void SetFont(Object setObject, Dictionary<Text, Font> textFontDictionary)
    {
        var gameObject = setObject as GameObject;
        
        if (gameObject != null)
        {
            UnityEngine.UI.Text[] textComp = gameObject.GetComponentsInChildren<UnityEngine.UI.Text>(true);

            for (short lood = 0; lood < textComp.Length; ++lood)
            {
                UnityEngine.UI.Text text = textComp[lood];

                if (textFontDictionary.ContainsKey(text))
                {
                    text.font = textFontDictionary[text];
                }
            }
        }
    }


}
