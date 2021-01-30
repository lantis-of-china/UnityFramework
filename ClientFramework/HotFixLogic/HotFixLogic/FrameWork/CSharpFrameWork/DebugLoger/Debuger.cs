using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class DebugType
{
    public int type;
    public string message;
}

/// <summary>
/// 未完成任务在390
/// </summary>
public class DebugLoger
{
    static bool draw;
    static bool IsLog = true;
    public static List<DebugType> logList = new List<DebugType>();
    static int maxRecordLogCount = 100;
    static string log_infor = "";

    public static Action<List<DebugType>> ChangeCall;

    public static System.Action<string> UpLog;

    public static void RigisiterLog()
    {
        CSTools.RegistDebugLogEvent(LogCallBack);
    }

    public static void LogCallBack(string condition, string stackTrace, LogType type)
    {
        if (LogType.Exception == type)
        {
            LogError(condition + "\n\r" + stackTrace);
        }
    }

    public static void Clear()
    {
        logList.Clear();

        ChangeLog();
    }

    public static void Log(string _logInfor)
    {
        if (!IsLog) return;

        DebugType dt = new DebugType { type = 0, message = _logInfor };
        logList.Add(dt);

        if (logList.Count > maxRecordLogCount)
        {
            logList.RemoveAt(0);
        }

        Debug.Log("LantisFramework -> Log -> " + _logInfor);

        if (UpLog != null)
        {
            UpLog(_logInfor);
        }

        ChangeLog();
    }

    public static void LogWrang(string _logInfor)
    {
        if (!IsLog) return;

        DebugType dt = new DebugType { type = 1, message = _logInfor };
        logList.Add(dt);

        if (logList.Count > maxRecordLogCount)
        {
            logList.RemoveAt(0);
        }

        Debug.LogWarning("LantisFramework -> Warning -> " + _logInfor);

        if (UpLog != null)
        {
            UpLog(_logInfor);
        }

        ChangeLog();
    }

    public static void LogError(string _logInfor,Exception e = null)
    {
        if (!IsLog) return;

        DebugType dt = new DebugType { type = 2, message = _logInfor };
        logList.Add(dt);

        if (logList.Count > maxRecordLogCount)
        {
            logList.RemoveAt(0);
        }

        if (e == null)
        {
            Debug.LogError("LantisFramework -> Error -> " + _logInfor);
        }
        else
        { 
            if (e.Data.Contains("StackTrace"))
            {
                Debug.LogError("LantisFramework -> Error -> " + _logInfor + " \nStackTrace:" + e.Data["StackTrace"]);
            }
            else
            {
                Debug.LogError("LantisFramework -> Error -> " + _logInfor);
            }
        }        

        if (UpLog != null)
        {
            UpLog(_logInfor);
        }

        ChangeLog();
    }

    public static void ServerLog(string _logInfor)
    {
        if (!IsLog) return;

        DebugType dt = new DebugType { type = 0, message = _logInfor };
        logList.Add(dt);

        if (logList.Count > maxRecordLogCount)
        {
            logList.RemoveAt(0);
        }

        Debug.Log("LantisFramework -> Log -> " + _logInfor);

        if (UpLog != null)
        {
            UpLog(_logInfor);
        }

        ChangeLog();
    }

    public static void ChangeLog()
    {
        if (ChangeCall != null)
        {
            ChangeCall(logList);
        }
    }
}

public class FpsRecorder
{
    private static bool isInit = false;
    private const float showTime = 1.0f;
    private static int recordCount = 0;
    private static float recordTime = 0;
    private static int fpsValue = 0;

    public static void Init()
    {
        recordCount = 0;
        recordTime = 0;
        fpsValue = 0;
        isInit = true;
    }

    public static void Update()
    {
        if (!isInit) 
        {
            Init();
        }

        recordCount++;
        recordTime += Time.deltaTime;

        if (recordTime >= showTime)
        {
            fpsValue = Mathf.RoundToInt(recordCount / recordTime);
            recordCount = 0;
            recordTime = 0;
        }
    }

    public static int GetFps()
    {
        return fpsValue;
    }
}

