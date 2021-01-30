using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class StringConfigClass
    {
		/// <summary>
		/// 登陆的时候需要版本好 对应服务器的版本
		/// </summary>
		public static int versionTold = 22;
		public static int loginPort = 2959;
        public static string loginIp = /*"703.3firegame.cn";//*/"127.0.0.1";
		public static string domAddr = "703.cdzxzjkj.cn";

        public static string bunidId = "com.dingfeng.shengshou";

        #region Fir分发配置
        public static string fir_api_token = "ea3b492b53adbee005cb3cd20fe996c4";
        public static string versito_android = "1.1";
        public static string versito_ios = "1.1";
	#endregion Fir分发配置

	    #region 微信需要的配置
	/// <summary>
	/// 微信AppId
	/// </summary>
	public static string weiChatAppId = "wx52d70cf5919866e7";//"wx7872a6fa816bedd1";
	/// <summary>
	/// 微信验证
	/// </summary>
	public static string weiChatAppSecret = "aad5bb965d2c7fa3d66366ad3a0e8964";//"ec1d5c75fe8150b003d6d448518750ac";
	/// <summary>
	/// 微信签名
	/// </summary>
	public static string weiChatSign = "060a38f1a7381eaab48d3a4e16171887";//"ce73e3b2e79d39556d72ad4dd3e64ccd";
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public static string weiChatMchId = "1481598942";
        #endregion 微信需要的配置

        #region 支付宝需要的配置
        #endregion 支付宝需要的配置

        #region 呀呀语音配置
        /// <summary>
        /// 丫丫语音AppId
        /// </summary>
        static public uint yayaAppId = 1001221;
	#endregion 呀呀语音配置

	#region LeanCloud
	public static string leanCloudId = "cObtCtufgJt49PhnvCSM0vql-gzGzoHsz";
	public static string leanCloudKey = "Jd0UFPI9Wy2An0EtM61vU904";
	#endregion LeanCloud
	/// <summary>
	/// 设置配置到SDK
	/// </summary>
	public static void SetToAppSdk()
        {
            SDKConfig.weiChatAppId = StringConfigClass.weiChatAppId;
            SDKConfig.weiChatAppSecret = StringConfigClass.weiChatAppSecret;
            SDKConfig.yayaAppId = StringConfigClass.yayaAppId;
        }


	/// <summary>
	/// IosAppStore 地址
	/// </summary>
	public static string IosDownloadUrl = "https://fir.im/zhongxiangIos";//"https://www.guanzhu.mobi/app/06pb";//"itms-apps://itunes.apple.com/us/app/id1230580462?mt=8";

	public static string AndroidDownloadUrl = "https://fir.im/zhongxiangIos";//"https://www.guanzhu.mobi/app/06pb";

        public static string qqNumber = "1000000";

        public static string weiChatNumber = "wxnumber";

        public static string GetDownloadUrl()
        {
            if(UnityEngine.Application.platform == UnityEngine.RuntimePlatform.Android)
            {
                return AndroidDownloadUrl;
            }
            else
            {
                return IosDownloadUrl;
            }
        }

        /// <summary>
        /// 是否开启功能
        /// </summary>
        public static bool canOpenHiddent = true; 
        public static bool CanOpenHiddent()
        {
            return canOpenHiddent;
        }

        /// <summary>
        /// 局部限制
        /// </summary>
        /// <returns></returns>
        public static bool CanOpenHiddentCheckLocak()
        {
            if(DateTime.Now.Ticks < new DateTime(2017,5,15).Ticks)
            {
                return false;
            }
            return true;
        }
    }
