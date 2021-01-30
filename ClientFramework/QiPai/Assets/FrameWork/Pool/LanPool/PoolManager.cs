using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LanPool
{
    public class PoolManager
    {
        private static GameObject managerRoot; 

        public static readonly Dictionary<string, SpawnPool> Pools = new Dictionary<string, SpawnPool>();

        public static GameObject GetManagerRoot()
        {
            if(managerRoot==null)
            {
                managerRoot = new GameObject("LeavePoolManager");
                GameObject.DontDestroyOnLoad(managerRoot);

                managerRoot.transform.localScale = Vector3.one;

                managerRoot.transform.position = new Vector3(2000, 2000, 2000);
            }

            return managerRoot;
        }

        /// <summary>
        /// 添加prefab pool
        /// </summary>
        /// <param name="poolName">池名</param>
        /// <param name="objectRes">对象资源</param>
        /// <param name="autoClean">是否开启自动清理</param>
        /// <param name="autoTime">自动清理时间间隔S</param>
        /// <param name="cleanCount">每个清理周期自动清理数量S</param>
        /// <param name="preloadAmount">初始化数量</param>
        /// <param name="cullAbove">最终清理保留数量</param>
        public static void AddPrefabPoolToPool(string poolName, GameObject objectRes,bool autoClean,float autoTime,int cleanCount, int preloadAmount,int cullAbove,int everyCreateCount,GameObject rootObject)
        {
            SpawnPool sp;

            if(Pools.TryGetValue(poolName,out sp))
            { 
                for(int loop=0;loop<sp._perPrefabPoolOptions.Count;++loop)
                {
                    PrefabPool pp = sp._perPrefabPoolOptions[loop];

                    if(pp.prefabGo == objectRes)
                    {
                        Debug.LogError("has same prefabPool");

                        return;
                    }
                }

                ///没有找到有就创建新的PrefabPool
                PrefabPool prePool = new PrefabPool();
                sp._perPrefabPoolOptions.Add(prePool);
                prePool.everyCreateCount = everyCreateCount;
                prePool.autoClean = autoClean;
                prePool.autoTime = autoTime;
                prePool.cleanCount = cleanCount;
                prePool.preloadAmount = preloadAmount;
                prePool.cullAbove = cullAbove;
                prePool.prefabGo = objectRes;

                prePool.InistancePrefabPool(sp.transform, sp);                                
            }
            else
            {
                GameObject spawnPoolObject = new GameObject(poolName);
                spawnPoolObject.transform.parent = GetManagerRoot().transform;
                sp = spawnPoolObject.AddComponent<SpawnPool>();
                sp.poolName = poolName;

                Pools.Add(poolName, sp);

                PrefabPool prePool = new PrefabPool();
                sp._perPrefabPoolOptions.Add(prePool);
                prePool.everyCreateCount = everyCreateCount;
                prePool.autoClean = autoClean;
                prePool.autoTime = autoTime;
                prePool.cleanCount = cleanCount;
                prePool.preloadAmount = preloadAmount;
                prePool.cullAbove = cullAbove;
                prePool.prefabGo = objectRes;

                if (rootObject != null)
                {
                    prePool.InistancePrefabPool(rootObject.transform, sp);
                }
                else
                {
                    prePool.InistancePrefabPool(sp.transform, sp);
                }
            }
        }


        public static GameObject Spawn(string poolName, GameObject gameObject)
        {
            return Pools[poolName].Spawn(gameObject.transform, Vector3.zero, Quaternion.identity).gameObject;
        }

        public static GameObject Spawn(string poolName, GameObject gameObject,Vector3 pos,Quaternion quaternion)
        {
            return Pools[poolName].Spawn(gameObject.transform, Vector3.zero, quaternion).gameObject;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="gameObject"></param>
        public static void Despawn(string poolName,GameObject gameObject)
        {
            Pools[poolName].Despawn(gameObject.transform);
        }

        /// <summary>
        /// 回收全部
        /// </summary>
        public static void DespawnAll()
        {
            foreach(var kv in Pools)
            {
                kv.Value.DespawnAll();
            }
        }

        /// <summary>
        /// 清理全部
        /// </summary>
        public static void ClearAll()
        {
            foreach (var kv in Pools)
            {
                kv.Value.CleanAll();
            }
            Pools.Clear();
        }
    }
}
