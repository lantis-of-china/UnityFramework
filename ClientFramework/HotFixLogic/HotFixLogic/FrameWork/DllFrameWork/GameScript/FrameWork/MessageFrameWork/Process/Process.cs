using System;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CherishWebGLSupport;
using System.Net.Sockets;


//必须被方法处理程序继承  记录处理方法
public abstract class ProcessMessageBase
    {
        public int ID = (int)NetMessageType.None;

        public abstract void Process(Socket socket, string ip, int port, byte[] DateBuf);
    }


public class ClassFinder
{
    public static void FindClass(string SpaceName,out System.Collections.Generic.Dictionary<int, ProcessMessageBase> ProcessMap)
    {
        ProcessMap = new System.Collections.Generic.Dictionary<int, ProcessMessageBase>();

        if (LSharpEntryGame.scriptType == ScriptType.Dotnet || LSharpEntryGame.scriptType == ScriptType.Script)
        {
            System.Reflection.Assembly asb = System.Reflection.Assembly.GetExecutingAssembly();
            System.Type[] AssemblyTypes = asb.GetTypes();
            for (int indexType = 0; indexType < AssemblyTypes.Length; indexType++)
            {
                if (AssemblyTypes[indexType].Namespace == SpaceName && !AssemblyTypes[indexType].IsAbstract)
                {
                    System.Reflection.FieldInfo[] FileArray = AssemblyTypes[indexType].GetFields();

                    for (int FieldIndex = 0; FieldIndex < FileArray.Length; ++FieldIndex)
                    {
                        if (FileArray[FieldIndex].Name == "ID")
                        {
                            for (int InstanceIndex = 0; InstanceIndex < FileArray.Length; InstanceIndex++)
                            {
                                if (FileArray[InstanceIndex].Name == "_Instance")
                                {
                                    //通过程序集获取到他的返回实例对象方法  并且初始化对象
                                    System.Reflection.MethodInfo mif = AssemblyTypes[indexType].GetMethod("GetProcessType");
                                    ProcessMessageBase pb = mif.Invoke(null, null) as ProcessMessageBase;
                                    int msgId = (int)FileArray[FieldIndex].GetValue(FileArray[InstanceIndex].GetValue(null));
                                    if(ProcessMap.ContainsKey(msgId))
                                    {
                                        DebugLoger.LogError("存在相同消息号:" + msgId);
                                        continue;
                                    }
                                    ProcessMap[msgId] = pb;
                                    DebugLoger.Log("注册消息:" + AssemblyTypes[indexType].FullName + " MsgId:" + msgId);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (LSharpEntryGame.scriptType == ScriptType.ILRuntime)
        {
            #region ILRuntime
            List<string> buffer = new List<string>(LSharpEntryGame.ILAppDomain.LoadedTypes.Keys);
            for (int loop = 0; loop < buffer.Count; ++loop)
            {
                ILRuntime.CLR.TypeSystem.IType value = LSharpEntryGame.ILAppDomain.LoadedTypes[buffer[loop]];
                if (value.FullName == SpaceName + "." + value.Name && (value.BaseType != null && value.BaseType.Name == "ProcessMessageBase"))
                {
                    FieldInfo[] FileArray = value.ReflectionType.GetFields();

                    for (int FieldIndex = 0; FieldIndex < FileArray.Length; ++FieldIndex)
                    {
                        if (FileArray[FieldIndex].Name == "ID")
                        {
                            for (int InstanceIndex = 0; InstanceIndex < FileArray.Length; InstanceIndex++)
                            {
                                if (FileArray[InstanceIndex].Name == "_Instance")
                                {
                                    ILRuntime.CLR.Method.IMethod ilMethod = value.GetMethod("GetProcessType", 0);
                                    ProcessMessageBase pb = LSharpEntryGame.ILAppDomain.Invoke(ilMethod, null) as ProcessMessageBase;
                                    ProcessMap[(int)FileArray[FieldIndex].GetValue(FileArray[InstanceIndex].GetValue(null))] = pb;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion ILRuntime
        }

        DebugLoger.Log("Process Register end");
    }
}

