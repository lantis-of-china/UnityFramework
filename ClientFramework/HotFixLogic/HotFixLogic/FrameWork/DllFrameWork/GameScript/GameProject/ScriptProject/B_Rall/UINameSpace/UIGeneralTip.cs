using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace UINameSpace
{
    public class UIGeneralTip : UIObject
    {
		private static UIGeneralTip __Instance;
		public static UIGeneralTip GetInstance()
		{
			return __Instance;
		}
		public override void SetInstance(UIObject target)
		{
			__Instance = target as UIGeneralTip; ;
		}

		private Button btn_close = null;
		private Button btn_submit = null;
		public GameObject animationNode;
		public Text lb_title;
        public Text lb_text;

		public string valueTitle;
		public string valueContent;
		public object valueParamar;
		public System.Action<object> callFun_Submit;


        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIGeneralTip_Rall, _className);
            return 1;
        }

        public UIGeneralTip()
        {
			mUiShowLayer = eUiShowLayer.UIUrgency;
			assetsName = Rall.UIDefineName.UIGeneralTip_Rall;
        }

        public override void OnAwake()
        {
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");
            btn_close = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_close");
			btn_submit = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_submit");

			btn_close.onClick.AddListener(OnClickClose);
			btn_submit.onClick.AddListener(OnClickSubmit);

			lb_title = GenericityTool.GetComponentByPath<Text>(animationNode, "txt_title");
			lb_text = GenericityTool.GetComponentByPath<Text>(animationNode, "txt_content");
		}
		       
        public override void OnEnable()
        {
			if (string.IsNullOrEmpty(valueTitle))
			{
				CloseClear();
			}
			else
			{
				lb_title.text = valueTitle;
				lb_text.text = valueContent;
			}
		}

		private void OnClickClose()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			CloseClear();
		}

		private void OnClickSubmit()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (callFun_Submit != null)
			{
				callFun_Submit(valueParamar);
			}
			CloseClear();
		}

		private void CloseClear()
		{
			valueTitle = "";
			valueContent = "";
			valueParamar = null;
			callFun_Submit = null;
			objectInstance.SetActive(false);
		}

		public override void OnDisable()
		{
			CloseClear();
		}

		public static void ShowTip(string title, string content, System.Action<object> callFun, object paramar)
		{
			UIObject ui = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIGeneralTip_Rall);
			if (ui == null)
			{
				FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGeneralTip_Rall, true);
			}
			else
			{
				UIGeneralTip generalTip = ui as UIGeneralTip;
				generalTip.valueTitle = title;
				generalTip.valueContent = content;
				generalTip.valueParamar = paramar;
				generalTip.callFun_Submit = callFun;

				generalTip.objectInstance.SetActive(true);
				generalTip.OnEnable();
			}
		}
	}
}
