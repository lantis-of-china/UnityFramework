using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UINameSpace
{
    public class UIDebuger : UIObject
    {
		private static UIDebuger __instance;
		public override void SetInstance(UIObject target)
		{
			__instance = target as UIDebuger;
		}
		public static UIDebuger GetInstance()
		{
			return __instance;
		}

		/// <summary>
		/// 反射调用的注册方法
		/// </summary>
		/// <param name="_className"></param>
		public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIDebuger_Rall, _className);
            return 1;
        }


        public UIDebuger()
        {
            mUiShowLayer = eUiShowLayer.UITop;
            assetsName = Rall.UIDefineName.UIDebuger_Rall;
        }

        /// <summary>
        /// 动画节点
        /// </summary>
        public GameObject animationNode;
        public Button btn_close;
        public Button btn_info;
        public Button btn_waring;
        public Button btn_error;
        public Button btn_clear;
        public Text lb_fps;
        public Text lb_info;
        public RectTransform contentRect;

        public bool error_state;
        public bool waring_state;
        public bool info_state;
        private int recordTime = 0;

        public override void OnAwake()
        {
            base.OnAwake();
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "animationNode");
            contentRect = GenericityTool.GetComponentByPath<RectTransform>(animationNode, "Scroll View/Viewport/Content");
            btn_close = GenericityTool.GetComponentByPath<Button>(animationNode, "anchor_Up/btn_close");
            lb_fps = GenericityTool.GetComponentByPath<Text>(animationNode, "anchor_Up/txt_fps");
            btn_info = GenericityTool.GetComponentByPath<Button>(animationNode, "anchor_Down/btn_info");
            btn_waring = GenericityTool.GetComponentByPath<Button>(animationNode, "anchor_Down/btn_wrang");
            btn_error = GenericityTool.GetComponentByPath<Button>(animationNode, "anchor_Down/btn_error");
            btn_clear = GenericityTool.GetComponentByPath<Button>(animationNode, "anchor_Down/btn_clear");
            lb_info = GenericityTool.GetComponentByPath<Text>(contentRect.gameObject, "Text");

            btn_close.onClick.AddListener(OnClick_Close);
            btn_info.onClick.AddListener(OnClick_Info);
            btn_waring.onClick.AddListener(OnClick_Waring);
            btn_error.onClick.AddListener(OnClick_Error);
            btn_clear.onClick.AddListener(OnClick_Clear);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            
            OnHidden();
        }

        public override void OnUpdate()
        {
            recordTime++;

            if (recordTime > 30)
            {
                recordTime = 0;
                lb_fps.text = $"FPS:{FpsRecorder.GetFps()} MinDelay:{GoableData.minPing * 1000}ms Ping:{GoableData.currentPing * 1000}ms";
            }

            if (Application.isMobilePlatform && !animationNode.activeSelf)
            {
                if (Input.touchCount > 3)
                {
                    OnShow();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && !animationNode.activeSelf)
                {
                    OnShow();
                }
            }
        }


        public void InitState()
        {
            error_state = true;
            waring_state = true;
            info_state = true;

            DebugLoger.ChangeCall = OnChangeCall;
            OnChangeCall(DebugLoger.logList);
        }

        public void OnChangeCall(List<DebugType> logics)
        {
            string logInfo = SetInfo(info_state, waring_state, error_state, logics);
            ShowLog(logInfo);
        }

        public static string SetInfo(bool info, bool waring, bool error, List<DebugType> logList)
        {
            int Line = 0;
            string log_infor = "";
            for (int i = 0; i < logList.Count; ++i)
            {
                DebugType dt = logList[i];
                if (info)
                {
                    if (dt.type == 0)
                    {
                        log_infor += "<color=#0fffff>" + Line + "</color><color=#ffffff> " + dt.message + "</color>\n\r";
                    }
                }

                if (waring)
                {
                    if (dt.type == 1)
                    {
                        log_infor += "<color=#0fffff>" + Line + "</color><color=#ffff0f> " + dt.message + "</color>\n\r";
                    }
                }

                if (error)
                {
                    if (dt.type == 2)
                    {
                        log_infor += "<color=#0fffff>" + Line + "</color><color=#ff0f00> " + dt.message + "</color>\n\r";
                    }
                }

                Line++;
            }
            return log_infor;
        }

        public void ShowLog(string logStr)
        {
            lb_info.text = logStr.Replace(" ","\u3000");
                
           contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, lb_info.rectTransform.rect.height);

            CherishDelay.DelayRun.Add(null, 0.1f, new CherishDelay.DelayRun.Run(() => 
            {
                contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, lb_info.rectTransform.rect.height);
            }));
        }

        private void OnClick_Clear()
        {
            DebugLoger.Clear();
        }

        private void OnClick_Info()
        {
            info_state = !info_state;
            OnChangeCall(DebugLoger.logList);
        }

        private void OnClick_Waring()
        {
            waring_state = !waring_state;
            OnChangeCall(DebugLoger.logList);
        }

        private void OnClick_Error()
        {
            error_state = !error_state;
            OnChangeCall(DebugLoger.logList);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        private void OnClick_Close()
        {
            OnHidden();
        }

        private void OnHidden()
        {
            animationNode.SetActive(false);
        }

        private void OnShow()
        {
            animationNode.SetActive(true);
            InitState();
        }

		/// <summary>
		/// 关闭
		/// </summary>
		public override void OnClose()
        {
            base.OnClose();
        }
    }
}