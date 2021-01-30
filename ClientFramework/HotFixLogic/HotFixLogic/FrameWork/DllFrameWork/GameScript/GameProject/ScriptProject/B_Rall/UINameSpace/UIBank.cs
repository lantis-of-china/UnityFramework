using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace UINameSpace
{
    public class UIBank : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIBank_Rall, _className);
            return 1;
        }

        public UIBank()
        {
            assetsName = Rall.UIDefineName.UIBank_Rall;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public Button btnClose;

        /// <summary>
        /// 验证密码输入类
        /// </summary>
        public class SendPassValied
        {
            public static GameObject panelNode;

            public static InputField entryPass;

            public static Button submit;

            public static void GetPanel(GameObject node)
            {
                panelNode = node;
                entryPass = GenericityTool.GetComponentByPath<InputField>(node, "input_password");
                submit = GenericityTool.GetComponentByPath<Button>(node, "btn_submit");

                submit.onClick.AddListener(Submit);
            }

            public static void Submit()
            {
				FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
				if (string.IsNullOrEmpty(entryPass.text))
                {
                    UINameSpace.UITipMessage.PlayMessage("密码不能为空!");
                    return;
                }

				Rall.MessageSend.LoginBank(entryPass.text);
            }

            public static void Open()
            {
                panelNode.SetActive(true);
            }

            public static void Close()
            {
                panelNode.SetActive(false);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public class ChangePass
        {
            public static GameObject panelNode;

            public static InputField entryPass;

            public static InputField input_changePassword_1;

            public static InputField input_changePassword_2;

            public static Button btns_submit;

            public static Button btn_bank;

            public static void GetPanel(GameObject node)
            {
                panelNode = node;
                entryPass = GenericityTool.GetComponentByPath<InputField>(node, "input_password");
                input_changePassword_1 = GenericityTool.GetComponentByPath<InputField>(node, "input_changePassword_1");
                input_changePassword_2 = GenericityTool.GetComponentByPath<InputField>(node, "input_changePassword_2");
                btns_submit = GenericityTool.GetComponentByPath<Button>(node, "btn_submit");
                btn_bank = GenericityTool.GetComponentByPath<Button>(node, "btn_bank");
                btns_submit.onClick.AddListener(Submit);
                btn_bank.onClick.AddListener(Bank);
            }

            public static void Submit()
            {
                if (string.IsNullOrEmpty(entryPass.text))
                {
                    UINameSpace.UITipMessage.PlayMessage("密码不能为空!");
                    return;
                }

                if (string.IsNullOrEmpty(input_changePassword_1.text))
                {
                    UINameSpace.UITipMessage.PlayMessage("请输入新密码!");
                    return;
                }

                if (string.IsNullOrEmpty(input_changePassword_2.text))
                {
                    UINameSpace.UITipMessage.PlayMessage("请确认新密码是否正确!");
                    return;
                }

				Rall.MessageSend.ChangeBankPassword(entryPass.text, input_changePassword_1.text);
            }
            public static void Bank()
            {
				FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
				Close();
                BankTask.Open();
            }

            public static void Open()
            {
                panelNode.SetActive(true);
            }

            public static void Close()
            {
                panelNode.SetActive(false);
            }
        }

        /// <summary>
        /// 银行
        /// </summary>
        public class BankTask
        {
            public static GameObject panelNode;
            public static Text txt_gold;
            public static Text txt_recharge;
            public static Text txt_goldBank;
            public static Text txt_rechargeBank;

            public static InputField inputCount;
            public static Button btnGold;
            public static Button btnRecharge;
            public static Button btnSave;
            public static Button btnGet;
            public static Button btnChangePassword;

            public static Image selectImage;
            public static Image selectNoImage;

            /// <summary>
            /// 操作类型 1 存入 2 取出
            /// </summary>
            public static byte controlType = 1;
            /// <summary>
            /// 货币类型 1 钻石 2 金币
            /// </summary>
            public static byte pointType = 1;

            public static void GetPanel(GameObject node)
            {
                panelNode = node;
                selectImage = GenericityTool.GetComponentByPath<Image>(node, "selectImg");
                selectNoImage = GenericityTool.GetComponentByPath<Image>(node, "selectNoImg");

                txt_gold = GenericityTool.GetComponentByPath<Text>(node, "lb_gold");
                txt_recharge = GenericityTool.GetComponentByPath<Text>(node, "lb_recharge");
                txt_goldBank = GenericityTool.GetComponentByPath<Text>(node, "lb_goldBank");
                txt_rechargeBank = GenericityTool.GetComponentByPath<Text>(node, "lb_rechargeBank");

                inputCount = GenericityTool.GetComponentByPath<InputField>(node, "input_controlCount");
                btnGold = GenericityTool.GetComponentByPath<Button>(node, "btn_gold");
                btnRecharge = GenericityTool.GetComponentByPath<Button>(node, "btn_recharge");

                btnSave = GenericityTool.GetComponentByPath<Button>(node, "btn_save");
                btnGet = GenericityTool.GetComponentByPath<Button>(node, "btn_get");
                btnChangePassword = GenericityTool.GetComponentByPath<Button>(node, "btn_changePassword");

                btnGold.onClick.AddListener(btnSelectGoldCall);
                btnRecharge.onClick.AddListener(btnSelectRechargeCall);
                btnSave.onClick.AddListener(btnSaveCall);
                btnGet.onClick.AddListener(btnGetCall);
                btnChangePassword.onClick.AddListener(btnChangePasswordCall);
            }

            public static void btnSelectGoldCall()
            {
                pointType = 2;
                UpInfo();
            }
            public static void btnSelectRechargeCall()
            {
                pointType = 1;
                UpInfo();
            }
            public static void btnSaveCall()
            {
                controlType = 1;
                SendTranslate();
            }
            public static void btnGetCall()
            {
                controlType = 2;
                SendTranslate();
            }
            public static void btnChangePasswordCall()
            {
                ChangePass.Open();
                Close();
            }

            public static void SendTranslate()
            {
                if (string.IsNullOrEmpty(inputCount.text))
                {
                    UINameSpace.UITipMessage.PlayMessage("数量不能为空!");
                    return;
                }
                int controlValue = 0;

                try
                {
                    controlValue = int.Parse(inputCount.text);
					Rall.MessageSend.TranslateBank(controlType, pointType, controlValue);
                }
                catch(Exception e)
                {
                    UINameSpace.UITipMessage.PlayMessage("操作异常!");
                }
            }

            public static void UpInfo()
            {
                if (pointType == 1)
                {
                    //钻石
                    (btnGold.targetGraphic as Image).overrideSprite = selectNoImage.overrideSprite;
                    (btnRecharge.targetGraphic as Image).overrideSprite = selectImage.overrideSprite;
                }
                else
                {
                    //金币
                    (btnGold.targetGraphic as Image).overrideSprite = selectImage.overrideSprite;
                    (btnRecharge.targetGraphic as Image).overrideSprite = selectNoImage.overrideSprite;
                }
            }

            public static void ShowBankTask(int gold, int recharge,int goldBank,int rechargeBank)
            {
                txt_gold.text = gold.ToString();
                txt_recharge.text = recharge.ToString();
                txt_goldBank.text = goldBank.ToString();
                txt_rechargeBank.text = rechargeBank.ToString();

                Open();
                SendPassValied.Close();
            }

            public static void Open()
            {
                panelNode.SetActive(true);

                UpInfo();
            }



            public static void Close()
            {
                panelNode.SetActive(false);
            }
        }


        public override void OnAwake()
        {
            base.OnAwake();

            btnClose = GenericityTool.GetComponentByPath<Button>(objectInstance, "anchorNode/animationNode/btn_close");
            if (btnClose != null) btnClose.onClick.AddListener(CloseUI);

            SendPassValied.GetPanel(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/panel_sendPassword"));
            ChangePass.GetPanel(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/panel_changePassword"));
            BankTask.GetPanel(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/panel_rank"));
        }

        public override void OnEnable()
        {
            base.OnEnable();
            SendPassValied.Open();
            ChangePass.Close();
            BankTask.Close();

            UIObject uiRall = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIRall_Rall);
            if (uiRall != null)
            {
                (uiRall as UINameSpace.UIRall).PlayOut(0.0f);
            }
        }

        private void CloseUI()
        {
            FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIBank_Rall,eCloseType.None);

            UIObject uiRall = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIRall_Rall);
            if (uiRall != null)
            {
                (uiRall as UINameSpace.UIRall).PlayIn(0.0f);
            }
        }
    }
}
