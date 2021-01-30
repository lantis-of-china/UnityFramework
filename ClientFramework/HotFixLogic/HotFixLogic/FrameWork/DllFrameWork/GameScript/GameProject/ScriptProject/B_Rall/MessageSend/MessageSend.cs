using UnityEngine;
using System.Collections;

namespace Rall
{
	public class MessageSend
	{
		public static void NAT()
		{
			try
			{
				if (GoableData.userValiadateInfor != null)
				{
					Server.UDPAddress nat = new Server.UDPAddress();
					nat.value = GoableData.userValiadateInfor;
					UdpNetWork.Instance.SendMessageUdp<Server.UDPAddress>(StringConfigClass.loginIp, 10810, nat, NetMessageType.LoginWithUser_UDPNAT_Request);
				}
			}
			catch(System.Exception ex)
			{
				DebugLoger.LogError(ex.ToString());
			}
		}
		///////////////////////////////TCP/////////////////////////////////////////
		/// <summary>
		/// ////登陆服务器///////////////////////////////////////////////////////
		/// </summary>
		/// <param name="_account"></param>
		/// <param name="_passWord"></param>
		/// <param name="_isPhone"></param>
		public static void LoginServer(string _account, string _passWord,string _unionid,bool _isPhone)
		{
			DebugLoger.Log("_unionid " + _unionid);
			UINameSpace.UIWaitting.AddShowWaitting(((int)NetMessageType.LoginWithUser_Login_Back).ToString());
			Server.MessageUserLogin _messageLogin = new Server.MessageUserLogin();
			_messageLogin.AccountNumber = _account;
			_messageLogin.Unionid = _unionid;
			_messageLogin.PassWord = _passWord;
			_messageLogin.IsPhone = _isPhone;
			_messageLogin.version = StringConfigClass.versionTold;
			_messageLogin.longitude = GoableData.ServerIpaddress.longitude;
			_messageLogin.latitude = GoableData.ServerIpaddress.latitude;

			if (GoableData.WeiChatUserData.hasUserData)
			{
				_messageLogin.isWeiChat = 1;
				_messageLogin.nickName = GoableData.WeiChatUserData.nickname;
				_messageLogin.sex = GoableData.WeiChatUserData.sex;
				_messageLogin.headUrl = GoableData.WeiChatUserData.headimgurl;
			}
			else
			{
				_messageLogin.isWeiChat = 2;
				_messageLogin.nickName = "";
				_messageLogin.sex = 3;
				_messageLogin.headUrl = "";
			}

			DebugLoger.Log("登陆....");
			//if (System.DateTime.Now < System.DateTime.Parse("2017-03-10"))
			//{
			//new TcpNet<Server.MessageUserLogin>("127.0.0.1", StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, _messageLogin, NetMessageType.LoginWithUser_Login_Request);
			//new TcpNet<Server.MessageUserLogin>("roxy.ns.cloudflare.com", StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, _messageLogin, NetMessageType.LoginWithUser_Login_Request);
			new TcpNet<Server.MessageUserLogin>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, _messageLogin, (int)NetMessageType.LoginWithUser_Login_Request);

			//}
			//else
			//{
			//    DebugLoger.LogError("非法软件");
			//}
		}

		/// <summary>
		/// 检测房间是否能进
		/// </summary>
		/// <param name="_ip"></param>
		/// <param name="_port"></param>
		public static void CheckLock(string serverid)
		{
			Server.CS_CheckGameLock _messageEntry = new Server.CS_CheckGameLock();
			_messageEntry.UserValiadate = GoableData.userValiadateInfor;
			_messageEntry.id = serverid;

			new TcpNet<Server.CS_CheckGameLock>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, _messageEntry, (int)NetMessageType.LoginWithUser_CheckLock_Request);
		}

		/// <summary>
		/// 创建角色
		/// </summary>
		/// <param name="_name"></param>
		/// <param name="_sex"></param>
		public static void CreateRole(string _name, short _sex)
		{
			UINameSpace.UIWaitting.AddShowWaitting(((int)NetMessageType.GameWithUser_Create_Back).ToString());
			Server.CreateRole createRole = new Server.CreateRole();
			createRole._name = _name;
			createRole._sex = _sex;
			createRole.UserValiadate = GoableData.userValiadateInfor;
			new TcpNet<Server.CreateRole>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, createRole, (int)NetMessageType.GameWithUser_Create_Request);
			//UserNetWork.Instance.SendMessageUdp<Server.CreateRole>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, createRole, NetMessageType.GameWithUser_Create_Request);
		}

		/// <summary>
		/// 登录银行
		/// </summary>
		public static void LoginBank(string password)
		{
			Server.CS_ValiedBank sendMsg = new Server.CS_ValiedBank();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.password = password;
			new TcpNet<Server.CS_ValiedBank>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_LoginBank_CS);
		}

		/// <summary>
		/// 修改银行密码
		/// </summary>
		/// <param name="oldPassword"></param>
		/// <param name="newPassword"></param>
		public static void ChangeBankPassword(string oldPassword, string newPassword)
		{
			Server.CS_ChangedBankPassword sendMsg = new Server.CS_ChangedBankPassword();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.oldPassword = oldPassword;
			sendMsg.newPassword = newPassword;
			new TcpNet<Server.CS_ChangedBankPassword>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_ChangeBankPassword_CS);
		}

		/// <summary>
		/// 银行存取
		/// </summary>
		public static void TranslateBank(byte controlType, byte pointType, int count)
		{
			Server.CS_BankTranlate sendMsg = new Server.CS_BankTranlate();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.controlType = controlType;
			sendMsg.pointType = pointType;
			sendMsg.count = count;
			new TcpNet<Server.CS_BankTranlate>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_TranslateBankPoint_CS);
		}

		/// <summary>
		/// 商城支付
		/// </summary>
		/// <param name="agentCode"></param>
		/// <param name="payType"></param>
		/// <param name="id"></param>
		public static void StorePay(string agentCode, string payType, string id)
		{
			Server.CS_BuyStore bs = new Server.CS_BuyStore();
			bs.UserValiadate = GoableData.userValiadateInfor;
			bs.payType = payType;
			bs.agentCode = agentCode;
			bs.id = id;
			new TcpNet<Server.CS_BuyStore>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, bs, (int)NetMessageType.GameWithUser_BuyStore_CS);
		}

		/// <summary>
		/// 刷新信息
		/// </summary>
		/// <param name="agentCode"></param>
		/// <param name="payType"></param>
		/// <param name="id"></param>
		public static void RefenceInfo()
		{
			Server.CS_RefenceInfo sendMsg = new Server.CS_RefenceInfo();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;

			new TcpNet<Server.CS_RefenceInfo>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_RefenceInfo_CS);
		}


		/// <summary>
		/// 获取推送消息
		/// </summary>
		public static void GetMsgRecive()
		{
			Server.CS_GetRoleMsg sendMsg = new Server.CS_GetRoleMsg();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;

			new TcpNet<Server.CS_GetRoleMsg>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_GetReciveMsg_CS);
		}

		/// <summary>
		/// 获取推送消息
		/// </summary>
		public static void GetMsgSystem()
		{
			Server.CS_GetSystem sendMsg = new Server.CS_GetSystem();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;

			new TcpNet<Server.CS_GetSystem>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_GetSystemMsg_CS);
		}

		/// <summary>
		/// 实名
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="phone"></param>
		public static void RealName(string id,string name,string phone)
		{
			Server.CS_RealName sendMsg = new Server.CS_RealName();
			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.realId = id;
			sendMsg.realName = name;
			sendMsg.realPhone = phone;

			new TcpNet<Server.CS_RealName>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_RealName_CS);
		}

		/// <summary>
		/// 代理请求
		/// </summary>
		public static void AgentRequet(string phoneNumber)
		{
			Server.CS_AgentRequirt sendMsg = new Server.CS_AgentRequirt();

			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.phoneNumber = phoneNumber;
			new TcpNet<Server.CS_AgentRequirt>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_AgentRequet_CS);
		}

		/// <summary>
		/// 加入房间
		/// </summary>
		public static void QuickJoinGame(int roomId)
		{
			Server.CS_QuickJoinGame sendMsg = new Server.CS_QuickJoinGame();

			sendMsg.UserValiadate = GoableData.userValiadateInfor;
			sendMsg.roomId = roomId;
			new TcpNet<Server.CS_QuickJoinGame>(StringConfigClass.loginIp, StringConfigClass.loginPort, LSharpEntryGame.monoBehaviour, sendMsg, (int)NetMessageType.GameWithUser_QuickJoin_CS);
		}






















		/// <summary>
		/// 登陆游戏 ChatWithUser_Login_Request
		/// </summary>
		public static void LoginGame(string _ip, int _port)
		{
			if (UINameSpace.UIWaitting.codeList.Count == 0)
			{
				UINameSpace.UIWaitting.AddShowWaitting(((int)NetMessageType.ChatWithUser_Login_Back).ToString());
			}
			Server.UserEntry _messageEntry = new Server.UserEntry();
			_messageEntry.UserValiadate = GoableData.userValiadateInfor;


			if (GoableData.WeiChatUserData.hasUserData)
			{
				_messageEntry.isWeiChat = 1;
				_messageEntry.nickName = GoableData.WeiChatUserData.nickname;
				_messageEntry.sex = GoableData.WeiChatUserData.sex;
				_messageEntry.headUrl = GoableData.WeiChatUserData.headimgurl;
			}
			else
			{
				if (Application.isMobilePlatform)
				{
					//return;
				}

				_messageEntry.isWeiChat = 2;
				_messageEntry.nickName = "";
				_messageEntry.sex = 1;
				_messageEntry.headUrl = "";
			}

			_messageEntry.longitude = GoableData.ServerIpaddress.longitude;
			_messageEntry.latitude = GoableData.ServerIpaddress.latitude;

			UserNetWork.Instance.SendMessageTcp<Server.UserEntry>(_ip, _port, _messageEntry, NetMessageType.ChatWithUser_Login_Request);
		}


		/// <summary>
		/// 进入场景完成 告诉服务器
		/// </summary>
		public static void LoginOut()
		{
			Server.CS_LoginOut _messageEntry = new Server.CS_LoginOut();
			_messageEntry.UserValiadate = GoableData.userValiadateInfor;

			UserNetWork.Instance.SendMessageTcp<Server.CS_LoginOut>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, _messageEntry, NetMessageType.GameWithUser_LoginOut_CS);
		}


		/// <summary>
		/// 进入场景完成 告诉服务器
		/// </summary>
		public static void EntryGameSucess(string _ip, int _port)
		{
			Server.CS_UserEntrySucess _messageEntry = new Server.CS_UserEntrySucess();
			_messageEntry.UserValiadate = GoableData.userValiadateInfor;

			UserNetWork.Instance.SendMessageTcp<Server.CS_UserEntrySucess>(_ip, _port, _messageEntry, NetMessageType.GameWithUser_EntrySucess_CS);
		}

		/// <summary>
		/// 生成心跳
		/// </summary>
		/// <param name="_ip"></param>
		/// <param name="_port"></param>
		public static void SpawnHeart(string _ip, int _port)
		{
			Server.CS_Heart _messageEntry = new Server.CS_Heart();
			_messageEntry.UserValiadate = GoableData.userValiadateInfor;
			_messageEntry.ticks = System.DateTime.UtcNow.Ticks;

			UserNetWork.Instance.SendMessageTcp<Server.CS_Heart>(_ip, _port, _messageEntry, NetMessageType.GameWithUser_Heart_CS);
		}
		/// <summary>
		/// 创建麻将房间
		/// </summary>
		/// <param name="_name"></param>
		/// <param name="_sex"></param>
		public static void CreateRoom(Server.CreateRoom createRoom)
		{
			UINameSpace.UIWaitting.AddShowWaitting(Rall.ConfigProject.entryGameRoomBack);
			createRoom.UserValiadate = GoableData.userValiadateInfor;
			UserNetWork.Instance.SendMessageTcp<Server.CreateRoom>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, createRoom, NetMessageType.GameWithUser_CreateMajiangRoom_Request);
		}

		/// <summary>
		/// 加入麻将房间
		/// </summary>
		public static void JoinRoom(int roomId,string clubId)
		{
			UINameSpace.UIWaitting.AddShowWaitting(Rall.ConfigProject.entryGameRoomBack);
			Server.JoinRoom joinRoom = new Server.JoinRoom();
			joinRoom.roomId = roomId;
			joinRoom.clubId = clubId;
			joinRoom.UserValiadate = GoableData.userValiadateInfor;
			UserNetWork.Instance.SendMessageTcp<Server.JoinRoom>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, joinRoom, NetMessageType.GameWithUser_JoinMajiangRoom_Request);

		}
	}
}