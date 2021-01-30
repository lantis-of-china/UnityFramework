using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// TablePanel
/// </summary>
public class TablePanelItem
{
    public static List<TablePanelItem> panelList = new List<TablePanelItem>();
    /// <summary>
    /// 添加一个面板
    /// </summary>
    /// <param name="panelItem"></param>
    public static void AddItem(TablePanelItem panelItem)
    {
        panelList.Add(panelItem);
    }

    /// <summary>
    /// 选中面板
    /// </summary>
    /// <param name="panelItem"></param>
    public static void SelectItem(TablePanelItem panelItem)
    {
        for (int loop = 0; loop < panelList.Count; ++loop)
        {
            TablePanelItem tpi = panelList[loop];
            if (tpi != panelItem && tpi.tag == panelItem.tag)
            {
                tpi.SetSelectState(false);
            }
        }
    }


    /// <summary>
    /// 获取指定标志第一个面板
    /// </summary>
    /// <param name="tag"></param>
    public static TablePanelItem GetFirstTablePanelWithTag(string tag)
    {
        for (int loop = 0; loop < panelList.Count; ++loop)
        {
            TablePanelItem tpi = panelList[loop];
            if (tpi.tag == tag)
            {
                return tpi;
            }
        }

        return null;
    }

	/// <summary>
	/// 获取指定标志第一个面板
	/// </summary>
	/// <param name="tag"></param>
	public static List<TablePanelItem> GetFirstTablePanelsWithTag(string tag)
	{
		List<TablePanelItem> panels = new List<TablePanelItem>();

		for (int loop = 0; loop < panelList.Count; ++loop)
		{
		    TablePanelItem tpi = panelList[loop];
			if (tpi.tag == tag)
			{
				panels.Add(tpi);
			}
		}

		return panels;
	}

	/// <summary>
	/// 获取指定标志的选中面板
	/// </summary>
	/// <param name="tag"></param>
	/// <returns></returns>
	public static TablePanelItem GetSelectTablePanelWithTag(string tag)
    {
        for (int loop = 0; loop < panelList.Count; ++loop)
        {
            TablePanelItem tpi = panelList[loop];
            if (tpi.tag == tag && tpi.isSelect)
            {
                return tpi;
            }
        }
        return null;
    }

    public static void CloseTableWithTag(string tag)
    {
        for (int loop = panelList.Count - 1; loop >= 0; --loop)
        {
            TablePanelItem tpi = panelList[loop];
            if (tpi.tag == tag)
            {
                panelList.RemoveAt(loop);
            }
        }
    }
    /// <summary>
    /// 连接对象
    /// </summary>
    public object linkObj;
    /// <summary>
    /// 是否选中
    /// </summary>
    public bool isSelect;
    /// <summary>
    /// panel的分类
    /// </summary>
    public string tag;
    /// <summary>
    /// 面板的索引
    /// </summary>
    public int index;
    /// <summary>
    /// panel的Table
    /// </summary>
    public Button tableButton;
    /// <summary>
    /// 面板子=
    /// </summary>
    public GameObject tablePanel;
    /// <summary>
    /// 是否获取过组件
    /// </summary>
    private bool isGet;
    /// <summary>
    /// 是否获取过组件
    /// </summary>
    public bool IsGet { get { return isGet; } }

    /// <summary>
    /// 注册按钮
    /// </summary>
    public void RegistListen()
    {
        tableButton.onClick.AddListener(delegate 
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SelectPanel();
		});
        TablePanelItem.AddItem(this);
    }

    /// <summary>
    /// 选中面板
    /// </summary>
    public void SelectPanel()
    {
		if (!isGet)
        {
			isGet = true;
            OnGetPanelInfo();
        }

        SetSelectState(true);
    }

    /// <summary>
    /// 设置选中
    /// </summary>
    /// <param name="_isSelect"></param>
    public void SetSelectState(bool _isSelect)
    {
        isSelect = _isSelect;
        tablePanel.gameObject.SetActive(isSelect);

        if(isSelect)
        {
            TablePanelItem.SelectItem(this);
            OnSelect();

            tableButton.interactable = false;
        }
        else
        {
            OnUnSelect();

            tableButton.interactable = true;
        }
    }

    /// <summary>
    /// 面板是否开启
    /// </summary>
    /// <returns></returns>
    public bool IsActive()
    {
        return tablePanel.activeSelf;
    }


    /// <summary>
    /// 获取面板信息
    /// </summary>
    public virtual void OnGetPanelInfo()
    {

    }

    /// <summary>
    /// 选中了
    /// </summary>
    public virtual void OnSelect()
    {
        isSelect = true;
    }

    /// <summary>
    /// 未被选中
    /// </summary>
    public virtual void OnUnSelect()
    {
        isSelect = false;
    }

    /// <summary>
    /// 外部调用
    /// </summary>
    public virtual void ExitCall(object parmaras)
    {
        
    }
}