using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_SkillHitProcess : ProcessMessageBase
    {
        public SingleMoba_SkillHitProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_HitSkill_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_SkillHitProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_HitSkill userEntry = new SingleMoba.SC_HitSkill();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.SkillLogic.SkillHit(userEntry);
        }
    }
}