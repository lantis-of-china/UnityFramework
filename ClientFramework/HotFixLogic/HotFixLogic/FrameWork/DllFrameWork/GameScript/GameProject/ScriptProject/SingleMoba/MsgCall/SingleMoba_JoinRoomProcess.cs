using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_JoinRoomProcess : ProcessMessageBase
    {
        public SingleMoba_JoinRoomProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_JoinRoom_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_JoinRoomProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_JoinRoom userEntry = new SingleMoba.SC_JoinRoom();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch
            {
                DebugLoger.LogError("消息异常--------------");
                return;
            }

            SingleMoba.CharacterManager.Instance.AddPlayerInfo(userEntry.joinPlayerInfo);
        }
    }
}