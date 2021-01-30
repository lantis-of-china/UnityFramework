using System;
using UnityEngine;


public class CherishTweenAngleY : CherishTween
{
    public static CherishTweenAngleY End(GameObject target)
    {
		CherishTweenAngleY thisTween = target.GetComponent<CherishTweenAngleY>();
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
    public static CherishTweenAngleY Begin(GameObject target, float from, float to,float time,float waitTime,bool isLocal,bool isLerp = true,ParamarCallFun _callFun = null, object paramar = null)
    {
		CherishTweenAngleY thisTween = target.GetComponent<CherishTweenAngleY>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenAngleY>();
        }
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

		thisTween.isLerp = isLerp;
        thisTween.isLocal = isLocal;
        thisTween.curTime = 0;
        thisTween.waitTime = waitTime;
		thisTween.time = time;

		thisTween.type = CherishTweenXYZ.Y;
        thisTween.from = from;
        thisTween.to = to;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

	public bool isLerp;
    public bool isLocal;
    public float from;
    public float to;
    public CherishTweenXYZ type;

    public void OnEnableAwake()
    {
        if (isLocal)
        {
            if (type == CherishTweenXYZ.X)
            {
                gameObject.transform.localEulerAngles = new Vector3(from, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Y)
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, from, gameObject.transform.localEulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Z)
            {
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, from);
            }
        }
        else
        {
            if (type == CherishTweenXYZ.X)
            {
                gameObject.transform.eulerAngles = new Vector3(from, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Y)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, from, gameObject.transform.eulerAngles.z);
            }
            else if (type == CherishTweenXYZ.Z)
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, from);
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
				float newValue = 0.0f;
				if (isLerp)
				{
					newValue = Mathf.LerpAngle(from, to, curTime / time);
				}
				else
				{
					if (curTime < (time / 3))
					{
						newValue = Mathf.LerpAngle(from, to, curTime / time / (time - curTime));
					}
					else
					{
						newValue = Mathf.LerpAngle(from, to, curTime / time / (-(curTime - time)));
					}
				}



				if (isLocal)
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(newValue, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, newValue, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, newValue);
                    }
                }
                else
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.eulerAngles = new Vector3(newValue, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, newValue, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, newValue);
                    }
                }
            }
            else
            {
				curTime = time;
				if (isLocal)
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(to, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, to, gameObject.transform.localEulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, to);
                    }
                }
                else
                {
                    if (type == CherishTweenXYZ.X)
                    {
                        gameObject.transform.eulerAngles = new Vector3(to, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Y)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, to, gameObject.transform.eulerAngles.z);
                    }
                    else if (type == CherishTweenXYZ.Z)
                    {
                        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, to);
                    }
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