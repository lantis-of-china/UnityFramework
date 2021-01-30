using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CherishTweenSpriteRenderAplash : CherishTween
{
    public static CherishTweenSpriteRenderAplash End(GameObject target)
    {
        CherishTweenSpriteRenderAplash thisTween = target.GetComponent<CherishTweenSpriteRenderAplash>();
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
    public static CherishTweenSpriteRenderAplash Begin(GameObject target, float from, float to, float time, float waitTime, bool child, ParamarCallFun _callFun = null, object paramar = null, List<GameObject> externs = null)
    {
        CherishTweenSpriteRenderAplash thisTween = target.GetComponent<CherishTweenSpriteRenderAplash>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenSpriteRenderAplash>();
        }
		thisTween.externObjs = externs;
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
    public float from;
    public float to;
    public SpriteRenderer[] graphicList;
	public List<GameObject> externObjs;

    public void OnEnableAwake()
    {
        if (includeChild)
        {
            graphicList  = GetComponentsInChildren<SpriteRenderer>(true);
        }
        else
        {
            graphicList = GetComponents<SpriteRenderer>();
        }

		if (graphicList != null && externObjs != null && externObjs.Count > 0)
		{
			List<SpriteRenderer> curSplist = new List<SpriteRenderer>(graphicList);
			for (int i = curSplist.Count - 1; i >= 0; --i)
			{
				if (HasExtern(curSplist[i].gameObject))
				{
					curSplist.RemoveAt(i);
				}
			}
			graphicList = curSplist.ToArray();
		}
        SetAlpha(from);
    }

	public bool HasExtern(GameObject target)
	{
		if (externObjs == null)
		{
			return false;
		}
		for (int i = 0; i < externObjs.Count; ++i)
		{
			if (externObjs[i] == target)
			{
				return true;
			}
		}
		return false;
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