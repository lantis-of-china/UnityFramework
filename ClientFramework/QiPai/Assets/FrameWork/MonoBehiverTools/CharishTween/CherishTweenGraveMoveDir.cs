using System;
using UnityEngine;
public class CherishTweenGraveMoveDir : CherishTween
{

    public static CherishTweenGraveMoveDir End(GameObject target)
    {
        CherishTweenGraveMoveDir thisTween = target.GetComponent<CherishTweenGraveMoveDir>();
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
    public static CherishTweenGraveMoveDir Begin(GameObject target, Vector3 from, Vector3 dir,float speed,float grave,float time, float waitTime, bool isLocal, ParamarCallFun _callFun = null, object paramar = null)
    {
        CherishTweenGraveMoveDir thisTween = target.GetComponent<CherishTweenGraveMoveDir>();
        if (thisTween == null)
        {
            thisTween = target.AddComponent<CherishTweenGraveMoveDir>();
        }
        thisTween.waitTime = waitTime;
        thisTween.curTime = 0;
        thisTween.time = time;
        thisTween.callFun = _callFun;
        thisTween.paramar = paramar;
        thisTween.from = from;
        thisTween.dir = dir;
        thisTween.speed = speed;

        thisTween.grave = grave;
        thisTween.isLocal = isLocal;
        thisTween.enabled = true;
        thisTween.OnEnableAwake();
        return thisTween;
    }

    public bool isLocal;
    public Vector3 from;
    public Vector3 dir;
    public float grave;
    public float speed;

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
                if (isLocal)
                {
                    gameObject.transform.localPosition += speed * dir * Time.deltaTime - new Vector3(0, grave * Time.deltaTime, 0);
                }
                else
                {
                    gameObject.transform.localPosition += speed * dir * Time.deltaTime - new Vector3(0, grave * Time.deltaTime, 0);
                }

                grave = Mathf.Lerp(grave, 0, curTime / time);
            }
            else
            {
                curTime = time;               

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