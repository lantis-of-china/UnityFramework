using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace LanPool
{
    public sealed class SpawnPool : MonoBehaviour
    {
        #region
        public string poolName = "";

        public bool dontDestroyOnLoad = false;

        public List<PrefabPool> _perPrefabPoolOptions = new List<PrefabPool>();
        #endregion


        /// <summary>
        /// 实例化池里面的对象池
        /// </summary>
        void Awake()
        {

            if (dontDestroyOnLoad)
                Object.DontDestroyOnLoad(this.transform);

            //有 且非空
            if (!PoolManager.Pools.ContainsKey(poolName) && !string.IsNullOrEmpty(poolName))
            {
                //没有当前SpawnPool
                for (int prefabPoolIndex = 0; prefabPoolIndex < _perPrefabPoolOptions.Count; prefabPoolIndex++)
                {
                    if (_perPrefabPoolOptions[prefabPoolIndex].prefab == null)
                    {
                        Debug.LogWarning(string.Format("Initialization Warning: Pool '{0}' " +
                                 "contains a PrefabPool with no prefab reference. Skipping.",
                                  this.poolName));
                        continue;
                    }

                    //实例化池对象
                    if (_perPrefabPoolOptions[prefabPoolIndex].InistancePrefabPool(transform, this))
                        return;
                }

                //当前spawnPool加入到
                PoolManager.Pools.Add(poolName, this);
            }
        }

        /// <summary>
        /// 产生
        /// </summary>
        /// <param name="t"></param>
        /// <param name="pos"></param>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public Transform Spawn(Transform t, Vector3 pos, Quaternion quaternion)
        {
            for (int i = 0; i < _perPrefabPoolOptions.Count; i++)
            {
                if (_perPrefabPoolOptions[i].prefab == t)
                {
                    return _perPrefabPoolOptions[i].Spawn(transform, pos, quaternion);
                }
            }

            return null;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="t"></param>
        public void Despawn(Transform t)
        {
            //if (activeInHierarchy(t.gameObject))
            {
                for (int i = 0; i < _perPrefabPoolOptions.Count; i++)
                {
                    if (_perPrefabPoolOptions[i].prefabDic.Exists(ts => ts.values == t))
                    {
                        _perPrefabPoolOptions[i].Despawn(t);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 回收所有
        /// </summary>
        public void DespawnAll()
        {
            for (int index = 0; index < _perPrefabPoolOptions.Count; index++)
            {
                _perPrefabPoolOptions[index].DespawnAll();
            }
        }

        /// <summary>
        /// 清除所有
        /// </summary>
        public void CleanAll()
        {
            for (int index = 0; index < _perPrefabPoolOptions.Count; index++)
            {
                _perPrefabPoolOptions[index].Clean();
            }

            _perPrefabPoolOptions.Clear();
        }

        /// <summary>
        /// 是不是加载在游戏面板中
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool activeInHierarchy(GameObject obj)
        {
#if (UNITY_3_5 || UNITY_3_4 || UNITY_3_3 || UNITY_3_2 || UNITY_3_1 || UNITY_3_0)
            return obj.active;
#else
            return obj.activeInHierarchy;
#endif
        }

    }
}
