//#define UseScript

using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.IO;
//using LanCLRHotManager;

/// <summary>
/// 脚本执行类型
/// </summary>
public enum ScriptType : int
{
    ILRuntime = 0,
    Dotnet,
    DotnetAppDom,
    Script    
}

/// <summary>
/// 开发模式
/// </summary>
public enum DeveloperType
{
    DeveloperRun = 0,
    PackageRun =1
}

/// <summary>
/// 脚本调用基类
/// </summary>
public class ScriptCallBase
{
    public virtual void StartRun(string dllName)
    {
        
    }

    public virtual void Update()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnGUI()
    {

    }

    public virtual void OnApplicationPause(bool isPause)
    {

    }

    public virtual void OnApplicationFocus(bool hasFocus)
    {

    }

    public virtual void OnApplicationQuit()
    {

    }

    public virtual void OnUnRelese()
    {

    }
}

/// <summary>
/// 脚本类
/// </summary>
public class ScriptCall : ScriptCallBase
{
   
    private object AppInstance;
    public override void StartRun(string dllName)
    {
        base.StartRun(dllName);

#if UseScript
        AppInstance = LSharpEntryGame.Instance;
        LSharpEntryGame.RunGame_Net(dllName, ScriptType.Script);
#endif
    }

    public override void Update()
    {
        base.Update();
#if UseScript
        LSharpEntryGame.Instance.Update();
#endif
    }

    public override void LateUpdate()
    {
        base.Update();
#if UseScript
        LSharpEntryGame.Instance.LateUpdate();
#endif
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
#if UseScript
        LSharpEntryGame.Instance.FixedUpdate();
#endif
    }

    public override void OnGUI()
    {
        base.OnGUI();
#if UseScript
        LSharpEntryGame.Instance.OnGUI();
#endif
    }

    public override void OnApplicationPause(bool isPause)
    {
        base.OnApplicationPause(isPause);
#if UseScript
        LSharpEntryGame.OnApplicationPause(isPause);
#endif
    }

    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        #if UseScript
        LSharpEntryGame.OnApplicationQuit();
        #endif
    }
}

/// <summary>
/// Net
/// </summary>
public class ScriptDotNetCall : ScriptCallBase
{
    private Assembly asm;

    private object AppInstance;

    private MethodInfo updateMethodInfo;

    private MethodInfo lateUpdateMethodInfo;

    private MethodInfo fixedUpdateMethodInfo;

    private MethodInfo guiMethodInfo;

    public override void StartRun(string dllName)
    {
        base.StartRun(dllName);

        byte[] addinStream = CherishExe.CherishExeAssetsPathManager.GetAssetsRootFileData(dllName, CherishExe.ePathType.Dll);
        byte[] symbolBuf = null;
        if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
        {
            symbolBuf = CherishExe.CherishExeAssetsPathManager.GetAssetsRootFileData(dllName, CherishExe.ePathType.Mdb);
        }

        if (GameMain.Instance.isEncryption)
        {
            addinStream = CompressEncryption.UnEncryption(addinStream);
        }

        if (symbolBuf != null)
        {
            //asm = Assembly.Load(addinStream);
            asm = Assembly.Load(addinStream, symbolBuf);
        }
        else
        {
            asm = Assembly.Load(addinStream);
        }

        MethodInfo minfo = asm.GetType("LSharpEntryGame").GetMethod("RunGame_Net");

        try
        {
            AppInstance = minfo.Invoke(null, new object[] { dllName, ScriptType.Dotnet });
        }
        catch (Exception error)
        {
            Debug.LogError("Awake -> " + error.ToString());
        }
    }

    public override void Update()
    {
        base.Update();
        if (asm != null)
        {
            if (updateMethodInfo == null)
            {
                updateMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("Update");
            }
            try
            {
                updateMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void LateUpdate()
    {
        base.Update();

        if (asm != null)
        {
            if (lateUpdateMethodInfo == null)
            {
                lateUpdateMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("LateUpdate");
            }

            try
            {
                lateUpdateMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (asm != null)
        {
            if (fixedUpdateMethodInfo == null)
            {
                fixedUpdateMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("FixedUpdate");
            }

            try
            {
                fixedUpdateMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("LateUpdate -> " + error.ToString());
            }
        }
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (asm != null)
        {
            if (guiMethodInfo == null)
            {
                guiMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("OnGUI");
            }

            try
            {
                guiMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("OnGUI -> " + error.ToString());
            }
        }
    }

    public override void OnApplicationPause(bool isPause)
    {
        base.OnApplicationPause(isPause);

        if (asm != null)
        {
            try
            {
                MethodInfo applicationQuitMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("OnApplicationPause");
                applicationQuitMethodInfo.Invoke(null,new object[] { isPause });
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        if (asm != null)
        {
            try
            {
                MethodInfo applicationQuitMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("OnApplicationQuit");
                applicationQuitMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }

        OnUnRelese();
    }


    public override void OnUnRelese()
    {
        base.OnUnRelese();
     
    }
}

/// <summary>
/// Net
/// </summary>
public class ScriptAppDomCall : ScriptCallBase
{
    public class ApplicationProxy : MarshalByRefObject
    {
        public Assembly asm;

        public Assembly Load(byte[] fileBug)
        {
            asm = Assembly.Load(fileBug);

            return asm;
        }
    }

    private AppDomain appdom;

    private Assembly asm;

    private object AppInstance;

    private MethodInfo updateMethodInfo;

    private MethodInfo lateUpdateMethodInfo;

    private MethodInfo fixedUpdateMethodInfo;

    private MethodInfo guiMethodInfo;

    public override void StartRun(string dllName)
    {
        base.StartRun(dllName);

        byte[] addinStream = CherishExe.CherishExeAssetsPathManager.GetAssetsRootFileData(dllName, CherishExe.ePathType.Dll);
        byte[] symbolBuf = null;
        if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
        {
            symbolBuf = CherishExe.CherishExeAssetsPathManager.GetAssetsRootFileData(dllName, CherishExe.ePathType.Mdb);
        }

        if (GameMain.Instance.isEncryption)
        {
            addinStream = CompressEncryption.UnEncryption(addinStream);
        }


		string appDomainName = "ConfuseChecker";
        string fulleName = Assembly.GetAssembly(typeof(ApplicationProxy)).FullName;
        appdom = AppDomain.CreateDomain(appDomainName);

        ApplicationProxy proxy = appdom.CreateInstanceAndUnwrap(fulleName, typeof(ApplicationProxy).ToString()) as ApplicationProxy;

        if (symbolBuf != null)
        {
            //asm = appdom.Load(addinStream);
            asm = Assembly.Load(addinStream, symbolBuf);
        }
        else
        {
            asm = appdom.Load(addinStream);
        }

        MethodInfo minfo = asm.GetType("LSharpEntryGame").GetMethod("RunGame_Net");

        try
        {
            AppInstance = minfo.Invoke(null, new object[] { dllName, ScriptType.Dotnet });
        }
        catch (Exception error)
        {
            Debug.LogError("Awake -> " + error.ToString());
        }
    }

    public override void Update()
    {
        base.Update();

        if (asm != null)
        {
            if (updateMethodInfo == null)
            {
                updateMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("Update");
            }
            try
            {
                updateMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        if (asm != null)
        {
            if (lateUpdateMethodInfo == null)
            {
                lateUpdateMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("LateUpdate");
            }

            try
            {
                lateUpdateMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("LateUpdate -> " + error.ToString());
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (asm != null)
        {
            if (fixedUpdateMethodInfo == null)
            {
                fixedUpdateMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("FixedUpdate");
            }

            try
            {
                fixedUpdateMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("FixedUpdate -> " + error.ToString());
            }
        }
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (asm != null)
        {
            if (guiMethodInfo == null)
            {
                guiMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("OnGUI");
            }

            try
            {
                guiMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("OnGUI -> " + error.ToString());
            }
        }
    }

    public override void OnApplicationPause(bool isPause)
    {
        base.OnApplicationPause(isPause);

        if (asm != null)
        {
            try
            {
                MethodInfo applicationQuitMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("OnApplicationPause");
                applicationQuitMethodInfo.Invoke(null, new object[] { isPause });
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        if (asm != null)
        {
            try
            {
                MethodInfo applicationQuitMethodInfo = asm.GetType("LSharpEntryGame").GetMethod("OnApplicationQuit");
                applicationQuitMethodInfo.Invoke(AppInstance, null);
            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }

        OnUnRelese();
    }


    public override void OnUnRelese()
    {
        base.OnUnRelese();

        try
        {
            AppDomain.Unload(appdom);
        }
        catch
        {
            Debug.LogError("卸载错误");
        }

    }
}

/// <summary>
/// ILRuntime
/// </summary>
public class ScriptILRuntimeCall : ScriptCallBase
{
    
    protected ILRuntime.Runtime.Enviorment.AppDomain App;

    private object AppInstance;

    ILRuntime.CLR.Method.IMethod updateMethod;

    ILRuntime.CLR.Method.IMethod lateUpdateMethod;

    ILRuntime.CLR.Method.IMethod fixedUpdateMethod;

    ILRuntime.CLR.Method.IMethod onGuiMethod;
    public override void StartRun(string dllName)
    {
        base.StartRun(dllName);
       
        byte[] dllBuf = CherishExe.CherishExeAssetsPathManager.GetAssetsRootFileData(dllName, CherishExe.ePathType.Dll);
        byte[] symbolBuf = null;

        if (GameMain.Instance.developerType == DeveloperType.DeveloperRun)
        {
            symbolBuf = CherishExe.CherishExeAssetsPathManager.GetAssetsRootFileData(dllName, CherishExe.ePathType.Pdb);
        }

        if (GameMain.Instance.isEncryption)
        {
            dllBuf = CompressEncryption.UnEncryption(dllBuf);
        }

        var fs = new System.IO.MemoryStream(dllBuf);
        //using (var fs = new System.IO.MemoryStream(dllBuf))
        {            
            App = new ILRuntime.Runtime.Enviorment.AppDomain();            

            if (symbolBuf != null)
            {
                var fs2 = new System.IO.MemoryStream(symbolBuf);
                //using (var fs2 = new System.IO.MemoryStream(symbolBuf))
                {
                    App.LoadAssembly(fs, fs2, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                }
            }
            else
            {
                App.LoadAssembly(fs);
            }
        }

        ILRuntimeRegistAdapter.RegisterAdapter(App);
        ILRuntime.CLR.Method.IMethod method = App.GetType("LSharpEntryGame").GetMethod("RunGame_ILRuntime", 3);
        AppInstance = App.Invoke(method, null, dllName, ScriptType.ILRuntime, App);
    }

    public override void Update()
    {
        base.Update();

        if(updateMethod == null)
        {
            updateMethod = App.GetType("LSharpEntryGame").GetMethod("Update", 0);
        }

        App.Invoke(updateMethod, AppInstance);
    }

    public override void LateUpdate()
    {
        base.Update();

        if (lateUpdateMethod == null)
        {
            lateUpdateMethod = App.GetType("LSharpEntryGame").GetMethod("LateUpdate", 0);
        }

        App.Invoke(lateUpdateMethod, AppInstance);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (fixedUpdateMethod == null)
        {
            fixedUpdateMethod = App.GetType("LSharpEntryGame").GetMethod("FixedUpdate", 0);
        }

        App.Invoke(fixedUpdateMethod, AppInstance);
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (onGuiMethod == null)
        {
            onGuiMethod = App.GetType("LSharpEntryGame").GetMethod("OnGUI", 0);
        }

        App.Invoke(onGuiMethod, AppInstance);
    }

    public override void OnApplicationPause(bool isPause)
    {
        base.OnApplicationPause(isPause);

        if (App != null)
        {
            try
            {
                ILRuntime.CLR.Method.IMethod method = App.GetType("LSharpEntryGame").GetMethod("OnApplicationPause", 1);
                App.Invoke(method, null, new object[] { isPause });

            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        if (App != null)
        {
            try
            {
                ILRuntime.CLR.Method.IMethod method = App.GetType("LSharpEntryGame").GetMethod("OnApplicationQuit", 0);
                App.Invoke(method, AppInstance);

            }
            catch (Exception error)
            {
                Debug.LogError("Update -> " + error.ToString());
            }
        }
    }

    public override void OnUnRelese()
    {
        base.OnUnRelese();

		AppInstance = null;

		updateMethod = null;

		lateUpdateMethod = null;

		fixedUpdateMethod = null;

		onGuiMethod = null;

		if (App != null)
        {
            try
            {
                App = null;
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }     
}



/// <summary>
/// 游戏入口
/// </summary>
public class GameMain : MonoBehaviour 
{
    /// <summary>
    /// dll解释方式
    /// </summary>
    public ScriptType scriptType = ScriptType.Script;
    /// <summary>
    /// 开发模式和发包模式
    /// </summary>
    public DeveloperType developerType = DeveloperType.DeveloperRun;

    public bool openHall;
    /// <summary>
    /// 是否加密
    /// </summary>
    public bool isEncryption;
    /// <summary>
    /// 是否开启调试
    /// </summary>
    public bool openLog;
	/// <summary>
	/// 开启限制版本
	/// </summary>
	public bool limitVersionOpen;

    private ScriptCallBase scriptCallInfo;

    public  static string dllName = "raw";

    public static GameMain Instance;

	public string recordKey = "recordKey";
	public string recordValue = "apk1.1";


	void Awake()
    {   
        Instance = this;
        StartLogic();
    }

    public void StartLogic()
    {
        if (scriptType == ScriptType.ILRuntime)
        {
            #region ILRuntime
            scriptCallInfo = new ScriptILRuntimeCall();
            scriptCallInfo.StartRun(dllName);
            #endregion ILRuntime
        }
        else if (scriptType == ScriptType.Dotnet)
        {
            #region Dotnet
            scriptCallInfo = new ScriptDotNetCall();
            scriptCallInfo.StartRun(dllName);
            #endregion Dotnet
        }
        else if (scriptType == ScriptType.DotnetAppDom)
        {
            #region Dotnet
            scriptCallInfo = new ScriptAppDomCall();
            scriptCallInfo.StartRun(dllName);
            #endregion Dotnet
        }
        else if (scriptType == ScriptType.Script)
        {
            #region Script
            scriptCallInfo = new ScriptCall();
            scriptCallInfo.StartRun("");
            #endregion Script
        }
    }

    public void ReStartLogic()
    {
        if (scriptCallInfo != null)
        {
            scriptCallInfo.OnUnRelese();
            scriptCallInfo = null;
        }

        GC.Collect();
		Invoke("StartLogic", 0.1f);
    }

    void Update()
    {
        if (scriptCallInfo != null)
        {
            scriptCallInfo.Update();
        }
    }

    void FixedUpdate()
    {
        if (scriptCallInfo != null)
        {
            scriptCallInfo.FixedUpdate();
        }
    }

    void LateUpdate()
    {
        if (scriptCallInfo != null)
        {
            scriptCallInfo.LateUpdate();
        }
    }

    void OnGUI()
    {
        if (scriptCallInfo != null)
        {
            scriptCallInfo.OnGUI();
        }
    }

    void OnApplicationPause(bool isPause)
    {
        if (scriptCallInfo != null)
        {
            scriptCallInfo.OnApplicationPause(isPause);
        }
    }

	void OnApplicationPause(string isPause)
	{
		OnApplicationPause(isPause == "false" ? false : true);
	}

	void OnApplicationFocus(bool hasFocus)
    {

    }

    void OnApplicationQuit()
    {
		if (scriptCallInfo != null)
		{
			scriptCallInfo.OnApplicationQuit();
		}
    }
}