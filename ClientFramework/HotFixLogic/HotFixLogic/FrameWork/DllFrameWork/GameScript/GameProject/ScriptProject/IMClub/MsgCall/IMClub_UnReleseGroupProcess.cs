using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

	/// <summary>
	/// 解散亲友圈
	/// </summary>
	public class IMClub_UnReleseGroupProcess : ProcessMessageBase
	{
		public IMClub_UnReleseGroupProcess()
		{
			ID = (int)IMClub.NetMessageType.SC_UnReleseClub_MsgType;
		}

		public static ProcessMessageBase _Instance;

		public static ProcessMessageBase GetProcessType()
		{
			if (_Instance == null)
			{
				_Instance = new IMClub_UnReleseGroupProcess();
			}
			return _Instance;
		}

		//处理方法
		public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
		{
			UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.SC_UnReleseClub_MsgType");
			IMClub.SC_UnReleseClub messageBack = new IMClub.SC_UnReleseClub();
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