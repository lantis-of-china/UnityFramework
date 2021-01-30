using QiPaiDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace IMClub
{
    public class IMCludWarp
    {

		/// <summary>
		/// 实例化完成
		/// </summary>
		/// <returns></returns>
		public static bool InitFinish()
        {

            return true;
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="conversation"></param>
        /// <param name="strMsg"></param>
        public static void SendTextMessage(string strMsg, Action<IAVIMMessage> sendCallBack)
        {
            Dictionary<string, object> messageMap = new Dictionary<string, object>();
            messageMap.Add("msgType", "user");
            messageMap.Add("content", strMsg);

            AVIMTextMessage textMsg = new AVIMTextMessage(LitJson.JsonMapper.ToJson(messageMap));
            
            if(sendCallBack!=null)
            {
                sendCallBack(textMsg);
            }
        }
    }
}
