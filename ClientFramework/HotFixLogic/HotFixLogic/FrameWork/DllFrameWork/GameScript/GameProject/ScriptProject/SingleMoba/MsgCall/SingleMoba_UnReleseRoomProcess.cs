using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    public class SingleMoba_UnReleseRoomProcess : ProcessMessageBase
    {
        public SingleMoba_UnReleseRoomProcess()
        {
            ID = (int)SingleMoba.NetMessageType.SC_UnReleseRoom_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SingleMoba_UnReleseRoomProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            SingleMoba.SC_UnReleseRoom userEntry = new SingleMoba.SC_UnReleseRoom();

            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
            catch(Exception e)
            {
                DebugLoger.LogError($"消息异常SingleMoba_EntryRoomProcess:{e}");
                return;
            }

            FrameWorkDrvice.UiManagerInstance.OpenUI(SingleMoba.ConfigProject.projectFloderName, SingleMoba.UIDefineName.UIFightEnd, true);
        }
    }
}