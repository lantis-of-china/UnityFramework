using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈最高设置
    /// </summary>
    public class ClubBestSettingPanel_Select : TablePanelItem
    {
        /// <summary>
        /// 分数限制
        /// </summary>
        public InputField input_scoreLimit;
        /// <summary>
        /// 保存按钮
        /// </summary>
        public Button btn_save;

        public Image selectType;
        public Image noselectType;

        public Image toggleSelectType;
        public Image toggleNoSelectType;

        /// <summary>
        /// 选择类型
        /// </summary>
        public P_ClubSetting clubSetting;

        #region 最大赢家组件
        /// <summary>
        /// 锁
        /// </summary>
        public GameObject lockObj_bestWiner;
        /// <summary>
        /// 大赢家选择按钮
        /// </summary>
        public Button btn_select_bestWiner;
        /// <summary>
        /// 大赢家选择
        /// </summary>
        public Text txt_select_bestWiner;
        /// <summary>
        /// 开始扣钱的门槛设置
        /// </summary>
        public InputField input_collectStart_bestWiner;
        /// <summary>
        /// 收取比例
        /// </summary>
        public InputField input_collectScale_bestWiner;
        /// <summary>
        /// 收取分数
        /// </summary>
        public InputField input_collectScore_bestWiner;
        /// <summary>
        /// 分数选择
        /// </summary>
        public Button btn_score_bestWiner;
        /// <summary>
        /// 比例选择
        /// </summary>
        public Button btn_scale_bestWiner;
        #endregion 最大赢家组件
        
        #region 按人数收
        /// <summary>
        /// 锁
        /// </summary>
        public GameObject lockObj_menberCount;
        /// <summary>
        /// 选择按钮
        /// </summary>
        public Button btn_select_menberCount;
        /// <summary>
        /// 选择的文本
        /// </summary>
        public Text txt_select_menberCount;
        /// <summary>
        /// 收集分数
        /// </summary>
        public InputField input_collectScore_menberCount;
        #endregion 按人数收

        #region 每个人收取
        /// <summary>
        /// 锁
        /// </summary>
        public GameObject lockObj_erverMenbers;
        /// <summary>
        /// 选择按钮
        /// </summary>
        public Button btn_select_erverMenbers;
        /// <summary>
        /// 成员队列
        /// </summary>
        public Text txt_select_erverMenbers;
        /// <summary>
        /// 开始扣钱的门槛设置
        /// </summary>
        public InputField input_collectStart_erverMenbers;
        /// <summary>
        /// 收取比例
        /// </summary>
        public InputField input_collectScale_erverMenbers;
        /// <summary>
        /// 收取分数
        /// </summary>
        public InputField input_collectScore_erverMenbers;
        /// <summary>
        /// 分数选择
        /// </summary>
        public Button btn_score_erverMenbers;
        /// <summary>
        /// 比例选择
        /// </summary>
        public Button btn_scale_erverMenbers;
        #endregion 每个人收取

        public void ResetShow()
        {
            clubSetting = ClubItem.clubItemState.bindGwInfo.groupInfo.clubSetting;

            input_scoreLimit.text = clubSetting.scoreLimit.ToString();
            SetTaxesType(clubSetting.collectTaxesType);
            SetCollectMode(clubSetting.collectMode);

            input_collectStart_bestWiner.text = clubSetting.collectStart.ToString();
            input_collectScale_bestWiner.text = clubSetting.collectScale.ToString();
            input_collectScore_bestWiner.text = clubSetting.collectScore.ToString();

            input_collectScore_menberCount.text = clubSetting.collectScore.ToString();

            input_collectStart_erverMenbers.text = clubSetting.collectStart.ToString();
            input_collectScale_erverMenbers.text = clubSetting.collectScale.ToString();
            input_collectScore_erverMenbers.text = clubSetting.collectScore.ToString();
        }

        /// <summary>
        /// 获取面包信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
            input_scoreLimit = GenericityTool.GetComponentByPath<InputField>(tablePanel, "input_scoreLimit");
            btn_save = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_save");
            btn_save.onClick.AddListener(OnClickSave);

            selectType = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgsource_modeSelect");
            noselectType = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgsource_modeNoSelect");

            toggleSelectType = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgsource_toggleSelect");
            toggleNoSelectType = GenericityTool.GetComponentByPath<Image>(tablePanel, "imgsource_toggleNoSelect");

            #region 最大赢家组件
            lockObj_bestWiner = GenericityTool.GetObjectByPath(tablePanel, "bestWiner/lockObj");
            btn_select_bestWiner = GenericityTool.GetComponentByPath<Button>(tablePanel, "bestWiner/btn_select");
            txt_select_bestWiner = GenericityTool.GetComponentByPath<Text>(tablePanel, "bestWiner/txt_select");     
            input_collectStart_bestWiner = GenericityTool.GetComponentByPath<InputField>(tablePanel, "bestWiner/input_collectStart");
            input_collectScale_bestWiner = GenericityTool.GetComponentByPath<InputField>(tablePanel, "bestWiner/input_collectScale");
            input_collectScore_bestWiner = GenericityTool.GetComponentByPath<InputField>(tablePanel, "bestWiner/input_collectScore");
            btn_score_bestWiner = GenericityTool.GetComponentByPath<Button>(tablePanel, "bestWiner/btn_score");
            btn_scale_bestWiner = GenericityTool.GetComponentByPath<Button>(tablePanel, "bestWiner/btn_scale");
            #endregion 最大赢家组件

            #region 按人数收取
            lockObj_menberCount = GenericityTool.GetObjectByPath(tablePanel, "menberCount/lockObj");
            btn_select_menberCount = GenericityTool.GetComponentByPath<Button>(tablePanel, "menberCount/btn_select");
            txt_select_menberCount = GenericityTool.GetComponentByPath<Text>(tablePanel, "menberCount/btn_select");
            input_collectScore_menberCount = GenericityTool.GetComponentByPath<InputField>(tablePanel, "menberCount/input_collectScore");
            #endregion 按人数收取

            #region 每个人收取
            lockObj_erverMenbers = GenericityTool.GetObjectByPath(tablePanel, "erverMenbers/lockObj");
            btn_select_erverMenbers = GenericityTool.GetComponentByPath<Button>(tablePanel, "erverMenbers/btn_select");
            txt_select_erverMenbers = GenericityTool.GetComponentByPath<Text>(tablePanel, "erverMenbers/txt_select");
            input_collectStart_erverMenbers = GenericityTool.GetComponentByPath<InputField>(tablePanel, "erverMenbers/input_collectStart");
            input_collectScale_erverMenbers = GenericityTool.GetComponentByPath<InputField>(tablePanel, "erverMenbers/input_collectScale");
            input_collectScore_erverMenbers = GenericityTool.GetComponentByPath<InputField>(tablePanel, "erverMenbers/input_collectScore");
            btn_score_erverMenbers = GenericityTool.GetComponentByPath<Button>(tablePanel, "erverMenbers/btn_score");
            btn_scale_erverMenbers = GenericityTool.GetComponentByPath<Button>(tablePanel, "erverMenbers/btn_scale");
            #endregion 每个人收取
            
            btn_scale_bestWiner.onClick.AddListener(OnClickScaleMode);
            btn_score_bestWiner.onClick.AddListener(OnClickScoreMode);

            btn_scale_erverMenbers.onClick.AddListener(OnClickScaleMode);
            btn_score_erverMenbers.onClick.AddListener(OnClickScoreMode);

            btn_select_bestWiner.onClick.AddListener(OnClickBigWiner);
            btn_select_menberCount.onClick.AddListener(OnClickMenberCount);
            btn_select_erverMenbers.onClick.AddListener(OnClickErverMenbers);
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
        /// 外部调用接口
        /// </summary>
        /// <param name="parmaras"></param>
        public override void ExitCall(object parmaras)
        {
            base.ExitCall(parmaras);
        }

        /// <summary>
        /// 选择大赢家
        /// </summary>
        public void OnClickBigWiner()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			SetTaxesType(0);
        }

        /// <summary>
        /// 选择按人数
        /// </summary>
        public void OnClickMenberCount()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			SetTaxesType(1);
        }

        /// <summary>
        /// 选择每人收取
        /// </summary>
        public void OnClickErverMenbers()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			SetTaxesType(2);
        }

        /// <summary>
        /// 选择比例
        /// </summary>
        public void OnClickScaleMode()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			SetCollectMode(1);
        }

        /// <summary>
        /// 选择分数
        /// </summary>
        public void OnClickScoreMode()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			SetCollectMode(0);
        }
        
        /// <summary>
        /// 选择类型
        /// </summary>
        /// <param name="collectType"></param>
        public void SetTaxesType(byte collectType)
        {
            clubSetting.collectTaxesType = collectType;

            if (clubSetting.collectTaxesType == 0)
            {
                //大赢家
                (btn_select_bestWiner.targetGraphic as Image).overrideSprite = toggleSelectType.overrideSprite;
                (btn_select_menberCount.targetGraphic as Image).overrideSprite = toggleNoSelectType.overrideSprite;
                (btn_select_erverMenbers.targetGraphic as Image).overrideSprite = toggleNoSelectType.overrideSprite;

                lockObj_bestWiner.SetActive(false);
                lockObj_menberCount.SetActive(true);
                lockObj_erverMenbers.SetActive(true);
            }
            else if (clubSetting.collectTaxesType == 1)
            {
                //人数
                (btn_select_bestWiner.targetGraphic as Image).overrideSprite = toggleNoSelectType.overrideSprite;
                (btn_select_menberCount.targetGraphic as Image).overrideSprite = toggleSelectType.overrideSprite;
                (btn_select_erverMenbers.targetGraphic as Image).overrideSprite = toggleNoSelectType.overrideSprite;

                lockObj_bestWiner.SetActive(true);
                lockObj_menberCount.SetActive(false);
                lockObj_erverMenbers.SetActive(true);
            }
            else if (clubSetting.collectTaxesType == 2)
            {
                //所有人
                (btn_select_bestWiner.targetGraphic as Image).overrideSprite = toggleNoSelectType.overrideSprite;
                (btn_select_menberCount.targetGraphic as Image).overrideSprite = toggleNoSelectType.overrideSprite;
                (btn_select_erverMenbers.targetGraphic as Image).overrideSprite = toggleSelectType.overrideSprite;

                lockObj_bestWiner.SetActive(true);
                lockObj_menberCount.SetActive(true);
                lockObj_erverMenbers.SetActive(false);
            }
        }

        /// <summary>
        /// 收分模式0分数 1缩放
        /// </summary>
        public void SetCollectMode(byte mode)
        {
            clubSetting.collectMode = mode;
            if(mode == 0)
            {
                (btn_score_bestWiner.targetGraphic as Image).overrideSprite = selectType.overrideSprite;
                (btn_score_erverMenbers.targetGraphic as Image).overrideSprite = selectType.overrideSprite;

                (btn_scale_bestWiner.targetGraphic as Image).overrideSprite = noselectType.overrideSprite;
                (btn_scale_erverMenbers.targetGraphic as Image).overrideSprite = noselectType.overrideSprite;
            }
            else
            {
                (btn_score_bestWiner.targetGraphic as Image).overrideSprite = noselectType.overrideSprite;
                (btn_score_erverMenbers.targetGraphic as Image).overrideSprite = noselectType.overrideSprite;

                (btn_scale_bestWiner.targetGraphic as Image).overrideSprite = selectType.overrideSprite;
                (btn_scale_erverMenbers.targetGraphic as Image).overrideSprite = selectType.overrideSprite;
            }
        }

        /// <summary>
        /// 提交保存给服务器
        /// </summary>
        public void OnClickSave()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			UINameSpace.UIWaitting.AddShowWaitting("SC_SetCollect_MsgType");

            if (clubSetting.collectTaxesType == 0)
            {
                if (string.IsNullOrEmpty(input_collectStart_bestWiner.text))
                {
                    input_collectStart_bestWiner.text = "1";
                }

                if (string.IsNullOrEmpty(input_collectScale_bestWiner.text))
                {
                    input_collectScale_bestWiner.text = "1";
                }

                if (string.IsNullOrEmpty(input_collectScore_bestWiner.text))
                {
                    input_collectScore_bestWiner.text = "1";
                }

                //大赢家
                clubSetting.collectStart = int.Parse(input_collectStart_bestWiner.text);
                clubSetting.collectScale = int.Parse(input_collectScale_bestWiner.text);
                clubSetting.collectScore = int.Parse(input_collectScore_bestWiner.text);
            }
            else if (clubSetting.collectTaxesType == 1)
            {
                if (string.IsNullOrEmpty(input_collectScore_menberCount.text))
                {
                    input_collectScore_menberCount.text = "1";
                }
                //按人数
                clubSetting.collectScore = int.Parse(input_collectScore_menberCount.text);
            }
            else if (clubSetting.collectTaxesType == 2)
            {
                if (string.IsNullOrEmpty(input_collectStart_erverMenbers.text))
                {
                    input_collectStart_erverMenbers.text = "1";
                }

                if (string.IsNullOrEmpty(input_collectScale_erverMenbers.text))
                {
                    input_collectScale_erverMenbers.text = "1";
                }

                if (string.IsNullOrEmpty(input_collectScore_erverMenbers.text))
                {
                    input_collectScore_erverMenbers.text = "1";
                }
                //每个人
                clubSetting.collectStart = int.Parse(input_collectStart_erverMenbers.text);
                clubSetting.collectScale = int.Parse(input_collectScale_erverMenbers.text);
                clubSetting.collectScore = int.Parse(input_collectScore_erverMenbers.text);
            }

            clubSetting.scoreLimit = int.Parse(input_scoreLimit.text);

            MessageSend.SetCollectSetting(ClubItem.clubItemState.bindGwInfo.groupInfo.clubId, clubSetting);
        }
    }
}


