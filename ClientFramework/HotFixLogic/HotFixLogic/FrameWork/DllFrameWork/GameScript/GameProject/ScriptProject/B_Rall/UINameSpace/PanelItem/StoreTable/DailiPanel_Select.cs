using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Rall
{
	/// <summary>
	/// 普通商城面板
	/// </summary>
	public class EntryCode_Select
	{
		public static bool isEntry = false;
		/// <summary>
		/// 面板节点
		/// </summary>
		public static GameObject panelNode;

		public static InputField input_entry;

		/// <summary>
		/// 获取面板信息
		/// </summary>
		public static void GetUI(GameObject node)
		{
			panelNode = node;
			input_entry = GenericityTool.GetComponentByPath<InputField>(panelNode, "input_entry");
		}

		/// <summary>
		/// 设置显示界面
		/// </summary>
		/// <param name="active"></param>
		public static void SetActive(bool active)
		{
			if (panelNode != null)
			{
				panelNode.SetActive(active);
			}
		}

		/// <summary>
		/// 设置进入游戏
		/// </summary>
		public static void SetEntry()
		{
			EntryCode_Select.isEntry = true;
			SetActive(false);
		}
	}


	public class DaiLiPanel_FkItem
	{
		/// <summary>
		/// 节点
		/// </summary>
		public GameObject itemNode;
		/// <summary>
		/// 购买按钮
		/// </summary>
		public Button btn_buy;
		/// <summary>
		/// 购买说明
		/// </summary>
		public Text txt_desc;
		/// <summary>
		/// 购买价格
		/// </summary>
		public Text txt_buy;
		/// <summary>
		/// 绑定的物品数据
		/// </summary>
		public GoodsThingData bindThingData;

		public void GetUI(GameObject node)
		{
			itemNode = node;
			btn_buy = itemNode.GetComponent<Button>();
			txt_desc = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_desc");
			txt_buy = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_buy");

			btn_buy.onClick.AddListener(OnClickBuy);
		}

		/// <summary>
		/// 显示信息
		/// </summary>
		/// <param name="bindData"></param>
		public void SetShow(GoodsThingData bindData)
		{
			bindThingData = bindData;

			txt_desc.text = bindThingData.count + "张";
			txt_buy.text = bindThingData.agentPrice + "元";
		}

		/// <summary>
		/// 购买
		/// </summary>
		public void OnClickBuy()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			//UINameSpace.UITipMessage.PlayMessage("购买尚未开启!");
			Rall.UIStorePay.bindThingData = bindThingData;
			Rall.UIStorePay.SetActive(true);
		}

		/// <summary>
		/// 显示或者隐藏
		/// </summary>
		/// <param name="active"></param>
		public void SetActive(bool active)
		{
			itemNode.SetActive(true);
		}

		/// <summary>
		/// 设置父节点
		/// </summary>
		/// <param name="parent"></param>
		public void SetParent(GameObject parent)
		{
			itemNode.transform.SetParent(parent.transform);
		}

		/// <summary>
		/// 设置局部信息
		/// </summary>
		public void SetLocal()
		{
			itemNode.transform.localPosition = Vector3.zero;
			itemNode.transform.localScale = Vector3.one;
		}

	}

	/// <summary>
	/// 代理商城面板
	/// </summary>
	public class DailiPanel_Select : TablePanelItem
    {
        /// <summary>
        /// 面板节点
        /// </summary>
        public static DailiPanel_Select Instance;
		public Image spriteSelect;
		public Image spriteNoSelect;
		/// <summary>
		/// 资源节点
		/// </summary>
		public GameObject itemSource;
		/// <summary>
		/// Item列表
		/// </summary>
		public List<DaiLiPanel_FkItem> fKItemList = new List<DaiLiPanel_FkItem>();

		/// <summary>
		/// 获取面板信息
		/// </summary>
		public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
			Instance = this;

			spriteSelect = GenericityTool.GetComponentByPath<Image>(tableButton.gameObject, "imgSelect");
			spriteNoSelect = GenericityTool.GetComponentByPath<Image>(tableButton.gameObject, "imgNoSelect");

			itemSource = GenericityTool.GetObjectByPath(tablePanel, "panel_daili/node/itemSource");
			itemSource.SetActive(false);

			List<GoodsThingData> goodsThing = new List<GoodsThingData>(Rall.LogicDataSpace.configGoodsData.dataList.Values);

			for (int i = 0; i < goodsThing.Count; ++i)
			{
				if (goodsThing[i].goodsType == 1)
				{
					GameObject item = GameObject.Instantiate(itemSource);
					DaiLiPanel_FkItem fkItem = new DaiLiPanel_FkItem();
					fkItem.GetUI(item);
					fkItem.SetShow(goodsThing[i]);
					fkItem.SetParent(itemSource.transform.parent.gameObject);
					fkItem.SetLocal();
					fkItem.SetActive(true);
					fKItemList.Add(fkItem);
				}
			}

			EntryCode_Select.GetUI(GenericityTool.GetObjectByPath(tablePanel, "entryCode"));
			EntryCode_Select.SetActive(false);
		}

        /// <summary>
        /// 选中这个Item
        /// </summary>
        public override void OnSelect()
        {
            base.OnSelect();
			spriteSelect.gameObject.SetActive(true);
			spriteNoSelect.gameObject.SetActive(false);

			EntryCode_Select.SetActive(true);
		}

        public override void OnUnSelect()
        {
            base.OnUnSelect();
            if (!IsGet) { return; }

			if (spriteSelect == null)
			{
				spriteSelect = GenericityTool.GetComponentByPath<Image>(tableButton.gameObject, "imgSelect");
				spriteNoSelect = GenericityTool.GetComponentByPath<Image>(tableButton.gameObject, "imgNoSelect");
			}
			spriteSelect.gameObject.SetActive(false);
			spriteNoSelect.gameObject.SetActive(true);
		}
    }
}


