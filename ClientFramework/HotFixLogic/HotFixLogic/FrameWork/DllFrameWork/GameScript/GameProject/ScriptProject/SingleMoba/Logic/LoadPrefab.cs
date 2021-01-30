using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{    public class PrefabWarp
    {
        /// <summary>
        /// 资源信息
        /// </summary>
        public AssetsData assetInfor;
        /// <summary>
        /// 源文件
        /// </summary>
        public GameObject source;

        /// <summary>
        /// 释放
        /// </summary>
        public void UnRelese()
        {
            assetInfor.assetsInfor.Dispose(assetInfor.assetsType);
        }
    }

    /// <summary>
    /// 预制体加载
    /// </summary>
    public class LoadPrefab
    {
        /// <summary>
        /// 记录当前需要的回调完成数量
        /// </summary>
        public static int curLoadCount;
        /// <summary>
        /// 判断Item资源是否加载完成
        /// </summary>
        public static bool isLoadFinish { get { return curLoadCount == 0; } }
        /// <summary>
        /// 预制体字典
        /// </summary>
        public static Dictionary<string, PrefabWarp> prefabMap = new Dictionary<string, PrefabWarp>();
               
        /// <summary>
        /// 获取预制体资源
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetPrefab(string name)
        {
            if (prefabMap.ContainsKey(name))
            {
                return prefabMap[name].source;
            }
            else
            {
                DebugLoger.LogError("当中不存在资源:" + name);
                return null;
            }
        }

        /// <summary>
        /// 包含主键
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ContainKey(string name)
        {
            return prefabMap.ContainsKey(name);
        }

        /// <summary>
        /// 预加载全部的实例
        /// </summary>
        public static void PrefabLoadAll()
        {
            foreach (var kv in DefineConfig.GetDatas())
            {
                PrefabLoad(kv.Value.assetName);
            }

            PrefabLoad("propskill");
            PrefabLoad("buttle");
            PrefabLoad("boom");
            PrefabLoad("effect_boom_end");
        }

        /// <summary>
        /// 释放所有的Prefab
        /// </summary>
        /// <param name="prefabNames"></param>
        public static void RelesePrefabs(List<string> prefabNames)
        {
            for (int i = 0; i < prefabNames.Count; ++i)
            {
                RelesePrefab(prefabNames[i]);
            }
        }

        /// <summary>
        /// 释放全部资源
        /// </summary>
        public static void ReleseAllPrefab()
        {
            List<string> keys = new List<string>(prefabMap.Keys);
            for (int i = 0; i < keys.Count; ++i)
            {
                RelesePrefab(keys[i]);
            }
        }

        public static void RelesePrefab(string prefabName)
        {
            if (prefabMap.ContainsKey(prefabName))
            {
                PrefabWarp prefabWarp = prefabMap[prefabName];
                prefabMap.Remove(prefabName);
                prefabWarp.UnRelese();
            }
        }

        /// <summary>
        /// Load一个单个资源
        /// </summary>
        /// <param name="uiName"></param>
        public static void PrefabLoad(string uiName)
        {
            if (ContainKey(uiName))
            {
                return;
            }

            curLoadCount++;
            string _path = FrameWorkDrvice.AssetsPathManagerInstance.GetFilePathWithType(SingleMoba.ConfigProject.projectFloderName, uiName, ePathType.Items);
            AssetsInfor assetsInforLoad = new AssetsInfor();
            assetsInforLoad.assetsPath = _path;
            assetsInforLoad.assetsName = uiName;

            assetsInforLoad.OnLoadFinishCall = LoadCompleted;

            assetsInforLoad.assetsLoadType = eAssetsLoadType.AssetsBuild;
            assetsInforLoad.assetsNameList.Add(new AssetsData() { assetsType = eAssetsType.GameObject, assetsName = assetsInforLoad.assetsName });
            FrameWorkDrvice.AssetsManageInstance.InitImmediate(assetsInforLoad);
        }


        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="assetInfor"></param>
        /// <param name="assetsLoadType"></param>
        private static void LoadCompleted(AssetsData assetInfor, eAssetsLoadType assetsLoadType)
        {
            curLoadCount--;

            if (assetInfor.assetsType == eAssetsType.GameObject)
            {
                GameObject uiObj = assetInfor.assetsData as GameObject;

                if (uiObj == null)
                {
                    DebugLoger.Log("itemObj " + assetInfor.assetsName + " 加载失败");
                    return;
                }

                PrefabWarp warp = new PrefabWarp()
                {
                    assetInfor = assetInfor,
                    source = uiObj
                };

                prefabMap.Add(assetInfor.assetsName, warp);
            }
        }

        /// <summary>
        /// 获取能量池
        /// </summary>
        /// <returns></returns>
        public static string GetPowerPoolName()
        {
            return "PowerPool";
        }

        /// <summary>
        /// 获取技能Item池
        /// </summary>
        /// <returns></returns>
        public static string GetPropSkillPoolName()
        {
            return "PropSkillPool";
        }

        /// <summary>
        /// 战斗使用特效
        /// </summary>
        /// <returns></returns>
        public static string GetFightSkillPoolName()
        {
            return "PropFightSkillPool";
        }

        public static GameObject GetPoolNode()
        {
            return null;
        }

        /// <summary>
        /// 实例化池
        /// </summary>
        public static void InitPool()
        {
            foreach (var kv in DefineConfig.GetDatas())
            {
                LanPool.PoolManager.AddPrefabPoolToPool(GetPowerPoolName(), GetPrefab(kv.Value.assetName), false, 3.0f, 3, 100, 2, 0, GetPoolNode());
            }

            LanPool.PoolManager.AddPrefabPoolToPool(GetPropSkillPoolName(), GetPrefab("propskill"), false, 3.0f, 3, 10, 2, 0, GetPoolNode());
            LanPool.PoolManager.AddPrefabPoolToPool(GetFightSkillPoolName(), GetPrefab("buttle"), false, 3.0f, 3, 10, 2, 0, GetPoolNode()); 
            LanPool.PoolManager.AddPrefabPoolToPool(GetFightSkillPoolName(), GetPrefab("boom"), false, 3.0f, 3, 10, 2, 0, GetPoolNode());
            LanPool.PoolManager.AddPrefabPoolToPool(GetFightSkillPoolName(), GetPrefab("effect_boom_end"), false, 3.0f, 3, 10, 2, 0, GetPoolNode());
        }

        /// <summary>
        /// 清理池
        /// </summary>
        public static void DesposePool()
        {
            LanPool.PoolManager.Pools[GetPropSkillPoolName()].CleanAll();
            LanPool.PoolManager.Pools[GetPowerPoolName()].CleanAll();
        }

        /// <summary>
        /// 生成能量
        /// </summary>
        /// <returns></returns>
        public static GameObject SpawnPower(string assetName)
        {
            return LanPool.PoolManager.Pools[GetPowerPoolName()].Spawn(GetPrefab(assetName).transform, Vector3.zero, Quaternion.identity).gameObject;
        }

        /// <summary>
        /// 回收能量
        /// </summary>
        /// <param name="despawnItem"></param>
        public static void DespawnPower(GameObject despawnItem)
        {
            LanPool.PoolManager.Pools[GetPowerPoolName()].Despawn(despawnItem.transform);
        }


        /// <summary>
        /// 生成能量
        /// </summary>
        /// <returns></returns>
        public static GameObject SpawnPropSkill()
        {
            return LanPool.PoolManager.Pools[GetPropSkillPoolName()].Spawn(GetPrefab("propskill").transform, Vector3.zero, Quaternion.identity).gameObject;
        }

        /// <summary>
        /// 回收能量
        /// </summary>
        /// <param name="despawnItem"></param>
        public static void DespawnItemSkill(GameObject despawnItem)
        {
            LanPool.PoolManager.Pools[GetPropSkillPoolName()].Despawn(despawnItem.transform);
        }


        /// <summary>
        /// 生成能量
        /// </summary>
        /// <returns></returns>
        public static GameObject SpawnFightSkill(string prefabName)
        {
            return LanPool.PoolManager.Pools[GetFightSkillPoolName()].Spawn(GetPrefab(prefabName).transform, Vector3.zero, Quaternion.identity).gameObject;
        }

        /// <summary>
        /// 回收能量
        /// </summary>
        /// <param name="despawnItem"></param>
        public static void DespawnFightSkill(GameObject despawnItem)
        {
            LanPool.PoolManager.Pools[GetFightSkillPoolName()].Despawn(despawnItem.transform);
        }
    }
}
