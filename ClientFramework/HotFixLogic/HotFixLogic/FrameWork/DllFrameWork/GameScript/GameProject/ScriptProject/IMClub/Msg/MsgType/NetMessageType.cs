using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClub
{
	/// <summary>
	/// 扯旋网络消息
	/// </summary>
	public enum NetMessageType
	{
		/// <summary>
		/// 创建组群
		/// </summary>
		CS_CreateGroup_MsgType = 20001,
		/// <summary>
		/// 创建组群
		/// </summary>
		SC_CreateGroup_MsgType = 20002,
		/// <summary>
		/// 查找组群
		/// </summary>
		CS_FindGroup_MsgType = 20003,
		/// <summary>
		/// 查找组群
		/// </summary>
		SC_FindGroup_MsgType = 20004,
		/// <summary>
		/// 添加组群
		/// </summary>
		CS_AddGroup_MsgType = 20005,
		/// <summary>
		/// 添加组群
		/// </summary>
		SC_AddGroup_MsgType = 20006,
		/// <summary>
		/// 获取加入的组群列表
		/// </summary>
		CS_GetGroups_MsgType = 20007,
		/// <summary>
		/// 获取加入的组群列表
		/// </summary>
		SC_GetGroups_MsgType = 20008,
		/// <summary>
		/// 获取成员详细信息
		/// </summary>
		CS_GetMenberInfo_MsgType = 20009,
		/// <summary>
		/// 获取成员详细信息
		/// </summary>
		SC_GetMenberInfo_MsgType = 20010,
		/// <summary>
		/// 获取组群成员列表
		/// </summary>
		CS_GetMenbers_MsgType = 20011,
		/// <summary>
		/// 获取组群成员列表
		/// </summary>
		SC_GetMenbers_MsgType = 20012,
		/// <summary>
		/// 从组群中删除成员
		/// </summary>
		CS_DeleteMenber_MsgType = 20013,
		/// <summary>
		/// 从组群中删除成员
		/// </summary>
		//SC_DeleteMenber_MsgType = 20014,
		/// <summary>
		/// 修改组群成员竞技分
		/// </summary>
		CS_ChangeMenberScore_MsgType = 20015,
		/// <summary>
		/// 设置收分
		/// </summary>
		CS_SetCollect_MsgType = 20016,
		/// <summary>
		/// 设置收分
		/// </summary>
		SC_SetCollect_MsgType = 20017,
		/// <summary>
		/// 设置添加游戏设置
		/// </summary>
		CS_AddGameSetting_MsgType = 20018,
		/// <summary>
		/// 设置游戏设置
		/// </summary>
		CS_SetGameSetting_MsgType = 20019,
		/// <summary>
		/// 将成员加入黑名单请求
		/// </summary>
		CS_AddBlackList_MsgType = 20020,
		/// <summary>
		/// 创建游戏房间
		/// </summary>
		CS_CreateGameRoom_MsgType = 20021,
		/// <summary>
		/// 设置房卡到亲友圈
		/// </summary>
		CS_SetRechargeToClub_MsgType = 20022,
		/// <summary>
		/// 设置亲友圈签名
		/// </summary>
		CS_SetSign_MsgType = 20023,
		/// <summary>
		/// 设置亲友圈签名
		/// </summary>
		SC_SetSign_MsgType = 20024,
		/// <summary>
		/// 返回创建房间
		/// </summary>
		SC_BackCreateRoom_MsgType = 20025,
		/// <summary>
		/// 获取亲友圈战绩
		/// </summary>
		CS_GetClubGrade_MsgType = 20026,
		/// <summary>
		/// 获取亲友圈战绩
		/// </summary>
		SC_GetClubGrade_MsgType = 20027,
		/// <summary>
		/// 解散亲友圈
		/// </summary>
		CS_UnReleseClub_MsgType = 20028,
		/// <summary>
		/// 解散亲友圈
		/// </summary>
		SC_UnReleseClub_MsgType = 20029,
		/// <summary>
		/// 退出亲友圈
		/// </summary>
		CS_LeaveClub_MsgType = 20030,
		/// <summary>
		/// 退出亲友圈
		/// </summary>
		SC_LeaveClub_MsgType = 20031,
		/// <summary>
		/// 同意加入亲友圈
		/// </summary>
		CS_AgrentMenberJoin_MsgType = 20032,
		/// <summary>
		/// 同意加入亲友圈
		/// </summary>
		SC_AgrentMenberJoin_MsgType = 20033,
		/// <summary>
		/// 请求亲友圈
		/// </summary>
		CS_RequestAddClub_MsgType = 20034,
		/// <summary>
		/// 请求亲友圈
		/// </summary>
		SC_RequestAddClub_MsgType = 20035,
		/// <summary>
		/// 请求进入
		/// </summary>
		SC_MenberRequestEntry_MsgType = 20036,
		/// <summary>
		/// 获取用户信息 是否在亲友圈中
		/// </summary>
		CS_GetUserInfo_MsgType = 20037,
		/// <summary>
		/// 获取用户信息 是否在亲友圈中
		/// </summary>
		SC_GetUserInfo_MsgType = 20038,
		/// <summary>
		/// 邀请进入亲友圈
		/// </summary>
		CS_RequestMenberJoinClub_MsgType = 20039,
		/// <summary>
		/// 邀请进入亲友圈
		/// </summary>
		SC_RequestMenberJoinClub_MsgType = 20040
	}
}
