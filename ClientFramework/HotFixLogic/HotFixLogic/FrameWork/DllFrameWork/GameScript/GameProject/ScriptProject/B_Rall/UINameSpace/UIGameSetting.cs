using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UINameSpace
{
    public class UIGameSetting : UIObject
    {
        public static bool hiddent = false;

		public static bool showLoginout = false;

		/// <summary>
		/// 反射调用的注册方法
		/// </summary>
		/// <param name="_className"></param>
		public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIGameSetting_Rall, _className);
            return 1;
        }


        public UIGameSetting()
        {
            assetsName = Rall.UIDefineName.UIGameSetting_Rall;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public Button btn_Close;
        /// <summary>
        /// 登出
        /// </summary>
        public Button btn_LoginOut;
        /// <summary>
        /// 音效进度条
        /// </summary>
        public Slider slider_sound;
        /// <summary>
        /// 背景音进度条
        /// </summary>
        public Slider slider_backGroundSound;
        /// <summary>
        /// 选择节点
        /// </summary>
        public GameObject toggleNode;
        /// <summary>
        /// 地方话
        /// </summary>
        public Toggle toggle_localLanguage;
        public Text togleText_local;

        /// <summary>
        /// 普通话
        /// </summary>
        public Toggle toggle_generalLanguage;
        public Text togleText_general;



        /// <summary>
        /// 动画节点
        /// </summary>
        public GameObject aniamtionNode;

        public override void OnAwake()
        {
            base.OnAwake();

            aniamtionNode = GenericityTool.GetObjectByPath(objectInstance, "anchor_CT/animationNode");

            btn_Close = GenericityTool.GetComponentByPath<Button>(aniamtionNode, "btn_close");
            btn_LoginOut = GenericityTool.GetComponentByPath<Button>(aniamtionNode, "btn_loginOut");

            slider_sound = GenericityTool.GetComponentByPath<Slider>(aniamtionNode, "slider_sound");
            slider_backGroundSound = GenericityTool.GetComponentByPath<Slider>(aniamtionNode, "slider_backGroundSound");

            toggleNode = GenericityTool.GetObjectByPath(aniamtionNode, "languageToggle");
            toggle_localLanguage = GenericityTool.GetComponentByPath<Toggle>(aniamtionNode, "languageToggle/toggle_localLanguage");
            togleText_local = GenericityTool.GetComponentByPath<Text>(aniamtionNode, "languageToggle/toggle_localLanguage/Label");
                        
            toggle_generalLanguage = GenericityTool.GetComponentByPath<Toggle>(aniamtionNode, "languageToggle/toggle_generalLanguage");
            togleText_general = GenericityTool.GetComponentByPath<Text>(aniamtionNode, "languageToggle/toggle_generalLanguage/Label");

            btn_Close.onClick.AddListener(OnClickClose);
            btn_LoginOut.onClick.AddListener(OnClickLoginOut);

            toggle_localLanguage.onValueChanged.AddListener(OnToggleValueChangeLocal);
            toggle_generalLanguage.onValueChanged.AddListener(OnToggleValueChangeGeneral);

            slider_sound.onValueChanged.AddListener(OnSliderValueChangeSound);
            slider_backGroundSound.onValueChanged.AddListener(OnSliderValueChangeBackgroundSound);
        }

        public override void OnEnable()
        {
            CherishTweenScale.Begin(aniamtionNode, Vector3.zero, Vector3.one, 0.2f, 0.0f);

            if (GoableData.UIGameSettingData.locakGenerilLanauge)
            {
                toggle_localLanguage.isOn = true;
            }
            else
            {
                toggle_generalLanguage.isOn = true;
            }


            slider_sound.value = GoableData.UIGameSettingData.soundValue;
            slider_backGroundSound.value = GoableData.UIGameSettingData.backgroundSoundValue;

            toggleNode.SetActive(!UIGameSetting.hiddent);
			btn_LoginOut.gameObject.SetActive(UIGameSetting.showLoginout);
		}


		public void OnClickClose()
        {
			AudioOutManager.SetSoundValue();

			UIGameSetting.hiddent = false;
			UIGameSetting.showLoginout = false;
			//UIMaJiangRall_QuanZhou.PlayAnimationIn();
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIGameSetting_Rall, eCloseType.None);
        }

		public void OnClickLoginOut()
		{
			UIGameSetting.hiddent = false;
			UIGameSetting.showLoginout = false;
			//UIMaJiangRall_QuanZhou.PlayAnimationIn();
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIGameSetting_Rall, eCloseType.None);

			UILogin.ClearLogin();
			if (UserNetWork.HasInstance())
			{
				UserNetWork.Instance.CloseSocket();
			}
			GoableData.ClearnData();
			FrameWorkDrvice.UiManagerInstance.CloseAllUI();
			//无响应 退出到登录界面 需要重连
			FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
		}

		/// <summary>
		/// 音效改变
		/// </summary>
		/// <param name="value"></param>
		public void OnSliderValueChangeSound(float value)
        {            
            FrameWorkDrvice.AudioOutManagerInstance.SetSoundVolume(value);
        }

        /// <summary>
        /// 音效改变
        /// </summary>
        /// <param name="value"></param>
        public void OnSliderValueChangeBackgroundSound(float value)
        {            
            FrameWorkDrvice.AudioOutManagerInstance.SetBackGroundSoundVolume(value);
        }


        public Color GetSelectColor()
        {
            return Color.white;
        }

        public Color GetNoSelectColor()
        {
            return Color.white;
        }

        /// <summary>
        /// 地方话发生改变调用
        /// </summary>
        /// <param name="isSelect"></param>
        public void OnToggleValueChangeLocal(bool isSelect)
        {
            if(isSelect)
            {
                GoableData.UIGameSettingData.locakGenerilLanauge = true;
                togleText_local.color = GetSelectColor();
            }
            else
            {
                GoableData.UIGameSettingData.locakGenerilLanauge = false;
                togleText_local.color = GetNoSelectColor();
            }
        }

        /// <summary>
        /// 普通话发生改变调用
        /// </summary>
        /// <param name="isSelect"></param>
        public void OnToggleValueChangeGeneral(bool isSelect)
        {
            if (isSelect)
            {
                GoableData.UIGameSettingData.locakGenerilLanauge = false;
                togleText_general.color = GetSelectColor();
            }
            else
            {
                GoableData.UIGameSettingData.locakGenerilLanauge = true;
                togleText_general.color = GetNoSelectColor();
            }
        }

		public override void OnClose()
		{
			base.OnClose();

			AudioOutManager.SetSoundValue();
		}

		public override void OnDisable()
        {
            base.OnDisable();

            UIGameSetting.hiddent = false;
			UIGameSetting.showLoginout = false;
		}

        public override void OnDispose()
        {
 	         base.OnDispose();
             UIGameSetting.hiddent = false;
			UIGameSetting.showLoginout = false;
		}

    }
}
