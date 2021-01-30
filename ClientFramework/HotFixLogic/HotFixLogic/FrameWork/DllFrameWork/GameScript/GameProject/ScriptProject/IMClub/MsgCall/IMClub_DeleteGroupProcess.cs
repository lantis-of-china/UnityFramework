//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace WordProcess
//{

//    /// <summary>
//    /// 用户进入游戏返回 结果 包括游戏服务器信息
//    /// </summary>
//    public class IMClub_DeleteGroupProcess : ProcessMessageBase
//    {
//        public IMClub_DeleteGroupProcess()
//        {
//            ID = (int)IMClub.NetMessageType.SC_DeleteMenber_MsgType;
//        }

//        public static ProcessMessageBase _Instance;

//        public static ProcessMessageBase GetProcessType()
//        {
//            if (_Instance == null)
//            {
//                _Instance = new IMClub_DeleteGroupProcess();
//            }
//            return _Instance;
//        }

//        //处理方法
//        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
//        {
//            IMClub.SC_DeleteMenber messageBack = new IMClub.SC_DeleteMenber();
//            try
//            {
//                messageBack.Deserializer(DateBuf, 0);
//            }
//            catch
//            {
//                DebugLoger.LogError("消息异常--------------");
//                return;
//            }        
//        }
//    }
//}