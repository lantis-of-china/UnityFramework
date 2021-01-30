using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_AddSkillBuffProcess : ProcessMessageBase
    {
        public SingleMoba_AddSkillBuffProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_AddSkillBuff_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_AddSkillBuffProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_AddSkillBuff userEntry = new SingleMoba.SC_AddSkillBuff();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.SkillBuffLogic.AddSkillBuff(userEntry);
        }
    }
}