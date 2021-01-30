using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_SetSignProcess : ProcessMessageBase
    {
        public IMClub_SetSignProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_SetSign_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_SetSignProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("SC_SetSign");
            IMClub.SC_SetClubSign messageBack = new IMClub.SC_SetClubSign();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == 1)
            {
                IMClub.ClubInfoPanel.SetSign(messageBack.clubId, messageBack.sign);
                UINameSpace.UITipMessage.PlayMessage("修改成功!");
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("操作失败!");
            }
        }
    }
}