using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈统计管理
    /// </summary>
    public class ClubStatisticsPanel_Select : TablePanelItem
    {
        public static ClubStatisticsPanel_Select Instance;
        /// <summary>
        /// 当前房卡数量
        /// </summary>
        public Text txt_curRechargeCount;
        /// <summary>
        /// 今日房卡消耗数量
        /// </summary>
        public Text txt_todayRechargeCount;
        /// <summary>
        /// 今日房卡消耗数量
        /// </summary>
        public Text txt_toldRechargeCount;
        /// <summary>
        /// 群主房卡数量
        /// </summary>
        public Text txt_masterRechargeCount;
        /// <summary>
        /// 变化数量
        /// </summary>
        public InputField input_changeCount;
        /// <summary>
        /// 按钮减
        /// </summary>
        public Button btn_sub;
        /// <summary>
        /// 按钮加
        /// </summary>
        public Button btn_add;
        /// <summary>
        /// 按钮提交
        /// </summary>
        public Button btn_submit;
        /// <summary>
        /// 绑定的亲友圈信息
        /// </summary>
        public P_GroupInfo bindGroupInfo;

        private int curValue;

        public void ResetShow()
        {
            curValue = 0;
            if (ClubItem.clubItemState != null)
            {
                ShowInfo(ClubItem.clubItemState.bindGwInfo.groupInfo);
            }
        }

        /// <summary>
        /// 获取面包信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
            Instance = this;
            txt_curRechargeCount = GenericityTool.GetComponentByPath<Text>(tablePanel, "txt_curRechargeCount");
            txt_todayRechargeCount = GenericityTool.GetComponentByPath<Text>(tablePanel, "txt_todayUseRechargeCount");
            txt_toldRechargeCount = GenericityTool.GetComponentByPath<Text>(tablePanel, "txt_toldUseRechargeCount");
            txt_masterRechargeCount = GenericityTool.GetComponentByPath<Text>(tablePanel, "txt_masterRechargeCount");
            input_changeCount = GenericityTool.GetComponentByPath<InputField>(tablePanel, "input_changeCount");

            btn_sub = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_sub");
            btn_add = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_add");
            btn_submit = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_submit");
            btn_sub.onClick.AddListener(OnClickSub);
            btn_add.onClick.AddListener(OnClickAdd);
            btn_submit.onClick.AddListener(OnClickSubmit);

			input_changeCount.onValueChanged.AddListener(OnValueChange);

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
        /// 显示信息
        /// </summary>
        public void ShowInfo(P_GroupInfo groupInfo)
        {
            bindGroupInfo = groupInfo;

            UpShow();
        }

        /// <summary>
        /// 显示全部
        /// </summary>
        public void UpShow()
        {
			input_changeCount.text = curValue.ToString();
			txt_curRechargeCount.text = bindGroupInfo.rechargeCount.ToString();
            txt_todayRechargeCount.text = bindGroupInfo.toDayUseRechargeCount.ToString();
            txt_toldRechargeCount.text = bindGroupInfo.toldUseRechargeCount.ToString();

            txt_masterRechargeCount.text = GoableData.userValiadateInforWarp.RechargeCount.ToString();
        }

        /// <summary>
        /// 减少
        /// </summary>
        private void OnClickSub()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			curValue--;

            if(curValue < -bindGroupInfo.rechargeCount)
            {
                curValue = -bindGroupInfo.rechargeCount;
            }

            input_changeCount.text = curValue.ToString();
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void OnClickAdd()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			curValue++;

            if (curValue > GoableData.userValiadateInforWarp.RechargeCount)
            {
                curValue = GoableData.userValiadateInforWarp.RechargeCount;
            }

            input_changeCount.text = curValue.ToString();
        }

		private void OnValueChange(string value)
		{
			try
			{
				try
				{
					curValue = int.Parse(value);
				}
				catch
				{ }

				if (curValue > GoableData.userValiadateInforWarp.RechargeCount)
				{
					curValue = GoableData.userValiadateInforWarp.RechargeCount;
				}

				if (curValue < -bindGroupInfo.rechargeCount)
				{
					curValue = -bindGroupInfo.rechargeCount;
				}

				input_changeCount.text = curValue.ToString();
			}
			catch(Exception e)
			{
				DebugLoger.LogError(e.ToString());
			}
		}

        /// <summary>
        /// 提交
        /// </summary>
        private void OnClickSubmit()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			curValue = int.Parse(input_changeCount.text);

            if (GoableData.userValiadateInforWarp.RechargeCount < curValue)
            {
                UINameSpace.UITipMessage.PlayMessage("个人房卡不足!");
                return;
            }

            UINameSpace.UIWaitting.AddShowWaitting("MessageSend.ChangeClubRecharge");
            MessageSend.ChangeClubRecharge(bindGroupInfo.clubId, curValue);
        }
    }
}


