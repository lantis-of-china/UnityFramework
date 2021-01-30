using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace UINameSpace
{
    public class UIBuyTip : UIObject
    {
        private Button btn_close = null;
        public GameObject animationNode;
        public Text lb_text;
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIBuyTip_Rall, _className);
            return 1;
        }


        public UIBuyTip()
        {
            assetsName = Rall.UIDefineName.UIBuyTip_Rall;
        }



        public override void OnAwake()
        {
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");
            btn_close = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_close");
            btn_close.onClick.AddListener(OnClickClose);
            lb_text = GenericityTool.GetComponentByPath<Text>(animationNode, "Text");
            if (!StringConfigClass.CanOpenHiddent())
            {
                lb_text.text = "暂无客服！";
            }
            else
            {
				lb_text.text = "代理、亲友圈相关问题\n请咨询" + "\n客服微信:" + StringConfigClass.weiChatNumber;
				//lb_text.text = "如有需要联系客服\nQQ " + StringConfigClass.qqNumber + "\n微信 " + StringConfigClass.weiChatNumber;
			}
        }

        private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			//UIMaJiangRall_QuanZhou.PlayAnimationIn();
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIBuyTip_Rall, eCloseType.None);
        }
       
        public override void OnEnable()
        {
            //UIMaJiangRall_QuanZhou.PlayAnimationOut();
            CherishTweenMove.Begin(animationNode, new Vector3(0,688,0),Vector3.zero, 0.2f, 0.2f,true);
        }

        public override void OnUpdate()
        {

        }

        public override void OnLateUpdate()
        {

        }

        public override void OnDisable()
        {
        }

        public override void OnDispose()
        {
            
        }       
    }
}
