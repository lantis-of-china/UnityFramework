using UnityEngine;
using System.Collections;

public class Delay : MonoBehaviour 
{
	
	public float delayTime = 1.0f;
    public bool ignoreTimeScale = false;
    //private MainThreadTimer timer = null;
    
	
	// Use this for initialization
	void Start () 
    {		
		gameObject.SetActive(false);
        if (!ignoreTimeScale)
        {
            Invoke("DelayFunc", delayTime);
        }
        else
        {

        }
	}
	
	void DelayFunc()
	{
        gameObject.SetActive(true);
	}

    static void DelayActive(string str, object obj)
    {
        GameObject gobj = (GameObject)obj;
        gobj.SetActive(true);
    }

    void Destroy()
    {

    }
	
}
