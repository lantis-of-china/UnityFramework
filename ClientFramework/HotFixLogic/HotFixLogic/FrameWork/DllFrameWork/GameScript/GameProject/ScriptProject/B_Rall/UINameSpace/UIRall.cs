using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace UINameSpace
{
    public class UIRall : UIObject
    {
		private static UIRall __Instance;

		public static UIRall GetInstance()
		{
			return __Instance;
		}

		public override void SetInstance(UIObject target)
		{
			__Instance = target as UIRall;
		}

        public void SetActive(bool active)
        {
            objectInstance.SetActive(active);
        }

		public Text textName;
        public Text textId;
        public Text textRoomKaCount;
        public Text textGoldCount;
        public CircleImage headIcon;
        public Text textMsg;
        public Button btnCreateRoom;
        public Button btnJoinRoom;
        public Button btnBuyRoomKaCount;
        public Button btnBuyGoldCount;
        public Button btnAction;
        public Button btnGrade;
        public Button btnShare;
		public Button btnRealName;
		public Button btnAgent;
        public Button btnResponse;
        public Button btnMessage;
        public Button btnBank;
        public Button btnSetting;
        public Button btnCheckGift;
        private Button btnShopping;
        private Button btnClub;
        private Button btnService;
		public Button btnQuick;
		public Text lb_roundInfo;


        public GameObject animationNode_LT;
        public GameObject animationNode_CT;
        public GameObject animationNode_RT;
        public GameObject animationNode_CB;

        private RollViewController _rollViewController;

        //public List<GameObject> itemSourceList;
        public List<GameObject> itemListFangKa = new List<GameObject>();
		public Dictionary<string,GameObject> itemListJinBi = new Dictionary<string, GameObject>();

		/// <summary>
		/// 等待登陆游戏服务器
		/// </summary>
		public static float whatTime = 0.0f;


		public static float natTime = 0.0f;

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIRall_Rall, _className);

            return 1;
        }

        public UIRall()
        {
            assetsName = Rall.UIDefineName.UIRall_Rall;
        }

        public override void OnAwake()
        {
			//UIIMClubMain.LoadInstance();
			Rall.LogicDataSpace.Load();
			//FrameWorkDrvice.MicrophoneManagerInstance.SdkLogin(int.Parse(GoableData.userValiadateInfor.DatingNumber));
            animationNode_LT = GenericityTool.GetObjectByPath(objectInstance, "anchorLT/animationNode");
            animationNode_CT = GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode");
            animationNode_RT = GenericityTool.GetObjectByPath(objectInstance, "anchorRT/animationNode");
            animationNode_CB = GenericityTool.GetObjectByPath(objectInstance, "anchorCB/animationNode");

            /////////////////////////////////////////////////////////////////////LT
            textName = GenericityTool.GetComponentByPath<Text>(animationNode_LT, "HeadItem/textName");
            textId = GenericityTool.GetComponentByPath<Text>(animationNode_LT, "HeadItem/textId");
            headIcon = GenericityTool.GetComponentByPath<CircleImage>(animationNode_LT, "HeadItem/HeadNode/HeadIcon");

            btnBuyRoomKaCount = GenericityTool.GetComponentByPath<Button>(animationNode_CT, "btnBuy");
            textRoomKaCount = GenericityTool.GetComponentByPath<Text>(animationNode_CT, "textCount");

            btnBuyGoldCount = GenericityTool.GetComponentByPath<Button>(animationNode_CT, "btnBuyGold");
            textGoldCount = GenericityTool.GetComponentByPath<Text>(animationNode_CT, "textGoldCount");

            ////////////////////////////////////////////////////////////////////CB
            btnCreateRoom = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnCreate");
            btnJoinRoom = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnJoin");
            lb_roundInfo = GenericityTool.GetComponentByPath<Text>(animationNode_CB, "MsgBar/msgMask/textMsg");
            ////////////////////////////////////////////////////////////////////RT
            btnMessage = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnMsg");
            btnBank = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnBank");
            btnSetting = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnSetting");
			btnQuick = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnQuick");
			btnGrade = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnGrade");
            btnAction = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnAction");
            btnShopping = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnShopping");
            btnClub = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "panelList/panel_0/GamesRoot/Games/Club");
            btnService = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btn_Service");
			btnShare = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnShare");
			btnRealName = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnRealName");
			btnAgent = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnAgent"); 
			/////////////////////////////////遗弃
			btnCheckGift = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnCheckGift");
            btnResponse = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnResopse");
            if (btnBuyRoomKaCount != null) btnBuyRoomKaCount.onClick.AddListener(OnOpenBuyRoomCount);
            if (btnBuyGoldCount != null) btnBuyGoldCount.onClick.AddListener(OnOpenBuyRoomCount);            

            if (btnCreateRoom != null) btnCreateRoom.onClick.AddListener(OnOpenCreateRoom);
            if (btnJoinRoom != null) btnJoinRoom.onClick.AddListener(OnOpenJoin);

            if (btnAction != null) btnAction.onClick.AddListener(OnOpenAction);
            if (btnGrade != null) btnGrade.onClick.AddListener(OnOpenGrade);
            if (btnShare != null) btnShare.onClick.AddListener(OnOpenShare);
			btnRealName.onClick.AddListener(OnClickRealName);
			btnAgent.onClick.AddListener(OnClickAgent);
			if (btnResponse != null) btnResponse.onClick.AddListener(OnOpenResponse);
            if (btnMessage != null) btnMessage.onClick.AddListener(OnOpenMessage);
            if (btnBank != null) btnBank.onClick.AddListener(OnOpenBank);
            if (btnSetting != null) btnSetting.onClick.AddListener(OnOpenSetting);
            if (btnShopping != null) btnShopping.onClick.AddListener(OnShopping);
            if (btnClub != null) btnClub.onClick.AddListener(OpenClub);
            btnService.onClick.AddListener(OnClickService);
			btnQuick.onClick.AddListener(OnClickQuick);

            GenericityTool.GetObjectByPath(animationNode_CB, "tableList").gameObject.SetActive(true);
            btnBank.gameObject.SetActive(false);
            btnGrade.gameObject.SetActive(false);
            btnCheckGift.onClick.AddListener(CheckGift);


            if (!StringConfigClass.CanOpenHiddent())
            {
                animationNode_RT.SetActive(false);
            }

            if (!GameMain.Instance.openHall)
            {
                SetSizeDelta(Vector2.zero);
            }

			itemListJinBi.Add("LaoHuJiServer", GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_1/GamesRoot/node_1"));
			itemListJinBi.Add("BingShangQuGunQiuServer", GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_1/GamesRoot/node_2"));
			itemListJinBi.Add("WuXingJingCaiServer", GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_1/GamesRoot/node_3"));
			itemListJinBi.Add("BuyuServer", GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_1/GamesRoot/node_4"));
			itemListJinBi.Add("SingleMoba", GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_1/GamesRoot/node_5"));
			itemListJinBi.Add("AskDaoServer", GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_1/GamesRoot/node_6"));

			Transform gamesParent = GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_0/GamesRoot/Games").transform;
            for (int i = 0; i < gamesParent.childCount; ++i)
			{
                Transform itemTran = gamesParent.Find("Game_" + (i + 1));
                if (itemTran == null)
				{
                    continue;
                }
                if (i >= GoableData.ServerIpaddress.mServerData.Count)
				{
                    itemTran.gameObject.SetActive(false);
                    continue;
                }
				itemListFangKa.Add(itemTran.gameObject);
            }

			int mode_0_index = 0;
            for (int i = 0; i < GoableData.ServerIpaddress.mServerData.Count;++i)
            {
				Server.GolabServerInfor golabServer = GoableData.ServerIpaddress.mServerData[i];

                //开始登陆服务器
                string gameServerIp = GoableData.ServerIpaddress.mServerData[i].ServerIp;
                int gameServerPort = GoableData.ServerIpaddress.mServerData[i].UdpServerPort;
                string id = GoableData.ServerIpaddress.mServerData[i].ServerId;
				DebugLoger.Log("id:" + id + " mode:"+ golabServer.gameMode);
				Button btnCallGame = null;
				if (golabServer.gameMode == 0)
				{
					//房卡
					if (mode_0_index >= itemListFangKa.Count)
					{
						GameObject cloneResource = itemListFangKa[0];
						GameObject cloneObject = GameObject.Instantiate(cloneResource);
						itemListFangKa.Add(cloneObject);
						cloneObject.transform.SetParent(cloneResource.transform.parent);
						cloneObject.transform.localScale = Vector3.one;
					}
					GameObject itemNode = itemListFangKa[mode_0_index];

					Image iconImage = itemNode.GetComponent<Image>();
					AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName, iconImage, string.Format("gameBack_{0}", id));

					btnCallGame = itemNode.GetComponent<Button>();					

					mode_0_index++;
				}
				else
				{
					//金币
					if (itemListJinBi.ContainsKey(id))
					{
						GameObject itemNode = itemListJinBi[id];
						//GameObject objItem = GenericityTool.GetObjectByPath(itemNode, "ImageIcon");
						//btnCallGame = objItem.AddComponent<Button>();
						btnCallGame = itemNode.GetComponent<Button>();
					}
				}

				if (btnCallGame != null)
				{
					btnCallGame.onClick.RemoveAllListeners();
					btnCallGame.onClick.AddListener(delegate
					{
						FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
						if (!string.IsNullOrEmpty(Rall.ConfigProject.currentRallName))
						{
							FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.ConfigProject.currentRallName, eCloseType.None);
						}

						if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
						{
							FrameWorkDrvice.UiManagerInstance.CloseUI(GoableData.reconnectExternUIName, eCloseType.None);
							GoableData.reconnectExternUIName = "";

							DebugLoger.Log("------------------------------------这里关闭了子游戏 游戏UI");
						}
						DebugLoger.Log("------------------------------------rall awake");
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
						CheckCanOpen(id);
					});
				}
			}

			InitGameRollView();
			InitGamesArrow();



			for (int i = 0; i < 2; i++)
			{
				string btnPath = "";
				string panelPath = "";
				TablePanelItem storePanel = null;
				if (i == 0)
				{
					storePanel = new Rall.Mode_0_Item();
					btnPath = "tableList/table_0";
					panelPath = "panelList/panel_0";
				}
				else if (i == 1)
				{
					storePanel = new Rall.Mode_0_Item();
					btnPath = "tableList/table_1";
					panelPath = "panelList/panel_1";
				}

				storePanel.tag = "ModePanel";
				storePanel.index = i;
				storePanel.tableButton = GenericityTool.GetComponentByPath<Button>(animationNode_CB, btnPath);
				storePanel.tablePanel = GenericityTool.GetObjectByPath(animationNode_CB, panelPath);
				storePanel.RegistListen();
			}
			TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("ModePanel");
			firstTablePanel.SelectPanel();
		}

		public static void CheckCanOpen(string id)
        {
            UINameSpace.UIWaitting.AddShowWaitting(NetMessageType.LoginWithUser_CheckLock_Back.ToString());
			Rall.MessageSend.CheckLock(id);
        }

        public void OpenGame(string id)
        {
			DebugLoger.Log("login game id " + id);
            Server.GolabServerInfor loginServer = null;
            for (int i = 0; i < GoableData.ServerIpaddress.mServerData.Count; ++i)
            {
                Server.GolabServerInfor curServerInfo = GoableData.ServerIpaddress.mServerData[i];

                if(curServerInfo.ServerId == id)
                {
                    loginServer = curServerInfo;

                    break;
                }
            }

            if (loginServer != null)
            {
				DebugLoger.Log("loginServer not null");
				UserNetWork.openExpecation = false;
                if (UserNetWork.HasInstance())
                {
                    UserNetWork.Instance.CloseSocket();
                }
                //开始登陆服务器
                GoableData.ServerIpaddress.gameServerIp = loginServer.ServerIp;
                GoableData.ServerIpaddress.gameServerPort = loginServer.UdpServerPort;

                GoableData.reconnectIp = GoableData.ServerIpaddress.gameServerIp;
                GoableData.reconnectPort = GoableData.ServerIpaddress.gameServerPort;
            }
            else
            {		
				UINameSpace.UITipMessage.PlayMessage("找不到指定游戏服务器!");
            }
        }
        
        public override void OnEnable()
        {
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UILogin_Rall, eCloseType.Queue);

			instance = this;
            
            base.OnEnable();

			InitValue();

            PlayRallBgAudio();

            ResetRoundPos();

            PlayIn(0.5f);

            GetRoundMsg();

			UpNat(true);

			Rall.MessageSend.GetMsgRecive();
			Rall.MessageSend.GetMsgSystem();

            //objectInstance.transform.localPosition = new Vector3(100000, 100000, 0);

            if (!GameMain.Instance.openHall)
            {
                OpenGame("SingleMoba");
            }

			DebugLoger.Log("GoableData.userValiadateInforWarp.serverId " + GoableData.userValiadateInforWarp.serverId);
			DebugLoger.Log("GoableData.reconnectIp " + GoableData.reconnectIp);
			//这里进行游戏重连
			if (!string.IsNullOrEmpty(GoableData.reconnectIp))
			{
				GoableData.ServerIpaddress.gameServerIp = GoableData.reconnectIp;
				GoableData.ServerIpaddress.gameServerPort = GoableData.reconnectPort;

				//这里看是否再游戏中 进行重连游戏
				if (!string.IsNullOrEmpty(GoableData.userValiadateInforWarp.serverId))
				{
					//OpenGame(GoableData.userValiadateInforWarp.serverId);

					if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
					{
						DebugLoger.Log("------------------------------------这里准备子游戏 游戏UI 层级 ----");
						UIObject uiGet = FrameWorkDrvice.UiManagerInstance.GetUI(GoableData.reconnectExternUIName);
						if (uiGet != null)
						{
							uiGet.ToBestLayer();
							DebugLoger.Log("------------------------------------这里提升了子游戏 游戏UI 层级 ---");
						}
					}
				}
			}
			else
			{
				//这里看是否再游戏中 进行重连游戏
				if (!string.IsNullOrEmpty(GoableData.userValiadateInforWarp.serverId))
				{
					OpenGame(GoableData.userValiadateInforWarp.serverId);

					if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
					{
						DebugLoger.Log("------------------------------------这里准备子游戏 游戏UI 层级");
						UIObject uiGet = FrameWorkDrvice.UiManagerInstance.GetUI(GoableData.reconnectExternUIName);
						if (uiGet != null)
						{
							uiGet.ToBestLayer();
							DebugLoger.Log("------------------------------------这里提升了子游戏 游戏UI 层级");
						}
					}
				}
				else
				{

					//公告
					if (!UIGameRallShow.isOpen)
					{
						FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGameRallShow_Rall, true);
					}
				}
			}
		}


		public void UpNat(bool seek)
		{
			bool send = false;
			if (seek)
			{
				send = true;
			}
			else
			{
				natTime += Time.unscaledDeltaTime;
				if (natTime > 30.0f)
				{
					send = true;
				}
			}

			if (send)
			{
				Rall.MessageSend.NAT();
				natTime = 0.0f;
			}
		}


        /// <summary>
        /// 设置进入服务器 设置进入房间号
        /// </summary>
        /// <param name="serverType"></param>
        public static void SetEntryServer(int serverType)
        {

        }
        
        private static UIRall instance;

        public static void PlayAnimationIn()
        {
            if (instance != null)
            {
                instance.PlayIn(0.0f);
            }
        }

        public static void PlayAnimationOut()
        {
            if (instance != null)
            {
                instance.PlayOut(0.0f);
            }
        }
        
        /// <summary>
        /// 播放进入动画
        /// </summary>
        public void PlayIn(float waitTime)
        {
            UINameSpace.UIWaitting.openOutTime = true;
            UINameSpace.UIWaitting.ClearAll();
            CherishTweenMove.Begin(animationNode_LT, new Vector3(-550, 0, 0), Vector3.zero, 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_RT, new Vector3(598, 0, 0), Vector3.zero, 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_CT, new Vector3(0, 300, 0), Vector3.zero, 0.3f, waitTime, true);

            CherishTweenMove.Begin(animationNode_CB, new Vector3(0, -1000, 0), Vector3.zero, 0.3f, waitTime, true);
        }
        
        /// <summary>
        /// 播放弹出动画
        /// </summary>
        public void PlayOut(float waitTime)
        {
            CherishTweenMove.Begin(animationNode_LT, animationNode_LT.transform.localPosition, new Vector3(-550, 0, 0), 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_RT, animationNode_RT.transform.localPosition, new Vector3(598, 0, 0), 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_CT, animationNode_CT.transform.localPosition, new Vector3(0, 300, 0), 0.3f, waitTime, true);

            CherishTweenMove.Begin(animationNode_CB, animationNode_CB.transform.localPosition, new Vector3(0, -1000, 0), 0.3f, waitTime, true);
        }

        public override void OnUpdate()
        {
            UpRoundPos();

            UpdateGoGame();

			UpNat(false);
        }


        /// <summary>
        /// 登陆游戏服务器
        /// </summary>
        public void UpdateGoGame()
        {			
            if (!GoableData.ServerIpaddress.isLoginGameLogic)
            {
                LoginGameServer();

                if (GoableData.ServerIpaddress.isLoginLogServerSend && GoableData.ServerIpaddress.isLoginGameServerSend)
                {
                    if (whatTime <= 0)
                    {
                        GoableData.ServerIpaddress.ClearnData();
                        UINameSpace.UITipMessage.PlayMessage("连接游戏服务器超时,请检查是否连接网络!");
                        FrameWorkDrvice.UiManagerInstance.CloseAllUI();
                        //无响应 退出到登录界面 需要重连
                        FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
                    }

                    whatTime -= Time.deltaTime;
                }
            }
        }

        /// <summary>
        /// 进入游戏服务器
        /// </summary>
        public void LoginGameServer()
        {            
            {
                if (!string.IsNullOrEmpty(GoableData.ServerIpaddress.gameServerIp))
                {
					DebugLoger.Log("login game --------------------------------------0");
					UserNetWork unw = UserNetWork.Instance;
                    if (UINameSpace.UIWaitting.codeList.Count == 0)
                    {
                        UINameSpace.UIWaitting.AddShowWaitting(((int)NetMessageType.ChatWithUser_Login_Back).ToString());
                    }

					DebugLoger.Log("login game --------------------------------------  " + GoableData.ServerIpaddress.isLoginLogServerSend + " " + GoableData.ServerIpaddress.isLoginGameServerSend);

					if (GoableData.ServerIpaddress.isLoginLogServerSend && !GoableData.ServerIpaddress.isLoginGameServerSend && UserNetWork.Instance.isConnect)
                    {
                        //登录服务器已经登录 游戏服务器没有登录
                        SetWaitLoginGameLogic();
                        GoableData.ServerIpaddress.isLoginGameServerSend = true;
						DebugLoger.Log("login game --------------------------------------Id:" + GoableData.ServerIpaddress.gameServerIp + " Port" + GoableData.ServerIpaddress.gameServerPort);
						Rall.MessageSend.LoginGame(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort);
                    }
                }
            }
        }
        
        /// <summary>
        /// 登陆游戏逻辑服务器 超时时间
        /// </summary>
        public void SetWaitLoginGameLogic()
        {
            whatTime = 10.0f;
        }
        
        /// <summary>
        /// 蒙板宽度
        /// </summary>
        public float centerWidth = 639.93f;
        public float speed = 100.0f;

        /// <summary>
        /// 重置滚动条
        /// </summary>
        private void ResetRoundPos()
        {
            lb_roundInfo.transform.localPosition = new Vector3(centerWidth, 0, 0);
        }

        /// <summary>
        /// 获取滚动条消息
        /// </summary>
        private void GetRoundMsg()
        {
            if (!StringConfigClass.CanOpenHiddent())
            {
                lb_roundInfo.text = "游戏初次上架，欢迎各位玩家在我们的游戏中进行娱乐活动，游戏中拒绝一切赌博行为。请大家健康游戏，共同维护网络和谐！";
                return;
            }

			lb_roundInfo.text = GoableData.DepachMsgData.GetNextMsg();
        }

        /// <summary>
        /// 消息滚动
        /// </summary>
        private void UpRoundPos()
        {
            lb_roundInfo.transform.localPosition += Vector3.left * Time.deltaTime * speed;
            if (lb_roundInfo.transform.localPosition.x <= -lb_roundInfo.preferredWidth - centerWidth)
            {
                ///左边超出
                ///重头来  重新获取字符串
                GetRoundMsg();
                lb_roundInfo.transform.localPosition = new Vector3(centerWidth, 0, 0);
            }
            else if (lb_roundInfo.transform.localPosition.x > 200)
            {
                lb_roundInfo.transform.localPosition += Vector3.left * Time.deltaTime * speed;
            }
        }

        /// <summary>
        /// 抽奖界面
        /// </summary>
        private void CheckGift()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRallGift_Rall, true);
        }

        /// <summary>
        /// 初始房间数据
        /// </summary>
        private void InitValue()
        {
            textName.text = GoableData.userValiadateInforWarp.PikeName;
            //if (textName.preferredWidth > 270)
            if (GoableData.userValiadateInforWarp.PikeName.Length >= 6)
            {
                textName.text = GoableData.userValiadateInforWarp.PikeName.Substring(0, 5) + "..";
            }
            else
            {
                textName.text = GoableData.userValiadateInforWarp.PikeName;
            }

            textId.text = "ID:" + GoableData.userValiadateInfor.DatingNumber;

            if (GoableData.userValiadateInforWarp.isWeiChat == 1)
            {
                SetCircleImageForHttpbytes.SetCircleImageFromUrl(headIcon, GoableData.userValiadateInforWarp.headUrl);
            }
            else
            {
                if (GoableData.userValiadateInforWarp.Sex == 1)
                {
                    ///男
                    AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, headIcon, "GameEnd10");
                }
                else
                {
                    ///女
                    AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, headIcon, "GameEnd9");
                }
            }

            RefenceRoomKa();
        }

        //刷新房卡
        public void RefenceRoomKa()
        {
            textRoomKaCount.text = GoableData.userValiadateInforWarp.RechargeCount.ToString();
            textGoldCount.text = GoableData.userValiadateInforWarp.Gold.ToString();
            SetActive(true);
        }
        
        /// <summary>
        /// 播放大厅音效
        /// </summary>
        public static void PlayRallBgAudio()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlayBackSound(Rall.ConfigProject.soundName, "audio_enterRoom", true);
        }

        /// <summary>
        /// 购买房卡
        /// </summary>
        private void OnOpenBuyRoomCount()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIBuyTip_Rall, true);
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        private void OnOpenCreateRoom()
        {
            //FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            //if (MaJiangPlayer_QuanZhou.roomId != -1)
            //{
            //    MessageSend.JoinMajiangRoom(MaJiangPlayer_QuanZhou.roomId);
            //}
            //else if (MaJiangPlayer.roomId != -1)
            //{
            //    MessageSend.JoinMajiangRoom(MaJiangPlayer.roomId);
            //}
            //else
            //{
            //    FrameWorkDrvice.UiManagerInstance.OpenUI(MaJiang_QuanZhou.ConfigProject.projectFloderName, MaJiang_QuanZhou.UIDefineName.UICreateMajiangRoom_QuanZhou, true);
            //}
        }

        /// <summary>
        /// 打开加入房间
        /// </summary>
        private void OnOpenJoin()
        {
            //FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            //if (MaJiangPlayer_QuanZhou.roomId != -1)
            //{
            //    MessageSend.JoinMajiangRoom(MaJiangPlayer_QuanZhou.roomId);
            //}
            //else if (MaJiangPlayer.roomId != -1)
            //{
            //    MessageSend.JoinMajiangRoom(MaJiangPlayer.roomId);
            //}
            //else
            //{
            //    FrameWorkDrvice.UiManagerInstance.OpenUI(MaJiang_QuanZhou.ConfigProject.projectFloderName, MaJiang_QuanZhou.UIDefineName.UIJoinMajiangRoom_QuanZhou, true);
            //}
        }

        /// <summary>
        /// 活动按钮
        /// </summary>
        public void OnOpenAction()
        {
            DebugLoger.Log("Open Action");
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            UINameSpace.UITipMessage.PlayMessage("活动功能暂未开放！");
            //FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIAction_Rall, true);
        }

        /// <summary>
        /// 打开战绩
        /// </summary>
        public void OnOpenGrade()
        {
            DebugLoger.Log("Open Grade");
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRankList_Rall, true);
        }

        /// <summary>
        /// 打开分享
        /// </summary>
        public void OnOpenShare()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            
            string IconPath = FrameWorkDrvice.AssetsPathManagerInstance.GetExternPathNode() + "/" + "icon.jpg";
            YanlongShareStudio.WeiCharShareLink("众享指尖", GoableData.userValiadateInforWarp.PikeName + ":我正在玩众享指尖，简直太好玩了，大家一起来玩吧！", StringConfigClass.GetDownloadUrl(), IconPath);
        }

		/// <summary>
		/// 实名认证
		/// </summary>
		public void OnClickRealName()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (string.IsNullOrEmpty(GoableData.userValiadateInforWarp.realName))
			{
				FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
				FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRealName_Rall, true);
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("已经完成实名认证!");
			}
		}

		/// <summary>
		/// 点击代理
		/// </summary>
		public void OnClickAgent()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIAgent_Rall, true);
		}

		/// <summary>
		/// 打开反馈
		/// </summary>
		public void OnOpenResponse()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
        }

        /// <summary>
        /// 打开消息
        /// </summary>
        public void OnOpenMessage()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIShowTipMsg_Rall, true);
        }

        /// <summary>
        /// 打开帮助
        /// </summary>
        public void OnOpenBank()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIBank_Rall, true);
        }

        /// <summary>
        /// 打开设置
        /// </summary>
        public void OnOpenSetting()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            UIGameSetting.hiddent = true;
			UIGameSetting.showLoginout = true;
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGameSetting_Rall, true);
        }

		/// <summary>
		/// 释放
		/// </summary>
		public override void OnDispose()
		{
			Rall.LogicDataSpace.Clear();
		}

        private void InitGameRollView() {
            GameObject gamesGo = GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_0/GamesRoot/Games");
            _rollViewController = gamesGo.AddComponent<RollViewController>();
            _rollViewController.Init();
        }

        private void InitGamesArrow() {
            Button btn = GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_0/Btn_LeftArrow/Button").GetComponent<Button>();
            btn.onClick.AddListener(() => { RollGamesView(EDragDirection.RightToLeft); });

            btn = GenericityTool.GetObjectByPath(animationNode_CB, "panelList/panel_0/Btn_RightArrow/Button").GetComponent<Button>();
            btn.onClick.AddListener(() => { RollGamesView(EDragDirection.LeftToRight); });
        }

        private void RollGamesView(EDragDirection direction) {
            if (_rollViewController == null) {
                return;
            }
            _rollViewController.Roll(direction);
        }
        private void OnShopping() {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIStore_Rall, true);
        }

        public void OpenClub()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			OpenClubFun();
		}

		public void OpenClubFun()
		{
			FrameWorkDrvice.UiManagerInstance.OpenUI(IMClub.ConfigProject.projectFloderName, IMClub.UIDefineName.UIMain_IMClub, true);

			TablePanelItem roomTablePanel = TablePanelItem.GetFirstTablePanelWithTag("ClubFunPanel");
			if (roomTablePanel != null)
			{
				if (IMClub.ClubItem.clubItemState != null && IMClub.ClubItem.clubItemState.bindGwInfo != null)
				{
					IMClub.ClubItem.clubItemState.bindGwInfo.clubGradeList = null;
				}
				roomTablePanel.SelectPanel();
			}
			//if (IMClub.IMCludWarp.InitFinish())
			//{
			//}
			//else
			//{
			//	UINameSpace.UITipMessage.PlayMessage("亲友圈初始化中,请稍后再试!");
			//}
		}

        private void OnClickService()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIBuyTip_Rall,true);
        }

		private void OnClickQuick()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIQuickJoin_Rall, true);
		}
	}
}
