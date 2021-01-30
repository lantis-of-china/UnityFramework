using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UINameSpace
{
    public class UIRealName : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIRealName_Rall, _className);
            return 1;
        }

        public UIRealName()
        {
            assetsName = Rall.UIDefineName.UIRealName_Rall;
        }


		private Button btn_close = null;
		public Button btn_save;
		public GameObject animationNode;
		public InputField input_name;
		public InputField input_id;
		public InputField input_phone;

		public override void OnAwake()
        {
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");
            btn_close = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_close");
            btn_close.onClick.AddListener(OnClickClose);

			btn_save = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_save");
			btn_save.onClick.AddListener(OnClickSave);

			input_name = GenericityTool.GetComponentByPath<InputField>(animationNode, "input_name");
			input_id = GenericityTool.GetComponentByPath<InputField>(animationNode, "input_id");
			input_phone = GenericityTool.GetComponentByPath<InputField>(animationNode, "input_phone");
		}

        private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIRealName_Rall, eCloseType.None);
        }

		private void OnClickSave()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (string.IsNullOrEmpty(input_name.text) || input_name.text.Length < 2 || input_name.text.Length > 5)
			{
				UINameSpace.UITipMessage.PlayMessage("名字输入不正确!");
				return;
			}

			if (string.IsNullOrEmpty(input_id.text) || input_id.text.Length != 18)
			{
				UINameSpace.UITipMessage.PlayMessage("身份证号输入不正确!");
				return;
			}

			if (string.IsNullOrEmpty(input_phone.text) || input_phone.text.Length != 11)
			{
				UINameSpace.UITipMessage.PlayMessage("手机号输入不正确!");
				return;
			}


			if ((!Regex.IsMatch(input_id.text, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))
			{
				UINameSpace.UITipMessage.PlayMessage("身份证号错误!"); 
				return;
			}

			if ((!Regex.IsMatch(input_phone.text, @"^1[34578]\d{9}$", RegexOptions.IgnoreCase)))
			{
				UINameSpace.UITipMessage.PlayMessage("手机号输入不正确!");
				return;
			}

			//这里全部正确 发送
			Rall.MessageSend.RealName(input_id.text, input_name.text, input_phone.text);
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIRealName_Rall, eCloseType.None);
		}
       
        public override void OnEnable()
        {
            CherishTweenMove.Begin(animationNode, new Vector3(0,688,0),Vector3.zero, 0.2f, 0.2f,true);
        } 
    }
}
