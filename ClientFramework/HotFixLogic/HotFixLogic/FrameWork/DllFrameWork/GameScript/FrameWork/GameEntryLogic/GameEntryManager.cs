using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;

public class GameEntryItem
{
    /// <summary>
    /// 游戏类型
    /// </summary>
    public int gameType;
    /// <summary>
    /// 游戏名
    /// </summary>
    public string gameName;
    /// <summary>
    /// 游戏的ID
    /// </summary>
    public string gameServerId;
    /// <summary>
    /// 资源所在目录
    /// </summary>
    public string assetFloder;
    /// <summary>
    /// UI名
    /// </summary>
    public string uiName;
    /// <summary>
    /// 是否通用大厅
    /// </summary>
    public bool isGernerlRall;

    #region 创建房间
    /// <summary>
    /// 创建房间UI名
    /// </summary>
    public string uiNameCreateRoom;
    /// <summary>
    /// 亲友圈调用房间的注册方法
    /// 亲友圈名,房间默认参数设置
    /// </summary>
    public Action<string, List<byte>> callInitCreateParamarFun;
    /// <summary>
    /// 通过设置构建房间设置字符串
    /// </summary>
    public Func<List<int>, string> callGetParmarsStr;
    /// <summary>
    /// 退出大厅的时候 清理方法
    /// </summary>
    public Action callLeaveReleseFun;

    /// <summary>
    /// 通过设置构建房间设置字符串
    /// </summary>
    public Action<int,int,int> callSendEntryRoomCall;

    /// <summary>
    /// 进入游戏
    /// </summary>
    public virtual void Install()
    {

    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public virtual void Unstall()
    {

    }

    public virtual void AddToData(List<P_GameLogicRecord> recordList) { }

	public virtual void AddToOneData(P_GameLogicRecord recordItem) { }
	/// <summary>
	/// 开启总局结算面板
	/// </summary>
	/// <param name="toldGoableData"></param>
	public virtual void OpenToldResultUI(byte[] toldGoableData)	{ }
	#endregion 创建房间
}

public class GameEntryManager
{
    public static string EntrySpcaeName = "GameEntrySpace";
    public static string EntryRegistFunName = "RegistSystem";
    public Dictionary<string, GameEntryItem> gameEntryDic = new Dictionary<string,GameEntryItem>();

    /// <summary>
    /// 注册
    /// </summary>
    public void RegistFunction()
    {
        if (LSharpEntryGame.scriptType == ScriptType.Dotnet || LSharpEntryGame.scriptType == ScriptType.Script)
        {
            #region Cs
            System.Reflection.Assembly asb = System.Reflection.Assembly.GetExecutingAssembly();

            System.Type[] AssemblyTypes = asb.GetTypes();


            for (int indexType = 0; indexType < AssemblyTypes.Length; indexType++)
            {
                if (AssemblyTypes[indexType].Namespace == EntrySpcaeName && !AssemblyTypes[indexType].IsAbstract && AssemblyTypes[indexType].BaseType == typeof(GameEntryItem))
                {
                    //通过程序集获取到他的返回实例对象方法  并且初始化对象
                    System.Reflection.MethodInfo mif = AssemblyTypes[indexType].GetMethod(EntryRegistFunName);

                    mif.Invoke(null, null);
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
                if (value.FullName == EntrySpcaeName + "." + value.Name && (value.BaseType != null && value.BaseType.Name == "GameEntryItem"))
                {
					//value.ReflectionType.GetMethod(EntryRegistFunName).Invoke(null,null);

					ILRuntime.CLR.Method.IMethod ilMethod = value.GetMethod(EntryRegistFunName,0);

					LSharpEntryGame.ILAppDomain.Invoke(ilMethod, null);
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
    public void RegistFunctionCallFun(GameEntryItem item)
    {

        if (gameEntryDic.ContainsKey(item.gameServerId))
        {
            DebugLoger.LogError("重复注册游戏入口id " + item.gameServerId);
            return;
        }
        
        gameEntryDic.Add(item.gameServerId, item);
    }

    /// <summary>
    /// 获取游戏入口通过服务器ID
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns></returns>
    public GameEntryItem GetGameEntry(string serverId)
    {
        if(!gameEntryDic.ContainsKey(serverId))
        {
            DebugLoger.LogError("不存在游戏ID " + serverId);            
        }

        return gameEntryDic[serverId];
    }

    /// <summary>
    /// 获取游戏入口通过游戏类型
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns></returns>
    public GameEntryItem GetGameEntryWithGameType(int gameType)
    {
        foreach (var item in gameEntryDic)
        {
            if(item.Value.gameType == gameType)
            {
                DebugLoger.Log("tem.Value.gameType" + item.Value.gameType + " gameType" + gameType);
                return item.Value;
            }
        }

        return null;
    }
}

