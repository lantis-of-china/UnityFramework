using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cherish
{
    public class KeyValue<K,V>
    {
        public K key;

        public V value;
    }


    public class Dictionary<K,V>
    {
        private List<KeyValue<K,V>> values;

        public void Add(K key,V value)
        {
            if (HasContent(key))
            {
                return;
            }
            KeyValue<K, V> keyValue = new KeyValue<K, V>();
            keyValue.key = key;
            keyValue.value = value;

            values.Add(keyValue);
        }

        public void Remove(K _key)
        {
            if (values == null)
            {
                DebugLoger.LogError("找不到指定移除的Key " + _key);
                return;
            }

            for (int loop = 0; loop < values.Count; ++loop)
            {
                if (object.Equals(values[loop].key, _key))
                {
                    values.RemoveAt(loop);
                }
            }
        }



        public bool HasContent(K _key)
        {
            if (values == null)
            {
                values = new List<KeyValue<K, V>>();
            }

            for (int loop = 0; loop < values.Count; ++loop)
            {
                if (object.Equals(values[loop].key,_key))
                {
                    return true;
                }
            }
            return false;
        }

        public V this[K key]
        {
            get
            {
                for (int loop = 0; loop < values.Count; ++loop)
                {
                    if (object.Equals(values[loop].key,key))
                    {
                        return values[loop].value;
                    }
                }
                return default(V);
            }
        }
    }


    public class RigibodyManager
    {
        private static List<Rigibody> rigibodyList = new List<Rigibody>();

        private static List<Rigibody> crashRigibodyList = new List<Rigibody>();
        
        public static bool RegistRigibody(Rigibody _rigibody)
        {
            if (HasContent(_rigibody))
            {
                DebugLoger.LogError("重复注册刚体");

                return false;
            }

            rigibodyList.Add(_rigibody);

            return true;
        }

        /// <summary>
        /// 移除刚体
        /// </summary>
        /// <param name="_rigibody"></param>
        public static void RemoveRigibody(Rigibody _rigibody)
        {
            for (int loop = 0; loop < rigibodyList.Count; ++loop)
            {
                Rigibody curRigibody = rigibodyList[loop];

                if (Rigibody.Equals(curRigibody, _rigibody))
                {
                    rigibodyList.RemoveAt(loop);
                    return;
                }
            }

            return;
        }

        /// <summary>
        /// 是否包含当前的刚体
        /// </summary>
        /// <param name="_rigibody"></param>
        /// <returns></returns>
        public static bool HasContent(Rigibody _rigibody)
        {
            for (int loop = 0; loop < rigibodyList.Count; ++loop)
            {
                Rigibody curRigibody = rigibodyList[loop];

                if (Rigibody.Equals(curRigibody, _rigibody))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新检测Rigibody
        /// </summary>
        public static void UpdateCheckRigibody()
        {
            for (int loopCur = 0; loopCur < rigibodyList.Count; ++loopCur)
            {
                Rigibody  rigibody = rigibodyList[loopCur];

                for (int loopCheck = 0; loopCheck < rigibodyList.Count; ++loopCheck)
                {
                     Rigibody checkRigibody = rigibodyList[loopCheck];

                     if (Rigibody.Equals(rigibody, checkRigibody))
                     {
                         continue;
                     }

                     if (Vector3.Distance(rigibody.currentPos, checkRigibody.currentPos) <= (rigibody.radius + checkRigibody.radius))
                     {
                         ///交错到的缓存起来
                         crashRigibodyList.Add(checkRigibody);

                         //交错
                         rigibody.OnRigibodyCrossEntry(checkRigibody);
                     }
                }

                for (int subIndex = rigibody.crossRigibodyList.Count - 1; subIndex >= 0; --subIndex)
                {
                    bool isFind = false;
                    Rigibody currentRigibody = rigibody.crossRigibodyList[subIndex];

                    for (int loopCrash = crashRigibodyList.Count-1; loopCrash >= 0; --loopCrash)
                    {
                        Rigibody crashRigibody = crashRigibodyList[loopCrash];

                        if (Rigibody.Equals(currentRigibody, crashRigibody))
                        {
                            ///找到了就停留在刚体内通知
                            rigibody.OnRigibodyStay(currentRigibody);
                            crashRigibodyList.RemoveAt(loopCrash);
                            isFind = true;
                            break;
                        }
                    }

                    if(!isFind)
                    {
                        ///没有找对指定的刚体
                        rigibody.OnRigibodyCrossLeave(currentRigibody);
                    }
                }

                crashRigibodyList.Clear();
            }
        }

        /// <summary>
        /// 躲避移动方向
        /// </summary>
        /// <param name="_rigibody"></param>
        /// <param name="_direction"></param>
        public static Vector3 EludeMoveDirection(Rigibody _rigibody, Vector3 _targetPos)
        {
            Rigibody nearMoveRigibody = null;
            Rigibody obsealeMoveRigibody = null;

            ///初始记录偏移角度
            float angleOffset = 361.0f;
            ///初始面向目标点的方向
            Vector3 dir = (_targetPos-_rigibody.currentPos).normalized;

            ///初始面向目标点的角度
            float angle = DirectionToAngle(new Vector2(dir.x, dir.z));

            bool hasRigibodyAtFoward = false;

            float forwardAt = 0.0f;

            ///遍历缓冲队列
            for (int loop = 0; loop < _rigibody.crossRigibodyList.Count; ++loop)
            {
                Rigibody curRigibody = _rigibody.crossRigibodyList[loop];

                Vector3 curRigToTargetDir = (_targetPos - curRigibody.currentPos).normalized;

                forwardAt = AtForward(_rigibody, curRigibody, dir);

                if (forwardAt > 0.5f)
                {
                    continue;
                }

                hasRigibodyAtFoward = true;

                ///缓存中一个面向目标方向
                Vector3 curDir = (curRigibody.currentPos - _rigibody.currentPos).normalized;
                ///缓存中一个面向目标的角度
                float curAngle = DirectionToAngle(new Vector2(curDir.x, curDir.z));
                ///缓存对象角度与原本角度差计算
                float curAngleOffset = curAngle - angle;

                Rigibody obsealeRigibody;
                
                ///当前遍历到的碰撞角度大于记录的角度  更新锁定的偏移对象
                if (IsCanNearMove(_rigibody, curRigibody, out obsealeRigibody) && Mathf.Abs(curAngleOffset) < Mathf.Abs(angleOffset))
                {
                    //if (curRigibody.currentPos)
                    nearMoveRigibody = curRigibody;

                    obsealeMoveRigibody = obsealeRigibody;

                    angleOffset = curAngleOffset;
                }
            }

            if (nearMoveRigibody != null)
            {
                float dirDistance = _rigibody.radius + nearMoveRigibody.radius;

                Vector3 obsealDir = Vector3.zero;

                float obsealAngle=0.0f;

                if (obsealeMoveRigibody != null)
                {
                    obsealDir = (obsealeMoveRigibody.currentPos - _rigibody.currentPos).normalized;

                    obsealAngle = DirectionToAngle(new Vector2(obsealDir.x, obsealDir.z));
                }
                else
                {
                    obsealDir = (_targetPos - _rigibody.currentPos).normalized;

                    obsealAngle = -DirectionToAngle(new Vector2(obsealDir.x, obsealDir.z));
                }
               
                Vector3 nearDir = (nearMoveRigibody.currentPos - _rigibody.currentPos).normalized;

                float nearAngle = DirectionToAngle(new Vector2(nearDir.x, nearDir.z));

                ///判断左边还是右边
                float offsetAngle = nearAngle - obsealAngle;

                //自身半径加上目标半径 转到一个角度
                float addAngle = DirectionToAngle(new Vector2(dirDistance, dirDistance));

                float moveAngle = 0.0f;

                //if (offsetAngle > 0)
                {
                    moveAngle = (nearAngle + addAngle * 2);                
                }
                //else
                //{
                //    moveAngle = (nearAngle - addAngle *2);        
                //}

                ///获取到移动角度  角度转方向
                Vector2 resultDir = AngleToDirection(moveAngle);

                return new Vector3(resultDir.x, 0, resultDir.y);
            }
            else
            {
                if (hasRigibodyAtFoward == false)
                {
                    return dir;
                }
                else
                {
                    return Vector3.zero;
                }                
            }
        }

        /// <summary>
        /// 检测移动位置 >=0 在前面 <0 后面
        /// </summary>
        /// <param name="_curRigibody"></param>
        /// <param name="_checkRigibody"></param>
        /// <param name="_moveFoward"></param>
        /// <returns></returns>
        public static float AtForward(Rigibody _curRigibody, Rigibody _checkRigibody,Vector3 _moveFoward)
        {
            Vector3 fowardToCheckPoint = (_curRigibody.currentPos - _checkRigibody.currentPos).normalized;

            float forwardDotValue = Vector2.Dot(_moveFoward, fowardToCheckPoint);

            return forwardDotValue;
        }

        /// <summary>
        /// 是否可以靠近移动
        /// </summary>
        /// <param name="_rigibody"></param>
        /// <param name="_nearRigibody"></param>
        /// <param name="_canMoveDir">输出移动方向</param>
        /// <returns></returns>
        public static bool IsCanNearMove(Rigibody _rigibody, Rigibody _nearRigibody, out Rigibody _obsealeRigibody)
        {
            _obsealeRigibody = null;

            int hasCantMove = 0;

            Rigibody obsealRigibody = null;

            for (int loop = 0; loop < _rigibody.crossRigibodyList.Count; ++loop)
            {
                Rigibody loopRigibody = _rigibody.crossRigibodyList[loop];

                if (Rigibody.Equals(loopRigibody, _nearRigibody))
                {
                    continue;
                }

                float distance = Vector2.Distance(new Vector2(loopRigibody.currentPos.x, loopRigibody.currentPos.z), new Vector2(_nearRigibody.currentPos.x, _nearRigibody.currentPos.z));

                distance = distance - loopRigibody.radius - _nearRigibody.radius;

                ///判断有小于角色移动的位置的对象  看看有几个
                if (distance < _rigibody.radius * 2)
                {
                    hasCantMove++;

                    obsealRigibody = loopRigibody;
                        
                    if (hasCantMove >= 2)
                    {
                        break;
                    }
                }
            }

            if (hasCantMove < 2)
            {
                _obsealeRigibody = obsealRigibody;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 方向转到角度
        /// </summary>
        /// <param name="_direction"></param>
        /// <returns></returns>
        public static float DirectionToAngle(Vector2 _direction)
        {
            float angle = Mathf.Atan2(_direction.x,_direction.y) * 180 / Mathf.PI;

            return angle;
        }

        /// <summary>
        /// 角度转方向
        /// </summary>
        /// <param name="_angle"></param>
        /// <returns></returns>
        public static Vector2 AngleToDirection(float _angle)
        {
            float b = _angle * Mathf.PI / 180;
            //转方位
            Vector2 dir = new Vector3(Mathf.Sin(b), Mathf.Cos(b));

            return dir;
        }
    }


    public class Rigibody : System.Object
    {
        /// <summary>
        /// 交叉刚体
        /// </summary>
        public List<Rigibody> crossRigibodyList = new List<Rigibody>();

        /// <summary>
        /// 半径
        /// </summary>
        public float radius;

        /// <summary>
        /// 当前位置
        /// </summary>
        public Vector3 currentPos;

        /// <summary>
        /// 偏移
        /// </summary>
        public Vector3 offset;

        /// <summary>
        /// 捆绑的对象
        /// </summary>
        public object bindObject;
        /// <summary>
        /// 当前layer
        /// </summary>
        public string Layer;
        /// <summary>
        /// 可碰撞的层级
        /// </summary>
        public List<string> layers = new List<string>();
        /// <summary>
        /// 碰撞进入事件
        /// </summary>
        public Action<Rigibody> OnEntry;
        /// <summary>
        /// 碰撞持续事件
        /// </summary>
        public Action<Rigibody> OnStay;
        /// <summary>
        /// 碰撞退出事件
        /// </summary>
        public Action<Rigibody> OnLeave;

        /// <summary>
        /// 存活状态
        /// </summary>
        protected bool isLife;


        public Rigibody()
        {

            isLife = RigibodyManager.RegistRigibody(this);
        }

        public void Dispose()
        {
            RigibodyManager.RemoveRigibody(this);

            crossRigibodyList.Clear();

            layers.Clear();

            isLife = false;
        }
        
        /// <summary>
        /// 是否拥有刚体
        /// </summary>
        /// <param name="_rigibody"></param>
        /// <returns></returns>
        public bool HasRigibody(Rigibody _rigibody)
        {
            for (int loop = 0; loop < crossRigibodyList.Count; ++loop)
            {
                 Rigibody rigibody = crossRigibodyList[loop];

                 if (Rigibody.Equals(rigibody, _rigibody))
                 {
                     return true;
                 }
            }

            return false;
        }

        /// <summary>
        /// 交叉刚体进入
        /// </summary>
        /// <param name="_entryRigibody"></param>
        public void OnRigibodyCrossEntry(Rigibody _entryRigibody)
        {
            if (!HasRigibody(_entryRigibody))
            {
                crossRigibodyList.Add(_entryRigibody);

                if(OnEntry != null)
                {
                    bool hasLayer = false;
                    for (int i = 0; i < layers.Count; ++i)
                    {
                        if (layers[i] == _entryRigibody.Layer)
                        {
                            hasLayer = true;
                            break;
                        }
                    }

                    if(hasLayer)
                    {
                        OnEntry(_entryRigibody);
                    }
                }
            }
        }

        /// <summary>
        /// 停留
        /// </summary>
        /// <param name="_entryRigibody"></param>
        public void OnRigibodyStay(Rigibody _entryRigibody)
        {
            if (OnStay != null)
            {
                bool hasLayer = false;
                for (int i = 0; i < layers.Count; ++i)
                {
                    if (layers[i] == _entryRigibody.Layer)
                    {
                        hasLayer = true;
                        break;
                    }
                }

                if (hasLayer)
                {
                    OnStay(_entryRigibody);
                }
            }
        }

        /// <summary>
        /// 交叉刚体退出
        /// </summary>
        /// <param name="_entryRigibody"></param>
        public void OnRigibodyCrossLeave(Rigibody _leaveRigibody)
        {            
            for (int loop = 0; loop < crossRigibodyList.Count; ++loop)
            {
                Rigibody rigibody = crossRigibodyList[loop];

                if (Rigibody.Equals(rigibody, _leaveRigibody))
                {
                    crossRigibodyList.RemoveAt(loop);
                    return;
                }
            }

            if (OnLeave != null)
            {
                bool hasLayer = false;
                for (int i = 0; i < layers.Count; ++i)
                {
                    if (layers[i] == _leaveRigibody.Layer)
                    {
                        hasLayer = true;
                        break;
                    }
                }

                if (hasLayer)
                {
                    OnLeave(_leaveRigibody);
                }
            }
        }
    
    }
}