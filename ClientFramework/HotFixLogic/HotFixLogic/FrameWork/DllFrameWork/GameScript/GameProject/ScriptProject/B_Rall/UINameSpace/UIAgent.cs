using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UINameSpace
{
    public class UIAgent : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIAgent_Rall, _className);
            return 1;
        }

        public UIAgent()
        {
            assetsName = Rall.UIDefineName.UIAgent_Rall;
        }

		private Button btn_close;
		private Button btn_copy;
		private Button btn_submit;
		private InputField input_phone;
		private Text lb_weichat;
		public GameObject animationNode;

		public override void OnAwake()
        {
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");
            btn_close = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_close");
			btn_copy = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_copy");
			btn_submit = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_submit");
			input_phone = GenericityTool.GetComponentByPath<InputField>(animationNode, "input_phone");
			lb_weichat = GenericityTool.GetComponentByPath<Text>(animationNode, "lb_weichat");
			
			btn_close.onClick.AddListener(OnClickClose);
			btn_copy.onClick.AddListener(OnClickCopy);
			btn_submit.onClick.AddListener(OnClickSubmit);
		}

		public override void OnEnable()
		{
			lb_weichat.text = StringConfigClass.weiChatNumber;
		}

		private void OnClickCopy()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			CherishUtility.SetClipboard(StringConfigClass.weiChatNumber);
			UINameSpace.UITipMessage.PlayMessage("复制成功!");
		}

		private void OnClickSubmit()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (string.IsNullOrEmpty(input_phone.text) || input_phone.text.Length != 11)
			{
				UINameSpace.UITipMessage.PlayMessage("手机号输入不正确!");
				return;
			}

			if ((!Regex.IsMatch(input_phone.text, @"^1[34578]\d{9}$", RegexOptions.IgnoreCase)))
			{
				UINameSpace.UITipMessage.PlayMessage("手机号输入不正确!");
				return;
			}

			Rall.MessageSend.AgentRequet(input_phone.text);

			CloseCurUI();
		}

		private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			CloseCurUI();
		}

		private void CloseCurUI()
		{
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIAgent_Rall, eCloseType.None);
		}
    }
}
