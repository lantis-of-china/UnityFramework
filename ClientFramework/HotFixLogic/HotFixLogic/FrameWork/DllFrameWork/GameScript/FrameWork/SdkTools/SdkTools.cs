using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class SdkTools
    {
        /// <summary>
        /// 获取微信授权json
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static void GetWeiChatAuthJson(string code,Action<string> callFun)
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token"
                            + "?appid="
                            + SDKConfig.weiChatAppId
                            + "&secret="
                            + SDKConfig.weiChatAppSecret
                            + "&code="
                            + code
                            + "&grant_type=authorization_code";

            HttpTools.GetHttpData(url, callFun);
        }


        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        public static void GetWeiChatUserInfo(string accessToken, string openId,Action<string> callFun)
        {
            String url = "https://api.weixin.qq.com/sns/userinfo?access_token="
                        + accessToken
                        + "&openid="
                        + openId;


            HttpTools.GetHttpData(url, callFun);
        }
}
