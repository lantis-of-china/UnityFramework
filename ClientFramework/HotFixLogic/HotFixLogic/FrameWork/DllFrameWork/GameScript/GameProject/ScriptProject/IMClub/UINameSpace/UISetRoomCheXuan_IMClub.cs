using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;




namespace UINameSpace
{
    public class UISetRoomCheXuan_IMClub : UIObject
    {
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(IMClub.UIDefineName.UISettingRoomCheXuan_IMClub, _className);

            return 1;
        }

        public UISetRoomCheXuan_IMClub()
        {
            assetsName = IMClub.UIDefineName.UISettingRoomCheXuan_IMClub;
        }

        public GameObject animationNode;
        public Button btnBack;
        public Button btnSubmit;
        /// <summary>
        /// 表单列表
        /// </summary>
        public List<TablePanelItem> tablePanelList = new List<TablePanelItem>();

		public static IMClub.P_GameSetting bindGameSetting;
        
		public override void OnAwake()
        {
			animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");
			btnBack = GenericityTool.GetComponentByPath<Button>(animationNode, "btnClose");
			btnSubmit = GenericityTool.GetComponentByPath<Button>(animationNode, "btnCreate");
            
           btnBack.onClick.AddListener(OnClickClose);
           btnSubmit.onClick.AddListener(OnSubmit);

           for (int i = 0; i < 2; ++i)
           {
               TablePanelItem tablePanelKv = null;
				if (i == 0)
				{
					tablePanelKv = new CheXuanSet_Select();
					CheXuanSet_Select curPanel = (tablePanelKv as CheXuanSet_Select);
					curPanel.SetParamars(bindGameSetting.roomValue,bindGameSetting.pamarasSetting);
				}
				else if (i == 1)
				{
					tablePanelKv = new CheXuanMangQuanSet_Select();
					CheXuanMangQuanSet_Select curPanel = (tablePanelKv as CheXuanMangQuanSet_Select);
					curPanel.SetParamars(bindGameSetting.roomValue,bindGameSetting.pamarasSetting);
				}

               tablePanelList.Add(tablePanelKv);
               tablePanelKv.tag = "IMClubSetRomPanel";
               tablePanelKv.index = i;
               tablePanelKv.tableButton = GenericityTool.GetComponentByPath<Button>(animationNode, "tableList/table_" + i.ToString());
               tablePanelKv.tablePanel = GenericityTool.GetObjectByPath(animationNode, "panelList/panel_" + i.ToString());
               tablePanelKv.RegistListen();
           }

           //TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("IMClubSetRomPanel");
           //firstTablePanel.SelectPanel();
        }
        
        public override void OnEnable()
        {
			if (bindGameSetting.pamarasSetting[5] == 0 && bindGameSetting.pamarasSetting[6] == 0)
			{
				tablePanelList[0].SelectPanel();
			}
			else
			{
				tablePanelList[1].SelectPanel();
			}
			//UICheXuanRall.PlayAnimationOut();
			//CherishTweenScale.Begin(animationNode, Vector3.zero, Vector3.one, 0.2f, 0.2f);
		}
        
        private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			CloseUI();
        }

        private void OnSubmit()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			TablePanelItem selectTab = TablePanelItem.GetSelectTablePanelWithTag("IMClubSetRomPanel");
            selectTab.ExitCall(0);
            CloseUI();
        }

        public void CloseUI()
        {
            //UICheXuanRall.PlayAnimationIn();
            FrameWorkDrvice.UiManagerInstance.CloseUI(IMClub.UIDefineName.UISettingRoomCheXuan_IMClub, eCloseType.Queue);
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
