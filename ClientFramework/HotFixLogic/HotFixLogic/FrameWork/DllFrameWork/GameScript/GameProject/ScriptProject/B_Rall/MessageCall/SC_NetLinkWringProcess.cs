using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_NetLinkWring : ProcessMessageBase
    {
        public SC_NetLinkWring()
        {
            ID = (int)NetMessageType.GameWithUser_NetWaring_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_NetLinkWring();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_NetLinkWring userEntry = new Server.SC_NetLinkWring();
            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			DebugLoger.LogError("网络异常消息 关闭套接字 --------------------------vuserEntry.state " + userEntry.state);
            if(UserNetWork.HasInstance())
            {
                DebugLoger.LogError("网络异常消息 关闭套接字");
                UserNetWork.Instance.CloseSocket();
            }

            GoableData.ClearnData();

            ///1 断开连接 2 心跳超时断开连接 3 其他地方登陆 4 退出登陆
            if(userEntry.state == 1)
            {
                GoableData.ServerIpaddress.SetReconnect();
                FrameWorkDrvice.UiManagerInstance.CloseAllUI();
                //无响应 退出到登录界面 需要重连
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName,Rall.UIDefineName.UILogin_Rall, true);
            }
            else if (userEntry.state == 2)
            {
                GoableData.ServerIpaddress.SetReconnect();
                FrameWorkDrvice.UiManagerInstance.CloseAllUI();
                //心跳超时 断开连接
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
            }
            else if (userEntry.state == 3)
            {              
                FrameWorkDrvice.UiManagerInstance.CloseAllUI();
                //其他地方登录 退出到登录界面
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
            }
            else if (userEntry.state == 4)
            {
                FrameWorkDrvice.UiManagerInstance.CloseAllUI();
                //回到登录界面
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
            }
            else if (userEntry.state == 5)
            {
                //本地网络异常断开连接
                FrameWorkDrvice.UiManagerInstance.CloseAllUI();
                //回到登录界面
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UILogin_Rall, true);
            }
        }
    }
}