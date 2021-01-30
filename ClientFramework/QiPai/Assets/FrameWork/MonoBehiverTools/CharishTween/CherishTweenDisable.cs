using System;
using UnityEngine;
public class CherishTweenDisable : CherishTween
{
    public static CherishTweenDisable End(GameObject target)
    {
        CherishTweenDisable thisTween = target.GetComponent<CherishTweenDisable>();
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
    public static CherishTweenDisable Begin(GameObject target, float time, float waitTime, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenDisable thisTween = target.GetComponent<CherishTweenDisable>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenDisable>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.waitTime = waitTime;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }


    public void OnEnableAwake()
    {
        gameObject.SetActive(true);
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
            }
            else
            {
                curTime = time;

                gameObject.SetActive(false);

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