using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//Request  请求
//Response 响应

public enum NetMessageType
{
    None = 00000,/*默认类型*/
    #region  登陆服务器 与 聊天服务器之间的数据消息
    LoginWithChat_Regist_Request = 10000,//游戏服务器注册
    LoginWithChat_Regist_Back = 10001,//游戏服务器注册返回
    LoginWithChat_User_Login_Validate_Request = 10002,//游戏服务器 玩家登陆验证
    LoginWithChat_User_Login_Validate_Back = 10003,//游戏服务器 玩家登陆验证返回
    LoginWithChat_User_Login_Out = 10004,
    #endregion  登陆服务器 与 游戏服务器之间的数据消息

    #region 登陆服务器 与 玩家之间的消息
    LoginWithUser_Regist_Request = 40003,/*注册请求*/
    LoginWithUser_Regist_Back = 40004,/*注册返回*/
    LoginWithUser_Login_Request = 40001,/*登陆请求*/
    LoginWithUser_Login_Back = 40002,/*登陆返回*/
    LoginWithUser_CheckLock_Request = 40005,
    LoginWithUser_CheckLock_Back = 40006,
	/// <summary>
	/// UDP推送
	/// </summary>
	LoginWithUser_UDPNotice_Back = 40007,
	/// <summary>
	/// UDP 打洞
	/// </summary>
	LoginWithUser_UDPNAT_Request = 40008,
	#endregion 登陆服务器 与 玩家之间的消息

	#region 游戏大厅
	/// <summary>
	/// 登陆聊天
	/// </summary>
	ChatWithUser_Login_Request = 50001,
    /// <summary>
    /// 登陆返回
    /// </summary>
    ChatWithUser_Login_Back = 50002,
    /// <summary>
    /// 创建角色
    /// </summary>
    GameWithUser_Create_Request = 50003,
    /// <summary>
    /// 创建角色返回
    /// </summary>
    GameWithUser_Create_Back = 50004,
    /// <summary>
    /// 创建麻将房间
    /// </summary>
    GameWithUser_CreateMajiangRoom_Request = 50005,
    /// <summary>
    /// 加入麻将房间
    /// </summary>
    GameWithUser_JoinMajiangRoom_Request = 50006,
    /// <summary>
    /// 网络切换操作  登陆操作
    /// </summary>
    GameWithUser_NetWaring_SC = 50012,
    /// <summary>
    /// 退出登陆
    /// </summary>
    GameWithUser_LoginOut_CS = 50013,
    /// <summary>
    /// 心跳
    /// </summary>
    GameWithUser_Heart_CS = 50014,
    /// <summary>
    /// 心跳返回
    /// </summary>
    GameWithUser_Heard_SC = 50015,
    /// <summary>
    /// 刷新房卡数量
    /// </summary>
    GameWithUser_Refencerecharge_SC = 50016,
    /// <summary>
    /// 提示消息
    /// </summary>
    GameWithUser_TipMessage_SC = 50017,
    /// <summary>
    /// 系统消息
    /// </summary>
    GameWithUser_SystemMsg_SC = 50018,
    /// <summary>
    /// 广播消息
    /// </summary>
    GameWithUser_ReceiveMsg_SC = 50019,
    /// <summary>
    /// 进入游戏成功
    /// </summary>
    GameWithUser_EntrySucess_CS = 50020,
    /// <summary>
    /// 推送所有的战绩数据
    /// </summary>
    GameWithUser_Grades_SC = 50021,
    /// <summary>
    /// 添加一条战绩数据
    /// </summary>
    GameWithUser_GradeAdd_SC = 50022,
    /// <summary>
    /// 登录银行
    /// </summary>
    GameWithUser_LoginBank_CS = 50023,
    /// <summary>
    /// 登录银行返回
    /// </summary>
    GameWithUser_LoginBank_SC = 50024,
    /// <summary>
    /// 修改银行密码
    /// </summary>
    GameWithUser_ChangeBankPassword_CS = 50025,
    /// <summary>
    /// 修改银行密码
    /// </summary>
    GameWithUser_ChangeBankPassword_SC = 50026,
    /// <summary>
    /// 存取
    /// </summary>
    GameWithUser_TranslateBankPoint_CS = 50027,
    /// <summary>
    /// 存取
    /// </summary>
    GameWithUser_TranslateBankPoint_SC = 50028,
    /// <summary>
    /// 输入代理码
    /// </summary>
    GameWithUser_AgentCode_CS = 50029,
    /// <summary>
    /// 代理进入
    /// </summary>
    GameWithUser_AgentCode_SC = 50030,
    /// <summary>
    /// 购买商城
    /// </summary>
    GameWithUser_BuyStore_CS = 50031,
    /// <summary>
    /// 购买商城
    /// </summary>
    GameWithUser_BuyStore_SC = 50032,
	/// <summary>
	/// 刷新信息
	/// </summary>
	GameWithUser_RefenceInfo_CS = 50033,
	/// <summary>
	/// 刷新信息
	/// </summary>
	GameWithUser_RefenceInfo_SC = 50034,
    /// <summary>
    /// 获取系统消息
    /// </summary>
    GameWithUser_GetSystemMsg_CS = 50035,
    /// <summary>
    /// 获取系统消息
    /// </summary>
    GameWithUser_GetSystemMsg_SC = 50036,
    /// <summary>
    /// 获取推送消息
    /// </summary>
    GameWithUser_GetReciveMsg_CS = 50037,
    /// <summary>
    /// 获取推送消息
    /// </summary>
    GameWithUser_GetReciveMsg_SC = 50038,
	/// <summary>
	/// 实名认证
	/// </summary>
	GameWithUser_RealName_CS = 50039,
	/// <summary>
	/// 实名认证
	/// </summary>
	GameWithUser_RealName_SC = 50040,
	/// <summary>
	/// 代理申请
	/// </summary>
	GameWithUser_AgentRequet_CS = 50041,
	/// <summary>
	/// 代理申请
	/// </summary>
	GameWithUser_AgentRequet_SC = 50042,
	/// <summary>
	/// 快速游戏
	/// </summary>
	GameWithUser_QuickJoin_CS = 50043,
	/// <summary>
	/// 快速游戏
	/// </summary>
	GameWithUser_QuickJoin_SC = 50044,
	#endregion 游戏大厅
}