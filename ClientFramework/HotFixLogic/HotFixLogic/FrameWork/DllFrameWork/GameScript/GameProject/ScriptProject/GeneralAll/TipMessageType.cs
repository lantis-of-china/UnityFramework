using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 提示消息类型
/// </summary>
public enum TipMessageType
{
    /// <summary>
    /// 版本错误
    /// </summary>
    VersionError = -1,
    /// <summary>
    /// 服务器已满
    /// </summary>
    ServerOverflover = 0,
    /// <summary>
    /// 没有登录
    /// </summary>
    NoLogine = 1,
    /// <summary>
    /// 验证不通过
    /// </summary>
    ValidateNotPass = 2,

    /// <summary>
    /// 房间不存在
    /// </summary>
    NotRoom = 1000,
    /// <summary>
    /// 没有可获取房间信息
    /// </summary>
    NotRoomInfo = 1001,
    /// <summary>
    /// 没有在房间中不能执行操作
    /// </summary>
    NotInRoomCantControl = 1002,
    /// <summary>
    /// 没有房主权限
    /// </summary>
    NotRoomMasterControl = 1003,
    /// <summary>
    /// 不能发起投票
    /// </summary>
    CantVote = 1004,
    /// <summary>
    /// 钻石不足
    /// </summary>
    NotEngothRechange = 1005,
    /// <summary>
    /// 当前不能退出房间
    /// </summary>
    CantLeaveRoom = 1006,
    /// <summary>
    /// 房间已满
    /// </summary>
    RoomFull = 1007,
    /// <summary>
    /// 吃什么不能打什么
    /// </summary>
    CantPutChi = 1008,
    /// <summary>
    /// 当前不能解散房间 需要发起投票
    /// </summary>
    CantUnReleseRoom = 1009,
	/// <summary>
	/// 非法操作
	/// </summary>
	ValidateNotPassControl = 1010,
	/// <summary>
	/// 加入亲友圈进入房间
	/// </summary>
	JoinClubEntryRoom = 1011,
}