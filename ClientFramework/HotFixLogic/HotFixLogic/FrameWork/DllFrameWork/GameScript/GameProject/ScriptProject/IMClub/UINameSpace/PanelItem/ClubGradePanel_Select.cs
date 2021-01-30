using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
	public class ClubGradeItem
	{
		/// <summary>
		/// 节点
		/// </summary>
		public GameObject itemNode;
		/// <summary>
		/// 进入
		/// </summary>
		public Button btn_entry;
		/// <summary>
		/// 游戏名
		/// </summary>
		public Text txt_gameName;
		/// <summary>
		/// 房间ID
		/// </summary>
		public Text txt_roomId;
		/// <summary>
		/// 玩家数量
		/// </summary>
		public Text txt_playerCount;
		/// <summary>
		/// 时间
		/// </summary>
		public Text txt_time;
		/// <summary>
		/// 玩家
		/// </summary>
		public Text txt_players;

		public RectTransform rectTransform;
		/// <summary>
		/// 绑定的战绩信息
		/// </summary>
		public P_ClubGradeInfo clubGradeInfo;

		public void GetUI(GameObject node)
		{
			itemNode = node;
			rectTransform = itemNode.GetComponent<RectTransform>();
			txt_gameName = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_gameName");
			txt_roomId = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_roomId");
			txt_playerCount = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_playerCount");
			txt_time = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_time");
			txt_players = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_players");

			btn_entry = GenericityTool.GetComponentByPath<Button>(itemNode, "btn_entry");
			btn_entry.onClick.AddListener(OnClickOpenInfo);
		}

		public void SetParent(GameObject parent)
		{
			itemNode.transform.SetParent(parent.transform);
            itemNode.transform.localPosition = Vector3.zero;
            itemNode.transform.localScale = Vector3.one;
		}

		public void SetActive(bool active)
		{
			itemNode.SetActive(active);
		}

		public void Destory()
		{
			GameObject.Destroy(itemNode);
		}

		public void SetToLast()
		{
			rectTransform.SetAsLastSibling();
		}

		public void ShowGrade(P_ClubGradeInfo info)
		{
			clubGradeInfo = info;
			GameEntryItem entryItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntryWithGameType(info.clubGrade.gameType);
            if (entryItem == null)
            {
                UINameSpace.UITipMessage.PlayMessage("系统错误!");
                return;
            }

			txt_gameName.text = entryItem.gameName;
			txt_roomId.text = clubGradeInfo.clubGrade.roomId.ToString();
			if (clubGradeInfo.clubGrade.clubChangeItem == null)
			{
				txt_playerCount.text = "0";
			}
			else
			{
				txt_playerCount.text = clubGradeInfo.clubGrade.clubChangeItem.Count.ToString();
			}

			txt_time.text = new DateTime(clubGradeInfo.time).ToString("MM-dd HH:mm:ss");
			
			string playerStr = "成员";
			for (int i = 0; i < clubGradeInfo.clubGrade.clubChangeItem.Count; ++i)
			{
				Server.P_ClubScoreBackItem menberGrade = clubGradeInfo.clubGrade.clubChangeItem[i];

				P_Menber menber = GoableClubDataInfo.GetMenberFromGroup(menberGrade.menberId, clubGradeInfo.clubGrade.clubId);
				if (menber != null)
				{
					playerStr += " " + menber.menberName;
				}
				else
				{
					playerStr += " 未知";
				}
			}

			txt_players.text = playerStr;
		}

		private void OnClickOpenInfo()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			//UINameSpace.UITipMessage.PlayMessage("暂未开启!");
			GameEntryItem entryItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntryWithGameType(clubGradeInfo.clubGrade.gameType);
            Debug.Log(clubGradeInfo.clubGrade.gameType);
			entryItem.OpenToldResultUI(clubGradeInfo.clubGrade.toldGameEndData.ToArray());
		}
	}

    /// <summary>
    /// 亲友圈房间列表
    /// </summary>
    public class ClubGradePanel_Select : TablePanelItem
    {
        /// <summary>
        /// 亲友圈聊天面板
        /// </summary>
        public static ClubGradePanel_Select clubGradePanel;

		public List<ClubGradeItem> clubGradeItemList = new List<ClubGradeItem>();

		public GameObject itemSource;

		public Button btn_next;

		public Button btn_up;

        /// <summary>
        /// 获取面包信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();

            clubGradePanel = this;

			itemSource = GenericityTool.GetObjectByPath(tablePanel, "greadListPanel/Viewport/Content/itemNode");
			itemSource.SetActive(false);


			btn_next = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_next");
			btn_up = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_up");

			btn_next.onClick.AddListener(OnClickNext);
			btn_up.onClick.AddListener(OnClickUp);
		}

		private void OnClickUp()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			if (ClubItem.clubItemState.bindGwInfo.page == 0)
			{
				UINameSpace.UITipMessage.PlayMessage("已经到最前页!");
				return;
			}

			GetPage((byte)(ClubItem.clubItemState.bindGwInfo.page - 1));
		}

		private void OnClickNext()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			GetPage((byte)(ClubItem.clubItemState.bindGwInfo.page + 1));
		}

		private void GetPage(byte page)
		{
			UINameSpace.UIWaitting.AddShowWaitting("SC_GetClubGrade_MsgType");
			MessageSend.GetClubGrade(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, page);
		}

		/// <summary>
		/// 选中了这个Item
		/// </summary>
		public override void OnSelect()
        {
            base.OnSelect();

			if (ClubItem.clubItemState.bindGwInfo.clubGradeList == null)
			{
				//获取第一页
				GetPage(0);
			}
			else
			{
				ShowItems();
			}
        }

		public void ShowItems()
		{
			DeleteItems();

			ClubItem.clubItemState.bindGwInfo.clubGradeList.Sort((left, right) => 
			{
				if (left.time < right.time)
				{
					return 1;
				}
				else if (left.time > right.time)
				{
					return -1;
				}
				else
				{
					return 0;
				}
			});
			for (int i = 0; i < ClubItem.clubItemState.bindGwInfo.clubGradeList.Count; ++i)
			{
				P_ClubGradeInfo clubGradeInfo = ClubItem.clubItemState.bindGwInfo.clubGradeList[i];
				GameObject item = GameObject.Instantiate(itemSource);
				item.name = "item_" + i;

				ClubGradeItem clubGradeItem = new ClubGradeItem();
				clubGradeItemList.Add(clubGradeItem);
				clubGradeItem.GetUI(item);
				clubGradeItem.SetParent(itemSource.transform.parent.gameObject);
				clubGradeItem.SetToLast();
				clubGradeItem.SetActive(true);
				clubGradeItem.ShowGrade(ClubItem.clubItemState.bindGwInfo.clubGradeList[i]);
			}
		}

		public void DeleteItems()
		{
			for (int i = 0; i < clubGradeItemList.Count; ++i)
			{
				clubGradeItemList[i].Destory();
			}

			clubGradeItemList.Clear();
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
        /// 重置显示
        /// </summary>
        public void ResetShow()
        {

        }
    }
}


