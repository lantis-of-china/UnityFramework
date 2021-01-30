using ILRuntime.Runtime.Intepreter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/// <summary>
/// UI关闭类型
/// </summary>
public enum eCloseType
{
	None,
	TimeRecord,
	Queue,
	TimeRecordAndQueue
}

/// <summary>
/// UI层级
/// </summary>
public enum eUiShowLayer
{
	Hud = 0,
	UI = 1,
	UIUrgency = 2,
    UITop = 3,
	UIMask = 4
}


public class UIObject
{
	public UIObject()
	{ }

	public virtual void SetInstance(UIObject target)
	{
	}

	public AssetsData assetInfor;
	/// <summary>
	/// 资源名
	/// </summary>
	public string assetsName;
	/// <summary>
	/// 资源对象
	/// </summary>
	public GameObject objectInstance;

	public Canvas mCanvas = null;                           //Canvas
															/// <summary>
															/// 
															/// </summary>
	public Canvas[] mCanvasAry = null;

	/// <summary>
	/// 需要调整层级的渲染器
	/// </summary>
	public List<Type> renderTypeList = new List<Type>()
	{
		typeof(MeshRenderer),
		typeof(SkinnedMeshRenderer),
		typeof(TrailRenderer),
		typeof(LineRenderer),
		typeof(RenderOrderLayerSet)
	};
	/// <summary>
	/// 获取的特殊渲染器列表
	/// </summary>
	public List<Component> renderList = new List<Component>();


	public RectTransform mRectTrans = null;                 //RectTransform

	public eUiShowLayer mUiShowLayer = eUiShowLayer.UI;

	public bool isuiAniamtion;
	#region 动画

	public void SetAplashStart()
	{
		UnityEngine.CanvasGroup cg = objectInstance.GetComponent<UnityEngine.CanvasGroup>();
		if (cg != null)
		{
			cg.alpha = 0.0f;
		}
	}

	public void OpenAplash(CherishTween.ParamarCallFun callFun, object paramars)
	{
		isuiAniamtion = true;
		CherishTweenCanvasApash.Begin(objectInstance, 0.0f, 1.0f, 0.3f, 0.0f, true, (obj) =>
			{
				isuiAniamtion = false;
				if (callFun != null)
				{
					callFun(paramars);
				}
			}, null);
	}

	public void CloseAplash(CherishTween.ParamarCallFun callFun, object paramars)
	{
		isuiAniamtion = true;
		CherishTweenCanvasApash.Begin(objectInstance, 1.0f, 0.0f, 0.3f, 0.0f, true, (obj)=> 
		{
			isuiAniamtion = false;
			if (callFun != null)
			{
				callFun(paramars);
			}
		}, null);
	}
	#endregion 动画

	public void SetActive(bool active)
	{
		objectInstance.SetActive(active);
	}

	public void SetLocalPos(Vector3 localPos)
	{
		objectInstance.transform.localPosition = localPos;
	}

	public void GetButtonBindSound()
	{
		//Button[] btnList = objectInstance.GetComponentsInChildren<Button>();
		//if (btnList != null)
		//{
		//	for (int i = 0; i < btnList.Length; ++i)
		//	{
		//		btnList[i].onClick.AddListener(() =>
		//		{
		//			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(GameLogic.ConfigProject.sounds, "btn_general");
		//		});
		//	}
		//}
	}

	/// <summary>
	/// Ui事件单例委托
	/// </summary>
	public virtual void OnAwake() { }

	/// <summary>
	/// 注册事件
	/// </summary>
	public virtual void OnRegistEvent()
	{ }

	/// <summary>
	/// 打开一次调用一次 用于刷新次数
	/// </summary>
	public virtual void OnEnable()
	{
	}
	/// <summary>
	/// 关闭一次调用一次
	/// </summary>
	public virtual void OnDisable() { }
	/// <summary>
	/// update更新事件
	/// </summary>
	public virtual void OnUpdate() { }

	/// <summary>
	/// 
	/// </summary>
	public virtual void OnLateUpdate() { }

	/// <summary>
	/// UI关闭事件
	/// </summary>
	public virtual void OnClose() { }

	/// <summary>
	/// 释放注册事件
	/// </summary>
	public virtual void OnUnRegistEvent()
	{ }

	/// <summary>
	/// 销毁事件
	/// </summary>
	public virtual void OnDispose() { }

	/// <summary>
	/// 游戏大厅回到大厅
	/// </summary>
	public virtual void OnGameBackRall() { }
	/// <summary>
	/// 是否开启
	/// </summary>
	public bool isOpen;
	/// <summary>
	/// 关闭类型
	/// </summary>
	public eCloseType closeType;
	/// <summary>
	/// 关闭计时
	/// </summary>
	public float closeTimeRecord;

	private int recordBaseLayer;

	public int baseLayer
	{
		set
		{
			recordBaseLayer = value;

			if (mCanvas != null)
			{
				int reocrdOrder = mCanvas.sortingOrder;

				if (mCanvasAry != null)
				{
					for (int i = 0; i < mCanvasAry.Length; ++i)
					{
						mCanvasAry[i].overrideSorting = true;
						mCanvasAry[i].sortingOrder += (value - reocrdOrder);
					}
				}

				mCanvas.sortingOrder = value;
				SetRenderOrder(renderList, value - reocrdOrder);
			}
		}

		get
		{
			return recordBaseLayer;
		}
	}

	/// <summary>
	/// 设置到最上面
	/// </summary>
	public void ToBestLayer()
	{
		UIManager.SetLayerToEnd(this);
		mRectTrans.SetAsLastSibling();
	}

	

	/// <summary>
	/// 设置渲染层级
	/// </summary>
	/// <param name="renderSetList"></param>
	/// <param name="sortingOrder"></param>
	public void SetRenderOrder(List<Component> renderSetList,int sortingOrder)
	{
		if (renderSetList != null)
		{
			for (int i = 0; i < renderSetList.Count; ++i)
			{
				Component componentThis = renderSetList[i];

				if (componentThis is Renderer)
				{
					(renderSetList[i] as Renderer).sortingOrder += sortingOrder;
				}

				else if (componentThis is RenderOrderLayerSet)
				{
					(renderSetList[i] as RenderOrderLayerSet).sortingOrder += sortingOrder;
					(renderSetList[i] as RenderOrderLayerSet).RefenceOrder();
				}
			}
		}
	}

	/// <summary>
	/// 获取特效渲染组件
	/// </summary>
	/// <returns></returns>
	public List<Component> GetEffectRenderComponent(GameObject effectNode)
	{
		List<Component> curRenderList = new List<Component>();

		#region 获取粒子特殊
		ParticleSystem[] pariticleAry = effectNode.GetComponentsInChildren<ParticleSystem>(true);

		if (pariticleAry != null)
		{
			for (int i = 0; i < pariticleAry.Length; ++i)
			{
				Renderer renderParitcle = pariticleAry[i].GetComponent<Renderer>();
				if (renderParitcle != null)
				{
					curRenderList.Add(renderParitcle);
				}
			}
		}
		#endregion 获取粒子特殊

		for (int i = 0; i < renderTypeList.Count; ++i)
		{
			Component[] components = effectNode.GetComponentsInChildren(renderTypeList[i], true);

			if (components != null)
			{
				for (int j = 0; j < components.Length; ++j)
				{
					curRenderList.Add(components[j]);
				}
			}
		}

		return curRenderList;
	}

	/// <summary>
	/// 设置特效到UI特效上
	/// </summary>
	/// <param name="effectNode"></param>
	public void SetEffectToUISorder(GameObject effectNode)
	{
		SetRenderOrder(GetEffectRenderComponent(effectNode), baseLayer);
	}


	/// <summary>
	/// 设置特效到UI特效上
	/// </summary>
	/// <param name="effectNode"></param>
	public void SetEffectResetUISorder(GameObject effectNode)
	{
		SetRenderOrder(GetEffectRenderComponent(effectNode), -baseLayer);
	}


	/// <summary>
	/// 设置层级到父节点层级
	/// </summary>
	/// <param name="effectNode"></param>
	public void SetEffectToParentLayer(GameObject effectNode)
	{
		Canvas nowCanvaw = effectNode.GetComponentInParent<Canvas>();

		if (nowCanvaw != null)
		{
			SetRenderOrder(GetEffectRenderComponent(effectNode), nowCanvaw.sortingOrder);
		}
		else
		{
			DebugLoger.LogError("SetEffectToParentLayer Not Find Parent Canvas");
		}
	}

	/// <summary>
	/// 设置特效到UI特效上
	/// </summary>
	/// <param name="effectNode"></param>
	public void SetEffectResetParentUISorder(GameObject effectNode)
	{
		Canvas nowCanvaw = effectNode.GetComponentInParent<Canvas>();

		if (nowCanvaw != null)
		{
			SetRenderOrder(GetEffectRenderComponent(effectNode), -nowCanvaw.sortingOrder);
		}
		else
		{
			DebugLoger.LogError("SetEffectToParentLayer Not Find Parent Canvas");
		}

	}

	/// <summary>
	/// 设置UI信息
	/// </summary>
	/// <param name="go"></param>
	public void SetUIInstance(GameObject go)
	{
		objectInstance = go;
		mCanvas = objectInstance.GetComponent<Canvas>();

		if (mCanvas == null)
		{
			mCanvas = objectInstance.AddComponent<Canvas>();
		}

		mCanvas.overrideSorting = true;
		mCanvas.sortingOrder = 0;
		mCanvasAry = objectInstance.GetComponentsInChildren<Canvas>(true);
		renderList = GetEffectRenderComponent(objectInstance);
		mRectTrans = objectInstance.GetComponent<RectTransform>();
	}

	public void SetSizeDelta(Vector2 sizeDelta)
	{
		mRectTrans.sizeDelta = sizeDelta;
	}

	public void SetRect(RectTransform rootRect)
	{
		mRectTrans.sizeDelta = rootRect.sizeDelta;
		mRectTrans.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Top, 0, 0);
		mRectTrans.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Left, 0, 0);
		mRectTrans.anchorMin = Vector2.zero;
		mRectTrans.anchorMax = Vector2.one;
		rootRect.transform.localPosition = Vector3.zero;
	}

	/// <summary>
	/// EventTrigger 绑定事件
	/// </summary>
	/// <param name="eventTrigger"></param>
	/// <param name="eventTiggerType"></param>
	/// <param name="callFun"></param>
	public void AddEventTriggerEvent(EventTrigger eventTrigger, EventTriggerType eventTiggerType, UnityAction<BaseEventData> callFun)
	{
		UtilityTool.AddEventTriggerEvent(eventTrigger, eventTiggerType, callFun);
	}
	
	/// <summary>
	/// 关闭
	/// </summary>
	public void Close()
	{
		FrameWorkDrvice.UiManagerInstance.CloseUI(assetsName, eCloseType.Queue);
	}


	/// <summary>
	/// 获取字符串宽度
	/// </summary>
	/// <param name="sourceStr"></param>
	/// <returns></returns>
	public float GetWidthWithStr(string sourceStr, Text textField)
	{
		TextGenerator tg = new TextGenerator();
		TextGenerationSettings settingsW = textField.GetGenerationSettings(new Vector2(0, 0));
		float textSize = tg.GetPreferredWidth(sourceStr, settingsW);

		return textSize;
	}


	public float GetHeightWithStr(string sourceStr, Text textField)
	{
		TextGenerator tg = new TextGenerator();
		TextGenerationSettings settingsW = textField.GetGenerationSettings(new Vector2(0, 0));
		float textSize = tg.GetPreferredHeight(sourceStr, settingsW);

		return textSize;
	}


	/// <summary>
	/// 获取字符串高度
	/// </summary>
	/// <param name="sourceStr"></param>
	/// <returns></returns>
	public float GetStrHeight(string sourceStr, Text textField)
	{
		string[] sArray = sourceStr.Split(new char[] { '\n', '\r' });
		float heigth = GetHeightWithStr(" ", textField);

		return sArray.Length * heigth;
	}

	/// <summary>
	/// 获取结束字符串宽度
	/// </summary>
	/// <param name="sourceStr"></param>
	/// <returns></returns>
	float GetEndStrWidth(string sourceStr, Text textField)
	{
		int position = sourceStr.LastIndexOf("\n");

		if ((position + 1) == sourceStr.Length)
		{
			return 0.0f;
		}

		sourceStr = sourceStr.Substring(0, sourceStr.Length - 1);
		System.IO.StreamReader sr = new System.IO.StreamReader(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(sourceStr)));
		string st = string.Empty;

		while (!sr.EndOfStream)
		{
			st = sr.ReadLine();
		}

		sr.Close();

		return GetWidthWithStr(st, textField);
	}


	/// <summary>
	/// 自动设置宽度
	/// </summary>
	/// <param name="sourceStr"></param>
	public string AutoWidth(string sourceStr, Text textField)
	{
		string textInfor = "";
		string inforStr = "";
		float strWidth = 0.0f;
		float maxWidth = textField.preferredWidth;
		int lineCount = 0;
		List<StringCompare> stringCompareList = new List<StringCompare>();
		float widthStr = GetWidthWithStr(sourceStr, textField);

		if (widthStr > maxWidth)
		{
			List<string> stringList = new List<string>();

			for (int indexFont = 0; indexFont <= sourceStr.Length; indexFont++)
			{
				char charEncod = indexFont < sourceStr.Length ? sourceStr[indexFont] : ' ';
				strWidth = GetWidthWithStr(inforStr, textField);

				if (strWidth > maxWidth)
				{
					string inforChar = inforStr.Substring(inforStr.Length - 1);
					inforStr = inforStr.Remove(inforStr.Length - 1);
					stringList.Add(inforStr);
					string g = inforStr;
					inforStr = inforChar;

					for (int changeLoop = 0; changeLoop < stringCompareList.Count; changeLoop++)
					{
						StringCompare sc = stringCompareList[changeLoop];
						int values = indexFont + stringList.Count - 2;

						if (sc.index > values)
						{
							sc.index += 1;
						}
					}

					if (indexFont == sourceStr.Length)
					{
						stringList.Add(inforStr);
					}
				}
				else if (indexFont == sourceStr.Length)
				{
					stringList.Add(inforStr);
				}

				inforStr += charEncod;
			}

			string strText = "";

			for (int fieldIndex = 0; fieldIndex < stringList.Count; fieldIndex++)
			{
				lineCount++;
				string s = stringList[fieldIndex] + (fieldIndex < (stringList.Count - 1) ? "\n" : "");
				strText += s;
			}

			textInfor = strText;
		}

		return textInfor;
	}
}

/// <summary>
/// 文本比较设置使用的
/// </summary>
public class StringCompare
{
	public int index;

	public int length;

	public string command;

	public string assetName;
}


/// <summary>
/// UI管理器
/// </summary>
public class UIManager
{
	public static bool loadSync;

	public static bool isPortraitRecord;

	public static Vector2 UILandscapeSize = new Vector2(1920, 1080);

	public static Vector2 UIPortraitSize = new Vector2(1080, 1920);

	private static GameObject uiRoot;

	private static RectTransform uiRootRectTransform;

	private static GameObject uiEventSystem;

	public static int maxCrashCount = 50;

	public static float closeRecordTime = 20.0f;

	public static GameObject uiCameraObj;

	public static Camera uiCameraComp;

	public static UniversalAdditionalCameraData uiCameraData;

	public static UniversalAdditionalCameraData senceCameraData;

	public static Canvas uiCanvas;

	public static CanvasScaler uiCanvasScale;

	public static string UISpcaeName = "UINameSpace";

	public static string UIRegistFunName = "RegistSystem";

	public static string assetsFieldName = "assetsName";

	public static List<string> staticExternUI = new List<string>();

	public static Vector2 curResolution
	{
		get
		{
			return uiRootRectTransform.sizeDelta;
		}
	}

	/// <summary>
	/// UI对应的节点
	/// </summary>
	private static System.Collections.Generic.Dictionary<eUiShowLayer, GameObject> UiShowLayerDic = new System.Collections.Generic.Dictionary<eUiShowLayer, GameObject>();

	/// <summary>
	/// 功能对应的Ui
	/// </summary>
	private static System.Collections.Generic.Dictionary<string, string> defineUIFun = new System.Collections.Generic.Dictionary<string, string>();

	/// <summary>
	/// 缓存中的UI信息
	/// </summary>
	private static System.Collections.Generic.List<UIObject> crachUIMap = new System.Collections.Generic.List<UIObject>();

	/// <summary>
	/// 当前开启列表
	/// </summary>
	private static System.Collections.Generic.List<string> openTaskList = new System.Collections.Generic.List<string>();

    /// <summary>
    /// 是否点击在UI上
    /// </summary>
    /// <returns></returns>
    public static bool IsTouchOnUI()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if IPHONE || ANDROID
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
                return true;

            else
                return false;
        }

        return false;
    }

	/// <summary>
	/// 关 
	/// </summary>
	/// <param name="uiName"></param>
	/// <returns></returns>
	public static bool CloseExternUI(string uiName)
	{
		for (int i = 0; i < staticExternUI.Count; ++i)
		{
			if (staticExternUI[i] == uiName)
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// 获取横屏尺寸
	/// </summary>
	/// <returns></returns>
	public Vector2 GetLandscapeSize()
	{
		//if (Application.isMobilePlatform && Application.platform != RuntimePlatform.IPhonePlayer)
		//{
		//	return new Vector2(Screen.height, Screen.width);
		//}
		return UILandscapeSize;
	}

	/// <summary>
	/// 获取竖屏尺寸
	/// </summary>
	/// <returns></returns>
	public Vector2 GetPortraitSize()
	{
		//if (Application.isMobilePlatform && Application.platform != RuntimePlatform.IPhonePlayer)
		//{
		//	return new Vector2(Screen.width, Screen.height);
		//}
		return UIPortraitSize;
	}

	public void SetLandscape()
	{
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.AutoRotation;//ScreenOrientation.LandscapeLeft;
		//uiCanvasScale.matchWidthOrHeight = 0;

		uiCanvasScale.referenceResolution = GetLandscapeSize();
		UpUIAllLayer(false);
	}

	public void SetPortrait()
	{
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.Portrait;
		//uiCanvasScale.matchWidthOrHeight = 1;

		uiCanvasScale.referenceResolution =  GetPortraitSize();
		UpUIAllLayer(true);
	}

	public void UpUIAllLayer(bool isPortrait)
	{
		isPortraitRecord = isPortrait;

		RectTransform rootRt = GetUIRootRectTransform();
		rootRt.sizeDelta = isPortrait ? GetPortraitSize() : GetLandscapeSize();

		foreach (var kv in UiShowLayerDic)
		{
			RectTransform layerRt = kv.Value.GetComponent<RectTransform>();
			layerRt.sizeDelta = isPortrait ? GetPortraitSize() : GetLandscapeSize();
		}

		foreach (var item in crachUIMap)
		{
			item.SetRect(GetUIRootRectTransform());
			//item.SetSizeDelta(curResolution);
		}
	}

	public void Update()
	{
		for (int loopCrach = 0; loopCrach < crachUIMap.Count; ++loopCrach)
		{
			if (crachUIMap[loopCrach].isOpen && crachUIMap[loopCrach].objectInstance != null && crachUIMap[loopCrach].objectInstance.activeSelf)
			{
				crachUIMap[loopCrach].OnUpdate();
			}

		}

	}

	public void NewNode()
	{
		GameObject goNew = new GameObject("UiRoot");
		GameObject.DontDestroyOnLoad(goNew);
		uiRoot.transform.parent = goNew.transform;
	}

	public void RegistSenceCamera(Camera senceCamera)
	{
		if (uiCameraComp != null)
		{
			if (uiCameraData != null)
			{
				uiCameraData.renderType = CameraRenderType.Overlay;
			}

			if (senceCamera != null)
			{
				senceCameraData = senceCamera.GetUniversalAdditionalCameraData();

				if (senceCameraData != null)
				{
					senceCameraData.cameraStack.Clear();
					senceCameraData.cameraStack.Add(uiCameraComp);
				}
			}
		}
	}

	public void UnRegistSenceCamera()
	{
		if (senceCameraData != null)
		{
			senceCameraData.cameraStack.Clear();
			senceCameraData = null;
		}

		if (uiCameraData == null)
		{
			uiCameraData = uiCameraComp.GetUniversalAdditionalCameraData();
		}

		if (uiCameraData != null)
		{
			uiCameraData.renderType = CameraRenderType.Base;
		}
	}

	/// <summary>
	/// 反射注册所有的UI类
	/// </summary>
	public void RegistFunction()
	{
		if (uiRoot == null)
		{
			uiRoot = new GameObject("UIROOT");
			uiRoot.transform.SetParent(LSharpEntryGame.Instance.gameDontDestroy.transform);
			uiRoot.layer = LayerManager.UILayer;
			GameObject.DontDestroyOnLoad(uiRoot);

			uiCameraObj = new GameObject("UICAMERA");
			uiCameraObj.transform.parent = uiRoot.transform;
			uiCameraObj.transform.localScale = Vector3.one;
			uiCameraComp = uiCameraObj.AddComponent<Camera>();
			UnRegistSenceCamera();

			uiCameraComp.orthographic = true;
			uiCameraComp.cullingMask = 1 << LayerManager.UILayer;
			uiCameraComp.clearFlags = CameraClearFlags.Depth;
			uiCameraComp.backgroundColor = Color.black;
			uiCameraComp.depth = 10;

			uiEventSystem = new GameObject("EventSystem");
			uiEventSystem.transform.parent = uiRoot.transform;
			uiEventSystem.transform.localPosition = Vector3.zero;
			uiEventSystem.transform.localScale = Vector3.one;
			uiEventSystem.AddComponent<EventSystem>();
			uiEventSystem.AddComponent<StandaloneInputModule>();
			uiEventSystem.AddComponent<TouchInputModule>();

			uiCanvas = uiRoot.AddComponent<Canvas>();
			uiCanvasScale = uiRoot.AddComponent<UnityEngine.UI.CanvasScaler>();
			//uiRoot.AddComponent<UnityEngine.UI.GraphicRaycaster>();            
			uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
			uiCanvas.worldCamera = uiCameraComp;
			uiCanvasScale.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
			uiCanvasScale.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand;
			//uiCanvasScale.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			uiRootRectTransform = uiRoot.GetComponent<RectTransform>();

			//GetUiShowLayer(eUiShowLayer.Hud);
			//GetUiShowLayer(eUiShowLayer.UI);
			//GetUiShowLayer(eUiShowLayer.UIUrgency);
		}

		if (LSharpEntryGame.scriptType == ScriptType.Dotnet || LSharpEntryGame.scriptType == ScriptType.Script)
		{
			#region Cs
			System.Reflection.Assembly asb = System.Reflection.Assembly.GetExecutingAssembly();

			System.Type[] AssemblyTypes = asb.GetTypes();


			for (int indexType = 0; indexType < AssemblyTypes.Length; indexType++)
			{
				if (AssemblyTypes[indexType].Namespace == UISpcaeName && !AssemblyTypes[indexType].IsAbstract && AssemblyTypes[indexType].BaseType == typeof(UIObject))
				{
					//通过程序集获取到他的返回实例对象方法  并且初始化对象
					System.Reflection.MethodInfo mif = AssemblyTypes[indexType].GetMethod(UIRegistFunName);

					mif.Invoke(null, new object[] { AssemblyTypes[indexType].Namespace + "." + AssemblyTypes[indexType].Name });
				}
			}
			#endregion Cs
		}
		else if (LSharpEntryGame.scriptType == ScriptType.ILRuntime)
		{
			#region ILRuntime
			List<string> buffer = new List<string>(LSharpEntryGame.ILAppDomain.LoadedTypes.Keys);

			for (int loop = 0; loop < buffer.Count; ++loop)
			{
				ILRuntime.CLR.TypeSystem.IType value = LSharpEntryGame.ILAppDomain.LoadedTypes[buffer[loop]];

				if (value.FullName == UISpcaeName + "." + value.Name && (value.BaseType != null && value.BaseType.Name == "UIObject"))
				{
					ILRuntime.CLR.Method.IMethod ilMethod = value.GetMethod(UIRegistFunName, 1);
					LSharpEntryGame.ILAppDomain.Invoke(ilMethod, null, value.FullName);
				}
			}
			#endregion ILRuntime
		}
	}

	/// <summary>
	/// 反射注册UI回调
	/// </summary>
	/// <param name="_assetsName"></param>
	/// <param name="_className"></param>
	public void RegistFunctionCallFun(string _assetsName, string _className)
	{
		if (defineUIFun.ContainsKey(_assetsName))
		{
			DebugLoger.LogError("重复注册UI " + _assetsName);
			return;
		}

		defineUIFun.Add(_assetsName, _className);
	}

	/// <summary>
	/// 下载
	/// </summary>
	/// <param name="assetInfor"></param>
	/// <param name="assetsLoadType"></param>
	private void PreafbLoadCompleted(AssetsData assetInfor, eAssetsLoadType assetsLoadType)
	{
		if (assetInfor.assetsType == eAssetsType.GameObject)
		{
			DebugLoger.Log("资源预加载成功 " + assetInfor.assetsName);
		}
	}

	/// <summary>
	/// 下载
	/// </summary>
	/// <param name="assetInfor"></param>
	/// <param name="assetsLoadType"></param>
	private void LoadCompleted(AssetsData assetInfor, eAssetsLoadType assetsLoadType)
	{
		DebugLoger.Log($"ui load completed:{assetInfor.assetsName}");

		if (assetInfor.assetsType == eAssetsType.GameObject)
		{
			GameObject uiObj = assetInfor.assetsData as GameObject;

			if (uiObj == null)
			{
				DebugLoger.Log("uiObj " + assetInfor.assetsName + " 加载失败");

				return;
			}

			GameObject m_uiRoot = GameObject.Instantiate(uiObj) as GameObject;
			m_uiRoot.AddComponent<UnityEngine.UI.GraphicRaycaster>();
			m_uiRoot.AddComponent<CanvasGroup>();

			UIObject _targetUi = AddUI(assetInfor.assetsName, m_uiRoot);

			_targetUi.assetInfor = assetInfor;

			if (_targetUi != null)
			{
				ShowUI(_targetUi);
			}
			else
			{
				DebugLoger.Log($"ui load completed:{assetInfor.assetsName} _targetUi != null");
			}
		}
		else
		{
			DebugLoger.Log($"ui load completed assetsType not gameobject");
		}		
	}

	/// <summary>
	/// 任务处理
	/// </summary>
	public void FontoadFinishCall()
	{
		for (int loop = 0; loop < openTaskList.Count; loop++)
		{
			string[] aryStr = openTaskList[loop].Split('|');
			LoadUI(aryStr[0], aryStr[1]);
		}

		openTaskList.Clear();
	}

    /// <summary>
    /// 设置字体
    /// </summary>
    /// <param name="uiPrefab"></param>
    public void SetFont(GameObject _uiPrefab)
    {
        if (_uiPrefab != null)
        {
            UnityEngine.UI.Text[] _textComp = _uiPrefab.GetComponentsInChildren<UnityEngine.UI.Text>(true);

            for (short loop = 0; loop < _textComp.Length; ++loop)
            {
                UnityEngine.UI.Text _text = _textComp[loop];

                FontBindSet fontBindSet = _text.GetComponent<FontBindSet>();

                if (fontBindSet == null)
                {
                    _text.font = _text.font = FontManager.GetFont(); ;
                }
                else
                {
                    _text.font = FontManager.GetFont(fontBindSet.fontName);
                }
            }
        }
    }

	public static int GetBaseLayer(eUiShowLayer layer)
	{
		var spawnLayer = GenericityTool.EnumToInt(layer) * 10;

		return spawnLayer;
	}

	public static void SetBaseLayer(eUiShowLayer layer,int baseLayer)
	{
	}

	/// <summary>
	/// 设置深度
	/// </summary>
	/// <param name="_uiObject"></param>
	public void SetLayer(UIObject _uiObject)
	{
		int _baseLayer = GetBaseLayer(_uiObject.mUiShowLayer);

		for (int i = 0; i < crachUIMap.Count; ++i)
		{
			UIObject _getUiObj = crachUIMap[i];

			if (_getUiObj.isOpen && _getUiObj.mUiShowLayer == _uiObject.mUiShowLayer)
			{
				_getUiObj.baseLayer = _baseLayer * 10;
				_baseLayer++;
				SetBaseLayer(_uiObject.mUiShowLayer, _baseLayer);
			}
		}
	}


	/// <summary>
	/// 设置深度
	/// </summary>
	/// <param name="_uiObject"></param>
	public static void SetLayerToEnd(UIObject _uiObject)
	{
		if (crachUIMap.Contains(_uiObject))
		{
			DebugLoger.Log("SetLayerToEnd");
			///这里是把UI拿到队里末尾
			crachUIMap.Remove(_uiObject);
			crachUIMap.Add(_uiObject);

			int _baseLayer = GetBaseLayer(_uiObject.mUiShowLayer);
			for (int i = 0; i < crachUIMap.Count; ++i)
			{
				UIObject _getUiObj = crachUIMap[i];

				if (_getUiObj.isOpen && _getUiObj.mUiShowLayer == _uiObject.mUiShowLayer)
				{
					_getUiObj.baseLayer = _baseLayer * 10;
					_baseLayer++;
					SetBaseLayer(_uiObject.mUiShowLayer, _baseLayer);
				}
			}
		}
	}


	/// <summary>
	/// 添加UI对应功能
	/// </summary>
	/// <param name="uiName"></param>
	/// <param name="uiObj"></param>
	/// <returns></returns>
	public UIObject AddUI(string uiName, GameObject uiObj)
	{
		if (CheckUI(uiName))
		{
			DebugLoger.Log(uiName + " 已经存在");
			return null;
		}

		DebugLoger.Log($"AddUI {uiName}");

		UIObject _UiObject = null;

		if (defineUIFun.ContainsKey(uiName))
		{

			DebugLoger.Log($"AddUI isDefine:{uiName} createInstance start");
			if (LSharpEntryGame.scriptType == ScriptType.Dotnet || LSharpEntryGame.scriptType == ScriptType.Script)
			{
				_UiObject = Activator.CreateInstance(Type.GetType(defineUIFun[uiName], true, true)) as UIObject;
			}
			else if (LSharpEntryGame.scriptType == ScriptType.ILRuntime)
			{
				DebugLoger.Log("LSharpEntryGame.scriptType == ScriptType.ILRuntime");
				//var type = LSharpEntryGame.ILAppDomain.GetType(defineUIFun[uiName]);

				//if (type != null)
				//{
				//	var ilInstance = new ILTypeInstance((ILRuntime.CLR.TypeSystem.ILType)type);

				//	if (ilInstance != null)
				//	{
				//		if (ilInstance.CLRInstance != null)
				//		{
				//			_UiObject = (UIObject)ilInstance.CLRInstance;
				//		}
				//		else
				//		{
				//			DebugLoger.LogError("ilInstance.CLRInstance null");
				//		}
				//	}
				//	else
				//	{
				//		DebugLoger.LogError("ilInstance null");
				//	}
				//}

				//_UiObject = Activator.CreateInstance(Type.GetType(defineUIFun[uiName])) as UIObject;
				ILTypeInstance iLTypeInstance = null;

				try
				{
					_UiObject = LSharpEntryGame.ILAppDomain.Instantiate(defineUIFun[uiName]).CLRInstance as UIObject;
				}
				catch (Exception e)
				{
					DebugLoger.LogError($"遇到问题 Instantiate{e.ToString()} class name:{defineUIFun[uiName]}");

					if (e.Data.Contains("StackTrace"))
					{
						DebugLoger.Log(" StackTrace:" + e.Data["StackTrace"]);
					}

					if (e.Data.Contains("Message"))
					{
						DebugLoger.Log("Message:" + e.Data["Message"]);
					}					
				}
			}

			DebugLoger.Log($"AddUI isDefine:{uiName} createInstance end");

			if (_UiObject == null)
			{
				DebugLoger.LogError("Error As UIObject");
			}

			DebugLoger.Log($"AddUI  SetInstance");
			_UiObject.SetInstance(_UiObject);
			DebugLoger.Log($"AddUI  SetUIInstance");
			_UiObject.SetUIInstance(uiObj);
			DebugLoger.Log($"AddUI  assetsName");
			_UiObject.assetsName = uiName;
			DebugLoger.Log($"AddUI  AddToMap");
			crachUIMap.Add(_UiObject);
			DebugLoger.Log($"AddUI  SetFont");
			SetFont(uiObj);
			DebugLoger.Log($"AddUI  SetFont End");
			uiObj.transform.parent = GetUiShowLayer(_UiObject.mUiShowLayer).transform;
			uiObj.transform.localScale = Vector3.one;
			uiObj.transform.localPosition = Vector3.zero;
			uiObj.transform.localRotation = Quaternion.identity;
			DebugLoger.Log($"AddUI  GetUiShowLayer End");
			FrameWork.UIEffectManager.RegistUIEffect(_UiObject.objectInstance);
			_UiObject.GetButtonBindSound();
			DebugLoger.Log($"AddUI  GetButtonBindSound End");
			_UiObject.OnAwake();
		}
		else
		{
			Debug.LogError(uiName + " 未声明的界面信息 -> " + uiName);
		}

		return _UiObject;
	}

	/// <summary>
	/// 打开UIObj
	/// </summary>
	/// <param name="_uiObj"></param>
	public void OpenUI(string projectName, string _uiName, bool _needFont)
	{
		bool isOpen = IsOpenUI(_uiName);
		///检测是否有UI打开
		if (CheckUI(_uiName) && !isOpen)
		{
			ShowUI(GetUI(_uiName));
		}
		else if (isOpen)
		{
			//UI已经打开
			ShowUI(GetUI(_uiName));
		}
		else
		{
			//检测字体是否加载
			if (!FontManager.IsLoadFinish())
			{
				openTaskList.Add(projectName + "|" + _uiName);
				return;
			}
			else
			{
				//字体加载完成UI没缓存就加载
				LoadUI(projectName, _uiName);
			}
		}
	}

	/// <summary>
	/// 开启UIObject
	/// </summary>
	/// <param name="uiObj"></param>
	public void ShowUI(UIObject _uiObj)
	{
		DebugLoger.Log("show ui" + _uiObj.assetsName);
		crachUIMap.Remove(_uiObj);
		crachUIMap.Add(_uiObj);

		if (!_uiObj.isOpen)
		{
			_uiObj.SetRect(GetUIRootRectTransform());
			_uiObj.objectInstance.SetActive(true);
			_uiObj.isOpen = true;
			_uiObj.OnRegistEvent();
		}

		SetLayer(_uiObj);
		FrameWork.UIEffectManager.UpTopEffect(_uiObj.objectInstance);
		_uiObj.OnEnable();
	}

	/// <summary>
	/// 小窗口部件的显示隐藏
	/// </summary>
	/// <param name="_uiObject"></param>
	public static void ActiveObject(GameObject _uiObject, bool _active)
	{
		if (_uiObject == null)
		{
			return;
		}

		_uiObject.SetActive(_active);
	}

	/// <summary>
	/// 关闭Ui通过名字
	/// </summary>
	/// <param name="uiName"></param>
	public void CloseUI(string uiName, eCloseType _closeType)
	{
		if (CheckUI(uiName))
		{
			CloseUIFromCrash(uiName, GetUI(uiName), _closeType);

			for (int i = crachUIMap.Count - 1; i >= 0; --i)
			{
				if (crachUIMap[i].objectInstance != null && crachUIMap[i].objectInstance.activeSelf)
				{
					FrameWork.UIEffectManager.UpTopEffect(crachUIMap[i].objectInstance);

					return;
				}
			}
		}
	}

	/// <summary>
	/// 关闭UI 通过名字
	/// </summary>
	/// <param name="uiName"></param>
	/// <param name="uiObj"></param>
	public void CloseUIFromCrash(string uiName, UIObject _uiObj, eCloseType _closeType)
	{
		if (_uiObj == null)
		{
			DebugLoger.LogError("关闭ui 未空 _uiObj ui名 -> " + uiName);
			return;
		}
		
		int _baseLayer = GetBaseLayer(_uiObj.mUiShowLayer);
		DebugLoger.Log($"CloseUIFromCrash { _uiObj.mUiShowLayer } _baseLayer:" + _baseLayer);
		_baseLayer--;
		SetBaseLayer(_uiObj.mUiShowLayer, _baseLayer);

		_uiObj.objectInstance.SetActive(false);


		if (_uiObj.isOpen)
		{
			_uiObj.isOpen = false;
			_uiObj.OnUnRegistEvent();
			_uiObj.OnClose();
		}
		//_uiObj.OnDisable();        

		///关闭类型 决定是否立即销毁
		if (_closeType == eCloseType.None)
		{
			DiposeUiObje(_uiObj);
		}
		else
		{
			_uiObj.closeType = _closeType;

			_uiObj.closeTimeRecord = closeRecordTime;

			UpCheckQueue();
		}
	}

	/// <summary>
	/// 关闭所有UI
	/// </summary>
	public void CloseAllUI()
	{
		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			if (crachUIMap[loop].isOpen && !CloseExternUI(crachUIMap[loop].assetsName))
			{
				CloseUIFromCrash(crachUIMap[loop].assetsName, crachUIMap[loop], eCloseType.Queue);
			}
		}
	}

	/// <summary>
	/// 关闭所有UI排除
	/// </summary>
	/// <param name="uiName"></param>
	public void CloseAllUIExtern(string uiName)
	{
		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			if (crachUIMap[loop].isOpen && crachUIMap[loop].assetsName != uiName && !CloseExternUI(crachUIMap[loop].assetsName))
			{
				CloseUIFromCrash(crachUIMap[loop].assetsName, crachUIMap[loop], eCloseType.Queue);
			}
		}
	}

	/// <summary>
	/// 关闭所有UI排除
	/// </summary>
	/// <param name="uiName"></param>
	public void CloseAllUIExtern(string[] uiNames)
	{
		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			if (crachUIMap[loop].isOpen && !CloseExternUI(crachUIMap[loop].assetsName))
			{
				bool canClose = true;
				for (int i = 0; i < uiNames.Length; ++i)
				{
					if (crachUIMap[loop].assetsName == uiNames[i])
					{
						canClose = false;
						break;
					}
				}

				if (canClose)
				{
					CloseUIFromCrash(crachUIMap[loop].assetsName, crachUIMap[loop], eCloseType.Queue);
				}
			}
		}
	}

	/// <summary>
	/// 检测缓存之后完之后的卸载操作
	/// </summary>
	/// <param name="_uiObj"></param>
	public void DiposeUiObje(UIObject _uiObj)
	{
		_uiObj.OnDispose();
		_uiObj.isOpen = false;
		crachUIMap.Remove(_uiObj);

		if (_uiObj.objectInstance != null)
		{
			_uiObj.objectInstance.SetActive(false);
			FrameWork.UIEffectManager.UnRegistUIEffect(_uiObj.objectInstance);
			GameObject.Destroy(_uiObj.objectInstance);
		}

		if (_uiObj.assetInfor != null)
		{
			_uiObj.assetInfor.assetsInfor.Dispose(_uiObj.assetInfor.assetsType);
		}
	}

	/// <summary>
	/// 检测是否包含UI
	/// </summary>
	/// <param name="uiName"></param>
	/// <returns></returns>
	protected bool CheckUI(string uiName)
	{
		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			if (uiName == crachUIMap[loop].assetsName)
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// 获取UIObject
	/// </summary>
	/// <param name="uiName"></param>
	/// <returns></returns>
	public UIObject GetUI(string uiName)
	{
		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			if (uiName == crachUIMap[loop].assetsName)
			{
				return crachUIMap[loop];
			}
		}

		return null;
	}

	/// <summary>
	/// UI是否打开
	/// </summary>
	/// <param name="uiName"></param>
	/// <returns></returns>
	public bool IsOpenUI(string uiName)
	{
		UIObject uiObject = GetUI(uiName);

		if (uiObject != null)
		{
			return uiObject.isOpen;
		}
		return false;
	}



	/// <summary>
	/// 预加载UI
	/// </summary>
	/// <param name="uiName"></param>
	public void PrefabLoadUI(string projectName, string uiName)
	{
		string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(projectName, uiName, ePathType.UIPathType);
		AssetsInfor assetsInforLoad = new AssetsInfor();
		assetsInforLoad.assetsPath = _path;
		assetsInforLoad.assetsName = uiName;
		//LanCLRHotManager.CLRHotBase clrHotInstance = LanCLRHotManager.CLRHotBase.GetCLRHotBaseWithDllTypeName(LSharpEntryGame.DllFileName);
		assetsInforLoad.OnLoadFinishCall = PreafbLoadCompleted;// CLRSharp.Delegate_Binder.MakeDelegate(typeof(AssetsInfor.LoadFinishCall), clrHotInstance.clrSharpInstance, clrHotInstance.GetDllType("UIManager").GetMethod("LoadCompleted", null)) as AssetsInfor.LoadFinishCall;

		assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
		assetsInforLoad.assetsNameList.Add(new AssetsData() { assetsType = eAssetsType.GameObject, assetsName = assetsInforLoad.assetsName });
		FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
	}

	/// <summary>
	/// 加载UI资源
	/// </summary>
	/// <param name="uiName"></param>
	public void LoadUI(string projectName, string uiName)
	{
		string _path = "";

		if (loadSync)
		{
			_path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithTypeFromWWW(projectName, uiName, ePathType.UIPathType);
		}
		else
		{
			_path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(projectName, uiName, ePathType.UIPathType);
		}

		DebugLoger.Log($"加载UI:{_path} loadSync:{loadSync}");
		AssetsInfor assetsInforLoad = new AssetsInfor();
		assetsInforLoad.assetsPath = _path;
		assetsInforLoad.assetsName = uiName;
		//LanCLRHotManager.CLRHotBase clrHotInstance = LanCLRHotManager.CLRHotBase.GetCLRHotBaseWithDllTypeName(LSharpEntryGame.DllFileName);
		assetsInforLoad.OnLoadFinishCall = LoadCompleted;// CLRSharp.Delegate_Binder.MakeDelegate(typeof(AssetsInfor.LoadFinishCall), clrHotInstance.clrSharpInstance, clrHotInstance.GetDllType("UIManager").GetMethod("LoadCompleted", null)) as AssetsInfor.LoadFinishCall;

		assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
		assetsInforLoad.assetsNameList.Add(new AssetsData() { assetsType = eAssetsType.GameObject, assetsName = assetsInforLoad.assetsName });

		if (loadSync)
		{
			FrameWorkDrvice.AssetsManageInstance.AddLoadImmediate(assetsInforLoad);
		}
		else
		{
			FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
		}
	}


	/// <summary>
	/// 关闭的时候调用一次
	/// </summary>
	public void UpCheckQueue()
	{
		if (crachUIMap.Count < maxCrashCount)
		{
			return;
		}

		System.Collections.Generic.List<UIObject> _removeList = new System.Collections.Generic.List<UIObject>();

		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			if (crachUIMap.Count < maxCrashCount)
			{
				return;
			}

			UIObject _uiObject = crachUIMap[loop];

			if (_uiObject.closeType == eCloseType.Queue || _uiObject.closeType == eCloseType.TimeRecordAndQueue)
			{
				if (!_uiObject.isOpen)
				{
					_removeList.Add(_uiObject);
				}
			}
		}
		ClearnUIObjectFromCrash(_removeList);
	}


	/// <summary>
	/// 检测关闭的UI的close时间
	/// </summary>
	public void UpCheckUiTime(float _daltaTime)
	{
		System.Collections.Generic.List<UIObject> _removeList = new System.Collections.Generic.List<UIObject>();

		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			UIObject _uiObject = crachUIMap[loop];

			if (_uiObject.closeType == eCloseType.TimeRecord || _uiObject.closeType == eCloseType.TimeRecordAndQueue)
			{
				_uiObject.closeTimeRecord -= _daltaTime;

				if (_uiObject.closeTimeRecord <= 0)
				{
					_removeList.Add(_uiObject);
				}
			}
		}

		ClearnUIObjectFromCrash(_removeList);
	}


	/// <summary>
	/// 从缓存清理
	/// </summary>
	/// <param name="_uiObject"></param>
	public void ClearnUIObjectFromCrash(System.Collections.Generic.List<UIObject> _removeList)
	{
		for (short loop = 0; loop < _removeList.Count; ++loop)
		{
			UIObject _uiObject = _removeList[loop];

			DiposeUiObje(_uiObject);
		}

		_removeList.Clear();
	}

	/// <summary>
	/// 销毁字体
	/// </summary>
	public void DisposeFont()
	{
		/*
        //UnityEngine.Object.DestroyImmediate(m_unityFont);
		CYChunkDefine.gameAssetManager.UnloadAsset (eDownloadResType.T_UI);
		CYChunkDefine.gameAssetManager.UnloadAsset (eDownloadResType.T_UNITY_FONT);
         * */
	}

	/// <summary>
	/// 获取UI的Rect
	/// </summary>
	/// <returns></returns>
	public static RectTransform GetUIRootRectTransform()
	{
		if (uiRootRectTransform == null)
		{
			uiRootRectTransform = uiRoot.GetComponent<RectTransform>();
		}

		return uiRootRectTransform;
	}

	/// <summary>
	/// 获取layer 对应的节点
	/// </summary>
	/// <param name="uiShowLayer"></param>
	/// <returns></returns>
	public GameObject GetUiShowLayer(eUiShowLayer uiShowLayer)
	{
		int layerNumber = GenericityTool.EnumToInt(uiShowLayer);
		GameObject uiGameObject = null;

		if (!UiShowLayerDic.TryGetValue(uiShowLayer, out uiGameObject))
		{
			uiGameObject = new GameObject("uiShowLayer_" + layerNumber);
			RectTransform rectTransform = uiGameObject.AddComponent<RectTransform>();
			rectTransform.parent = uiRoot.transform;
			rectTransform.localScale = Vector3.one;
			rectTransform.anchoredPosition3D = new Vector3(0, 0, layerNumber * -500);
			uiGameObject.layer = uiRoot.layer;

			rectTransform.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Top, 0, 0);
			rectTransform.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Left, 0, 0);
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;

			rectTransform.SetSiblingIndex(layerNumber);
			UiShowLayerDic.Add(uiShowLayer, uiGameObject);
			uiCameraObj.transform.localPosition = rectTransform.transform.localPosition + new Vector3(0, 0, -10000);
		}

		return uiGameObject;
	}

	/// <summary>
	/// 清理UiManager
	/// </summary>
	public void UIManagerDispose()
	{
		DisposeFont();

		for (short loop = 0; loop < crachUIMap.Count; ++loop)
		{
			CloseUI(crachUIMap[loop].assetsName, eCloseType.None);
		}

		crachUIMap.Clear();
	}
}


public class FontManager
{
    public static Dictionary<string, AssetsData> fontAssetDic = new Dictionary<string, AssetsData>();

    private static int taskCount = 0;

    public static void LoadPrefabFont()
    {
        LoadFont(Rall.ConfigProject.projectFloderName, new List<string>() { "yegengyoublack","simkai" ,"default" });
    }

    public static bool IsLoadFinish()
    {
        if (taskCount == 0 && fontAssetDic.Count > 0)
        {
            return true;
        }

        return false;
    }

    public static Font GetFont(string fontName)
    {
        if (fontAssetDic.ContainsKey(fontName))
        {
            return fontAssetDic[fontName].assetsData as Font;
        }

        DebugLoger.LogError($"没有找到字体{fontName}");

        return GetFont();
    }

    public static Font GetFont()
    {
        if (fontAssetDic.Count > 0)
        {
            return fontAssetDic.ElementAt(0).Value.assetsData as Font;
        }

        return null;
    }

    public static void LoadFont(string projectName, List<string> fontNames)
    {
        for (var i = 0; i < fontNames.Count; ++i)
        {
            LoadFont(projectName, fontNames[i]);
        }
    }


    /// <summary>
    /// 预加载字体
    /// </summary>
    private static void LoadFont(string projectName,string fontName)
    {
        taskCount++;
        string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(projectName, fontName, ePathType.FontPathType);
        AssetsInfor assetsInforLoad = new AssetsInfor();
        assetsInforLoad.assetsPath = _path;
        assetsInforLoad.assetsName = fontName;
        assetsInforLoad.OnLoadFinishCall = PreafbLoadCompleted;
        assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
        assetsInforLoad.assetsNameList.Add(new AssetsData() { assetsType = eAssetsType.UnityFont, assetsName = assetsInforLoad.assetsName });

        FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="assetInfor"></param>
    /// <param name="assetsLoadType"></param>
    private static void PreafbLoadCompleted(AssetsData assetInfor, eAssetsLoadType assetsLoadType)
    {
        taskCount--;

        if (assetInfor.assetsType == eAssetsType.UnityFont)
        {
            if (assetInfor.assetsData != null)
            {
                fontAssetDic.Add(assetInfor.assetsName,assetInfor);
            }
        }
    }

}

