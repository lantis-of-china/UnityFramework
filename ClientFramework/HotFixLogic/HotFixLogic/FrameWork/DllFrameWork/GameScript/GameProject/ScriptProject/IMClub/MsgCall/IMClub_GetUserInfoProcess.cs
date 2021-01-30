using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 要求玩家获取玩家信息
    /// </summary>
    public class IMClub_GetUserInfoProcess : ProcessMessageBase
    {
        public IMClub_GetUserInfoProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_GetUserInfo_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_GetUserInfoProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_GetUserInfo_MsgType");
            IMClub.SC_GetUserInfo messageBack = new IMClub.SC_GetUserInfo();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
            catch(Exception ex)
            {
				DebugLoger.LogError("消息异常--------------" + ex.ToString());
                return;
            }

			if (messageBack.result == 1)
			{
				IMClub.RequestFriendPanel.SetUserInfoShow(messageBack);
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("没有查找到玩家信息!");
			}
        }
    }
}