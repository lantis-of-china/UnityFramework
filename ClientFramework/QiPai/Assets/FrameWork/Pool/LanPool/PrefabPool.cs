using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LanPool
{
    [System.Serializable]
    public class PrefabPool
    {
        #region
        /// <summary>
        /// 要生成的物体变换
        /// </summary>
        public Transform prefab;

        /// <summary>
        /// 要生成的物体
        /// </summary>
        internal GameObject prefabGo;
        /// <summary>
        /// 初始化的数量
        /// </summary>
        public int preloadAmount = 1;

        /// <summary>
        /// 最终保留
        /// </summary>
        public int cullAbove = 3;

        /// <summary>
        /// 自动清理时间间隔以S为单位
        /// </summary>
        public float autoTime = 5.0f;

        /// <summary>
        /// 是否自动清理
        /// </summary>
        public bool autoClean = true;

        /// <summary>
        /// 每次清理的数量
        /// </summary>
        public int cleanCount = 5;

        /// <summary>
        /// 当前池中数量
        /// </summary>
        public int spawntCount = 0;

        /// <summary>
        /// 当前池中激活的数量
        /// </summary>
        public int despawnCount = 0;

        /// <summary>
        /// 每帧初始化最大个数
        /// </summary>
        public int everyCreateCount;

        /// <summary>
        /// 激活的实例
        /// </summary>
        public List<StateValues> prefabDic = new List<StateValues>();

        private SpawnPool spawnPoolRoot;
        private int audoCleanCount = 0;
        #endregion


        /// <summary>
        /// 开辟对象空间
        /// </summary>
        /// <param name="root"></param>
        public bool InistancePrefabPool(Transform root, SpawnPool spawnPool)
        {
            this.spawnPoolRoot = spawnPool;

            if (prefab != null)
            {
                for (int i = 0; i < spawnPool._perPrefabPoolOptions.Count; i++)
                {
                    if (spawnPool._perPrefabPoolOptions[i].prefab == prefab)
                    {
                        return false;
                    }
                }

                this.prefabGo = this.prefab.gameObject;
            }
            else if (this.prefabGo != null)
            {
                this.prefab = this.prefabGo.transform;
            }


            if (autoClean)
            {
                spawnPool.StartCoroutine(AutoClean());
            }


            spawnPool.StartCoroutine(InistancePrefabPoolCount(root));
            return true;

        }

        IEnumerator InistancePrefabPoolCount(Transform root)
        {
            if (everyCreateCount == 0)
            {
                //初始化容器
                for (; prefabDic.Count < preloadAmount; )
                {
                    GameObject go = Object.Instantiate(this.prefabGo, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    go.name = go.name.Replace("(Clone)", despawnCount.ToString());
                    go.transform.parent = root;

                    SetActive(go, false);

                    //加入到容器
                    prefabDic.Add(new StateValues() { state = false, values = go.transform });

                    despawnCount++;
                }
            }
            else
            {
                int curTimes = 1;
                while (prefabDic.Count < preloadAmount)
                {
                    //初始化容器
                    for (; prefabDic.Count < curTimes * everyCreateCount; )
                    {
                        GameObject go = Object.Instantiate(this.prefabGo, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        go.name = go.name.Replace("(Clone)", despawnCount.ToString());
                        go.transform.parent = root;

                        SetActive(go, false);

                        //加入到容器
                        prefabDic.Add(new StateValues() { state = false, values = go.transform });

                        despawnCount++;
                    }

                    curTimes++;
                    yield return null;
                }
            }

        }





        /// <summary>
        /// 自动清理
        /// </summary>
        /// <returns></returns>
        IEnumerator AutoClean()
        {
            audoCleanCount++;

            while (autoClean)
            {
                yield return new WaitForSeconds(autoTime);

                for (int cleanIndex = 0; cleanIndex < cleanCount; cleanIndex++)
                {
                    //总数大于就清理
                    if ((despawnCount + spawntCount) > cullAbove)
                    {
                        //如果处于Despawn状态 就清理掉
                        if (!prefabDic[cleanIndex].state)
                        {
                            Object.Destroy(prefabDic[cleanIndex].values.gameObject);
                            prefabDic.RemoveAt(cleanIndex);
                            despawnCount--;
                        }

                    }
                }


                //如果计时 就是auto大于1  就结束
                if (audoCleanCount > 1)
                {
                    break;
                }
            }

            audoCleanCount--;

        }


        /// <summary>
        /// spaw 产生一个
        /// </summary>
        /// <param name="root">父节点</param>
        /// <param name="pos">指定位置</param>
        /// <param name="quaternion">指定旋转</param>
        /// <returns></returns>
        public Transform Spawn(Transform root, Vector3 pos, Quaternion quaternion)
        {
            //var q = from p in prefabDic where p.state == false select p;

            StateValues stateValue = prefabDic.Find(st => st.state == false);
            if (stateValue != null)
            {
                despawnCount--;
                spawntCount++;
                stateValue.state = true;
                return SetActive(stateValue.values.gameObject, true);
            }
            else
            {
                GameObject gameObject = Object.Instantiate(prefab.gameObject, pos, quaternion) as GameObject;

                gameObject.name = gameObject.name.Replace("(Clone)", (spawntCount + despawnCount).ToString());

                gameObject.transform.parent = root;

                prefabDic.Add(new StateValues() { state = true, values = gameObject.transform });
                spawntCount++;
                return gameObject.transform;
            }

        }

        /// <summary>
        /// 回收一个
        /// </summary>
        /// <param name="t">回收的变换</param>
        public void Despawn(Transform t)
        {
            //var q = from p in prefabDic where p.values == t select p;
            StateValues stateValue = prefabDic.Find(st => st.values == t);

            if (stateValue.state)
            {
                SetActive(stateValue.values.gameObject, false);
                stateValue.values.gameObject.transform.SetParent(spawnPoolRoot.transform);
                despawnCount++;
                spawntCount--;
                stateValue.state = false;
            }
        }

        /// <summary>
        /// 回收所有的
        /// </summary>
        public void DespawnAll()
        {
            for (int index = 0; index < prefabDic.Count; index++)
            {
                if (prefabDic[index].state)
                {
                    SetActive(prefabDic[index].values.gameObject, false);
                    prefabDic[index].values.gameObject.transform.SetParent(spawnPoolRoot.transform);
                    despawnCount++;
                    spawntCount--;
                    prefabDic[index].state = false;
                }
            }
        }

        /// <summary>
        /// 清空这个容器 并删除全部的物体
        /// </summary>
        public void Clean()
        {
            for (int index = 0; index < prefabDic.Count; index++)
            {
                Object.Destroy(prefabDic[index].values.gameObject);
                despawnCount--;
            }
            autoClean = false;
            //清空容器
            prefabDic.Clear();

            spawnPoolRoot._perPrefabPoolOptions.Remove(this);
        }



        /// <summary>
        /// 激活或隐藏
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Transform SetActive(GameObject obj, bool state)
        {
//#if (UNITY_3_5 || UNITY_3_4 || UNITY_3_3 || UNITY_3_2 || UNITY_3_1 || UNITY_3_0)
//            obj.SetActiveRecursively(state);
//#else
//            obj.SetActive(state);
//#endif
            obj.SetActive(state);
            return obj.transform;
        }

    }


    /// <summary>
    /// 状态和值得组合
    /// </summary>
    public class StateValues
    {
        public bool state;
        public Transform values;
    }

}
