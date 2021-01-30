using UnityEngine;
using System.Collections;

/*
'***********************************************
'类 名 称 : MobileUtility
'命名空间 : Assets.Scripts.CYEngine.Mobile
'创建时间 : 2015/2/12 17:59:09
'作    者 : Tank
'修改时间 :
'修 改 人 :
'版 本 号 : v1.0.0
'*******Copyright (c) 2015, ********
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class SDKConfig
{
    static public string JavaUtilityName = "com.cherish.cherishutility.CherishUtility";
    static public string JavaUtilityPath = "com/cherish/cherishutility/CherishUtility";

    /// <summary>
    /// 微信AppId
    /// </summary>
    public static string weiChatAppId { get; set; }//= "wxf87fbfb7c51703fd";//"wx3f61b80c352c14e3";
    /// <summary>
    /// 微信验证
    /// </summary>
    public static string weiChatAppSecret { get; set; }//= "a4fcf3353a548b0f0099faaa9fb5eea9";//"48cb1dc71ab4b0784dab739fb75e30c3";
    /// <summary>
    /// 丫丫语音AppId
    /// </summary>
    static public uint yayaAppId { get; set; }
}

public static class CherishUtility
{
#if UNITY_IPHONE && !UNITY_EDITOR
    //[DllImport("__Internal")]
    //extern static public void iosCallSdkApi(string apiName, string strJson);

    [DllImport("__Internal")]
    extern static public int _GetBattlePerValue();

    [DllImport("__Internal")]
    extern static public bool _IsNetWorkWifi();

	[DllImport("__Internal")]
    extern static public void _TakePhoto(int type);

	[DllImport("__Internal")]
    extern static public int _GetTakePhontResult();

	[DllImport("__Internal")]
	extern static public void _AliPay(string orderString,string appScheme);

	[DllImport("__Internal")]
	extern static public void _SetClipboard(string str);
	
	[DllImport("__Internal")]
	extern static public void _WeiChatPay(string partnerId,string prepayId,string nonceStr,string timeStamp,string sign);
#endif

	static public void init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
                using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
                {
                    cls.CallStatic("init");
                }
        //#elif UNITY_IPHONE && !UNITY_EDITOR
                //return iosCallSdkApi(apiName, args);
#else

#endif


        if (Application.isMobilePlatform)
        {
            RegisterBatteryLevelReceiver();
            YanlongShareStudio.InstanceWeiChat("", "", "");
        }
    }

    /// <summary>
    /// 获取游戏版本号
    /// </summary>
    /// <returns></returns>
    static public string getVersion()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<string>("getVersion");
        }
//#elif UNITY_IPHONE && !UNITY_EDITOR
        //return iosCallSdkApi(apiName, args);
#else
        return "1.0";
#endif

    }


    static public bool isEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif

    }

    /// <summary>
    /// 获取平台名字
    /// </summary>
    /// <returns></returns>
    static public string getPlatformName()
    {
#if  UNITY_STANDALONE_WIN
        return "web";
#elif UNITY_ANDROID
        return "android";
#elif UNITY_IPHONE
        return "ios";
#else
        return "editor";
#endif

    }

    /// <summary>
    /// 获取游戏包名
    /// </summary>
    /// <returns></returns>
    static public string getApkName()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<string>("getApkName");
        }
//#elif UNITY_IPHONE && !UNITY_EDITOR
        //return iosCallSdkApi(apiName, args);
#else
        return "cn.com.cywx";
#endif
    }


	/// <summary>
	/// 微信支付
	/// </summary>
	/// <param name="ms"></param>
	static public void WeiChatPay(string partnerId, string prepayId, string nonceStr, string timeStamp, string sign)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("WeiChatPay",partnerId,prepayId,nonceStr,timeStamp,sign);
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
		_WeiChatPay(partnerId,prepayId,nonceStr,timeStamp,sign);
#elif UNITY_EDITOR
#endif
	}

	/// <summary>
	/// 支付宝
	/// </summary>
	/// <param name="ms"></param>
	static public void AliPay(string orderInfo)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("StartPay",orderInfo);
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
		_AliPay(orderInfo,"zxalipay");
#elif UNITY_EDITOR
#endif
	}

	/// <summary>
	/// 重启
	/// </summary>
	/// <param name="ms"></param>
	static public void DoReStar(int ms=200)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("DoRestart",ms);
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
		Application.Quit();		
#elif UNITY_EDITOR
		UnityEditor.EditorApplication.Exit(0);
#endif
	}

	/// <summary>
	/// 获取项目在sd卡中的路径
	/// </summary>
	/// <returns></returns>
	static public string getProjectSDCardPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<string>("getProjectSDCardPath");
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
        //return iosCallSdkApi(apiName, args);
        return Application.streamingAssetsPath;
#else
        return System.IO.Directory.GetCurrentDirectory();
#endif

    }


    /// <summary>
    /// 获取sd卡剩余容量
    /// </summary>
    /// <returns></returns>
    static public long getSDCardFreeSize()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<long>("getSDCardFreeSize");
        }
//#elif UNITY_IPHONE && !UNITY_EDITOR
        //return iosCallSdkApi(apiName, args);
#else
        return -1;
#endif

    }

    /// <summary>
    /// sd卡是否激活
    /// </summary>
    /// <returns></returns>
    static public bool isSDCardActive()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<bool>("isSDCardActive");
        }
//#elif UNITY_IPHONE && !UNITY_EDITOR
        //return iosCallSdkApi(apiName, args);
#else
        return true;
#endif

    }

    /// <summary>
    /// 是否在wifi模式
    /// </summary>
    /// <returns></returns>
    static public bool isNetWorkWifi()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
//        {
//            return cls.CallStatic<bool>("isNetWorkWifi");
//        }
//#elif UNITY_IPHONE && !UNITY_EDITOR
//        return _IsNetWorkWifi();
//#else

        if(Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return true;
        }
        return false;
//#endif
    }

    /// <summary>
    /// 是否在3g模式
    /// </summary>
    /// <returns></returns>
    static public bool isNetWorkMoble()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
//        {
//            return cls.CallStatic<bool>("isNetWorkMoble");
//        }
////#elif UNITY_IPHONE && !UNITY_EDITOR
//        //return iosCallSdkApi(apiName, args);

//#else
        if(Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return true;
        }
        return true;
//#endif
        
    }

    /// <summary>
    /// 获取手机型号
    /// </summary>
    /// <returns></returns>
    static public string getPhoneModel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<string>("getPhoneModel");
        }
//#elif UNITY_IPHONE && !UNITY_EDITOR
        //return iosCallSdkApi(apiName, args);
#endif
        return "";
    }


    /// <summary>
    /// 获取Android 内部文件
    /// </summary>
    /// <param name="fileName"></param>
    static public byte[] GetFileData(string pathStart, string pathEnd)
    {		
			string path = pathStart + "/" + pathEnd;
#if UNITY_ANDROID && !UNITY_EDITOR
			IntPtr clazzPtr = AndroidJNI.FindClass(SDKConfig.JavaUtilityPath);
            IntPtr methodPtr = AndroidJNI.GetStaticMethodID(clazzPtr, "getFileData", "(Ljava/lang/String;)[B");
            IntPtr v1 = AndroidJNI.NewStringUTF(pathEnd);
            jvalue j1 = new jvalue();
            j1.l = v1;
            IntPtr resPtr = AndroidJNI.CallStaticObjectMethod(clazzPtr, methodPtr, new jvalue[] { j1 });//调用
            byte[] b = AndroidJNI.FromByteArray(resPtr);
            //删除Local Ref。methodPtr不需要手动删除，因为它不是一个jobject对象。   
            AndroidJNI.DeleteLocalRef(clazzPtr);
            AndroidJNI.DeleteLocalRef(v1);
            AndroidJNI.DeleteLocalRef(resPtr);
            return b;
#else
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
#endif
    }

    /// <summary>
    /// 获取Android 内部文件
    /// </summary>
    /// <param name="fileName"></param>
    static public byte[] GetFileDataWithPath(string filePath)
    {
        string path = Path.GetDirectoryName(filePath);
        string pathEnd = filePath.Replace(Application.streamingAssetsPath + "/", "");

#if UNITY_ANDROID && !UNITY_EDITOR
        
        if(filePath.Contains(Application.streamingAssetsPath))
        {
			IntPtr clazzPtr = AndroidJNI.FindClass(SDKConfig.JavaUtilityPath);
            IntPtr methodPtr = AndroidJNI.GetStaticMethodID(clazzPtr, "getFileData", "(Ljava/lang/String;)[B");
            IntPtr v1 = AndroidJNI.NewStringUTF(pathEnd);
            jvalue j1 = new jvalue();
            j1.l = v1;
            IntPtr resPtr = AndroidJNI.CallStaticObjectMethod(clazzPtr, methodPtr, new jvalue[] { j1 });//调用
            byte[] b = AndroidJNI.FromByteArray(resPtr);
            //删除Local Ref。methodPtr不需要手动删除，因为它不是一个jobject对象。   
            AndroidJNI.DeleteLocalRef(clazzPtr);
            AndroidJNI.DeleteLocalRef(v1);
            AndroidJNI.DeleteLocalRef(resPtr);
            return b;            
        }
        else
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            Debug.Log("找不到资源:" + filePath);
            return null;
        }
#else
        if (File.Exists(filePath))
        {
            return File.ReadAllBytes(filePath);
        }

        Debug.Log("找不到资源:" + filePath);
        return null;
#endif
    }


    /// <summary>
    /// 获取内部文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    static public byte[] GetFileDataInternal(string pathStart,string pathEnd)
    {
		string path = pathStart + "/" + pathEnd;
#if UNITY_ANDROID && !UNITY_EDITOR
        IntPtr clazzPtr = AndroidJNI.FindClass(SDKConfig.JavaUtilityPath);
        IntPtr methodPtr = AndroidJNI.GetStaticMethodID(clazzPtr, "getFileData", "(Ljava/lang/String;)[B");
		IntPtr v1 = AndroidJNI.NewStringUTF(pathEnd);
        jvalue j1 = new jvalue();
        j1.l = v1;
        IntPtr resPtr = AndroidJNI.CallStaticObjectMethod(clazzPtr, methodPtr, new jvalue[] { j1 });//调用
        byte[] b = AndroidJNI.FromByteArray(resPtr);        
        //删除Local Ref。methodPtr不需要手动删除，因为它不是一个jobject对象。   
        AndroidJNI.DeleteLocalRef(clazzPtr);
        AndroidJNI.DeleteLocalRef(v1);
        AndroidJNI.DeleteLocalRef(resPtr);
        return b;
#else
		if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }

        return null;
#endif
    }


    public static bool isMobile()
    {
#if UNITY_EDITOR
        return false;
#else
        return true;
#endif
    }


    /// <summary>
    /// 注册电量广播接听
    /// </summary>
    public static void RegisterBatteryLevelReceiver()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("RegisterBatteryLevelReceiver");
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
#else
#endif
    }

    /// <summary>
    /// 获取电量
    /// </summary>
    public static int GetBattlePerValue()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<int>("GetBattlePerValue");
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
         return _GetBattlePerValue();
#else
        return 100;
#endif
    }





    /// <summary>
    /// 调用SDK静态函数
    /// </summary>
    /// <param name="className"></param>
    /// <param name="functionName"></param>
    /// <param name="p"></param>
    public static void CallStaticFunction(string className, string functionName, params object[] args)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(className))
        {
            if (cls != null)
            {
                cls.CallStatic(functionName, args);
            }
            else
            {
               
            }
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
    string tmpStr = "{\"func\":\"" + functionName +  "\"";
	  
    if (args != null && args.Length > 0)
    {
      tmpStr += ",";
    
        int argCount = args.Length;
      for (int i = 0; i < argCount; ++i)
      {
    	  if (i%2 == 0)
    	  {
    		  tmpStr += ("\"" + args[i] + "\":");
    	  }
    	  else
    	  {
    		  if ((args[i] as string) != null)
    		  {
    			  tmpStr += ("\"" + args[i] + "\"");
    		  }
    		  else
    		  {
                    tmpStr += args[i].ToString();
    		  }
    		  
    		  if (i != (argCount - 1))
    		  {
    			  tmpStr += ",";
    		  }			  
    	  }
    
      }		  
    }

    tmpStr += "}";
	  
    //LogManager.LogError("in app: CallUnityMessage=> " + tmpStr);
    //iosCallSdkApi(functionName, tmpStr);
#endif
    }




    /// <summary>
    /// 获取文件协议
    /// </summary>
    public static string GetFileProtocol()
    {        
#if (UNITY_ANDROID && !UNITY_EDITOR)
		return "jar:file://";
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		return "";
#elif (UNITY_ANDROID && UNITY_EDITOR)
		return "file:///";
#elif (UNITY_IPHONE && UNITY_EDITOR)
		return "file://";
#elif (UNITY_EDITOR)
        return "file://";
#endif
        return "";
    }

    public static RuntimePlatform GetPlatform()
    {
#if (UNITY_EDITOR)
        return RuntimePlatform.WindowsPlayer;
#elif (UNITY_IPHONE)
		return RuntimePlatform.IPhonePlayer;
#elif (UNITY_ANDROID)
		return RuntimePlatform.Android;
#endif
        return RuntimePlatform.WindowsPlayer;
    }

	/// <summary>
	/// 拷贝到剪贴板
	/// </summary>
	public static void SetClipboard(string txt)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("SetClipboard",txt);
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
           _SetClipboard(txt);
#else
		//Application.OpenURL(url);
#endif
	}

		/// <summary>
		/// 拷贝到剪贴板
		/// </summary>
		public static void InstallPark(string path)
		{
		#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
		{
		cls.CallStatic("installApk",path);
		}
		#elif UNITY_IPHONE && !UNITY_EDITOR
		#else
		#endif
	}


	/// <summary>
	/// 开启webView
	/// </summary>
	/// <param name="url"></param>

	public static void OpenWebView(string url)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("PutGotoWebView",url);
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
            Application.OpenURL(url);
#else
        Application.OpenURL(url);
#endif
    }


	public static void PlayMovie()
	{
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
		Handheld.PlayFullScreenMovie("movie.mp4", Color.black, FullScreenMovieControlMode.Hidden);
#endif
	}

	/// <summary>
	/// 开启相片裁剪
	/// type 0-拍照 1-相册
	/// </summary>
	/// <param name="url"></param>

	public static void OpenTakePhoto(int type)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            cls.CallStatic("TakePhoto",type);
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
		_TakePhoto(type);
#else
#endif
	}

	/// <summary>
	/// 获取裁剪返回结果
	/// </summary>
	/// <returns></returns>
	public static int GetTakePhotoResult()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<int>("GetTakePhontResult");
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
		return _GetTakePhontResult();
#else
		return 0;
#endif
	}


	/// <summary>
	/// 获取裁剪返回结果
	/// </summary>
	/// <returns></returns>
	public static int GetInActivityProcess()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
            return cls.CallStatic<int>("GetInActivityActivity");
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
		return 0;
#else
		return 0;
#endif
	}
}


