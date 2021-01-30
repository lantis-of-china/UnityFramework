using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_ChangeBankPasswordProcess : ProcessMessageBase
    {
        public SC_ChangeBankPasswordProcess()
        {
            ID = (int)NetMessageType.GameWithUser_ChangeBankPassword_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_ChangeBankPasswordProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_ChangeBankPassword entryMsg = new Server.SC_ChangeBankPassword();
            try
            {
                entryMsg.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常-------------- " + e.ToString());
				return;
			}

			if (entryMsg.result == 1)
            {
                UINameSpace.UIBank.ChangePass.Close();
                UINameSpace.UIBank.BankTask.Open();
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("修改错误!");
            }
        }
    }
}