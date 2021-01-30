using System;
using UnityEngine;
public class CherishTweenFollowMove : CherishTween
{

    public static CherishTweenFollowMove End(GameObject target)
    {
        CherishTweenFollowMove thisTween = target.GetComponent<CherishTweenFollowMove>();
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
    public static CherishTweenFollowMove Begin(GameObject target, Vector3 from, GameObject to, float time, float waitTime, bool isLocal, ParamarCallFun _callFun = null,object paramar = null,bool isLerp = true)
    {
        CherishTweenFollowMove thisTween = target.GetComponent<CherishTweenFollowMove>();
        if (thisTween == null)
        {            
            thisTween = target.AddComponent<CherishTweenFollowMove>();
        }

        thisTween.waitTime = waitTime;
        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;
        thisTween.from = from;
        thisTween.to = to;
		thisTween.isLerp = isLerp;
        thisTween.isLocal = isLocal;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

	public bool isLerp;
    public bool isLocal;
    public Vector3 from;
    public GameObject to;

    public void OnEnableAwake()
    {
        if (isLocal)
        {
            gameObject.transform.localPosition = from;
        }
        else
        {
            gameObject.transform.position = from;
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
				Vector3 newValue = Vector3.zero;

				if (isLerp)
				{
                    if (isLocal)
                    {
                        newValue = Vector3.Lerp(from, to.transform.localPosition, curTime / time);
                    }
                    else
                    {
                        newValue = Vector3.Lerp(from, to.transform.position, curTime / time);
                    }
				}
				else
				{
                    if (isLocal)
                    {
                        newValue = Vector3.Slerp(from, to.transform.localPosition, curTime / time);
                    }
                    else
                    {
                        newValue = Vector3.Slerp(from, to.transform.position, curTime / time);
                    }
                }

				if (isLocal)
                {
                    gameObject.transform.localPosition = newValue;
                }
                else
                {
                    gameObject.transform.position = newValue;
                }
            }
            else
            {
                curTime = time;

                if (isLocal)
                {
                    gameObject.transform.localPosition = to.transform.localPosition;
                }
                else
                {
                    gameObject.transform.position = to.transform.position;
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

    public void SetCurPos()
    {
        if (isLocal)
        {
            gameObject.transform.localPosition = Vector3.Lerp(from, to.transform.localPosition, curTime / time);
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(from, to.transform.position, curTime / time);
        }       
    }
}