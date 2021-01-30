using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_EatPropsProcess : ProcessMessageBase
    {
        public SingleMoba_EatPropsProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_EatProp_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_EatPropsProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_EatProp userEntry = new SingleMoba.SC_EatProp();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.PropLogic.EatProps(userEntry.playerId, userEntry.propId, userEntry.players);
        }
    }
}