using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class IAPInterface : MonoBehaviour
{
	public static Action<string> ValiadeActionCall;
	private static IAPInterface _instance;
	public static IAPInterface Instance { get { if (_instance == null) { GameObject IAPObje = new GameObject("IAPurchase"); _instance = IAPObje.AddComponent<IAPInterface>(); } return _instance; } }

#if (UNITY_IPHONE) && !UNITY_EDITOR
    [DllImport("__Internal")]
    public static extern void _InstancePurchase(string CallBackObjName, string CallFunName);

    [DllImport("__Internal")]
    public static extern bool _CanPurchase();

    [DllImport("__Internal")]
    public static extern void _BuyPay(string prounctId);
#endif

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}

	public void InstancePurchase()
	{
		this.gameObject.name = "IAPurchase";
#if (UNITY_IPHONE) && !UNITY_EDITOR
        _InstancePurchase(gameObject.name, "CallBackInfor");
#endif
	}

	public bool CanPurchase()
	{
#if (UNITY_IPHONE) && !UNITY_EDITOR
        return _CanPurchase();
#endif
		return false;
	}

	public void BuyPay(string prounctId)
	{
#if (UNITY_IPHONE) && !UNITY_EDITOR
        _BuyPay(prounctId);
#endif
	}

	//回调方法类型
	void CallBackInfor(string Receipt)
	{
		//GUIDebugLog.AddLog("CallBackIap");
		if (!string.IsNullOrEmpty(Receipt))
		{
			string[] InforArray = Receipt.Split('/');

			if (InforArray[0] == "Sucess")
			{
				if (ValiadeActionCall != null)
				{
					ValiadeActionCall(InforArray[1]);
				}
				//GUIDebugLog.AddLog("CallBackIap Sucess");
				//IOSNative.showMessage(Application.systemLanguage == SystemLanguage.Chinese ? "购买提示！" : "Purchase tips", Application.systemLanguage == SystemLanguage.Chinese ? "购买成功！" : "Purchase sucessed");

			}
			else
				if (InforArray[0] == "Restore")
			{ }
			else
				if (InforArray[0] == "Failed")
			{
				//GUIDebugLog.AddLog("CallBackIap Failed");
				//IOSNative.showMessage(Application.systemLanguage == SystemLanguage.Chinese ? "购买提示！" : "Purchase tips", Application.systemLanguage == SystemLanguage.Chinese ? "购买失败！" : "Purchase failed");
			}
		}
	}
}
