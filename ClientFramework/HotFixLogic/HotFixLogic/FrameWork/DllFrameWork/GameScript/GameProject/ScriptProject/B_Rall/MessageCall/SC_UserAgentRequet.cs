using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 申请代理
    /// </summary>
    public class SC_UserAgentRequet : ProcessMessageBase
    {
        public SC_UserAgentRequet()
        {
            ID = (int)NetMessageType.GameWithUser_AgentRequet_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_UserAgentRequet();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_AgentRequirt entryMsg = new Server.SC_AgentRequirt();
            try
            {
				entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			if (entryMsg.result == 1)//成功
			{
				UINameSpace.UITipMessage.PlayMessage("已经发起代理申请,请静待消息!");
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("发起代理申请失败!");
			}
        }
    }
}