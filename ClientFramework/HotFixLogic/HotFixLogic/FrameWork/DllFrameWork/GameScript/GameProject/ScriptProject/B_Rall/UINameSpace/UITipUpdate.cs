using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UINameSpace
{
    public class UITipUpdate : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UITipUpdate_Rall, _className);
            return 1;
        }


        public UITipUpdate()
        {
            mUiShowLayer = eUiShowLayer.UIUrgency;
            assetsName = Rall.UIDefineName.UITipUpdate_Rall;
        }



        public static string downloadUrl = "";
        public Button btnUpdate;
        public GameObject animationNode;
        public override void OnAwake()
        {
            base.OnAwake();
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "achorNode/animationNode");
            btnUpdate = GenericityTool.GetComponentByPath<Button>(animationNode, "btn_update");

            btnUpdate.onClick.AddListener(OnClickUpdate);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            CherishTweenScale.Begin(animationNode, Vector3.zero, Vector3.one, 0.2f, 0.2f);
        }


        public void OnClickUpdate()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			Application.OpenURL(downloadUrl);
        }

        public static void SetDownloadUrl(string url)
        {
            downloadUrl = url;
        }
    }
}
