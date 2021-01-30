using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈规则
    /// </summary>
    public class ClubRuleItem
    {
        /// <summary>
        /// 规则节点
        /// </summary>
        public GameObject itemNode;
        /// <summary>
        /// 头像节点
        /// </summary>
        public Image imgHead;
        /// <summary>
        /// 名字
        /// </summary>
        public Text txt_name;
        /// <summary>
        /// 规则
        /// </summary>
        public Text txt_rule;
        /// <summary>
        /// 设置
        /// </summary>
        public Button btn_setting;
        /// <summary>
        /// 绑定的设置
        /// </summary>
        public P_GameSetting bindSetting;

        /// <summary>
        /// 获取到
        /// </summary>
        public void GetUI(GameObject node)
        {
            itemNode = node;
            imgHead = GenericityTool.GetComponentByPath<Image>(itemNode, "imgIcon");
            txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_name");
            txt_rule = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_rule");
            btn_setting = GenericityTool.GetComponentByPath<Button>(itemNode, "btnSetting");

            btn_setting.onClick.AddListener(OpenRuleSetting);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="setting"></param>
        public void Show(P_GameSetting setting)
        {
            bindSetting = setting;
            AssetsParkManager.SetUguiImageThingIcon(ConfigProject.iconsName, imgHead, "icon_game_" + bindSetting.gameType);

            GameEntryItem gameEntry = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntryWithGameType(bindSetting.gameType);
            if (gameEntry == null)
            {
                UINameSpace.UITipMessage.PlayMessage("未找到对应的游戏设置功能!");
                return;
            }
            txt_rule.text = bindSetting.roomValue + "局"+ gameEntry.callGetParmarsStr(bindSetting.pamarasSetting);
            txt_name.text = gameEntry.gameName;
		}


        /// <summary>
        /// 设置父节点
        /// </summary>
        public void SetParent(Transform parent)
        {
            itemNode.transform.parent = parent;
            itemNode.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }

        public void OpenRuleSetting()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			if (IMClub.ClubItem.clubItemState == null || bindSetting == null)
            {
                UINameSpace.UITipMessage.PlayMessage("系统错误，请重启后再试!");
                return;
            }
            //if (bindSetting.gameType == 11)
            //{
            //    UINameSpace.UISetRoomCheXuan_IMClub.bindGameSetting = bindSetting;
            //    FrameWorkDrvice.UiManagerInstance.OpenUI(IMClub.ConfigProject.projectFloderName, UIDefineName.UISettingRoomCheXuan_IMClub, true);
            //}

            GameEntryItem gameEntry = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntryWithGameType(bindSetting.gameType);
            if (gameEntry == null)
            {
                UINameSpace.UITipMessage.PlayMessage("未找到对应的游戏设置功能!");
                return;
            }

            List<byte> setPar = new List<byte>();
            setPar.Add(bindSetting.roomValue);
            for (int i = 0; i < bindSetting.pamarasSetting.Count; ++i)
            {
                setPar.Add((byte)bindSetting.pamarasSetting[i]);
            }
            gameEntry.callInitCreateParamarFun(IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, setPar);
            FrameWorkDrvice.UiManagerInstance.OpenUI(gameEntry.assetFloder, gameEntry.uiNameCreateRoom, true);
        }
    }

    /// <summary>
    /// 亲友圈游戏规则
    /// </summary>
    public class ClubRuleSettingPanel_Select : TablePanelItem
    {
        public static ClubRuleSettingPanel_Select Instance;
        /// <summary>
        /// 亲友圈规则列表
        /// </summary>
        public static List<ClubRuleItem> clubRuleItemList = new List<ClubRuleItem>();
        /// <summary>
        /// 实例资源
        /// </summary>
        public GameObject itemSource;
        /// <summary>
        /// 添加新数据
        /// </summary>
        public Button btn_addNew;
        /// <summary>
        /// 绑定亲友圈设置
        /// </summary>
        public P_ClubSetting bindClubSetting;

        public void ResetShow()
        {
            DeleteItems();
            AddGameRulePanel.SetActive(false);
            bindClubSetting = ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting;
            ShowItems();
        }

        /// <summary>
        /// 获取面包信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
            Instance = this;

            itemSource = GenericityTool.GetObjectByPath(tablePanel, "listNode/ruleItem");

            btn_addNew = GenericityTool.GetComponentByPath<Button>(tablePanel, "btnAddNewRule");

            btn_addNew.onClick.AddListener(OnClickAddNewRule);

            itemSource.SetActive(false);

            AddGameRulePanel.GetUI(GenericityTool.GetObjectByPath(tablePanel, "selectRulePanel"));
        }

        /// <summary>
        /// 选中了这个Item
        /// </summary>
        public override void OnSelect()
        {
            base.OnSelect();
            ResetShow();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void OnUnSelect()
        {
            base.OnUnSelect();

            DeleteItems();
        }


        /// <summary>
        /// 外部调用接口
        /// </summary>
        /// <param name="parmaras"></param>
        public override void ExitCall(object parmaras)
        {
            base.ExitCall(parmaras);
        }


        /// <summary>
        /// 获取规则节点
        /// </summary>
        /// <returns></returns>
        public ClubRuleItem GetClubRuleItem()
        {
            GameObject itemNode = GameObject.Instantiate(itemSource);
            ClubRuleItem clubRuleItem = new ClubRuleItem();
            clubRuleItem.GetUI(itemNode);
            return clubRuleItem;
        }


        /// <summary>
        /// 显示列表
        /// </summary>
        public void ShowItems()
        {
			if (ClubItem.clubItemState == null)
			{
				DebugLoger.Log("1");
			}
			if (ClubItem.clubItemState.bindGwInfo == null)
			{
				DebugLoger.Log("2");
			}
			if (ClubItem.clubItemState.bindGwInfo.groupInfo == null)
			{
				DebugLoger.Log("3");
			}
			if (ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting == null)
			{
				DebugLoger.Log("4");
			}
			if (ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting.gamesSetting == null)
			{
				DebugLoger.Log("5");
			}
			for (int i = 0; i < ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting.gamesSetting.Count;++i)
            {
                ClubRuleItem clubMenberItem = GetClubRuleItem();
                clubRuleItemList.Add(clubMenberItem);
                clubMenberItem.Show(ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting.gamesSetting[i]);
                clubMenberItem.SetParent(itemSource.transform.parent);
                clubMenberItem.SetActive(true);
            }
        }

        /// <summary>
        /// 删除其他东西
        /// </summary>
        public void DeleteItems()
        {
            for (int i = 0; i < clubRuleItemList.Count; ++i)
            {
                GameObject.Destroy(clubRuleItemList[i].itemNode);
            }
            clubRuleItemList.Clear();
        }
                
        /// <summary>
        /// 添加新的规则
        /// </summary>
        public void OnClickAddNewRule()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			AddGameRulePanel.Open(bindClubSetting);
        }   
    }
}


