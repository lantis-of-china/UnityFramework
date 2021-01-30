using UnityEngine;
using System.Collections;

namespace UINameSpace
{
    public class UIGameMask : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIGameMask_Rall, _className);
            return 1;
        }


        public UIGameMask()
        {
            mUiShowLayer = eUiShowLayer.UIMask;
            assetsName = Rall.UIDefineName.UIGameMask_Rall;
        }
    }
}
