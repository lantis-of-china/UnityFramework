using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_GetMenbersProcess : ProcessMessageBase
    {
        public IMClub_GetMenbersProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_GetMenbers_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_GetMenbersProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.GetMenbers",true);
            IMClub.SC_GetMenbers messageBack = new IMClub.SC_GetMenbers();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == (byte)1)
            {
                IMClub.GroupWarp gw = IMClub.GoableClubDataInfo.AddMenbersToGroup(messageBack.menberList, messageBack.clubId);
				if (gw != null)
				{
					IMClub.GroupFunPanel.UpShow(!gw.isGetMenbers);

					gw.isGetMenbers = true;
				}
			}
            else
            {
                //失败之后取消获取状态
            }
        }
    }
}