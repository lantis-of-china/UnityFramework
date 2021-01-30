using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AssetsParkManager
{
    public static int currentLoadCount = 0;

    public static Dictionary<string, AssetsParkManager> parksMap = new Dictionary<string, AssetsParkManager>();

    public static void LoadParkData()
    {
        LoadParkWithName(Rall.ConfigProject.projectFloderName, Rall.ConfigProject.iconsName);
        LoadSoundParkWithName(Rall.ConfigProject.projectFloderName, Rall.ConfigProject.soundName);
    }

    public static void LoadParkWithName(string projectName, string parkName)
    {
        AssetsParkManager curParn = null;
        if (parksMap.ContainsKey(parkName))
        {
            curParn = parksMap[parkName];
        }
        else
        {
            curParn = new AssetsParkManager();
            parksMap.Add(parkName, curParn);
        }

        if (!curParn.isLoading)
        {
            curParn.LoadPark(projectName, parkName, ePathType.ThingIcon);
        }
    }

    public static void LoadSoundParkWithName(string projectName, string parkName)
    {
        AssetsParkManager curParn = null;
        if (parksMap.ContainsKey(parkName))
        {
            curParn = parksMap[parkName];
        }
        else
        {
            curParn = new AssetsParkManager();
            parksMap.Add(parkName, curParn);
        }

        if (!curParn.isLoading)
        {
            curParn.LoadPark(projectName, parkName, ePathType.Audio);
        }
    }

    /// <summary>
    /// 清理一个park
    /// </summary>
    public void Dispose()
    {
        isLoading = false;
        infoSource.Dispose(eAssetsType.None);
    }

    /// <summary>
    /// park在加载中获取已经加载
    /// </summary>
    public bool isLoading = false;
    /// <summary>
    /// 加载的资源文件
    /// </summary>
    private AssetsInfor infoSource;

    private AssetsData dataSource;

    public DelegateDiv.VoidCallFun LoadFinishCall;

    public void LoadPark(string projectName, string thingPark, ePathType ePath)
    {
        isLoading = true;
        //GetFilePathWithType
        //GetFilePathWithTypeFromWWW
        string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(projectName, thingPark, ePath);
        AssetsInfor assetsInforLoad = new AssetsInfor();
        assetsInforLoad.assetsPath = _path;
        assetsInforLoad.assetsName = thingPark;
        //LanCLRHotManager.CLRHotBase clrHotInstance = LanCLRHotManager.CLRHotBase.GetCLRHotBaseWithDllTypeName(LSharpEntryGame.DllFileName);
        assetsInforLoad.OnLoadFinishCall = LoadCompleted;// CLRSharp.Delegate_Binder.MakeDelegate(typeof(AssetsInfor.LoadFinishCall), clrHotInstance.clrSharpInstance, clrHotInstance.GetDllType("UIManager").GetMethod("LoadCompleted", null)) as AssetsInfor.LoadFinishCall;

        assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
        assetsInforLoad.assetsNameList.Add(new AssetsData()
        {
            assetsType = eAssetsType.None,
            assetsName = "",
        });
        //FrameWorkDrvice.AssetsManageInstance.AddLoadImmediate(assetsInforLoad);
        FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
    }

    public void LoadParkPrefab(string projectName, string thingPark, ePathType ePath)
    {
        isLoading = true;
        string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(projectName, thingPark, ePath);
        AssetsInfor assetsInforLoad = new AssetsInfor();
        assetsInforLoad.assetsPath = _path;
        assetsInforLoad.assetsName = thingPark;        
        assetsInforLoad.OnLoadFinishCall = LoadCompleted;

        assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
        assetsInforLoad.assetsNameList.Add(new AssetsData() { assetsType = eAssetsType.GameObject, assetsName = assetsInforLoad.assetsName });

        FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
    }

    /// <summary>
    /// 包加载完成
    /// </summary>
    /// <param name="assetInfor"></param>
    /// <param name="assetsLoadType"></param>
    private void LoadCompleted(AssetsData assetInfor, eAssetsLoadType assetsLoadType)
    {
        if (assetInfor == null || assetInfor.assetsInfor.assetBundle == null)
        {
            DebugLoger.LogError("进行大型资源加载的时候出错 " + assetInfor.assetsInfor.assetsName);
        }

        infoSource = assetInfor.assetsInfor;
        dataSource = assetInfor;
        if (LoadFinishCall != null)
        {
            LoadFinishCall();
        }

        currentLoadCount++;
    }

    /// <summary>
    /// 获取资源通过资源名
    /// </summary>
    public AssetsData GetAssetByAssetName<T>(string assetName, eAssetsType getAssetType) where T : UnityEngine.Object
    {
        if (infoSource != null)
        {
            AssetsData ad = infoSource.GetAssetsFromBundleByNameWithType<T>(assetName, getAssetType);
            return ad;
        }

        return null;
    }

    public AssetsData GetAssetInfo()
    {
        return dataSource;
    }



    public static AssetsParkManager GetAssetsParnManagerWithName(string parkName)
    {
        if (parksMap.ContainsKey(parkName))
        {
            return parksMap[parkName];
        }

        return null;
    }


    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////工具使用代码//////////////////////////////////
    /// <summary>
    /// 设置UGUI Image ThingIcon图标
    /// </summary>
    /// <param name="imageComp"></param>
    /// <param name="iconName"></param>
    public static void SetUguiImageThingIcon(string parkName, Image imageComp, string iconName)
    {
		if (imageComp == null)
		{
			DebugLoger.LogError("设置UGUI错误:SetUguiImageThingIcon Image null iconName " + iconName);

			return;
		}
		imageComp.overrideSprite = null;
		imageComp.sprite = null;

		AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);
        if (apm == null)
        {
			DebugLoger.LogError("设置UGUI错误:apm未找到:" + parkName);
			apm = GetAssetsParnManagerWithName(Rall.ConfigProject.iconsName);
			AssetsData adError = apm.GetAssetByAssetName<Texture2D>("system_error", eAssetsType.Texture);
			imageComp.overrideSprite = null;
			imageComp.sprite = null;
			Texture2D texture2dResourceError = adError.assetsData as Texture2D;
			imageComp.overrideSprite = Sprite.Create(texture2dResourceError, new Rect(0, 0, texture2dResourceError.width, texture2dResourceError.height), Vector2.zero);
			imageComp.sprite = imageComp.overrideSprite;
			return;
        }

		int loadCount = 0;
		Reload:
		AssetsData ad = apm.GetAssetByAssetName<Texture2D>(iconName, eAssetsType.Texture);
		loadCount++;

		if (ad == null)
        {
			DebugLoger.LogError("SetUguiImageIcon 失败没有加载到图标" + iconName);
			ad = apm.GetAssetByAssetName<Texture2D>("system_error", eAssetsType.Texture);
		}

		try
		{
			imageComp.overrideSprite = null;
			imageComp.sprite = null;

			Texture2D texture2dResource = ad.assetsData as Texture2D;
			imageComp.overrideSprite = Sprite.Create(texture2dResource, new Rect(0, 0, texture2dResource.width, texture2dResource.height), Vector2.zero);
			imageComp.sprite = imageComp.overrideSprite;
		}
		catch(Exception e)
		{
			DebugLoger.ServerLog(e.ToString());

			if (loadCount < 2)
			{
				goto Reload;
			}
		}
		imageComp.gameObject.SetActive(false);
		imageComp.gameObject.SetActive(true);
	}

	/// <summary>
	/// 从图包获取精灵
	/// </summary>
	/// <param name="imageComp"></param>
	/// <param name="iconName"></param>
	public static Sprite GetSprite(string parkName, string iconName)
	{
		AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);
		if (apm == null)
		{
			return null;
		}

		AssetsData ad = apm.GetAssetByAssetName<Texture2D>(iconName, eAssetsType.Texture);

		if (ad == null)
		{
			DebugLoger.LogError("SetUguiImageIcon 失败 物品图标图集中没有加载到图标" + iconName);
		}
		Texture2D texture2dResource = ad.assetsData as Texture2D;
		return Sprite.Create(texture2dResource, new Rect(0, 0, texture2dResource.width, texture2dResource.height), Vector2.zero);
	}

	/// <summary>
	/// 设置UGUI Image ThingIcon图标
	/// </summary>
	/// <param name="imageComp"></param>
	/// <param name="iconName"></param>
	public static void SetCircleImageThingIcon(string parkName, CircleImage imageComp, string iconName)
    {
        AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);
        if (apm == null)
        {
            return;
        }

        AssetsData ad = apm.GetAssetByAssetName<Texture2D>(iconName, eAssetsType.Texture);

        if (ad == null)
        {
            DebugLoger.LogError("SetUguiImageIcon 失败 物品图标图集中没有加载到图标" + iconName);
        }

		imageComp.overrideSprite = null;
		imageComp.sprite = null;

		Texture2D texture2dResource = ad.assetsData as Texture2D;
        imageComp.overrideSprite = Sprite.Create(texture2dResource, new Rect(0, 0, texture2dResource.width, texture2dResource.height), Vector2.zero);
        imageComp.sprite = imageComp.overrideSprite;
    }

    /// <summary>
    /// 设置UGUI Image ThingIcon图标
    /// </summary>
    /// <param name="imageComp"></param>
    /// <param name="iconName"></param>
    public static void SetUguiRawImageThingIcon(string parkName, RawImage imageComp, string iconName)
    {
        AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);
        if (apm == null)
        {
            return;
        }

        AssetsData ad = apm.GetAssetByAssetName<Texture2D>(iconName, eAssetsType.Texture);

        if (ad == null)
        {
            DebugLoger.LogError("SetUguiImageIcon 失败 物品图标图集中没有加载到图标" + iconName);
        }

		imageComp.texture = null;

		Texture2D texture2dResource = ad.assetsData as Texture2D;
        imageComp.texture = texture2dResource;
        //imageComp.overrideSprite = Sprite.Create(texture2dResource, new Rect(0, 0, texture2dResource.width, texture2dResource.height), Vector2.zero);
    }

    /// <summary>
    /// 释放一个Park
    /// </summary>
    /// <param name="parkName"></param>
    public static void RelesePark(string parkName)
    {
        AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);
        if (apm == null)
        {
            return;
        }
        apm.Dispose();

        RemovePark(parkName);
    }


    public static void RemovePark(string parkName)
    {
        if (parksMap.ContainsKey(parkName))
        {
            parksMap.Remove(parkName);
        }
    }



    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="parkName"></param>
    /// <param name="audioSource"></param>
    /// <param name="iconName"></param>
    public static void PlaySound(string parkName, AudioSource audioSource, string iconName)
    {
        AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);
        if (apm == null)
        {
            return;
        }


        AssetsData ad = apm.GetAssetByAssetName<AudioClip>(iconName, eAssetsType.Texture);

        if (ad == null)
        {
            DebugLoger.LogError("audio null" + iconName);
            return;
        }

        if (ad.assetsData == null)
        {
            DebugLoger.LogError("audio null" + iconName);
            return;
        }

        AudioClip audioClip = ad.assetsData as AudioClip;

        if (audioClip != null)
        {
            audioSource.volume = GoableData.UIGameSettingData.soundValue;
            audioSource.PlayOneShot(audioClip);
        }
    }

    /// <summary>
    /// 播放背景声音
    /// </summary>
    /// <param name="parkName"></param>
    /// <param name="audioSource"></param>
    /// <param name="iconName"></param>
    public static void PlayBGMSound(string parkName, AudioSource audioSource, string soundName, bool loop)
    {
        AssetsParkManager apm = GetAssetsParnManagerWithName(parkName);

        if (apm == null)
        {
            DebugLoger.LogError("没有对应的 Park " + parkName);
            return;
        }

        if (apm.infoSource == null)
        {
            DebugLoger.LogError("infoSource Null ");
            return;
        }

        AssetsData ad = apm.GetAssetByAssetName<AudioClip>(soundName, eAssetsType.None);
        if (ad == null)
        {
            DebugLoger.LogError("PlayBGMSound 失败 没有加载到对应的资源 parkName:"+ parkName + " assetName:" + soundName);
            return;
        }

        if (null == ad.assetsData)
        {
            DebugLoger.Log("Loger ad.assetsData null");
            return;
        }

        AudioClip audioClip = (AudioClip)ad.assetsData;

        if (audioClip != null)
        {
            audioSource.volume = GoableData.UIGameSettingData.backgroundSoundValue;
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            audioSource.Play();
        }

    }
}
