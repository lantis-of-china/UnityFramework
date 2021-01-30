using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 要求玩家获取玩家信息
    /// </summary>
    public class IMClub_RequestMenberJoinClubProcess : ProcessMessageBase
    {
        public IMClub_RequestMenberJoinClubProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_RequestMenberJoinClub_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_RequestMenberJoinClubProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_RequestMenberJoinClub_MsgType");
            IMClub.SC_RequestMenberJoinClub messageBack = new IMClub.SC_RequestMenberJoinClub();
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
				UINameSpace.UITipMessage.PlayMessage("请求成功!");
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("请求失败!");
			}
        }
    }
}