using UnityEngine;
using System.Collections.Generic;
using ILRuntime.Other;
using System;
using System.Collections;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.CLR.Method;

public class QueueAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(Queue);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    internal class Adaptor : Queue, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

        IMethod mCountMethod;
        public int Count
        {
            get
            {
                if (mCountMethod == null)
                {
                    mCountMethod = instance.Type.GetMethod("System.Collections.Queue.get_Count", 0);
                }

                if (mCountMethod != null)
                {
                    return (int)appdomain.Invoke(mCountMethod, instance);
                }
                else
                {
                    return 0;
                }
            }
        }

        IMethod mDequeueMethod;
        public object Dequeue()
        {
            if (mDequeueMethod == null)
            {
                mDequeueMethod = instance.Type.GetMethod("Dequeue", 0);
            }

            if (mDequeueMethod != null)
            {
                return appdomain.Invoke(mDequeueMethod, instance);
            }
            else
            {
                return null;
            }
        }

        IMethod mEnqueueMethod;
        public void Enqueue(object objectValue)
        {
            if (mEnqueueMethod == null)
            {
                mEnqueueMethod = instance.Type.GetMethod("System.Collections.Queue.Enqueue", 1);
            }

            if (mEnqueueMethod != null)
            {
                appdomain.Invoke(mEnqueueMethod, instance, objectValue);
            }
        }

        public override string ToString()
        {
            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
            m = instance.Type.GetVirtualMethod(m);
            if (m == null || m is ILMethod)
            {
                return instance.ToString();
            }
            else
                return instance.Type.FullName;
        }
    }
}

