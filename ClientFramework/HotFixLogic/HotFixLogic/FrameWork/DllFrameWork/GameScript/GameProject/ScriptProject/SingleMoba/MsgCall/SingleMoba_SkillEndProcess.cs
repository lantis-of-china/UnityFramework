using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_SkillEndProcess : ProcessMessageBase
    {
        public SingleMoba_SkillEndProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_SkillEnd_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_SkillEndProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_SkillEnd userEntry = new SingleMoba.SC_SkillEnd();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.SkillLogic.SkillEnd(userEntry);
        }
    }
}