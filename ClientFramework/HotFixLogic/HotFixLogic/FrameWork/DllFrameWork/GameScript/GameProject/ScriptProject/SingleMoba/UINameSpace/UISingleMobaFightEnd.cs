using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SingleMoba;

namespace UINameSpace
{    
    public class UISingleMobaFightEnd : UIObject
    {
        private static UISingleMobaFightEnd __Instance;

        public static UISingleMobaFightEnd GetInstance()
        {
            return __Instance;
        }

        public override void SetInstance(UIObject target)
        {
            __Instance = target as UISingleMobaFightEnd;
        }

		/// <summary>
		/// 反射调用的注册方法
		/// </summary>
		/// <param name="_className"></param>
		public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(UIDefineName.UIFightEnd, _className);

            return 1;
        }

        private Button buttonClose;

        public UISingleMobaFightEnd()
        {
            assetsName = UIDefineName.UIFightEnd;
        }

        public override void OnAwake()
        {
            buttonClose = GenericityTool.GetComponentByPath<Button>(objectInstance, "buttonClose");
            buttonClose.onClick.AddListener(OnClickExit);
        }

        public override void OnEnable()
        {
            base.OnEnable();        
        }

        public void OnClickExit()
        {
            Close();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            var rallInstance = UINameSpace.UISingleMobaRall.GetInstance();

            if (rallInstance != null)
            {
                rallInstance.ToBestLayer();
            }
        }
    }
}
