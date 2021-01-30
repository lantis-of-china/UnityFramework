using System;
using UnityEngine;
public class CherishTweenScale : CherishTween
{
    public static CherishTweenScale End(GameObject target)
    {
        CherishTweenScale thisTween = target.GetComponent<CherishTweenScale>();
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
    public static CherishTweenScale Begin(GameObject target, Vector3 from, Vector3 to, float time, float waitTime, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenScale thisTween = target.GetComponent<CherishTweenScale>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenScale>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.waitTime = waitTime;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    public Vector3 from;
    public Vector3 to;

    public void OnEnableAwake()
    {
        gameObject.transform.localScale = from;

    }

    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            curTime += Time.deltaTime;

            if (curTime < time)
            {
                gameObject.transform.localScale = Vector3.Lerp(from, to, curTime / time);
            }
            else
            {
                curTime = time;

                gameObject.transform.localScale = to;

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