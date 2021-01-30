using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class YanlongShareStudio : MonoBehaviour 
{
    #if (UNITY_IPHONE ) && !UNITY_EDITOR
    [DllImport("__Internal")]
    public static extern bool _WeiChatInstall();
    [DllImport("__Internal")]
    public static extern void _InstanceWeiChat(string AppId,string AppSecret, string Desc, string MessageTitle, string MessageContent);
    [DllImport("__Internal")]
    public static extern void _WeiChatAuth();
    [DllImport("__Internal")]
    public static extern string _GetWeiChatOpenId();
    
    
    [DllImport("__Internal")]
    public static extern void _WeiCharShareImage(string ImagePath);
    [DllImport("__Internal")]
    public static extern void _WeiCharShareImageToFriend(string ImagePath);
    [DllImport("__Internal")]
    public static extern void _WeiCharShareLink(string Title, string ContentText, string URL, string ImagePath);
    [DllImport("__Internal")]
    public static extern void _WeiCharShareLinkToTimeLine(string Title, string ContentText, string URL, string ImagePath);
    [DllImport("__Internal")]
    public static extern bool _ShareSinaWeiBo(string _TextContent, string _ImagePath, string _URL, string _Title, string _NotSupportMessage, string _SucessMessage, string _CancleMessage);
    [DllImport("__Internal")]
    public static extern bool _ShareTencentWeiBo(string _TextContent, string _ImagePath, string _URL, string _Title, string _NotSupportMessage, string _SucessMessage, string _CancleMessage);
    [DllImport("__Internal")]
    public static extern bool _ShareFaceBookWeiBo(string _TextContent, string _ImagePath, string _URL, string _Title, string _NotSupportMessage, string _SucessMessage, string _CancleMessage);
    [DllImport("__Internal")]
    public static extern bool _ShareTwitterWeiBo(string _TextContent, string _ImagePath, string _URL, string _Title, string _NotSupportMessage, string _SucessMessage, string _CancleMessage);
    [DllImport("__Internal")]
    public static extern void _OpenSystemShareActivity(string ImagePath, string Content);
#endif

    static string _MessageTitle = Application.systemLanguage == SystemLanguage.Chinese ? "分享提示" : "Share tip";
    static string _NotSupportMessage = Application.systemLanguage == SystemLanguage.Chinese ? "亲！你的系统没有内置该功能，你可以试试别的分享，抱歉！" : "Dear, your system is not built the share function, you can try something else to share, sorry!";
    static string _SucessMessage=Application.systemLanguage == SystemLanguage.Chinese ?"分享完成！":"Share sucess!";
    static string _CancleMessage = Application.systemLanguage == SystemLanguage.Chinese ? "分享失败！" : "Share failed!";

   
    
    /// <summary>
    /// 微信安装
    /// </summary>
    /// <returns></returns>
    public static bool WeiChatInstall()
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
         return _WeiChatInstall();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           return cls.CallStatic<bool>("WeiChatInstall");
        }
#endif
        return false;
    }

    //注册微信
    public static void InstanceWeiChat(string Desc, string MessageTitle, string MessageContent)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
         _InstanceWeiChat(SDKConfig.weiChatAppId, SDKConfig.weiChatAppSecret,Desc, MessageTitle, MessageContent);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           cls.CallStatic("RgistToWeiChat",SDKConfig.weiChatAppId,SDKConfig.weiChatAppSecret,MessageTitle,MessageContent);
        }
#endif
    }

    //微信登陆授权
    public static void WeiChatLoginAuth()
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
         _WeiChatAuth();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           cls.CallStatic("WXReqLoginAuth");
        }
#endif
    }

    //微信登陆授权代码
    public static string WeiChatGetAuthCode()
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
         return _GetWeiChatOpenId();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           return cls.CallStatic<string>("GetAuthCode");
        }
#endif
        return "null str";
    }















    //微信图片分享到朋友圈
    public static void WeiCharShareImage(string ImagePath)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _WeiCharShareImage(ImagePath);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
        
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           cls.CallStatic("ShareImageContentToWeiChatTimeLine",ImagePath);
        }
#endif
    }

    //微信图片分享给好友
    public static void WeiCharShareImageToFriend(string ImagePath)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _WeiCharShareImageToFriend(ImagePath);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
        
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           cls.CallStatic("ShareImageContentToWeiChatSceneSession",ImagePath);
        }
#endif
    }

    //微信连接分享
    public static void WeiCharShareLink(string Title, string ContentText, string URL, string ImagePath)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _WeiCharShareLink(Title, ContentText, URL, ImagePath);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           cls.CallStatic("ShareLinkContentToWeiCharSceneSession",Title,ContentText,URL,ImagePath);
        }
#endif
    }

    //微信连接分享到朋友圈
    public static void WeiCharShareLinkTimeLine(string Title, string ContentText, string URL, string ImagePath)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _WeiCharShareLinkToTimeLine(Title, ContentText, URL, ImagePath);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
        using (AndroidJavaClass cls = new AndroidJavaClass(SDKConfig.JavaUtilityName))
        {
           cls.CallStatic("ShareLinkContentToWeiCharTimeLine",Title,ContentText,URL,ImagePath);
        }
#endif
    }

    //新浪分享
    public static void ShareSinaWeiBo(string _TextContent, string _ImagePath, string _URL)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _ShareSinaWeiBo(_TextContent,_ImagePath,_URL,_MessageTitle,_NotSupportMessage,_SucessMessage,_CancleMessage);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
#endif
    }

    //腾讯微博分享
    public static void ShareTencentWeiBo(string _TextContent, string _ImagePath, string _URL)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _ShareTencentWeiBo(_TextContent, _ImagePath, _URL, _MessageTitle, _NotSupportMessage, _SucessMessage, _CancleMessage);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
#endif
    }

    //FaceBook分享
    public static void ShareFaceBookWeiBo(string _TextContent, string _ImagePath, string _URL)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _ShareFaceBookWeiBo(_TextContent, _ImagePath, _URL, _MessageTitle, _NotSupportMessage, _SucessMessage, _CancleMessage);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
#endif
    }

    //Twitter分享
    public static void ShareTwitterWeiBo(string _TextContent, string _ImagePath, string _URL)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _ShareTwitterWeiBo(_TextContent, _ImagePath, _URL, _MessageTitle, _NotSupportMessage, _SucessMessage, _CancleMessage);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
#endif
    }

    //打开系统分享
    public static void OpenSystemShareActivity(string ImagePath, string Content)
    {
#if (UNITY_IPHONE ) && !UNITY_EDITOR
        _OpenSystemShareActivity(ImagePath, Content);
#elif (UNITY_ANDROID && ! UNITY_EDITOR)
#endif
    }
}
