using System;
using UnityEngine;
using UnityEngine.UI;

public class CherishTweenImageFill : CherishTween
{
    public static CherishTweenImageFill End(GameObject target)
    {
        CherishTweenImageFill thisTween = target.GetComponent<CherishTweenImageFill>();
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
    public static CherishTweenImageFill Begin(GameObject target, float from, float to,float time, float waitTime,ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenImageFill thisTween = target.GetComponent<CherishTweenImageFill>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenImageFill>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.curTime = 0;
        thisTween.waitTime = waitTime;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    public float from;
    public float to;
    public Image img;

    public void OnEnableAwake()
    {
        img = gameObject.GetComponent<Image>();
        img.fillAmount = from;
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
                float newValue = 0.0f;
                newValue = Mathf.Lerp(from, to, curTime / time);
                img.fillAmount = newValue;
            }
            else
            {
                curTime = time;
                img.fillAmount = to;

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