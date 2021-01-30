using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClub
{
    public class MessageSend
    {
        /// <summary>
        /// 创建亲友圈
        /// </summary>
        /// <param name="groupName"></param>
        public static void CreateGroup(string groupName)
        {
            CS_CreateGroup sendMsg = new CS_CreateGroup();
            sendMsg.UserValiadate = GoableData.userValiadateInfor;
            sendMsg.groupName = groupName;

            new TcpNet<CS_CreateGroup>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_CreateGroup_MsgType);
        }

        /// <summary>
        /// 查找群组
        /// </summary>
        /// <param name="groupId"></param>
        public static void FindGroup(string groupId)
        {
            CS_FindGroup sendMsg = new CS_FindGroup();
            sendMsg.clubId = groupId;
            sendMsg.UserValiadate = GoableData.userValiadateInfor;

            new TcpNet<CS_FindGroup>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_FindGroup_MsgType);
        }

		/// <summary>
		/// 添加群组请求
		/// </summary>
		/// <param name="groupId"></param>
		public static void AddGroupRequeset(string clubId)
		{
			CS_RequestAddClub sendMsg = new CS_RequestAddClub();
			sendMsg.clubId = clubId;
			sendMsg.UserValiadate = GoableData.userValiadateInfor;

			new TcpNet<CS_RequestAddClub>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_RequestAddClub_MsgType);
		}

		public static void AgreeRequestClub(string clubId,int menberId,byte type)
		{
			CS_AgrentMenberJoin sendMsg = new CS_AgrentMenberJoin();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.clubId = clubId;
			sendMsg.menberId = menberId;
			sendMsg.controlType = type;

			new TcpNet<CS_AgrentMenberJoin>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_AgrentMenberJoin_MsgType);
		}

		/// <summary>
		/// 添加群组
		/// </summary>
		/// <param name="groupId"></param>
		public static void AddGroup(string clubId)
        {
            CS_AddGroup sendMsg = new CS_AddGroup();
            sendMsg.clubId = clubId;
            sendMsg.UserValiadate = GoableData.userValiadateInfor;

            new TcpNet<CS_AddGroup>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_AddGroup_MsgType);
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="clubId"></param>
        /// <param name="menberId"></param>
        public static void DeleteMenber(string clubId,int menberId)
        {
            CS_DeleteMenber deleteMenber = new CS_DeleteMenber();
            deleteMenber.UserValiadate = GoableData.userValiadateInfor;
            deleteMenber.clubId = clubId;
            deleteMenber.menberId = menberId;
            new TcpNet<CS_DeleteMenber>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, deleteMenber, (int)IMClub.NetMessageType.CS_DeleteMenber_MsgType);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="clubId"></param>
        /// <param name="menberId"></param>
        public static void AddBlackMenber(string clubId, int menberId)
        {
            CS_AddBlackList addBlackMenber = new CS_AddBlackList();
            addBlackMenber.UserValiadate = GoableData.userValiadateInfor;
            addBlackMenber.clubId = clubId;
            addBlackMenber.menberId = menberId.ToString();
            new TcpNet<CS_AddBlackList>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, addBlackMenber, (int)IMClub.NetMessageType.CS_AddBlackList_MsgType);
        }

        /// <summary>
        /// 获取组列表
        /// </summary>
        public static void GetGroups()
        {
            CS_GetGroups sendMsg = new CS_GetGroups();
            sendMsg.menberId = int.Parse(GoableData.userValiadateInfor.DatingNumber);
            sendMsg.UserValiadate = GoableData.userValiadateInfor;

            new TcpNet<CS_GetGroups>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_GetGroups_MsgType);
        }

        /// <summary>
        /// 获取亲友圈成员
        /// </summary>
        /// <param name="groupId"></param>
        public static void GetMenbers(string groupId)
        {
            CS_GetMenbers sendMsg = new CS_GetMenbers();
            sendMsg.clubId = groupId;
            sendMsg.UserValiadate = GoableData.userValiadateInfor;

            new TcpNet<CS_GetMenbers>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_GetMenbers_MsgType);
        }

        /// <summary>
        /// 设置收分设置
        /// </summary>
        public static void SetCollectSetting(string clubId,P_ClubSetting clubSetting)
        {
            CS_SetCollect sendMsg = new CS_SetCollect();
            sendMsg.UserValiadate = GoableData.userValiadateInfor;
            sendMsg.clubId = clubId;
            sendMsg.scoreLimit = clubSetting.scoreLimit;
            sendMsg.collectTaxesType = clubSetting.collectTaxesType;
            sendMsg.collectMode = clubSetting.collectMode;
            sendMsg.collectScale = clubSetting.collectScale;
            sendMsg.collectScore = clubSetting.collectScore;
            sendMsg.collectStart = clubSetting.collectStart;

            new TcpNet<CS_SetCollect>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_SetCollect_MsgType);
        }

        /// <summary>
        /// 设置亲友圈成员积分
        /// </summary>
        public static void SetMenberScore(string clubId,int menberId,int changeValue,byte credit)
        {
            CS_ChangeMenberScore changeScore = new CS_ChangeMenberScore();
            changeScore.UserValiadate = GoableData.userValiadateInfor;
            changeScore.clubId = clubId;
            changeScore.menberId = menberId;
            changeScore.scoreChange = changeValue;
            changeScore.controlType = changeValue > 0 ? (byte)1 : (byte)2;
			changeScore.credit = credit;

			new TcpNet<CS_ChangeMenberScore>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, changeScore, (int)IMClub.NetMessageType.CS_ChangeMenberScore_MsgType);
        }

        /// <summary>
        /// 添加游戏设置
        /// </summary>
        /// <param name="clubId"></param>
        /// <param name="gameType"></param>
        public static void AddGameSetting(string clubId,int gameType)
        {
            CS_AddGameSetting gameSetting = new CS_AddGameSetting();
            gameSetting.UserValiadate = GoableData.userValiadateInfor;
            gameSetting.clubId = clubId;
            gameSetting.gameType = (byte)gameType;

            new TcpNet<CS_AddGameSetting>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, gameSetting, (int)IMClub.NetMessageType.CS_AddGameSetting_MsgType);
        }

        /// <summary>
        /// 创建游戏房间
        /// </summary>
        /// <param name="clubId">亲友圈ID</param>
        /// <param name="gameType">游戏类型</param>
        public static void CreateGameRoom(string clubId,byte gameType)
        {
            CS_CreateGameRoom createGameRoom = new CS_CreateGameRoom();
            createGameRoom.UserValiadate = GoableData.userValiadateInfor;
            createGameRoom.clubId = clubId;
            createGameRoom.gameType = gameType;

            new TcpNet<CS_CreateGameRoom>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, createGameRoom, (int)IMClub.NetMessageType.CS_CreateGameRoom_MsgType);
        }

        /// <summary>
        /// 设置玩法设置
        /// </summary>
        public static void PlaySetting(string clubId, byte gameType,byte roomValue,List<int> paramars)
        {
            CS_SetGameSetting setGameSetting = new CS_SetGameSetting();
            setGameSetting.UserValiadate = GoableData.userValiadateInfor;
            setGameSetting.clubId = clubId;
            setGameSetting.gameType = gameType;
            setGameSetting.roomValue = roomValue;
            setGameSetting.paramars = paramars;

            new TcpNet<CS_SetGameSetting>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, setGameSetting, (int)IMClub.NetMessageType.CS_SetGameSetting_MsgType);
        }

        /// <summary>
        /// 设置修改亲友圈房卡
        /// </summary>
        public static void ChangeClubRecharge(string clubId,int changeRecharge)
        {
            CS_ChangeRechargeToClub sendMsg = new CS_ChangeRechargeToClub();
            sendMsg.UserValiadate = GoableData.userValiadateInfor;
            sendMsg.clubId = clubId;
            sendMsg.changeRecharge = changeRecharge;

            new TcpNet<CS_ChangeRechargeToClub>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_SetRechargeToClub_MsgType);
        }

        /// <summary>
        /// 设置亲友圈签名
        /// </summary>
        public static void SetClubSign(string clubId,string sign)
        {
            CS_SetClubSign sendMsg = new CS_SetClubSign();
            sendMsg.UserValiadate = GoableData.userValiadateInfor;
            sendMsg.clubId = clubId;
            sendMsg.sign = sign;
            new TcpNet<CS_SetClubSign>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_SetSign_MsgType);
        }

        /// <summary>
        /// 获取亲友圈战绩
        /// </summary>
        public static void GetClubGrade(string clubId,byte page)
        {
            CS_GetClubGrade sendMsg = new CS_GetClubGrade();
            sendMsg.clubId = clubId;
            sendMsg.page = page;
            sendMsg.UserValiadate = GoableData.userValiadateInfor;

            new TcpNet<CS_GetClubGrade>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_GetClubGrade_MsgType);
        }

		/// <summary>
		/// 退出亲友圈
		/// </summary>
		public static void LeaveClub(string clubId)
		{
			CS_LeaveClub sendMsg = new CS_LeaveClub();
			sendMsg.clubId = clubId;
			sendMsg.UserValiadate = GoableData.userValiadateInfor;

			new TcpNet<CS_LeaveClub>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_LeaveClub_MsgType);
		}

		/// <summary>
		/// 解散亲友圈
		/// </summary>
		public static void UnReleseClub(string clubId)
		{
			CS_UnReleseClub sendMsg = new CS_UnReleseClub();
			sendMsg.clubId = clubId;
			sendMsg.UserValiadate = GoableData.userValiadateInfor;

			new TcpNet<CS_UnReleseClub>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_UnReleseClub_MsgType);
		}

		/// <summary>
		/// 获取用户信息关于亲友圈
		/// </summary>
		/// <param name="clubId"></param>
		/// <param name="menberId"></param>
		public static void GetUserInfoAndClubInfo(string clubId,int menberId)
		{
			CS_GetUserInfo sendMsg = new CS_GetUserInfo();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.clubId = clubId;
			sendMsg.menberId = menberId;

			new TcpNet<CS_GetUserInfo>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_GetUserInfo_MsgType);
		}

		/// <summary>
		/// 获取用户信息关于亲友圈
		/// </summary>
		/// <param name="clubId"></param>
		/// <param name="menberId"></param>
		public static void RequestMenberJoinClub(string clubId, int menberId)
		{
			CS_RequestMenberJoinClub sendMsg = new CS_RequestMenberJoinClub();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.clubId = clubId;
			sendMsg.menberId = menberId;

			new TcpNet<CS_RequestMenberJoinClub>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)IMClub.NetMessageType.CS_RequestMenberJoinClub_MsgType);
		}
	}
}
