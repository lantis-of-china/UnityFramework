using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_CreateGroupProcess : ProcessMessageBase
    {
        public IMClub_CreateGroupProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_CreateGroup_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_CreateGroupProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_CreateGroup_MsgType");
            IMClub.SC_CreateGroup messageBack = new IMClub.SC_CreateGroup();

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
				//这里创建一个亲友圈成功了 存到数据里面去
				IMClub.GoableClubDataInfo.AddGroup(messageBack.groupInfo);
				IMClub.ClubListPanel.UpdateClubList();
			}
			else if (messageBack.result == (byte)0)
			{
				UINameSpace.UITipMessage.PlayMessage("创建失败,请先申请代理!");
			}
			else if (messageBack.result == (byte)2)
			{
				UINameSpace.UITipMessage.PlayMessage("创建失败,是否已经创建2个以上亲友圈!");
			}
			else
			{
				UINameSpace.UITipMessage.PlayMessage("创建失败,系统错误!");
			}
        }
    }
}