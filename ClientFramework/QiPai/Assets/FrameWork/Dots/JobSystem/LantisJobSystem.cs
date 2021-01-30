using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Jobs;

namespace Lantis
{
    public class LantisJobRegistBase
    {
        public object paramar;
        public JobHandle handle;
        public Action releseCall;
    }

    public class LantisJobRegist1 : LantisJobRegistBase
    {
        public Action<object> callFun;
    }

    public class LantisJobRegist2 : LantisJobRegistBase
    {
        public Action<object,object> callFun;
    }
    public class LantisJobRegist3 : LantisJobRegistBase
    {
        public Action<object,object,object> callFun;
    }


    public class LantisJobSystem
    {
        public static object lockself = new object();
        public static LantisDictronaryList<object, LantisJobRegistBase> jobRegistHandle = new LantisDictronaryList<object, LantisJobRegistBase>();

        public static void AddRegistParamar(object job, LantisJobRegistBase regist)
        {
            lock (lockself)
            {
                jobRegistHandle.AddValue(job, regist);
            }
        }

        public static void RemoveRegistParamar(object job)
        {
            lock (lockself)
            {
                if (jobRegistHandle.HasKey(job))
                {

                    jobRegistHandle.RemoveKey(job);
                }
            }
        }

        public static LantisJobRegistBase GetRegistParamar(object job)
        {
            lock (lockself)
            {
                if (jobRegistHandle.HasKey(job))
                {

                    return jobRegistHandle[job];
                }
            }

            return null;
        }

        public static void SetHaneld(object job,JobHandle handle)
        {
            lock (lockself)
            {
                if (jobRegistHandle.HasKey(job))
                {
                    jobRegistHandle[job].handle = handle;
                }
            }
        }

        public static void Complete()
        {
            var taskList = jobRegistHandle.ValueToList();

            for (var i = 0; i < taskList.Count; ++i)
            {
                var regist = taskList[i];
                regist.handle.Complete();
            }

            for (var i = taskList.Count - 1; i >= 0; --i)
            {
                var regist = taskList[i];

                if (regist.releseCall != null)
                {
                    regist.releseCall();
                }
            }
        }
    }
}