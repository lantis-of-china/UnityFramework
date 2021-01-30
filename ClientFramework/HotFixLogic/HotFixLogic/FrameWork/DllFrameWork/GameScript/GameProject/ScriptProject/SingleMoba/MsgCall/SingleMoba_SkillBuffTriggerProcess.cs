using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_SkillBuffTriggerProcess : ProcessMessageBase
    {
        public SingleMoba_SkillBuffTriggerProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_SkillBuffTrigger_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_SkillBuffTriggerProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_SkillBuffTrigger userEntry = new SingleMoba.SC_SkillBuffTrigger();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.SkillBuffLogic.SkillBuffTrigger(userEntry);
        }
    }
}