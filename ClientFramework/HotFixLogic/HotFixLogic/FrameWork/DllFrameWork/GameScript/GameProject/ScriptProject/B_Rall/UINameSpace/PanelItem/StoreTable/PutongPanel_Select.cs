using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Rall
{
    /// <summary>
    /// 普通商城面板
    /// </summary>
    public class PutongPanel_Select : TablePanelItem
    {
        /// <summary>
        /// 面板节点
        /// </summary>
        public static PutongPanel_Select putongPanel;

        public Image spriteSelect;
        public Image spriteNoSelect;

        /// <summary>
        /// 获取面板信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();

            putongPanel = this;

            spriteSelect = GenericityTool.GetComponentByPath<Image>(tableButton.gameObject, "imgSelect");
            spriteNoSelect = GenericityTool.GetComponentByPath<Image>(tableButton.gameObject, "imgNoSelect");

            for (int i = 0; i < 2; i++)
            {
                string btnPath = "";
                string panelPath = "";
                TablePanelItem typePanel = null;
                if (i == 0)
                {
                    typePanel = new Rall.FKPanel_Select();
                    btnPath = "table_type/table_0";
                    panelPath = "panel_type/fk";
                }
                else if (i == 1)
                {
                    typePanel = new Rall.ZSPanel_Select();
                    btnPath = "table_type/table_1";
                    panelPath = "panel_type/zs";
                }

                typePanel.tag = "TypePanel";
                typePanel.index = i;
                typePanel.tableButton = GenericityTool.GetComponentByPath<Button>(tablePanel,btnPath);
                typePanel.tablePanel = GenericityTool.GetObjectByPath(tablePanel, panelPath);
                typePanel.RegistListen();
            }
            TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("TypePanel");
            firstTablePanel.SelectPanel();
        }

        /// <summary>
        /// 选中这个Item
        /// </summary>
        public override void OnSelect()
        {
            base.OnSelect();
            spriteSelect.gameObject.SetActive(true);
            spriteNoSelect.gameObject.SetActive(false);
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

        /// <summary>
        /// 外部调用接口
        /// </summary>
        /// <param name="parmaras"></param>
        public override void ExitCall(object parmaras)
        {
            base.ExitCall(parmaras);
        }
    }
}


