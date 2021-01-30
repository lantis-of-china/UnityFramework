using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace UINameSpace
{    
    public class UIGeneralRall : UIObject
    {
        public static GameEntryItem curEntryItem;

        public Text textName;
        public Text textId;
        public Text textRoomKaCount;
        public Image headIcon;
        public Text textMsg;
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
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIGeneralRall_Rall, _className);

            return 1;
        }

        public UIGeneralRall()
        {
            assetsName = Rall.UIDefineName.UIGeneralRall_Rall;
        }

        public override void OnAwake()
        {
            animationNode_LT = GenericityTool.GetObjectByPath(objectInstance, "anchorLT/animationNode");
            animationNode_CT = GenericityTool.GetObjectByPath(objectInstance, "anchorCT/animationNode");
            animationNode_RT = GenericityTool.GetObjectByPath(objectInstance, "anchorRT/animationNode");
            animationNode_CB = GenericityTool.GetObjectByPath(objectInstance, "anchorCB/animationNode");

            /////////////////////////////////////////////////////////////////////LT
            textName = GenericityTool.GetComponentByPath<Text>(animationNode_LT, "HeadItem/textName");
            textId = GenericityTool.GetComponentByPath<Text>(animationNode_LT, "HeadItem/textId");            
            headIcon = GenericityTool.GetComponentByPath<Image>(animationNode_LT, "HeadItem/HeadNode/HeadIcon");

            btnBuyRoomKaCount = GenericityTool.GetComponentByPath<Button>(animationNode_RT, "btnBuy");
            textRoomKaCount = GenericityTool.GetComponentByPath<Text>(animationNode_RT, "textCount");
            ////////////////////////////////////////////////////////////////////CB
            btnCreateRoom = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnCreate");
            btnJoinRoom = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnJoin");
            lb_roundInfo = GenericityTool.GetComponentByPath<Text>(animationNode_CB, "MsgBar/msgMask/textMsg");
            ////////////////////////////////////////////////////////////////////RT
            btnMessage = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnMsg");
            btnHelp = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnHelp");
            btnSetting = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnSetting");
            btnGrade = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnGrade");
            btnBackRall = GenericityTool.GetComponentByPath<Button>(animationNode_CB, "btnBack");

            /////////////////////////////////遗弃
            btnCheckGift = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnCheckGift");      
            btnShare = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnShare");
            btnResponse = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnResopse");
            if (btnCreateRoom != null) btnCreateRoom.onClick.AddListener(OnOpenCreateRoom);

            if (btnBackRall != null) btnBackRall.onClick.AddListener(OnBackRall);

            //btnCheckGift.onClick.AddListener(CheckGift);
            
            if (!StringConfigClass.CanOpenHiddent())
            {
                animationNode_RT.SetActive(false);
            }
        }

        public override void OnEnable()
        {
            instance = this;

            base.OnEnable();

            InitValue();

            ResetRoundPos();

            PlayIn(0.5f);

            GetRoundMsg();

			Rall.MessageSend.EntryGameSucess(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort);
        }


        private static UIGeneralRall instance;
        public static void PlayAnimationIn()
        {
            if(instance != null)
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
            base.OnUpdate();
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
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRallGift_Rall, true);
        }

        /// <summary>
        /// 初始房间数据
        /// </summary>
        private void InitValue()
        {
            textName.text = GoableData.gameCoreData._roleInfor.name;
            //if (textName.preferredWidth > 270)
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
                SetImageForHttpbytes.SetImageFromUrl(headIcon,GoableData.gameCoreData._roleInfor.headUrl);
            }
            else
            {
                if (GoableData.gameCoreData._roleInfor.sex == 1)
                {
                    ///男
                    AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName, headIcon, "GameEnd10");
                }
                else
                {
                    ///女
                    AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName, headIcon, "GameEnd9");
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



        /// <summary>
        /// 创建房间
        /// </summary>
        private void OnOpenCreateRoom()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            curEntryItem.callSendEntryRoomCall(1, 0, 0);
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
            if(curEntryItem.callLeaveReleseFun != null)
            {
                curEntryItem.callLeaveReleseFun();
            }
            
        }

        public override void OnDisable()
        {
            base.OnDisable();
            ReleseAllGameRes();
        }
    }
}
