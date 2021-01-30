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
                DebugLoger.LogError("�Ҳ���ָ���Ƴ���Key " + _key);
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
                DebugLoger.LogError("�ظ�ע�����");

                return false;
            }

            rigibodyList.Add(_rigibody);

            return true;
        }

        /// <summary>
        /// �Ƴ�����
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
        /// �Ƿ������ǰ�ĸ���
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
        /// ���¼��Rigibody
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
                         ///�����Ļ�������
                         crashRigibodyList.Add(checkRigibody);

                         //����
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
                            ///�ҵ��˾�ͣ���ڸ�����֪ͨ
                            rigibody.OnRigibodyStay(currentRigibody);
                            crashRigibodyList.RemoveAt(loopCrash);
                            isFind = true;
                            break;
                        }
                    }

                    if(!isFind)
                    {
                        ///û���Ҷ�ָ���ĸ���
                        rigibody.OnRigibodyCrossLeave(currentRigibody);
                    }
                }

                crashRigibodyList.Clear();
            }
        }

        /// <summary>
        /// ����ƶ�����
        /// </summary>
        /// <param name="_rigibody"></param>
        /// <param name="_direction"></param>
        public static Vector3 EludeMoveDirection(Rigibody _rigibody, Vector3 _targetPos)
        {
            Rigibody nearMoveRigibody = null;
            Rigibody obsealeMoveRigibody = null;

            ///��ʼ��¼ƫ�ƽǶ�
            float angleOffset = 361.0f;
            ///��ʼ����Ŀ���ķ���
            Vector3 dir = (_targetPos-_rigibody.currentPos).normalized;

            ///��ʼ����Ŀ���ĽǶ�
            float angle = DirectionToAngle(new Vector2(dir.x, dir.z));

            bool hasRigibodyAtFoward = false;

            float forwardAt = 0.0f;

            ///�����������
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

                ///������һ������Ŀ�귽��
                Vector3 curDir = (curRigibody.currentPos - _rigibody.currentPos).normalized;
                ///������һ������Ŀ��ĽǶ�
                float curAngle = DirectionToAngle(new Vector2(curDir.x, curDir.z));
                ///�������Ƕ���ԭ���ǶȲ����
                float curAngleOffset = curAngle - angle;

                Rigibody obsealeRigibody;
                
                ///��ǰ����������ײ�Ƕȴ��ڼ�¼�ĽǶ�  ����������ƫ�ƶ���
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

                ///�ж���߻����ұ�
                float offsetAngle = nearAngle - obsealAngle;

                //����뾶����Ŀ��뾶 ת��һ���Ƕ�
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

                ///��ȡ���ƶ��Ƕ�  �Ƕ�ת����
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
        /// ����ƶ�λ�� >=0 ��ǰ�� <0 ����
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
        /// �Ƿ���Կ����ƶ�
        /// </summary>
        /// <param name="_rigibody"></param>
        /// <param name="_nearRigibody"></param>
        /// <param name="_canMoveDir">����ƶ�����</param>
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

                ///�ж���С�ڽ�ɫ�ƶ���λ�õĶ���  �����м���
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
        /// ����ת���Ƕ�
        /// </summary>
        /// <param name="_direction"></param>
        /// <returns></returns>
        public static float DirectionToAngle(Vector2 _direction)
        {
            float angle = Mathf.Atan2(_direction.x,_direction.y) * 180 / Mathf.PI;

            return angle;
        }

        /// <summary>
        /// �Ƕ�ת����
        /// </summary>
        /// <param name="_angle"></param>
        /// <returns></returns>
        public static Vector2 AngleToDirection(float _angle)
        {
            float b = _angle * Mathf.PI / 180;
            //ת��λ
            Vector2 dir = new Vector3(Mathf.Sin(b), Mathf.Cos(b));

            return dir;
        }
    }


    public class Rigibody : System.Object
    {
        /// <summary>
        /// �������
        /// </summary>
        public List<Rigibody> crossRigibodyList = new List<Rigibody>();

        /// <summary>
        /// �뾶
        /// </summary>
        public float radius;

        /// <summary>
        /// ��ǰλ��
        /// </summary>
        public Vector3 currentPos;

        /// <summary>
        /// ƫ��
        /// </summary>
        public Vector3 offset;

        /// <summary>
        /// ����Ķ���
        /// </summary>
        public object bindObject;
        /// <summary>
        /// ��ǰlayer
        /// </summary>
        public string Layer;
        /// <summary>
        /// ����ײ�Ĳ㼶
        /// </summary>
        public List<string> layers = new List<string>();
        /// <summary>
        /// ��ײ�����¼�
        /// </summary>
        public Action<Rigibody> OnEntry;
        /// <summary>
        /// ��ײ�����¼�
        /// </summary>
        public Action<Rigibody> OnStay;
        /// <summary>
        /// ��ײ�˳��¼�
        /// </summary>
        public Action<Rigibody> OnLeave;

        /// <summary>
        /// ���״̬
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
        /// �Ƿ�ӵ�и���
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
        /// ����������
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
        /// ͣ��
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
        /// ��������˳�
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