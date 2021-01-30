using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class BuyStoreProcess : ProcessMessageBase
    {
        public BuyStoreProcess()
        {
            ID = (int)NetMessageType.GameWithUser_BuyStore_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new BuyStoreProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
			DebugLoger.Log(" 购买消息进入 ");
			Server.SC_BuyStore entryMsg = new Server.SC_BuyStore();
            try
            {
				entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			Rall.UIStoreWebViewPay.CallBackOrder(entryMsg);
		}
    }
}