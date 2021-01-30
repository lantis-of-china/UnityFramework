using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WorldSystem
{
    public delegate void StringCallFun(string _string);
    public delegate void UpdataCallFun(float _deltaTime);
    public delegate void VoidCallFun();
    /// <summary>
    /// 场景名
    /// </summary>
    public string worldName;
    /// <summary>
    /// 场景节点
    /// </summary>
    public GameObject senceRoot;
    /// <summary>
    /// 是否准备好
    /// </summary>
    public bool ready;
    /// <summary>
    /// 进入场景
    /// </summary>
    public StringCallFun OnEntryWorldCall;
    /// <summary>
    /// 离开场景
    /// </summary>
    public StringCallFun OnLeaveWorldCall;
    /// <summary>
    /// 刷新方法 
    /// </summary>
    public UpdataCallFun OnFixedUpdateCall;
    /// <summary>
    /// 刷新方法 
    /// </summary>
    public UpdataCallFun OnLateUpdateCall;
}

public class WorldManager
{
    public static string WorldSpcaeName = "WorldSpace";
    /// <summary>
    /// 当前场景名
    /// </summary>
    public string currentWorldName;
    /// <summary>
    /// 当前场景
    /// </summary>
    public WorldSystem currentWorld;

    public AssetsData assetsData;

    /// <summary>
    /// 场景名
    /// </summary>
    private static System.Collections.Generic.Dictionary<string, string> defineWorldFun = new System.Collections.Generic.Dictionary<string, string>();

    /// <summary>
    /// 反射注册所有的UI类
    /// </summary>
    public void RegisterFunction()
    {
        if (LSharpEntryGame.scriptType == ScriptType.Dotnet || LSharpEntryGame.scriptType == ScriptType.Script)
        {
            #region Cs
            System.Reflection.Assembly asb = System.Reflection.Assembly.GetExecutingAssembly();

            System.Type[] AssemblyTypes = asb.GetTypes();

            for (int indexType = 0; indexType < AssemblyTypes.Length; indexType++)
            {                
                if (CSTools.GetLastNameSpaceName(AssemblyTypes[indexType].Namespace) == WorldSpcaeName && !AssemblyTypes[indexType].IsAbstract)
                {
                    //通过程序集获取到他的返回实例对象方法  并且初始化对象
                    System.Reflection.MethodInfo mif = AssemblyTypes[indexType].GetMethod("RegistSystem");

                    if (mif != null)
                    {
                        mif.Invoke(null, new object[] { AssemblyTypes[indexType].Namespace + "." + AssemblyTypes[indexType].Name });
                    }
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

                if (value.FullName == WorldSpcaeName + "." + value.Name && (value.BaseType != null && CSTools.GetLastNameSpaceName(value.BaseType.Name) == "WorldSystem"))
                {
                    ILRuntime.CLR.Method.IMethod ilMethod = value.GetMethod("RegistSystem", 1);
                    LSharpEntryGame.ILAppDomain.Invoke(ilMethod, null, value.FullName);
                }
            }
            #endregion ILRuntime
        }
    }

    /// <summary>
    /// 注册回调
    /// </summary>
    /// <param name="_assetsName"></param>
    /// <param name="_className"></param>
    public void RegistCallFun(string _assetsName, string _className)
    {
        if (defineWorldFun.ContainsKey(_assetsName))
        {
            DebugLoger.LogError("同一个场景重复注册:" + _assetsName);
            return;
        }
        DebugLoger.Log("场景注册:" + _assetsName);
        defineWorldFun.Add(_assetsName, _className);
    }

    /// <summary>
    /// uP刷新方法
    /// </summary>
    /// <param name="_deltaTime"></param>
    public void FixedUpdate()
    {
        if (currentWorld != null)
        {
            if (currentWorld.OnFixedUpdateCall != null && currentWorld.ready)
            {
                currentWorld.OnFixedUpdateCall(Time.fixedDeltaTime);
            }
        }
    }

    /// <summary>
    /// 摄像机刷新
    /// </summary>
    public void LateUpdate()
    {
        if (currentWorld != null)
        {
            if (currentWorld.OnLateUpdateCall != null && currentWorld.ready)
            {
                currentWorld.OnLateUpdateCall(Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 世界名字
    /// </summary>
    /// <param name="_worldName"></param>
    public void OpenWorld(string projectFloder, string _worldName)
    {
        if (_worldName == currentWorldName)
        {
            DebugLoger.LogWrang("加载重复的场景！");
        }

        ///开始加载场景
        LoadWorld(projectFloder, _worldName);
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="_worldName"></param>
    public void LoadWorld(string projectFloder, string _worldName)
    {
        string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(projectFloder, _worldName, ePathType.WorldPathType);
        AssetsInfor assetsInforLoad = new AssetsInfor();
        assetsInforLoad.assetsPath = _path;
        assetsInforLoad.assetsName = _worldName;
        assetsInforLoad.OnLoadFinishCall = LoadCompleted;
        assetsInforLoad.OnLoadingInfor = LoadProgress;
        assetsInforLoad.OnEntryLoad = EntryLoad;
        assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
        assetsInforLoad.assetsNameList.Add(new AssetsData() { assetsType = eAssetsType.Sence, assetsName = assetsInforLoad.assetsName });
        FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
    }

    /// <summary>
    /// 场景加载回调
    /// </summary>
    /// <param name="_assetsData"></param>
    /// <param name="_assetsLoadType"></param>
    public void LoadCompleted(AssetsData _assetsData, eAssetsLoadType _assetsLoadType)
    {
        if (currentWorld != null)
        {
            currentWorld.ready = false;

            if (currentWorld.OnLeaveWorldCall != null)
            {
                currentWorld.OnLeaveWorldCall(currentWorldName);
            }
        }
        if (currentWorldName != _assetsData.assetsName)
        {
            OnDispose(currentWorldName);
        }

        assetsData = _assetsData;

        try
        {
            Application.LoadLevel(_assetsData.assetsName);
        }
        catch (Exception e)
        {
            DebugLoger.LogError("Load Sence Failed:" + e.ToString());
        }

        currentWorldName = _assetsData.assetsName;

        DebugLoger.Log("当前加载完成场景:" + currentWorldName);

        if (LSharpEntryGame.scriptType == ScriptType.Dotnet || LSharpEntryGame.scriptType == ScriptType.Script)
        {
            currentWorld = Activator.CreateInstance(Type.GetType(defineWorldFun[currentWorldName], true, true)) as WorldSystem;
        }
        else if (LSharpEntryGame.scriptType == ScriptType.ILRuntime)
        {
            currentWorld = LSharpEntryGame.ILAppDomain.Instantiate(defineWorldFun[currentWorldName]).CLRInstance as WorldSystem;
        }

        currentWorld.ready = false;

        IEnumeratorManager.Instance.StartCoroutine(EntryWorld());
    }

    IEnumerator EntryWorld()
    {
        yield return new IEnumeratorManager.WaitForSeconds(0.1f);

        DebugLoger.Log("EntryWorld call wait end");

        if (currentWorld != null)
        {
            currentWorld.senceRoot = GameObject.Find("SenceRoot");

            if (currentWorld.OnEntryWorldCall != null)
            {
                DebugLoger.Log("currentWorld.OnEntryWorldCall Call");
                currentWorld.OnEntryWorldCall(currentWorldName);
            }
            else
            {
                DebugLoger.Log("currentWorld.OnEntryWorldCall Null");
            }

            currentWorld.ready = true;
        }
        else
        {
            DebugLoger.Log("currentWorld null");
        }
    }

    public void EntryWorldRun()
    {
        if (currentWorld != null)
        {
            currentWorld.senceRoot = GameObject.Find("SenceRoot");

            if (currentWorld.OnEntryWorldCall != null)
            {
                currentWorld.OnEntryWorldCall(currentWorldName);
            }

            currentWorld.ready = true;
        }
    }


    /// <summary>
    /// 加载进度
    /// </summary>
    public void LoadProgress(float progress)
    {
        ///通知加载
    }

    public void EntryLoad(string _path, string _name)
    { }

    /// <summary>
    /// 释放之前场景的资源
    /// </summary>
    public void OnDispose(string _worldName)
    {
        if (assetsData != null)
        {
            assetsData.assetsInfor.Dispose(assetsData.assetsType);
        }
    }
}

