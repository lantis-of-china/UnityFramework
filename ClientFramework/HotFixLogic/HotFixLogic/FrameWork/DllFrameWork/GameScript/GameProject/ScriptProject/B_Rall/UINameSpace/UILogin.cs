using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UINameSpace
{
    public class UILogin : UIObject
    {
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UILogin_Rall, _className);
            return 1;
        }
        
        public UILogin()
        {
            assetsName = Rall.UIDefineName.UILogin_Rall;
            DebugLoger.Log("UILogin Create");
            //mName = "Login";
            //eUIType = EUIType.Queue;

            //base.OnLateUpdate = OnLateUpdate;
            //onDisable = OnDisable;
            //onDispose = OnDisable;
        }


        public bool showUI = true;
        public GameObject backGroundObje;
        public GameObject animationNode_LT;
        public GameObject animationNode_RB;
        public GameObject animationNode_LB;

        private Button mLoginBtn = null;
        private Button mLoginWeiChatBtn = null;
        private Transform mServerListRoot = null;
        private GameObject mServerGroup = null;
        private GameObject mServerBtnPrefab = null;
        private Button mOkBtn = null;
        private List<Button> mServerArr = new List<Button>();

        private List<Server.GolabServerInfor> mServerData = new List<Server.GolabServerInfor>();
        private Server.GolabServerInfor mCurSelectedServerData = null;
        /// <summary>
        /// 登陆错误
        /// </summary>
        public static bool loginError;
        /// <summary>
        /// 等待登陆游戏服务器
        /// </summary>
        public static float whatTime = 0.0f;

        public static UILogin instance;
        public override void OnAwake()
        {
            instance = this;
            if (!PlayerPrefs.HasKey("user_xieyi"))
            {
                PlayerPrefs.SetInt("user_xieyi", 0);
            }
            FrameWorkDrvice.MicrophoneManagerInstance.Init();
            AssetsParkManager.LoadParkData();
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIWaitting_Rall, false);

            backGroundObje = GenericityTool.GetObjectByPath(objectInstance, "ImgBgRight");
            animationNode_RB = GenericityTool.GetObjectByPath(objectInstance, "anchorRB/animationNode");
            animationNode_LT = GenericityTool.GetObjectByPath(objectInstance, "anchorLT/animationNode");
            animationNode_LB = GenericityTool.GetObjectByPath(objectInstance, "anchorLB/animationNode");

            mLoginBtn = GenericityTool.GetComponentByPath<Button>(animationNode_LB, "btnGeneralLogin");
            mLoginWeiChatBtn = GenericityTool.GetComponentByPath<Button>(animationNode_LB, "btnWeiChatLogin");

            GenericityTool.GetObjectByPath(animationNode_LB, "xieyi/Image/entry").SetActive(false);
            GenericityTool.GetObjectByPath(animationNode_LB, "xieyi/Image/full").SetActive(false);
            Button show= GenericityTool.GetComponentByPath<Button>(animationNode_LB, "ImgFont");
            Button closexieyi = GenericityTool.GetComponentByPath<Button>(animationNode_LB, "xieyi/close");


            Button entry = GenericityTool.GetComponentByPath<Button>(animationNode_LB, "btnClick");
            entry.enabled = true;
            Button full = GenericityTool.GetComponentByPath<Button>(animationNode_LB, "btnCheck");
            full.enabled = true;
            full.gameObject.SetActive(PlayerPrefs.GetInt("user_xieyi") == 0);
            entry.gameObject.SetActive(PlayerPrefs.GetInt("user_xieyi") == 1);
            entry.onClick.AddListener(() => { full.gameObject.SetActive(true); entry.gameObject.SetActive(false); PlayerPrefs.SetInt("user_xieyi", 0); });
            full.onClick.AddListener(() => { full.gameObject.SetActive(false); entry.gameObject.SetActive(true); PlayerPrefs.SetInt("user_xieyi", 1); });
            closexieyi.onClick.AddListener(() => { GenericityTool.GetObjectByPath(animationNode_LB, "xieyi").gameObject.SetActive(false); });
            show.onClick.AddListener(() => { GenericityTool.GetObjectByPath(animationNode_LB, "xieyi").gameObject.SetActive(true); });

            if (mLoginBtn != null)
            {
                mLoginBtn.onClick.AddListener(() => {
                    if (PlayerPrefs.GetInt("user_xieyi") == 1)
                    {
                        OnLoginBtnClick();
                        return;
                    }
                    UITipMessage.PlayMessage("请先阅读用户协议，并同意协议，才能登陆游戏");
                });
            }
            if (mLoginWeiChatBtn != null)
            {

                mLoginWeiChatBtn.onClick.AddListener(() =>
                {
					//if (FrameWorkDrvice.UiManagerInstance.IsOpenUI(Rall.UIDefineName.UIRall_Rall))
					//{
					//	FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIRall_Rall, eCloseType.None);
					//}
                    if (PlayerPrefs.GetInt("user_xieyi") == 1)
                    {
                        OnLoginWeiChatBtnClick();
                        return;
                    }
                    UITipMessage.PlayMessage("请先阅读用户协议，并同意协议，才能登陆游戏");
                });
            }
            
            if (mOkBtn != null) mOkBtn.onClick.AddListener(OnOkBtnClick);

			//mLoginBtn.gameObject.SetActive(false);
			mLoginWeiChatBtn.transform.localPosition = new Vector3(0, mLoginBtn.transform.localPosition.y, mLoginBtn.transform.localPosition.z);
			//if (!YanlongShareStudio.WeiChatInstall())
			//         {
			//             //mLoginBtn.transform.localPosition = new Vector3(0, mLoginBtn.transform.localPosition.y, mLoginBtn.transform.localPosition.z);
			//             //mLoginWeiChatBtn.gameObject.SetActive(false);
			//         }

			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIEntrysplsh_Rall, eCloseType.None);
		}

		void PlayBGM()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlayBackSound(Rall.ConfigProject.soundName, "audio_enterRoom", false);
        }

        /// <summary>
        /// 登陆游戏逻辑服务器 超时时间
        /// </summary>
        public void SetWaitLoginGameLogic()
        {
            whatTime = 10.0f;
        }

		public void ShowUIOrHidden()
		{
			if (!showUI)
			{
				UIWaitting.AddShowWaitting("Reconnect");
				animationNode_LT.SetActive(false);
				animationNode_RB.SetActive(false);
				animationNode_LB.SetActive(false);
				showUI = true;
			}
			else
			{
				GoableData.userValiadateInforWarp = null;
				animationNode_LT.SetActive(true);
				animationNode_RB.SetActive(true);
				animationNode_LB.SetActive(true);
			}
		}

		public override void OnEnable()
		{
			UINameSpace.UIWaitting.ClearAll();
			UINameSpace.UIWaitting.openOutTime = false;

			ShowUIOrHidden();

			if (UpResourceManager.Instance.IsLimitFunVersion())
			{
				mLoginWeiChatBtn.gameObject.SetActive(false);
				mLoginBtn.gameObject.SetActive(true);

				mLoginBtn.transform.localPosition = new Vector3(0, mLoginBtn.transform.localPosition.y, mLoginBtn.transform.localPosition.z);
			}
			else
			{
				mLoginWeiChatBtn.gameObject.SetActive(true);
				mLoginBtn.gameObject.SetActive(false);

				mLoginWeiChatBtn.transform.localPosition = new Vector3(0, mLoginWeiChatBtn.transform.localPosition.y, mLoginWeiChatBtn.transform.localPosition.z);
			}
			//if (!YanlongShareStudio.WeiChatInstall())
			//         {
			//             mLoginWeiChatBtn.gameObject.SetActive(false);
			//         }
			//         else
			//         {
			//             mLoginWeiChatBtn.gameObject.SetActive(true);
			//         }
			//mLoginBtn.gameObject.SetActive(false);
			DebugLoger.Log("GoableData.ServerIpaddress.needReconnect " + GoableData.ServerIpaddress.needReconnect);
			DebugLoger.Log("GoableData.ServerIpaddress.isLoginLogServerSend " + GoableData.ServerIpaddress.isLoginLogServerSend);
			DebugLoger.Log("GoableData.WeiChatUserData.hasUserData " + GoableData.WeiChatUserData.hasUserData);
			
			//PlayBGM();
            GoableData.CloseHeart();
            loginError = false;
            if (GoableData.ServerIpaddress.needReconnect && !GoableData.ServerIpaddress.isLoginLogServerSend)
            {
                if (GoableData.WeiChatUserData.hasUserData)
                {
                    WeiChatLoginStartServer();
                }
                else
                {
                    LogineStartServer();
                }
            }
            else
            {
                GoableData.ClearnData();

				CheckLogin();
			}

        }


		public void SaveLogin(bool wx)
		{
			
			Dictionary<string, string> jsonLogin = new Dictionary<string, string>();
			if (wx)
			{
				jsonLogin.Add("type", "wx");
				jsonLogin.Add("unionid", GoableData.WeiChatUserData.unionid);
				jsonLogin.Add("openid", GoableData.WeiChatUserData.openId);
				jsonLogin.Add("nickname", GoableData.WeiChatUserData.nickname);
				jsonLogin.Add("headimgurl", GoableData.WeiChatUserData.headimgurl);
				jsonLogin.Add("sex", GoableData.WeiChatUserData.sex.ToString());
			}
			else
			{
				jsonLogin.Add("type", "general");
			}

			string extSave = LitJson.JsonMapper.ToJson(jsonLogin);

			PlayerPrefs.SetString("login", extSave);
			
		}

		/// <summary>
		/// 清理登陆
		/// </summary>
		public static void ClearLogin()
		{
			PlayerPrefs.DeleteKey("login");
		}

		public void CheckLogin()
		{
			if (PlayerPrefs.HasKey("login"))
			{
				mLoginBtn.gameObject.SetActive(false);
				mLoginWeiChatBtn.gameObject.SetActive(false);
				string loginStr = PlayerPrefs.GetString("login");

				LitJson.JsonData jsdate = CSTools.JsonToData(loginStr);

				if (jsdate["type"].ToString() == "wx")
				{
					GoableData.WeiChatUserData.hasUserData = true;
					GoableData.WeiChatUserData.unionid = jsdate["unionid"].ToString();
					GoableData.WeiChatUserData.openId = jsdate["openid"].ToString();
					GoableData.WeiChatUserData.nickname = jsdate["nickname"].ToString();
					GoableData.WeiChatUserData.headimgurl = jsdate["headimgurl"].ToString();
					GoableData.WeiChatUserData.sex = byte.Parse(jsdate["sex"].ToString());

					//WeiChatLoginStartServer();
					UINameSpace.UIWaitting.AddShowWaitting("weiChatAuth");
					IEnumeratorManager.Instance.StartCoroutine(StartLogin(WeiChatLoginStartServer));
				}
				else
				{
					//LogineStartServer();
					IEnumeratorManager.Instance.StartCoroutine(StartLogin(LogineStartServer));
				}
			}
			else
			{
				if (UpResourceManager.Instance.IsLimitFunVersion())
				{
					mLoginBtn.gameObject.SetActive(true);
				}
				else
				{
					mLoginWeiChatBtn.gameObject.SetActive(true);
				}
			}
		}

		public IEnumerator StartLogin(System.Action loginAction)
		{
			yield return new IEnumeratorManager.WaitForSeconds(0.1f);

			if (UpResourceManager.Instance.IsLimitFunVersion())
			{
				mLoginBtn.gameObject.SetActive(true);
			}
			else
			{
				mLoginWeiChatBtn.gameObject.SetActive(true);
			}

			if (loginAction != null)
			{
				loginAction();
			}
		}

		public static void ShowLoginButton()
		{
			UINameSpace.UIWaitting.RemoveShowWaitting("weiChatAuth");
			UINameSpace.UIWaitting.RemoveShowWaitting(((int)NetMessageType.LoginWithUser_Login_Back).ToString());

			UIObject ui = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UILogin_Rall);
			if (ui != null)
			{

				if (UpResourceManager.Instance.IsLimitFunVersion())
				{
					(ui as UILogin).mLoginBtn.gameObject.SetActive(true);
				}
				else
				{
					(ui as UILogin).mLoginWeiChatBtn.gameObject.SetActive(true);

				}
			}
		}



        public void PlayIn(float waitTime)
        {
            CherishTweenMove.Begin(animationNode_RB, new Vector3(800, 0, 0), Vector3.zero, 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_LB, new Vector3(0, -650, 0), Vector3.zero, 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_LT, new Vector3(0, 300, 0), Vector3.zero, 0.3f, waitTime, true);
        }

        public static void OutLineSys()
        {
            loginError = true;
        }

        public static void OutLine()
        {
            //退出登录
            TcpGoable.Distance();
            if (UserNetWork.HasInstance())
            {
                UserNetWork.Instance.CloseSocket();
            }

            GoableData.ServerIpaddress.ClearnData();
            UINameSpace.UITipMessage.PlayMessage("连接服务器超时,请检查是否连接网络!");
            FrameWorkDrvice.UiManagerInstance.CloseAllUI();
            //无响应 退出到登录界面 需要重连
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
        }


        public static void UpLine()
        {
            if (loginError)
            {
                loginError = false;
                //GoableData.ServerIpaddress.ClearnData();
                GoableData.ServerIpaddress.gameServerIp = "";
                GoableData.reconnectIp = "";
                UINameSpace.UITipMessage.PlayMessage("连接服务器超时,请检查是否连接网络!");
                UINameSpace.UIWaitting.ClearAll();
                FrameWorkDrvice.UiManagerInstance.CloseAllUIExtern(new string[]{Rall.UIDefineName.UIWaitting_Rall, Rall.UIDefineName.UITipMessge_Rall, Rall.UIDefineName.UIGeneralTip_Rall, IMClub.UIDefineName.UIMain_IMClub});
                //无响应 退出到登录界面 需要重连
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
            }
        }

        public override void OnDisable()
        {
            mServerData = null;

            mCurSelectedServerData = null;

            UIWaitting.RemoveShowWaitting("Reconnect");
        }

        public override void OnDispose()
        {
            
        }

        /// <summary>
        /// 进入游戏服务器
        /// </summary>
        public void LoginGameServer()
        {
            if (!string.IsNullOrEmpty(GoableData.ServerIpaddress.gameServerIp))
            {
                UserNetWork unw = UserNetWork.Instance;
                if(UINameSpace.UIWaitting.codeList.Count == 0)
                {
                    UINameSpace.UIWaitting.AddShowWaitting(((int)NetMessageType.ChatWithUser_Login_Back).ToString());
                }
                if (GoableData.ServerIpaddress.isLoginLogServerSend && !GoableData.ServerIpaddress.isLoginGameServerSend && UserNetWork.Instance.isConnect)
                {
                    //登录服务器已经登录 游戏服务器没有登录
                    SetWaitLoginGameLogic();

                    GoableData.ServerIpaddress.isLoginGameServerSend = true;
                    Rall.MessageSend.LoginGame(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort);
                }
            }
        }

        /// <summary>
        /// 连接登陆服务器
        /// </summary>
        public void LogineStartServer()
        {            
            string pass = "";
            string generaPass = "generaPass";
            if ((GameMain.Instance.developerType == DeveloperType.PackageRun) && PlayerPrefs.HasKey(generaPass))
            {
                pass = PlayerPrefs.GetString(generaPass);                
            }
            else
            {
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();
                Random.seed++;
                pass += Random.Range(0, 9).ToString();

                if (GameMain.Instance.developerType == DeveloperType.PackageRun)
                {
                    PlayerPrefs.SetString(generaPass, pass);
                }
            }

            if (!GoableData.ServerIpaddress.isLoginLogServerSend)
            {
                //这里虚拟微信    
                /*
                GoableData.WeiChatUserData.hasUserData = true;
                GoableData.WeiChatUserData.nickname = "年满69";
                GoableData.WeiChatUserData.openId = "ujehduijngggfesdw";
                GoableData.WeiChatUserData.sex = 3;
                GoableData.WeiChatUserData.headimgurl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1509042861047&di=3842f70b827646d0227bd53e107c8178&imgtype=0&src=http%3A%2F%2Fimg3.duitang.com%2Fuploads%2Fitem%2F201602%2F27%2F20160227192212_SmNWM.jpeg";
                pass = GoableData.WeiChatUserData.openId;
                */
                GoableData.ServerIpaddress.isLoginLogServerSend = true;
				SaveLogin(false);
				Rall.MessageSend.LoginServer(pass, "88888888","", false);
            }
        }


        /// <summary>
        /// 微信登陆服务器
        /// </summary>
        public void WeiChatLoginStartServer()
        {
			SaveLogin(true);
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            //登录
            if (!GoableData.ServerIpaddress.isLoginLogServerSend)
            {
				DebugLoger.Log("WeiChatLoginStartServer ing send");
				GoableData.ServerIpaddress.isLoginLogServerSend = true;
				Rall.MessageSend.LoginServer(GoableData.WeiChatUserData.openId,"88888888", GoableData.WeiChatUserData.unionid, false);
            }
        }


        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        public void OnLoginBtnClick()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

           
            GoableData.WeiChatUserData.ClearData();
            LogineStartServer();           
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        public void OnLoginWeiChatBtnClick()
        {
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			{
				OnLoginBtnClick();
			}
			else
			{
				if (!YanlongShareStudio.WeiChatInstall())
				{
					UINameSpace.UITipMessage.PlayMessage("没有检测到安装微信或版本过低！");
					//GoableData.WeiChatUserData.ClearData();
					//LogineStartServer();      
				}
				//else
				//{
					GoableData.WeiChatUserData.ClearData();
					UINameSpace.UIWaitting.AddShowWaitting("weiChatAuth");
					GoableData.ServerIpaddress.SetGetWeiChatAuthState();
					YanlongShareStudio.WeiChatLoginAuth();
				//}
			}
        }


        /// <summary>
        /// 服务器按钮点击
        /// </summary>
        /// <param name="index"></param>
        private void OnServerBtnClick(int index)
        {
            if (mServerData == null || mServerData.Count <= 0)
            {
                GoableData.ServerIpaddress.ClearnData();
                UINameSpace.UITipMessage.PlayMessage("当前没有可连接的服务器!");
                return;
            }
            //这个index 是服务器传过来的列表数据的下标
            mCurSelectedServerData = mServerData[index];


            GoableData.ServerIpaddress.gameServerIp = mCurSelectedServerData.ServerIp;
            GoableData.ServerIpaddress.gameServerPort = mCurSelectedServerData.UdpServerPort;
        }

        /// <summary>
        /// 显示服务器列表
        /// </summary>
        public void ShowServersList(List<Server.GolabServerInfor> serverData)
        {
            mServerData = serverData;
            OnServerBtnClick(0);
            return;
            mServerGroup.gameObject.SetActive(true);
            for (int i = 0; i < serverData.Count; ++i)
            {
                if (i < mServerArr.Count)
                {
                    mServerArr[i].onClick.AddListener(()=>{ OnServerBtnClick(i); });
                    Text _btnLabel = mServerArr[i].GetComponentInChildren<Text>();
                    _btnLabel.text = serverData[i].ServerName;
                }
                else
                {
                    mServerArr.Add(GameObject.Instantiate(mServerBtnPrefab).GetComponent<Button>());
                    mServerArr[i].onClick.AddListener(()=>{ OnServerBtnClick(i); });
                    mServerArr[i].transform.SetParent(mServerListRoot);
                    Text _btnLabel = mServerArr[i].GetComponentInChildren<Text>();
                    _btnLabel.text = serverData[i].ServerName;
                }
            }
        }

        /// <summary>
        /// 关闭服务列表方法
        /// </summary>
        private void HideServerList()
        {
            mServerGroup.gameObject.SetActive(false);
        }

        /// <summary>
        /// 选择服务器之后点击确定进入游戏
        /// </summary>
        public void OnOkBtnClick()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            if (mCurSelectedServerData == null)
            {
                Debug.LogError("没有选中任何服务器");
                return;
            }
            //mCurSelectedServerData.ServerId;
            //mCurSelectedServerData.ServerIp;

            //MessageSend.EnterGame()
        }


        /// <summary>
        /// 授权信息
        /// </summary>
        /// <param name="msg"></param>
        public void GetCallAuthStr(string msg)
        {
            if(string.IsNullOrEmpty(msg))
            {
                //网络问题 访问失败
                //关闭遮挡
                UINameSpace.UIWaitting.RemoveShowWaitting("weiChatAuth");
            }
            else
            {
                //访问是成功的
                DebugLoger.Log("获取到的 msg " + msg);
                LitJson.JsonData jsObj  = CSTools.JsonToData(msg);

                //{ 
                //"access_token":"ACCESS_TOKEN", 
                //"expires_in":7200, 
                //"refresh_token":"REFRESH_TOKEN",
                //"openid":"OPENID", 
                //"scope":"SCOPE" 
                //}

                //{
                //"errcode":40029,"errmsg":"invalid code"
                //}
                if (jsObj["errcode"] != null)
                {
                    //错误的返回
                    DebugLoger.Log("errcode " + jsObj["errcode"]);
                    DebugLoger.Log("errmsg " + jsObj["errmsg"]);
                }
                else
                {
                    //正确的返回
                    DebugLoger.Log("access_token " + jsObj["access_token"]);
                    DebugLoger.Log("expires_in " + jsObj["expires_in"]);
                    DebugLoger.Log("refresh_token " + jsObj["refresh_token"]);
                    DebugLoger.Log("openid " + jsObj["openid"]);
                    DebugLoger.Log("scope " + jsObj["scope"]);
					DebugLoger.Log("unionid " + jsObj["unionid"]);

					SdkTools.GetWeiChatUserInfo(jsObj["access_token"].ToString(), jsObj["openid"].ToString(), GetCallUserInfo);
                }
            }
        }




		/// <summary>
		/// 用户登录信息返回
		/// </summary>
		/// <param name="msg"></param>
		public void GetCallUserInfo(string msg)
        {
			try
			{
				if (string.IsNullOrEmpty(msg))
				{
					//网络问题 访问失败
					UINameSpace.UIWaitting.RemoveShowWaitting("weiChatAuth");
				}
				else
				{
					//{ 
					//"openid":"OPENID",
					//"nickname":"NICKNAME",
					//"sex":1,
					//"province":"PROVINCE",
					//"city":"CITY",
					//"country":"COUNTRY",
					//"headimgurl": "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0",
					//"privilege":[
					//"PRIVILEGE1", 
					//"PRIVILEGE2"
					//],
					//"unionid": " o6_bmasdasdsad6_2sgVt7hMZOPfL"
					//}
					//{ 
					//"errcode":40003,"errmsg":"invalid openid"
					//}
					//访问成功 看下获取信息是否成功
					LitJson.JsonData jsObj = CSTools.JsonToData(msg);

					if (jsObj["errcode"] != null)
					{
						//错误的返回
						DebugLoger.Log("errcode " + jsObj["errcode"]);
						DebugLoger.Log("errmsg " + jsObj["errmsg"]);
					}
					else
					{
						DebugLoger.Log("unionid 2");
						DebugLoger.Log("unionid " + jsObj["unionid"]);

						//这里记录下用户信息
						GoableData.WeiChatUserData.unionid = jsObj["unionid"].ToString();
						GoableData.WeiChatUserData.openId = jsObj["openid"].ToString();
						GoableData.WeiChatUserData.nickname = jsObj["nickname"].ToString();
						GoableData.WeiChatUserData.headimgurl = jsObj["headimgurl"].ToString();
						GoableData.WeiChatUserData.sex = byte.Parse(jsObj["sex"].ToString());
						GoableData.WeiChatUserData.hasUserData = true;
						DebugLoger.Log("GoableData.WeiChatUserData.hasUserData = true");

						if (!string.IsNullOrEmpty(GoableData.WeiChatUserData.headimgurl))
						{
							//设定头像大小
							int position = GoableData.WeiChatUserData.headimgurl.LastIndexOf('/');
							DebugLoger.Log("pos ing 1");
							DebugLoger.Log("GoableData.WeiChatUserData.headimgurl " + GoableData.WeiChatUserData.headimgurl + " position " + position);
							GoableData.WeiChatUserData.headimgurl = GoableData.WeiChatUserData.headimgurl.Substring(0, position);
							DebugLoger.Log("pos ing 2");
							GoableData.WeiChatUserData.headimgurl += "/96";
							DebugLoger.Log("WeiChatLoginStartServer ing");
						}



						WeiChatLoginStartServer();
					}
				}
			}
			catch (System.Exception e)
			{
				DebugLoger.Log(e.ToString());
			}
        }

    }
}
