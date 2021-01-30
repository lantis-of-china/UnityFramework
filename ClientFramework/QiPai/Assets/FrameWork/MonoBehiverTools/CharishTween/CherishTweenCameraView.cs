using System;
using UnityEngine;
public class CherishTweenCameraView : CherishTween
{
    public static CherishTweenCameraView End(GameObject target)
    {
        CherishTweenCameraView thisTween = target.GetComponent<CherishTweenCameraView>();
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
    public static CherishTweenCameraView Begin(GameObject target, float from,float to,float time, float waitTime, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenCameraView thisTween = target.GetComponent<CherishTweenCameraView>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenCameraView>();
        }
        thisTween.camera = target.GetComponent<Camera>();
        thisTween.from = from;
        thisTween.to = to;
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;

        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.waitTime = waitTime;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    /// <summary>
    /// 获取到的相机
    /// </summary>
    public Camera camera;
    public float from;
    public float to;

    public void OnEnableAwake()
    {
        camera.fieldOfView = from;
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
                camera.fieldOfView = Mathf.Lerp(from, to, curTime / time);
            }
            else
            {
                curTime = time;

                camera.fieldOfView = to;

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