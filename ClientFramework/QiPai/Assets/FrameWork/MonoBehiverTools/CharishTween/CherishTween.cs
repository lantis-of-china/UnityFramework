using System;
using UnityEngine;



public class CherishTween : MonoBehaviour
{

    public delegate void ParamarCallFun(object paramar);
    public float waitTime;
    public float time;
    public float curTime;
    public ParamarCallFun callFun;
    public object paramar;

	public static void ClearAllTween(GameObject target)
	{
		CherishTween[] comps = target.GetComponents<CherishTween>();
		if (comps != null)
		{
			for (int i = 0; i < comps.Length; ++i)
			{
				GameObject.Destroy(comps[i]);
			}
		}
	}

    public static void EndAllTween(GameObject target)
    {
        CherishTween[] comps = target.GetComponents<CherishTween>();
        if(comps!= null)
        {
            for(int i = 0;i < comps.Length;++i)
            {
                comps[i].enabled = false;
            }
        }
    }
}
