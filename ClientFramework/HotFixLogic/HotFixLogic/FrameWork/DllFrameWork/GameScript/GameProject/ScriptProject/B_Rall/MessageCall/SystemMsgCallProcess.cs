using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SystemMsgCallProcess : ProcessMessageBase
    {
        public SystemMsgCallProcess()
        {
            ID = (int)NetMessageType.GameWithUser_GetSystemMsg_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SystemMsgCallProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_SystemMsg userMsg = new Server.SC_SystemMsg();
            try
            {
                userMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			GoableData.UISystemMsg.systemMsgList = userMsg.msgList;

            ///调用界面更新  或者打开界面的时候才更新
        }
    }
}