using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 创建扯旋面板
/// </summary>
public class CheXuanSet_Select : TablePanelItem
{
    public Image spriteSelect;
    public Image spriteNoSelect;
    public Image spriteSelect_Zs;
    public Image spriteNoSelect_Zs;

	public Image spriteSelect_sport;
	public Image spriteNoSelect_sport;
	/// <summary>
	/// 局数
	/// </summary>
	private Text text_times_4;
    private Text text_times_8;
    private Button btn_times_4;
    private Button btn_times_8;
    public int times = 4;

    /// <summary>
    /// 特牌
    /// </summary>
    private Text text_hasTePai;
    private Text text_notTePai;
    private Button btn_hasTePai;
    private Button btn_notTePai;
    public int isTePi = 0;

    /// <summary>
    /// 地九王大
    /// </summary>
    private Text text_DijiuDa;
    private Text text_DijiuBuDa;
    private Button btn_DijiuDa;
    private Button btn_DijiuBuDa;
    public int diJiuDa = 0;

    /// <summary>
    /// 庄家先手
    /// </summary>
    private Text text_zhuangXian;
    private Text text_zhuangHou;
    private Button btn_zhuangXian;
    private Button btn_zhuangHou;
    public int zhuangXian = 0;

	/// <summary>
	/// 起投
	/// </summary>
	private Text text_qiTou_1;
	private Text text_qiTou_2;
	private Text text_qiTou_3;
	private Button btn_qiTou_1;
	private Button btn_qiTou_2;
	private Button btn_qiTou_3;
	public int qiTouCount = 50;

	/// <summary>
	/// 丢皮
	/// </summary>
	private Text text_pi_1;
	private Text text_pi_2;
	private Text text_pi_3;
	private Button btn_pi_1;
	private Button btn_pi_2;
	private Button btn_pi_3;
	public int piCount = 1;



	/// <summary>
	/// 获取面包信息
	/// </summary>
	public override void OnGetPanelInfo()
    {
        base.OnGetPanelInfo();

        spriteSelect = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgSourceSelect");
        spriteNoSelect = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgSourceNoSelect");
        spriteSelect_Zs = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgSourceSelect_Zs");
        spriteNoSelect_Zs = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgSourceNoSelect_Zs");
        spriteSelect_sport = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgSourceSelect_Sport");
        spriteNoSelect_sport = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgSourceNoSelect_Sport");

        text_times_4 = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_times_4/text_times_4");
        text_times_8 = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_times_8/text_times_8");
        btn_times_4 = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_times_4");
        btn_times_8 = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_times_8");
        btn_times_4.onClick.AddListener(Times_4);
        btn_times_8.onClick.AddListener(Times_8);

        text_hasTePai = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_tePai/text_tePai");
        text_notTePai = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_notTePai/text_notTePai");
        btn_hasTePai = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_tePai");
        btn_notTePai = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_notTePai");
        btn_hasTePai.onClick.AddListener(IsTePai);
        btn_notTePai.onClick.AddListener(NotTePai);

        text_DijiuDa = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_diJiuDa/text_diJiuDa");
        text_DijiuBuDa = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_diJiuBuDa/text_diJiuBuDa");
        btn_DijiuDa = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_diJiuDa");
        btn_DijiuBuDa = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_diJiuBuDa");
        btn_DijiuDa.onClick.AddListener(DiJiuDa);
        btn_DijiuBuDa.onClick.AddListener(DiJiuBuDa);

        text_zhuangXian = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_zhuangXian/text_zhuangXian");
        text_zhuangHou = GenericityTool.GetComponentByPath<Text>(tablePanel, "image_zhuangHou/text_zhuangHou");
        btn_zhuangXian = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_zhuangXian");
        btn_zhuangHou = GenericityTool.GetComponentByPath<Button>(tablePanel, "image_zhuangHou");
        btn_zhuangXian.onClick.AddListener(ZhuangXian);
        btn_zhuangHou.onClick.AddListener(ZhuangHou);

        text_qiTou_1 = GenericityTool.GetComponentByPath<Text>(tablePanel, "btn_qiTou_1/txt_qiTou_1");
        text_qiTou_2 = GenericityTool.GetComponentByPath<Text>(tablePanel, "btn_qiTou_2/txt_qiTou_2");
        text_qiTou_3 = GenericityTool.GetComponentByPath<Text>(tablePanel, "btn_qiTou_3/txt_qiTou_3");
        btn_qiTou_1 = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_qiTou_1");
        btn_qiTou_2 = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_qiTou_2");
        btn_qiTou_3 = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_qiTou_3");
        btn_qiTou_1.onClick.AddListener(OnClickQiTou_1);
        btn_qiTou_2.onClick.AddListener(OnClickQiTou_2);
        btn_qiTou_3.onClick.AddListener(OnClickQiTou_3);

        text_pi_1 = GenericityTool.GetComponentByPath<Text>(tablePanel, "btn_diuPi_1/txt_diuPi_1");
        text_pi_2 = GenericityTool.GetComponentByPath<Text>(tablePanel, "btn_diuPi_2/txt_diuPi_2");
        text_pi_3 = GenericityTool.GetComponentByPath<Text>(tablePanel, "btn_diuPi_3/txt_diuPi_3");
        btn_pi_1 = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_diuPi_1");
        btn_pi_2 = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_diuPi_2");
        btn_pi_3 = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_diuPi_3");
        btn_pi_1.onClick.AddListener(OnClickPi_1);
        btn_pi_2.onClick.AddListener(OnClickPi_2);
        btn_pi_3.onClick.AddListener(OnClickPi_3);
	}

	public void SetParamars(int roomTimes,List<int> paramars)
	{
		times = roomTimes;
		for (int l = 0; l < paramars.Count; ++l)
		{
			int setValue = paramars[l];
			if (l == 0)
			{
				//特牌
				isTePi = setValue;
			}
			else if (l == 1)
			{
				//地九王
				diJiuDa = setValue;
			}
			else if (l == 2)
			{
				//先手
				zhuangXian = setValue;
			}
			else if (l == 3)
			{
				//起投簸簸数量
				qiTouCount = setValue;
			}
			else if (l == 4)
			{
				//底皮数量
				piCount = setValue;
			}
			else if (l == 5)
			{
				//圈芒
				//quanMang = setValue;
			}
			else if (l == 6)
			{
				//休芒
				//xiuManag = setValue;
			}
		}

		//UpShow();
	}

    public Color GetSelectColor()
    {
        return new Color(125.0f/256.0f,209.0f/256.0f,208.0f/256.0f);
    }

    public Color GetNoSelectColor()
    {
        return new Color(125.0f / 256.0f, 209.0f / 256.0f, 208.0f / 256.0f);
    }

	private void OnClickQiTou_1()
	{
		qiTouCount = 50;
		UpShow();
	}

	private void OnClickQiTou_2()
	{
		qiTouCount = 150;
		UpShow();
	}

	private void OnClickQiTou_3()
	{
		qiTouCount = 300;
		UpShow();
	}

	private void OnClickPi_1()
	{
		if (piCount == 2)
		{
			piCount = 1;
		}
		else
		{
			piCount = 2;
		}
		UpShow();
	}

	private void OnClickPi_2()
	{
		if (piCount == 3)
		{
			piCount = 1;
		}
		else
		{
			piCount = 3;
		}
		UpShow();
	}

	private void OnClickPi_3()
	{
		if (piCount == 4)
		{
			piCount = 1;
		}
		else
		{
			piCount = 4;
		}
		UpShow();
	}

	/// <summary>
	/// 刷新显示
	/// </summary>
	public void UpShow()
    {
        ///局数
        if(times == 4)
        {
            text_times_4.color = GetSelectColor();
            text_times_8.color = GetNoSelectColor();

            (btn_times_4.targetGraphic as Image).overrideSprite = spriteSelect_Zs.overrideSprite;
            (btn_times_8.targetGraphic as Image).overrideSprite = spriteNoSelect_Zs.overrideSprite;
        }
        else if(times == 8)
        {
            text_times_4.color = GetNoSelectColor();
            text_times_8.color = GetSelectColor();

            (btn_times_4.targetGraphic as Image).overrideSprite = spriteNoSelect_Zs.overrideSprite;
            (btn_times_8.targetGraphic as Image).overrideSprite = spriteSelect_Zs.overrideSprite;
        }

        //特牌
        if (isTePi == 1)
        {
            text_hasTePai.color = GetSelectColor();
            text_notTePai.color = GetNoSelectColor();

            (btn_hasTePai.targetGraphic as Image).overrideSprite = spriteSelect.overrideSprite;
            (btn_notTePai.targetGraphic as Image).overrideSprite = spriteNoSelect.overrideSprite;
        }
        else if (isTePi == 0)
        {
            text_hasTePai.color = GetNoSelectColor();
            text_notTePai.color = GetSelectColor();

            (btn_hasTePai.targetGraphic as Image).overrideSprite = spriteNoSelect.overrideSprite;
            (btn_notTePai.targetGraphic as Image).overrideSprite = spriteSelect.overrideSprite;
        }

        //地九王
        if(diJiuDa == 1)
        {
            text_DijiuDa.color = GetSelectColor();
            text_DijiuBuDa.color = GetNoSelectColor();

            (btn_DijiuDa.targetGraphic as Image).overrideSprite = spriteSelect.overrideSprite;
            (btn_DijiuBuDa.targetGraphic as Image).overrideSprite = spriteNoSelect.overrideSprite;
        }
        else
        {
            text_DijiuDa.color = GetNoSelectColor();
            text_DijiuBuDa.color = GetSelectColor();

            (btn_DijiuDa.targetGraphic as Image).overrideSprite = spriteNoSelect.overrideSprite;
            (btn_DijiuBuDa.targetGraphic as Image).overrideSprite = spriteSelect.overrideSprite;
        }

        //庄家先
        if (zhuangXian == 1)
        {
            text_zhuangXian.color = GetSelectColor();
            text_zhuangHou.color = GetNoSelectColor();

            (btn_zhuangXian.targetGraphic as Image).overrideSprite = spriteSelect.overrideSprite;
            (btn_zhuangHou.targetGraphic as Image).overrideSprite = spriteNoSelect.overrideSprite;
        }
        else
        {
            text_zhuangXian.color = GetNoSelectColor();
            text_zhuangHou.color = GetSelectColor();

            (btn_zhuangXian.targetGraphic as Image).overrideSprite = spriteNoSelect.overrideSprite;
            (btn_zhuangHou.targetGraphic as Image).overrideSprite = spriteSelect.overrideSprite;
        }

		if (qiTouCount == 50)
		{
			text_qiTou_1.color = GetSelectColor();
			text_qiTou_2.color = GetNoSelectColor();
			text_qiTou_3.color = GetNoSelectColor();

			(btn_qiTou_1.targetGraphic as Image).overrideSprite = spriteSelect_sport.overrideSprite;
			(btn_qiTou_2.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_qiTou_3.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
		}
		else if (qiTouCount == 150)
		{
			text_qiTou_1.color = GetNoSelectColor();
			text_qiTou_2.color = GetSelectColor();
			text_qiTou_3.color = GetNoSelectColor();

			(btn_qiTou_1.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_qiTou_2.targetGraphic as Image).overrideSprite = spriteSelect_sport.overrideSprite;
			(btn_qiTou_3.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
		}
		else if (qiTouCount == 300)
		{
			text_qiTou_1.color = GetNoSelectColor();
			text_qiTou_2.color = GetNoSelectColor();
			text_qiTou_3.color = GetSelectColor();

			(btn_qiTou_1.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_qiTou_2.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_qiTou_3.targetGraphic as Image).overrideSprite = spriteSelect_sport.overrideSprite;
		}

		//丢皮
		if (piCount == 2)
		{
			text_pi_1.color = GetSelectColor();
			text_pi_2.color = GetNoSelectColor();
			text_pi_3.color = GetNoSelectColor();

			(btn_pi_1.targetGraphic as Image).overrideSprite = spriteSelect_sport.overrideSprite;
			(btn_pi_2.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_pi_3.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
		}
		else if (piCount == 3)
		{
			text_pi_1.color = GetNoSelectColor();
			text_pi_2.color = GetSelectColor();
			text_pi_3.color = GetNoSelectColor();

			(btn_pi_1.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_pi_2.targetGraphic as Image).overrideSprite = spriteSelect_sport.overrideSprite;
			(btn_pi_3.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
		}
		else if (piCount == 4)
		{
			text_pi_1.color = GetNoSelectColor();
			text_pi_2.color = GetNoSelectColor();
			text_pi_3.color = GetSelectColor();

			(btn_pi_1.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_pi_2.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_pi_3.targetGraphic as Image).overrideSprite = spriteSelect_sport.overrideSprite;
		}
		else
		{
			text_pi_1.color = GetNoSelectColor();
			text_pi_2.color = GetNoSelectColor();
			text_pi_3.color = GetNoSelectColor();

			(btn_pi_1.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_pi_2.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
			(btn_pi_3.targetGraphic as Image).overrideSprite = spriteNoSelect_sport.overrideSprite;
		}
	}

    /// <summary>
    /// 4局
    /// </summary>
    public void Times_4()
    {
        times = 4;
        UpShow();
    }

    /// <summary>
    /// 8局
    /// </summary>
    public void Times_8()
    {
        times = 8;
        UpShow();
    }

    /// <summary>
    /// 选择特牌
    /// </summary>
    public void IsTePai()
    {
        isTePi = 1;
        UpShow();
    }

    /// <summary>
    /// 没有特牌
    /// </summary>
    public void NotTePai()
    {
        isTePi = 0;
        UpShow();
    }


    /// <summary>
    /// 选择地九王大
    /// </summary>
    public void DiJiuDa()
    {
        diJiuDa = 1;
        UpShow();
    }

    /// <summary>
    /// 地九王不大
    /// </summary>
    public void DiJiuBuDa()
    {
        diJiuDa = 0;
        UpShow();
    }

    /// <summary>
    /// 庄家先
    /// </summary>
    public void ZhuangXian()
    {
        zhuangXian = 1;
        UpShow();
    }

    /// <summary>
    /// 庄家后
    /// </summary>
    public void ZhuangHou()
    {
        zhuangXian = 0;
        UpShow();
    }





    /// <summary>
    /// 选中了这个Item
    /// </summary>
    public override void OnSelect()
    {
        base.OnSelect();

        UpShow();
    }

    /// <summary>
    /// 外部调用接口
    /// </summary>
    /// <param name="parmaras"></param>
    public override void ExitCall(object parmaras)
    {
        base.ExitCall(parmaras);
		UINameSpace.UIWaitting.AddShowWaitting("IMClub.NetMessageType.CS_SetGameSetting_MsgType");

        List<int> paramarsList = new List<int>();
        //特牌
        paramarsList.Add(isTePi);
        //地九王
        paramarsList.Add(diJiuDa);
        //庄家先
        paramarsList.Add(zhuangXian);
		//起投
		paramarsList.Add(qiTouCount);
		//底皮
		paramarsList.Add(piCount);
		//圈芒类型
		paramarsList.Add(0);
		//休芒
		paramarsList.Add(0);

		IMClub.MessageSend.PlaySetting(IMClub.ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, (byte)UINameSpace.UISetRoomCheXuan_IMClub.bindGameSetting.gameType, (byte)times, paramarsList);
	}
}


