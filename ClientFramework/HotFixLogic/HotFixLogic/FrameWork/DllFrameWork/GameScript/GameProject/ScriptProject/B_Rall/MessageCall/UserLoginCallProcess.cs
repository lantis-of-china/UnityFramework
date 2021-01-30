using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordProcess
{
    /// <summary>
    /// 用户登录服务器返回 结果 包括游戏服务器信息
    /// </summary>
    public class UserLoginCallProcess : ProcessMessageBase
    {
        public UserLoginCallProcess()
        {
            ID = (int)NetMessageType.LoginWithUser_Login_Back;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new UserLoginCallProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.MessageUserLoginBack userLogin = new Server.MessageUserLoginBack();
            try
            {
                userLogin.Deserializer(DateBuf, 0);
            }
            catch(Exception e)
            {
                DebugLoger.LogError("消息异常-------------- " + e.ToString());
                return;
            }

            if (userLogin.LoginState == 1)//成功  可以刷新选择服务器
            {
                GoableData.userValiadateInfor = userLogin.UserValiadate;
                GoableData.userValiadateInforWarp = userLogin.UserValiadateWarp;

                GoableData.ServerIpaddress.mServerData = userLogin.ChatServerList;

                //UIObject _uiObj = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UILogin);

                //if (_uiObj != null)
                //{
                //    UINameSpace.UILogin _uiLogine = (_uiObj as UINameSpace.UILogin);

                //    _uiLogine.ShowServersList(userLogin.ChatServerList);
                //}


                if (userLogin.UserValiadateWarp != null)
                {
                    if (userLogin.UserValiadateWarp.Sex == 3)
                    {
                        //需要创建角色
                        FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UICreate_Rall, true);
                    }
                    else
                    {
                        //开启大厅
                        FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRall_Rall, true);
                    }
                }
                else
                {
                    UINameSpace.UITipMessage.PlayMessage("没有用户数据!");
                }
            }
            else
            {
                ///失败原因
                DebugLoger.LogError("Login Failed");
            }

        }
    }
}
