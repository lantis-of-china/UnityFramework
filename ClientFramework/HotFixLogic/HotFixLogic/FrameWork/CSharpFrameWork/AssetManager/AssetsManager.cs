using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// 资源加载类型 
/// </summary>
public enum eAssetsLoadType
{
    None = 0,
    AssetsBuild = 1,
    String = 2,
    Bytes = 3,
}

/// <summary>
/// 资源类型
/// </summary>
public enum eAssetsType
{
    None = 0,
    Bytes = 1,
    String = 2,
    Object = 3,
    GameObject = 4,
    Texture = 5,
    Matrial = 6,
    Text = 7,
    UnityFont = 8,
    NguiFont = 9,
    UI = 10,
    Sence = 11,
    AnimatorController = 12,
    AnimationClip = 13,
    AudioClip = 14
}

public class AssetsData
{

    /// <summary>
    /// 描述名 作为资源的信息记录用
    /// </summary>
    public string descName;
    /// <summary>
    /// 描述字符 作为资源的信息记录
    /// </summary>
    public string descInfor;
    ///------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 资源实例名
    /// </summary>
    public string assetsName;
    /// <summary>
    /// 资源类型
    /// </summary>
    public eAssetsType assetsType;

    /// <summary>
    /// 资源数据
    /// </summary>
    public Object assetsData;

    /// <summary>
    /// 指向存在的assetsInfor结构  用于方便调用销毁方法
    /// </summary>
    public AssetsInfor assetsInfor;
}

public class AssetsInfor
{
    public static Dictionary<string, AssetsData> assetsMap = new Dictionary<string, AssetsData>();

    public List<AssetsData> assetsDataList = new List<AssetsData>();

    /// <summary>
    /// 加载完成回调委托
    /// </summary>
    /// <param name="assetInfor"></param>
    public delegate void LoadFinishCall(AssetsData assetInfor, eAssetsLoadType assetsLoadType);

    /// <summary>
    /// 开始加载的回调方法
    /// </summary>
    public delegate void EntryLoad(string assetsLoadPath, string assetsLoadName);

    /// <summary>
    /// 加载过程通知
    /// </summary>
    /// <param name="process"></param>
    public delegate void LoadingInfor(float process);

    /// <summary>
    /// 引用次数
    /// </summary>
    public int refCount;
    /// <summary>
    /// 加载完成
    /// </summary>
    public bool loadFinish;
    /// <summary>
    /// 资源路径
    /// </summary>
    public string assetsPath;
    /// <summary>
    /// 资源名
    /// </summary>
    public string assetsName;

    /// <summary>
    /// 需要加载的资源信息列表
    /// </summary>
    public System.Collections.Generic.List<AssetsData> assetsNameList = new List<AssetsData>();

    /// <summary>
    /// 开始加载资源
    /// </summary>
    public EntryLoad OnEntryLoad;

    /// <summary>
    /// 加载过程通知更新
    /// </summary>
    public LoadingInfor OnLoadingInfor;

    /// <summary>
    /// 完成加载的回调
    /// </summary>
    public LoadFinishCall OnLoadFinishCall;
    /// <summary>
    /// 存在的www资源
    /// </summary>
    public AssetBundle assetBundle;

    /// <summary>
    /// 字节信息
    /// </summary>
    public byte[] assetsBytes;

    /// <summary>
    /// 资源信息
    /// </summary>
    public string assetsString;

    /// <summary>
    /// 贴图资源
    /// </summary>
    public Texture2D assetsTexture;

    /// <summary>
    /// 资源加载文件类型
    /// </summary>
    public eAssetsLoadType assetsLoadType;

    /// <summary>
    /// 资源加载信息构造函数
    /// </summary>
    public AssetsInfor()
    {
        refCount = 0;

        loadFinish = false;
    }

    /// <summary>
    /// 使用新的Assets重置AssetsInfor
    /// </summary>
    /// <param name="newAssetsInfor"></param>
    public void SetNewAssetsInfor(AssetsInfor newAssetsInfor)
    {
        if (newAssetsInfor.OnEntryLoad != null)
        {
            OnEntryLoad = newAssetsInfor.OnEntryLoad;
        }

        if (newAssetsInfor.OnLoadingInfor != null)
        {
            OnLoadingInfor = newAssetsInfor.OnLoadingInfor;
        }

        if (newAssetsInfor.OnLoadFinishCall != null)
        {
            OnLoadFinishCall = newAssetsInfor.OnLoadFinishCall;
        }

        if (newAssetsInfor.assetsNameList != null)
        {
            assetsNameList = newAssetsInfor.assetsNameList;
        }
    }

    /// <summary>
    /// 添加资源中包含的子集资源和类型信息
    /// </summary>
    /// <param name="assetsNameAdd"></param>
    /// <param name="assetType"></param>
    public void AddAssetsName(string assetsNameAdd, eAssetsType assetType)
    {
        if (assetsNameList == null)
        {
            assetsNameList = new System.Collections.Generic.List<AssetsData>();
        }

        assetsNameList.Add(new AssetsData() { assetsName = assetsName, assetsType = assetType, assetsInfor = this });
    }

    /// <summary>
    /// 资源加载完毕
    /// </summary>
    public void LoadFinish()
    {
        try
        {
            if (assetsLoadType == eAssetsLoadType.AssetsBuild)
            {
                for (int loop = 0; loop < assetsNameList.Count; loop++)
                {
                    AssetsData assetsData = assetsNameList[loop];

                    if (assetsData.assetsName != null)
                    {
                        if (assetsData.assetsType == eAssetsType.Text)
                        {
                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(TextAsset));
                        }
                        else if (assetsData.assetsType == eAssetsType.UnityFont)
                        {
                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(Font));
                        }
                        else if (assetsData.assetsType == eAssetsType.GameObject)
                        {
                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(GameObject));
                        }
                        else if (assetsData.assetsType == eAssetsType.Texture)
                        {

                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(Texture));
                        }
                        else if (assetsData.assetsType == eAssetsType.Matrial)
                        {
                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(Material));
                        }
                        else if (assetsData.assetsType == eAssetsType.AnimationClip)
                        {

                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(AnimationClip));
                        }
                        else if (assetsData.assetsType == eAssetsType.AnimatorController)
                        {

                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(RuntimeAnimatorController));
                        }
                        else if (assetsData.assetsType == eAssetsType.AudioClip)
                        {
                            assetsData.assetsData = assetBundle.LoadAsset(assetsData.assetsName, typeof(AudioClip));
                        }
                        else
                        {
                            assetsData.assetsData = assetBundle;
                        }
                    }

                    if (assetsData.assetsData != null)
                    {
                        AddAssetsToMap(assetsData);
                    }
                    else
                    {
                        assetsData.assetsInfor = this;
                    }

                    if (OnLoadFinishCall != null)
                    {
                        OnLoadFinishCall(assetsData, eAssetsLoadType.AssetsBuild);
                    }
                }
            }
            else
            {
                AssetsData ad = new AssetsData();

                AddAssetsToMap(ad);

                if (OnLoadFinishCall != null)
                {
                    OnLoadFinishCall(ad, assetsLoadType);
                }
            }
        }
        catch (System.Exception e)
        {
            //DebugLoger.Log(e.ToString());
        }
    }

    /// <summary>
    /// 资源加入到map中
    /// </summary>
    /// <param name="ad"></param>
    public void AddAssetsToMap(AssetsData ad)
    {
        ad.assetsInfor = this;

        bool hasSameAssets = false;

        for (int loop = 0; loop < assetsDataList.Count; ++loop)
        {
            AssetsData findAd = assetsDataList[loop];

            if (findAd.assetsName == ad.assetsName)
            {
                hasSameAssets = true;

                break;
            }
        }

        if (!hasSameAssets)
        {
            assetsDataList.Add(ad);
        }

        if (!hasSameAssets)
        {
            string assetsNameInfor = ad.assetsName;

            if (string.IsNullOrEmpty(assetsNameInfor))
            {
                assetsNameInfor = this.assetsName;
            }

            assetsNameInfor = AssetsKeySpawn(assetsNameInfor, ad.assetsInfor.assetsLoadType, ad.assetsType);

            if (!assetsMap.ContainsKey(assetsNameInfor))
            {
                assetsMap.Add(assetsNameInfor, ad);
            }

            ad.assetsInfor.refCount++;
        }
    }

    public AssetsData GetAssetsNotRef(string assetsName)
    {
        AssetsData assetsD = null;

        assetsMap.TryGetValue(assetsName, out assetsD);

        return assetsD;
    }

    public AssetsData GetAssetsAddRef(string assetsName)
    {
        AssetsData assetsD = null;

        assetsMap.TryGetValue(assetsName, out assetsD);

        if (assetsD != null)
        {
            assetsD.assetsInfor.refCount++;
        }

        return assetsD;
    }

    /// <summary>
    /// 清理一个指针
    /// </summary>
    public void ReleseOneRef()
    {
        refCount--;
    }

    /// <summary>
    /// 从map移除
    /// </summary>
    /// <param name="_assetsName"></param>
    public static void RemoveAssets(string _assetsName, eAssetsLoadType _assetsLoadType, eAssetsType _assetsType)
    {
        string _assetsKey = AssetsKeySpawn(_assetsName, _assetsLoadType, _assetsType);
        if (assetsMap.ContainsKey(_assetsKey))
        {
            assetsMap.Remove(_assetsKey);
        }
    }

    /// <summary>
    /// 释放所有的东西
    /// </summary>
    public static void DisposeAll()
    {
        List<string> keys = new List<string>(assetsMap.Keys);

        for (int i = 0; i < keys.Count; ++i)
        {
            string key = keys[i];

            AssetsData ad = assetsMap[key];

            if (ad.assetsInfor != null)
            {
                if (ad.assetsInfor.assetBundle != null)
                {
                    //Resources.UnloadUnusedAssets();
                    ad.assetsInfor.assetBundle.Unload(true);
                    ad.assetsInfor.assetBundle = null;
                }

                if (ad.assetsInfor.assetsDataList != null && ad.assetsInfor.assetsDataList.Count > 0)
                {
                    AssetsInfor.RemoveAssets(ad.assetsInfor.assetsName, ad.assetsInfor.assetsLoadType, ad.assetsInfor.assetsDataList[0].assetsType);
                }
            }
        }

        assetsMap.Clear();
    }

    /// <summary>
    /// 销毁对象资源
    /// </summary>
    public void Dispose(eAssetsType _assetsType)
    {
        ReleseOneRef();

        if (refCount <= 0)
        {
            for (int i = assetsNameList.Count - 1; i >= 0; --i)
            {
                if (assetsNameList[i].assetsData != null)
                {
                    assetsNameList[i].assetsData = null;

                    //try
                    //{
                    //    Object.DestroyImmediate(assetsNameList[i].assetsData, true);
                    //}
                    //catch { }
                    //finally
                    //{
                    //}
                }
            }

            assetsNameList.Clear();

            RemoveAssets(assetsName, assetsLoadType, _assetsType);

            if (assetBundle != null)
            {
                //Resources.UnloadUnusedAssets();
                assetBundle.Unload(true);
                assetBundle = null;
            }

            if (assetsBytes != null)
            {
                assetsBytes = null;
            }

            if (assetsString != null)
            {
                assetsString = null;
            }

            if (assetsTexture != null)
            {
                assetsTexture = null;
            }

        }
    }

    /// <summary>
    /// 判断是否包含
    /// </summary>
    /// <param name="_assetsInfor"></param>
    public static AssetsInfor HasAssets(AssetsInfor _assetsInfor)
    {
        AssetsData assetsInfor;

        for (int loop = 0; loop < _assetsInfor.assetsNameList.Count; ++loop)
        {
            AssetsData ad = _assetsInfor.assetsNameList[loop];

            if (assetsMap.TryGetValue(AssetsKeySpawn(_assetsInfor.assetsName, _assetsInfor.assetsLoadType, ad.assetsType), out assetsInfor))
            {
                return assetsInfor.assetsInfor;
            }
        }

        return null;
    }

    /// <summary>
    /// 资源key生成
    /// </summary>
    /// <param name="_assetsName"></param>
    /// <param name="_assetsLoadType"></param>
    /// <param name="_assetsType"></param>
    /// <returns></returns>
    public static string AssetsKeySpawn(string _assetsName, eAssetsLoadType _assetsLoadType, eAssetsType _assetsType)
    {
        string _nameId = _assetsLoadType.ToString() + "_" + _assetsType.ToString() + " " + _assetsName;

        return _nameId;
    }

    /// <summary>
    /// 通过资源名字和类型获取资源
    /// </summary>
    /// <returns></returns>
    public AssetsData GetAssetsFromBundleByNameWithType<T>(string _assetsName, eAssetsType _assetsType) where T : UnityEngine.Object
    {
        string assetsKey = AssetsKeySpawn(_assetsName, eAssetsLoadType.AssetsBuild, _assetsType);

        AssetsData assetsData = GetAssetsAddRef(assetsKey);

        if (assetsData == null && assetBundle != null)
        {
            Object loadObject = assetBundle.LoadAsset(_assetsName);

            if (loadObject != null)
            {
                assetsData = new AssetsData()
                {
                    assetsData = loadObject,
                    assetsInfor = this,
                    assetsName = _assetsName,
                    assetsType = _assetsType
                };

                AddAssetsToMap(assetsData);
            }
        }

        return assetsData;
    }
}

/// <summary>
/// 加载队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class LoadQueue<T> : Queue where T : class, new()
{
    public T Dequeue()
    {
        return (T)base.Dequeue();
    }

    public void Enqueue(T targetObj)
    {
        base.Enqueue(targetObj);
    }
}


/// <summary>
/// 资源加载
/// </summary>
/// <typeparam name="T"></typeparam>
public class AssetsManage<T> where T : AssetsInfor, new()
{
    private LoadQueue<T> loadQueue = new LoadQueue<T>();

    private T currentLoading;

    private MonoBehaviour monoBehaviour;

    /// <summary>
    /// 当前使用的www
    /// </summary>
    private WWW wwwLoad;

    public void SetMonoBehaviour(MonoBehaviour _monoBehaviour)
    {
        //Application.backgroundLoadingPriority = ThreadPriority.High;
        monoBehaviour = _monoBehaviour;
    }

    /// <summary>
    /// 把加载资源添加到加载队列
    /// </summary>
    /// <param name="loadInfor"></param>
    public void AddLoadImmediate(T loadInfor)
    {
        if (loadInfor == null)
        {
            Debug.Log("loadInfor Null");
        }

        loadQueue.Enqueue(loadInfor);
    }

    /// <summary>
    /// 直接加载资源
    /// </summary>
    /// <param name="loadInfor"></param>
    public void InitImmediate(T loadInfor)
    {
        if (loadInfor.assetsLoadType == eAssetsLoadType.AssetsBuild)
        {
            AssetsInfor ai = AssetsInfor.HasAssets(loadInfor);

            if (ai == null)
            {
                if (GameMain.Instance.isEncryption)
                {
                    byte[] bytes = CherishUtility.GetFileDataWithPath(loadInfor.assetsPath);
                    loadInfor.assetBundle = AssetBundle.LoadFromMemory(CompressEncryption.UnEncryption(bytes));
                }
                else
                { 
                    loadInfor.assetBundle = AssetBundle.LoadFromFile(loadInfor.assetsPath);
                }
            }
            else
            {
                ai.SetNewAssetsInfor(loadInfor);
                loadInfor = ai as T;
            }
        }
        else if (loadInfor.assetsLoadType == eAssetsLoadType.String)
        {
            byte[] bytes = CherishUtility.GetFileDataWithPath(loadInfor.assetsPath);

            if (GameMain.Instance.isEncryption)
            {
                bytes = CompressEncryption.UnEncryption(bytes);
            }

            loadInfor.assetsString = System.Text.Encoding.UTF8.GetString(bytes);
        }
        else if (loadInfor.assetsLoadType == eAssetsLoadType.Bytes)
        {
        }

        if (loadInfor == null)
        {
        }
        else
        {
            loadInfor.LoadFinish();
        }
    }


    /// <summary>
    /// 加载队列中数据
    /// </summary>
    public void Update()
    {
        if (currentLoading == null || currentLoading.loadFinish)
        {
            if (currentLoading != null)
            {
                LoadAssetsFinishCall(currentLoading);
                currentLoading = null;
            }
            if (loadQueue != null && loadQueue.Count > 0)
            {
                currentLoading = null;

                currentLoading = loadQueue.Dequeue();

                if (currentLoading != null && !currentLoading.loadFinish)
                {
                    monoBehaviour.StartCoroutine(LoadAssetsProcess(currentLoading));
                }
            }
        }
        else
        {
            if (wwwLoad != null)
            {
                if (currentLoading.OnLoadingInfor != null)
                {
                    currentLoading.OnLoadingInfor(wwwLoad.progress);
                }
            }
        }
    }


    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="currentLoading"></param>
    /// <returns></returns>
    IEnumerator LoadAssetsProcess(T currentLoading)
    {
        AssetsInfor ai = AssetsInfor.HasAssets(currentLoading);
        if (ai == null)
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            wwwLoad = new WWW(currentLoading.assetsPath);
            wwwLoad.threadPriority = ThreadPriority.High;

            if (currentLoading.OnEntryLoad != null)
            {
                currentLoading.OnEntryLoad(currentLoading.assetsPath, currentLoading.assetsName);
            }

            while (!wwwLoad.isDone)
            {
                yield return null;
            }

            wwwLoad.threadPriority = ThreadPriority.Low;
            Application.backgroundLoadingPriority = ThreadPriority.Low;

            if (!string.IsNullOrEmpty(wwwLoad.error))
            {
                DebugLoger.LogError("null infor " + wwwLoad.error + " path " + currentLoading.assetsPath);
            }

            if (GameMain.Instance.isEncryption)
            {
                byte[] bytes = CompressEncryption.UnEncryption(wwwLoad.bytes);

                if (currentLoading.assetsLoadType == eAssetsLoadType.AssetsBuild)
                {
                    currentLoading.assetBundle = AssetBundle.LoadFromMemory(bytes);
                }
                else if (currentLoading.assetsLoadType == eAssetsLoadType.String)
                {
                    currentLoading.assetsString = System.Text.Encoding.UTF8.GetString(bytes);
                }
                else if (currentLoading.assetsLoadType == eAssetsLoadType.Bytes)
                {
                    currentLoading.assetsBytes = bytes;
                }
            }
            else
            {
                if (currentLoading.assetsLoadType == eAssetsLoadType.AssetsBuild)
                {
                    currentLoading.assetBundle = wwwLoad.assetBundle;
                }
                else if (currentLoading.assetsLoadType == eAssetsLoadType.String)
                {
                    currentLoading.assetsString = wwwLoad.text;
                }
                else if (currentLoading.assetsLoadType == eAssetsLoadType.Bytes)
                {
                    currentLoading.assetsBytes = wwwLoad.bytes;
                }
            }

            wwwLoad.Dispose();
            wwwLoad = null;
        }
        else
        {
            ai.SetNewAssetsInfor(currentLoading);
            currentLoading = ai as T;
        }

        LoadAssetsFinishCall(currentLoading);
    }


    /// <summary>
    /// 加载完资源回调到资源信息 由资源内部信息处理
    /// </summary>
    /// <param name="loadInfor"></param>
    private void LoadAssetsFinishCall(T loadInfor)
    {
        if (loadInfor == null)
        {
            return;
        }

        loadInfor.loadFinish = true;
        currentLoading = null;
        loadInfor.LoadFinish();
    }

    /// <summary>
    /// 清理所有资源
    /// </summary>
    public void ClearAllTask()
    {
        loadQueue.Clear();
    }
}
