using UnityEngine;
using System.Collections;
using CherishDelay;
using FsmSystem;
using System;
using System.Collections.Generic;

/// <summary>
/// 框架驱动核心
/// </summary>
public class FrameWorkDrvice
{
    private static FrameWorkDrvice instance;
    public static FrameWorkDrvice Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FrameWorkDrvice();
            }

            return instance;
        }
    }

    FrameWorkDrvice()
    {
        EventNoticesInstance.ClearAll();
    }

    private List<Action> updateEventList = new List<Action>();
    private List<Action> lateUpdateEventList = new List<Action>();
    public void AddEventToUpdate(Action ac)
    {
        updateEventList.Add(ac);
    }

    public void RemoveEventFromUpdate(Action ac)
    {
        updateEventList.Remove(ac);
    }

    public void UpEventToUpdate()
    {
        for(int i = 0;i < updateEventList.Count;++i)
        {
            updateEventList[i]();
        }
    }

    public void AddEventToLateUpdate(Action ac)
    {
        lateUpdateEventList.Add(ac);
    }

    public void RemoveEventFromLateUpdate(Action ac)
    {
        lateUpdateEventList.Remove(ac);
    }

    public void UpEventToLateUpdate()
    {
        for (int i = 0; i < lateUpdateEventList.Count; ++i)
        {
            lateUpdateEventList[i]();
        }
    }

    /// <summary>
    /// 释放游戏
    /// </summary>
    public static void UnReleseGame()
    {
        AssetsManageInstance.ClearAllTask();
        AssetsInfor.DisposeAll();
    }

	public void Update()
	{
		Server.Process.MessageDriver.UpMessage();

        AssetsManageInstance.Update();

        UiManagerInstance.Update();        

        MicrophoneManagerInstance.Update();

        GoableData.Update();

        IEnumeratorManager.Instance.UpIEnumerator();

        Cherish.RigibodyManager.UpdateCheckRigibody();

        DelayRun.Update();

        FsmSystemDiv.Update();

        UpEventToUpdate();

        FpsRecorder.Update();
    }

	public void LateUpdate()
	{        
        WorldManagerInstance.LateUpdate();

        UpEventToLateUpdate();
    }

    public void FixedUpdate()
    {
        try
        {
            WorldManagerInstance.FixedUpdate();
        }
        catch (Exception e)
        {
            DebugLoger.LogError(e.ToString(),e);
        }
    }

    public static void RegistMonoBehaviour(MonoBehaviour _monoBehaviour)
    {
        appMonoBehaviour = _monoBehaviour;
    }

    private static MonoBehaviour appMonoBehaviour;
    public static MonoBehaviour AppMonoBehaviour
    {
        get 
        {
            return appMonoBehaviour;
        }
    }

    /// <summary>
    /// 配置表路径
    /// </summary>
    private static AssetsPathManager assetsPathManagerInstance;
    public static AssetsPathManager AssetsPathManagerInstance
    {
        get
        {
            if (assetsPathManagerInstance == null)
            {
                assetsPathManagerInstance = new AssetsPathManager();
            }

            return assetsPathManagerInstance;
        }
    }

    /// <summary>
    /// 配置表管理器
    /// </summary>
    private static ConfigDataManager configDataManagerInstance;
    public static ConfigDataManager ConfigDataManagerInstance
    {
        get
        {
            if (configDataManagerInstance == null)
            {
                configDataManagerInstance = new ConfigDataManager();
            }

            return configDataManagerInstance;
        }
    }

    /// <summary>
    /// 资源管理器
    /// </summary>
    private static AssetsManage<AssetsInfor> assetsManageInstance;
    public static AssetsManage<AssetsInfor> AssetsManageInstance
    {
        get
        {
            if (assetsManageInstance == null)
            {
                assetsManageInstance = new AssetsManage<AssetsInfor>();

                AssetsManageInstance.SetMonoBehaviour(LSharpEntryGame.monoBehaviour);
            }

            return assetsManageInstance;
        }
    }

    /// <summary>
    /// 界面管理器
    /// </summary>
    private static UIManager uiManagerInstance;
    public static UIManager UiManagerInstance
    {
        get
        {
            if (uiManagerInstance == null)
            {
                uiManagerInstance = new UIManager();
            }

            return uiManagerInstance;
        }
    }

    private static WorldManager worldManagerInstance;
    public static WorldManager WorldManagerInstance
    {
        get
        {
            if (worldManagerInstance == null)
            {
                worldManagerInstance = new WorldManager();
            }

            return worldManagerInstance;
        }
    }

    ///// <summary>
    ///// 相机管理器
    ///// </summary>
    //private static CameraManager cameraManagerInstance;
    //public static CameraManager CameraManagerInstance
    //{
    //	get
    //	{
    //		if (cameraManagerInstance == null)
    //		{
    //			cameraManagerInstance = new CameraManager();
    //		}

    //		return cameraManagerInstance;
    //	}
    //}

    /// <summary>
    /// 音频管理器
    /// </summary>
    private static AudioOutManager audioOutManagerInstance;
    public static AudioOutManager AudioOutManagerInstance
    {
        get
        {
            if (audioOutManagerInstance == null)
            {
                audioOutManagerInstance = new AudioOutManager();
            }

            return audioOutManagerInstance;
        }
    }

    /// <summary>
    /// 语音管理器
    /// </summary>
    private static MicrophoneManager microphoneManagerInstance;
    public static MicrophoneManager MicrophoneManagerInstance
    {
        get
        {
            if (microphoneManagerInstance == null)
            {
                microphoneManagerInstance = new MicrophoneManager();
            }

            return microphoneManagerInstance;
        }
    }


    /// <summary>
    /// 游戏入口管理器
    /// </summary>
    private static GameEntryManager gameEntryManagerInstance;
    public static GameEntryManager GameEntryManagerInstanece
    {
        get
        {
            if (gameEntryManagerInstance == null)
            {
                gameEntryManagerInstance = new GameEntryManager();
            }

            return gameEntryManagerInstance;
        }
    }

    private static EventNotices eventNoticesInstance;
    public static EventNotices EventNoticesInstance
    {
        get
        {
            if (eventNoticesInstance == null)
            {
                eventNoticesInstance = new EventNotices();
            }

            return eventNoticesInstance;
        }
    }


    //private static CharacterManager characterManagerInstance;
    //public static CharacterManager CharacterManagerInstance
    //{
    //    get
    //    {
    //        if (characterManagerInstance == null)
    //        {
    //            characterManagerInstance = new CharacterManager();
    //        }

    //        return characterManagerInstance;
    //    }
    //}

    //private static NonPlayerCharacterManager nonPlayerCharacterManager;
    //public static NonPlayerCharacterManager NonPlayerCharacterInstance
    //{
    //    get
    //    {
    //        if (nonPlayerCharacterManager == null)
    //        {
    //            nonPlayerCharacterManager = new NonPlayerCharacterManager();
    //        }

    //        return nonPlayerCharacterManager;
    //    }
    //}



    //private static HUDManager hudManagerInstance;
    //public static HUDManager HudManagerInstance
    //{
    //    get
    //    {
    //        if (hudManagerInstance == null)
    //        {
    //            hudManagerInstance = new HUDManager();
    //        }

    //        return hudManagerInstance;
    //    }
    //}
}
