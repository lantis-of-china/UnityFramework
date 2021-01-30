using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_CheckLockProcess : ProcessMessageBase
    {
        public SC_CheckLockProcess()
        {
            ID = (int)NetMessageType.LoginWithUser_CheckLock_Back;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_CheckLockProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting(NetMessageType.LoginWithUser_CheckLock_Back.ToString());

            Server.SC_CheckGameLock userEntry = new Server.SC_CheckGameLock();
            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			if (userEntry.state == 1)//成功
            {
                UIObject targetObj = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIRall_Rall);
                if (targetObj != null)
                {
                    UINameSpace.UIRall uiRall = targetObj as UINameSpace.UIRall;
                    if (uiRall != null)
                    {
                        uiRall.OpenGame(userEntry.id);
                    }
                }
                else
                {
                    UINameSpace.UITipMessage.PlayMessage("不在大厅中!");
                }
            }
            else 
            {
                UINameSpace.UITipMessage.PlayMessage("当前正在结算中,请稍后再试!");
            }
        }
    }
}