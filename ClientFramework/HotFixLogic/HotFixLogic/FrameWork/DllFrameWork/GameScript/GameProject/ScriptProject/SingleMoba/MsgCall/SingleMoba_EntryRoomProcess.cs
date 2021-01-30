using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_EntryRoomProcess : ProcessMessageBase
    {
        public SingleMoba_EntryRoomProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_EntryRoom_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_EntryRoomProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            DebugLoger.Log($"Entry Room DateBuf:{DateBuf.Length}");
            SingleMoba.SC_EntryRoom userEntry = new SingleMoba.SC_EntryRoom();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch(Exception e)
            {
                DebugLoger.LogError($"消息异常SingleMoba_EntryRoomProcess:{e}");
                return;
            }

            SingleMoba.LogicDataSpace.gameRoomInfo = userEntry;
            FrameWorkDrvice.WorldManagerInstance.OpenWorld(SingleMoba.ConfigProject.projectFloderName, SingleMoba.WorldAssetsNameDefine.FightSence);
        }
    }
}