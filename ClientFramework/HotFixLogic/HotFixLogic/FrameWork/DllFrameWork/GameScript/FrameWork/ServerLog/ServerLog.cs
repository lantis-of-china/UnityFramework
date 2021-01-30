using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FrameWork
{
	public static class ServerLog
	{
		public static Queue<KeyValuePair<string, string>> logQueue = new Queue<KeyValuePair<string, string>>();

		public static void Start()
		{
			if (string.IsNullOrEmpty(UpResourceManager.Instance.logServerAddress))
			{
				return;
			}

			DebugLoger.UpLog = UpLogcALL;

			new Thread(new ThreadStart(() => {

				while (true)
				{
					Thread.Sleep(50);

					UpLog();
				}
			})).Start();
		}

		public static void UpLogcALL(string logInfo)
		{
			if (GoableData.userValiadateInfor != null)
			{
				FrameWork.ServerLog.Insert(GoableData.userValiadateInfor.DatingNumber, logInfo);
			}
		}

		public static void Insert(string id, string log)
		{
			if (string.IsNullOrEmpty(UpResourceManager.Instance.logServerAddress))
			{
				return;
			}

			logQueue.Enqueue(new KeyValuePair<string, string>(id, log));
		}

		public static void UpLog()
		{
			while (logQueue.Count > 0 && UdpNetWork.HasInstance() && UdpNetWork.Instance.run)
			{
				KeyValuePair<string, string> log = logQueue.Dequeue();

				//Dictionary<string, string> upMap = new Dictionary<string, string>();
				//upMap.Add("playerId", log.Key);
				//upMap.Add("log", log.Value);
				//string msg = LitJson.JsonMapper.ToJson(upMap);

				Server.UserValiadateInfor user = new Server.UserValiadateInfor();
				user.DatingNumber = log.Key;
				user.ValidateGUID = log.Value;			 
				UdpNetWork.Instance.SendMessageUdp(UpResourceManager.Instance.logServerAddress, 9839, user.Serializer(), 10000090);					
				
				//HttpHelper.CreatePostHttpResponse(string.Format("http://{0}:9839", UpResourceManager.Instance.logServerAddress), msg, 1000, "",null);
				//HttpTools.PostHttpData(string.Format("http://{0}:9839", UpResourceManager.Instance.logServerAddress), System.Text.Encoding.UTF8.GetBytes(msg), UpBack);
			}
		}

		public static void UpBack(string result)
		{ }
	}
}
