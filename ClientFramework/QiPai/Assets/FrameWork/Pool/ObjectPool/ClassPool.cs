using System.Collections;

using System.Collections.Generic;

/// <summary>
/// 类型对象池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool
{
    private static System.Collections.Generic.Dictionary<System.Type, Pool> poolInforDic
                    =
                    new System.Collections.Generic.Dictionary<System.Type, Pool>();

    private static CherishWebGLSupport.CherishThread clearThread = null;

    /// <summary>
    /// 退出清理线程
    /// </summary>
    public static void ExitClearnThread()
    {
        lock (clearThread)
        {
            try
            {
                clearThread.Abort();
            }
            finally
            {
                clearThread = null;
            }
        }
    }

    /// <summary>
    /// 运行清理线程
    /// </summary>
    public static void RunClearThread()
    {
        if (clearThread != null)
        {
            return;
        }

        clearThread = new CherishWebGLSupport.CherishThread(Clear);
        clearThread.Start();
    }


    [AOT.MonoPInvokeCallback(typeof(System.Threading.ThreadStart))]
    public static void Clear()
    {
        ///ms 毫秒
        int clearnInvate = 1000;
        while (clearThread != null)
        {
            Dictionary<System.Type, Pool> poolInforDicClone = null;
            lock (((ICollection)poolInforDic).SyncRoot)
            {
                poolInforDicClone = new Dictionary<System.Type, Pool>(poolInforDic);
            }

            foreach (var value in poolInforDicClone)
            {
                value.Value.ClearPool((float)clearnInvate / 1000);
            }

            System.Threading.Thread.Sleep(clearnInvate);
        }
    }



    /// <summary>
    /// 产生一个对象
    /// </summary>
    /// <param name="minCount"></param>
    /// <returns></returns>
    public static T Spawn<T>(uint minCount = 100, float maxClearTime = 3) where T : new()
    {
        Pool pool = GetPool(true, typeof(T), minCount, maxClearTime);

        T objInstance = pool.SpawnInstance<T>();

        return objInstance;
    }

    /// <summary>
    /// 回收一个对象
    /// </summary>
    /// <param name="temple"></param>
    public static void Despawn<T>(T temple) where T : new()
    {
        Pool pool = GetPool(false, typeof(T), 0, 0.0f);

        if (pool != null)
        {
            pool.DespawnInstance(temple);
        }
    }

    /// <summary>
    /// 获取对象池
    /// </summary>
    /// <param name="temple"></param>
    /// <param name="minCount"></param>
    /// <returns></returns>
    public static Pool GetPool(bool isSpawn, System.Type temple, uint minCount, float maxClearTime)
    {
        Pool poolList = null;
        if (poolInforDic.ContainsKey(temple))
        {
            poolList = poolInforDic[temple];
        }

        if (poolList == null && isSpawn)
        {
            poolList = new Pool();
            poolList.SetParamars(minCount, maxClearTime);

            lock (((ICollection)poolInforDic).SyncRoot)
            {
                poolInforDic.Add(temple, poolList);
            }
        }
        return poolList;
    }
}

/// <summary>
/// 对象池
/// </summary>
public class Pool
{
    private uint spawnCount = 0;
    private uint minCount = 0;
    private float maxClearTime = 0.0f;
    private float currentClearTime = 0.0f;

    public System.Collections.Generic.List<PoolInfoBase> objList = new System.Collections.Generic.List<PoolInfoBase>();

    /// <summary>
    /// 设置最小保留个数
    /// </summary>
    /// <param name="min"></param>
    public void SetParamars(uint min, float setMaxClearTime)
    {
        minCount = min;
        maxClearTime = setMaxClearTime;
    }

    /// <summary>
    /// 产生一个实例
    /// </summary>
    /// <returns></returns>
    public T SpawnInstance<T>() where T : new()
    {
        PoolInfoBase inforBase = null;
        if (spawnCount < objList.Count)
        {
            inforBase = objList.Find(m => m.isSpawn == false);
        }

        PoolInfor<T> infor = null;
        if (inforBase == null)
        {
            infor = new PoolInfor<T>();
            infor.isSpawn = true;
            infor.objInstance = new T();
            lock (((ICollection)objList).SyncRoot)
            {
                objList.Add(infor);
            }
        }
        else
        {
            infor = inforBase as PoolInfor<T>;
        }
        infor.isSpawn = true;
        spawnCount++;
        return infor.objInstance;
    }

    /// <summary>
    /// 回收一个实例
    /// </summary>
    /// <param name="temple"></param>
    public void DespawnInstance<T>(T temple) where T : new()
    {
        PoolInfoBase inforBase = objList.Find(m => m.isSpawn && (object)(m as PoolInfor<T>).objInstance == (object)temple);

        if (inforBase != null)
        {
            inforBase.isSpawn = false;
            spawnCount--;
            if (objList.Count > minCount && maxClearTime == 0.0f)
            {
                lock (((ICollection)objList).SyncRoot)
                {
                    objList.Remove(inforBase);
                }
                inforBase.Dispose();
                inforBase = null;
            }
        }
    }


    /// <summary>
    /// 清理对象池
    /// </summary>
    public void ClearPool(float sencend)
    {
        if (maxClearTime > 0.0f && objList.Count > minCount)
        {
            currentClearTime += sencend;

            if (currentClearTime > maxClearTime)
            {
                currentClearTime = 0.0f;

                for (int loop = objList.Count - 1; loop >= 0; loop--)
                {
                    PoolInfoBase poolInfor = objList[loop];

                    if (poolInfor == null || !poolInfor.isSpawn)
                    {
                        lock (((ICollection)objList).SyncRoot)
                        {
                            objList.Remove(poolInfor);
                        }
                        poolInfor.Dispose();
                        poolInfor = null;
                    }

                    if (objList.Count <= minCount)
                    {
                        return;
                    }
                }
            }
        }

    }
}


public class PoolInfoBase
{
    public bool isSpawn = false;

    /// <summary>
    /// 释放对象
    /// </summary>
    public virtual void Dispose()
    {

    }
}


public class PoolInfor<T> : PoolInfoBase
{
    public T objInstance = default(T);

    /// <summary>
    /// 释放对象
    /// </summary>
    public override void Dispose()
    {

    }
}
