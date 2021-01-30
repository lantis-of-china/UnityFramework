using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈黑名单
    /// </summary>
    public class ClubBlackItem
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
        /// 移除成员
        /// </summary>
        public Button btn_Remove;
        /// <summary>
        /// 绑定对象
        /// </summary>
        public P_Menber bindMenber;

        /// <summary>
        /// 获取UI
        /// </summary>
        public void GetUI(GameObject item)
        {
            itemNode = item;
            txt_menberName = GenericityTool.GetComponentByPath<Text>(itemNode,"txt_menberName");
            txt_menberId = GenericityTool.GetComponentByPath<Text>(itemNode,"txt_menberId");
            txt_menberScore = GenericityTool.GetComponentByPath<Text>(itemNode,"txt_menberScore");
            btn_Remove = GenericityTool.GetComponentByPath<Button>(itemNode,"btn_remove");
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
        public void Show(P_Menber menber)
        {
            bindMenber = menber;
            txt_menberName.text = bindMenber.menberName;
            txt_menberId.text = bindMenber.menberId.ToString();
            txt_menberScore.text = bindMenber.Score.ToString();

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
        /// 设置激活
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }
    }

    /// <summary>
    /// 亲友圈成员列表
    /// </summary>
    public class ClubBlackListPanel_Select : TablePanelItem
    {
        public static ClubBlackListPanel_Select Instance;
        /// <summary>
        /// 黑名单
        /// </summary>
        public List<ClubBlackItem> clubBlackList = new List<ClubBlackItem>();
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
            Instance = this;
            base.OnGetPanelInfo();

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
                for (int i = 0; i < clubBlackList.Count; ++i)
                {
                    clubBlackList[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < clubBlackList.Count; ++i)
                {
                    if (clubBlackList[i].bindMenber.menberId.ToString().Contains(text))
                    {
                        clubBlackList[i].SetActive(true);
                    }
                    else
                    {
                        clubBlackList[i].SetActive(false);
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
        /// 获取亲友圈节点数据
        /// </summary>
        /// <param name="clubItem"></param>
        /// <returns></returns>
        public ClubBlackItem GetClubBlackItem()
        {
            GameObject clubItem = GameObject.Instantiate(menberItemSource);
            ClubBlackItem clubBlackItem = new ClubBlackItem();
            clubBlackItem.GetUI(clubItem);
            return clubBlackItem;
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        public void ShowItems()
        {
            input_findClub.text = "";
            foreach (var kv in ClubItem.clubItemState.bindGwInfo.menberList)
            {
                ClubBlackItem clubMenberItem = GetClubBlackItem();
                clubBlackList.Add(clubMenberItem);
                clubMenberItem.Show(kv.Value);
                clubMenberItem.SetParent(menberItemSource.transform.parent);
            }
        }

        /// <summary>
        /// 删除其他东西
        /// </summary>
        public void DeleteItems()
        {
            for (int i = 0; i < clubBlackList.Count; ++i)
            {
                GameObject.Destroy(clubBlackList[i].itemNode);
            }

            clubBlackList.Clear();
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="menberId"></param>
        public void UpScore(int menberId)
        {
            for (int i = 0; i < clubBlackList.Count; ++i)
            {
                if (clubBlackList[i].bindMenber.menberId == menberId)
                {
                    clubBlackList[i].Show(clubBlackList[i].bindMenber);
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
            for (int i = 0; i < clubBlackList.Count; ++i)
            {
                if (clubBlackList[i].bindMenber.menberId == menberId)
                {
                    GameObject.Destroy(clubBlackList[i].itemNode);
                    clubBlackList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}


