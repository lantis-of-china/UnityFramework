using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_GamerMoveStopProcess : ProcessMessageBase
    {
        public SingleMoba_GamerMoveStopProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_GamerMoveStop_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_GamerMoveStopProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_GamerMoveStop userEntry = new SingleMoba.SC_GamerMoveStop();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.CharacterManager.Instance.SetCharacterMoveStop(userEntry);
        }
    }
}