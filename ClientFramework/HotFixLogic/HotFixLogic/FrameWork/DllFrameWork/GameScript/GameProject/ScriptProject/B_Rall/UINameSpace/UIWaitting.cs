using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UINameSpace
{
    public class UIWaitting : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIWaitting_Rall, _className);
            return 1;
        }


        public UIWaitting()
        {
            mUiShowLayer = eUiShowLayer.UIUrgency;
            assetsName = Rall.UIDefineName.UIWaitting_Rall;
        }

        public static bool openOutTime;

        public GameObject obj_round;
        public override void OnAwake()
        {
            base.OnAwake();
            obj_round = GenericityTool.GetObjectByPath(objectInstance, "imgRound");            
        }

        public override void OnEnable()
        {
            base.OnEnable();

            objectInstance.SetActive(false);
        }


        public override void OnUpdate()
        {
            obj_round.transform.Rotate(new Vector3(0, 0, Time.unscaledDeltaTime * 50.0f));

			if (openOutTime)
			{
				if (codeList.Count > 0)
				{
					waitTime += Time.deltaTime;
					if (waitTime > 10.0f)
					{
						waitTime = 0.0f;
						//超时
						codeList.Clear();
						RefencesShow();

						GoableData.ClearnData();
						//超时处理
						FrameWorkDrvice.UiManagerInstance.CloseAllUI();
						//回到登录界面
						FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
					}
				}
			}
			else
			{
				if (codeList.Count > 0)
				{
					waitTime += Time.deltaTime;
					if (waitTime > 10.0f)
					{
						waitTime = 0.0f;
						//超时
						codeList.Clear();
						RefencesShow();

						GoableData.ClearnData();
						//超时处理
						//FrameWorkDrvice.UiManagerInstance.CloseAllUI();
						//回到登录界面
						//FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
					}
				}
			}
        }






        /// <summary>
        /// 等待面板
        /// </summary>
        public static UIWaitting uiWaitting;
        /// <summary>
        /// 记录的等待
        /// </summary>
        public static List<string> codeList = new List<string>();

        public static float waitTime = 0.0f;

        /// <summary>
        /// 添加一个等待
        /// </summary>
        /// <param name="code"></param>
        public static void AddShowWaitting(string code,bool openUpshow = true)
        {
            codeList.Add(code);
            if (codeList.Count == 1)
            {
                waitTime = 0.0f;
            }

            if (openUpshow)
            {
                RefencesShow();
            }
        }

        /// <summary>
        /// 移除一个等待
        /// </summary>
        /// <param name="code"></param>
        public static void RemoveShowWaitting(string code, bool openUpshow = true)
        {
            bool hasRemove = false;
            for (int i = codeList.Count - 1; i >= 0; --i)
            {
                if(codeList[i] == code)
                {
                    hasRemove = true;
                    codeList.RemoveAt(i);
                    break;
                }
            }

            if(codeList.Count == 0)
            {
                waitTime = 0.0f;
            }

            //for (int i = 0; i < codeList.Count; ++i)
            //{
            //    DebugLoger.Log("RemoveShowWaitting rem " + codeList[i]);
            //}
            if (hasRemove && openUpshow || codeList.Count == 0)
            {
                RefencesShow();
            }
        }

        /// <summary>
        /// 清理全部遮挡
        /// </summary>
        public static void ClearAll()
        {
            codeList.Clear();
            RefencesShow();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public static void RefencesShow()
        {
            uiWaitting = null;

            if (uiWaitting == null || !uiWaitting.isOpen)
            {
                uiWaitting = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIWaitting_Rall) as UIWaitting;
            }
            if(codeList.Count > 0)
            {
                //显示
                if (uiWaitting != null && uiWaitting.isOpen && uiWaitting.objectInstance != null && !uiWaitting.objectInstance.activeSelf)
                {
                    uiWaitting.objectInstance.SetActive(true);
                    //CherishTweenScale.Begin(uiWaitting.objectInstance, Vector3.zero, Vector3.one, 0.2f, 0.0f);
                }
            }
            else
            {
                //隐藏
                if (uiWaitting != null && uiWaitting.isOpen && uiWaitting.objectInstance != null && uiWaitting.objectInstance.activeSelf)
                {
                    uiWaitting.objectInstance.SetActive(false);
                }
            }
        }
    }
}
