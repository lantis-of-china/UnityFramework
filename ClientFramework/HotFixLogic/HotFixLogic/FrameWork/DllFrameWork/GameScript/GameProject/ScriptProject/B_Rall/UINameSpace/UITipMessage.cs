using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace UINameSpace
{
    public class UITipMessage : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UITipMessge_Rall, _className);
            return 1;
        }


        public UITipMessage()
        {
            DebugLoger.Log("UITipMessage ctor entry");
            mUiShowLayer = eUiShowLayer.UIUrgency;
            assetsName = Rall.UIDefineName.UITipMessge_Rall;
        }

        /// <summary>
        /// 提示消息节点
        /// </summary>
        public GameObject tipNode;
        /// <summary>
        /// 背景
        /// </summary>
        public Image backImage;
        /// <summary>
        /// 消息内容
        /// </summary>
        public Text lb_text;


        public override void OnAwake()
        {
            tipNode = GenericityTool.GetObjectByPath(objectInstance, "AnchorNode/tipNode");
            backImage = GenericityTool.GetComponentByPath<Image>(tipNode, "imgBack");
            lb_text = GenericityTool.GetComponentByPath<Text>(tipNode, "lb_info");
        }

        /// <summary>
        /// 播放消息
        /// </summary>
        public void PlayMessageAnimation(string msg)
        {
            ToBestLayer();

            lb_text.text = msg;
            backImage.rectTransform.sizeDelta = new Vector2(lb_text.preferredWidth + 30.0f, 100);

            CherishTweenMove.Begin(tipNode, Vector3.zero, new Vector3(0.0f, 600.0f, 0.0f), 0.2f, 0.0f, true);
            CherishTweenAlpha.Begin(tipNode, 1.0f, 0.0f, 0.5f, 1.5f, true);
        }


        private static UITipMessage instance;

        /// <summary>
        /// 开启
        /// </summary>
        public static void OpenTip()
        {
            FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UITipMessge_Rall, true);
        }
        /// <summary>
        /// 播放消息
        /// </summary>
        /// <param name="msg"></param>
        public static void PlayMessage(string msg)
        {
            if(instance == null)
            {
                instance = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UITipMessge_Rall) as UITipMessage;
            }

            instance.PlayMessageAnimation(msg);
        }
    }
}
