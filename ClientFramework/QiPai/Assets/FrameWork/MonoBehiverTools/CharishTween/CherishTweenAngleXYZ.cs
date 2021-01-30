using System;
using UnityEngine;
public class CherishTweenAngleXYZ : CherishTween
{
    public static CherishTweenAngleXYZ End(GameObject target)
    {
		CherishTweenAngleXYZ thisTween = target.GetComponent<CherishTweenAngleXYZ>();
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
	public static CherishTweenAngleXYZ Begin(GameObject target, Vector3 from, Vector3 to, float time, float waitTime, bool isLocal, bool isLerp,ParamarCallFun _callFun = null, object paramar = null)
    {
		CherishTweenAngleXYZ thisTween = target.GetComponent<CherishTweenAngleXYZ>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenAngleXYZ>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

		thisTween.isLerp = isLerp;
		thisTween.isLocal = isLocal;
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
	public bool isLocal;
	public bool isLerp;

    public void OnEnableAwake()
    {
		if (isLocal)
		{
			gameObject.transform.localEulerAngles = from;
		}
		else
		{
			gameObject.transform.eulerAngles = from;
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
				if (isLocal)
				{
					gameObject.transform.localEulerAngles = Fun(); 
				}
				else
				{
					gameObject.transform.eulerAngles = Fun();
				}
			}
            else
            {
                curTime = time;

				if (isLocal)
				{
					gameObject.transform.localEulerAngles = to;
				}
				else
				{
					gameObject.transform.eulerAngles = to;
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

	public Vector3 Fun()
	{
		float thisTime = curTime / time;
		if (isLerp)
		{			
			return new Vector3(Mathf.LerpAngle(from.x, to.x, thisTime), Mathf.LerpAngle(from.y, to.y, thisTime), Mathf.LerpAngle(from.z, to.z, thisTime));
		}
		else
		{
			return new Vector3(Mathf.LerpAngle(from.x, to.x, thisTime), Mathf.LerpAngle(from.y, to.y, thisTime), Mathf.LerpAngle(from.z, to.z, thisTime));
			//float recordTime = time;
			//recordTime /= 3;

			//if ((curTime) > recordTime)
			//{
			//	return new Vector3(Mathf.LerpAngle(from.x, to.x, curTime / time), Mathf.LerpAngle(from.y, to.y, curTime / time), Mathf.LerpAngle(from.z, to.z, curTime / time));
			//}
			//else if(curTime < recordTime)
			//{


			//	//前期
			//	return new Vector3(Mathf.LerpAngle(from.x, to.x, curTime / recordTime / 2), Mathf.LerpAngle(from.y, to.y, curTime / time / 2), Mathf.LerpAngle(from.z, to.z, curTime / time / 2));
			//}
		}
	}
}