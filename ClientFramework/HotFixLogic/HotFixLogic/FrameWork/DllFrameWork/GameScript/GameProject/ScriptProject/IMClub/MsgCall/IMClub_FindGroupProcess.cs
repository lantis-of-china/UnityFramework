using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_FindGroupProcess : ProcessMessageBase
    {
        public IMClub_FindGroupProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_FindGroup_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_FindGroupProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("CLUBFindGroup");
            IMClub.SC_FindGroup messageBack = new IMClub.SC_FindGroup();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == (byte)1)
			{
				IMClub.JoinShowClubPanel.ShowPanelInfo(messageBack.groupInfo);
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("没用找到对应亲友圈信息!");
			}
        }
    }
}