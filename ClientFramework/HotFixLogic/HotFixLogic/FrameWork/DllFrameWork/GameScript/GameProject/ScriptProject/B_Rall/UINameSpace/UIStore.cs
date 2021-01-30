using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UINameSpace
{
    public class UIStore : UIObject
    {
		private static UIStore __Instance;

		public static UIStore GetInstance()
		{
			return __Instance;
		}

		public override void SetInstance(UIObject target)
		{
			__Instance = target as UIStore;
		}

		public GameObject animationNode;

        public Button btnClose;

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIStore_Rall, _className);
            return 1;
        }


        public UIStore()
        {
            assetsName = Rall.UIDefineName.UIStore_Rall;
        }

        
        public override void OnAwake()
        {
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode");

            btnClose = GenericityTool.GetComponentByPath<Button>(objectInstance, "anchorNode/animationNode/btnClose");

            btnClose.onClick.AddListener(OnClickClose);

			Rall.UIStorePay.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/paySelectPanel"));
			Rall.UIStoreWebViewPay.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/payPanel"));

			for (int i = 0; i < 2; i++)
            {
                string btnPath = "";
                string panelPath = "";
                TablePanelItem storePanel = null;
                if (i == 0)
                {
                    storePanel = new Rall.PutongPanel_Select();
                    btnPath = "table_store/table_0";
                    panelPath = "panel_store/putongPanel";
                }
                else if(i == 1)
                {
                    storePanel = new Rall.DailiPanel_Select();
                    btnPath = "table_store/table_1";
                    panelPath = "panel_store/dailiPanel";
                }

                storePanel.tag = "StorePanel";
                storePanel.index = i;
                storePanel.tableButton = GenericityTool.GetComponentByPath<Button>(animationNode, btnPath);
                storePanel.tablePanel = GenericityTool.GetObjectByPath(animationNode, panelPath);
                storePanel.RegistListen();
            }
            TablePanelItem firstTablePanel = TablePanelItem.GetFirstTablePanelWithTag("StorePanel");
            firstTablePanel.SelectPanel();

        }

        public override void OnEnable()
        {
            base.OnEnable();
			Rall.EntryCode_Select.isEntry = false;
			Rall.UIStorePay.SetActive(false);
			Rall.UIStoreWebViewPay.SetActive(false);
		}

        public void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIStore_Rall, eCloseType.None);
        }

        public override void OnDispose()
        {
            base.OnDispose();
            TablePanelItem.CloseTableWithTag("StorePanel");
            TablePanelItem.CloseTableWithTag("TypePanel");
        }
    }
}

namespace Rall
{
	public class UIStorePay
	{
		public static GameObject panelNode;

		public static Button btn_close;

		public static Button btn_weiChatPay;

		public static Button btn_aliPay;
		/// <summary>
		/// 绑定的物品数据
		/// </summary>
		public static GoodsThingData bindThingData;


		public static void GetUI(GameObject item)
		{
			panelNode = item;

			btn_close = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_close");
			btn_weiChatPay = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_weiChatPay");
			btn_aliPay = GenericityTool.GetComponentByPath<Button>(panelNode, "btn_aliPay");

			btn_close.onClick.AddListener(OnClickClose);
			btn_weiChatPay.onClick.AddListener(OnClickWeiChatPay);
			btn_aliPay.onClick.AddListener(OnClickAliPay);
		}

		public static void OnClickClose()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);
		}


		public static void OnClickWeiChatPay()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			//UINameSpace.UITipMessage.PlayMessage("购买暂未开启!");
			OrderPay("weixinpay");

			SetActive(false);
		}

		public static void OnClickAliPay()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			//UINameSpace.UITipMessage.PlayMessage("购买暂未开启!");
			OrderPay("alipay");
			SetActive(false);
		}

		public static void IAPPay()
		{
			IAPInterface.ValiadeActionCall = UIStoreWebViewPay.IAPValiadeCall;
			OrderPay("iap");
		}

		/// <summary>
		/// 支付
		/// </summary>
		/// <param name="payType"></param>
		public static void OrderPay(string payType)
		{
			DebugLoger.Log("OrderPay " + payType);
			MessageSend.StorePay("123", payType, bindThingData.key);
		}

		public static void SetActive(bool active)
		{
			if (UpResourceManager.Instance.IsLimitFunVersion() && active && Application.platform == RuntimePlatform.IPhonePlayer)
			{
				panelNode.SetActive(false);
				UINameSpace.UITipMessage.PlayMessage("请等待调起支付...");
				IAPPay();
			}
			else
			{
				panelNode.SetActive(active);
			}
		}
	}


	public class UIStoreWebViewPay
	{
		public static GameObject panelNode;

		public static Button btn_finish;

		public static Button btn_cansal;

		public static Server.SC_BuyStore bindBuyData;

		public static void GetUI(GameObject uiNode)
		{
			panelNode = uiNode;

			btn_finish = GenericityTool.GetComponentByPath<Button>(uiNode, "btn_finish");
			btn_cansal = GenericityTool.GetComponentByPath<Button>(uiNode, "btn_cansal");

			btn_finish.onClick.AddListener(OnClickFinish);
			btn_cansal.onClick.AddListener(OnClickCansal);
		}

		/// <summary>
		/// 点击完成
		/// </summary>
		public static void OnClickFinish()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);

			//拉取金币
			MessageSend.RefenceInfo();
		}

		/// <summary>
		/// 点击取消
		/// </summary>
		public static void OnClickCansal()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			SetActive(false);

            //拉取金币
            MessageSend.RefenceInfo();
		}
		
		public static void SetActive(bool active)
		{
			panelNode.SetActive(active);
		}

		public static void IAPValiadeCall(string receipt)
		{
			Dictionary<string, string> resultMap = new Dictionary<string, string>
			{
				{ "isSandbox","false"},
				{ "receipt-data","receipt"},
				{ "out_trade_no",bindBuyData.orderId },
				{ "transactionid","transactionid" },
			};

			if (UpResourceManager.Instance.IsLimitFunVersion())
			{
				resultMap["isSandbox"] = "true";
			}

			string msg = LitJson.JsonMapper.ToJson(resultMap);

			HttpTools.PostHttpData(string.Format("http://{0}:9893", StringConfigClass.domAddr), System.Text.Encoding.UTF8.GetBytes(msg), UpIAPCallBack);
		}

		static public void UpIAPCallBack(string data)
		{
			DebugLoger.Log("IAP支付结果!");
		}

		/// <summary>
		/// 下单回调
		/// </summary>
		/// <param name="agentCode"></param>
		public static void CallBackOrder(Server.SC_BuyStore entryMsg)
		{
			try
			{
				if (entryMsg.result != 1)
				{
					UINameSpace.UITipMessage.PlayMessage("下单失败!");
					return;
				}
				bindBuyData = entryMsg;

				DebugLoger.Log("entryMsg.payType " + entryMsg.payType);
				if (entryMsg.payType == "weixinpay")
				{
					string payEntry = entryMsg.url;
					LitJson.JsonData jsData = CSTools.JsonToData(payEntry);
					CherishUtility.WeiChatPay(
						jsData["partnerid"].ToString(),
						jsData["prepayid"].ToString(),
						jsData["noncestr"].ToString(),
						jsData["timestamp"].ToString(),
						jsData["sign"].ToString());

					//appid = dicParam["appid"],
					//	partnerid = dicParam["partnerid"],
					//	prepayid = dicParam["prepayid"],
					//	package = dicParam["package"],
					//	noncestr = dicParam["noncestr"],
					//	timestamp = dicParam["timestamp"],
					//	sign = dicParam["sign"]
				}
				else if (entryMsg.payType == "alipay")
				{
					CherishUtility.AliPay(entryMsg.url);
				}
				else if (entryMsg.payType == "iap")
				{
					if (IAPInterface.Instance.CanPurchase())
					{
						IAPInterface.Instance.BuyPay(entryMsg.id);
					}
					else
					{
						UINameSpace.UITipMessage.PlayMessage("无法支付,请检查系统支付是否正常!");
					}
				}

				/*仟易付
				DebugLoger.Log("下单成功!");
				string signStr = string.Format("userid={0}&orderid={1}&bankid={2}&keyvalue={3}", entryMsg.payUserId, entryMsg.orderId, entryMsg.payType, entryMsg.payKey);

				string sign = CSTools.GetMD5(signStr, System.Text.Encoding.GetEncoding("gb2312"));
				string getUrl = string.Format("?userid={0}&orderid={1}&money={2}&hrefurl={3}&url={4}&bankid={5}&sign={6}&ext={7}",
					entryMsg.payUserId,
					entryMsg.orderId,
					entryMsg.price,
					entryMsg.hrefurl,
					entryMsg.url,
					entryMsg.payType,
					sign,
					"qy");

				string callUrl = entryMsg.payUrl + getUrl;
				HttpTools.GetHttpData(callUrl, PayCallBack);
				*/
				SetActive(true);
			}
			catch (System.Exception e)
			{
				DebugLoger.LogError(e.ToString());
			}
		}

		public static void PayCallBack(string value)
		{
            DebugLoger.Log("Pay Html " + value);
			System.IO.File.WriteAllText(Application.persistentDataPath + "/pay.html", value,System.Text.Encoding.GetEncoding("gb2312"));

            //WWW a = new WWW(FrameWorkDrvice.AssetsPathManagerInstance.GetFileProtocol() + Application.persistentDataPath + "//pay.html");

            //Application.OpenURL(a.url);
            CherishUtility.OpenWebView(FrameWorkDrvice.AssetsPathManagerInstance.GetFileProtocol() + Application.persistentDataPath + "//pay.html");
            DebugLoger.Log(FrameWorkDrvice.AssetsPathManagerInstance.GetFileProtocol() + Application.persistentDataPath + "//pay.html");
		}
	}
}


