using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_SkillRemoveProcess : ProcessMessageBase
    {
        public SingleMoba_SkillRemoveProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_RemoveSkill_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_SkillRemoveProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_RemoveSkill userEntry = new SingleMoba.SC_RemoveSkill();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.SkillLogic.SkillRemove(userEntry);
        }
    }
}