using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DelegateDiv
{

    public delegate void UpdateCallFun(float _deltaTime);
    public delegate void VoidCallFun();

    public delegate object GeneralCallFun();
    public delegate object GeneralCallFunOneParmer(object paramer);

    public static T SpawnDelegate<T>() where T:new()
    {
      T delegateInstance = new T();

      return delegateInstance;
    }

    public class UguiActionCall
    {
        public static List<UguiActionCall> actionList = new List<UguiActionCall>();
        public UnityEngine.Events.UnityAction unityAction;

        public UguiActionCall(UnityEngine.UI.Button button)
        {
            if (button != null)
            {
                button.onClick.AddListener(CallFun);

                actionList.Add(this);
            }
        }
        public void CallFun()
        {
            if(unityAction!=null)
            {
                Debug.Log("Call Fun");
                unityAction();
            }
            else
            {
                Debug.Log("No Call Fun");
            }

            actionList.Remove(this);
        }
    }
}


public class GenericityTool
{
    /// <summary>
    /// 枚举对象转到Short类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static short EnumToShort<T>(T _enumType)
    {
        object objValue = _enumType as object;
        //return System.Convert.ToInt16(objValue);
        return (short)objValue;
    }

    /// <summary>
    /// 枚举对象转到Int类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static int EnumToInt<T>(T _enumType)
    {
        object objValue = _enumType as object;
        //return System.Convert.ToInt32(objValue);
        return (int)objValue;
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectInstance"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T GetComponentByPath<T>(GameObject objectInstance, string path) where T : Component
    {
        if (objectInstance != null)
        {
            Transform childTransform = objectInstance.transform.Find(path);
            if (childTransform != null)
            {
                return childTransform.GetComponent<T>();
            }
        }

        return null;
    }

    /// <summary>
    /// 获取子物体通过路径
    /// </summary>
    /// <param name="objectInstance"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject GetObjectByPath(GameObject objectInstance,string path)
    {
        if(objectInstance!=null)
        {
            Transform findTrans = objectInstance.transform.Find(path);

            if(findTrans!=null)
            {
                return findTrans.gameObject;
            }
        }
        return null;
    }
}











/// <summary>
/// 自定义KeyValue
/// </summary>
public class CherishKeyValue<K, V>
{
    public K key;
    public V value;
}