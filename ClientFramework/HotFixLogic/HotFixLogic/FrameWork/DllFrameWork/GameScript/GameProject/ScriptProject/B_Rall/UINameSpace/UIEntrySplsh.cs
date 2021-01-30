using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UINameSpace
{
    public class UIEntrySplsh : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIEntrysplsh_Rall, _className);
            return 1;
        }


        public  UIEntrySplsh()
        {
            assetsName = Rall.UIDefineName.UIEntrysplsh_Rall;
        }


        public static UIEntrySplsh Instance; 
        /// <summary>
        /// 检测信息条
        /// </summary>
        public Text txt_upInfo;

        #region 热更节点
        /// <summary>
        /// 热更节点
        /// </summary>
        public GameObject upNode;
        /// <summary>
        /// 更新信息
        /// </summary>
        public Text txt_upContent;
        /// <summary>
        /// 进度
        /// </summary>
        public Slider slider_up;
        /// <summary>
        /// 进度信息
        /// </summary>
        public Text txt_slider_upInfo;
        /// <summary>
        /// 退出游戏
        /// </summary>
        public Button btn_closeGame;
        /// <summary>
        /// 更新游戏
        /// </summary>
        public Button btn_up;
        #endregion 热更节点

        #region 重连节点
        /// <summary>
        /// 重连节点
        /// </summary>
        public GameObject reconnectNode;
        /// <summary>
        /// 重连
        /// </summary>
        public Button btn_reconnect;
        #endregion 重连节点
        /// <summary>
        /// 更新类型 0客户端 1资源
        /// </summary>
        public int upType;
        public string curAssetName;
        public int curTimes;
        public int toldTimes;
        public override void OnAwake()
        {
			base.OnAwake();
            DebugLoger.LogError("UIEntrySplsh OnAwake");
            RegistUpResourceManager();
            Instance = this;
            txt_upInfo = GenericityTool.GetComponentByPath<Text>(objectInstance, "txt_upInfo");
            upNode = GenericityTool.GetObjectByPath(objectInstance, "upNode");
            slider_up = GenericityTool.GetComponentByPath<Slider>(upNode, "slider_up");
            txt_slider_upInfo = GenericityTool.GetComponentByPath<Text>(upNode, "txt_upInfo");
            txt_upContent = GenericityTool.GetComponentByPath<Text>(upNode, "txt_content");
            btn_closeGame = GenericityTool.GetComponentByPath<Button>(upNode, "btn_close");
            btn_up = GenericityTool.GetComponentByPath<Button>(upNode, "btn_update");
            btn_closeGame.onClick.AddListener(OnClickCloseGame);
            btn_up.onClick.AddListener(OnClickUpdate);
            reconnectNode = GenericityTool.GetObjectByPath(objectInstance, "disconnectNode");
            btn_reconnect = GenericityTool.GetComponentByPath<Button>(reconnectNode, "btn_reconnect");
            btn_reconnect.onClick.AddListener(OnClickReconnect);
            HiddenUpdate();
            reconnectNode.SetActive(false);
		}

		public override void OnEnable()
        {
            DebugLoger.LogError("UIEntrySplsh OnEnable");
            CheckTextShowInfo("");
            //启动检测更新
            UpResourceManager.Instance.GetLocalAddress();

			LSharpEntryGame.GetLocation();
		}

        /// <summary>
        /// 关闭游戏
        /// </summary>
        public void OnClickCloseGame()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			UpResourceManager.Instance.CloseApplication();
        }

        /// <summary>
        /// 更新游戏
        /// </summary>
        public void OnClickUpdate()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			slider_up.gameObject.SetActive(true);
            slider_up.value = 0.0f;

            btn_closeGame.gameObject.SetActive(false);
            btn_up.gameObject.SetActive(false);
            txt_slider_upInfo.text = "更新准备中...";

            if (upType == 0)
            {
                UpResourceManager.Instance.StarUpClient();
            }
            else
            {
                UpResourceManager.Instance.StarUpdate();
            }
        }

        /// <summary>
        /// 重连
        /// </summary>
        public void OnClickReconnect()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			reconnectNode.SetActive(false);
            UpResourceManager.Instance.ReConnectCall();
        }

        /// <summary>
        /// 显示文本信息
        /// </summary>
        /// <param name="info"></param>
        public void CheckTextShowInfo(string info)
        {
            txt_upInfo.text = info;
        }


        /// <summary>
        /// 隐藏显示
        /// </summary>
        public void HiddenUpdate()
        {
            upNode.SetActive(false);
        }

        /// <summary>
        /// 显示更新资源
        /// </summary>
        public void ShowUpdate(string version,int size)
        {
            Debug.Log(size);
            upNode.SetActive(true);
            txt_slider_upInfo.text = "";
            slider_up.gameObject.SetActive(false);

            txt_upContent.text = "游戏更新信息\n";

            if(upType == 0)
            {
                txt_upContent.text += "亲爱的玩家,由于您的客户端版本过低无法进入游戏,请更新游戏后再进入游戏,带来不便非常抱歉!\n";
            }
            else
            {
                txt_upContent.text += "亲爱的玩家,由于系统资源更新,您需要更新游戏资源后才能正常游戏,谢谢配合！\n";
            }

            txt_upContent.text += "本地版本 " + UpResourceManager.Instance.GetLocalClientVersion();
            txt_upContent.text += " 最新版本 " + UpResourceManager.Instance.GetHttpClientVersion();
            txt_upContent.text += "\n资源大小" + ((float)size / (float)1024).ToString("0.0000") + "kBytes";
        }


        /// <summary>
        /// 注册更新接口
        /// </summary>
        public void RegistUpResourceManager()
        {
            UpResourceManager.Instance.mClientUpdateC = UpClientParrmaresCallFun;
            UpResourceManager.Instance.mResourceNeedUpdateC = ResourceNeedUpdate;
            UpResourceManager.Instance.mUpProgressC = UpProgressCallFun;
            UpResourceManager.Instance.mUpTimesInforC = UpTimesInforCallFun;
            UpResourceManager.Instance.mReadyEntryGameFunctionC = ReadyEntryGameFunction;
            UpResourceManager.Instance.mUpVersionCompletedC = UpVersionCompleted;
            UpResourceManager.Instance.mUpVersionClientCompletedC = UpVersionClientCompleted;
            UpResourceManager.Instance.mShowCheckNetC = ShowCheckNet;
            UpResourceManager.Instance.mCheckUpdateC = CheckUpdate;
            UpResourceManager.Instance.mCheckUpdateCompareC = CheckUpdateCompare;
        }

        #region 更新事件
        /// <summary>
        /// 开始检测短版本号
        /// </summary>
        public static void CheckUpdate()
        {
            Instance.CheckTextShowInfo("检测版本号中...");
        }

        /// <summary>
        /// 开始检测资源版本
        /// </summary>
        public static void CheckUpdateCompare()
        {
            Instance.CheckTextShowInfo("连网校验资源信息中...");
        }

        /// <summary>
        /// 客户端安装文件需要更新
        /// </summary>
        /// <param name="str">版本</param>
        /// <param name="intValue">总大小</param>
        public static void UpClientParrmaresCallFun(string str, int intValue)
        {
            Instance.CheckTextShowInfo("");
            Instance.upType = 0;
            Instance.ShowUpdate(str, intValue);
        }

        /// <summary>
        /// 资源需要更新 热更新
        /// </summary>
        /// <param name="str">版本</param>
        /// <param name="intValue">总大小</param>
        public static void ResourceNeedUpdate(string str, int intValue)
        {
            Instance.CheckTextShowInfo("");
            Instance.upType = 1;
            Instance.ShowUpdate(str, intValue);
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="intSize">当下进度</param>
        /// <param name="toldSize">总共进度</param>
        public static void UpProgressCallFun(int intSize, int toldSize)
        {
            float curValue = (float)intSize / (float)toldSize;

            Instance.txt_slider_upInfo.text = "当前" + Instance.curAssetName + Instance.curTimes + "/" + Instance.toldTimes + "进度" + (curValue * 100).ToString("0.0") + "%";
            Instance.slider_up.gameObject.SetActive(true);
            Instance.slider_up.value = curValue;
        }

        /// <summary>
        /// 通知更新到的文件
        /// </summary>
        /// <param name="currentTimes">当前第几个</param>
        /// <param name="toldTimes">总共多少个</param>
        /// <param name="version">版本</param>
        /// <param name="resourceName">资源名</param>
        /// <param name="byteCount">当前资源大小</param>
        public static void UpTimesInforCallFun(int currentTimes, int toldTimes, string version, string resourceName, int byteCount)
        {
            Instance.curTimes = currentTimes;
            Instance.toldTimes = toldTimes;
            string[] ary = resourceName.Split('/');
            Instance.curAssetName = ".." + ary[ary.Length - 1];
        }

		/// <summary>
		/// 资源更新完毕
		/// </summary>
		public static void UpVersionCompleted()
		{
			Instance.txt_slider_upInfo.text = "100%";
			Instance.slider_up.value = 1;
			//ReadyEntryGameFunction();
			//Application.Quit();
			//清理客户端包括UI 释放资源 重新运行游戏
			//LSharpEntryGame.UnReleseGame();
			if (Application.platform == RuntimePlatform.Android)
			{
				CherishUtility.DoReStar(10);
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				CherishUtility.DoReStar(10);
			}
			else if(Application.platform == RuntimePlatform.WindowsEditor)
			{
				CherishUtility.DoReStar(10);
			}
        }

        /// <summary>
        /// 客户端更新完毕
        /// </summary>
        public static void UpVersionClientCompleted()
        {
		}

        /// <summary>
        /// 通知更新掉线
        /// </summary>
        public static void ShowCheckNet()
        {
            Instance.reconnectNode.SetActive(true);
        }

        /// <summary>
        /// 进入游戏
        /// </summary>
        public static void ReadyEntryGameFunction()
        {
			FrameWork.ServerLog.Start();
            LSharpEntryGame.Instance.GameStarRun();
        }
        #endregion 更新事件
    }
}
