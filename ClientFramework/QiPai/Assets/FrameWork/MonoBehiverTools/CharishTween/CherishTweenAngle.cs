using System;
using UnityEngine;

public enum CherishTweenXYZ
{
    X,
    Y,
    Z
}

public class CherishTweenAngle : CherishTween
{
    public static CherishTweenAngle End(GameObject target)
    {
        CherishTweenAngle thisTween = target.GetComponent<CherishTweenAngle>();
        if (thisTween != null)
        {
            thisTween.enabled = false;
            return thisTween;
        }
        return null;
    }

    /// <summary>
    /// 开始移动移动动画
    /// </summary>
    /// <param name="target">要移动的目标</param>
    /// <param name="from">开始位置</param>
    /// <param name="to">目标位置</param>
    /// <param name="time">需要时间</param>
    /// <param name="waitTime">延时启动</param>
    /// <param name="_callFun">完成回调</param>
    /// <param name="isLocal">是否局部坐标系</param>
    /// <returns></returns>
    public static CherishTweenAngle Begin(GameObject target, float from, float to, CherishTweenXYZ type, float waitTime,bool isAdd,float speed,bool isLocal,ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenAngle thisTween = target.GetComponent<CherishTweenAngle>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenAngle>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.isAdd = isAdd;
        thisTween.speed = speed;
        thisTween.isLocal = isLocal;
        thisTween.curTime = 0;
        thisTween.waitTime = waitTime;
        thisTween.type = type;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    public bool isAdd;
    public bool isLocal;
    public float from;
    public float to;
    public CherishTweenXYZ type;
    public float speed;

    public void OnEnableAwake()
    {
        if (isLocal)
        {
            if (type == CherishTweenXYZ.X)
            {
                gameObject.transform.localEulerAngles = new Vector3(from, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Y)
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, from, gameObject.transform.localEulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Z)
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, from);
            }
        }
        else
        {
            if (type == CherishTweenXYZ.X)
            {
                gameObject.transform.eulerAngles = new Vector3(from, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Y)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, from, gameObject.transform.eulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Z)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, from);
            }
        }
    }

    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            float newValue = 0.0f;
            if (isAdd)
            {
                newValue += Time.deltaTime * speed;
            }
            else
            {
                newValue -= Time.deltaTime * speed;
            }

            if (isLocal)
            {
                if (type == CherishTweenXYZ.X)
                {
                    newValue += gameObject.transform.localEulerAngles.x;
                }
                else if (type == CherishTweenXYZ.Y)
                {
                    newValue += gameObject.transform.localEulerAngles.y;
                }
                else if (type == CherishTweenXYZ.Z)
                {
                    newValue += gameObject.transform.localEulerAngles.z;
                }
            }
            else
            {
                if (type == CherishTweenXYZ.X)
                {
                    newValue += gameObject.transform.eulerAngles.x;
                }
                else if (type == CherishTweenXYZ.Y)
                {
                    newValue += gameObject.transform.eulerAngles.y;
                }
                else if (type == CherishTweenXYZ.Z)
                {
                    newValue += gameObject.transform.eulerAngles.z;
                }
            }


            bool isFinish = false;
            if (isAdd)
            {
                if (newValue >= to)
                {
                    isFinish = true;
                }
                while (newValue > 360.0f)
                {
                    newValue -= 360.0f;
                }
            }
            else
            {
                if (newValue <= to)
                {
                    isFinish = true;
                }
                while (newValue < 0.0f)
                {
                    newValue += 360.0f;
                }
            }



            if (!isFinish)
            {
                if (isLocal)
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(newValue, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, newValue, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, newValue);
                    }
                }
                else
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.eulerAngles = new Vector3(newValue, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, newValue, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, newValue);
                    }
                }
            }
            else
            {

                if (isLocal)
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(to, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, to, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, to);
                    }
                }
                else
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.eulerAngles = new Vector3(to, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, to, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, to);
                    }
                }

                enabled = false;

                if (callFun != null)
                {
                    ParamarCallFun cullThis = callFun;
                    callFun = null;
                    cullThis(paramar);
                }
            }
        }
    }
}