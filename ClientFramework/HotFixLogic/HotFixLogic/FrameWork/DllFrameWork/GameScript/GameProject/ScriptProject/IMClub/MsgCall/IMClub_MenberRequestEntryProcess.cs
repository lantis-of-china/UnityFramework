using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_MenberRequestEntryProcess : ProcessMessageBase
    {
        public IMClub_MenberRequestEntryProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_MenberRequestEntry_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_MenberRequestEntryProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            IMClub.SC_MenberRequestEntry messageBack = new IMClub.SC_MenberRequestEntry();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.request != null)
			{
				IMClub.GoableClubDataInfo.AddRequestToGroup(messageBack.request);

				IMClub.ClubListPanel.UpShowRequest();
			}
		}
    }
}