using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CherishWebGLSupport
{
    public class CherishThread
    {
        public void Test()
        {

        }
#if UNITY_WEBGL && !UNITY_EDITOR
        public CherishThread(ThreadStart action)
        {
        }

        public CherishThread(ParameterizedThreadStart action)
        {
        }

        public void Start()
        {
        }

        public void Start(object objparamar)
        {
        }

        public void Abort()
        {
        }

        public void Abort(object state)
        {
        }

        public static void Sleep(int msTime)
        {
        }
#else
        public Thread curThread;
        public CherishThread(ThreadStart action)
        {
            curThread = new Thread(action);
        }

        public CherishThread(ParameterizedThreadStart action)
        {
            curThread = new Thread(action);
        }

        public void Start()
        {
            curThread.Start();
        }

        public void Start(object objparamar)
        {
            curThread.Start(objparamar);
        }

        public void Abort()
        {
            curThread.Abort();
        }

        public void Abort(object state)
        {
            curThread.Abort(state);
        }

        public static void Sleep(int msTime)
        {
            Thread.Sleep(msTime);
        }
#endif
    }
}
