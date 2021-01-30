using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_GamerMoveProcess : ProcessMessageBase
    {
        public SingleMoba_GamerMoveProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_GamerMove_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_GamerMoveProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_GamerMove userEntry = new SingleMoba.SC_GamerMove();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.CharacterManager.Instance.SetCharacterMove(userEntry);
        }
    }
}