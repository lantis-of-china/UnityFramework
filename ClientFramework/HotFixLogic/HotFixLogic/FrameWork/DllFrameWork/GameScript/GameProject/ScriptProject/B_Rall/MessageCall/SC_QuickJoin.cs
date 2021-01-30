using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_QuickJoin : ProcessMessageBase
    {
        public SC_QuickJoin()
        {
            ID = (int)NetMessageType.GameWithUser_QuickJoin_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_QuickJoin();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("QuickJame");
            //Server.UserEntryBack userEntry = Server.NetWork.GameSerialzTool.Desrializer<Server.UserEntryBack>(DateBuf);
            Server.SC_QuickJoinGame userEntry = new Server.SC_QuickJoinGame();
            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			DebugLoger.Log("userEntry.result " + userEntry.result);
			if (userEntry.result == 1)//成功
			{
				GameEntryItem gameEntryItem = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntry(userEntry.serverId);
				if (gameEntryItem != null)
				{
					GoableData.reconnectExternUIName = gameEntryItem.uiName;
				}
				GoableData.ServerIpaddress.readyEntryRoomId = userEntry.roomId;
				GoableData.ServerIpaddress.clubId = userEntry.clubId;
				UINameSpace.UIRall.CheckCanOpen(userEntry.serverId);
			}
			else if (userEntry.result == 0)
			{
				UINameSpace.UITipMessage.PlayMessage("加入失败!");
			}
			else if (userEntry.result == 2)
			{
				UINameSpace.UITipMessage.PlayMessage("未加入对应亲友圈,无法进入房间!");
			}
			else if (userEntry.result == 3)
			{
				UINameSpace.UITipMessage.PlayMessage("人数已满!");
			}
			else if (userEntry.result == 4)
			{
				UINameSpace.UITipMessage.PlayMessage("房间不存在!");
			}
			else if (userEntry.result == 5)
			{
				//竞技分不足
				UINameSpace.UITipMessage.PlayMessage("加入失败!");
			}
		}
    }
}