using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CherishDelay
{
    /// <summary>
    /// 延迟
    /// </summary>
    public class DelayData
    {
        public float totleTime = 0;
        public DelayRun.Run runCall;
        public object parmar;
        public string groupName;
    }

    public class DelayRun
    {
        private static List<DelayData> listData = new List<DelayData>();
        public static void Update()
        {
            for(int i =  listData.Count - 1;i >= 0 ;--i)
            {
                listData[i].totleTime -= Time.deltaTime;
                if(listData[i].totleTime <= 0.0f)
                {
                    listData[i].runCall();
                    listData.RemoveAt(i);
                }
            }
        }

        public delegate void Run();

        public static void Add(object paramar, float time, Run run)
        {
            Add(paramar, time, run,"default");
        }

        public static void Add(object paramar, float time, Run run, string group)
        {
            DelayData data = new DelayData();
            data.parmar = paramar;
            data.totleTime = time;
            data.runCall = run;
            data.groupName = group;
            listData.Add(data);
        }

        public static void RemoveGroup(string group)
        {
            for (int i = listData.Count - 1; i >= 0; --i)
            {
                if (listData[i].groupName == group)
                {
                    listData.RemoveAt(i);
                }
            }
        }

        public static void RemoveAll(string group)
        {
            listData.Clear();
        }
    }
}
