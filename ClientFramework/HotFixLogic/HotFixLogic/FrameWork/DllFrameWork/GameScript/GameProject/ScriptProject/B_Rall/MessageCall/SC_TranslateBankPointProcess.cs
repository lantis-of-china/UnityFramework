using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{    
    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class SC_TranslateBankPointProcess : ProcessMessageBase
    {
        public SC_TranslateBankPointProcess()
        {
            ID = (int)NetMessageType.GameWithUser_TranslateBankPoint_SC;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new SC_TranslateBankPointProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            Server.SC_BankTranlate entryMsg = new Server.SC_BankTranlate();
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
                GoableData.userValiadateInforWarp.Gold = entryMsg.goldCount;
                GoableData.userValiadateInforWarp.RechargeCount = entryMsg.rechargeCount;
                UINameSpace.UIBank.BankTask.ShowBankTask(GoableData.userValiadateInforWarp.Gold, GoableData.userValiadateInforWarp.RechargeCount, entryMsg.goldBank, entryMsg.rechargeBank);
                UIObject uiRall = FrameWorkDrvice.UiManagerInstance.GetUI(Rall.UIDefineName.UIRall_Rall);

                if(uiRall != null)
                {
                    (uiRall as UINameSpace.UIRall).RefenceRoomKa();
                }
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("操作异常!");
            }
        }
    }
}