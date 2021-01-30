using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_UDPNotice : ProcessMessageBase
    {
        public SC_UDPNotice()
        {
            ID = (int)NetMessageType.LoginWithUser_UDPNotice_Back;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_UDPNotice();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.UDPNotice userEntry = new Server.UDPNotice();
            try
            {
				DateBuf = CompressEncryption.UnCompressDecompressData(DateBuf);

				userEntry.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			if (userEntry.type == "club")
			{
				IMClub.ClubItem.UDPNoticeMessageEntry(userEntry.content);
			}
			else
			{
				DebugLoger.LogError("未支持通知");
			}
        }
    }
}