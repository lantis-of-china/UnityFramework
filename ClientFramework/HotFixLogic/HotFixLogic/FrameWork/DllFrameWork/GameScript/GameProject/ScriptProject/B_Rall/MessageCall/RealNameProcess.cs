using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 
    /// </summary>
    public class RealNameProcess : ProcessMessageBase
    {
        public RealNameProcess()
        {
            ID = (int)NetMessageType.GameWithUser_RealName_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new RealNameProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_RealName entryMsg = new Server.SC_RealName();
            try
            {
				entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			if (entryMsg.result == 0)
			{
				UINameSpace.UITipMessage.PlayMessage("认证失败!");
				return;
			}

			UINameSpace.UITipMessage.PlayMessage("认证成功!");
			GoableData.userValiadateInforWarp.realName = entryMsg.realName;
			GoableData.userValiadateInforWarp.realId = entryMsg.realId;
			GoableData.userValiadateInforWarp.realPhone = entryMsg.realPhone;
		}
    }
}