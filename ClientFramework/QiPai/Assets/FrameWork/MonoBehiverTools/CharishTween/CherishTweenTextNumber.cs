using System;
using UnityEngine;
using UnityEngine.UI;

public class CherishTweenTextNumber : CherishTween
{
    public static CherishTweenTextNumber End(GameObject target)
    {
        CherishTweenTextNumber thisTween = target.GetComponent<CherishTweenTextNumber>();
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
    public static CherishTweenTextNumber Begin(GameObject target, float from, float to,float time, float waitTime,string parStr,bool isFloat,ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenTextNumber thisTween = target.GetComponent<CherishTweenTextNumber>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenTextNumber>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.isFloat = isFloat;
        thisTween.parStr = parStr;
        thisTween.curTime = 0;
        thisTween.waitTime = waitTime;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    public bool isFloat;
    public string parStr;
    public float from;
    public float to;
    public Text lb_text;

    public void OnEnableAwake()
    {
        lb_text = gameObject.GetComponent<Text>();
        if (isFloat)
        {
            lb_text.text = string.Format(parStr,from.ToString("0.00"));
        }
        else
        {
            lb_text.text = string.Format(parStr, from.ToString("0"));
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
            curTime += Time.deltaTime;

            if (curTime < time)
            {
                float newValue = 0.0f;
                newValue = Mathf.Lerp(from, to, curTime / time);

                if (isFloat)
                {
                    lb_text.text = string.Format(parStr,newValue.ToString("0.00"));
                }
                else
                {
                    lb_text.text = string.Format(parStr,newValue.ToString("0"));
                }
            }
            else
            {
                curTime = time;
                if (isFloat)
                {
                    lb_text.text = string.Format(parStr,to.ToString("0.00"));
                }
                else
                {
                    lb_text.text = string.Format(parStr,to.ToString("0"));
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