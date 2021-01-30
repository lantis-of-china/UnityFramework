using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;


public class UtilityTool 
{
    /// <summary>
    /// 点击事件到Trigger
    /// </summary>
    /// <param name="eventTrigger"></param>
    /// <param name="eventTiggerType"></param>
    /// <param name="callFun"></param>
    public static void AddEventTriggerEvent(EventTrigger eventTrigger, EventTriggerType eventTiggerType, UnityAction<BaseEventData> callFun)
    {
        EventTrigger.Entry eventEntry = null;

        for (int loop = 0; loop < eventTrigger.triggers.Count; ++loop)
        {
            EventTrigger.Entry eventE = eventTrigger.triggers[loop];

            if (eventE.eventID == eventTiggerType)
            {
                eventEntry = eventE;

                break;
            }
        }

        if (eventEntry == null)
        {
            eventEntry = new EventTrigger.Entry();

            eventEntry.eventID = eventTiggerType;

            eventEntry.callback = new EventTrigger.TriggerEvent();

            eventTrigger.triggers.Add(eventEntry);
        }

        UnityEngine.Events.UnityAction<BaseEventData> callback = callFun;// new UnityEngine.Events.UnityAction<BaseEventData>(callFun);

        eventEntry.callback.AddListener(callback);
    }

    /// <summary>
    /// 技能伤害输出计算 通过技能等级
    /// </summary>
    /// <param name="_baseValue"></param>
    /// <param name="_levelAddValue"></param>
    /// <param name="_level"></param>
    /// <returns></returns>
    public static int SkillDamageWithLevel(int _baseValue,int _levelAddValue,short _level)
    {
        return _baseValue + _levelAddValue * (_level - 1);
    }

    /// <summary>
    /// 通过基本值和等级加成值通过等级计算最终值
    /// </summary>
    /// <param name="_baseValue"></param>
    /// <param name="_levelAddValue"></param>
    /// <param name="_level"></param>
    /// <returns></returns>
    public static int ValueFromBaseAndAddValueWithLevel(int _baseValue, int _levelAddValue, short _level)
    {
        return _baseValue + _levelAddValue * (_level - 1);
    }

    /// <summary>
    /// 技能蓝耗计算 通过技能等级
    /// </summary>
    /// <param name="_baseValue"></param>
    /// <param name="_levelAddValue"></param>
    /// <param name="_level"></param>
    /// <returns></returns>
    public static int SkillUseMpWithLevel(int _baseValue, int _levelAddValue, short _level)
    {
        return _baseValue + _levelAddValue * (_level - 1);
    }

    /// <summary>
    /// 获取两点距离 类似勾股定理
    /// </summary>
    /// <returns></returns>
    public static float GetDistance(Vector3 pos1,Vector3 pos2)
    {
        return Mathf.Sqrt(Mathf.Abs(Mathf.Pow((pos1.x - pos2.x), 2)) + Mathf.Abs(Mathf.Pow((pos1.y - pos2.y), 2)) + Mathf.Abs(Mathf.Pow((pos1.z - pos2.z), 2)));
    }

    /// <summary>
    /// 获取方向
    /// </summary>
    /// <param name="_currentPos"></param>
    /// <param name="_targetPos"></param>
    /// <returns></returns>
    public static Vector3 GetDirection(Vector3 _currentPos,Vector3 _targetPos)
    {
        return (_targetPos - _currentPos).normalized;
    }

    /// <summary>
    /// 角度转方向
    /// </summary>
    /// <param name="_angle"></param>
    /// <returns></returns>
    public static Vector3 AngleToDirection(float _angle)
    {
        float cirlB = _angle * Mathf.PI / 180;
        Vector3 dir = new Vector3(Mathf.Sin(cirlB), 0, Mathf.Cos(cirlB));
        return (dir).normalized;
    }

    public static Quaternion DirectionToAngle(Vector3 direction)
    {
        Quaternion q4 = Quaternion.LookRotation(direction);
        return q4;
    }

    /// <summary>
    /// 判定目标在方向的右边
    /// 叉乘：求得垂直两条向量的一条向量 也就是法向量
    /// </summary>
    /// <returns></returns>
    public static bool TargetIsOnRight(Vector3 up, Vector3 target)
    {        
        Vector3 c = Vector3.Cross(up, target);

        if (c.y > 0)
        {
            return true;
        }
        else if (c.y < 0)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 判定目标在方向的左边
    /// 叉乘：求得垂直两条向量的一条向量
    /// </summary>
    /// <returns></returns>
    public static bool TargetIsOnLeft(Vector3 up, Vector3 target)
    {
        Vector3 c = Vector3.Cross(up, target);

        if (c.y < 0)
        {
            return true;
        }
        else if (c.y > 0)
        {
            return false;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 获取向量夹角
    /// 点乘：a · b = |a||b|Cos(θ)  θ[0,180]
    /// 通过向量点乘计算出 数量积 数量积可以判定 >0 <0 ==0 判定在前面还是后面还是垂直
    /// Cos(数量积) 得到弧度
    /// 弧度 * Mathf.Rad2Deg 求得夹角
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float GetIncludedAngle(Vector3 pos1, Vector3 pos2)
    {
        var quantityProduct = Vector3.Dot(pos1.normalized, pos2.normalized);
        var rad = Mathf.Acos(quantityProduct);
        return rad * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 设置所有层级
    /// </summary>
    public static void SetAllLayer(int _layer,GameObject _gameObject)
    {
        if(_gameObject == null)
        {
            DebugLoger.LogError("SetAllLayer null");

            return;
        }

        _gameObject.layer = _layer;
        Transform[] childTransform =_gameObject.GetComponentsInChildren<Transform>(true);

        for (int loop = 0; loop < childTransform.Length; ++loop)
        {
            Transform child = childTransform[loop];

            child.gameObject.layer = _layer;
        }        
    }

    /// <summary>
    /// 激活物体
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="active"></param>
    public static void SetActive(GameObject gameObject,bool active)
    {
        gameObject.SetActive(active);
    }
}
