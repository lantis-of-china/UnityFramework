using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_RoomInfoProcess : ProcessMessageBase
    {
        public SingleMoba_RoomInfoProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_RoomInfo_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_RoomInfoProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_RoomInfo userEntry = new SingleMoba.SC_RoomInfo();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch(Exception e)
            {
                DebugLoger.LogError($"消息异常SingleMoba_EntryRoomProcess:{e}");
                return;
            }

            var worldSence = FrameWorkDrvice.WorldManagerInstance.currentWorld as WorldSpace.SingleMobaFightWorld;

            if (worldSence != null)
            {
                worldSence.InitSenceData(userEntry);
            }
        }
    }
}