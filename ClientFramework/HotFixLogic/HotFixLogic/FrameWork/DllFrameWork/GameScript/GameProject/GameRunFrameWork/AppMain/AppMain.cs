using UnityEngine;
using System.Collections;
using ILRuntime.Runtime.Enviorment;
using System.Collections.Generic;
using System.IO;
using Unity.Jobs;
using UnityEngine.Jobs;

public enum OutAppEnum
{
	/// <summary>
	/// 无状态
	/// </summary>
	None = 0,
	/// <summary>
	/// 第三方过滤跳转
	/// </summary>
	OtherLink = 1,
	/// <summary>
	/// 微信分享
	/// </summary>
	WeiChatShare = 2,
	/// <summary>
	/// 微信登陆
	/// </summary>
	WeiChatAuth = 3,
	/// <summary>
	/// 裁剪照片
	/// </summary>
	TakePhoto = 4
}

public class LSharpEntryGame
{
	/// <summary>
	/// 记录App登录出去时间
	/// </summary>
	private static System.DateTime outTime = System.DateTime.Now;
	/// <summary>
	/// 登出记录
	/// </summary>
	public static OutAppEnum outAppEnum = OutAppEnum.None;
	/// <summary>
	/// 照片裁剪回调
	/// </summary>
	public static System.Action TakePhotoFinishCallBack;
	/// <summary>
	/// 设置程序入口启动的运行方式
	/// </summary>
	public static ScriptType scriptType = ScriptType.Dotnet;
	/// <summary>
	/// 记录App是否退出后台
	/// </summary>
	public static bool isAppPause = false;
	/// <summary>
	/// Dll文件名
	/// </summary>
	public static string DllFileName;
	/// <summary>
	/// ILRuntime的程序执行域
	/// </summary>
	public static AppDomain ILAppDomain;
	/// <summary>
	/// 游戏根节点不销毁节点
	/// </summary>
	public GameObject gameDontDestroy;
	/// <summary>
	/// 外部Main节点
	/// </summary>
	private static GameObject gameRootRun;
	/// <summary>
	/// 外部传入的Main脚本
	/// </summary>
	public static MonoBehaviour monoBehaviour;
	/// <summary>
	/// 脚本程序的单例
	/// </summary>
	private static LSharpEntryGame instance;
	/// <summary>
	/// 获取脚本程序单例
	/// </summary>
	public static LSharpEntryGame Instance
	{
		get
		{
			return instance;
		}
	}

	/// <summary>
	/// 设置程序主动退出后台类型
	/// </summary>
	public static void SetAppEnumNone()
	{
		outAppEnum = OutAppEnum.None;
	}

	/// <summary>
	/// 设置程序主动退出后台类型
	/// </summary>
	public static void SetAppEnumTakePhoto()
	{
		outAppEnum = OutAppEnum.TakePhoto;
	}

	/// <summary>
	/// 释放游戏
	/// </summary>
	public static void UnReleseGame()
	{
		GameObject.Destroy(LSharpEntryGame.Instance.gameDontDestroy);
		FrameWorkDrvice.UnReleseGame();

		if (monoBehaviour != null)
		{
			DebugLoger.Log("------------------ReStartLogic---------------------------");
			(monoBehaviour as GameMain).ReStartLogic();
		}
	}

	/// <summary>
	/// Net程序进入入口
	/// </summary>
	/// <param name="_dllFileName"></param>
	/// <param name="scrtType"></param>
	/// <returns></returns>
	public static LSharpEntryGame RunGame_Net(string _dllFileName, ScriptType scrtType)
	{
		return EntryGame(_dllFileName, scrtType);
	}

	/// <summary>
	/// ILRuntime程序进入入口
	/// </summary>
	/// <param name="_dllFileName"></param>
	/// <param name="scrtType"></param>
	/// <param name="app"></param>
	/// <returns></returns>
	public static LSharpEntryGame RunGame_ILRuntime(string _dllFileName, ScriptType scrtType, AppDomain app)
	{
		ILAppDomain = app;

		return EntryGame(_dllFileName, scrtType);
	}

	/// <summary>
	/// 脚本程序入口
	/// </summary>
	/// <param name="_dllFileName"></param>
	/// <param name="scrtType"></param>
	/// <returns></returns>
	public static LSharpEntryGame EntryGame(string _dllFileName, ScriptType scrtType)
	{
		Application.backgroundLoadingPriority = ThreadPriority.Low;

		if (Application.isMobilePlatform)
		{
			UIManager.loadSync = false;
		}
		else
		{
			if (Application.isEditor)
			{
				UIManager.loadSync = false;
			}
			else 
			{
				UIManager.loadSync = false;
			}
		}

		//UIManager.loadSync = true;
		outTime = System.DateTime.Now;
		Time.fixedDeltaTime = 0.02f;
		Application.targetFrameRate = 60;
		Application.runInBackground = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		StringConfigClass.SetToAppSdk();
		CherishUtility.init();

		if (!PlayerPrefs.HasKey("first"))
		{
			PlayerPrefs.SetString("first", "first");
			CherishUtility.PlayMovie();
		}

		GameMain.Instance.openLog = true;
		scriptType = scrtType;
		DllFileName = _dllFileName;

		if (instance == null)
		{
			instance = new LSharpEntryGame();
		}

		monoBehaviour = GameObject.FindObjectOfType<GameMain>();

		if (monoBehaviour != null)
		{
			FrameWorkDrvice.RegistMonoBehaviour(monoBehaviour);
			gameRootRun = monoBehaviour.gameObject;

			Instance.Init();
		}

		//TestJob();

		return instance;
	}

	public static void TestJob()
	{
		var jobTest = new Lantis.JobExecute();
		jobTest.SetParamar(10, (paramar) =>
		{
			DebugLoger.Log($"log run test:{ paramar }");
		});
		jobTest.SetHandle(jobTest.Schedule());

		var data = new List<int>() { 10,9,8,7,6,5,4,3,2,1 };
		var jobFor = new Lantis.JobParallelForExecute();
		jobFor.SetParamar(data, (paramar,i) =>
		{
			var list = paramar as List<int>;
			var index = (int)i;
			DebugLoger.Log($"log run test:{ list[index] }");
		});
		jobTest.SetHandle(jobFor.Schedule(data.Count,0));

		TransformAccessArray transformAccessArray = new TransformAccessArray(new Transform[] { gameRootRun.transform });
		var jobTestTransfrom = new Lantis.JobParallelForTransformExecute();
		jobTestTransfrom.SetParamar(null,(transformObject,paramars, index) =>
		{
			var transformAccess = (TransformAccess)transformObject;
			transformAccess.localScale = Vector3.zero;
		});
		jobTest.SetHandle(jobTestTransfrom.Schedule(transformAccessArray));

		Lantis.LantisJobSystem.Complete();
		DebugLoger.Log($"log run test end");
	}

	/// <summary>
	/// 获取定位
	/// </summary>
	public static void GetLocation()
	{
		if (Application.isMobilePlatform)
		{
			IEnumeratorManager.Instance.StartCoroutine(IEGetLocation());
		}
		else
		{
			GoableData.ServerIpaddress.longitude = (float)ServerRandom.GetRandomDoubleValue(10, 40);
			GoableData.ServerIpaddress.latitude = (float)ServerRandom.GetRandomDoubleValue(10, 40); ;
		}
	}

	/// <summary>
	/// 获取定位
	/// </summary>
	public static IEnumerator IEGetLocation()
	{
		DebugLoger.Log("定位第一步!");

		if (!Input.location.isEnabledByUser)
		{
			DebugLoger.Log("请到设置中开启定位功能!");
			yield return 0;
		}

		DebugLoger.Log("定位第二步!");
		Input.location.Start();

		int maxWait = 1000;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new IEnumeratorManager.WaitForSeconds(0.1f);
			maxWait--;
		}

		DebugLoger.Log("定位第三步!");

		if (maxWait < 1)
		{
			DebugLoger.Log("定位初始化失败!");
			UINameSpace.UITipMessage.PlayMessage("定位初始化失败!");
			yield return 0;
		}

		if (Input.location.status == LocationServiceStatus.Failed)
		{
			DebugLoger.Log("定位服务开启失败!");
			yield return 0;
		}
		else
		{
			GoableData.ServerIpaddress.longitude = Input.location.lastData.longitude;
			GoableData.ServerIpaddress.latitude = Input.location.lastData.latitude;
			DebugLoger.Log("定位成功! 经度:" + Input.location.lastData.longitude + " 纬度:" + Input.location.lastData.latitude);
		}
	}


	public static void OnApplicationPause(bool isPause)
	{
		DebugLoger.Log($"OnApplicationPause:{isPause.ToString()}");
		isAppPause = isPause;

		if (!isAppPause)
		{
			System.TimeSpan ts = System.DateTime.Now - outTime;

			if (ts.TotalSeconds >= 30 && ts.TotalSeconds < (25 * 60) && GoableData.ServerIpaddress.isLoginLogServerSend)
			{
				DebugLoger.Log("第一阶段 进入恢复");
				IMClub.ClubItem.RecordSelectItem();
				FrameWorkDrvice.UiManagerInstance.CloseUI(IMClub.UIDefineName.UIMain_IMClub, eCloseType.Queue);
				GoableData.ClearEventData();
				IMClub.ClubItem.ResetRecordSelectItem();

				if (UdpNetWork.HasInstance())
				{
					UdpNetWork.Instance.CloseSocket();
				}

				UdpNetWork.Instance.Start();
				Rall.MessageSend.NAT();
			}
			else if (ts.TotalSeconds >= (25 * 60) && GoableData.ServerIpaddress.isLoginLogServerSend)
			{
				DebugLoger.Log("第二阶段 进入恢复");

				if (UINameSpace.UILogin.instance != null)
				{
					UINameSpace.UILogin.instance.showUI = true;
				}

				UINameSpace.UILogin.instance = null;
				GoableData.ServerIpaddress.needReconnect = true;
				GoableData.ServerIpaddress.isLoginLogServerSend = false;
				GoableData.ServerIpaddress.isLoginGameLogic = false;
				GoableData.ServerIpaddress.isLoginGameServerSend = false;
				GoableData.ServerIpaddress.gameServerIp = "";
				GoableData.SetReconnectEnable();
				GoableData.ServerIpaddress.SetReconnect();
				GoableData.ClearLogicsData();

				IMClub.ClubItem.RecordSelectItem();
				FrameWorkDrvice.UiManagerInstance.CloseUI(IMClub.UIDefineName.UIMain_IMClub, eCloseType.Queue);
				GoableData.ClearEventData();
				IMClub.ClubItem.ResetRecordSelectItem();

				if (UdpNetWork.HasInstance())
				{
					UdpNetWork.Instance.CloseSocket();
				}

				UdpNetWork.Instance.Start();
				Rall.MessageSend.NAT();
			}
			else
			{
				DebugLoger.Log("直接进入恢复");

				if (UdpNetWork.HasInstance())
				{
					UdpNetWork.Instance.CloseSocket();
				}

				UdpNetWork.Instance.Start();
				Rall.MessageSend.NAT();
			}
		}
		else
		{
			outTime = System.DateTime.Now;
		}

		FrameWorkDrvice.MicrophoneManagerInstance.ClearMicroEnd();


		UserNetWork.openExpecation = false;

		if (GoableData.ServerIpaddress.getSdkOut)
		{
			if (!isAppPause)
			{
				GoableData.ServerIpaddress.ClearSdkOutGame();
			}

			return;
		}

		if (outAppEnum == OutAppEnum.TakePhoto)
		{
			int activityProcess = CherishUtility.GetInActivityProcess();

			if (activityProcess == 0)
			{
				if (TakePhotoFinishCallBack != null)
				{
					TakePhotoFinishCallBack();
				}
			}

			return;
		}

		if (!GoableData.ServerIpaddress.getWeiChatAuth)
		{
			if (GoableData.NeedReconnect() || (UserNetWork.HasInstance() && !UserNetWork.Instance.CenterNetServer.Connected))
			{
				if (isPause == true)
				{
					TcpGoable.Distance();

					if (UserNetWork.HasInstance())
					{
						UserNetWork.Instance.CloseSocket(Rall.MessageSend.LoginOut);
					}
				}
				else
				{
					GoableData.SetReconnectDisable();

					TcpGoable.Distance();

					if (UserNetWork.HasInstance())
					{
						UserNetWork.Instance.CloseSocket(Rall.MessageSend.LoginOut);
					}

					if (GoableData.ServerIpaddress.isLoginLogServerSend && GoableData.ServerIpaddress.isLoginGameServerSend)
					{
						GoableData.ClearnData(false);

						GoableData.ServerIpaddress.SetReconnect();
					}

					List<string> uiExternList = new List<string>();
					uiExternList.Add(Rall.UIDefineName.UIWaitting_Rall);
					uiExternList.Add(Rall.UIDefineName.UITipMessge_Rall);
					uiExternList.Add(Rall.UIDefineName.UIGeneralTip_Rall);
					uiExternList.Add(IMClub.UIDefineName.UIMain_IMClub);

					if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
					{
						uiExternList.Add(GoableData.reconnectExternUIName);
					}

					if (!string.IsNullOrEmpty(Rall.ConfigProject.currentRallName))
					{
						uiExternList.Add(Rall.ConfigProject.currentRallName);
					}

					FrameWorkDrvice.UiManagerInstance.CloseAllUIExtern(uiExternList.ToArray());

					if (UINameSpace.UILogin.instance != null)
					{
						UINameSpace.UILogin.instance.showUI = false;
					}

					if (string.IsNullOrEmpty(GoableData.reconnectExternUIName))
					{
						if (UINameSpace.UILogin.instance != null)
						{
							UINameSpace.UILogin.instance.showUI = true;
						}
					}

					FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);

					if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
					{
						UIObject uiGame = FrameWorkDrvice.UiManagerInstance.GetUI(GoableData.reconnectExternUIName);

						if (uiGame != null)
						{
							uiGame.ToBestLayer();
						}
					}
				}
			}
		}
		else
		{
			if (!isPause)
			{
				Debug.Log("第三方切换帐号登录");
				GoableData.ServerIpaddress.ClearGetWeihatAuthState();
				string code = YanlongShareStudio.WeiChatGetAuthCode();
				Debug.Log($"获取第三方帐号code:{code}");

				if (string.IsNullOrEmpty(code))
				{
					UINameSpace.UILogin.instance.GetCallAuthStr("");
				}
				else
				{
					SdkTools.GetWeiChatAuthJson(code, UINameSpace.UILogin.instance.GetCallAuthStr);
				}
			}
		}
	}


	public static void OnApplicationQuit()
	{
		ObjectPool.ExitClearnThread();

		TcpGoable.Distance();

		if (UserNetWork.HasInstance())
		{
			UserNetWork.Instance.CloseSocket(Rall.MessageSend.LoginOut);
		}

		Server.Process.MessageDriver.CloseRunUpSend();

		if (UdpNetWork.HasInstance())
		{
			UdpNetWork.Instance.CloseSocket();
		}

		FrameUdpCenter.Close();
	}

	public static List<Coroutine> coroutineList = new List<Coroutine>();

	public static Coroutine StartCoroutine(IEnumerator routine)
	{
		if (monoBehaviour != null)
		{
			Coroutine curCoutine = monoBehaviour.StartCoroutine(routine);

			coroutineList.Add(curCoutine);

			return curCoutine;
		}

		return null;
	}
	
	/// <summary>
	/// 更新方法
	/// </summary>
	public void Update()
	{
		FrameWorkDrvice.Instance.Update();
	}

	public void LateUpdate()
	{
		FrameWorkDrvice.Instance.LateUpdate();
	}

	public void FixedUpdate()
	{
		FrameWorkDrvice.Instance.FixedUpdate();
	}

	public void OnGUI()
	{
	}

	public void Init()
	{
		DebugLoger.Log($"设置不可被销毁:{gameRootRun.name}");
		GameObject.DontDestroyOnLoad(gameRootRun);
		IAPInterface.Instance.InstancePurchase();
		InitGame();
		StarSplshImage();
		ObjectPool.RunClearThread();
		Server.Process.MessageDriver.UpSendMessage();
	}

	/// <summary>
	/// 实例化游戏节点
	/// </summary>
	private void InitGame()
	{
		AudioOutManager.GetSoundValueSaveValue();
		CreateGameNode();
		UIManager.staticExternUI.Add(Rall.UIDefineName.UIGameMask_Rall);
		UIManager.staticExternUI.Add(Rall.UIDefineName.UIGeneralTip_Rall);
		UIManager.staticExternUI.Add(Rall.UIDefineName.UIWaitting_Rall);
		UIManager.staticExternUI.Add(Rall.UIDefineName.UITipMessge_Rall);
		UIManager.staticExternUI.Add(IMClub.UIDefineName.UIMain_IMClub);
		FrameWorkDrvice.GameEntryManagerInstanece.RegistFunction();
		FrameWorkDrvice.WorldManagerInstance.RegisterFunction();
		FrameWorkDrvice.UiManagerInstance.RegistFunction();
		FrameWorkDrvice.UiManagerInstance.SetLandscape();
		UdpNetWork.Instance.Start();
		FrameUdpCenter.StartRun();
	}

	/// <summary>
	/// 创建游戏需要节点
	/// </summary>
	private void CreateGameNode()
	{
		gameDontDestroy = new GameObject("gameDontDestroy");
		gameDontDestroy.transform.position = Vector3.zero;
		GameObject.DontDestroyOnLoad(gameDontDestroy);
	}

	/// <summary>
	/// 启动图片
	/// </summary>
	private void StarSplshImage()
	{
		monoBehaviour.StartCoroutine(SplshImageCallFun());
	}

	/// <summary>
	/// 启动图片时间到  游戏开始
	/// </summary>
	private IEnumerator SplshImageCallFun()
	{
		FontManager.LoadPrefabFont();
		yield return new WaitForSeconds(0.5f);
		FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGameMask_Rall, false);
		FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIDebuger_Rall, true);
		//FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIEntrysplsh_Rall, true);
		FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGeneralTip_Rall, true);
		UINameSpace.UITipMessage.OpenTip();
		yield return new WaitForSeconds(0.2f);

		
		GameLogic.ConfigLoader.Load();
		FrameWorkDrvice.Instance.AddEventToUpdate(GLGame.WorldManager.Instance.Update);
		FrameWorkDrvice.Instance.AddEventToLateUpdate(GLGame.WorldManager.Instance.LateUpdate);
		UINameSpace.UIFadeMasker.OpenFade(0.0f, 1.0f, () =>
		{
			Debug.Log("UINameSpace.UIFadeMasker.OpenFade call back");
			FrameWorkDrvice.UiManagerInstance.SetPortrait();
			GLGame.WorldManager.Instance.RegisterFunction();
			GLGame.WorldManager.Instance.OpenWorld(GameLogic.ConfigProject.projectFloderName, GameLogic.WorldDefineSoupport.startSence);
		});
		

		FrameWorkDrvice.ConfigDataManagerInstance.LoaeConfig();
		ChekExternIcon();
	}

	/// <summary>
	/// 游戏正式开始
	/// </summary>
	public void GameStarRun()
	{
		FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
	}

	/// <summary>
	/// 检测外部Icon
	/// </summary>
	private void ChekExternIcon()
	{
		string iconName = "icon.jpg";
		string externPath = FrameWorkDrvice.AssetsPathManagerInstance.GetExternPathNode();
		string iconPath = externPath + "/" + iconName;

		if (!System.IO.File.Exists(iconPath))
		{
			Texture2D iconObj = Resources.Load(iconName.Replace(".jpg", "")) as Texture2D;

			if (iconObj == null)
			{
				DebugLoger.LogError("ChekExternIcon icon null");
				return;
			}

			byte[] imageBuf = iconObj.EncodeToPNG();

			if (imageBuf == null)
			{
				DebugLoger.LogError("ChekExternIcon imageBuf null");
				return;
			}

			System.IO.File.WriteAllBytes(iconPath, imageBuf);
			GameObject.DestroyImmediate(iconObj);
		}
	}
}
