using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class UIShowTipItem
{
    public GameObject objectInstance;

    public Text lb_title;
    public Text lb_content;


    public void GetCompont(GameObject node)
    {
        objectInstance = node;
        lb_title = GenericityTool.GetComponentByPath<Text>(node, "Text_table");
        lb_content = GenericityTool.GetComponentByPath<Text>(node, "Text_Con");
    }

    public void SetActive(bool active)
    {
        objectInstance.SetActive(active);
    }

    public void SetValue(Server.P_MsgInfo info)
    {
        lb_title.text = info.title;
        lb_content.text = info.msg; 
    }
}

namespace UINameSpace
{
    public class UIShowTipMsg : UIObject
    {
        public GameObject aniamtionNode;

        private Button btn_close = null;

        private List<UIShowTipItem> tipItemList = new List<UIShowTipItem>();

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIShowTipMsg_Rall, _className);
            return 1;
        }


        public UIShowTipMsg()
        {
            assetsName = Rall.UIDefineName.UIShowTipMsg_Rall;
        }



        public override void OnAwake()
        {
            aniamtionNode = GenericityTool.GetObjectByPath(objectInstance, "animationNode");
            btn_close = GenericityTool.GetComponentByPath<Button>(aniamtionNode, "btn_close");
            btn_close.onClick.AddListener(OnClickClose);
            GameObject contentNode = GenericityTool.GetObjectByPath(aniamtionNode,"Scroll View/Viewport/Content");
            for(int i = 0; i < 9;++i)
            {
                UIShowTipItem itemInfo = new UIShowTipItem();
                itemInfo.GetCompont(GenericityTool.GetObjectByPath(contentNode, "Item_" + i));
                tipItemList.Add(itemInfo);
            }
        }

        private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIShowTipMsg_Rall, eCloseType.None);
        }
       
        public override void OnEnable()
        {
            for(int i = 0; i <tipItemList.Count;++i)
            {
                tipItemList[i].SetActive(false);
            }
            if (GoableData.UISystemMsg.systemMsgList != null)
            {
                for (int i = 0; i < GoableData.UISystemMsg.systemMsgList.Count; ++i)
                {
                    if (i < tipItemList.Count)
                    {
                        tipItemList[i].SetValue(GoableData.UISystemMsg.systemMsgList[i]);
                        tipItemList[i].SetActive(true);
                    }
                }
            }
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
