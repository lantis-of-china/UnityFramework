using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UINameSpace
{

    public class UIRankList : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIRankList_Rall, _className);
            return 1;
        }

        public UIRankList()
        {
            assetsName = Rall.UIDefineName.UIRankList_Rall;
        }

        public Button btnClose;

        public override void OnAwake()
        {
            base.OnAwake();

            btnClose = GenericityTool.GetComponentByPath<Button>(objectInstance, "animationNode/btn_close");
            btnClose.onClick.AddListener(CloseUI);
        }

        public void CloseUI()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIRankList_Rall,eCloseType.Queue);
        }
    }
}
