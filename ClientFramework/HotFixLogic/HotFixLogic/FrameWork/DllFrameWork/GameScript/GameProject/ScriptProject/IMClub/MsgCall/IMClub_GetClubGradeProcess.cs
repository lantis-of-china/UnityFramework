using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_GetClubGradeProcess : ProcessMessageBase
    {
        public IMClub_GetClubGradeProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_GetClubGrade_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_GetClubGradeProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("SC_GetClubGrade_MsgType");
            IMClub.SC_GetClubGrade messageBack = new IMClub.SC_GetClubGrade();
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
				IMClub.ClubItem clubItem = IMClub.ClubListPanel.GetClubItemWithClubId(messageBack.clubId);
				if (clubItem != null)
				{
					clubItem.bindGwInfo.page = messageBack.page;
					clubItem.bindGwInfo.clubGradeList = messageBack.clubGradeList;
                    DebugLoger.Log("messageBack.clubId " + messageBack.clubId);

					if (IMClub.ClubItem.clubItemState != null && 
						IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == messageBack.clubId)
					{
						if (IMClub.ClubGradePanel_Select.clubGradePanel != null && IMClub.ClubGradePanel_Select.clubGradePanel.tablePanel != null)
						{
							IMClub.ClubGradePanel_Select.clubGradePanel.ShowItems();
						}
					}
				}				
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("操作失败!");
            }
        }
    }
}