using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
    ///// <summary>
    ///// 用户进入游戏返回 结果 包括游戏服务器信息
    ///// </summary>
    //public class IMClub_ChangeMenberScoreProcess : ProcessMessageBase
    //{
    //    public IMClub_ChangeMenberScoreProcess()
    //    {
    //        ID = (int)IMClub.NetMessageType.SC_ChangeMenberScore_MsgType;
    //    }

    //    public static ProcessMessageBase _Instance;

    //    public static ProcessMessageBase GetProcessType()
    //    {
    //        if (_Instance == null)
    //        {
    //            _Instance = new IMClub_ChangeMenberScoreProcess();
    //        }
    //        return _Instance;
    //    }

    //    //处理方法
    //    public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
    //    {
    //        IMClub.SC_ChangeMenberScore messageBack = new IMClub.SC_ChangeMenberScore();
    //        try
    //        {
    //            messageBack.Deserializer(DateBuf, 0);
    //        }
    //        catch
    //        {
    //            DebugLoger.LogError("消息异常--------------");
    //            return;
    //        }     
    //    }
    //}
}