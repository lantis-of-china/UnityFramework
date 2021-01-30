using System;
using UnityEngine;
public class CherishTweenWaitCallBack : CherishTween
{
    public static CherishTweenWaitCallBack End(GameObject target)
    {
        CherishTweenWaitCallBack thisTween = target.GetComponent<CherishTweenWaitCallBack>();
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
    public static CherishTweenWaitCallBack Begin(GameObject target,float waitTime, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenWaitCallBack thisTween = target.GetComponent<CherishTweenWaitCallBack>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenWaitCallBack>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.curTime = 0;
        thisTween.waitTime = waitTime;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }


    public void OnEnableAwake()
    {

    }

    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
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