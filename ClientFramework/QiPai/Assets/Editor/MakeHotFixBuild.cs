using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class MakeHotFixBuild
{
    public static string PlayerPrefsKey = "SpawnMD5UpVersionExportPath";
    /// <summary>
    /// 对应平台文件的结尾
    /// </summary>
    /// <returns></returns>
    public static string PlatformExe()
    {
#if UNITY_ANDROID
        return "apk";
#elif UNITY_IPHONE
	return "ipa";
#else
        return "exe";
#endif
    }

    /// <summary>
    /// 获取对应平台的文件夹
    /// </summary>
    /// <returns>The floder.</returns>
    public static string PlatformFloder()
    {
#if UNITY_ANDROID
        return "/AndroidAssets";
#elif UNITY_IPHONE
	return "/IosAssets";
#else
        return "/WindowsAssets";
#endif
    }

    /// <summary>
    /// 设置导出资源路径
    /// </summary>
    /// <returns></returns>
    public static string SetExportPath()
    {
        // 设置导出根目录 //
        string exportPath = PlayerPrefs.GetString(PlayerPrefsKey + "OneKeySampleBuildPath");
        if (string.IsNullOrEmpty(exportPath))
        {
            // 选择需要保存的文件目录 //
            exportPath = EditorUtility.SaveFolderPanel("设置导出目录(需选择web/android/ios这几个目录)", "", "");
            if (string.IsNullOrEmpty(exportPath))
            {
                Debug.LogError("请先选择路径保存。。。");
                return null;
            }

            PlayerPrefs.SetString(PlayerPrefsKey + "OneKeySampleBuildPath", exportPath);
        }

        return exportPath;
    }


    /// <summary>
    /// 生成MD5文件版本文件
    /// </summary>
    /// <param name="versionPath">短版本文件位置</param>
    /// <param name="resourceVersionPath">资源版本文件位置</param>
    /// <param name="assetsRootPath">要读取的资源位置</param>
    /// <param name="hotfoxPath">热更新空间</param>
    /// <param name="plantFloder">平台路径</param>
    /// <param name="systemFloder">操作系统路径</param>
    /// <param name="platformExe">后缀名</param>
    /// <param name="compress">加密</param>
    /// <param name="copyAsset">拷贝资源</param>
    /// <returns></returns>
    public static string SpawnMD5UpVersionFile
        (string versionPath, 
        string resourceVersionPath, 
        string assetsRootPath, 
        string hotfoxPath, 
        string platformPath, 
        string systemFloder, 
        string platformExe,
        bool compress, 
        bool copyAsset)
    {
        string parkPath = "";
        string path = "";
        string clientInfor = "";
        string saveVersionInforPath = "";

        

        // 获取安装包路径
        string exportPath = hotfoxPath;
        if (string.IsNullOrEmpty(exportPath))
        {
            exportPath = SetExportPath();
            int pos = exportPath.LastIndexOf('/');
            exportPath = exportPath.Substring(0, pos + 1);
        }

        parkPath = exportPath + "/Binary/install." + platformExe;

        ////版本号路径
        if (!File.Exists(versionPath))
        {
            Debug.LogError("找不到版本数据 " + versionPath);
            return "none";
        }


        byte[] byteBuf = File.ReadAllBytes(versionPath);
        string versionStr = Encoding.Default.GetString(byteBuf);
        if (versionStr == "")
        {
            versionStr = "0.0.1";
        }

        //第一个作为版本号版本号
        string[] versionInfo = versionStr.Split('.');

        versionStr = versionInfo[0] + "." + versionInfo[1] + "." + versionInfo[2];

        var mdbPath = $"{Application.streamingAssetsPath.Replace('\\','/')}/raw.mdb";
        var pdbPath = $"{Application.streamingAssetsPath.Replace('\\', '/')}/raw.pdb";

        if (File.Exists(mdbPath))
        {
            File.Delete(mdbPath);
        }

        if (File.Exists(pdbPath))
        {
            File.Delete(pdbPath);
        }

        var dllPath = Application.dataPath + "//..//.." + "/GameScriptDll/QiPaiDll/QiPaiDll/bin/Debug/QiPaiDll.dll";
        string outdllPath = hotfoxPath + "/raw.data";
        byte[] byteDll = File.ReadAllBytes(dllPath);

        if (compress)
        {
            byteDll = CompressEncryption.Encryption(byteDll);
        }

        File.WriteAllBytes(outdllPath, byteDll);

        if (copyAsset)
        {
            EditorUtility.DisplayProgressBar("正在拷贝资源", "请耐心等待", 0.9f);
            CopyDirectory(assetsRootPath + systemFloder, hotfoxPath,true, compress);
            EditorUtility.ClearProgressBar();
        }

        if (string.IsNullOrEmpty(parkPath) || !File.Exists(parkPath))
        {
            Debug.LogError("二进制 不存在 " + parkPath);
            string apkFile = parkPath;

            apkFile = apkFile.Replace('\\', '/');

            string apkMd5 = "null";

            apkFile = apkFile.Replace(exportPath.Replace('\\', '/') + "/", "");

            clientInfor = "AssetsName," + versionStr + ",MD5MD5,ByteCount" + "\n" + apkFile + "," + 0 + "," + apkMd5 + "," + 0 + "";
        }
        else
        {
            string apkFile = parkPath;

            apkFile = apkFile.Replace('\\', '/');

            Stream apkFileString = GetFileStream(apkFile);

            string apkMd5 = GetFileMD5(apkFileString);

            long byteCount = apkFileString.Length;

            apkFileString.Close();

            apkFile = apkFile.Replace(exportPath.Replace('\\', '/') + "/", "");

            clientInfor = "AssetsName," + versionStr + ",MD5MD5,ByteCount" + "\n" + apkFile + "," + 0 + "," + apkMd5 + "," + byteCount + "";
        }

        List<string> fileListName = null;
        // 首先查找最后版本中的文件 、、
        fileListName = GetFileListPath(hotfoxPath, "*.*");
        fileListName.Insert(0, outdllPath);

        for (int loop = 0; loop < fileListName.Count; loop++)
        {            
            string filePath = fileListName[loop].Replace('\\', '/');

            if (Path.GetExtension(filePath) == ".meta")
                continue;

            EditorUtility.DisplayProgressBar("生成资源版本信息", filePath, loop / (float)fileListName.Count);

            Stream streamFile = GetFileStream(filePath);

            string md5 = GetFileMD5(streamFile);

            long byteCount = streamFile.Length;

            streamFile.Close();

            if (loop == 0)
            {
                filePath = filePath.Replace(hotfoxPath + "/", "");
            }
            else
            {
                Debug.Log($"filePath path:{filePath} assetsRootPath:{hotfoxPath}/");
                filePath = filePath.Replace(hotfoxPath + "/", "");
            }

            string changeLine = "\n";

            if (loop == 0)
            {
                changeLine = "\n";
            }

            clientInfor += changeLine + filePath + "," + "0" + "," + md5 + "," + byteCount;
        }
        EditorUtility.ClearProgressBar();

        if (string.IsNullOrEmpty(resourceVersionPath))
        {
            saveVersionInforPath = EditorUtility.SaveFilePanel("保存生成的MD5数据", path, "", "txt");
        }
        else
        {
            saveVersionInforPath = resourceVersionPath;
        }

        byte[] md5IfoBuf = Encoding.Default.GetBytes(clientInfor);
        if (compress)
        {
            md5IfoBuf = CompressEncryption.Encryption(md5IfoBuf);
        }

        File.WriteAllBytes(hotfoxPath + "/ResourceVersion.txt", md5IfoBuf);
        fileListName.Clear();

        return versionStr;
    }



    /// <summary>
    /// 得到 资源 MD5
    /// </summary>
    /// <param name="md5Text"></param>
    public static Dictionary<string, ResourceTemplate> GetResourceMd5(string md5Text)
    {
        if (string.IsNullOrEmpty(md5Text)) return null;

        List<List<string>> TableString = CsvAnalysic.SplitCSV(md5Text);

        Dictionary<string, ResourceTemplate> results = new Dictionary<string, ResourceTemplate>();

        for (int loop = 1; loop < TableString.Count; loop++)
        {
            int fieldIndex = 0;

            ResourceTemplate temp = new ResourceTemplate();

            temp.ResourceName = TableString[loop][fieldIndex++];

            temp.ClientVersion = TableString[loop][fieldIndex++];

            temp.Md5 = TableString[loop][fieldIndex++];

            temp.ByteCount = int.Parse(TableString[loop][fieldIndex++]);

            results.Add(temp.ResourceName, temp);
        }

        return results;
    }

    /// <summary>
    /// 获取MD5效验码
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string GetFileMD5(Stream get_file)
    {
        try
        {
            System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash_byte = get_md5.ComputeHash(get_file);
            string resule = System.BitConverter.ToString(hash_byte);
            resule = resule.Replace("-", "").ToUpper();
            return resule;
        }
        catch
        {
            return "";
        }
    }

    private static Stream GetFileStream(string path)
    {
        System.IO.FileStream get_file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        return get_file;
    }

    private static List<string> GetFileListPath(string path, string exe)
    {
        List<string> fileNameList = new List<string>();
        if (!IsCanWrtile(path))
        {
            return fileNameList;
        }

        ///获取当前目录文件
        string[] fileNameArray = Directory.GetFiles(path, exe);

        if (fileNameArray != null)
        {
            for (int fileIndex = 0; fileIndex < fileNameArray.Length; fileIndex++)
            {
                if (IsCanWrtile(fileNameArray[fileIndex]))
                {
                    fileNameList.Add(fileNameArray[fileIndex]);
                }
            }
        }

        ///获取局部路径中的文件
        string[] directionPaths = Directory.GetDirectories(path);

        if (directionPaths != null)
        {
            for (int loop = 0; loop < directionPaths.Length; loop++)
            {
                string localPath = directionPaths[loop];

                List<string> pathFile = GetFileListPath(localPath, exe);

                for (int loopIndex = 0; loopIndex < pathFile.Count; loopIndex++)
                {
                    if (IsCanWrtile(pathFile[loopIndex]))
                    {
                        fileNameList.Add(pathFile[loopIndex]);
                    }
                }
            }
        }

        return fileNameList;
    }

    public static bool IsCanWrtile(string strPath)
    {
        FileInfo fi = new FileInfo(strPath);
        if (IsCanWrtileFile(fi))
        {
            return true;
        }

        DirectoryInfo di = new DirectoryInfo(strPath);
        if (IsCanWrtileDirectory(di))
        {
            return true;
        }

        Debug.Log("过滤文件 " + strPath);

        return false;
    }

    public static bool IsCanWrtileFile(FileInfo fi)
    {
        if (fi.Exists)
        {
            if (fi.Extension.ToLower() == "meta" || fi.Name.Contains(".DS_Store") || fi.Name.Contains(".manifest"))
            {
                return false;
            }


            if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden
            || fi.IsReadOnly)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public static bool IsCanWrtileDirectory(DirectoryInfo di)
    {
        if (di.Exists)
        {
            if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden
                || (di.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public static int GetCopyFileCount(string srcdir, string desdir,int hasCount)
    {
        int curCount = 0;

        string folderName = srcdir.Substring(srcdir.LastIndexOf("/") + 1);

        string desfolderdir = desdir + "/" + folderName;

        if (desdir.LastIndexOf("/") == (desdir.Length - 1))
        {
            desfolderdir = desdir + folderName;
        }
        string[] filenames = Directory.GetFileSystemEntries(srcdir);

        foreach (string file in filenames)// 遍历所有的文件和目录
        {
            string filePath = file.Replace("\\", "/");

            EditorUtility.DisplayProgressBar("检测文件数量中", "检测到" + curCount + hasCount + "个文件", 1.0f);

            if (Directory.Exists(filePath))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
            {
                curCount += GetCopyFileCount(filePath, desfolderdir,curCount + hasCount);
            }
            else if(Path.GetExtension(filePath) != ".meta" && Path.GetExtension(filePath) != ".manifest")
            {
                curCount++;
            }
        }

        EditorUtility.ClearProgressBar();
        return curCount;
    }


    static int toldCount = 0;
    static int curCount = 0;
    /// <summary>
    /// 拷贝文件夹
    /// </summary>
    /// <param name="srcdir"></param>
    /// <param name="desdir"></param>
    public static void CopyDirectory(string srcdir, string desdir,bool getCount = true,bool compress = false)
    {
        if(getCount)
        {
            toldCount = GetCopyFileCount(srcdir, desdir,0);
            curCount = 0;
        }     

        string folderName = srcdir.Substring(srcdir.LastIndexOf("/") + 1);

        string desfolderdir = desdir + "/" + folderName;

        if (desdir.LastIndexOf("/") == (desdir.Length - 1))
        {
            desfolderdir = desdir + folderName;
        }
        string[] filenames = Directory.GetFileSystemEntries(srcdir);

        foreach (string file in filenames)// 遍历所有的文件和目录
        {
            string filePath = file.Replace("\\", "/");
            EditorUtility.DisplayProgressBar("拷贝文件", filePath + " 到 " + desfolderdir, curCount / toldCount);

            if (Directory.Exists(filePath))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
            {
                string currentdir = desfolderdir + "/" + filePath.Substring(filePath.LastIndexOf("/") + 1);
                if (!Directory.Exists(currentdir))
                {
                    Directory.CreateDirectory(currentdir);
                }

                CopyDirectory(filePath, desfolderdir,false, compress);
            }
            else if(Path.GetExtension(filePath) != ".meta" && Path.GetExtension(filePath) != ".manifest")
            {
                curCount++;
                string srcfileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                srcfileName = desfolderdir + "/" + srcfileName;

                if (!Directory.Exists(desfolderdir))
                {
                    Directory.CreateDirectory(desfolderdir);
                }

                if (compress)
                {
                    if (File.Exists(srcfileName))
                    {
                        File.Delete(srcfileName);
                    }

                    var fileDatas = File.ReadAllBytes(filePath);
                    var startCount = fileDatas.Length;
                    fileDatas = CompressEncryption.Encryption(fileDatas);
                    File.WriteAllBytes(srcfileName, fileDatas);
                    Debug.Log($"加密资源保存到:{srcfileName} 加密前:{startCount} 加密后：{fileDatas.Length}");
                }
                else
                {
                    File.Copy(filePath, srcfileName, true);
                }
            }
        }

        EditorUtility.ClearProgressBar();
    }
}


/// <summary>
/// MD5文件模板
/// </summary>
public class ResourceTemplate
{
    public string ResourceName = string.Empty;

    public string ClientVersion = string.Empty;

    public string Md5 = string.Empty;

    public int ByteCount = 0;
}
