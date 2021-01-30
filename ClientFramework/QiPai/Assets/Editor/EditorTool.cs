using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class EditorTool
{
    [MenuItem("Assets/SetMd5/PlayerPrefsDeleteAll")]
    public static void ClearnData()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Assets/SetMd5/解密")]
    public static void UnEncryptionMd5()
    {
        string parkPath = EditorUtility.OpenFilePanel("select encryption file", Application.streamingAssetsPath, "text");

        byte[] encryptionBuf = File.ReadAllBytes(parkPath);

        encryptionBuf = CompressEncryption.UnEncryption(encryptionBuf);

        File.WriteAllBytes(parkPath, encryptionBuf);
    }

    [MenuItem("Assets/SetMd5/加密")]
    public static void EncryptionMd5()
    {
        string parkPath = EditorUtility.OpenFilePanel("select encryption file", Application.streamingAssetsPath, "text");

        byte[] encryptionBuf = File.ReadAllBytes(parkPath);

        encryptionBuf = CompressEncryption.Encryption(encryptionBuf);

        File.WriteAllBytes(parkPath, encryptionBuf);
    }

    [MenuItem("Assets/SetMd5/加密选中")]
    static void CompressAssets()
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

                bufs = CompressEncryption.Encryption(bufs);

                File.WriteAllBytes(assetsPath, bufs);
                Debug.Log("压缩完成 " + assetsPath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("压缩过程出现一个问题 " + e.ToString());
            }
        }
    }

    [MenuItem("Assets/SetMd5/解密选中")]
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




    [MenuItem("Copycode/CopyFloaderFromTo")]
    public static void CopyCodeToEditor()
    {
        string fromKey = "copyCodeFrom";
        string toKey = "copyCodeto";
        string fromPath = "";
        string toPath = "";
        if (EditorPrefs.HasKey(fromKey))
        {
            fromPath = EditorPrefs.GetString(fromKey);
        }
        else
        {
            fromPath = EditorUtility.OpenFolderPanel("select from floader", Application.streamingAssetsPath, "");
            EditorPrefs.SetString(fromKey, fromPath);
        }

        if (EditorPrefs.HasKey(toKey))
        {
            toPath = EditorPrefs.GetString(toKey);
        }
        else
        {
            toPath = EditorUtility.OpenFolderPanel("select to floader", Application.streamingAssetsPath, "");
            EditorPrefs.SetString(toKey, toPath);
        }

        if(string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath))
        {
            ClearCopyPrefs();
        }
        else
        {
            CopyDirectory(fromPath,toPath);

            AssetDatabase.Refresh();
        }
    }

    [MenuItem("Copycode/ClearPath")]
    public static void ClearCopyPrefs()
    {
        EditorPrefs.DeleteKey("copyCodeFrom");
        EditorPrefs.DeleteKey("copyCodeto");
    }

    /// <summary>
    /// 清理全部数据
    /// </summary>
    [MenuItem("EditorTools/ResetAllData")]
    public static void ResetAllData()
    {
        if (Directory.Exists(Application.persistentDataPath))
        {
            Directory.Delete(Application.persistentDataPath, true);
        }

        Debug.Log("清理路径 " + Application.persistentDataPath);
    }

	/// <summary>
	/// 开启缓存路径
	/// </summary>
	[MenuItem("EditorTools/OpenPersistentPath")]
	public static void OpenPersistentPath()
	{
		if (Directory.Exists(Application.persistentDataPath))
		{
			EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
			return;
		}

		Debug.Log("路径不存在 " + Application.persistentDataPath);
	}

	/// <summary>
	/// 拷贝文件夹
	/// </summary>
	/// <param name="srcdir"></param>
	/// <param name="desdir"></param>
	public static void CopyDirectory(string srcdir, string desdir)
    {
        string folderName = srcdir.Substring(srcdir.LastIndexOf("/") + 1);

        string desfolderdir = desdir + "/" + folderName;

        if (desdir.LastIndexOf("/") == (desdir.Length - 1))
        {
            desfolderdir = desdir + folderName;
        }
        string[] filenames = Directory.GetFileSystemEntries(srcdir);
        int loopCount = 0;
        foreach (string file in filenames)// 遍历所有的文件和目录
        {
            string filePath = file.Replace("\\", "/");
            if (Directory.Exists(filePath))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
            {

                string currentdir = desfolderdir + "/" + filePath.Substring(filePath.LastIndexOf("/") + 1);
                if (!Directory.Exists(currentdir))
                {
                    Directory.CreateDirectory(currentdir);
                }

                CopyDirectory(filePath, desfolderdir);
            }

            else // 否则直接copy文件
            {
                string srcfileName = filePath.Substring(filePath.LastIndexOf("/") + 1);

                srcfileName = desfolderdir + "/" + srcfileName;


                if (!Directory.Exists(desfolderdir))
                {
                    Directory.CreateDirectory(desfolderdir);
                }

                if(File.Exists(srcfileName))
                {
                    File.SetAttributes(srcfileName, FileAttributes.Normal);
                }
                File.Copy(filePath, srcfileName,true);
            }
        }
    }
}
