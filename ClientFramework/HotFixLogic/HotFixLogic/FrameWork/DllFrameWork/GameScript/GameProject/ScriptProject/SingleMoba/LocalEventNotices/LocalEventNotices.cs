using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleMoba
{
	public class EventNoticesDefine
	{
		public const string PlayerStateChange = "PlayerStateChange";
		public const string ChactarPropyteType = "ChactarPropyteType";
		public const string ChactarJoin = "ChactarJoin";
	}

	public class LocalEventNotices
	{
		public static void Regist(string key, Action<object> action)
		{
			FrameWorkDrvice.EventNoticesInstance.Regist(ConfigProject.projectFloderName, key,action);
		}

		public static void UnRegist(string key, Action<object> action)
		{
			FrameWorkDrvice.EventNoticesInstance.UnRegist(ConfigProject.projectFloderName, key, action);
		}

		public static void Notices(string key, object paramar)
		{
			FrameWorkDrvice.EventNoticesInstance.Notices(ConfigProject.projectFloderName, key, paramar);
		}
	}
}
