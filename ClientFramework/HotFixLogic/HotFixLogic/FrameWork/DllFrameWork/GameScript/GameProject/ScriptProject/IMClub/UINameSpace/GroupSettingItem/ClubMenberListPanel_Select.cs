using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈成员
    /// </summary>
    public class ClubMenberItem
    {
        /// <summary>
        /// 黑名单节点
        /// </summary>
        public GameObject itemNode;
        /// <summary>
        /// 成员名
        /// </summary>
        public Text txt_menberName;
        /// <summary>
        /// 成员ID
        /// </summary>
        public Text txt_menberId;
        /// <summary>
        /// 成员分数
        /// </summary>
        public Text txt_menberScore;
        /// <summary>
        /// 成员状态
        /// </summary>
        public Text txt_menberState;
        /// <summary>
        /// 移除成员
        /// </summary>
        public Button btn_remove;
        /// <summary>
        /// 恢复成员
        /// </summary>
        public Button btn_reset;
        /// <summary>
        /// 编辑器
        /// </summary>
        public Button btn_editor;
        /// <summary>
        /// 绑定成员
        /// </summary>
        public P_Menber bindMenber;
        /// <summary>
        /// 绑定的亲友圈ID
        /// </summary>
        public string bindClubId;

        /// <summary>
        /// 获取UI
        /// </summary>
        public void GetUI(GameObject item)
        {
            itemNode = item;
            txt_menberName = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberName");
            txt_menberId = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberId");
            txt_menberScore = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberScore");
            txt_menberState = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_menberState");
            btn_remove = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_addblack");
            btn_reset = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_removeblack");
            btn_editor = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_editor");

            btn_editor.onClick.AddListener(OnClickEditor);
            btn_remove.onClick.AddListener(OnClickRemove);
            btn_reset.onClick.AddListener(OnClickReset);
        }

        /// <summary>
        /// 点击编辑竞技分
        /// </summary>
        private void OnClickEditor()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			ClubScoreSettingPanel.SetDataShow(bindClubId,bindMenber);
        }

        /// <summary>
        /// 点击移除成员
        /// </summary>
        private void OnClickRemove()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.CS_AddBlackList_MsgType");
            MessageSend.AddBlackMenber(bindClubId, bindMenber.menberId);
        }

        /// <summary>
        /// 漂白
        /// </summary>
        private void OnClickReset()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			UINameSpace.UITipMessage.PlayMessage("暂未开通!");
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
        /// 显示
        /// </summary>
        public void Show(string clubId,P_Menber menber)
        {
            bindClubId = clubId;
            bindMenber = menber;
            txt_menberName.text = bindMenber.menberName;
            txt_menberId.text = bindMenber.menberId.ToString();
            txt_menberScore.text = bindMenber.Score.ToString();
            if (menber.black == 0)
            {
                txt_menberState.text = "正常";
                txt_menberState.color = Color.green;
                btn_remove.gameObject.SetActive(true);
                btn_reset.gameObject.SetActive(false);
            }
            else
            {
                txt_menberState.text = "黑名单";
                txt_menberState.color = Color.red;
                btn_remove.gameObject.SetActive(false);
                btn_reset.gameObject.SetActive(true);
            }
            itemNode.SetActive(true);

			if (bindMenber.credit == 1)
			{
				txt_menberScore.color = Color.white;
			}
			else
			{
				txt_menberScore.color = Color.red;
			}
		}

        /// <summary>
        /// 设置显示
        /// </summary>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }
    }

    /// <summary>
    /// 亲友圈黑名单
    /// </summary>
    public class ClubMenberListPanel_Select : TablePanelItem
    {
        public static ClubMenberListPanel_Select Instance;
        /// <summary>
        /// 亲友圈成员列表
        /// </summary>
        public List<ClubMenberItem> clubMenberList = new List<ClubMenberItem>();
        /// <summary>
        /// 用户节点资源显示
        /// </summary>
        public GameObject menberItemSource;
        /// <summary>
        /// 查找
        /// </summary>
        public Button btnFind;
        /// <summary>
        /// 查找输入
        /// </summary>
        public InputField input_findClub;

        public void ResetShow()
        {
            OnUnSelect();
            ShowItems();
        }

        /// <summary>
        /// 获取面包信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
            Instance = this;

            menberItemSource = GenericityTool.GetObjectByPath(tablePanel, "Scroll View/Viewport/Content/itemSource");
            menberItemSource.SetActive(false);

            btnFind = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_find");

            input_findClub = GenericityTool.GetComponentByPath<InputField>(tablePanel, "input_menberId");

            input_findClub.onValueChanged.AddListener(OnValueChange);
        }

        /// <summary>
        /// 字符串改变
        /// </summary>
        /// <param name="text"></param>
        private void OnValueChange(string text)
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			if (string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < clubMenberList.Count; ++i)
                {
                    clubMenberList[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < clubMenberList.Count; ++i)
                {
                    if (clubMenberList[i].bindMenber.menberId.ToString().Contains(text))
                    {
                        clubMenberList[i].SetActive(true);
                    }
                    else
                    {
                        clubMenberList[i].SetActive(false);
                    }
                }
            }
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
        /// 获取成员Item
        /// </summary>
        /// <returns></returns>
        public ClubMenberItem GetMenberItem()
        {
            GameObject clubItem = GameObject.Instantiate(menberItemSource);
            ClubMenberItem clubMenberItem = new ClubMenberItem();
            clubMenberItem.GetUI(clubItem);
            return clubMenberItem;
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        public void ShowItems()
        {
            input_findClub.text = "";

            foreach(var kv in ClubItem.clubItemState.bindGwInfo.menberList)
            {
                ClubMenberItem clubMenberItem = GetMenberItem();
                clubMenberList.Add(clubMenberItem);
                clubMenberItem.Show(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, kv.Value);
                clubMenberItem.SetParent(menberItemSource.transform.parent);
            }
        }

        /// <summary>
        /// 删除其他东西
        /// </summary>
        public void DeleteItems()
        {
            for(int i = 0;i < clubMenberList.Count;++i)
            {
                GameObject.Destroy(clubMenberList[i].itemNode);
            }

            clubMenberList.Clear();
        }



        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="menberId"></param>
        public void UpScore(int menberId)
        {
            for (int i = 0; i < clubMenberList.Count; ++i)
            {
                if (clubMenberList[i].bindMenber.menberId == menberId)
                {
                    clubMenberList[i].Show(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, clubMenberList[i].bindMenber);
                    break;
                }
            }
        }

        /// <summary>
        /// 移除成员
        /// </summary>
        /// <param name="menberId"></param>
        public void Remove(int menberId)
        {
            for (int i = 0; i < clubMenberList.Count; ++i)
            {
                if (clubMenberList[i].bindMenber.menberId == menberId)
                {
                    GameObject.Destroy(clubMenberList[i].itemNode);
                    clubMenberList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}


