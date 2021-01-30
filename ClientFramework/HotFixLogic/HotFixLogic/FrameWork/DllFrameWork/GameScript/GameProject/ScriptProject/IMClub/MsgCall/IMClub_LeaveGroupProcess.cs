using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{
	/// <summary>
	/// 离开亲友圈
	/// </summary>
	public class IMClub_LeaveGroupProcess : ProcessMessageBase
	{
		public IMClub_LeaveGroupProcess()
		{
			ID = (int)IMClub.NetMessageType.SC_LeaveClub_MsgType;
		}

		public static ProcessMessageBase _Instance;

		public static ProcessMessageBase GetProcessType()
		{
			if (_Instance == null)
			{
				_Instance = new IMClub_LeaveGroupProcess();
			}
			return _Instance;
		}

		//处理方法
		public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
		{
			UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_LeaveClub_MsgType");
			IMClub.SC_LeaveClub messageBack = new IMClub.SC_LeaveClub();
			try
			{
				messageBack.Deserializer(DateBuf, 0);
			}
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == 0)
			{
				UINameSpace.UITipMessage.PlayMessage("操作失败!");
			}
		}
	}
}