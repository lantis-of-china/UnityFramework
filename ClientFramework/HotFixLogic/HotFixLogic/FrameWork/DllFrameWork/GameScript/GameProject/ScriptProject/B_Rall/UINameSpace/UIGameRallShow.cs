using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UINameSpace
{
    public class UIGameRallShow : UIObject
    {
		public static bool isOpen = true;

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIGameRallShow_Rall, _className);
            return 1;
        }

        public UIGameRallShow()
        {
            assetsName = Rall.UIDefineName.UIGameRallShow_Rall;
        }

		
		private Button btn_close;
		public GameObject animationNode;

		public override void OnAwake()
        {
			animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");
            btn_close = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_close");
			
			btn_close.onClick.AddListener(OnClickClose);
		}

		public override void OnEnable()
		{
			isOpen = true;
			animationNode.transform.localScale = Vector3.zero;
			CherishTweenScale.Begin(animationNode, Vector3.zero, Vector3.one, 0.3f, 1.0f);
		}


		private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			CloseCurUI();
		}

		private void CloseCurUI()
		{
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIGameRallShow_Rall, eCloseType.None);
		}
    }
}
