using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SingleMoba;

namespace UINameSpace
{    
    public class UISingleMobaRall : UIObject
    {
        private static UISingleMobaRall __Instance;

        public static UISingleMobaRall GetInstance()
        {
            return __Instance;
        }

        public override void SetInstance(UIObject target)
        {
            __Instance = target as UISingleMobaRall;
        }


        public Text textName;
		public Text textId;
		public Text textRoomKaCount;
		public Text textGoldCount;
		public CircleImage headIcon;
		public Text textMsg;
		public Button btnClub;
		public Button btnCreateRoom;
		public Button btnJoinRoom;
		public Button btnBuyRoomKaCount;
		public Button btnGrade;
		public Button btnShare;
		public Button btnResponse;
		public Button btnMessage;
		public Button btnHelp;
		public Button btnSetting;
		public Button btnCheckGift;
		public Button btnShopping;
		public Text lb_roundInfo;
		public Button btnBackRall;


		public GameObject animationNode_LT;
		public GameObject animationNode_CT;
		public GameObject animationNode_RT;
		public GameObject animationNode_CB;

		/// <summary>
		/// 反射调用的注册方法
		/// </summary>
		/// <param name="_className"></param>
		public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(UIDefineName.UIRall, _className);

            return 1;
        }

        public UISingleMobaRall()
        {
            assetsName = UIDefineName.UIRall;
        }

        public override void OnAwake()
        {
			GoableData.RefenceGameEven = RefenceRoomKa;
			AssetsParkManager.LoadSoundParkWithName(ConfigProject.projectFloderName, ConfigProject.soundName);
            AssetsParkManager.LoadParkWithName(ConfigProject.projectFloderName, ConfigProject.iconsName);
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
			textGoldCount = GenericityTool.GetComponentByPath<Text>(animationNode_CT, "goldCount");

			btnHelp = GenericityTool.GetComponentByPath<Button>(animationNode_RT, "btnHelp");
			btnGrade = GenericityTool.GetComponentByPath<Button>(animationNode_RT, "btnGrade");
			////////////////////////////////////////////////////////////////////CB
			btnClub = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnClub");
			btnCreateRoom = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnCreate");
			btnJoinRoom = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnJoin");
			lb_roundInfo = GenericityTool.GetComponentByPath<Text>(animationNode_CB, "MsgBar/msgMask/textMsg");
			btnShopping = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnShopping");
			btnBackRall = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnBack");
			////////////////////////////////////////////////////////////////////RT
			btnMessage = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnMsg");
			btnSetting = GenericityTool.GetComponentByPath<Button>(animationNode_RT, "btnSetting");

			/////////////////////////////////遗弃
			btnCheckGift = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnCheckGift");
			btnShare = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnShare");
			btnResponse = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnResopse");

			if (btnBuyRoomKaCount != null) btnBuyRoomKaCount.onClick.AddListener(OnOpenBuyRoomCount);
			if (btnCreateRoom != null) btnCreateRoom.onClick.AddListener(OnOpenCreateRoom);
			if (btnJoinRoom != null) btnJoinRoom.onClick.AddListener(OnOpenJoin);

			if (btnGrade != null) btnGrade.onClick.AddListener(OnOpenGrade);
			if (btnShare != null) btnShare.onClick.AddListener(OnOpenShare);
			if (btnResponse != null) btnResponse.onClick.AddListener(OnOpenResponse);
			if (btnMessage != null) btnMessage.onClick.AddListener(OnOpenMessage);
			if (btnHelp != null) btnHelp.onClick.AddListener(OnOpenHelp);
			if (btnSetting != null) btnSetting.onClick.AddListener(OnOpenSetting);
			if (btnBackRall != null) btnBackRall.onClick.AddListener(OnBackRall);

			btnCheckGift.onClick.AddListener(CheckGift);

			if (!StringConfigClass.CanOpenHiddent())
			{
				animationNode_RT.SetActive(false);
			}
		}

        public override void OnEnable()
        {
            base.OnEnable();

            Rall.ConfigProject.currentRallName = assetsName;

            GoableData.RegistLogicDataList(SingleMoba.ConfigProject.dataSpaceName, new SingleMoba.LogicDataSpace());

            InitValue();

            PlayRallBgAudio();

            ResetRoundPos();

            PlayIn(0.5f);

            GetRoundMsg();

            Rall.MessageSend.EntryGameSucess(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort);

            if (GoableData.ServerIpaddress.readyEntryRoomId != -1)
            {
                OnOpenJoin();
            }            
        }


        public static void PlayAnimationIn()
        {
            if(GetInstance() != null)
            {
                GetInstance().PlayIn(0.0f);
            }
        }

        public static void PlayAnimationOut()
        {
            if (GetInstance() != null)
            {
                GetInstance().PlayOut(0.0f);
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
            if(GoableData.DepachMsgData.depachMsgList != null && GoableData.DepachMsgData.depachMsgList.Count > 0)
            {
                lb_roundInfo.text = GoableData.DepachMsgData.depachMsgList[0];
                GoableData.DepachMsgData.depachMsgList.RemoveAt(0);
            }
            else
            {
                lb_roundInfo.text = GoableData.DepachMsgData.defaultStr;
            }
        }

        /// <summary>
        /// 消息滚动
        /// </summary>
        private void UpRoundPos()
        {
            //lb_roundInfo.transform.localPosition += Vector3.left * Time.deltaTime * speed;
            //if (lb_roundInfo.transform.localPosition.x <= -lb_roundInfo.preferredWidth - centerWidth)
            //{
            //    ///左边超出
            //    ///重头来  重新获取字符串
            //    GetRoundMsg();
            //    lb_roundInfo.transform.localPosition = new Vector3(centerWidth, 0, 0);
            //}
            //else if (lb_roundInfo.transform.localPosition.x > 200)
            //{
            //    lb_roundInfo.transform.localPosition += Vector3.left * Time.deltaTime * speed;
            //}           
        }

        /// <summary>
        /// 抽奖界面
        /// </summary>
        private void CheckGift()
        {
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRallGift_Rall, true);
        }

        /// <summary>
        /// 初始房间数据
        /// </summary>
        private void InitValue()
        {
            textName.text = GoableData.gameCoreData._roleInfor.name;

            if (GoableData.gameCoreData._roleInfor.name.Length >= 6)
            {
                textName.text = GoableData.gameCoreData._roleInfor.name.Substring(0, 5) + "..";
            }
            else
            {
                textName.text = GoableData.gameCoreData._roleInfor.name;
            }

            textId.text = "ID:" + GoableData.gameCoreData._roleInfor.roleId;

            if(GoableData.gameCoreData._roleInfor.isWeiChat == 1)
            {
                SetCircleImageForHttpbytes.SetCircleImageFromUrl(headIcon,GoableData.gameCoreData._roleInfor.headUrl);
            }
            else
            {
                if (GoableData.gameCoreData._roleInfor.sex == 1)
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
            textRoomKaCount.text = GoableData.gameCoreData._roleInfor.goldCount.ToString();
            GoableData.userValiadateInforWarp.Gold = GoableData.gameCoreData._roleInfor.goldCount;
        }


        public static void PlayRallBgAudio()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlayBackSound(Rall.ConfigProject.soundName, "audio_enterRoom", false);
        }

        /// <summary>
        /// 购买房卡
        /// </summary>
        private void OnOpenBuyRoomCount()
        {
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIBuyTip_Rall, true);            
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        private void OnOpenCreateRoom()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            MessageSend.EntryRoom(1,0);
        }

        /// <summary>
        /// 打开加入房间
        /// </summary>
        private void OnOpenJoin()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            MessageSend.EntryRoom(1,0);
		}

        /// <summary>
        /// 打开战绩
        /// </summary>
        public void OnOpenGrade()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
        }

        /// <summary>
        /// 打开分享
        /// </summary>
        public void OnOpenShare()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            string IconPath = FrameWorkDrvice.AssetsPathManagerInstance.GetExternPathNode() + "/" + "icon.jpg";
            YanlongShareStudio.WeiCharShareLink("我正在玩四川麻将", GoableData.gameCoreData._roleInfor.name + "我正在玩四川麻将，简直太好玩了，刚赢了几大百，大家一起来玩吧！" , "http://www.baidu.com", IconPath);
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
        public void OnOpenHelp()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
        }

        /// <summary>
        /// 打开设置
        /// </summary>
        public void OnOpenSetting()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGameSetting_Rall, true);
        }

        public void OnBackRall()
        {
            GoableData.reconnectIp = "";
            GoableData.ServerIpaddress.gameServerIp = "";
            GoableData.CloseHeart();
            GoableData.ServerIpaddress.isLoginGameServerSend = false;
            GoableData.ServerIpaddress.isLoginGameLogic = false;

            if (UserNetWork.HasInstance())
            {
                UserNetWork.Instance.CloseSocket();
            }

            ReleseAllGameRes();
            UIObject gameRall = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIRall_Rall);

            if (gameRall != null)
            {
                (gameRall as UIRall).RefenceRoomKa();
            }
        }

        public void ReleseAllGameRes()
        {
            GoableData.RefenceGameEven = null;

            if (GoableData.GetLogicData<SingleMoba.LogicDataSpace>(SingleMoba.ConfigProject.dataSpaceName) != null)
            {
                GoableData.GetLogicData<SingleMoba.LogicDataSpace>(SingleMoba.ConfigProject.dataSpaceName).ClearData();
            }

            AssetsParkManager.RelesePark(ConfigProject.iconsName);
            AssetsParkManager.RelesePark(ConfigProject.soundName);
            FrameWorkDrvice.UiManagerInstance.CloseUI(UIDefineName.UIRall, eCloseType.None);
            FrameWorkDrvice.UiManagerInstance.CloseUI(UIDefineName.UIFight, eCloseType.None);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            ReleseAllGameRes();
        }
    }
}
