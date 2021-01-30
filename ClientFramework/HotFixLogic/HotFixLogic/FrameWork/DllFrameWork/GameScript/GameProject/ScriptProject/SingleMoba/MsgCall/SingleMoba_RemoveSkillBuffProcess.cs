using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_RemoveSkillBuffProcess : ProcessMessageBase
    {
        public SingleMoba_RemoveSkillBuffProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_RemoveSkillBuff_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_RemoveSkillBuffProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_RemoveSkillBuff userEntry = new SingleMoba.SC_RemoveSkillBuff();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.SkillBuffLogic.RemoveSkillBuff(userEntry);
        }
    }
}