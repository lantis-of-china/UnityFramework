using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CherishTweenSpriteMeshColor : CherishTween
{
    public static CherishTweenSpriteMeshColor End(GameObject target)
    {
		CherishTweenSpriteMeshColor thisTween = target.GetComponent<CherishTweenSpriteMeshColor>();
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
    public static CherishTweenSpriteMeshColor Begin(GameObject target, Color from, Color to, float time, float waitTime, bool child, bool setStart, ParamarCallFun _callFun = null, object paramar = null, List<GameObject> externs = null)
    {
		CherishTweenSpriteMeshColor thisTween = target.GetComponent<CherishTweenSpriteMeshColor>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenSpriteMeshColor>();
        }
		thisTween.externObjs = externs;
		thisTween.waitTime = waitTime;
        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;
		thisTween.setStart = setStart;
		thisTween.includeChild = child;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

	public bool setStart;
    public bool includeChild;
    public Color from;
    public Color to;
    public MeshRenderer[] graphicList;
	public List<GameObject> externObjs;

    public void OnEnableAwake()
    {
        if (includeChild)
        {
            graphicList  = GetComponentsInChildren<MeshRenderer>(true);
        }
        else
        {
            graphicList = GetComponents<MeshRenderer>();
        }

		if (graphicList != null && externObjs != null && externObjs.Count > 0)
		{
			List<MeshRenderer> curSplist = new List<MeshRenderer>(graphicList);
			for (int i = curSplist.Count - 1; i >= 0; --i)
			{
				if (HasExtern(curSplist[i].gameObject))
				{
					curSplist.RemoveAt(i);
				}
			}
			graphicList = curSplist.ToArray();
		}

		if (setStart)
		{
			SetColor(from);
		}
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
    public void SetColor(Color color)
    {
        if(graphicList != null)
        {
            for (int i = 0; i < graphicList.Length;++i)
            {
				if (graphicList[i].sharedMaterial != null && graphicList[i].sharedMaterials.Length > 0 && graphicList[i].sharedMaterials[0] != null)
				{
                    graphicList[i].sharedMaterials[0].color = color;// new Color(graphicList[i].sharedMaterials[0].color.r, graphicList[i].sharedMaterials[0].color.g, graphicList[i].sharedMaterials[0].color.b, graphicList[i].sharedMaterials[0].color.a);
				}
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
				SetColor(Color.Lerp(from, to, curTime / time));
            }
            else
            {
                curTime = time;

				SetColor(to);

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