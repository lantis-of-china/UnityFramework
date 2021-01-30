using System;
using UnityEngine;
using UnityEngine.UI;
public class CherishTweenColor : CherishTween
{
    public static CherishTweenColor End(GameObject target)
    {
        CherishTweenColor thisTween = target.GetComponent<CherishTweenColor>();
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
    public static CherishTweenColor Begin(GameObject target, Color from, Color to, float time, float waitTime, bool child, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenColor thisTween = target.GetComponent<CherishTweenColor>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenColor>();
        }
        thisTween.waitTime = waitTime;
        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;
        thisTween.includeChild = child;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    public bool includeChild;
    public Color from;
    public Color to;
    public Graphic[] graphicList;

    public void OnEnableAwake()
    {
        if (includeChild)
        {
            graphicList  = GetComponentsInChildren<Graphic>(true);
        }
        else
        {
            graphicList = GetComponents<Graphic>();
        }

        SetAlpha(from);
    }

    /// <summary>
    /// 设置
    /// </summary>
    public void SetAlpha(Color color)
    {
        if(graphicList != null)
        {
            for (int i = 0; i < graphicList.Length;++i)
            {
                graphicList[i].color = color;
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
            curTime += Time.deltaTime;

            if (curTime < time)
            {
                SetAlpha(Color.Lerp(from, to, curTime / time));
            }
            else
            {
                curTime = time;

                SetAlpha(to);

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