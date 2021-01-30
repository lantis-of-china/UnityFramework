using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_RequestAddGroupProcess : ProcessMessageBase
    {
        public IMClub_RequestAddGroupProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_RequestAddClub_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_RequestAddGroupProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            IMClub.SC_RequestAddClub messageBack = new IMClub.SC_RequestAddClub();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == 0)
			{
				UINameSpace.UITipMessage.PlayMessage("申请失败!");
			}
			else if (messageBack.result == 1)
			{
				UINameSpace.UITipMessage.PlayMessage("申请成功，请等待群主确认");
			}
			else if (messageBack.result == 2)
			{
				UINameSpace.UITipMessage.PlayMessage("已经申请，请等待群主确认!");
			}
			else if (messageBack.result == 3)
			{
				UINameSpace.UITipMessage.PlayMessage("已经在亲友圈中了!");
			}
		}
    }
}