using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using CherishWebGLSupport;

public class UpResourceManager
{
	private static UpResourceManager _instance;
	
	public static UpResourceManager Instance { get { if (_instance == null) { _instance = new UpResourceManager(); } return _instance; } }
	/// <summary>
	/// 資源更新的完成回調
	/// </summary>
	/// <param name="wwwData"></param>
	/// <returns></returns>
	private delegate IEnumerator DownLoadCompleted(byte[] wwwData);
	public delegate void NotParrmaresCallFun();
	public delegate void UpClientParrmaresCallFun(string str,int intValue);
	public delegate void UpProgressCallFun(int intSize, int toldSize);
	public delegate void UpTimesInforCallFun(int currentTimes, int toldTimes,string version,string resourceName,int byteCount);
	
	enum LoadResourceType { LoadResourceVersion, LoadResourceFile }
	
	private LoadResourceType currentLoadType;

	public string logServerAddress = "";

    /// <summary>
    /// 登录服务器地址
    /// </summary>
    public string loginAddress = "";
    /// <summary>
    /// 资源服务器地址
    /// </summary>
    public string[] assetsAddressData;

    /// <summary>
    /// 使用外部资源
    /// </summary>
    public static bool useExternAssets = true;
	
	/// <summary>
	/// 打包资源的路径
	/// </summary>
    private static string projectParkPath = FrameWorkDrvice.AssetsPathManagerInstance.GetProjectPathNode();
	/// <summary>
	/// 外部资源的路径
	/// </summary>
    private static string externAssetsPath = FrameWorkDrvice.AssetsPathManagerInstance.GetExternPathNode();
	
	/// <summary>
	/// 文件系统前缀
	/// </summary>
	private string mFileSystem = "";

    private string mAddressFile = "ServerAddress.txt";
	
	private string mResourceVersion = "ResourceVersion.txt";
	
	private string mhttpShortVersion = "Version.txt";
	
	private string mHttpVersionStr;
	
	private string mHttpVersionToldStr;
	
	private string mLocalVersionStr="";

    private string mLocalApkVersionStr = "";
    /// <summary>
    /// 网络获取版本数据
    /// </summary>
    private VersionData mHttpVersionData;	
	/// <summary>
	/// 网络获取的完整更新版本数据
	/// </summary>
	private VersionData mHttpVersionToldData;
	/// <summary>
	/// 本地获取的版本数据
	/// </summary>
	private VersionData mLocalVersionData;
    /// <summary>
    /// Apk内部版本号
    /// </summary>
    private VersionData mLocalApkVersionData;
	/// <summary>
	/// 当前加载的数据
	/// </summary>
	private ResourceVersionItem mCurrentVersionDate;
	/// <summary>
	/// 需要更新的资源表
	/// </summary>
	private List<ResourceVersionItem> mResourceVersionList;

    /// <summary>
    /// 更新完成的资源列表
    /// </summary>
    private List<ResourceVersionItem> mUpFinishVersionList;
	
	/// <summary>
	/// 服务器下载信息配置数据
	/// </summary>
	//private DownLoadServerDataItem mVersionServerConfig;

    private string vVersionServerAddress;
	
	/// <summary>
	/// 文件缓存文件夹
	/// </summary>
	private string mSaveFilePath;
	
	/// <summary>
	/// 当前下载文件名字
	/// </summary>
	private string mDownLoadUrl;
	
	/// <summary>
	/// 当前更新完毕的资源数据
	/// </summary>
	private byte[] wwwBytes;
	
	/// <summary>
	/// 当前加载的资源加载进度
	/// </summary>
	private float mProgress;
	
	/// <summary>
	/// 已经更新完毕的文件的总大小
	/// </summary>
	private int mToldFileLoadSize;
	
	/// <summary>
	/// 当前正在更新的文件的大小
	/// </summary>
	private int mCurrentFileLoadSize;

    /// <summary>
    /// 加载成功
    /// </summary>
    private bool mLoadSucceed;
	
	/// <summary>
	/// 更新完资源后的回调
	/// </summary>
	private DownLoadCompleted downLoadCompletedCall;


    #region 更新界面需要注册的接口
    /// <summary>
	/// 通知客户端需要更新方法
	/// </summary>
	public UpClientParrmaresCallFun mClientUpdateC;	
	/// <summary>
	/// 通知资源需要更新方法
	/// </summary>
	public UpClientParrmaresCallFun mResourceNeedUpdateC;	
	/// <summary>
	/// 通知更新进度方法
	/// </summary>
	public UpProgressCallFun mUpProgressC;	
	/// <summary>
	/// 通知当前更新到的资源信息
	/// </summary>
	public UpTimesInforCallFun mUpTimesInforC;	
	/// <summary>
	/// 通知进入游戏方法
	/// </summary>
	public NotParrmaresCallFun mReadyEntryGameFunctionC;	
	/// <summary>
	/// 通知更新完毕
	/// </summary>
	public NotParrmaresCallFun mUpVersionCompletedC;	
	/// <summary>
	/// 通知客户端更新完毕
	/// </summary>
	public NotParrmaresCallFun mUpVersionClientCompletedC;
    /// <summary>
    /// 显示检查网络
    /// </summary>
    public NotParrmaresCallFun mShowCheckNetC;
    /// <summary>
    /// 检查更新
    /// </summary>
    public NotParrmaresCallFun mCheckUpdateC;
    /// <summary>
    /// 更新对比接口
    /// </summary>
    public NotParrmaresCallFun mCheckUpdateCompareC;
    #endregion 更新界面需要注册的接口

    /// <summary>
    /// 记录当前应该要更新资源的索引
    /// </summary>
    public int curUpdateIndex = 0;
    /// <summary>
    /// 记录当前已读取的字节数量  如果开始续传就从当前的Index开始续传
    /// </summary>
    public int curAppBufIndex = 0;
    public UpResourceNetStep upResourceNetStep = UpResourceNetStep.GetServerAddrFromHttp;


	/// <summary>
	/// 检测是否是新的APP
	/// </summary>
	public void CheckNewApp()
	{
		if (PlayerPrefs.HasKey(GameMain.Instance.recordKey))
		{
			if (PlayerPrefs.GetString(GameMain.Instance.recordKey) == GameMain.Instance.recordValue)
			{
				return;
			}
		}
		DebugLoger.Log("CheckNewApp - ClearPath");
		//这里是旧的APP 需要删除资源路径文件夹
		ClearPath();
		DebugLoger.Log("SaveRecordApp");
		SaveRecordApp();
	}

	/// <summary>
	/// 记录App
	/// </summary>
	public void SaveRecordApp()
	{
		PlayerPrefs.SetString(GameMain.Instance.recordKey, GameMain.Instance.recordValue);
		DebugLoger.Log("SaveRecordApp End");
	}

    /// <summary>
    /// 更新资源网络阶段
    /// </summary>
    public enum UpResourceNetStep : int
    {
        GetServerAddrFromHttp = 0,
        GetShortVersionData = 1,
        GetServerResourceVersion = 2,
        UpdateApk = 3,
        UpdateResource = 4,
    }

    /// <summary>
    /// 记录步骤
    /// </summary>
    /// <param name="step"></param>
    public void SetUpNetStep(UpResourceNetStep step)
    {
        upResourceNetStep = step;
    }

    /// <summary>
    /// 重连
    /// </summary>
    public void ReConnectCall()
    {        
        if(upResourceNetStep == UpResourceNetStep.GetServerAddrFromHttp)
        {
            //获取服务器地址重连
            GetServerAddrFromHttp();
        }
        else if(upResourceNetStep == UpResourceNetStep.GetShortVersionData)
        {
            //获取短版本号
            GetShortVersionData();
        }
        else if (upResourceNetStep == UpResourceNetStep.GetServerResourceVersion)
        {
            //获取版本资源信息
            GetServerResourceVersion();
        }
        else if (upResourceNetStep == UpResourceNetStep.UpdateApk)
        {
            //更新APK
            StarUpClient();
        }
        else if (upResourceNetStep == UpResourceNetStep.UpdateResource)
        {
            //更新资源需要重读当前资源
            StarUpdate();
        }
    }

    /// <summary>
    /// 关闭程序
    /// </summary>
    public void CloseApplication()
    {
        if (upResourceNetStep == UpResourceNetStep.UpdateResource)
        {
            SaveUpdateFinshList(false);
        }

        Application.Quit();
    }
	

    /// <summary>
    /// 获取服务器最新客户端版本号
    /// </summary>
    /// <returns></returns>
    public string GetHttpClientVersion()
    {
        if (mHttpVersionData == null)
        {
            return "not open update game of the http version is 0.0";
        }
        else
        {
            return mHttpVersionData.Version.ToString();
        }
    }

    /// <summary>
    /// 获取本地客户端版本号
    /// </summary>
    /// <returns></returns>
    public string GetLocalClientVersion()
    {
        if (mLocalVersionData == null)
        {
            return "0.0.0";
        }
        else
        {
            return mLocalVersionData.Version.ToString();
        }
    }

    /// <summary>
    /// 获取服务器版本值
    /// </summary>
    /// <returns></returns>
    public int GetLocalVersionValue()
    {
        return mLocalVersionData.toldVersionValue;
    }




    /// <summary>
    /// 获取本地服务器地址 1
    /// </summary>
    public void GetLocalAddress()
    {
		try
		{
			DebugLoger.Log("GetLocalAddress 1");
			CheckNewApp();
			CheckUseVersionInternal();

			curUpdateIndex = 0;
			curAppBufIndex = 0;
			//顺序不能乱
			mCurrentVersionDate = null;
			string addressFileStrInfo = "";
			string addressFile = externAssetsPath + "/" + mAddressFile;

			if (!File.Exists(addressFile) || !useExternAssets)
			{
				DebugLoger.Log("not file path  externAssetsPath " + externAssetsPath);
				DebugLoger.Log("not file path  projectParkPath " + projectParkPath);

				///找不到外部路径 或者 不使用外部路径  从这里加载内部资源

				///文件打包路径下查找 不使用外部路径
				downLoadCompletedCall = null;
				byte[] byteBuf = GetFileData(mAddressFile);

                //yield return CYApp.Instance.StartCoroutine(DownLoadData(path + "/" + mResourceVersion));

                if (byteBuf != null)
                {
                    MemoryStream ms = new MemoryStream(byteBuf);

                    byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

                    addressFileStrInfo = Encoding.Default.GetString(unEncryption);

                    ms.Dispose();

                    ms.Close();

                    byteBuf = null;
                }
                else
                {
                    DebugLoger.LogError("");
                }

			}
			else
			{
				DebugLoger.Log("file path " + addressFile);

				///外部路径下查找
				downLoadCompletedCall = null;

				byte[] fileBytes = File.ReadAllBytes(addressFile);


				if (fileBytes != null)
				{
					MemoryStream ms = new MemoryStream(fileBytes);

					byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

					addressFileStrInfo = Encoding.Default.GetString(unEncryption);

					ms.Dispose();

					ms.Close();
				}
			}

			DebugLoger.Log("addressFileStrInfo " + addressFileStrInfo);

			if (!string.IsNullOrEmpty(addressFileStrInfo))
			{
				///实例化地址
				List<List<string>> addressArray = CsvAnalysic.SplitCSV(addressFileStrInfo);

				loginAddress = addressArray[0][0];
				assetsAddressData = new string[3];

				assetsAddressData[0] = addressArray[1][0];
				assetsAddressData[1] = addressArray[2][0];
				assetsAddressData[2] = addressArray[3][0];

				DebugLoger.Log("assetsAddressData[0] " + assetsAddressData[0]);
				DebugLoger.Log("assetsAddressData[1] " + assetsAddressData[1]);
				DebugLoger.Log("assetsAddressData[2] " + assetsAddressData[2]);
			}
			else
			{
				DebugLoger.LogError("本地地址文件为空");
			}
		}
		catch
		{
			ClearPath();
			CherishUtility.DoReStar(10);
		}

        GetServerAddrFromHttp();
    }

    /// <summary>
    /// 获取服务器资源地址 2
    /// </summary>
    public void GetServerAddrFromHttp()
    {
        SetUpNetStep(UpResourceNetStep.GetServerAddrFromHttp);
        string serverAddr = GetArrayAddresData(assetsAddressData);
        downLoadCompletedCall = AddressDataCallBack;
        IEnumeratorManager.Instance.StartCoroutine(DownLoadData(serverAddr + "/" + mAddressFile));
    }

    /// <summary>
    /// 获取服务器地址返回 3 确保地址正确
    /// </summary>
    private IEnumerator AddressDataCallBack(byte[] wwwData)
    {
        ///这里是http上面的版本信息加载完毕
		downLoadCompletedCall = null;
        if (wwwData == null)
        {
            yield break;
        }

        MemoryStream ms = new MemoryStream(wwwData);

        byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

        string addressStrInfo = Encoding.Default.GetString(unEncryption);

        ms.Dispose();

        ms.Close();
		DebugLoger.Log(addressStrInfo);
		List<List<string>> serverAddresArray = CsvAnalysic.SplitCSV(addressStrInfo);
		DebugLoger.Log("serverAddresArray count " + serverAddresArray.Count);

		string[] serverAddresData = new string[4];
        serverAddresData[0] = serverAddresArray[1][0];
        serverAddresData[1] = serverAddresArray[2][0];
        serverAddresData[2] = serverAddresArray[3][0];
		serverAddresData[3] = serverAddresArray[4][0];

        string serverAssetsAddr = GetArrayAddresData(serverAddresData);
        string localAssetsAddr = GetArrayAddresData(assetsAddressData);

        ///服务器获取的新的资源地址替换旧的资源地址
        assetsAddressData = serverAddresData;
        ///登录地址替换
        loginAddress = serverAddresArray[0][0];
		string logServer = serverAddresData[3];
		string[] logServerSwitch = logServer.Split(':');
		if (logServerSwitch[0] == "logserver")
		{
			CherishSocket.AddressFamily useFamily = CherishSocket.AddressFamily.InterNetwork;
			logServerAddress = NetDataManager.DomainIp(logServerSwitch[1], NetDataManager.DomainExctption, ref useFamily);
		}
		else
		{
			logServerAddress = "";
		}

		serverAddresArray = null;
        serverAddresData = null;

        if (serverAssetsAddr != localAssetsAddr)
        {
            ///本地和服务器地址不同
            GetServerAddrFromHttp();
        }
        else
        {
			///确定地址
			///保存地址
			StringConfigClass.domAddr = loginAddress;
            StringConfigClass.loginIp = loginAddress;
            string addressFile = externAssetsPath + "/" + mAddressFile;
            unEncryption = CompressEncryption.Encryption(unEncryption);
            File.WriteAllBytes(addressFile, unEncryption);
            GetShortVersionData();
        }
    }

    
    /// <summary>
    /// 获取短版本数据  4
    /// </summary>
    public void GetShortVersionData()
	{
        mFileSystem = CherishUtility.GetFileProtocol();
        if (CherishUtility.GetPlatform() == RuntimePlatform.Android)
        {
            vVersionServerAddress = assetsAddressData[0];
        }
        else if (CherishUtility.GetPlatform() == RuntimePlatform.IPhonePlayer)
        {
            vVersionServerAddress = assetsAddressData[1];
        }
        else
        {
            vVersionServerAddress = assetsAddressData[2];
        }

        ///获取版本数据
        GetHttpShortVersion();
    }

    /// <summary>
    /// 获取服务器短版本号 5
    /// </summary>
    public void GetHttpShortVersion()
    {
        SetUpNetStep(UpResourceNetStep.GetShortVersionData);
        //完整信息数据加载完成回调方法
        downLoadCompletedCall = shortVersionCallBack;
        IEnumeratorManager.Instance.StartCoroutine(DownLoadData(vVersionServerAddress + "/" + mhttpShortVersion));
    }

    /// <summary>
    /// 短版本 获取完成和本地版本对比 6
    /// </summary>
    /// <param name="wwwData"></param>
    /// <returns></returns>
    private IEnumerator shortVersionCallBack(byte[] wwwData)
    {
        downLoadCompletedCall = null;

        if (wwwData == null)
        {
            yield break;
        }

        MemoryStream ms = new MemoryStream(wwwData);

        string versionStr = Encoding.Default.GetString(ms.ToArray());

        ms.Dispose();
        ms.Close();
        
        VersionData vd = new VersionData();
        vd.Version = versionStr;
        vd.InitVersion();
        mHttpVersionData = vd;

        ///对比APK内部资源版本和服务器版本区别
        if (mLocalApkVersionData.toldVersionValue >= mHttpVersionData.toldVersionValue)
        {
            ///内部版本和服务器版本一直
            //直接到下一步
            //更新对比中
            if (mCheckUpdateC != null)
            {
                mCheckUpdateC();
            }

            OnReadyEntryGame();
            yield break;
        }
        else
        {
            if(mCheckUpdateCompareC != null)
            {
                mCheckUpdateCompareC();
            }

            GetServerResourceVersion();
        }
    }


    //需要更新 获取服务器版本 7
    public void GetServerResourceVersion()
    {
        SetUpNetStep(UpResourceNetStep.GetServerResourceVersion);
        //需要更新 获取服务器版本
        currentLoadType = LoadResourceType.LoadResourceVersion;
        //完整信息数据加载完成回调方法
        downLoadCompletedCall = VersionDataCallBack;
        IEnumeratorManager.Instance.StartCoroutine(DownLoadData(vVersionServerAddress + "/" + mResourceVersion));
    }

    /// <summary>
    /// 获取Http版本数据返回 对比本地版本数据 确定更新呼出更新面板 8
    /// </summary>
    private IEnumerator VersionDataCallBack(byte[] wwwData)
    {
        ///这里是http上面的版本信息加载完毕
		downLoadCompletedCall = null;
        if (wwwData == null)
        {
            yield break;
        }
        MemoryStream ms = new MemoryStream(wwwData);

        byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

        mHttpVersionStr = Encoding.Default.GetString(unEncryption);

        ms.Dispose();

        ms.Close();

        mHttpVersionData = StringToVersionData(mHttpVersionStr);

        ///对比APK内部资源版本和服务器版本区别
        if (mLocalApkVersionData.toldVersionValue >= mHttpVersionData.toldVersionValue)
        {
            ///内部版本和服务器版本一直
            //直接到下一步
            OnReadyEntryGame();
            yield break;
        }
        else
        {
            if (useExternAssets)
            {
                if (mLocalApkVersionData.toldVersionValue >= mLocalVersionData.toldVersionValue)
                {
                    ///如果内部版本高于外部版本就使用内部版本
                    mLocalVersionData = mLocalApkVersionData;
                }
            }
            else
            {
                ///使用内部更新资源
                mLocalVersionData = mLocalApkVersionData;
            }

            //服务器和客户端数据已经加载出来了
            if (mHttpVersionData.apkVersionValue > mLocalVersionData.apkVersionValue)
            {
                mResourceVersionList = new List<ResourceVersionItem>();
                mResourceVersionList.Add(mHttpVersionData.ClientVersionInfor);
                OnClientUpdate();
                yield break;
            }

            mResourceVersionList = CompareResourceVersionData();

            if (mResourceVersionList != null && mResourceVersionList.Count > 0 && mLocalVersionData.toldVersionValue < mHttpVersionData.toldVersionValue)
            {
                //有新的更新
                OnResourceNeedUpdate();
            }
            else
            {
                //直接到下一步
                OnReadyEntryGame();
            }
        }
    }


    /// <summary>
    /// 获取资源服务器地址对应平台地址
    /// </summary>
    /// <param name="addressArray"></param>
    /// <returns></returns>
    public string GetArrayAddresData(string[] addressArray)
    {

        if (CherishUtility.GetPlatform() == RuntimePlatform.Android)
        {
            return addressArray[0];
        }
        else if (CherishUtility.GetPlatform() == RuntimePlatform.IPhonePlayer)
        {
            return addressArray[1];
        }
        else
        {
            return addressArray[2];
        }
    }

    /// <summary>
    /// 加载资源数据
    /// </summary>
    /// <param name="resourcePath"></param>
    /// <returns></returns>
    private IEnumerator DownLoadData(string resourcePath)
	{
		DebugLoger.Log("download url " + resourcePath);
        mLoadSucceed = false;
        int count = 0;
	ResetConnect:
		count++;
		wwwBytes = null;
		
		WWW www = new WWW(resourcePath + "?t= + new Date().getTime()");
        
		
		while (!www.isDone)
		{
			mProgress = www.progress;
			
			if(mCurrentVersionDate != null)
			{
                mCurrentFileLoadSize = (int)(mProgress * mCurrentVersionDate.mByteCount);
				
				OnUpProgress(mCurrentFileLoadSize);
			}
			
			yield return null;
		}

        if (!string.IsNullOrEmpty(www.error))
        {
				DebugLoger.Log("download error "+ www.error);
				//无法连接到服务器
				www.Dispose();

                yield return new IEnumeratorManager.WaitForSeconds(1.0f);

                if (count <= 10)
                {
                    goto ResetConnect;
                }
                else
                {
                    //通知掉线
                    count = 0;
                    if (mShowCheckNetC != null)
                    {
                        mShowCheckNetC();
                    }
                    yield break;
                }     
            www.Dispose();
        }
		else
		{			
            mLoadSucceed = true;
            count = 0;
            byte[] wwwData = www.bytes;
            wwwBytes = wwwData;            
            www.Dispose();   
            //资源更新完毕的回调
            if (downLoadCompletedCall != null)
			{
                yield return IEnumeratorManager.Instance.StartCoroutine(downLoadCompletedCall(wwwData));				
			}
		}
    }

    /// <summary>
    /// 获取本地数据
    /// </summary>
    public void LoadLocalClientVersionData()
    {
        string localResourceVersionFile = externAssetsPath + "/" + mResourceVersion;
        
        if (!File.Exists(localResourceVersionFile) || !useExternAssets)
        {
            ///找不到外部路径 或者 不使用外部路径  从这里加载内部资源

            ///文件打包路径下查找 不使用外部路径
            downLoadCompletedCall = null;

            byte[] byteBuf = GetFileData(mResourceVersion);

            if (byteBuf != null)
            {
                MemoryStream ms = new MemoryStream(byteBuf);

                byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

                mLocalVersionStr = Encoding.Default.GetString(unEncryption);

                ms.Dispose();

                ms.Close();

                byteBuf = null;
            }
        }
        else
        {
			DebugLoger.Log("localResourceVersionFile 外部路径 " + localResourceVersionFile);
            ///外部路径下查找
            downLoadCompletedCall = null;

            byte[] fileBytes = File.ReadAllBytes(localResourceVersionFile);

            //yield return 0;

            if (fileBytes != null)
            {
                MemoryStream ms = new MemoryStream(fileBytes);

                byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

                mLocalVersionStr = Encoding.Default.GetString(unEncryption);

                ms.Dispose();

                ms.Close();
            }
        }
    }


	/// <summary>
	/// 检测使用内部资源
	/// </summary>
	public void CheckUseVersionInternal()
	{
		DebugLoger.Log("CheckUseVersionInternal");
		//开始加载APK内部Version
		LoadApkClientExistVersion();
		mLocalApkVersionData = StringToVersionData(mLocalApkVersionStr);

		//载入本地的版本文件 外部优先
		LoadLocalClientVersionData();
		mLocalVersionData = StringToVersionData(mLocalVersionStr);

		if (mLocalApkVersionData.ClientVersionInfor.mResourceName == mLocalVersionData.ClientVersionInfor.mResourceName)
		{
			DebugLoger.Log("删除资源包中的Apk 以安装的Apk为主名!");
			DeleteApp(externAssetsPath.Replace("\\", "/") + "/" + mLocalApkVersionData.ClientVersionInfor.mResourceName);
		}
		else
		{
			DebugLoger.Log("删除资源包中的Apk 以老版本名和当前安装版本名为主!");
			DeleteApp(externAssetsPath.Replace("\\", "/") + "/" + mLocalApkVersionData.ClientVersionInfor.mResourceName);
			DeleteApp(externAssetsPath.Replace("\\", "/") + "/" + mLocalVersionData.ClientVersionInfor.mResourceName);
		}

		if (mLocalApkVersionData.toldVersionValue > mLocalVersionData.toldVersionValue)
		{
			///资源版本号 大于等于 残留资源版本号
			///不使用外部优先
			useExternAssets = false;
			DebugLoger.LogError("是否使用外部资源文件进行读取 ---- 这里不使用外部文件!");
		}
		else
		{
			///资源版本号 小于 残留版本号
			if (mLocalApkVersionData.apkVersionValue > mLocalVersionData.apkVersionValue)
			{
				///当前Apk版本 大于 残留Apk资源版本 不使用外部优先
				useExternAssets = false;
				DebugLoger.LogError("是否使用外部资源文件进行读取 ---- 当前Apk版本 大于 残留Apk资源版本 不使用外部优先!");
			}
			else
			if (mLocalApkVersionData.apkVersionValue == mLocalVersionData.apkVersionValue)
			{
				///当前Apk版本 等于 残留Apk资源版本 使用外部
				useExternAssets = true;
				DebugLoger.LogError("是否使用外部资源文件进行读取 ---- 当前Apk版本 等于 残留Apk资源版本 使用外部!");
			}
			else
			{
				///当前Apk版本 低于 残留Apk资源版本  不使用外部优先
				useExternAssets = false;
				DebugLoger.LogError("当前Apk版本 低于 残留Apk资源版本  不使用外部优先!");
			}
		}

		if (!useExternAssets)
		{
			ClearPath();
		}
	}

	public void ClearPath()
	{
		try
		{
			string assetsPath = externAssetsPath;// + "/" + AssetsPathManager.GetPlatform();
			DebugLoger.Log("外部资源判定 外部路径!Path:" + assetsPath);
			if (Directory.Exists(assetsPath))
			{
				DebugLoger.Log("外部资源被判定无效 删除 外部路径!Path:" + assetsPath);
				Directory.Delete(assetsPath, true);
			}
		}
		catch (System.Exception e)
		{
			DebugLoger.LogError(e.ToString());
		}
	}


	/// <summary>
	/// 获取APK中版本数据
	/// </summary>
	public void LoadApkClientExistVersion()
    {
        byte[] byteBuf = CherishUtility.GetFileDataInternal(projectParkPath,mResourceVersion);

        if (byteBuf != null)
        {
            MemoryStream ms = new MemoryStream(byteBuf);

            byte[] unEncryption = CompressEncryption.UnEncryption(ms.ToArray());

            mLocalApkVersionStr = Encoding.Default.GetString(unEncryption);

            ms.Dispose();

            ms.Close();

            byteBuf = null;
        }
    }

    

    /// <summary>
    /// 对比资源数据  并且返回需要更新的资源列
    /// </summary>
    /// <returns></returns>
    private List<ResourceVersionItem> CompareResourceVersionData()
	{
		List<ResourceVersionItem> UpResourceVersionList = new List<ResourceVersionItem>();
		
		for (int loop = 0; loop < mHttpVersionData.ResourceVersionList.Count; loop++)
		{
			ResourceVersionItem httpResourceVersion = mHttpVersionData.ResourceVersionList[loop];
			
			ResourceVersionItem localResourceVersion = mLocalVersionData.ResourceVersionList.Find(item => item.mResourceName == httpResourceVersion.mResourceName);
			
			if (localResourceVersion == null)
			{
				//新增资源
				UpResourceVersionList.Add(httpResourceVersion);
				continue;
			}
			
			//检查外部路径
			string resourcePath = externAssetsPath.Replace("\\","/") +"/" + localResourceVersion.mResourceName;
			
			if(!File.Exists(resourcePath))
			{
				//设置为内部路径
                resourcePath = projectParkPath.Replace("\\", "/") + "/" + localResourceVersion.mResourceName;
			}


            string fileMd5 = localResourceVersion.mMd5;// GetFileMD5(localResourceVersion.mResourceName);
			
			
			//检查内部路径
			if (string.IsNullOrEmpty(fileMd5))
			{
				//文件不存在 需要更新
				UpResourceVersionList.Add(httpResourceVersion);
				continue;
			}

			
			if (httpResourceVersion.mMd5 != fileMd5)
			{
				//MD5验证不通过 需要更新
				UpResourceVersionList.Add(httpResourceVersion);
				continue;
			}
		}
		
		return UpResourceVersionList;
	}
	
    /// <summary>
    /// 清理多余的更新文件数据
    /// </summary>
    public void ClearVersionData()
    {
        List<ResourceVersionItem> needClearList = new List<ResourceVersionItem>();
        for (int loopResource = 0; loopResource < mLocalVersionData.ResourceVersionList.Count; ++loopResource)
        {
            ResourceVersionItem checkResourceItem = mLocalVersionData.ResourceVersionList[loopResource];

            ResourceVersionItem sameResourceItem = mHttpVersionData.ResourceVersionList.Find(item => item.mResourceName == checkResourceItem.mResourceName);

            if(sameResourceItem == null)
            {
                needClearList.Add(sameResourceItem);
            }
        }

        ClearAssets(needClearList);

        for (int loopClear = 0;loopClear < needClearList.Count;++loopClear)
        {
            mLocalVersionData.ResourceVersionList.Remove(needClearList[loopClear]);
        }        

        needClearList.Clear();
    }

    /// <summary>
    /// 清理多余的不再有的资源
    /// </summary>
    /// <param name="needClearList"></param>
    private void ClearAssets(List<ResourceVersionItem> needClearList)
    {
        string longPath = externAssetsPath.Replace("\\", "/") + "/";
        for (int loop = 0;loop < needClearList.Count;++loop)
        {
            ResourceVersionItem rv = needClearList[loop];

            if (rv != null && !string.IsNullOrEmpty(rv.mResourceName))
            {
                string assetsPath = longPath + rv.mResourceName;

                if (File.Exists(assetsPath))
                {
                    File.Delete(assetsPath);
                }
            }
        }
    }

    /// <summary>
    /// 创建新的版本数据
    /// </summary>
    public void CreateNewVersionData(bool useHttpVersion)
    {
        ClearVersionData();

        if (useHttpVersion)
        {
            mLocalVersionData.Version = mHttpVersionData.Version;
        }

        if (mUpFinishVersionList.Count == 1 && mUpFinishVersionList[0].mResourceName == mHttpVersionData.ClientVersionInfor.mResourceName)
        {
            if (useHttpVersion)
            {
                //这里是更新的安装包信息
                mLocalVersionData.ClientVersionInfor = mHttpVersionData.ClientVersionInfor;
            }
        }
        else
        {
            //这里是更新的资源包信息
            for (int loopUpSucess = 0; loopUpSucess < mUpFinishVersionList.Count; ++loopUpSucess)
            {
                ResourceVersionItem resourceVersionItem = mUpFinishVersionList[loopUpSucess];

                ResourceVersionItem sameVersionItem = mLocalVersionData.ResourceVersionList.Find(item => item.mResourceName == resourceVersionItem.mResourceName);

                if (sameVersionItem != null)
                {
                    sameVersionItem.SetValue(mUpFinishVersionList[loopUpSucess]);
                }
                else
                {
                    mLocalVersionData.ResourceVersionList.Add(resourceVersionItem);
                }
            }
        }
    }

	/// <summary>
	/// 字符串转换到VersionData格式
	/// </summary>
	/// <param name="versionStr"></param>
	/// <returns></returns>
	private VersionData StringToVersionData(string versionStr)
	{
		VersionData vd = new VersionData();
		
		if (string.IsNullOrEmpty(versionStr))
		{
			return vd;
		}
		
		ResourceVersionItem resourceVersionItem = new ResourceVersionItem();

		List<List<string>> TableString = CsvAnalysic.SplitCSV(versionStr);
        //这里获取资源版本号
        vd.Version = TableString[0][1];
        vd.InitVersion();
        ///这里是安装包版本号
		resourceVersionItem.mResourceName = TableString[1][0];
		
		resourceVersionItem.mClientVersion = float.Parse(TableString[1][1]);
		
		resourceVersionItem.mMd5 = TableString[1][2];
		
		resourceVersionItem.mByteCount = int.Parse(TableString[1][3]);        
		
		vd.ClientVersionInfor = resourceVersionItem;
		
        ///这里是资源版本号
		for (int loop = 2; loop < TableString.Count; loop++)
		{
			resourceVersionItem = new ResourceVersionItem();
			
			int fieldIndex = 0;
			
			resourceVersionItem.mResourceName = TableString[loop][fieldIndex++];
			
			resourceVersionItem.mClientVersion = float.Parse(TableString[loop][fieldIndex++]);
			
			resourceVersionItem.mMd5 = TableString[loop][fieldIndex++];
			
			resourceVersionItem.mByteCount = int.Parse(TableString[loop][fieldIndex++]);
			
			vd.ResourceVersionList.Add(resourceVersionItem);
		}
		
		return vd;
	}

    /// <summary>
    /// VersionData格式转字符串
    /// </summary>
    /// <param name="versionData"></param>
    /// <returns></returns>
    private string ResourceVersionToString(VersionData versionData)
    {        
        string outStr = "AssetsName," + versionData.Version + ",MD5MD5,ByteCount\n" + versionData.ClientVersionInfor.mResourceName + "," + versionData.ClientVersionInfor.mClientVersion.ToString("0.0") + "," + versionData.ClientVersionInfor.mMd5 + "," + versionData.ClientVersionInfor.mByteCount + "";

        for(int loop=0;loop< versionData.ResourceVersionList.Count;++loop)
        {
            ResourceVersionItem resourceVerItem = versionData.ResourceVersionList[loop];

            outStr += "\n" + resourceVerItem.mResourceName + "," + resourceVerItem.mClientVersion.ToString("0.0") + "," + resourceVerItem.mMd5 + "," + resourceVerItem.mByteCount;
        }

        return outStr;
    }


	/// <summary>
	/// 获取需要更新的资源数量
	/// </summary>
	/// <returns></returns>
	private int GetNeedUpResourceCount()
	{
		if (mResourceVersionList == null || mResourceVersionList.Count <= 0)
		{
			return 0;
		}
		else
		{
			return mResourceVersionList.Count;
		}
	}
	
	
	/// <summary>
	/// 获取总共的大小
	/// </summary>
	/// <returns></returns>
	private int GetToladSize()
	{
		int size = 0;
		if (mResourceVersionList != null || mResourceVersionList.Count > 0)
		{
			for (int loop = 0; loop < mResourceVersionList.Count; loop++)
			{
				size += mResourceVersionList[loop].mByteCount;
			}
		}
        else
        {
            DebugLoger.LogError("GetToladSize mResourceVersionList null");
        }

		return size;
	}
	
	/// <summary>
	/// 获取MD5效验码
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	private static string GetFileMD5(string path)
	{
		try
		{
			byte[] byteBuf = GetFileData(path);
			
			if (byteBuf == null || byteBuf.Length <= 0)
			{
				return "";
			}
			
			System.IO.MemoryStream get_file = new MemoryStream(byteBuf);
			
			System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			
			byte[] hash_byte = get_md5.ComputeHash(get_file);
			
			string resule = System.BitConverter.ToString(hash_byte);
			
			resule = resule.Replace("-", "").ToUpper();
			
			get_file.Close();
			
			return resule;
		}
		catch
		{
			return "";
		}
	}


	public void StarUpClient()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			SetUpNetStep(UpResourceNetStep.UpdateApk);
			mCurrentVersionDate = mHttpVersionData.ClientVersionInfor;

			IEnumeratorManager.Instance.StartCoroutine(IEStartUpClient());
		}
		else
		{
			Application.OpenURL(StringConfigClass.GetDownloadUrl());
		}
	}
    

    private IEnumerator IEStartUpClient()
	{
		currentLoadType = LoadResourceType.LoadResourceFile;
		
		downLoadCompletedCall = null;
        string localResourceVersionFile = externAssetsPath.Replace("\\", "/") + "/" + mHttpVersionData.ClientVersionInfor.mResourceName;
        yield return IEnumeratorManager.Instance.StartCoroutine(DownLoadApp(vVersionServerAddress + "/" + mHttpVersionData.ClientVersionInfor.mResourceName, mHttpVersionData.ClientVersionInfor.mByteCount, localResourceVersionFile));
        OnUpVersionClientCompleted();
	}


    /// <summary>
    /// 下载安装包
    /// </summary>
    /// <param name="resourcePath"></param>
    /// <returns></returns>
    private IEnumerator DownLoadApp(string resourcePath, int bufLength,string filePath)
    {
        yield return null;

        int pos = filePath.LastIndexOf('/');

        string dirPath = filePath.Substring(0, pos);

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        FileStream fs = File.Open(filePath,FileMode.OpenOrCreate);  

        int everyLength = 4096;

        int recive = curAppBufIndex;

        bool hasError = false;


        Thread downLoadThread = new Thread(new ThreadStart(() =>
        {
            bool headSucess = false;
            HttpWebRequest httpWebRequest = null;
            try
            {
                httpWebRequest = HttpWebRequest.Create(resourcePath) as HttpWebRequest;
                httpWebRequest.AddRange(curAppBufIndex);
                httpWebRequest.Timeout = 2000;
                httpWebRequest.ReadWriteTimeout = 2000;
            }
            catch(System.Exception e)
            {
				DebugLoger.LogError("exception " + e);
                if (hasError == false)
                {
                    hasError = true;
                }
            }

            if (!hasError)
                try
                {
                    using (HttpWebResponse webResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                    {
                        headSucess = true;
                        if (webResponse.StatusCode == HttpStatusCode.OK
                           || webResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            DisConnectDownApp();
                        }
						DebugLoger.Log("webResponse.ContentLength " + webResponse.ContentLength);
                        bufLength = (int)webResponse.ContentLength;

                        ///侦听流数据
                        using (Stream stream = webResponse.GetResponseStream())
                        {
                            int count = 0;
                            byte[] readBuf = new byte[everyLength];
                            bool getLoop = true;
                            while (getLoop)
                            {
                                try
                                {
                                    count = stream.Read(readBuf, 0, everyLength);

                                    if (recive >= bufLength)
                                    {
                                        getLoop = false;
                                    }
                                    else
                                    {
										Monitor.Enter(fs);
                                        //{
                                            fs.Write(readBuf, 0, count);
										//}
										Monitor.Exit(fs);
										recive += count;
                                        curAppBufIndex = recive;
                                    }
                                }
                                catch (System.Exception e)
                                {
                                    getLoop = false;

									DebugLoger.LogError("exception " + e);


                                    if (hasError == false)
                                    {
                                        hasError = true;
                                    }
                                }

                            }
                        }
                    }
                    if (!headSucess)
                    {
                        hasError = true;
                    }
                }
                catch(System.Exception eout)
                {
					DebugLoger.LogError("exception " + eout);
                    hasError = true;
                }
        }));

        downLoadThread.Start();

        yield return null;

        while (!hasError && (downLoadThread.ThreadState & ThreadState.Running & ThreadState.WaitSleepJoin) == 0 && recive != bufLength)
        {
            OnUpProgress(recive);
            yield return null;
        }
        fs.Close();
        fs.Dispose();
        
        if (hasError)
        {
            DisConnectDownApp();
        }
        else
        {
            curAppBufIndex = 0;
        }
    }

    /// <summary>
    /// 删除安装文件
    /// </summary>
    /// <param name="filePath"></param>
    public void DeleteApp(string filePath)
    {
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }


    public void DisConnectDownApp()
    {
        if (mShowCheckNetC != null)
        {
            mShowCheckNetC();
        }
    }
	
	/// <summary>
	/// 外部调用 开始更新资源
	/// </summary>
	public  void StarUpdate()
	{
        SetUpNetStep(UpResourceNetStep.UpdateResource);
        IEnumeratorManager.Instance.StartCoroutine(IEStarUpdate());		
	}
	
	/// <summary>
	/// 在协同里面处理资源更新信息
	/// </summary>
	/// <returns></returns>
	private IEnumerator IEStarUpdate()
	{
		downLoadCompletedCall = ResourceLoadCompleted;
		
		currentLoadType = LoadResourceType.LoadResourceFile;

        if (curUpdateIndex == 0)
        {
            if (mUpFinishVersionList == null)
            {
                mUpFinishVersionList = new List<ResourceVersionItem>();
            }
            else
            {
                mUpFinishVersionList.Clear();
            }
        }

        bool isFinish = true;
        for (int loop = curUpdateIndex; loop < mResourceVersionList.Count; loop++)
		{
			ResourceVersionItem resourceVersionItem = mResourceVersionList[loop];
			
			mCurrentVersionDate = resourceVersionItem;
			
			//通知更新的当前资源的信息
			OnUpTimesInfor(loop+1,mResourceVersionList.Count, resourceVersionItem);
            DebugLoger.Log("resourceVersionItem.mResourceName:" + resourceVersionItem.mResourceName);
            yield return IEnumeratorManager.Instance.StartCoroutine(DownLoadData(vVersionServerAddress + "/" + resourceVersionItem.mResourceName));

            if (mLoadSucceed)
            {
                curUpdateIndex++;
                ///添加更新完成的资源信息
                mUpFinishVersionList.Add(resourceVersionItem);
                mToldFileLoadSize += mCurrentFileLoadSize;
            }
            else
            {
                isFinish = false;
                yield break;
            }     
		}
        if (isFinish)
        {
            curUpdateIndex = 0;
            mCurrentVersionDate = null;
            OnUpVersionCompleted();
        }     
	}
	
	/// <summary>
	/// 资源加载完成需要保存
	/// </summary>
	/// <param name="wwwData"></param>
	/// <returns></returns>
	private IEnumerator ResourceLoadCompleted(byte[] wwwData)
	{
		if (mCurrentVersionDate != null)
		{
			string savePath = externAssetsPath + "/" + mCurrentVersionDate.mResourceName;
			
			int pos = savePath.LastIndexOf('/');
			
			string dirPath = savePath.Substring(0, pos);
			
			DebugLoger.Log("保存资源路径 " + dirPath);
			
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}
			
			//保存资源
			File.WriteAllBytes(savePath, wwwData);
		}
		yield return 0;
	}
	
	
	/// <summary>
	/// 通知更新客户端信息
	/// </summary>
	private void OnClientUpdate()
	{
		if (mClientUpdateC != null)
		{
			mClientUpdateC(mHttpVersionData.Version, mHttpVersionData.ClientVersionInfor.mByteCount);
		}
	}
	
	
	/// <summary>
	/// 通知更新资源
	/// </summary>
	private void OnResourceNeedUpdate()
	{		
		if (mResourceNeedUpdateC != null)
		{
			mResourceNeedUpdateC(mHttpVersionData.Version, GetToladSize());
		}
	}
	
	/// <summary>
	/// 通知更新进度
	/// </summary>
	/// <param name="upSize"></param>
	private void OnUpProgress(int upSize)
	{
		int currentUpToldSize=0;
		
		if (currentLoadType == LoadResourceType.LoadResourceFile)
		{
			currentUpToldSize = mToldFileLoadSize + upSize;
		}
		
		if (mUpProgressC != null)
		{
			mUpProgressC(currentUpToldSize, GetToladSize());
		}
	}
	
	
	/// <summary>
	/// 通知开始一次新的更新
	/// </summary>
	/// <param name="times"></param>
	/// <param name="resourceItem"></param>
	private void OnUpTimesInfor(int times,int toldTimes,ResourceVersionItem resourceItem)
	{		
		if (mUpTimesInforC != null)
		{
			mUpTimesInforC(times, toldTimes, resourceItem.mClientVersion.ToString(), resourceItem.mResourceName, resourceItem.mByteCount);
		}
	}
	
	
	/// <summary>
	/// 通知准备进入游戏
	/// </summary>
	private void OnReadyEntryGame()
	{		
		if (mReadyEntryGameFunctionC != null)
		{
			mReadyEntryGameFunctionC();
		}
	}
	
	
    /// <summary>
    /// 存储当前需要关闭的资源
    /// </summary>
    public void SaveUpdateFinshList(bool useHttpVersion)
    {
        //更新新的数据
        CreateNewVersionData(useHttpVersion);

        mLocalVersionStr = ResourceVersionToString(mLocalVersionData);

        ///在外部存一个信息
        string localResourceVersionFile = externAssetsPath.Replace("\\", "/") + "/" + mResourceVersion;

        int pos = localResourceVersionFile.LastIndexOf('/');

        string dirPath = localResourceVersionFile.Substring(0, pos);

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        byte[] versionBuf = Encoding.Default.GetBytes(mLocalVersionStr);

        versionBuf = CompressEncryption.Encryption(versionBuf);

        File.WriteAllBytes(localResourceVersionFile, versionBuf);
    }

	/// <summary>
	/// 更新资源完毕
	/// </summary>
	private void OnUpVersionCompleted()
	{
        SaveUpdateFinshList(true);
        
        ///记录使用外部路径
        useExternAssets = true;
		
		if (mUpVersionCompletedC != null)
		{
			mUpVersionCompletedC();
		}
	}
	
	
	/// <summary>
	/// 更新客户端完毕
	/// </summary>
	private void OnUpVersionClientCompleted()
	{
        ///保存文件
        string localResourceVersionFile = externAssetsPath.Replace("\\", "/") + "/" + mHttpVersionData.ClientVersionInfor.mResourceName;
		
		int pos = localResourceVersionFile.LastIndexOf('/');
		
		string dirPath = localResourceVersionFile.Substring(0, pos);


        if (File.Exists(localResourceVersionFile))
		{
			
			//File.WriteAllBytes(localResourceVersionFile, wwwBytes);

			
			wwwBytes = null;                                                                     
			///这里调用GC是大资源包 Apk加载完毕  调用完GC后 立即释放大资源  方便之后的安装文件操作
			//System.GC.Collect();
			
			//更新新的数据
			mLocalVersionStr = mHttpVersionToldStr;
			
			string localVersionFile = externAssetsPath.Replace("\\","/") + "/" + mResourceVersion;
			
			int poss = localVersionFile.LastIndexOf('/');
			
			string dirPathF = localVersionFile.Substring(0,poss);
			
			if (!Directory.Exists(dirPathF))
			{
				Directory.CreateDirectory(dirPathF);
			}

			
			File.WriteAllText(localVersionFile, mLocalVersionStr);


            IEnumeratorManager.Instance.StartCoroutine(InitApk(localResourceVersionFile));            
		}
		else
		{
			DebugLoger.LogError("资源为空 -> ");
		}
		
		if (mUpVersionClientCompletedC != null)
		{
			mUpVersionClientCompletedC();
		}
	}
	

    /// <summary>
    /// 安装Apk
    /// </summary>
    /// <param name="localResourceVersionFile"></param>
    /// <returns></returns>
	IEnumerator InitApk(string localResourceVersionFile)
	{
        yield return new IEnumeratorManager.WaitForSeconds(0.5f);

        while(!File.Exists(localResourceVersionFile))
        {
            yield return new IEnumeratorManager.WaitForSeconds(0.5f);
        }
		
        CherishUtility.InstallPark(localResourceVersionFile);
		yield return new IEnumeratorManager.WaitForSeconds(1.5f);
		//DebugLoger.LogError("Application.Quit");
		Application.Quit();
    }

    /// <summary>
    /// 获取优先获取外部单个文件
    /// </summary>
    /// <param name="fileName"></param>
    public static byte[] GetFileData(string fileName)
    {
        string path = "";
        // 首先统一去外部路径进行获取 //
        if (UpResourceManager.useExternAssets)
        {
            //外部路径文件检测并读取
            path = externAssetsPath + "/" + fileName;
            path = path.Replace("\\", "/");
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
        }

        //构建内部文件路径
        //path = projectParkPath + "/" + fileName;
        //path = path.Replace("\\", "/");
        //DebugLoger.LogError("get file data 2 " + path);
        byte[] bufData = CherishUtility.GetFileData(projectParkPath.Replace("\\", "/"),fileName);
        return bufData;
    }


	/// <summary>
	/// 是否限制版本
	/// </summary>
	/// <returns></returns>
	public bool IsLimitFunVersion()
	{
        return true;

		if (!GameMain.Instance.limitVersionOpen)
		{
			return false;
		}

		if (mLocalApkVersionData.toldVersionValue > mHttpVersionData.toldVersionValue)
		{
			return true;
		}
		else
		{
			return false;
		}
	}




    /// <summary>
    /// 版本数据
    /// </summary>
	private class VersionData
	{
        public string Version = "0.0.0";
        public int toldVersionValue = 0;
        public int apkVersionValue = 0;

		public ResourceVersionItem ClientVersionInfor=new ResourceVersionItem();
		
		public System.Collections.Generic.List<ResourceVersionItem> ResourceVersionList = new System.Collections.Generic.List<ResourceVersionItem>();


        public void InitVersion()
        {
            DebugLoger.Log("InitVersion " + Version);
            string[] versionBit = Version.Split('.');
            DebugLoger.Log("versionBit[0][0] " + versionBit[0].Length);
            apkVersionValue = int.Parse(versionBit[0]) * 10000000 + int.Parse(versionBit[1]) * 10000;
            toldVersionValue = apkVersionValue + int.Parse(versionBit[2]);

        }
	}
	
    /// <summary>
    /// 版本数据条例
    /// </summary>
	private class ResourceVersionItem
	{
		public string mResourceName;
		
		public float mClientVersion;
		
		public string mMd5;
		
		public int mByteCount;


        public void SetValue(ResourceVersionItem source)
        {
            mResourceName = source.mResourceName;
            mClientVersion = source.mClientVersion;
            mMd5 = source.mMd5;
            mByteCount = source.mByteCount;
        }
	}
}
