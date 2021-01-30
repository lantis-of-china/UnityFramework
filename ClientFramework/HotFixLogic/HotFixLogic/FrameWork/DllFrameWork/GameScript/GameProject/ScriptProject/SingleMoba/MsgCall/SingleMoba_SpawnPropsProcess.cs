using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_SpawnPropsProcess : ProcessMessageBase
    {
        public SingleMoba_SpawnPropsProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_SpawnProp_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_SpawnPropsProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_SpawnProps userEntry = new SingleMoba.SC_SpawnProps();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.PropLogic.AddProps(userEntry.props);
        }
    }
}