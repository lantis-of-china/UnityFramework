using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_LoginBankProcess : ProcessMessageBase
    {
        public SC_LoginBankProcess()
        {
            ID = (int)NetMessageType.GameWithUser_LoginBank_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_LoginBankProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_ValiedBank entryMsg = new Server.SC_ValiedBank();
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
                UINameSpace.UIBank.BankTask.ShowBankTask(GoableData.userValiadateInforWarp.Gold, GoableData.userValiadateInforWarp.RechargeCount, entryMsg.goldBank, entryMsg.rechargeBank);
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("密码错误无法进入银行系统!");
            }
        }
    }
}