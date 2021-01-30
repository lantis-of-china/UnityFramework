using System;
using UnityEngine;
using UnityEngine.UI;
public class CherishTweenAlpha : CherishTween
{
	public static void Clear(GameObject target)
	{
		CherishTweenAlpha thisTween = target.GetComponent<CherishTweenAlpha>();
		if (thisTween != null)
		{
			GameObject.Destroy(thisTween);
		}
	}

    public static CherishTweenAlpha End(GameObject target)
    {
        CherishTweenAlpha thisTween = target.GetComponent<CherishTweenAlpha>();
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
    public static CherishTweenAlpha Begin(GameObject target, float from, float to, float time, float waitTime, bool child, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenAlpha thisTween = target.GetComponent<CherishTweenAlpha>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenAlpha>();
        }
		thisTween.end = false;
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

	public bool end;
    public bool includeChild;
    public float from;
    public float to;
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
    public void SetAlpha(float alpha)
    {
        if(graphicList != null)
        {
            for (int i = 0; i < graphicList.Length;++i)
            {
                graphicList[i].color = new Color(graphicList[i].color.r, graphicList[i].color.g, graphicList[i].color.b, alpha);
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
                SetAlpha(Mathf.Lerp(from, to, curTime / time));
            }
            else
            {
                curTime = time;

                SetAlpha(to);

				if (end)
				{
					enabled = false;

					if (callFun != null)
					{
						ParamarCallFun cullThis = callFun;
						callFun = null;
						cullThis(paramar);
					}
				}
				end = true;
            }
        }
    }
}