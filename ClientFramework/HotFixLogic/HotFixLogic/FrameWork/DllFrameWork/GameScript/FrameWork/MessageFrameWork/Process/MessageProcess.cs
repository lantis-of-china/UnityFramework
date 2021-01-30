using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Process
{
    public class MessageProcess
    {
        public System.Collections.Generic.Dictionary<int, ProcessMessageBase> ProcessMap;

        private static MessageProcess _Instance;

        public static MessageProcess Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MessageProcess();
                }
                return _Instance;
            }
        }

        public MessageProcess()
        {
            ClassFinder.FindClass("WordProcess",out ProcessMap);
        }
    }
}
