using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.IO;
using IMClub;
using QiPaiDll;

namespace UINameSpace
{
    public class UIIMClubMain : UIObject
    {
        public static UIIMClubMain Instance;
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(UIDefineName.UIMain_IMClub, _className);

            return 1;
        }

        public UIIMClubMain()
        {
            assetsName = UIDefineName.UIMain_IMClub;
        }

        /// <summary>
        /// 返回大厅
        /// </summary>
        public Button btnBackrall;

        public override void OnDispose()
        {
			try
			{
				GoableData.ClearCallEvent.Remove(GoableClubDataInfo.ClearData);

				TablePanelItem.CloseTableWithTag("ClubSettingPanel");
				TablePanelItem.CloseTableWithTag("ClubFunPanel");
			}
			catch
			{ }
        }

        public override void OnAwake()
        {
            Instance = this;
			try
			{
				GoableData.ClearCallEvent.Add(GoableClubDataInfo.ClearData);
			}
			catch { }
            AssetsParkManager.LoadSoundParkWithName(IMClub.ConfigProject.projectFloderName, IMClub.ConfigProject.soundName);
            AssetsParkManager.LoadParkWithName(IMClub.ConfigProject.projectFloderName, IMClub.ConfigProject.iconsName);
            ClubListPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/clubListNode"));
            GroupFunPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/funListNode/groupPanel"));
            JoinShowClubPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/joinPanel"));
			AgreeRequestPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/agrentPanel"));

			CreateClubPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/createPanel"));
            ClubInfoPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/groupInfoPanel"));
			ClubMenberListPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/clubListNode/menberListPanel"));
			JoinClubPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/joinClubId"));
			RequestFriendPanel.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode/requestFriendPanel"));
			btnBackrall = GenericityTool.GetComponentByPath<Button>(objectInstance, "anchorCT/animationNode/clubListNode/btn_backRall");
            btnBackrall.onClick.AddListener(OnClickBackRall);
        }

        public override void OnEnable()
        {
			JoinClubPanel.SetActive(false);
			ClubMenberListPanel.SetActive(false);
			GroupFunPanel.SetActivity(false);
            JoinShowClubPanel.SetActive(false);
            CreateClubPanel.SetActive(false);
            ClubInfoPanel.SetActive(false);
			AgreeRequestPanel.SetActive(false);
			RequestFriendPanel.SetActive(false);

			if (!GoableClubDataInfo.isOpenClub)
            {
                GoableClubDataInfo.isOpenClub = true;
                ClubListPanel.GetGroups();
            }
            else
            {
                SetUpShowGroupFun();
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
			
			//ClubItem.clubItemState = null;
		}


        /// <summary>
        /// 开始
        /// </summary>
        public void SetUpShowGroupFun()
        {
            if (GoableClubDataInfo.GetGroupCount() > 0)
            {
                GroupFunPanel.SetActivity(true);

				DebugLoger.Log("openClubId -----------------> " + ClubItem.openClubId);
				if (!string.IsNullOrEmpty(ClubItem.openClubId))
				{
					ClubItem.clubItemState = null;
				}

				if (ClubItem.clubItemState == null)
				{
					if (string.IsNullOrEmpty(ClubItem.openClubId))
					{
						ClubItem.clubItemState = GoableClubDataInfo.groupInfoList[0].bindClubItem;
					}
					else
					{
						for (int i = 0; i < GoableClubDataInfo.groupInfoList.Count; ++i)
						{
							GroupWarp gw = GoableClubDataInfo.groupInfoList[i];
							if (gw.groupInfo.clubId == ClubItem.openClubId)
							{
								ClubItem.clubItemState = gw.bindClubItem;
								break;
							}
						}

						if (ClubItem.clubItemState == null)
						{
							ClubItem.clubItemState = GoableClubDataInfo.groupInfoList[0].bindClubItem;
						}
					}

					ClubItem.clubItemState.OnNotAudioSelect();
				}
			}
        }

        /// <summary>
        /// 更新
        /// </summary>
        public override void OnUpdate()
        {
            ClubScoreSettingPanel.Update();
        }

        /// <summary>
        /// 组件数量
        /// </summary>
        public static int compCount = 0;
        /// <summary>
        /// 组件
        /// </summary>
		public static List<GameObject> clubCompList = new List<GameObject>();

		public static void CloseAllLoginItem()
		{
			for (int i = 0; i < clubCompList.Count; ++i)
			{
				GameObject.DestroyImmediate(clubCompList[i], true);
			}
			clubCompList.Clear();
			compCount = 0;
		}

        /// <summary>
        /// 返回大厅
        /// </summary>
        public void OnClickBackRall()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			if (FrameWorkDrvice.UiManagerInstance.IsOpenUI(UIDefineName.UIMain_IMClub))
			{
				FrameWorkDrvice.UiManagerInstance.CloseUI(UIDefineName.UIMain_IMClub, eCloseType.Queue);
			}
        }
    }
}

namespace IMClub
{
	/// <summary>
	/// 亲友圈消息结构
	/// </summary>
	public class ClubMsgStruct
	{
		/// <summary>
		/// 会话ID
		/// </summary>
		public string conversationId;
		/// <summary>
		/// 发送者ID
		/// </summary>
		public string senderId;
		/// <summary>
		/// 消息
		/// </summary>
		public string txtMsg;
		/// <summary>
		/// 二进制消息
		/// </summary>
		public byte[] binaryMessage;
		/// <summary>
		/// 消息类型0-文本消息 1-图片消息 2-系统结算
		/// </summary>
		public byte msgType;
	}

	/// <summary>
	/// 亲友圈行列
	/// </summary>
	public class ClubItem
	{
		/// <summary>
		/// UDP推送的消息进来了
		/// </summary>
		/// <param name="msg"></param>
		public static void UDPNoticeMessageEntry(string msg)
		{
			LitJson.JsonData jobj = CSTools.JsonToData(msg);
			string clubId = jobj["SclubId"].ToString();
			string senderId = jobj["SsenderId"].ToString();

			GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(clubId);
			if (gw != null)
			{
				AVIMTextMessage iMessage = new AVIMTextMessage();
				iMessage.ConversationId = "";
				iMessage.FromClientId = senderId;
				iMessage.TextContent = msg;
				gw.bindClubItem.MessageEntry(iMessage);
			}
		}

		/// <summary>
		/// 静态亲友圈
		/// </summary>
		public static ClubItem clubItemRecord;
		public static void RecordSelectItem()
		{
			if (clubItemState != null)
			{
				clubItemRecord = clubItemState;
			}
			else
			{
				clubItemRecord = null;
			}
		}

		public static void ResetRecordSelectItem()
		{
			if (clubItemRecord != null)
			{
				clubItemState = clubItemRecord;
			}
		}

		public static string openClubId;
		public static void SetOpenClubItemId(string setClubId)
		{
			openClubId = setClubId;
		}

		/// <summary>
		/// 静态亲友圈
		/// </summary>
		public static ClubItem clubItemState;
		/// <summary>
		/// 节点
		/// </summary>
		public GameObject itemNode;
		/// <summary>
		/// 选中状态
		/// </summary>
		public GameObject selectState;
		/// <summary>
		/// 选中的按钮
		/// </summary>
		public Button btn_back;
		/// <summary>
		/// 图片
		/// </summary>
		public Image img_icon;
		/// <summary>
		/// 名字
		/// </summary>
		public Text txt_name;
		/// <summary>
		/// 名字
		/// </summary>
		public Text txt_count;
		/// <summary>
		/// 绑定的亲友圈信息
		/// </summary>
		public IMClub.GroupWarp bindGwInfo;
		/// <summary>
		/// 消息列表
		/// </summary>
		public List<ClubMsgStruct> clubMsgList = new List<ClubMsgStruct>();

		/// <summary>
		/// 获取节点
		/// </summary>
		/// <param name="node"></param>
		public void GetItem(GameObject node, string clubId)
		{
			itemNode = node;
			img_icon = GenericityTool.GetComponentByPath<Image>(node, "img_icon");
			txt_name = GenericityTool.GetComponentByPath<Text>(node, "txt_name");
			txt_count = GenericityTool.GetComponentByPath<Text>(node, "txt_count");
			btn_back = GenericityTool.GetComponentByPath<Button>(node, "img_back");
			selectState = GenericityTool.GetObjectByPath(node, "img_openState");
			btn_back.onClick.AddListener(OnClickSelect);

			if (clubItemState == null)
			{
				clubItemState = this;
				bindGwInfo.isGetMenbers = true;
				UINameSpace.UIWaitting.AddShowWaitting("IMClub.GetMenbers");
				MessageSend.GetMenbers(clubId);

				SetSelectState(true);
			}
			else
			{
				SetSelectState(false);

				clubItemState.SetSelectState(true);
			}
		}

		/// <summary>
		/// 选中状态
		/// </summary>
		/// <param name="select"></param>
		public void SetSelectState(bool select)
		{
			if (select)
			{
				ClubListPanel.UnSelectAllState();
			}
			selectState.SetActive(select);
		}

		/// <summary>
		/// 选中
		/// </summary>
		public void OnClickSelect()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			OnNotAudioSelect();
			
		}

		public void OnNotAudioSelect()
		{
			clubItemState = this;
			//显示成员列表
			if (!bindGwInfo.isGetMenbers)
			{
				UINameSpace.UIWaitting.AddShowWaitting("IMClub.GetMenbers");
				///获取成员
				IMClub.MessageSend.GetMenbers(bindGwInfo.groupInfo.clubId);
			}
			else
			{
				GroupFunPanel.UpShow();
			}

			SetSelectState(true);
		}

		/// <summary>
		/// 设置头像节点显示
		/// </summary>
		public void SetItemInfo()
		{
			bindGwInfo.bindClubItem = this;
		}

		
		/// <summary>
		/// 刷新显示亲友圈信息
		/// </summary>
		public void UpShowItemInfo()
		{
			txt_name.text = bindGwInfo.groupInfo.groupName;

			if (bindGwInfo.groupInfo.groupMasterId == int.Parse(GoableData.userValiadateInfor.DatingNumber))
			{
				txt_count.text = "我  的                      ";
			}
			else
			{
				txt_count.text = "加入的                      ";
			}

			txt_count.text += bindGwInfo.groupInfo.groupMenberCount.ToString() + "/500";

			if (!string.IsNullOrEmpty(bindGwInfo.groupInfo.iconUrl))
			{
				SetImageForHttpbytes.SetImageFromUrl(img_icon, bindGwInfo.groupInfo.iconUrl);
			}
			else
			{
				AssetsParkManager.SetUguiImageThingIcon(IMClub.ConfigProject.iconsName, img_icon, "clubIcon");
			}
		}

		/// <summary>
		/// 加入聊天
		/// </summary>
		public void JoinChat()
		{

		}

		/// <summary>
		/// 离开聊天
		/// </summary>
		public void LeaveChat()
		{

		}

		/// <summary>
		/// 发送自己的消息
		/// </summary>
		public void SelfMessage(IAVIMMessage msg)
		{
			ClubMsgStruct msgStruct = new ClubMsgStruct();
			msgStruct.conversationId = msg.ConversationId;
			msgStruct.senderId = GoableData.userValiadateInfor.DatingNumber;

			if (msg is AVIMTextMessage)
			{
				var textMessage = (AVIMTextMessage)msg;
				msgStruct.msgType = (byte)0;

				string msgContent = textMessage.TextContent;
				LitJson.JsonData jsonData = CSTools.JsonToData(msgContent);
				if (jsonData["msgType"].ToString() == "user")
				{
					msgStruct.txtMsg = jsonData["content"].ToString();
				}
				else if (jsonData["msgType"].ToString() == "system")
				{
					if (jsonData["cmd"].ToString() == "joinUser")
					{
						//用户加入
						string strBuf = jsonData["content"].ToString();
						P_Menber joinMenber = new P_Menber();
						joinMenber.DeserializerJson(strBuf);
						//JsonUtility.FromJsonOverwrite(strBuf, joinMenber);
					}
					return;
				}
			}
			else if (msg is AVIMBinaryMessage)
			{
				var binaryMessage = (AVIMBinaryMessage)msg;
				msgStruct.msgType = (byte)1;
				msgStruct.binaryMessage = binaryMessage.BinaryData;
			}

			AddMsg(msgStruct);
		}



		/// <summary>
		/// 消息进入
		/// </summary>
		public void MessageEntry(IAVIMMessage msg)
		{
			try
			{
				ClubMsgStruct msgStruct = new ClubMsgStruct();
				msgStruct.conversationId = msg.ConversationId;
				msgStruct.senderId = msg.FromClientId;
				if (msg is AVIMTextMessage)
				{
					var textMessage = (AVIMTextMessage)msg;
					msgStruct.msgType = (byte)0;

					string msgContent = textMessage.TextContent;
					DebugLoger.Log("msgContent " + msgContent);
					LitJson.JsonData jsonData = CSTools.JsonToData(msgContent);
					if (jsonData["msgType"].ToString() == "user")
					{
						msgStruct.txtMsg = jsonData["content"].ToString();
					}
					else if (jsonData["msgType"].ToString() == "system")
					{
						if (jsonData["cmd"].ToString() == "menberChangeOnLineState")
						{
							#region 成员登陆状态变化
							string strBuf = jsonData["content"].ToString();
							SC_MenberChangeOnLineState entryMsg = new SC_MenberChangeOnLineState();
							entryMsg.DeserializerJson(strBuf);

							GroupWarp gw = GoableClubDataInfo.GetGroup(entryMsg.clubId);
							if (gw != null && gw.menberList != null && gw.menberList.Count > 0)
							{
								if (gw.menberList.ContainsKey(entryMsg.menberId))
								{
									gw.menberList[entryMsg.menberId].insertTag = entryMsg.state;
								}
							}

							if (gw != null && gw.bindClubItem != null && gw.bindClubItem == IMClub.ClubItem.clubItemState)
							{
								ClubMenberListPanel.UpScore(entryMsg.menberId);
							}
							#endregion 成员登陆状态变化
						}
						else if (jsonData["cmd"].ToString() == "unReleseClub")
						{
							//解散亲友圈
							#region 解散亲友圈
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_UnReleseClub_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_UnReleseClub entryMsg = new SC_UnReleseClub();
							entryMsg.DeserializerJson(strBuf);

							if (entryMsg.result == 0)
							{
								return;
							}

							GroupWarp gw = GoableClubDataInfo.GetGroup(entryMsg.clubId);
							if (gw != null && gw.groupInfo != null)
							{
								GoableClubDataInfo.RemoveGroup(entryMsg.clubId);
								IMClub.ClubListPanel.CloseClubItem(entryMsg.clubId);
								IMClub.ClubListPanel.UpdateClubList();
								UINameSpace.UITipMessage.PlayMessage(string.Format("亲友圈[{0}]解散!", gw.groupInfo.groupName));
							}
							#endregion 解散亲友圈
						}
						else if (jsonData["cmd"].ToString() == "joinUser")
						{
							//用户加入
							#region 用户加入
							string jsonStr = jsonData["content"].ToString();
							SC_MenberJoin joinMenber = new SC_MenberJoin();
							joinMenber.DeserializerJson(jsonStr);
							//JsonUtility.FromJsonOverwrite(jsonStr, joinMenber);

							IMClub.GoableClubDataInfo.AddMenberToGroup(joinMenber.menberInfo, joinMenber.groupId);

							GroupWarp gw = IMClub.GoableClubDataInfo.GetGroupWithGroupId(joinMenber.groupId);

							if (gw != null && gw.bindClubItem != null && gw.bindClubItem == IMClub.ClubItem.clubItemState)
							{
								ClubMenberListPanel.ResetShow();
							}

							if (gw != null && gw.bindClubItem != null)
							{
								if (joinMenber.menberInfo.menberId.ToString() != GoableData.userValiadateInfor.DatingNumber)
								{
									gw.groupInfo.groupMenberCount++;
								}
								gw.bindClubItem.UpShowItemInfo();
							}

							GroupFunPanel.UpInfo();
							#endregion 用户加入
						}
						else if (jsonData["cmd"].ToString() == "deleteUser")
						{
							//移除用户
							#region 移除用户
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_DeleteMenber_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_DeleteMenber deleteMenber = new SC_DeleteMenber();
							deleteMenber.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, deleteMenber);

							if (deleteMenber.result == 0)
							{
								return;
							}
							P_Menber menber = IMClub.GoableClubDataInfo.RemoveMenberFromGroup(deleteMenber.menberId, deleteMenber.clubId);
							if (menber != null)
							{
									if (menber.menberId.ToString() == GoableData.userValiadateInfor.DatingNumber)
									{
										//自己退出
										GroupWarp rmgw = GoableClubDataInfo.GetGroup(deleteMenber.clubId);
										if (rmgw != null && rmgw.groupInfo != null)
										{
											GoableClubDataInfo.RemoveGroup(deleteMenber.clubId);
											IMClub.ClubListPanel.CloseClubItem(deleteMenber.clubId);
											IMClub.ClubListPanel.UpdateClubList();
											UINameSpace.UITipMessage.PlayMessage(string.Format("被管理员移出亲友圈[{0}]", rmgw.groupInfo.groupName));
										}

									if (IMClub.ClubItem.clubItemState != null && IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == deleteMenber.clubId)
									{
										ClubMenberListPanel.CloseUI();
									}
								}

								if (IMClub.ClubItem.clubItemState == null)
								{
									return;
								}

								if (IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == deleteMenber.clubId)
								{
									if (ClubMenberListPanel_Select.Instance != null && ClubMenberListPanel_Select.Instance.IsActive())
									{
										ClubMenberListPanel_Select.Instance.Remove(deleteMenber.menberId);									
										
									}

									ClubMenberListPanel.Remove(deleteMenber.menberId);
									//if (ClubBlackListPanel_Select.Instance != null && ClubBlackListPanel_Select.Instance.IsActive())
									//{
									//    ClubBlackListPanel_Select.Instance.Remove(deleteMenber.menberId);
									//}
								}
							}

							GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(deleteMenber.clubId);
							if (gw != null && gw.bindClubItem != null)
							{
								gw.groupInfo.groupMenberCount--;
								gw.bindClubItem.UpShowItemInfo();
							}

							GroupFunPanel.UpInfo();
							#endregion 移除用户
						}
						else if (jsonData["cmd"].ToString() == "leaveClub")
						{
							//移除用户
							#region 移除用户
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_LeaveClub_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_LeaveClub deleteMenber = new SC_LeaveClub();
							deleteMenber.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, deleteMenber);

							if (deleteMenber.result == 0)
							{
								return;
							}
							P_Menber menber = IMClub.GoableClubDataInfo.RemoveMenberFromGroup(deleteMenber.menberId, deleteMenber.clubId);
							if (menber != null)
							{
								if (IMClub.ClubItem.clubItemState == null)
								{
									return;
								}

								if (IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == deleteMenber.clubId)
								{
									if (ClubMenberListPanel_Select.Instance != null && ClubMenberListPanel_Select.Instance.IsActive())
									{
										ClubMenberListPanel_Select.Instance.Remove(deleteMenber.menberId);
									}

									//if (ClubBlackListPanel_Select.Instance != null && ClubBlackListPanel_Select.Instance.IsActive())
									//{
									//    ClubBlackListPanel_Select.Instance.Remove(deleteMenber.menberId);
									//}

									ClubMenberListPanel.Remove(deleteMenber.menberId);
								}

								if (menber.menberId.ToString() == GoableData.userValiadateInfor.DatingNumber)
								{
									//自己退出
									GroupWarp gw = GoableClubDataInfo.GetGroup(deleteMenber.clubId);
									if (gw != null && gw.groupInfo != null)
									{
										GoableClubDataInfo.RemoveGroup(deleteMenber.clubId);
										IMClub.ClubListPanel.CloseClubItem(deleteMenber.clubId);
										IMClub.ClubListPanel.UpdateClubList();
										UINameSpace.UITipMessage.PlayMessage("退出亲友圈成功!");
									}
								}
							}

							#endregion 移除用户
						}
						else if (jsonData["cmd"].ToString() == "createRoom")
						{
							//创建游戏房间
							#region 创建房间
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_CreateGameRoom_MsgType");

							string jsonStr = jsonData["content"].ToString();
							IMClub.SC_CreateGameRoom createGameRoom = new IMClub.SC_CreateGameRoom();
							createGameRoom.DeserializerJson(jsonStr);
							DebugLoger.Log("Room ID " + createGameRoom.roomId + " times " + createGameRoom.toldTimes);

							P_RoomInfo roomInfo = new P_RoomInfo();
							roomInfo.serverId = createGameRoom.serverId;
							roomInfo.clubId = createGameRoom.clubId;
							roomInfo.gameType = createGameRoom.gameType;
							roomInfo.roomId = createGameRoom.roomId;
							roomInfo.curCount = createGameRoom.curPlayerCount;
							roomInfo.maxCount = createGameRoom.maxPlayerCount;
							roomInfo.curTimes = 0;
							roomInfo.toldTimes = createGameRoom.toldTimes;
							roomInfo.state = createGameRoom.state;
							roomInfo.costPay = createGameRoom.useRecharge;
							roomInfo.menberList = new List<int>();
							RoomManager.AddRoom(roomInfo);

							GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(createGameRoom.clubId);
							if (gw != null)
							{
								gw.groupInfo.rechargeCount -= createGameRoom.useRecharge;
							}
							if (ClubItem.clubItemState != null && ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == createGameRoom.clubId)
							{
								if (ClubStatisticsPanel_Select.Instance != null && ClubStatisticsPanel_Select.Instance.IsActive())
								{
									ClubStatisticsPanel_Select.Instance.ShowInfo(gw.groupInfo);
									//ClubStatisticsPanel_Select.Instance.UpShow();
								}
							}

							DebugLoger.Log("-----------------------------------1");
							if (ClubRoomPanel_Select.Instance != null && ClubRoomPanel_Select.Instance.IsActive())
							{
								DebugLoger.Log("-----------------------------------2");

								ClubRoomPanel_Select.Instance.UpAllShowInfoExtern();


							}
							#endregion 创建房间
						}
						else if (jsonData["cmd"].ToString() == "joinRoom")
						{
							//加入游戏房间
							#region 加入游戏房间
							string strBuf = jsonData["content"].ToString();
							SC_JoinGameRoom entryMsg = new SC_JoinGameRoom();
							entryMsg.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, entryMsg);

							P_RoomInfo roomInfo = RoomManager.GetRoom(entryMsg.clubId, entryMsg.roomId);
							DebugLoger.Log("roomInfo 1 + " + roomInfo.curCount);
							if (roomInfo != null)
							{
								roomInfo.curCount += 1;

								if (!roomInfo.menberList.Contains(entryMsg.menberId))
								{
									roomInfo.menberList.Add(entryMsg.menberId);
								}
							}
							DebugLoger.Log("roomInfo 2 + " + roomInfo.curCount);

							if (ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == entryMsg.clubId)
							{
								//当前亲友圈
								if (ClubRoomPanel_Select.Instance != null && ClubRoomPanel_Select.Instance.IsActive())
								{
									ClubRoomPanel_Select.Instance.UpRoomInfo(entryMsg.roomId);
								}

								ClubMenberListPanel.ResetShow();
							}
							#endregion 加入游戏房间
						}
						else if (jsonData["cmd"].ToString() == "leaveRoom")
						{
							//离开游戏房间
							#region 离开游戏房间
							string strBuf = jsonData["content"].ToString();
							SC_LeaveGameRoom entryMsg = new SC_LeaveGameRoom();
							entryMsg.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, entryMsg);

							P_RoomInfo roomInfo = RoomManager.GetRoom(entryMsg.clubId, entryMsg.roomId);
							if (roomInfo != null)
							{
								roomInfo.curCount -= 1;

								if (roomInfo.menberList.Contains(entryMsg.menberId))
								{
									roomInfo.menberList.Remove(entryMsg.menberId);
								}
							}

							if (ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == entryMsg.clubId)
							{
								//当前亲友圈
								if (ClubRoomPanel_Select.Instance !=null && ClubRoomPanel_Select.Instance.IsActive())
								{
									ClubRoomPanel_Select.Instance.UpRoomInfo(entryMsg.roomId);
								}

								ClubMenberListPanel.ResetShow();
							}
							#endregion 离开游戏房间
						}
						else if (jsonData["cmd"].ToString() == "unReleseRoom")
						{
							//解散游戏房间
							#region 解散游戏房间
							string strBuf = jsonData["content"].ToString();
							SC_UnReleseGameRoom entryMsg = new SC_UnReleseGameRoom();
							entryMsg.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, entryMsg);

							RoomManager.RemoveRoom(entryMsg.clubId, entryMsg.roomId);
							if (ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == entryMsg.clubId)
							{
								//当前亲友圈
								if (IMClub.ClubRoomPanel_Select.Instance != null && ClubRoomPanel_Select.Instance.IsActive())
								{
									ClubRoomPanel_Select.Instance.UnReleseRoom(entryMsg.roomId);
								}
							}

							GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(entryMsg.clubId);
							if (gw != null)
							{
								gw.groupInfo.rechargeCount = entryMsg.curRecharge;
								gw.groupInfo.toldUseRechargeCount = entryMsg.toldUseRecharge;
								gw.groupInfo.toDayUseRechargeCount = entryMsg.todayUseRecharge;
							}

							if (ClubItem.clubItemState != null && ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == entryMsg.clubId)
							{
								if (ClubStatisticsPanel_Select.Instance != null && ClubStatisticsPanel_Select.Instance.IsActive())
								{
									ClubStatisticsPanel_Select.Instance.ShowInfo(gw.groupInfo);
									//ClubStatisticsPanel_Select.Instance.UpShow();
								}

								ClubMenberListPanel.ResetShow();
							}
							#endregion 解散游戏房间
						}
						else if (jsonData["cmd"].ToString() == "stateChangeRoom")
						{
							//解散游戏房间
							#region 切换房间状态
							string strBuf = jsonData["content"].ToString();
							SC_StateChangeGameRoom entryMsg = new SC_StateChangeGameRoom();
							entryMsg.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, entryMsg);

							P_RoomInfo roomInfo = RoomManager.GetRoom(entryMsg.clubId, entryMsg.roomId);
							if (roomInfo != null)
							{
								roomInfo.curTimes = entryMsg.times;
								roomInfo.state = entryMsg.state;
							}

							if (ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == entryMsg.clubId)
							{
								//当前亲友圈
								if (ClubRoomPanel_Select.Instance != null && ClubRoomPanel_Select.Instance.IsActive())
								{
									ClubRoomPanel_Select.Instance.UpRoomInfo(entryMsg.roomId);
								}
							}
							#endregion 切换房间状态
						}
						else if (jsonData["cmd"].ToString() == "changeMenberScore")
						{
							//修改竞技分
							#region 修改竞技分
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_ChangeMenberScore_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_ChangeMenberScore changeMenberScore = new SC_ChangeMenberScore();
							changeMenberScore.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, changeMenberScore);

							P_Menber menber = IMClub.GoableClubDataInfo.GetMenberFromGroup(changeMenberScore.menberId, changeMenberScore.clubId);
							if (menber != null)
							{
								menber.Score = changeMenberScore.score;
								menber.credit = changeMenberScore.credit;

								if (IMClub.ClubItem.clubItemState == null)
								{
									return;
								}

								if (IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == changeMenberScore.clubId)
								{
									if (ClubMenberListPanel_Select.Instance != null && ClubMenberListPanel_Select.Instance.IsActive())
									{
										ClubMenberListPanel_Select.Instance.UpScore(changeMenberScore.menberId);
									}

									if (ClubBlackListPanel_Select.Instance != null && ClubBlackListPanel_Select.Instance.IsActive())
									{
										ClubBlackListPanel_Select.Instance.UpScore(changeMenberScore.menberId);
									}
								}
							}
							#endregion 修改竞技分
						}
						else if (jsonData["cmd"].ToString() == "addGameSetting")
						{
							//添加游戏设置
							#region 添加游戏设置
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_AddGameSetting_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_AddGameSetting addGameSetting = new SC_AddGameSetting();
							addGameSetting.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, addGameSetting);



							if (addGameSetting.result == 0)
							{
								UINameSpace.UITipMessage.PlayMessage("游戏规则添加失败!");
								return;
							}
							else
							{
								GroupWarp groupWarp = IMClub.GoableClubDataInfo.GetGroup(addGameSetting.clubId);
								if (groupWarp != null)
								{
									groupWarp.groupInfo.clubSetting.gamesSetting.Add(addGameSetting.gameSetting);

									if (AddGameRulePanel.IsActive())
									{
										AddGameRulePanel.AddRule(addGameSetting.gameSetting.gameType);
									}
								}
								else
								{
									UINameSpace.UITipMessage.PlayMessage("没有获取到对应亲友圈,游戏规则添加失败!");
								}
							}
							#endregion 添加游戏设置
						}
						else if (jsonData["cmd"].ToString() == "setGameSetting")
						{
							//设置游戏设置
							#region 设置游戏
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_SetGameSetting_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_SetGameSetting addGameSetting = new SC_SetGameSetting();
							addGameSetting.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, addGameSetting);

							GroupWarp groupWarp = IMClub.GoableClubDataInfo.GetGroup(addGameSetting.clubId);
							if (groupWarp != null)
							{
								for (int i = 0; i < groupWarp.groupInfo.clubSetting.gamesSetting.Count; ++i)
								{
									if (groupWarp.groupInfo.clubSetting.gamesSetting[i].gameType == addGameSetting.gameSetting.gameType)
									{
										groupWarp.groupInfo.clubSetting.gamesSetting[i].roomValue = addGameSetting.gameSetting.roomValue;
										for (int b = 0; b < addGameSetting.gameSetting.pamarasSetting.Count; ++b)
										{
											if (b >= groupWarp.groupInfo.clubSetting.gamesSetting[i].pamarasSetting.Count)
											{
												groupWarp.groupInfo.clubSetting.gamesSetting[i].pamarasSetting.Add(addGameSetting.gameSetting.pamarasSetting[b]);
											}
											else
											{
												groupWarp.groupInfo.clubSetting.gamesSetting[i].pamarasSetting[b] = addGameSetting.gameSetting.pamarasSetting[b];
											}
										}

										DebugLoger.Log("修改成功哦");
										break;
									}
								}
							}
							else
							{
								DebugLoger.Log("没有找到组群");
							}
							#endregion 设置游戏
						}
						else if (jsonData["cmd"].ToString() == "changeRechargeClub")
						{
							//修改房卡数量
							#region 修改房卡数量
							UINameSpace.UIWaitting.RemoveShowWaitting("MessageSend.ChangeClubRecharge");
							string strBuf = jsonData["content"].ToString();
							SC_ChangeRechargeToClub changeGameRecharge = new SC_ChangeRechargeToClub();
							changeGameRecharge.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, changeGameRecharge);

							if (changeGameRecharge.masterId == GoableData.userValiadateInfor.DatingNumber)
							{
								GoableData.userValiadateInforWarp.RechargeCount -= changeGameRecharge.changeRecharge;
								if (UINameSpace.UIRall.GetInstance() != null && UINameSpace.UIRall.GetInstance().objectInstance != null)
								{
									UINameSpace.UIRall.GetInstance().RefenceRoomKa();
								}
							}

							GroupWarp groupWarp = IMClub.GoableClubDataInfo.GetGroup(changeGameRecharge.clubId);
							if (groupWarp != null)
							{
								groupWarp.groupInfo.rechargeCount = changeGameRecharge.clubRecharge;
								if (ClubStatisticsPanel_Select.Instance != null && ClubStatisticsPanel_Select.Instance.IsActive())
								{
									ClubStatisticsPanel_Select.Instance.ShowInfo(groupWarp.groupInfo);
									//ClubStatisticsPanel_Select.Instance.UpShow();
								}
							}
							else
							{
								DebugLoger.Log("没有找到组群");
							}
							#endregion 修改房卡数量
						}
						else if (jsonData["cmd"].ToString() == "changeRechargeClubEncoding")
						{
							//修改房卡数量 公众号设置
							#region 修改房卡数量
							UINameSpace.UIWaitting.RemoveShowWaitting("MessageSend.ChangeClubRecharge");
							string strBuf = jsonData["content"].ToString();
							SC_ChangeRechargeToClub changeGameRecharge = new SC_ChangeRechargeToClub();
							changeGameRecharge.DeserializerJson(strBuf);
							//JsonUtility.FromJsonOverwrite(strBuf, changeGameRecharge);

							GroupWarp groupWarp = IMClub.GoableClubDataInfo.GetGroup(changeGameRecharge.clubId);
							if (groupWarp != null)
							{
								groupWarp.groupInfo.rechargeCount = changeGameRecharge.clubRecharge;
								if (ClubStatisticsPanel_Select.Instance != null && ClubStatisticsPanel_Select.Instance.IsActive())
								{
									ClubStatisticsPanel_Select.Instance.ShowInfo(groupWarp.groupInfo);
									//ClubStatisticsPanel_Select.Instance.UpShow();
								}
							}
							else
							{
								DebugLoger.Log("没有找到组群");
							}
							#endregion 修改房卡数量
						}
						else if (jsonData["cmd"].ToString() == "addBlackList")
						{
							//添加黑名单
							#region 移除用户
							UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_AddBlackList_MsgType");
							string strBuf = jsonData["content"].ToString();
							SC_AddBlackList deleteMenber = new SC_AddBlackList();
							deleteMenber.DeserializerJson(strBuf);

							if (deleteMenber.result == 0)
							{
								return;
							}
							P_Menber menber = IMClub.GoableClubDataInfo.GetMenberFromGroup(int.Parse(deleteMenber.menberId), deleteMenber.clubId);
							if (menber != null)
							{
								menber.black = deleteMenber.type;
								if (IMClub.ClubItem.clubItemState == null)
								{
									return;
								}

								if (IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId == deleteMenber.clubId)
								{
									if (ClubMenberListPanel_Select.Instance != null && ClubMenberListPanel_Select.Instance.IsActive())
									{
										ClubMenberListPanel_Select.Instance.UpScore(int.Parse(deleteMenber.menberId));
									}

									//if (ClubBlackListPanel_Select.Instance != null && ClubBlackListPanel_Select.Instance.IsActive())
									//{
									//    ClubBlackListPanel_Select.Instance.Remove(deleteMenber.menberId);
									//}
								}
							}

							#endregion 移除用户
						}
						else if (jsonData["cmd"].ToString() == "clubScoreBack")
						{
							//游戏结算
							#region 游戏结算
							msgStruct.msgType = (byte)2;

							string strBuf = jsonData["content"].ToString();
							msgStruct.txtMsg = strBuf;
							AddMsg(msgStruct);

							Server.SC_ClubScoreBack clubScoreData = new Server.SC_ClubScoreBack();
							clubScoreData.DeserializerJson(strBuf);

							for (int i = 0; i < clubScoreData.clubChangeItem.Count; ++i)
							{
								Server.P_ClubScoreBackItem clubScoreItemBack = clubScoreData.clubChangeItem[i];
								P_Menber menberInfo = IMClub.GoableClubDataInfo.GetMenberFromGroup(clubScoreItemBack.menberId, clubScoreData.clubId);
								if (menberInfo != null)
								{
									menberInfo.Score += clubScoreItemBack.backChangeScore - clubScoreItemBack.deductionScore;
								}
							}	

							GroupFunPanel.UpShow();
							#endregion 游戏结算
						}
						return;
					}
				}
				else if (msg is AVIMBinaryMessage)
				{
					var binaryMessage = (AVIMBinaryMessage)msg;
					msgStruct.msgType = (byte)1;
					msgStruct.binaryMessage = binaryMessage.BinaryData;
				}

				AddMsg(msgStruct);
			}
			catch (Exception e)
			{
				DebugLoger.LogError(e.ToString());
			}
		}

		/// <summary>
		/// 添加消息
		/// </summary>
		public void AddMsg(ClubMsgStruct msgData)
		{
			clubMsgList.Add(msgData);

			if (clubItemState == this)
			{
				//需要更新面板
				if (ClubChatPanel_Select.clubChatPanel != null)
				{
					ClubChatPanel_Select.clubChatPanel.AddMsgRefence();
				}
			}
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Destroy()
		{
			GameObject.Destroy(itemNode);
		}
	}

	/// <summary>
	/// 亲友圈列表类
	/// </summary>
	public class ClubListPanel
	{
		/// <summary>
		/// 亲友圈节点
		/// </summary>
		public static GameObject rootNode;
		/// <summary>
		/// 创建亲友圈
		/// </summary>
		public static Button btnCreateGroup;
		/// <summary>
		/// 加入亲友圈
		/// </summary>
		public static Button btnJoinGroup;
		/// <summary>
		/// 返回大厅
		/// </summary>
		public static Button btnBackrall;
		/// <summary>
		///  查找组
		/// </summary>
		public static Button btnFindGroup;
		/// <summary>
		/// 请求查看
		/// </summary>
		public static Button btnRequest;
		/// <summary>
		/// 请求节点
		/// </summary>
		public static GameObject requestItem;
		/// <summary>
		/// 数量
		/// </summary>
		public static Text txt_requestCount;
		/// <summary>
		/// 查找组输入信息
		/// </summary>
		public static InputField inputGroupId;
		/// <summary>
		/// 亲友圈列表节点
		/// </summary>
		public static GameObject groupListNode;
		/// <summary>
		/// 亲友圈节点
		/// </summary>
		public static GameObject itemScoreNode;
		/// <summary>
		/// 亲友圈列表<会话ID,亲友圈列表>
		/// </summary>
		public static Dictionary<string, ClubItem> clubItemList = new Dictionary<string, ClubItem>();
		/// <summary>
		/// 起点Y
		/// </summary>
		public static float distanceStartPosY = -50.0f;
		/// <summary>
		/// 每个的偏移
		/// </summary>
		public static float distanceOffset = 170.0f;

		/// <summary>
		/// 关闭所有的列表
		/// </summary>
		public static void CloseClubList()
		{
			foreach (var kv in clubItemList)
			{
				kv.Value.Destroy();
			}

			clubItemList.Clear();

			ClubItem.clubItemState = null;
		}

		/// <summary>
		/// 关闭单个亲友圈节点
		/// </summary>
		/// <param name="clubId"></param>
		public static void CloseClubItem(string clubId)
		{
			foreach (var kv in clubItemList)
			{
				if (kv.Value.bindGwInfo.groupInfo.clubId == clubId)
				{
					clubItemList[kv.Key].Destroy();
					clubItemList.Remove(kv.Key);

					if (ClubItem.clubItemState == kv.Value)
					{
						ClubItem.clubItemState = null;
						//如果是选中
						if (clubItemList.Count > 0)
						{
							clubItemList.Values.ToArray()[0].OnNotAudioSelect();
						}
						else
						{
							//置为空
							GroupFunPanel.SetActivity(false);
							JoinShowClubPanel.SetActive(false);
							CreateClubPanel.SetActive(false);
							ClubInfoPanel.SetActive(false);
							ClubMenberListPanel.SetActive(false);
						}
					}
					break;
				}
			}

		}

		/// <summary>
		/// 取消所有状态
		/// </summary>
		public static void UnSelectAllState()
		{
			foreach (var kv in clubItemList)
			{
				kv.Value.SetSelectState(false);
			}
		}

		/// <summary>
		/// 刷新亲友圈列表
		/// </summary>
		public static void UpdateClubList()
		{
			int selfId = int.Parse(GoableData.userValiadateInfor.DatingNumber);
			GoableClubDataInfo.groupInfoList.Sort((left, right) => 
			{
				if (left.groupInfo.groupMasterId != selfId && right.groupInfo.groupMasterId == selfId)
				{
					return 1;
				}
				else if (left.groupInfo.groupMasterId == selfId && right.groupInfo.groupMasterId != selfId)
				{
					return -1;
				}
				else
				{
					return 0;
				}
			});
			for (int i = 0; i < GoableClubDataInfo.groupInfoList.Count; ++i)
			{
				GroupWarp gw = GoableClubDataInfo.groupInfoList[i];
				if (gw.bindClubItem == null)
				{
					AddClubList(i,gw);
				}
				else
				{
					UpClubPos(i,gw);
				}
			}
		}

		public static void UpClubPos(int index,GroupWarp gw)
		{
			gw.bindClubItem.itemNode.transform.localPosition = new Vector3(248.0f, distanceStartPosY - distanceOffset * index, 0.0f);
		}

		/// <summary>
		/// 添加亲友圈列表
		/// </summary>
		public static void AddClubList(int index,GroupWarp gw)
		{
			GameObject newClub = GameObject.Instantiate(itemScoreNode);
			newClub.transform.SetParent(itemScoreNode.transform.parent);
			newClub.transform.localScale = Vector3.one;
			newClub.transform.localPosition = new Vector3(248.0f, distanceStartPosY - distanceOffset * index, 0.0f);
			newClub.SetActive(true);

			ClubItem ci = new ClubItem();
			clubItemList.Add(gw.groupInfo.groupId, ci);
			ci.bindGwInfo = gw;
			ci.GetItem(newClub, gw.groupInfo.clubId);
			ci.SetItemInfo();
			ci.UpShowItemInfo();

			RectTransform tectTransform = groupListNode.GetComponent<RectTransform>();
			tectTransform.sizeDelta = new Vector2(tectTransform.sizeDelta.x, distanceOffset * (index + 1));
			GroupFunPanel.SetActivity(true);
		}

		/// <summary>
		/// 返回一个亲友圈句柄
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public static ClubItem GetClubItem(string groupId)
		{
			if (clubItemList.ContainsKey(groupId))
			{
				return clubItemList[groupId];
			}

			return null;
		}

		/// <summary>
		/// 获取亲友圈Item
		/// </summary>
		/// <param name="clubId"></param>
		/// <returns></returns>
		public static ClubItem GetClubItemWithClubId(string clubId)
		{
			foreach (var item in clubItemList)
			{
				if (item.Value.bindGwInfo.groupInfo.clubId == clubId)
				{
					return item.Value;
				}
			}

			return null;
		}


		/// <summary>
		/// 设置亲友圈签名
		/// </summary>
		/// <param name="clubId"></param>
		/// <param name="sign"></param>
		public static void SetClubSign(string clubId, string sign)
		{
			ClubItem itemClub = GetClubItemWithClubId(clubId);

			if (itemClub != null)
			{
				itemClub.bindGwInfo.groupInfo.sign = sign;
			}
		}

		#region 网络请求
		/// <summary>
		/// 获取节点
		/// </summary>
		/// <param name="node"></param>
		public static void GetUI(GameObject node)
		{

			ClubItem.clubItemState = null;
			rootNode = node;
			btnJoinGroup = GenericityTool.GetComponentByPath<Button>(rootNode, "btn_joinGroup");
			btnCreateGroup = GenericityTool.GetComponentByPath<Button>(rootNode, "btn_createGroup");
			btnBackrall = GenericityTool.GetComponentByPath<Button>(rootNode, "btn_findGroup");
			btnFindGroup = GenericityTool.GetComponentByPath<Button>(rootNode, "btn_findGroup");
			btnRequest = GenericityTool.GetComponentByPath<Button>(rootNode, "btn_request");
			requestItem = GenericityTool.GetObjectByPath(rootNode, "btn_request/sport");
			txt_requestCount = GenericityTool.GetComponentByPath<Text>(rootNode, "btn_request/sport/txt_value");

			inputGroupId = GenericityTool.GetComponentByPath<InputField>(rootNode, "input_groupId");

			btnJoinGroup.onClick.AddListener(JoinGroup);
			btnCreateGroup.onClick.AddListener(CreateGroup);
			btnBackrall.onClick.AddListener(BackRall);
			btnFindGroup.onClick.AddListener(FindGroupId);
			btnRequest.onClick.AddListener(RequestShow);
			groupListNode = GenericityTool.GetObjectByPath(rootNode, "Scroll View/Viewport/Content");
			itemScoreNode = GenericityTool.GetObjectByPath(rootNode, "Scroll View/Viewport/Content/item_source");
			itemScoreNode.SetActive(false);
		}

		public static void UpShowRequest()
		{
			List<P_RequestInfo> requestList = IMClub.GoableClubDataInfo.GetAllGroupRequest();
			if (requestList != null && requestList.Count <= 0)
			{
				requestItem.SetActive(false);
			}
			else
			{
				requestItem.SetActive(true);
				txt_requestCount.text = requestList.Count.ToString();
			}
		}

		public static void RequestShow()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			List<P_RequestInfo> requestList = IMClub.GoableClubDataInfo.GetAllGroupRequest();

			AgreeRequestPanel.OpenInfo(requestList);
		}

		public static void JoinGroup()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			IMClub.JoinClubPanel.OpenPanel();
		}

		public static void CreateGroup()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			IMClub.CreateClubPanel.ShowPanelInfo();
		}

		public static void FindGroupId()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (string.IsNullOrEmpty(inputGroupId.text))
			{
				UINameSpace.UITipMessage.PlayMessage("查找值不能为空");
				return;
			}

			UINameSpace.UIWaitting.AddShowWaitting("CLUBFindGroup");
			MessageSend.FindGroup(inputGroupId.text);
		}

		/// <summary>
		/// 返回大厅
		/// </summary>
		public static void BackRall()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
		}

		/// <summary>
		/// 获取组群
		/// </summary>
		public static void GetGroups()
		{
			CloseClubList();
			IMClub.MessageSend.GetGroups();
		}
		#endregion 网络请求
	}

	/// <summary>
	/// 亲友圈功能界面
	/// </summary>
	public class GroupFunPanel
	{
		/// <summary>
		/// 亲友圈节点
		/// </summary>
		public static GameObject funNode;
		/// <summary>
		/// 亲友圈头像
		/// </summary>
		public static Image imgGroupHead;
		/// <summary>
		/// 亲友圈名字
		/// </summary>
		public static Text txtGroupName;
		/// <summary>
		/// 亲友圈ID
		/// </summary>
		public static Text txtGroupId;
		/// <summary>
		/// 自己的竞技分
		/// </summary>
		public static Text txtSelfScore;
		/// <summary>
		/// 按钮设置
		/// </summary>
		public static Button btnSetting;
		/// <summary>
		/// 邀请玩家按钮
		/// </summary>
		public static Button btnRequest;
		/// <summary>
		/// 分享
		/// </summary>
		public static Button btnShare;
		/// <summary>
		/// 按钮退出
		/// </summary>
		public static Button btnLeave;
		/// <summary>
		/// 亲友圈信息
		/// </summary>
		public static Button btnGroupInfo;

		/// <summary>
		/// 获取UI节点
		/// </summary>
		public static void GetUI(GameObject objNode)
		{
			funNode = objNode;
			imgGroupHead = GenericityTool.GetComponentByPath<Image>(funNode, "groupHead/img_icon");
			txtGroupName = GenericityTool.GetComponentByPath<Text>(funNode, "groupHead/txt_groupName");
			txtGroupId = GenericityTool.GetComponentByPath<Text>(funNode, "groupHead/txt_groupId");
			txtSelfScore = GenericityTool.GetComponentByPath<Text>(funNode, "groupHead/txt_selfScore");
			btnSetting = GenericityTool.GetComponentByPath<Button>(funNode, "btn_setting");
			btnRequest = GenericityTool.GetComponentByPath<Button>(funNode, "btn_requestFriend");
			btnShare = GenericityTool.GetComponentByPath<Button>(funNode, "btn_share");
			btnLeave = GenericityTool.GetComponentByPath<Button>(funNode, "btn_back");
			btnGroupInfo = GenericityTool.GetComponentByPath<Button>(funNode, "groupHead/img_icon");
			btnRequest.onClick.AddListener(OnClickRequestFriend);
			btnSetting.onClick.AddListener(OnClickOpenSetting);
			btnLeave.onClick.AddListener(OnClickOpenLeave);
			btnShare.onClick.AddListener(OnClickShare);
			btnGroupInfo.onClick.AddListener(OnClickGroupInfo);
			GetFunNode();
			GroupSettingPanel.GetUI(GenericityTool.GetObjectByPath(funNode, "clubSettingNode"));
			GroupSettingPanel.SetActive(false);

			NoticeFunPanel.GetUI(GenericityTool.GetObjectByPath(funNode, "notice/noticeInfo"));
		}

		/// <summary>
		/// 群信息
		/// </summary>
		public static void OnClickGroupInfo()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			ClubInfoPanel.OpenClubInfo();
		}

		/// <summary>
		/// 获取功能面板
		/// </summary>
		public static void GetFunNode()
		{
			for (int i = 0; i < 3; ++i)
			{
				string btnPath = "";
				string panelPath = "";
				TablePanelItem tablePanelKv = null;
				if (i == 0)
				{
					tablePanelKv = new ClubChatPanel_Select();
					btnPath = "table_list/btn_chatRoom_select";
					panelPath = "panel_list/chatRoom";
				}
				else if (i == 1)
				{
					tablePanelKv = new ClubRoomPanel_Select();
					btnPath = "table_list/btn_gameRoom_select";
					panelPath = "panel_list/gameRoom";
				}
				else if (i == 2)
				{
					tablePanelKv = new ClubGradePanel_Select();
					btnPath = "table_list/btn_clubRoom_select";
					panelPath = "panel_list/gradeRoom";
				}

				tablePanelKv.tag = "ClubFunPanel";
				tablePanelKv.index = i;
				tablePanelKv.tableButton = GenericityTool.GetComponentByPath<Button>(funNode, btnPath);
				tablePanelKv.tablePanel = GenericityTool.GetObjectByPath(funNode, panelPath);
				tablePanelKv.RegistListen();
			}

			TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("ClubFunPanel");
			firstTablePanel.SelectPanel();
		}

		/// <summary>
		/// 刷新
		/// </summary>
		public static void UpShow(bool openMenberList = true)
		{
			UpInfo();

			if (openMenberList)
			{
				ClubMenberListPanel.OpenShow();
			}

			RefenceGroupPanel();

			NoticeFunPanel.EntryGroup();
		}

		public static void UpInfo()
		{
			txtGroupName.text = ClubItem.clubItemState.bindGwInfo.groupInfo.groupName;

			txtGroupId.text = "亲友圈ID " + ClubItem.clubItemState.bindGwInfo.groupInfo.clubId;

			P_ClubSetting ps = ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting;
			if (ps.collectScale == 0 && ps.collectScore == 0 && ps.scoreLimit == 0)
			{
				txtSelfScore.text = "";
			}
			else
			{
				//txtGroupId.text += string.Format("/{0}", ClubItem.clubItemState.bindGwInfo.menberList[int.Parse(GoableData.userValiadateInfor.DatingNumber)].Score.ToString());
				txtSelfScore.text = "";//string.Format("/{0}", ClubItem.clubItemState.bindGwInfo.menberList[int.Parse(GoableData.userValiadateInfor.DatingNumber)].Score.ToString());
			}

			txtSelfScore.text = "人数:" + ClubItem.clubItemState.bindGwInfo.menberList.Count + "/500";

			if (!string.IsNullOrEmpty(ClubItem.clubItemState.bindGwInfo.groupInfo.iconUrl))
			{
				SetImageForHttpbytes.SetImageFromUrl(imgGroupHead, ClubItem.clubItemState.bindGwInfo.groupInfo.iconUrl);
			}
			else
			{
				AssetsParkManager.SetUguiImageThingIcon(IMClub.ConfigProject.iconsName, imgGroupHead, "clubIcon");
			}
		}

		/// <summary>
		/// 刷新亲友圈面板
		/// </summary>
		public static void RefenceGroupPanel()
		{
			if (ClubChatPanel_Select.clubChatPanel != null)
			{
				ClubChatPanel_Select.clubChatPanel.ResetShow();
			}
			if (ClubGradePanel_Select.clubGradePanel != null)
			{
				ClubGradePanel_Select.clubGradePanel.ResetShow();
			}
			if (ClubRoomPanel_Select.clubRoomPanel != null)
			{
				ClubRoomPanel_Select.clubRoomPanel.ResetShow();
			}

			GroupSettingPanel.SetActive(false);
		}

		/// <summary>
		/// 开启设置
		/// </summary>

		public static void OnClickOpenSetting()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			GroupSettingPanel.SetActive(true);
		}

		public static void OnClickRequestFriend()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			RequestFriendPanel.SetActive(true);
		}

		/// <summary>
		/// 开启离开
		/// </summary>
		public static void OnClickOpenLeave()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
		}

		/// <summary>
		/// 分享
		/// </summary>
		public static void OnClickShare()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			string IconPath = FrameWorkDrvice.AssetsPathManagerInstance.GetExternPathNode() + "/" + "icon.jpg";
			string groupName = ClubItem.clubItemState.bindGwInfo.groupInfo.groupName;
			string id = ClubItem.clubItemState.bindGwInfo.groupInfo.clubId;
			P_ClubSetting clubSetting = ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting;

			YanlongShareStudio.WeiCharShareLink(string.Format("众享指尖亲友圈:{0}", id), "(" + GoableData.userValiadateInforWarp.PikeName + ")邀请你加入亲友圈,待你还原贴切的现实玩法,不封群不封号,让您安全无忧的竞技娱乐。", StringConfigClass.GetDownloadUrl(), IconPath);
		}

		/// <summary>
		/// 设置界面的显示状态
		/// </summary>
		/// <param name="active"></param>
		public static void SetActivity(bool active)
		{
			funNode.SetActive(active);

			OpenSettingState(false);
		}

		/// <summary>
		/// 开启设置状态
		/// </summary>
		/// <param name="state"></param>
		public static void OpenSettingState(bool state)
		{
			bool active = !state;

			if (active)
			{

				if (ClubItem.clubItemState != null
					&& ClubItem.clubItemState.bindGwInfo.groupInfo.groupMasterId.ToString() == GoableData.userValiadateInfor.DatingNumber)
				{
					btnSetting.gameObject.SetActive(true);
					btnRequest.gameObject.SetActive(true);
				}
				else
				{
					btnSetting.gameObject.SetActive(false);
					btnRequest.gameObject.SetActive(false);
				}
			}
			else
			{
				btnSetting.gameObject.SetActive(false);
				btnRequest.gameObject.SetActive(false);
			}
		}
	}

	/// <summary>
	/// 推送功能面板
	/// </summary>
	public class NoticeFunPanel
	{
		/// <summary>
		/// 节点
		/// </summary>
		public static GameObject funNode;

		/// <summary>
		/// 开启按钮
		/// </summary>
		public static Button btn_open;

		/// <summary>
		/// 关闭按钮
		/// </summary>
		public static Button btn_close;
		/// <summary>
		/// 公告信息
		/// </summary>
		public static Text txt_info;

		public static bool isOpen;

		/// <summary>
		/// 获取节点
		/// </summary>
		/// <param name="uiNode"></param>
		public static void GetUI(GameObject uiNode)
		{
			isOpen = false;
			funNode = uiNode;
			btn_open = GenericityTool.GetComponentByPath<Button>(funNode.transform.parent.gameObject, "btn_down");
			btn_close = GenericityTool.GetComponentByPath<Button>(funNode, "btn_up");

			txt_info = GenericityTool.GetComponentByPath<Text>(funNode, "txt_Content");

			btn_open.onClick.AddListener(OnChangePanel);
			btn_close.onClick.AddListener(OnChangePanel);
		}

		/// <summary>
		/// 切换面板
		/// </summary>
		public static void OnChangePanel()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			OnChangePanelNoAudio();
		}

		public static void OnChangePanelNoAudio()
		{
			isOpen = !isOpen;

			if (isOpen)
			{
				OpenShowInfo();
			}
			else
			{
				CloseShowInfo();
			}
		}


		public static void OpenShowInfo()
		{
			if (string.IsNullOrEmpty(ClubItem.clubItemState.bindGwInfo.groupInfo.sign))
			{
				txt_info.text = "亲友圈内暂无公告!";
			}
			else
			{
				txt_info.text = ClubItem.clubItemState.bindGwInfo.groupInfo.sign;
			}

			CherishTweenMove.Begin(funNode, funNode.transform.localPosition, Vector3.zero, 0.2f, 0.0f, true);

			btn_close.gameObject.SetActive(true);
		}

		public static void CloseShowInfo()
		{
			CherishTweenMove.Begin(funNode, funNode.transform.localPosition, new Vector3(0, 680.0f), 0.2f, 0.0f, true);

			btn_close.gameObject.SetActive(false);
		}

		public static void EntryGroup()
		{
			
			if (string.IsNullOrEmpty(ClubItem.clubItemState.bindGwInfo.groupInfo.sign))
			{
				isOpen = true;
			}
			else
			{
				isOpen = false;
			}
			OnChangePanelNoAudio();
		}
	}
	
    /// <summary>
    /// 亲友圈设置面板
    /// </summary>
    public class GroupSettingPanel
    {
        /// <summary>
        /// 面板节点
        /// </summary>
        public static GameObject panelNode;
        /// <summary>
        /// 关闭
        /// </summary>
        public static Button btn_close;

        /// <summary>
        /// 获取UI
        /// </summary>
        /// <param name="node"></param>
        public static void GetUI(GameObject node)
        {
            panelNode = node;
            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
            btn_close.onClick.AddListener(OnClickClose);
            GetFunNode();

            ClubScoreSettingPanel.GetUI(GenericityTool.GetObjectByPath(panelNode, "setScoreNode"));
            ClubScoreSettingPanel.SetActive(false);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
        }

        /// <summary>
        /// 获取功能面板
        /// </summary>
        public static void GetFunNode()
        {
            for (int i = 0; i < 4; ++i)
            {
                string btnPath = "";
                string panelPath = "";
                TablePanelItem tablePanelKv = null;
                if (i == 0)
                {
                    tablePanelKv = new ClubStatisticsPanel_Select();
                    btnPath = "table_list/btn_statistics_select";
                    panelPath = "panel_list/statisticsPanel";
                }
                else if (i == 1)
                {
                    tablePanelKv = new ClubBestSettingPanel_Select();
                    btnPath = "table_list/btn_bestSetting_select";
                    panelPath = "panel_list/bestSettingPanel";
                }
                else if (i == 2)
                {
                    tablePanelKv = new ClubRuleSettingPanel_Select();
                    btnPath = "table_list/btn_ruleSetting_select";
                    panelPath = "panel_list/ruleSettingPanel";
                }
                else if (i == 3)
                {
                    tablePanelKv = new ClubMenberListPanel_Select();
                    btnPath = "table_list/btn_menberList_select";
                    panelPath = "panel_list/menberListPanel";
                }
                else if (i == 4)
                {
                    tablePanelKv = new ClubBlackListPanel_Select();
                    btnPath = "table_list/btn_blackList_select";
                    panelPath = "panel_list/blackListPanel";
                }

                tablePanelKv.tag = "ClubSettingPanel";
                tablePanelKv.index = i;
                tablePanelKv.tableButton = GenericityTool.GetComponentByPath<Button>(panelNode, btnPath);
                tablePanelKv.tablePanel = GenericityTool.GetObjectByPath(panelNode, panelPath);
                tablePanelKv.RegistListen();
            }

            TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("ClubSettingPanel");
            firstTablePanel.SelectPanel();
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            GroupFunPanel.OpenSettingState(active);
            panelNode.SetActive(active);
            if(active)
            {
                TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("ClubSettingPanel");
                firstTablePanel.SelectPanel();
            }
        }
    }

    /// <summary>
    /// 亲友圈面板信息
    /// </summary>
    public class ClubInfoPanel
    {
        /// <summary>
        /// 亲友圈节点
        /// </summary>
        public static GameObject panelNode;
		/// <summary>
		/// 图标
		/// </summary>
		public static Image img_icon;
		/// <summary>
		/// 上传图标
		/// </summary>
		public static Button btn_upload;
        /// <summary>
        /// 亲友圈名字
        /// </summary>
        public static Text txt_name;
		/// <summary>
		/// 亲友圈ID
		/// </summary>
		public static Text txt_id;
        /// <summary>
        /// 输入亲友圈信息
        /// </summary>
        public static InputField input_Info;
        /// <summary>
        /// 提交
        /// </summary>
        public static Button btn_submit;
        /// <summary>
        /// 关闭
        /// </summary>
        public static Button btn_close;
		/// <summary>
		/// 离开
		/// </summary>
		public static Button btn_leave;
		/// <summary>
		/// 解散
		/// </summary>
		public static Button btn_unrelese;

		/// <summary>
		/// 获取UI
		/// </summary>
		public static void GetUI(GameObject node)
        {
            panelNode = node;
			img_icon = GenericityTool.GetComponentByPath<Image>(panelNode, "img_icon");
			btn_upload = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_upload");
			txt_name = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_clubName");
			txt_id = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_clubId");
			input_Info = GenericityTool.GetComponentByPath<InputField>(panelNode, "input_clubInfo");
            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
            btn_submit = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_submit");

			btn_leave = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_leave");
			btn_unrelese = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_unRelese");


			btn_close.onClick.AddListener(OnClickClose);
            btn_submit.onClick.AddListener(OnClickSubmit);

			btn_leave.onClick.AddListener(OnClickLeave);
			btn_unrelese.onClick.AddListener(OnClickUnRelese);

			btn_upload.onClick.AddListener(OnClickUpHead);

		}

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }

        /// <summary>
        /// 开启亲友圈信息
        /// </summary>
        public static void OpenClubInfo()
        {
            P_GroupInfo groupInfo = ClubItem.clubItemState.bindGwInfo.groupInfo;
            SetActive(true);

			if (!string.IsNullOrEmpty(groupInfo.iconUrl))
			{
				SetImageForHttpbytes.SetImageFromUrl(img_icon, groupInfo.iconUrl);
			}
			else
			{
				AssetsParkManager.SetUguiImageThingIcon(IMClub.ConfigProject.iconsName, img_icon, "clubIcon");
			}

            txt_name.text = groupInfo.groupName;
			txt_id.text = groupInfo.clubId;

			input_Info.text = groupInfo.sign;

            if (groupInfo.groupMasterId.ToString() == GoableData.userValiadateInfor.DatingNumber)
            {
                input_Info.readOnly = false;
                btn_submit.gameObject.SetActive(true);
				btn_unrelese.gameObject.SetActive(true);
				btn_leave.gameObject.SetActive(false);
				btn_upload.gameObject.SetActive(true);
			}
            else
            {
                input_Info.readOnly = true;
                btn_submit.gameObject.SetActive(false);
				btn_unrelese.gameObject.SetActive(false);
				btn_leave.gameObject.SetActive(true);
				btn_upload.gameObject.SetActive(false);
			}
        }

        /// <summary>
        /// 点击关闭
        /// </summary>
        public static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
        }

        /// <summary>
        /// 点击提交
        /// </summary>
        public static void OnClickSubmit()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (string.IsNullOrEmpty(input_Info.text))
            {
                UINameSpace.UITipMessage.PlayMessage("提交签名不能为空!");
                return;
            }
            UINameSpace.UIWaitting.AddShowWaitting("SC_SetSign");
            P_GroupInfo groupInfo = ClubItem.clubItemState.bindGwInfo.groupInfo;
            MessageSend.SetClubSign(groupInfo.clubId, input_Info.text);
            SetActive(false);
        }

		/// <summary>
		/// 离开
		/// </summary>
		public static void OnClickLeave()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			P_GroupInfo groupInfo = ClubItem.clubItemState.bindGwInfo.groupInfo;
			int curScore = ClubItem.clubItemState.bindGwInfo.menberList[int.Parse(GoableData.userValiadateInfor.DatingNumber)].Score;
			if (curScore > 0 || curScore < 0)
			{
				UINameSpace.UITipMessage.PlayMessage("请联系亲友圈管理员!");
				return;
			}
			UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.SC_LeaveClub_MsgType");
			MessageSend.LeaveClub(groupInfo.clubId);
			SetActive(false);
		}

		/// <summary>
		/// 解散
		/// </summary>
		public static void OnClickUnRelese()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.SC_UnReleseClub_MsgType");
			P_GroupInfo groupInfo = ClubItem.clubItemState.bindGwInfo.groupInfo;
			MessageSend.UnReleseClub(groupInfo.clubId);
			SetActive(false);
		}

        /// <summary>
        /// 亲友圈简介设置
        /// </summary>
        public static void SetSign(string clubId,string sign)
        {
            ClubListPanel.SetClubSign(clubId, sign);
        }

		/// <summary>
		/// 上传头像
		/// </summary>
		public static void OnClickUpHead()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (!Application.isMobilePlatform)
			{
				UpIcon();
			}
			else
			{
				LSharpEntryGame.TakePhotoFinishCallBack = UpHeadCallBack;
				LSharpEntryGame.SetAppEnumTakePhoto();
				CherishUtility.OpenTakePhoto(1);
			}
		}

		/// <summary>
		/// 上传头像结束
		/// </summary>
		public static void UpHeadCallBack()
		{
			int resultCode = CherishUtility.GetTakePhotoResult();
			DebugLoger.Log("上传图片! resultCode " + resultCode);
			if (resultCode == 0)
			{
				//UINameSpace.UITipMessage.PlayMessage("照片处理取消!");
				return;
			}
			LSharpEntryGame.TakePhotoFinishCallBack = null;
			UpIcon();
		}

		public static void UpIcon()
		{
			try
			{
				byte[] dataBuf = null;
				//这里是处理完成照片 准备上传
				string imagePath = Application.persistentDataPath.Replace('\\', '/') + "/image.jpg";
				if (!File.Exists(imagePath))
				{
					DebugLoger.Log("图片不存在!");
					UINameSpace.UITipMessage.PlayMessage("图片不存在!");
					return;
				}
				using (FileStream fs = File.Open(imagePath, FileMode.Open))
				{
					dataBuf = new byte[fs.Length];
					fs.Read(dataBuf, 0, dataBuf.Length);
				}

				if (dataBuf != null && dataBuf.Length > 0)
				{
					P_GroupInfo groupInfo = ClubItem.clubItemState.bindGwInfo.groupInfo;
					//这里可以上传数据
					Server.CS_UpLoadFile upLoadFile = new Server.CS_UpLoadFile();
					upLoadFile.UserValiadate = GoableData.userValiadateInfor;
					upLoadFile.fileType = 0;
					upLoadFile.otherBuf = new List<byte>(BitConverter.GetBytes(int.Parse(groupInfo.clubId)));
					upLoadFile.fileBuf = new List<byte>(dataBuf);
					DebugLoger.Log("post url:" + string.Format("http://{0}:9899", StringConfigClass.domAddr));
					HttpTools.PostHttpData(string.Format("http://{0}:9899", StringConfigClass.domAddr), upLoadFile.Serializer(), UpLoadFileCallBack);
				}
			}
			catch (Exception e)
			{
				DebugLoger.Log(e.ToString());
			}
		}

		static public void UpLoadFileCallBack(string data)
		{
			DebugLoger.Log("开始上传头像回调!");

			Server.SC_UpLoadFile entryMsg = new Server.SC_UpLoadFile();
			entryMsg.DeserializerJson(data);

			if (entryMsg.result == 1)
			{
				int clubId = BitConverter.ToInt32(entryMsg.otherBuf.ToArray(),0);

				string url = entryMsg.strInfo;

				GroupWarp gw = GoableClubDataInfo.GetGroup(clubId.ToString());

				if (gw != null)
				{
					gw.groupInfo.iconUrl = url;
					if (gw.bindClubItem != null)
					{
						gw.bindClubItem.UpShowItemInfo();
					}
				}

				OpenClubInfo();

				GroupFunPanel.UpInfo();
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("图标上传失败!");
			}
		}
    }

    /// <summary>
    /// 竞技分调整面板
    /// </summary>
    public class ClubScoreSettingPanel
    {
        /// <summary>
        /// 面板节点
        /// </summary>
        public static GameObject panelNode;
        /// <summary>
        /// 关闭
        /// </summary>
        public static Button btn_close;
        /// <summary>
        /// 昵称
        /// </summary>
        public static Text txt_name;
        /// <summary>
        /// 当前竞技分
        /// </summary>
        public static Text txt_score;
        /// <summary>
        /// 输入的要修改的值
        /// </summary>
        public static InputField input_score;
        /// <summary>
        /// 减少
        /// </summary>
        public static EventTrigger trigger_sub;
        /// <summary>
        /// 添加
        /// </summary>
        public static EventTrigger trigger_add;
        /// <summary>
        /// 保存
        /// </summary>
        public static Button btn_save;
        /// <summary>
        /// 绑定成员信息
        /// </summary>
        public static P_Menber bindMenber;
        /// <summary>
        /// 绑定的亲友圈ID
        /// </summary>
        public static string bindClubId;


        /// <summary>
        /// 添加的值类型
        /// </summary>
        public static float addValue;
        public static int maxValue = 200;
        /// <summary>
        /// 按下时间
        /// </summary>
        public static float tapTime;
        /// <summary>
        /// 按下状态
        /// </summary>
        public static bool subDown;
        public static bool addDown;

		/// <summary>
		/// 信用
		/// </summary>
		public static Button btn_credit;
		/// <summary>
		/// 选择节点
		/// </summary>
		public static Image imgSelect;
		/// <summary>
		/// 非选择节点
		/// </summary>
		public static Image imgNoSelect;
		/// <summary>
		/// 征信 0不好 1好
		/// </summary>
		public static byte credit;

        /// <summary>
        /// 速度
        /// </summary>
        public static float speed = 10.0f;

        public static void GetUI(GameObject node)
        {
            panelNode = node;
            DebugLoger.Log(panelNode.name);
            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
            txt_name = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_name");
            txt_score = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_curScore");
            input_score = GenericityTool.GetComponentByPath<InputField>(panelNode, "input_changeScore");
            trigger_sub = GenericityTool.GetComponentByPath<EventTrigger>(panelNode, "trigger_sub");
            trigger_add = GenericityTool.GetComponentByPath<EventTrigger>(panelNode, "trigger_add");
            btn_credit = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_credit");
            btn_save = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_send");
			imgSelect = GenericityTool.GetComponentByPath<Image>(panelNode, "img_select");
			imgNoSelect = GenericityTool.GetComponentByPath<Image>(panelNode, "img_selectNo");

			btn_credit.onClick.AddListener(OnClickCredit);
            btn_save.onClick.AddListener(OnClickSave);
            btn_close.onClick.AddListener(OnClickClose);

            UtilityTool.AddEventTriggerEvent(trigger_sub, EventTriggerType.PointerDown, OnTriggerSubDown);
            UtilityTool.AddEventTriggerEvent(trigger_sub, EventTriggerType.PointerUp, OnTriggerSubUp);
            UtilityTool.AddEventTriggerEvent(trigger_add, EventTriggerType.PointerDown, OnTriggerAddDown);
            UtilityTool.AddEventTriggerEvent(trigger_add, EventTriggerType.PointerUp, OnTriggerAddUp);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
        }

        /// <summary>
        /// 设置显示数据
        /// </summary>
        /// <param name="menberInfo"></param>
        public static void SetDataShow(string clubId,P_Menber menberInfo)
        {
            bindClubId = clubId;
            bindMenber = menberInfo;
			credit = bindMenber.credit;

			SetActive(true);
            txt_name.text = string.Format("昵称 {0}", bindMenber.menberName);
            txt_score.text = bindMenber.Score.ToString();
			
			UpCredit();
            addValue = 0.0f;
            subDown = false;
            addDown = false;
        }

        /// <summary>
        /// 面板是否开启
        /// </summary>
        /// <returns></returns>
        public static bool IsActive()
        {
            return panelNode.activeSelf;
        }

        /// <summary>
        /// 设置激活状态
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }

        /// <summary>
        /// 更新信用状态
        /// </summary>
        public static void UpCredit()
        {
			if (credit == 1)
			{
				txt_score.color = Color.white;
				(btn_credit.targetGraphic as Image).overrideSprite = imgSelect.overrideSprite;
			}
			else
			{
				txt_score.color = Color.red;
				(btn_credit.targetGraphic as Image).overrideSprite = imgNoSelect.overrideSprite;
			}
        }
        
        /// <summary>
        /// 信用点击
        /// </summary>
        private static void OnClickCredit()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (credit == 1)
			{
				credit = 0;
			}
			else
			{
				credit = 1;
			}
            UpCredit();
        }

        /// <summary>
        /// 保存点击
        /// </summary>
        private static void OnClickSave()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			string inputText = input_score.text;
			if (string.IsNullOrEmpty(inputText))
			{
				input_score.text = "0";
				UINameSpace.UITipMessage.PlayMessage("输入的数值不能为空!");
				return;
			}

			if (!System.Text.RegularExpressions.Regex.IsMatch(inputText, "^(0|[1-9][0-9]*|-[1-9][0-9]*)$"))
			{
				input_score.text = "0";
				UINameSpace.UITipMessage.PlayMessage("请输入整数!");
				return;
			}

			SetActive(false);
			UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.CS_ChangeMenberScore_MsgType");

			int changeValue = int.Parse(input_score.text);
            MessageSend.SetMenberScore(bindClubId, bindMenber.menberId, changeValue,credit);
        }

        /// <summary>
        /// 添加按下
        /// </summary>
        private static void OnTriggerAddDown(BaseEventData baseData)
        {
            addDown = true;
        }

        /// <summary>
        /// 添加弹起
        /// </summary>
        private static void OnTriggerAddUp(BaseEventData baseData)
        {
            addDown = false;
            addValue = 0.0f;
            if (tapTime < 0.5f)
            {
                int oldValue = int.Parse(input_score.text);
                input_score.text = (oldValue + 1).ToString();
            }
            tapTime = 0.0f;
        }

        /// <summary>
        /// 扣除按下
        /// </summary>
        private static void OnTriggerSubDown(BaseEventData baseData)
        {
            subDown = true;
        }

        /// <summary>
        /// 扣除弹起
        /// </summary>
        private static void OnTriggerSubUp(BaseEventData baseData)
        {
            subDown = false;
            addValue = 0.0f;

            if(tapTime < 0.5f)
            {
                int oldValue = int.Parse(input_score.text);
                input_score.text = (oldValue - 1).ToString();
            }

            tapTime = 0.0f;
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        public static void Update()
        {
            if (subDown || addDown)
            {
                tapTime += Time.deltaTime;
                if(tapTime >= 0.5f)
                {
                    tapTime = 0.5f;
                }

                if (tapTime >= 0.5f)
                {
					if (!System.Text.RegularExpressions.Regex.IsMatch(input_score.text, "^(0|[1-9][0-9]*|-[1-9][0-9]*)$"))
					{
						input_score.text = "0";
					}

					int oldValue = int.Parse(input_score.text);
                    if (addDown)
                    {
                        addValue += Time.deltaTime * speed;

                        if (addValue > maxValue)
                        {
                            addValue = maxValue;
                        }
                    }
                    else if (subDown)
                    {
                        addValue -= Time.deltaTime * speed;
                        if (addValue < -maxValue)
                        {
                            addValue = -maxValue;
                        }
                    }

                    input_score.text = (oldValue + (int)addValue).ToString();
                }
            }
        }
    }

    /// <summary>
    /// 加入亲友圈面板
    /// </summary>
    public class JoinShowClubPanel
    {
        public static GameObject panelNode;
        /// <summary>
        /// 查找
        /// </summary>
        public static Button btn_find;
        /// <summary>
        /// 关闭
        /// </summary>
        public static Button btn_close;
        /// <summary>
        /// 亲友圈信息
        /// </summary>
        public static Text txt_info;
        /// <summary>
        /// 亲友圈ID
        /// </summary>
        public static Text txt_id;
        /// <summary>
        /// 亲友圈简介
        /// </summary>
        public static Text txt_sign;
        /// <summary>
        /// 绑定的亲友圈信息
        /// </summary>
        public static P_GroupInfo bindGroupInfo;
		
        /// <summary>
        /// 获取UI节点
        /// </summary>
        /// <param name="node"></param>
        public static void GetUI(GameObject node)
        {
            panelNode = node;

            btn_find = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_find");
            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
            txt_info = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_info");
            txt_id = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_id");
            txt_sign = GenericityTool.GetComponentByPath<Text>(panelNode, "txt_content");

            btn_find.onClick.AddListener(OnClickJoinClub);
            btn_close.onClick.AddListener(OnClickClose);
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        public static void ShowPanelInfo(P_GroupInfo groupInfo)
        {
            SetActive(true);
            bindGroupInfo = groupInfo;

            txt_info.text = bindGroupInfo.groupName;
            txt_sign.text = bindGroupInfo.sign;
            txt_id.text = string.Format("ID {0}",bindGroupInfo.clubId);
        }

        /// <summary>
        /// 点击加入亲友圈
        /// </summary>
        private static void OnClickJoinClub()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			//UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.CS_AddGroup_MsgType");
			IMClub.MessageSend.AddGroupRequeset(bindGroupInfo.clubId);
            SetActive(false);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        private static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
        }

        /// <summary>
        /// 切换面激活状态
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }
    }
	
	/// <summary>
	/// 同意的ITEM
	/// </summary>
	public class AgreeRequestItem
	{
		/// <summary>
		/// 节点
		/// </summary>
		public GameObject itemNode;
		/// <summary>
		/// 用户头像
		/// </summary>
		public CircleImage headImage;
		/// <summary>
		/// 请求用户名字
		/// </summary>
		public Text txt_name;
		/// <summary>
		/// 请求用户ID
		/// </summary>
		public Text txt_id;
		/// <summary>
		/// 请求的亲友圈
		/// </summary>
		public Text txt_club;
		/// <summary>
		/// 同意按钮
		/// </summary>
		public Button btn_agree;
		/// <summary>
		/// 拒绝按钮
		/// </summary>
		public Button btn_refuse;
		/// <summary>
		/// 绑定的请求用户数据
		/// </summary>
		public P_RequestInfo bindRequestInfo;

		public void GetUI(GameObject itemObj)
		{
			itemNode = itemObj;

			headImage = GenericityTool.GetComponentByPath<CircleImage>(itemNode, "imageHead");
			txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_name");
			txt_id = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_id");
			txt_club = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_joinClub");

			btn_agree = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_agree");
			btn_refuse = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_refuse");

			btn_agree.onClick.AddListener(OnAgree);
			btn_refuse.onClick.AddListener(OnRefuse);
		}

		/// <summary>
		/// 同意
		/// </summary>
		public void OnAgree()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			UINameSpace.UIWaitting.AddShowWaitting("SC_AgrentMenberJoin_MsgType");
			MessageSend.AgreeRequestClub(bindRequestInfo.clubId, bindRequestInfo.menberId, 1);
		}

		/// <summary>
		/// 拒绝
		/// </summary>
		public void OnRefuse()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			UINameSpace.UIWaitting.AddShowWaitting("SC_AgrentMenberJoin_MsgType");
			MessageSend.AgreeRequestClub(bindRequestInfo.clubId, bindRequestInfo.menberId, 0);
		}

		/// <summary>
		/// 设置信息
		/// </summary>
		public void SetInfo(P_RequestInfo info)
		{
			bindRequestInfo = info;

			if (!string.IsNullOrEmpty(bindRequestInfo.headUrl))
			{
				SetCircleImageForHttpbytes.SetCircleImageFromUrl(headImage, bindRequestInfo.headUrl);
			}
			else
			{
				if (bindRequestInfo.sex == 1)
				{
					///男
					AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, headImage, "GameEnd10");
				}
				else
				{
					///女
					AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, headImage, "GameEnd9");
				}
			}

			txt_name.text = bindRequestInfo.menberName;
			txt_id.text = bindRequestInfo.menberId.ToString();
			GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(bindRequestInfo.clubId);
			if (gw != null)
			{
				txt_club.text = "亲友圈:" + gw.groupInfo.groupName;
			}
			else
			{
				txt_club.text = "亲友圈:错误";
			}
		}

		/// <summary>
		/// 显示激活
		/// </summary>
		/// <param name="active"></param>
		public void SetActive(bool active)
		{
			itemNode.SetActive(active);
		}

		/// <summary>
		/// 设置父节点
		/// </summary>
		/// <param name="parent"></param>
		public void SetParent(GameObject parent)
		{
			itemNode.transform.SetParent(parent.transform);
			itemNode.transform.localPosition = Vector3.zero;
			itemNode.transform.localScale = Vector3.one;
		}

		/// <summary>
		/// 删除节点
		/// </summary>
		public void Destory()
		{
			GameObject.DestroyImmediate(itemNode);
		}
	}

	/// <summary>
	/// 同意加入面板
	/// </summary>
	public class AgreeRequestPanel
	{
		/// <summary>
		/// 面板节点
		/// </summary>
		public static GameObject panelNode;
		/// <summary>
		/// 关闭按钮
		/// </summary>
		public static Button btn_close;
		/// <summary>
		/// 资源节点
		/// </summary>
		public static GameObject sourceItem;
		/// <summary>
		/// 列表
		/// </summary>
		public static List<AgreeRequestItem> agreeRequestList = new List<AgreeRequestItem>();

		/// <summary>
		/// 获取UI
		/// </summary>
		/// <param name="uiNode"></param>
		public static void GetUI(GameObject uiNode)
		{
			panelNode = uiNode;

			btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btnClose");
			sourceItem = GenericityTool.GetObjectByPath(panelNode, "agrentList/Viewport/Content/itemSource");
			sourceItem.SetActive(false);

			btn_close.onClick.AddListener(ClosePanel);
		}

		/// <summary>
		/// 关闭面板
		/// </summary>
		public static void ClosePanel()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
		}

		/// <summary>
		/// 所有信息开启
		/// </summary>
		/// <param name="allRequestInfo"></param>
		public static void OpenInfo(List<P_RequestInfo> allRequestInfo,bool mustOpen = true)
		{
			if (panelNode == null)
			{
				return;
			}
			
			DestoryAll();
			if (allRequestInfo.Count > 0 || mustOpen)
			{
				SetActive(true);
				for (int i = 0; i < allRequestInfo.Count; ++i)
				{
					P_RequestInfo requestInfo = allRequestInfo[i];
					GameObject newItem = GameObject.Instantiate(sourceItem);

					AgreeRequestItem agreeRequest = new AgreeRequestItem();
					agreeRequest.GetUI(newItem);
					agreeRequestList.Add(agreeRequest);

					agreeRequest.SetInfo(requestInfo);
					agreeRequest.SetParent(sourceItem.transform.parent.gameObject);
					agreeRequest.SetActive(true);
				}
			}
			else
			{
				SetActive(false);
			}
		}

		/// <summary>
		/// 设置激活状态
		/// </summary>
		/// <param name="active"></param>
		public static void SetActive(bool active)
		{
			panelNode.SetActive(active);
		}


		/// <summary>
		/// 删除全部
		/// </summary>
		public static void DestoryAll()
		{
			for (int i = 0; i < agreeRequestList.Count; ++i)
			{
				agreeRequestList[i].Destory();
			}

			agreeRequestList.Clear();
		}
	}

    /// <summary>
    /// 创建亲友圈面板
    /// </summary>
    public class CreateClubPanel
    {
        /// <summary>
        /// 面板节点
        /// </summary>
        public static GameObject panelNode;
        /// <summary>
        /// 亲友圈名字
        /// </summary>
        public static InputField input_clubName;
        /// <summary>
        /// 创建亲友圈
        /// </summary>
        public static Button btn_create;
        /// <summary>
        /// 关闭
        /// </summary>
        public static Button btn_close;
        /// <summary>
        /// 获取UI
        /// </summary>
        public static void GetUI(GameObject node)
        {
            panelNode = node;
            input_clubName = GenericityTool.GetComponentByPath<InputField>(panelNode, "input_clubName");
            btn_create = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_create");
            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");

            btn_create.onClick.AddListener(OnClickCreate);
            btn_close.onClick.AddListener(OnClickClose);
        }

        private static void OnClickCreate()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
            if (string.IsNullOrEmpty(input_clubName.text) || input_clubName.text.Length < 3)
            {
                UINameSpace.UITipMessage.PlayMessage("亲友圈名字不能为空或者长度不能小于3个字符");
                return;
            }

            UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.SC_CreateGroup_MsgType");
            IMClub.MessageSend.CreateGroup(input_clubName.text);
        }

        private static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        public static void ShowPanelInfo()
        {
            SetActive(true);
        }

        /// <summary>
        /// 切换面激活状态
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }
    }

	/// <summary>
	/// 加入亲友圈面板
	/// </summary>
	public class JoinClubPanel
	{
		/// <summary>
		/// 面板节点
		/// </summary>
		public static GameObject panelNode;
		/// <summary>
		/// 确认亲友圈
		/// </summary>
		public static Button btn_submit;
		/// <summary>
		/// 关闭
		/// </summary>
		public static Button btn_close;
		/// <summary>
		/// 显示字符串
		/// </summary>
		public static Text txt_number;
		/// <summary>
		/// 按钮列表
		/// </summary>
		public static List<Button> numberBtn = new List<Button>();
		public static Button btnReset;
		public static Button btnDelete;

		public static List<int> numberList = new List<int>();

		public static void GetUI(GameObject node)
		{
			panelNode = node;

			txt_number = GenericityTool.GetComponentByPath<Text>(panelNode, "imageLine/txt_number");
			btn_submit = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_submit");
			btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btnClose");
			btn_close.onClick.AddListener(CloseUI);
			btn_submit.onClick.AddListener(Submit);

			for (int i = 0; i < 10; ++i)
			{
				Button btn = GenericityTool.GetComponentByPath<Button>(panelNode, "btnNumberList/btn_" + i);
				numberBtn.Add(btn);
			}
			numberBtn[0].onClick.AddListener(OnTapNumber_0);
			numberBtn[1].onClick.AddListener(OnTapNumber_1);
			numberBtn[2].onClick.AddListener(OnTapNumber_2);
			numberBtn[3].onClick.AddListener(OnTapNumber_3);
			numberBtn[4].onClick.AddListener(OnTapNumber_4);
			numberBtn[5].onClick.AddListener(OnTapNumber_5);
			numberBtn[6].onClick.AddListener(OnTapNumber_6);
			numberBtn[7].onClick.AddListener(OnTapNumber_7);
			numberBtn[8].onClick.AddListener(OnTapNumber_8);
			numberBtn[9].onClick.AddListener(OnTapNumber_9);

			btnReset = GenericityTool.GetComponentByPath<Button>(panelNode, "btnNumberList/btnReset");
			btnDelete = GenericityTool.GetComponentByPath<Button>(panelNode, "btnNumberList/btnDelete");

			btnReset.onClick.AddListener(OnReste);
			btnDelete.onClick.AddListener(OnDelete);
		}

		public static void OpenPanel()
		{
			SetActive(true);

			numberList.Clear();

			UpNumberValue();
		}

		public static void SetActive(bool active)
		{
			panelNode.SetActive(active);
		}

		public static void CloseUI()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
		}

		public static void Submit()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);

			string numberStr = "";

			for (int i = 0; i < numberList.Count; ++i)
			{
				int numberValue = numberList[i];

				numberStr += numberValue.ToString();
			}

			if (string.IsNullOrEmpty(numberStr))
			{
				UINameSpace.UITipMessage.PlayMessage("查找值不能为空");
				return;
			}

			UINameSpace.UIWaitting.AddShowWaitting("CLUBFindGroup");
			MessageSend.FindGroup(numberStr);
		}


		private static void OnReste()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			numberList.Clear();
			UpNumberValue();
		}

		private static void OnDelete()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count > 0)
			{
				numberList.RemoveAt(numberList.Count - 1);
			}
			UpNumberValue();
		}


		private static void OnTapNumber_0()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(0);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_1()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(1);

				UpNumberValue();
			}
		}


		private static void OnTapNumber_2()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(2);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_3()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(3);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_4()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(4);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_5()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(5);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_6()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(6);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_7()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(7);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_8()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(8);

				UpNumberValue();
			}
		}

		private static void OnTapNumber_9()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 8)
			{
				numberList.Add(9);

				UpNumberValue();
			}
		}

		private static void UpNumberValue()
		{
			txt_number.text = "";
			for (int i = 0; i < numberList.Count; ++i)
			{
				int numberValue = numberList[i];

				if (i == 0)
				{
					txt_number.text = numberValue.ToString();
				}
				else
				{
					txt_number.text += "  " + numberValue.ToString();
				}
			}
		}
	}

	/// <summary>
	/// 邀请玩家加入亲友圈
	/// </summary>
	public class RequestFriendPanel
	{
		/// <summary>
		/// 面板节点
		/// </summary>
		public static GameObject panelNode;
		/// <summary>
		/// 输入的成员ID
		/// </summary>
		public static InputField input_menberId;
		/// <summary>
		/// 关闭按钮
		/// </summary>
		public static Button btn_find;
		/// <summary>
		/// 关闭按钮
		/// </summary>
		public static Button btn_close;
		/// <summary>
		/// 成员节点
		/// </summary>
		public static GameObject menberInfoNode;
		/// <summary>
		/// 成员头像
		/// </summary>
		public static CircleImage img_head;
		/// <summary>
		/// 成员名字
		/// </summary>
		public static Text txt_name;
		/// <summary>
		/// 成员ID
		/// </summary>
		public static Text txt_id;
		/// <summary>
		/// 邀请好友
		/// </summary>
		public static Button btn_requestMenber;
		/// <summary>
		/// 获取到的信息
		/// </summary>
		public static IMClub.SC_GetUserInfo bindMenberInfo;

		/// <summary>
		/// 获取UI
		/// </summary>
		/// <param name="panelObj"></param>
		public static void GetUI(GameObject panelObj)
		{
			panelNode = panelObj;
			btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
			btn_close.onClick.AddListener(OnClickClose);

			btn_find = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_find");
			btn_find.onClick.AddListener(OnClickFind);
			input_menberId = GenericityTool.GetComponentByPath<InputField>(panelNode, "input_menberId");

			menberInfoNode = GenericityTool.GetObjectByPath(panelNode, "requredFriend");
			img_head = GenericityTool.GetComponentByPath<CircleImage>(menberInfoNode, "img_head");
			txt_name = GenericityTool.GetComponentByPath<Text>(menberInfoNode, "txt_menbername");
			txt_id = GenericityTool.GetComponentByPath<Text>(menberInfoNode, "txt_menberId");
			btn_requestMenber = GenericityTool.GetComponentByPath<Button>(menberInfoNode, "btn_requestFriend");
			btn_requestMenber.onClick.AddListener(OnClickRequestFriend);
		}

		/// <summary>
		/// 点击关闭
		/// </summary>
		public static void OnClickClose()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
		}

		/// <summary>
		/// 点击查找
		/// </summary>
		public static void OnClickFind()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			try
			{
				if (string.IsNullOrEmpty(input_menberId.text))
				{
					UINameSpace.UITipMessage.PlayMessage("用户ID不能为空!");
					return;
				}
				int menberId = int.Parse(input_menberId.text);
				UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.SC_GetUserInfo_MsgType");
				MessageSend.GetUserInfoAndClubInfo(IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, menberId);
			}
			catch (Exception ex)
			{
				UINameSpace.UITipMessage.PlayMessage("输入的用户ID错误!");
				DebugLoger.LogError(ex.ToString());
			}
		}

		/// <summary>
		/// 点击请求
		/// </summary>
		public static void OnClickRequestFriend()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			try
			{
				UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.SC_RequestMenberJoinClub_MsgType");
				MessageSend.RequestMenberJoinClub(IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, int.Parse(input_menberId.text));

				SetActive(false);
			}
			catch (Exception ex)
			{
				DebugLoger.LogError(ex.ToString());
			}
		}

		/// <summary>
		/// 设置界面开启关闭
		/// </summary>
		/// <param name="active"></param>
		public static void SetActive(bool active)
		{
			panelNode.SetActive(active);

			if (!active)
			{
				SetUserInfoShow(null);
			}

			input_menberId.text = "";

			bindMenberInfo = null;
		}

		/// <summary>
		/// 设置成员信息显示
		/// </summary>
		/// <param name="menberInfo"></param>
		public static void SetUserInfoShow(IMClub.SC_GetUserInfo menberInfo)
		{
			if (menberInfoNode == null)
			{
				return;
			} 

			if (menberInfo == null)
			{
				bindMenberInfo = null;
				menberInfoNode.SetActive(false);
			}
			else
			{
				bindMenberInfo = menberInfo;
				menberInfoNode.SetActive(true);

				txt_name.text = bindMenberInfo.nickName;
				txt_id.text = bindMenberInfo.menberId.ToString();

				if (!string.IsNullOrEmpty(bindMenberInfo.headUrl))
				{
					SetCircleImageForHttpbytes.SetCircleImageFromUrl(img_head, bindMenberInfo.headUrl);
				}
				else
				{
					if (bindMenberInfo.sex == 0)
					{
						AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd10");
					}
					else
					{
						AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd9");
					}
				}
			}
		}
	}
	
	/// <summary>
	/// 添加游戏规则节点
	/// </summary>
	public class AddGameRuleItem
    {
        /// <summary>
        /// Item节点
        /// </summary>
        public GameObject itemNode;
        /// <summary>
        /// 游戏图标
        /// </summary>
        public Image img_icon;
        /// <summary>
        /// 游戏名字
        /// </summary>
        public Text txt_name;
        /// <summary>
        /// 添加游戏
        /// </summary>
        public Button btn_add;
        /// <summary>
        /// 绑定的类型
        /// </summary>
        public int bindeType;

        /// <summary>
        /// 获取UI
        /// </summary>
        /// <param name="node"></param>
        public void GetUI(GameObject node)
        {
            itemNode = node;
            img_icon = GenericityTool.GetComponentByPath<Image>(itemNode, "imgIcon");
            txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_name");
            btn_add = GenericityTool.GetComponentByPath<Button>(itemNode, "btnAdd");

            btn_add.onClick.AddListener(OnClickAddRule);
        }

        /// <summary>
        /// 显示游戏信息
        /// </summary>
        /// <param name="gameType"></param>
        public void ShowInfo(int gameType)
        {
            DebugLoger.Log("showGameType " + gameType);
            bindeType = gameType;
            AssetsParkManager.SetUguiImageThingIcon(ConfigProject.iconsName, img_icon, "icon_game_" + bindeType);
            GameEntryItem gameItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntryWithGameType(bindeType);
            if (gameItem == null)
            {
                UINameSpace.UITipMessage.PlayMessage("系统错误,请关闭应用后再试!");
                return;
            }

            txt_name.text = gameItem.gameName;

        }

        /// <summary>
        /// 添加规则
        /// </summary>
        private void OnClickAddRule()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.CS_AddGameSetting_MsgType");
            //要给服务器发送一个添加规则
            MessageSend.AddGameSetting(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, bindeType);
        }

        /// <summary>
        /// 开启
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(GameObject parent)
        {
            itemNode.transform.SetParent(parent.transform);
        }

        /// <summary>
        /// 对齐位置
        /// </summary>
        public void SetPos()
        {
            itemNode.transform.localPosition = Vector3.zero;
            itemNode.transform.localScale = Vector3.one;
        }
    }
    
    /// <summary>
    /// 添加游戏规则面板
    /// </summary>
    public class AddGameRulePanel
    {
        /// <summary>
        /// 游戏规则列表
        /// </summary>
        public static List<AddGameRuleItem> gameRuleList = new List<AddGameRuleItem>();
        /// <summary>
        /// 面板节点
        /// </summary>
        public static GameObject panelNode;
        /// <summary>
        /// 滚动视图组件
        /// </summary>
        public static ScrollRect scroll_Compent;
        /// <summary>
        /// 按钮
        /// </summary>
        public static Button btn_close;
        /// <summary>
        /// 资源Item
        /// </summary>
        public static GameObject itemSource;
        /// <summary>
        /// 获取UI节点
        /// </summary>
        /// <param name="node"></param>
        public static void GetUI(GameObject node)
        {
            panelNode = node;

            scroll_Compent = GenericityTool.GetComponentByPath<ScrollRect>(panelNode, "scrollView");
            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
            itemSource = GenericityTool.GetObjectByPath(panelNode, "scrollView/Viewport/Content/ruleItem");
            itemSource.SetActive(false);
            btn_close.onClick.AddListener(OnClickClose);
        }

        /// <summary>
        /// 是否激活
        /// </summary>
        public static bool IsActive()
        {
            return panelNode.activeSelf;
        }

        /// <summary>
        /// 点击按钮
        /// </summary>
        private static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
        }

        /// <summary>
        /// 开启面板
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }

        /// <summary>
        /// 开启
        /// </summary>
        public static void Open(P_ClubSetting bindClubSetting)
        {
            //gameRuleList.Clear();
            List<int> toldGameIdList = new List<int>();
            toldGameIdList.Add((int)eRoomType.CheXuan);
            toldGameIdList.Add((int)eRoomType.PaoDeKuai);


            for (int i = 0; i < toldGameIdList.Count; ++i)
            {
                int value = toldGameIdList[i];
                bool isFind = false;
                //查找现在的游戏列表
                for (int b = 0; b < bindClubSetting.gamesSetting.Count; ++b)
                {
                    if (bindClubSetting.gamesSetting[b].gameType == value)
                    {
                        isFind = true;
                        break;
                    }
                }

                if (isFind)
                {
                    //这里添加一条到列表
                    toldGameIdList.Remove((int)value);
                }
            }

            if (toldGameIdList.Count > 0)
            {
                DeleteAllRule();
                ShowAllRule(toldGameIdList);
                SetActive(true);
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("不存在规则,无法添加!");
            }
        }

        /// <summary>
        /// 显示所有的规则
        /// </summary>
        public static void ShowAllRule(List<int> needAddGameTypeList)
        {
            for (int i = 0; i < needAddGameTypeList.Count; ++i)
            {
                int gameType = needAddGameTypeList[i];
                GameObject item = GameObject.Instantiate(itemSource);
                AddGameRuleItem gameRuleItem = new AddGameRuleItem();
                gameRuleList.Add(gameRuleItem);
                gameRuleItem.GetUI(item);
                gameRuleItem.SetParent(itemSource.transform.parent.gameObject);
                gameRuleItem.SetPos();
                gameRuleItem.ShowInfo(gameType);
                gameRuleItem.SetActive(true);
            }
        }

        /// <summary>
        /// 关闭所有的规则
        /// </summary>
        public static void DeleteAllRule()
        {
            for(int i = 0;i < gameRuleList.Count;++i)
            {
                AddGameRuleItem gameRuleItem = gameRuleList[i];
                GameObject.Destroy(gameRuleItem.itemNode);
            }
            gameRuleList.Clear();
        }

        /// <summary>
        /// 添加游戏规则
        /// </summary>
        /// <param name="gameType"></param>
        public static void AddRule(int gameType)
        {
            for (int i = 0; i < gameRuleList.Count; ++i)
            {
                AddGameRuleItem gameRuleItem = gameRuleList[i];

                if (gameRuleItem.bindeType == gameType)
                {
                    GameObject.Destroy(gameRuleItem.itemNode);
                    gameRuleList.RemoveAt(i);
                    break;
                }
            }

            if(gameRuleList.Count <= 0)
            {
                SetActive(false);               
            }
            ClubRuleSettingPanel_Select.Instance.OnSelect();
        }
    }
    
    /// <summary>
    /// 创建房间游戏
    /// </summary>
    public class CreateRoomSelectItem
    {
        /// <summary>
        /// 条例节点
        /// </summary>
        public GameObject itemNode;
        /// <summary>
        /// 图片
        /// </summary>
        public Image img_icon;
        /// <summary>
        /// 名字
        /// </summary>
        public Text txt_name;
		/// <summary>
		/// 房间规则
		/// </summary>
		public Text txt_rule;
        /// <summary>
        /// 按钮
        /// </summary>
        public Button btn_create;
        /// <summary>
        /// 绑定设置
        /// </summary>
        public P_GameSetting bindSetting;

        /// <summary>
        /// 获取UI
        /// </summary>
        /// <param name="node"></param>
        public void GetUI(GameObject node)
        {
            itemNode = node;
            img_icon = GenericityTool.GetComponentByPath<Image>(itemNode, "img_icon");
            txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_name");
			txt_rule = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_rule");
			btn_create = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_create");

            btn_create.onClick.AddListener(OnClickCreateGame);
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="setting"></param>
        public void SetShow(P_GameSetting setting)
        {
            bindSetting = setting;
            DebugLoger.Log("bindSetting.gameType " + bindSetting.gameType);
            AssetsParkManager.SetUguiImageThingIcon(ConfigProject.iconsName, img_icon, "icon_game_" + bindSetting.gameType);
            GameEntryItem gameItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntryWithGameType(bindSetting.gameType);

            if (gameItem == null)
            {
                UINameSpace.UITipMessage.PlayMessage("系统错误,请关闭应用后再试!");
                return;
            }

            txt_name.text = gameItem.gameName;
			txt_rule.text = bindSetting.roomValue + "局" + gameItem.callGetParmarsStr(bindSetting.pamarasSetting);


			SetActive(true);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        public void SetParent(GameObject parent)
        {
            itemNode.transform.SetParent(parent.transform);
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        public void SetPos()
        {
            itemNode.transform.localScale = Vector3.one;
            itemNode.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }

        /// <summary>
        /// 创建游戏
        /// </summary>
        public void OnClickCreateGame()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			CreateRoomSelectPanel.SetActive(false);
            UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.CS_CreateGameRoom_MsgType");
            //构建创建房间消息
            MessageSend.CreateGameRoom(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, (byte)bindSetting.gameType);
        }
    }

    /// <summary>
    /// 创建房间选择游戏面板
    /// </summary>
    public class CreateRoomSelectPanel
    {
        /// <summary>
        /// 创建游戏单条
        /// </summary>
        public static List<CreateRoomSelectItem> createSelectItemList = new List<CreateRoomSelectItem>();
        /// <summary>
        /// 面板节点
        /// </summary>
        public static GameObject panelNode;
        /// <summary>
        /// 关闭
        /// </summary>
        public static Button btn_close;
        /// <summary>
        /// 资源节点
        /// </summary>
        public static GameObject itemSource;

        /// <summary>
        /// 获取UI
        /// </summary>
        /// <param name="node"></param>
        public static void GetUI(GameObject node)
        {
            panelNode = node;

            btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "panelnode/btn_close");
            itemSource = GenericityTool.GetObjectByPath(panelNode, "panelnode/Scroll View/Viewport/Content/sourceItem");
            itemSource.SetActive(false);

            btn_close.onClick.AddListener(OnClickClose);
        }

        /// <summary>
        /// 关闭点击
        /// </summary>
        public static void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			DeleteItems();
            SetActive(false);
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="active"></param>
        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }

        /// <summary>
        /// 显示游戏
        /// </summary>
        public static void ShowInfoItems(List<P_GameSetting> gameSettings)
        {
            DeleteItems();
            for(int i= 0;i < gameSettings.Count;++i)
            {
                P_GameSetting setting = gameSettings[i];
                GameObject item = GameObject.Instantiate(itemSource);

                CreateRoomSelectItem selectItem = new CreateRoomSelectItem();
                createSelectItemList.Add(selectItem);
                selectItem.GetUI(item);
                selectItem.SetParent(itemSource.transform.parent.gameObject);
                selectItem.SetPos();
                selectItem.SetShow(setting);
            }
            SetActive(true);
        }

        /// <summary>
        /// 删除所有的条例
        /// </summary>
        public static void DeleteItems()
        {
            for (int i = 0; i < createSelectItemList.Count; ++i)
            {
                GameObject.Destroy(createSelectItemList[i].itemNode);
            }

            createSelectItemList.Clear();
        }
    }
	
	/// <summary>
	/// 亲友圈成员
	/// </summary>
	public class ClubMenberShowItem
	{
		/// <summary>
		/// 黑名单节点
		/// </summary>
		public GameObject itemNode;
		/// <summary>
		/// 头像图片
		/// </summary>
		public CircleImage headImage;
		/// <summary>
		/// 成员名
		/// </summary>
		public Text txt_menberName;
		/// <summary>
		/// 成员ID
		/// </summary>
		public Text txt_menberId;
		/// <summary>
		/// 成员状态
		/// </summary>
		public Text txt_menberState;
		/// <summary>
		/// 移除成员
		/// </summary>
		public Button btn_remove;
		/// <summary>
		/// 绑定成员
		/// </summary>
		public P_Menber bindMenber;
		/// <summary>
		/// 绑定的亲友圈ID
		/// </summary>
		public string bindClubId;

		/// <summary>
		/// 获取UI
		/// </summary>
		public void GetUI(GameObject item)
		{
			itemNode = item;
			headImage = GenericityTool.GetComponentByPath<CircleImage>(itemNode, "img_head");
			txt_menberName = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberName");
			txt_menberId = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberId");
			txt_menberState = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberState");
			btn_remove = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_remove");

			btn_remove.onClick.AddListener(OnClickRemove);
		}

		/// <summary>
		/// 点击移除成员
		/// </summary>
		private void OnClickRemove()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(bindClubId);
			if (gw == null)
			{
				UINameSpace.UIGeneralTip.ShowTip("确认移除", string.Format("是否确认从亲友圈移除成员 [{0}] ？",bindMenber.menberName), RemoveClickCallFun, bindMenber);
			}
			else
			{
				UINameSpace.UIGeneralTip.ShowTip("确认移除", string.Format("是否确认从亲友圈 [{0}] 移除成员 [{1}] ？", gw.groupInfo.groupName, bindMenber.menberName), RemoveClickCallFun, bindMenber);
			}
		}

		/// <summary>
		/// 漂白
		/// </summary>
		private void OnClickReset()
		{
			UINameSpace.UITipMessage.PlayMessage("暂未开通!");
		}

		private void RemoveClickCallFun(object paramar)
		{
			UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.SC_DeleteMenber_MsgType");
			MessageSend.DeleteMenber(bindClubId, bindMenber.menberId);
		}

		/// <summary>
		/// 设置父节点
		/// </summary>
		public void SetParent(Transform parent)
		{
			itemNode.transform.parent = parent;
			itemNode.transform.localScale = Vector3.one;
		}

		public void SetTopLess()
		{
			itemNode.transform.SetSiblingIndex(2);
		}

		public void SetEnd()
		{
			itemNode.transform.SetAsLastSibling();
		}

		/// <summary>
		/// 显示
		/// </summary>
		public void Show(string clubId, P_Menber menber)
		{
			Debug.Log("-------------1");
			bindClubId = clubId;
			bindMenber = menber;
			txt_menberName.text = bindMenber.menberName;
			txt_menberId.text = bindMenber.menberId.ToString();
			Debug.Log("-------------2");

			if (ClubItem.clubItemState.bindGwInfo.groupInfo.groupMasterId.ToString() == GoableData.userValiadateInfor.DatingNumber)
			{
				Debug.Log("-------------2-1");

				btn_remove.gameObject.SetActive(true);
			}
			else
			{
				Debug.Log("-------------2-2");

				btn_remove.gameObject.SetActive(false);
			}
			Debug.Log("-------------3");


			if (menber.insertTag == 1)
			{
				Debug.Log("-------------3-1");

				txt_menberState.text = "在线";
				txt_menberState.color = Color.green;
			}
			else
			{
				Debug.Log("-------------3-2");

				txt_menberState.text = "离线";
				txt_menberState.color = Color.white;
			}
			Debug.Log("-------------4");



			bool isGaming = false;
			List<P_RoomInfo> roomList = RoomManager.GetClubRoomInfoList(clubId);
			if (roomList != null)
			{
				for (int i = 0; i < roomList.Count; ++i)
				{
					P_RoomInfo roomInfo = roomList[i];
					if (roomInfo.menberList.Contains(menber.menberId))
					{
						isGaming = true;
						break;
					}
				}
			}
			Debug.Log("-------------5");

			if (isGaming)
			{
				txt_menberState.text = "游戏中";
				txt_menberState.color = Color.yellow;
			}

			if (!string.IsNullOrEmpty(menber.headUrl))
			{
				SetCircleImageForHttpbytes.SetCircleImageFromUrl(headImage, menber.headUrl);
			}
			else
			{
				if (menber.sex == 1)
				{
					AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, headImage, "GameEnd10");
				}
				else
				{
					AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, headImage, "GameEnd9");
				}
			}

			itemNode.SetActive(true);
		}

		/// <summary>
		/// 设置显示
		/// </summary>
		public void SetActive(bool active)
		{
			itemNode.SetActive(active);
		}
	}
	
	/// <summary>
	/// 亲友圈成员列表
	/// </summary>
	public class ClubMenberListPanel
	{
		/// <summary>
		/// 桌面UI
		/// </summary>
		public static GameObject tablePanel;
		/// <summary>
		/// 亲友圈成员列表
		/// </summary>
		public static List<ClubMenberShowItem> clubMenberList = new List<ClubMenberShowItem>();
		/// <summary>
		/// 用户节点资源显示
		/// </summary>
		public static GameObject menberItemSource;
		/// <summary>
		/// 查找
		/// </summary>
		public static Button btnFind;
		/// <summary>
		/// 关闭按钮
		/// </summary>
		public static Button btnClose;
		/// <summary>
		/// 查找输入
		/// </summary>
		public static InputField input_findClub;

		/// <summary>
		/// 开启关闭
		/// </summary>
		/// <param name="active"></param>
		public static void SetActive(bool active)
		{
			tablePanel.SetActive(active);
		}


		/// <summary>
		/// 开启显示
		/// </summary>
		public static void OpenShow()
		{
			SetActive(true);

			ResetShow();
		}

		/// <summary>
		/// 刷新显示效果
		/// </summary>
		public static void ResetShow()
		{
			if (tablePanel.activeSelf)
			{
				DeleteItems();
				ShowItems();
			}
		}

		/// <summary>
		/// 获取面包信息
		/// </summary>
		public static void GetUI(GameObject uiNode)
		{
			tablePanel = uiNode;
			menberItemSource = GenericityTool.GetObjectByPath(tablePanel, "Scroll View/Viewport/Content/itemSource");
			menberItemSource.SetActive(false);

			btnFind = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_find");
			btnClose = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_back");

			input_findClub = GenericityTool.GetComponentByPath<InputField>(tablePanel, "input_menberId");
			input_findClub.onValueChanged.AddListener(OnValueChange);
			btnClose.onClick.AddListener(CloseUI);
		}

		/// <summary>
		/// 关闭UI
		/// </summary>
		public static void CloseUI()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
		}

		/// <summary>
		/// 字符串改变
		/// </summary>
		/// <param name="text"></param>
		private static void OnValueChange(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < clubMenberList.Count; ++i)
				{
					clubMenberList[i].SetActive(true);
				}
			}
			else
			{
				for (int i = 0; i < clubMenberList.Count; ++i)
				{
					if (clubMenberList[i].bindMenber.menberId.ToString().Contains(text))
					{
						clubMenberList[i].SetActive(true);
					}
					else
					{
						clubMenberList[i].SetActive(false);
					}
				}
			}
		}

		/// <summary>
		/// 获取成员Item
		/// </summary>
		/// <returns></returns>
		public static ClubMenberShowItem GetMenberItem()
		{
			GameObject clubItem = GameObject.Instantiate(menberItemSource);
			ClubMenberShowItem clubMenberItem = new ClubMenberShowItem();
			clubMenberItem.GetUI(clubItem);
			return clubMenberItem;
		}

		/// <summary>
		/// 显示列表
		/// </summary>
		public static void ShowItems()
		{
			input_findClub.text = "";
			int selfMenberId = int.Parse(GoableData.userValiadateInfor.DatingNumber);
			List <P_Menber> menberList = new List<P_Menber>(ClubItem.clubItemState.bindGwInfo.menberList.Values);
			if (menberList == null || menberList.Count == 0)
			{
				SetActive(false);
				return;
			}
			string clubId = ClubItem.clubItemState.bindGwInfo.groupInfo.clubId;
			menberList.Sort((left, right) =>
			{
				if (right.menberId == selfMenberId)
				{
					return 1;
				}
				else if (left.menberId == selfMenberId)
				{
					return -1;
				}
				else
				{
					if (left.insertTag < right.insertTag)
					{
						return 1;
					}
					else if (left.insertTag > right.insertTag)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			});

			for (int i = 0;i < menberList.Count;++i)
			{
				P_Menber menberInfo = menberList[i];
				ClubMenberShowItem clubMenberItem = GetMenberItem();
				clubMenberList.Add(clubMenberItem);
				clubMenberItem.Show(clubId, menberInfo);
				clubMenberItem.SetParent(menberItemSource.transform.parent);
			}
		}

		/// <summary>
		/// 删除其他东西
		/// </summary>
		public static void DeleteItems()
		{
			for (int i = 0; i < clubMenberList.Count; ++i)
			{
				GameObject.Destroy(clubMenberList[i].itemNode);
			}

			clubMenberList.Clear();
		}



		/// <summary>
		/// 更新成员
		/// </summary>
		/// <param name="menberId"></param>
		public static void UpScore(int menberId)
		{
			for (int i = 0; i < clubMenberList.Count; ++i)
			{
				if (clubMenberList[i].bindMenber.menberId == menberId)
				{
					clubMenberList[i].Show(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, clubMenberList[i].bindMenber);
					if (clubMenberList[i].bindMenber.insertTag == 1)
					{
						//最前后面一个
						clubMenberList[i].SetTopLess();
					}
					else
					{
						//最后
						clubMenberList[i].SetEnd();
					}
					break;
				}
			}
		}

		/// <summary>
		/// 移除成员
		/// </summary>
		/// <param name="menberId"></param>
		public static void Remove(int menberId)
		{
			for (int i = 0; i < clubMenberList.Count; ++i)
			{
				if (clubMenberList[i].bindMenber.menberId == menberId)
				{
					GameObject.Destroy(clubMenberList[i].itemNode);
					clubMenberList.RemoveAt(i);
					break;
				}
			}
		}
	}
}