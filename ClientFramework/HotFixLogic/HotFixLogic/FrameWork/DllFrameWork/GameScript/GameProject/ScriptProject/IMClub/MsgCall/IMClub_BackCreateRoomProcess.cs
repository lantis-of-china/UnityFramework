using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_BackCreateRoomProcess : ProcessMessageBase
    {
        public IMClub_BackCreateRoomProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_BackCreateRoom_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_BackCreateRoomProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_CreateGameRoom_MsgType");
            IMClub.SC_CreateGameRoom messageBack = new IMClub.SC_CreateGameRoom();
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
                UINameSpace.UITipMessage.PlayMessage("请先加入无开始的房间!");
            }
            else if (messageBack.result == 1)
            {
                UINameSpace.UITipMessage.PlayMessage("亲友圈房卡到达15开通,请联系群主投入房卡!");
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("条件限制!");
            }
        }
    }
}