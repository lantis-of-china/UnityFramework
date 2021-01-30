using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_TipMessageProcess : ProcessMessageBase
    {
        public SC_TipMessageProcess()
        {
            ID = (int)NetMessageType.GameWithUser_TipMessage_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_TipMessageProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.TipMessage_SC userEntry = new Server.TipMessage_SC();
            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			if (userEntry.messageType == (int)TipMessageType.VersionError)
            {
				UINameSpace.UILogin.ShowLoginButton();
				UINameSpace.UITipMessage.PlayMessage("版本过旧,请尝试使用最新版本!");
            }
			else if (userEntry.messageType == (int)TipMessageType.JoinClubEntryRoom)
			{
				UINameSpace.UITipMessage.PlayMessage("请从亲友圈加入游戏!");
			}
			else if(userEntry.messageType == (int)TipMessageType.NoLogine)
            {
                //当前没有登录
                UINameSpace.UITipMessage.PlayMessage("当前状态为离线状态,请重新登陆再试!");
            }
            else if (userEntry.messageType == (int)TipMessageType.ServerOverflover)
            {
                //服务器已满
                UINameSpace.UITipMessage.PlayMessage("服务器已满!");
            }
            else if (userEntry.messageType == (int)TipMessageType.ValidateNotPass)
            {
                //验证错误
                UINameSpace.UITipMessage.PlayMessage("验证错误,请重新登陆!");
            }
            else if (userEntry.messageType == (int)TipMessageType.NotRoom)
            {                
                //房间不存在
                UINameSpace.UITipMessage.PlayMessage("请求房间不存在!");
                UINameSpace.UIWaitting.RemoveShowWaitting(Rall.ConfigProject.entryGameRoomBack);
				if (string.IsNullOrEmpty(Rall.ConfigProject.currentRallName))
				{
					UIObject ui = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.ConfigProject.currentRallName);
					if (ui != null)
					{
						ui.OnGameBackRall();
						Rall.ConfigProject.currentRallName = "";
					}
				}

				if (userEntry.messagePamars != null && userEntry.messagePamars.Count > 0)
				{
					int roomId = userEntry.messagePamars[0];
					IMClub.P_RoomInfo roomInfo = IMClub.RoomManager.GetRoom(roomId);
					if (roomInfo != null && !string.IsNullOrEmpty(roomInfo.clubId) && roomInfo.clubId != IMClub.RoomManager.generalClub)
					{
						IMClub.RoomManager.RemoveRoom(roomInfo.clubId, roomId);
						if (IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == roomInfo.clubId)
						{
							//当前亲友圈
							if (IMClub.ClubRoomPanel_Select.Instance != null && IMClub.ClubRoomPanel_Select.Instance.IsActive())
							{
								IMClub.ClubRoomPanel_Select.Instance.UnReleseRoom(roomId);
							}
						}
					}
				}
            }
            else if (userEntry.messageType == (int)TipMessageType.NotRoomInfo)
            {
                //没有可获取的房间信息
                UINameSpace.UITipMessage.PlayMessage("没有可获取的房间数据!");
            }
            else if (userEntry.messageType == (int)TipMessageType.NotRoomMasterControl)
            {
                //没有房主权限 不能执行操作
                UINameSpace.UITipMessage.PlayMessage("当前操作需要房主权限!");
            }
            else if (userEntry.messageType == (int)TipMessageType.NotInRoomCantControl)
            {
                //没有在房间中不能执行房间中操作
                UINameSpace.UITipMessage.PlayMessage("没有在游戏房间中无法执行当前操作!");
            }
            else if (userEntry.messageType == (int)TipMessageType.CantVote)
            {
                //发起投票无效
                UINameSpace.UITipMessage.PlayMessage("当前阶段不能发起该投票!");
            }
            else if(userEntry.messageType == (int)TipMessageType.NotEngothRechange)
            {
                //钻石不足 不能开房
                UINameSpace.UITipMessage.PlayMessage("房卡不足,无法创建房间!");
                UINameSpace.UIWaitting.RemoveShowWaitting(Rall.ConfigProject.entryGameRoomBack);
            }
            else if(userEntry.messageType == (int)TipMessageType.CantLeaveRoom)
            {
                UINameSpace.UITipMessage.PlayMessage("当局未完成,不能退出房间!");
            }
            else if (userEntry.messageType == (int)TipMessageType.CantUnReleseRoom)
            {
                UINameSpace.UITipMessage.PlayMessage("当前处于牌局阶段,请先发起投票结束牌局!");
            }
            else if (userEntry.messageType == (int)TipMessageType.RoomFull)
            {
                UINameSpace.UITipMessage.PlayMessage("房间已满!");
                UINameSpace.UIWaitting.RemoveShowWaitting(Rall.ConfigProject.entryGameRoomBack);
            }
        }
    }
}