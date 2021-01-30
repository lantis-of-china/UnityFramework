using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈房间实例
    /// </summary>
    public class ClubGameRoomItem
    {
        public GameObject itemNode;
        /// <summary>
        /// 进入房间
        /// </summary>
        public Button btn_entry;
		/// <summary>
		/// 游戏名
		/// </summary>
		public Text txt_gameName;
        /// <summary>
        /// 房间ID
        /// </summary>
        public Text txt_roomId;
        /// <summary>
        /// 玩家数量
        /// </summary>
        public Text txt_playerCount;
        /// <summary>
        /// 局数
        /// </summary>
        public Text txt_times;
        /// <summary>
        /// 状态
        /// </summary>
        public Text txt_state;
		/// <summary>
		/// 规则
		/// </summary>
		public Text txt_rule;
		/// <summary>
		/// 游戏图标
		/// </summary>
		public Image img_icon;

		/// <summary>
		/// 绑定的房间信息
		/// </summary>
		public P_RoomInfo bindRoomInfo;

        /// <summary>
        /// 获取UI
        /// </summary>
        public void GetUI(GameObject node)
        {
            itemNode = node;
			txt_gameName = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_gameName");
			txt_roomId = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_roomId");
            txt_playerCount = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_playerCount");
            txt_times = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_times");
            txt_state = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_state");
            btn_entry = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_entry");
			txt_rule = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_rule");
			img_icon = GenericityTool.GetComponentByPath<Image>(itemNode, "img_icon");

			btn_entry.onClick.AddListener(OnClickEntry);
        }

        /// <summary>
        /// 进入游戏
        /// </summary>
        private void OnClickEntry()
        {
			//FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			DebugLoger.Log(string.Format("进入房间 {0}", bindRoomInfo.roomId));
			//bindRoomInfo.gameType
			GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(bindRoomInfo.clubId);

			if (gw == null)
			{
				UINameSpace.UITipMessage.PlayMessage("找不到亲友圈数据!");
				return;
			}

			if (!gw.menberList.ContainsKey(int.Parse(GoableData.userValiadateInfor.DatingNumber)))
			{
				UINameSpace.UITipMessage.PlayMessage("找不到成员数据!");
				return;
			}

			P_Menber menber = gw.menberList[int.Parse(GoableData.userValiadateInfor.DatingNumber)];

			if (gw.groupInfo.clubSetting.scoreLimit != 0 || gw.groupInfo.clubSetting.collectScore != 0 || gw.groupInfo.clubSetting.collectScale != 0)
			{
				if (gw.groupInfo.clubSetting.scoreLimit > menber.Score)
				{
					UINameSpace.UITipMessage.PlayMessage("无权进入房间,请联系群主!");
					return;
				}
			}

            if (menber.black == 1)
            {
                UINameSpace.UITipMessage.PlayMessage("当前处于黑名单中,无法加入房间!");
                return;
            }

			if (!string.IsNullOrEmpty(Rall.ConfigProject.currentRallName))
			{				
				FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.ConfigProject.currentRallName,eCloseType.None);
			}

			if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
			{
				FrameWorkDrvice.UiManagerInstance.CloseUI(GoableData.reconnectExternUIName, eCloseType.None);
				GoableData.reconnectExternUIName = "";
			}
			GoableData.SetReconnectDisable();
			GoableData.reconnectIp = "";
			GoableData.ServerIpaddress.gameServerIp = "";
			GoableData.CloseHeart();
			GoableData.ServerIpaddress.isLoginGameServerSend = false;
			GoableData.ServerIpaddress.isLoginGameLogic = false;
			GoableData.ServerIpaddress.readyEntryRoomId = -1;
			GoableData.ServerIpaddress.clubId = "";
			if (UserNetWork.HasInstance())
			{
				UserNetWork.Instance.CloseSocket(Rall.MessageSend.LoginOut);
			}


			GameEntryItem gameEntryItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntry(bindRoomInfo.serverId);
			if (gameEntryItem != null)
			{
				GoableData.reconnectExternUIName = gameEntryItem.uiName;
			}

			GoableData.ServerIpaddress.readyEntryRoomId = bindRoomInfo.roomId;
			GoableData.ServerIpaddress.clubId = bindRoomInfo.clubId;

			UINameSpace.UIRall.CheckCanOpen(bindRoomInfo.serverId);
			//UINameSpace.UICheXuanJoinRoom.JoinRoom(bindRoomInfo.roomId);
        }

        /// <summary>
        /// 设置节点激活
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        public void SetParent(Transform parent)
        {
            itemNode.transform.parent = parent;
            itemNode.transform.localScale = Vector3.one;
            itemNode.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="bindRoomInfo"></param>
        public void ShowInfo(P_RoomInfo roomInfo)
        {
			GameEntryItem gameEntryItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntry(roomInfo.serverId);
            bindRoomInfo = roomInfo;

			txt_gameName.text = gameEntryItem.gameName;
			txt_roomId.text = bindRoomInfo.roomId.ToString();
            txt_playerCount.text = bindRoomInfo.curCount + "/" + bindRoomInfo.maxCount;
            txt_times.text = bindRoomInfo.curTimes + "/" + bindRoomInfo.toldTimes;
            txt_state.text = bindRoomInfo.state == 0 ? "等待中" : "正在游戏";
			P_ClubSetting curClubSetting = ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting;
			P_GameSetting curGameSetting = null;
			for (int i = 0; i < curClubSetting.gamesSetting.Count; ++i)
			{
				if (curClubSetting.gamesSetting[i].gameType == roomInfo.gameType)
				{
					curGameSetting = curClubSetting.gamesSetting[i];
					break;
				}
			}
			txt_rule.text = curGameSetting.roomValue + "局" + gameEntryItem.callGetParmarsStr(curGameSetting.pamarasSetting);
			AssetsParkManager.SetUguiImageThingIcon(IMClub.ConfigProject.iconsName, img_icon, "icon_game_" + roomInfo.gameType);
			SetActive(true);
        }
    }

    /// <summary>
    /// 亲友圈房间列表
    /// </summary>
    public class ClubRoomPanel_Select : TablePanelItem
    {
        public static ClubRoomPanel_Select Instance;
        /// <summary>
        /// 亲友圈聊天面板
        /// </summary>
        public static ClubRoomPanel_Select clubRoomPanel;
        /// <summary>
        /// 房间列表资源
        /// </summary>
        public GameObject itemSource;
        /// <summary>
        /// 房间列表
        /// </summary>
        public List<ClubGameRoomItem> clubRoomList = new List<ClubGameRoomItem>();
        /// <summary>
        /// 创建房间
        /// </summary>
        public Button btn_create;

        /// <summary>
        /// 获取面包信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
            Instance = this;

            clubRoomPanel = this;
            itemSource = GenericityTool.GetObjectByPath(tablePanel, "roomListPanel/Viewport/Content/itemNode");
            itemSource.SetActive(false);
            btn_create = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_create");
            btn_create.onClick.AddListener(OnClickCreateRoom);
            CreateRoomSelectPanel.GetUI(GenericityTool.GetObjectByPath(tablePanel, "createRoomList"));
            CreateRoomSelectPanel.SetActive(false);
        }

        /// <summary>
        /// 选中了这个Item
        /// </summary>
        public override void OnSelect()
        {
            base.OnSelect();
            ResetShow();
        }

        /// <summary>
        /// 重置显示
        /// </summary>
        public void ResetShow()
        {
            DeleteAll();
            UpAllShow();
        }

        /// <summary>
        /// 点击按钮事件
        /// </summary>
        public void OnClickCreateRoom()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			CreateRoomSelectPanel.ShowInfoItems(ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting.gamesSetting);
        }

        /// <summary>
        /// 刷新所有的信息
        /// </summary>
        public void UpAllShowInfoExtern()
        {
            DeleteAll();
            UpAllShow();
        }

        /// <summary>
        /// 显示所有的
        /// </summary>
        public void UpAllShow()
        {
			DebugLoger.Log("UpAllShow");

			List<P_RoomInfo> roomList = RoomManager.GetClubRoomInfoList(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId);
			
			if (roomList != null)
            {
				//roomList.Sort((leftItem, rightItem) => 
				//{
				//	if(leftItem.state)
				//});

				DebugLoger.Log("roomList count " + roomList.Count);
				for (int i = 0; i < roomList.Count; ++i)
                {
                    P_RoomInfo roomInfo = roomList[i];
                    GameObject item = GameObject.Instantiate(itemSource);
                    ClubGameRoomItem clubGameRoomItem = new ClubGameRoomItem();
                    clubGameRoomItem.GetUI(item);
                    clubGameRoomItem.SetParent(itemSource.transform.parent);
                    clubGameRoomItem.ShowInfo(roomInfo);
                    clubRoomList.Add(clubGameRoomItem);
                }
            }
            else
            {
				DebugLoger.Log("not roomList");
			}
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        public void DeleteAll()
        {
			DebugLoger.Log("DeleteAllRoom");

			for (int i = 0;i < clubRoomList.Count;++i)
            {
                GameObject.Destroy(clubRoomList[i].itemNode);
            }
            clubRoomList.Clear();
        }
    
        /// <summary>
        /// 用户加入
        /// </summary>
        /// <param name="clubId"></param>
        public void UpRoomInfo(int roomId)
        {
            for (int i = 0; i < clubRoomList.Count; ++i)
            {
                DebugLoger.Log("clubRoomList[i].bindRoomInfo room id + " + clubRoomList[i].bindRoomInfo.roomId);
                if (clubRoomList[i].bindRoomInfo.roomId == roomId)
                {
                    DebugLoger.Log("clubRoomList[i].bindRoomInfo 3 + " + clubRoomList[i].bindRoomInfo.curCount);
                    clubRoomList[i].ShowInfo(clubRoomList[i].bindRoomInfo);
                }
            }
        }

        /// <summary>
        /// 房间ID
        /// </summary>
        /// <param name="roomId"></param>
        public void UnReleseRoom(int roomId)
        {
            for (int i = 0; i < clubRoomList.Count; ++i)
            {
                if (clubRoomList[i].bindRoomInfo.roomId == roomId)
                {
                    GameObject.Destroy(clubRoomList[i].itemNode);
                    clubRoomList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}


