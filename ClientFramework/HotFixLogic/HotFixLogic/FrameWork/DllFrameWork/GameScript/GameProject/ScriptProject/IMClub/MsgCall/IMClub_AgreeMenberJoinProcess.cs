using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_AgreeMenberJoinProcess : ProcessMessageBase
    {
        public IMClub_AgreeMenberJoinProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_AgrentMenberJoin_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_AgreeMenberJoinProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
			UINameSpace.UIWaitting.RemoveShowWaitting("SC_AgrentMenberJoin_MsgType");
			IMClub.SC_AgrentMenber messageBack = new IMClub.SC_AgrentMenber();
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
				UINameSpace.UITipMessage.PlayMessage("操作失败!");
				return;
			}

			UINameSpace.UITipMessage.PlayMessage("操作成功!");
			IMClub.GoableClubDataInfo.RemoveRequestToGroup(messageBack.clubId, messageBack.menberId);
			List<IMClub.P_RequestInfo> requestList = IMClub.GoableClubDataInfo.GetAllGroupRequest();
			IMClub.AgreeRequestPanel.OpenInfo(requestList,false);
			IMClub.ClubListPanel.UpShowRequest();
		}
    }
}