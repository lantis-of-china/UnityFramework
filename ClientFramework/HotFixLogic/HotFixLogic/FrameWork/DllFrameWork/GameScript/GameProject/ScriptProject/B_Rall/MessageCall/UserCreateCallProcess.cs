using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordProcess
{
    /// <summary>
    /// 用户创建角色返回
    /// </summary>
    public class UserCreateCallProcess : ProcessMessageBase
    {
        public UserCreateCallProcess()
        {
            ID = (int)NetMessageType.GameWithUser_Create_Back;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new UserCreateCallProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            //Server.CreateRoleBack entryMsg = Server.NetWork.GameSerialzTool.Desrializer<Server.CreateRoleBack>(DateBuf);
            Server.CreateRoleBack entryMsg = new Server.CreateRoleBack();
            try
            {
                entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}
			UIObject objUi = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UICreate_Rall);
            if (objUi != null)
            {
                UINameSpace.UICreate createUi = objUi as UINameSpace.UICreate;
                createUi.TipShow(entryMsg.state);
            }
        }
    }
}