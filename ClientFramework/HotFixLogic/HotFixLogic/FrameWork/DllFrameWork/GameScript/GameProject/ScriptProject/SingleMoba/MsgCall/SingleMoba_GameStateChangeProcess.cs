using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_GameStateChangeProcess : ProcessMessageBase
    {
        public SingleMoba_GameStateChangeProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_GamerStateChange_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_GameStateChangeProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_GamerStateChange userEntry = new SingleMoba.SC_GamerStateChange();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.LogicDataSpace.SetPlayerChangeState(userEntry.stateChanges);
        }
    }
}