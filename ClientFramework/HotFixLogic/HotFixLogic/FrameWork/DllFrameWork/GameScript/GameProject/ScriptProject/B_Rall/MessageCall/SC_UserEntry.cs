using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_UserEntry : ProcessMessageBase
    {
        public SC_UserEntry()
        {
            ID = (int)NetMessageType.ChatWithUser_Login_Back;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_UserEntry();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            GoableData.ServerIpaddress.isLoginGameLogic = true;
            UINameSpace.UIWaitting.RemoveShowWaitting("weiChatAuth");
            UINameSpace.UIWaitting.RemoveShowWaitting(((int)NetMessageType.GameWithUser_Create_Back).ToString());
            //Server.UserEntryBack userEntry = Server.NetWork.GameSerialzTool.Desrializer<Server.UserEntryBack>(DateBuf);
            Server.UserEntryBack userEntry = new Server.UserEntryBack();
            try
            {
                userEntry.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}
			if (userEntry.ResaultState == 1)//成功
            {
                GoableData.OpenHeart();
                GoableData.gameCoreData = userEntry._gameCoreData;
                GoableData.conditionList = userEntry.conditionList;
                FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UILogin_Rall, eCloseType.Queue);
                FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UICreate_Rall, eCloseType.None);

                if(GoableData.gameCoreData._roleInfor.openHiddent == 1)
                {
                    //开启
                    StringConfigClass.canOpenHiddent = true;
                }
                else
                {
                    //不开启
                    StringConfigClass.canOpenHiddent = false;
                }

                DebugLoger.Log("userEntry.serverId  " + userEntry.serverId);

                GameEntryItem gameEntry = FrameWorkDrvice.GameEntryManagerInstanece.GetGameEntry(userEntry.serverId);
                gameEntry.Install();
                if (!gameEntry.isGernerlRall)
                {
                    if (!string.IsNullOrEmpty(gameEntry.uiName))
                    {
                        FrameWorkDrvice.UiManagerInstance.OpenUI(gameEntry.assetFloder, gameEntry.uiName, true);
                    }
                }
                else
                {
                    UINameSpace.UIGeneralRall.curEntryItem = gameEntry;
                    FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIGeneralRall_Rall, true);
                }
            }
            else if (userEntry.ResaultState == 3)//创建角色
            {
                GoableData.OpenHeart();
                FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UILogin_Rall, eCloseType.Queue);
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UICreate_Rall, true);
            }
            else
            {
                ///失败原因
                DebugLoger.LogError("Login Failed " + userEntry.ResaultState);
            }

        }
    }
}