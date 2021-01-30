using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class RenfenceInfoProcess : ProcessMessageBase
    {
        public RenfenceInfoProcess()
        {
            ID = (int)NetMessageType.GameWithUser_RefenceInfo_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new RenfenceInfoProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_RefenceInfo entryMsg = new Server.SC_RefenceInfo();
            try
            {
				entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}
			GoableData.userValiadateInforWarp.Gold = entryMsg.goldCount;
			GoableData.userValiadateInforWarp.RechargeCount = entryMsg.rechargeCount;

			UINameSpace.UIRall.GetInstance().RefenceRoomKa();
		}
    }
}