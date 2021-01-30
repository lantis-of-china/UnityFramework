using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 从字节设置图标
/// </summary>
public class SetImageForHttpbytes
{
    /// <summary>
    /// 处理中的列表
    /// </summary>
    public static List<SetImageForHttpbytes> setImageBytesList = new List<SetImageForHttpbytes>();

    /// <summary>
    /// 设置Image 使用网络图标
    /// </summary>
    /// <param name="sourceImage"></param>
    /// <param name="url"></param>
    public static void SetImageFromUrl(Image sourceImage,string url)
    {
        SetImageForHttpbytes setImage = new SetImageForHttpbytes();
        setImage.targetImage = sourceImage;
        setImageBytesList.Add(setImage);

        HttpTools.GetHttpData(url, setImage.CallByteBack);
    }


    /// <summary>
    /// 要设置的Image
    /// </summary>
    public UnityEngine.UI.Image targetImage;


    /// <summary>
    /// 字节回调方法
    /// </summary>
    /// <param name="byteBuf"></param>
    public void CallByteBack(byte[] byteBuf)
    {
        setImageBytesList.Remove(this);
        if (byteBuf == null || targetImage == null)
        {
            return;
        }
        Texture2D t2 = new Texture2D(100, 100,TextureFormat.RGBA32,false);
        t2.LoadImage(byteBuf);
        Sprite sprite = Sprite.Create(t2, new Rect(0, 0, t2.width, t2.height), new Vector2(0.5f, 0.5f));
        targetImage.sprite = sprite;
        targetImage.overrideSprite = sprite;
    }
}

/// <summary>
/// 从字节设置图标
/// </summary>
public class SetRawImageForHttpbytes
{
    /// <summary>
    /// 处理中的列表
    /// </summary>
    public static List<SetRawImageForHttpbytes> setImageBytesList = new List<SetRawImageForHttpbytes>();

    /// <summary>
    /// 设置Image 使用网络图标
    /// </summary>
    /// <param name="sourceImage"></param>
    /// <param name="url"></param>
    public static void SetRawImageFromUrl(RawImage sourceImage, string url)
    {
        SetRawImageForHttpbytes setImage = new SetRawImageForHttpbytes();
        setImage.targetImage = sourceImage;
        setImageBytesList.Add(setImage);

        HttpTools.GetHttpData(url, setImage.CallByteBack);
    }


    /// <summary>
    /// 要设置的Image
    /// </summary>
    public UnityEngine.UI.RawImage targetImage;


    /// <summary>
    /// 字节回调方法
    /// </summary>
    /// <param name="byteBuf"></param>
    public void CallByteBack(byte[] byteBuf)
    {
        setImageBytesList.Remove(this);
        if (byteBuf == null || targetImage == null)
        {
            return;
        }
        Texture2D t2 = new Texture2D(100, 100, TextureFormat.RGBA32, false);
        t2.LoadImage(byteBuf);
        //Sprite sprite = Sprite.Create(t2, new Rect(0, 0, t2.width, t2.height), new Vector2(0.5f, 0.5f));
        targetImage.texture = t2;
        //targetImage.overrideSprite = sprite;
    }
}

/// <summary>
/// 从字节设置图标
/// </summary>
public class SetCircleImageForHttpbytes
{
    /// <summary>
    /// 处理中的列表
    /// </summary>
    public static List<SetCircleImageForHttpbytes> setImageBytesList = new List<SetCircleImageForHttpbytes>();

    /// <summary>
    /// 设置Image 使用网络图标
    /// </summary>
    /// <param name="sourceImage"></param>
    /// <param name="url"></param>
    public static void SetCircleImageFromUrl(CircleImage sourceImage, string url)
    {
		if (sourceImage == null)
		{
			DebugLoger.LogError("SetCircleImageFromUrl sourceImage null");
			return;
		}

		sourceImage.sprite = null;
		sourceImage.overrideSprite = null;
		SetCircleImageForHttpbytes setImage = new SetCircleImageForHttpbytes();
        setImage.targetImage = sourceImage;
        setImageBytesList.Add(setImage);

        HttpTools.GetHttpData(url, setImage.CallByteBack);
    }


    /// <summary>
    /// 要设置的Image
    /// </summary>
    public CircleImage targetImage;


    /// <summary>
    /// 字节回调方法
    /// </summary>
    /// <param name="byteBuf"></param>
    public void CallByteBack(byte[] byteBuf)
    {
        setImageBytesList.Remove(this);
        if (byteBuf == null || targetImage == null)
        {
            return;
        }
        Texture2D t2 = new Texture2D(100, 100, TextureFormat.RGBA32, false);
        t2.LoadImage(byteBuf);
        Sprite sprite = Sprite.Create(t2, new Rect(0, 0, t2.width, t2.height), new Vector2(0.5f, 0.5f));
        targetImage.sprite = sprite;
        targetImage.overrideSprite = sprite;
    }
}

public class HttpParamar
{
    public Action<string> httpCallBack_String;
    public Action<byte[]> httpCallBack_Bytes;
    /// <summary>
    /// 0文本 1字节流
    /// </summary>
    public int type;

    /// <summary>
    /// 下载处理
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public System.Collections.IEnumerator GetHttpDataProcess(string url)
    {
		int getTimes = 0;
		ReGet:

		getTimes++;
		UnityEngine.WWW get = new UnityEngine.WWW(url);
        while (!get.isDone)
        {
            yield return null;
        }

        if (!string.IsNullOrEmpty(get.error))
        {
			if (getTimes > 2)
			{
				DebugLoger.LogError("url 获取错误");
				if (type == 0)
				{
					httpCallBack_String("");
				}
				else if (type == 1)
				{
					httpCallBack_Bytes(null);
				}
			}
			else
			{
				yield return new IEnumeratorManager.WaitForSeconds(1.0f);
				goto ReGet;
			}
        }
        else
        {
            if (type == 0)
            {
                httpCallBack_String(get.text);
            }
            else if (type == 1)
            {
                httpCallBack_Bytes(get.bytes);
            }
        }

        HttpTools.httpList.Remove(this);
    }


	/// <summary>
	/// 下载处理
	/// </summary>
	/// <param name="url"></param>
	/// <param name="callBack"></param>
	/// <returns></returns>
	public System.Collections.IEnumerator PostHttpDataProcess(string url,byte[] buf)
	{
		UnityEngine.WWW get = new UnityEngine.WWW(url,buf);
		while (!get.isDone)
		{
			yield return null;
		}

		if (!string.IsNullOrEmpty(get.error))
		{
			DebugLoger.LogError("www error url:" + url + " error:" + get.error);
			if (type == 0)
			{
				httpCallBack_String("");
			}
			else if (type == 1)
			{
				httpCallBack_Bytes(null);
			}
		}
		else
		{
			if (type == 0)
			{
				if (!string.IsNullOrEmpty(get.text))
				{
					Debug.LogError("is txt back " + get.text);
				}
				httpCallBack_String(get.text);
			}
			else if (type == 1)
			{
				httpCallBack_Bytes(get.bytes);
			}
		}

		HttpTools.httpList.Remove(this);
	}
}

public class HttpTools
{
    public static List<HttpParamar> httpList = new List<HttpParamar>();
    /// <summary>
    /// 获取http数据
    /// </summary>
    /// <param name="url"></param>
    static public void GetHttpData(string url,Action<string> callBack)
    {
        if (callBack == null)
        {
            return;
        }
        HttpParamar hp = new HttpParamar();
        hp.httpCallBack_String = callBack;
        hp.type = 0;
        httpList.Add(hp);
        IEnumeratorManager.Instance.StartCoroutine(hp.GetHttpDataProcess(url));
    }

    /// <summary>
    /// 获取http数据
    /// </summary>
    /// <param name="url"></param>
    static public void GetHttpData(string url, Action<byte[]> callBack)
    {
        if (callBack == null)
        {
            return;
        }
        HttpParamar hp = new HttpParamar();
        hp.httpCallBack_Bytes = callBack;
        hp.type = 1;
        httpList.Add(hp);
        IEnumeratorManager.Instance.StartCoroutine(hp.GetHttpDataProcess(url));
    }

	static public void PostHttpData(string url,byte[] postData, Action<string> callBack)
	{
		if (callBack == null)
		{
			return;
		}

		HttpParamar hp = new HttpParamar();
		hp.httpCallBack_String = callBack;
		hp.type = 0;
		httpList.Add(hp);
		IEnumeratorManager.Instance.StartCoroutine(hp.PostHttpDataProcess(url,postData));
	}
}

public class HttpHelper
{
    /// <summary>  
    /// 创建GET方式的HTTP请求  
    /// </summary>  
    public static HttpWebResponse CreateGetHttpResponse(string url, int timeout, string userAgent, CookieCollection cookies)
    {
        HttpWebRequest request = null;
        if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        {
            //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(CheckValidationResult);

            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version11;    //http版本，默认是1.1,这里设置为1.0
            request.UseDefaultCredentials = true;
        }
        else
        {
            request = WebRequest.Create(url) as HttpWebRequest;
        }
        request.Method = "GET";

        //设置代理UserAgent和超时
        //request.UserAgent = userAgent;
        //request.Timeout = timeout;
        if (cookies != null)
        {
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);
        }
        return request.GetResponse() as HttpWebResponse;
    }

    /// <summary>  
    /// 创建POST方式的HTTP请求  
    /// </summary>  
    public static HttpWebResponse CreatePostHttpResponse(string url,string postData, int timeout, string userAgent, CookieCollection cookies)
    {
        HttpWebRequest request = null;
        //如果是发送HTTPS请求  
        if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            //request.ProtocolVersion = HttpVersion.Version10;
        }
        else
        {
            request = WebRequest.Create(url) as HttpWebRequest;
        }
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        //设置代理UserAgent和超时
        //request.UserAgent = userAgent;
        //request.Timeout = timeout; 

        if (cookies != null)
        {
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);
        }
        //发送POST数据  

            byte[] data = Encoding.ASCII.GetBytes(postData.ToString());
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        
        string[] values = request.Headers.GetValues("Content-Type");
        return request.GetResponse() as HttpWebResponse;
    }

    /// <summary>
    /// 获取请求的数据
    /// </summary>
    public static string GetResponseString(HttpWebResponse webresponse)
    {
        using (Stream s = webresponse.GetResponseStream())
        {
            StreamReader reader = new StreamReader(s, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
    
    /// <summary>
    /// 验证证书
    /// </summary>
    private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        if (errors == SslPolicyErrors.None)
            return true;
        return false;
    }
}