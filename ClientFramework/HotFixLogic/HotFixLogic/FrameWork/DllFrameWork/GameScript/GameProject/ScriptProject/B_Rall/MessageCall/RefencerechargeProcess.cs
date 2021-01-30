using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class RefencerechargeProcess : ProcessMessageBase
    {
        public RefencerechargeProcess()
        {
            ID = (int)NetMessageType.GameWithUser_Refencerecharge_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new RefencerechargeProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.Refencerecharge_SC entryMsg = new Server.Refencerecharge_SC();
            try
            {
				entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}
			GoableData.gameCoreData._roleInfor.rechargeCount = entryMsg.rechargeCount;

			if (GoableData.RefenceGameEven != null)
			{
				GoableData.RefenceGameEven();
			}
		}
    }
}