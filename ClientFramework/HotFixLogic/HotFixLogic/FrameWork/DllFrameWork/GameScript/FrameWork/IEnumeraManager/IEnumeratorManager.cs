using UnityEngine;
using System.Collections;
using System;




public class IEnumeratorManager
{
    public class Cortinue 
    {
        public bool finish;
    }

    public class YieldCortinue : Cortinue, IEnumerator
    {
        public string group = "";
        public string key = "";
        public IEnumerator IEnumeratorInstance = null;

        private object current;

        public bool MoveNext()
        {
            current = 1;
            if (this.finish)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
        }

        public object Current
        {
            get { return current; }
        }

        public void Dispose()
        {
        }
    }

    public class WaitForSeconds : Cortinue, IEnumerator
    {
        public float recrdTime;
        public float seconds;
        private object current;

        public WaitForSeconds(float time)
        {
            this.finish = false;
            this.seconds = time;
            this.recrdTime = 0.0f;
        }

        public bool MoveNext()
        {
            if (this.recrdTime >= this.seconds)
            {
                this.finish = true;
                return false;
            }
            this.recrdTime += Time.deltaTime;


            return true;
        }

        public void Reset()
        {
        }

        public object Current
        {
            get { return current; }
        }

        public void Dispose()
        {
        }
    }


    public System.Collections.Generic.List<YieldCortinue> IEnumerotorList = new System.Collections.Generic.List<YieldCortinue>();

    private static IEnumeratorManager _instance;

    public static IEnumeratorManager Instance { get { if (_instance == null) { _instance = new IEnumeratorManager(); } return _instance; } }

    /// <summary>
    /// 更新协同
    /// </summary>
    /// <param name="currentIEnFun"></param>
    public void UpIEnumerator(YieldCortinue currentIEnFun)
    {
        if (currentIEnFun.IEnumeratorInstance.Current == null)
        {
            if (!currentIEnFun.IEnumeratorInstance.MoveNext())
            {
                currentIEnFun.finish = true;
                currentIEnFun.key = null;

                IEnumerotorList.Remove(currentIEnFun);
            }
        }
        else
        {
            if (currentIEnFun.IEnumeratorInstance.Current is WaitForSeconds)
            {
                WaitForSeconds curCall = currentIEnFun.IEnumeratorInstance.Current as WaitForSeconds;
                if (curCall != null && !curCall.MoveNext())
                {
                    if (!currentIEnFun.IEnumeratorInstance.MoveNext())
                    {
                        currentIEnFun.finish = true;
                        currentIEnFun.key = null;

                        IEnumerotorList.Remove(currentIEnFun);
                    }
                }
            }
            else if (currentIEnFun.IEnumeratorInstance.Current is YieldCortinue)
            {
                YieldCortinue curCall = currentIEnFun.IEnumeratorInstance.Current as YieldCortinue;
                if (curCall != null && !curCall.MoveNext())
                {
                    if (!currentIEnFun.IEnumeratorInstance.MoveNext())
                    {
                        currentIEnFun.finish = true;
                        currentIEnFun.key = null;

                        IEnumerotorList.Remove(currentIEnFun);
                    }
                }
            }
            else
            {
                if (!currentIEnFun.IEnumeratorInstance.MoveNext())
                {
                    currentIEnFun.finish = true;
                    currentIEnFun.key = null;

                    IEnumerotorList.Remove(currentIEnFun);
                }
            }
        }
    }

    public void UpIEnumerator()
    {
        for (int loop = IEnumerotorList.Count - 1; loop >= 0; loop--)
        {
            YieldCortinue currentIEnFun = IEnumerotorList[loop];
            UpIEnumerator(currentIEnFun);
        }
    }
	

    public YieldCortinue StartCoroutine(IEnumerator FUN,string group = "")
    {
        YieldCortinue IenumeratorAttrab = new YieldCortinue();

        IenumeratorAttrab.finish = false;
        IenumeratorAttrab.group = group;
        IenumeratorAttrab.key = FUN.ToString();
        IenumeratorAttrab.IEnumeratorInstance = FUN;

        StartCoroutineAdd(IenumeratorAttrab);

        UpIEnumerator(IenumeratorAttrab);
        return IenumeratorAttrab;
    }

    public void StartCoroutineAdd(YieldCortinue FunAttrab)
    {
        IEnumerotorList.GetType().ToString();
        IEnumerotorList.Add(FunAttrab);
    }
	
    public void Stop(IEnumerator FUN)
    {
        System.Collections.Generic.List<YieldCortinue> ienumeratorArray = IEnumerotorList.FindAll(item => item.IEnumeratorInstance == FUN);

        if (ienumeratorArray != null)
        {
            for (int loop = 0; loop < ienumeratorArray.Count; loop++)
            {
                YieldCortinue iea = ienumeratorArray[loop];           

                IEnumerotorList.Remove(iea);
            }
        }
    }
	
    public void StopGroupAll(string group)
    {
		for (int i = IEnumerotorList.Count - 1; i >= 0; --i)
		{
			if (IEnumerotorList[i].group == group)
			{
				IEnumerotorList.RemoveAt(i);
			}
		}
    }

    public void StopAll()
    {
        IEnumerotorList.Clear();
    }
}