using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_GetGroupsProcess : ProcessMessageBase
    {
        public IMClub_GetGroupsProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_GetGroups_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_GetGroupsProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            IMClub.SC_GetGroups messageBack = new IMClub.SC_GetGroups();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == (byte)1 && messageBack.groupList != null)
            {
                IMClub.RoomManager.ClearRooms();
                IMClub.GoableClubDataInfo.ClearGroups();
                IMClub.GoableClubDataInfo.AddGroups(messageBack.groupList);
				IMClub.GoableClubDataInfo.AddRequests(messageBack.requestList);

				if (messageBack.roomList != null)
                {
                    IMClub.RoomManager.AdddRooms(messageBack.roomList);
                }
                IMClub.ClubListPanel.CloseClubList();
                IMClub.ClubListPanel.UpdateClubList();
				IMClub.ClubListPanel.UpShowRequest();

				UINameSpace.UIIMClubMain.Instance.SetUpShowGroupFun();
            }
        }
    }
}