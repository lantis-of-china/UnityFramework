using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	public class EventNoticesData
	{
		public string project;
		public Dictionary<string, List<Action<object>>> eventAll = new Dictionary<string, List<Action<object>>>();

		public void ClearAll()
		{
			eventAll.Clear();
		}

		public void ClearKey(string key)
		{
			if (eventAll.ContainsKey(key))
			{
				eventAll[key].Clear();
			}
		}

		public void Regist(string key, Action<object> call)
		{
			List<Action<object>> noticesCalls = null;

			if (eventAll.ContainsKey(key))
			{
				noticesCalls = eventAll[key];
				return;
			}
			else
			{
				noticesCalls = new List<Action<object>>();
				eventAll.Add(key, noticesCalls);
			}

			noticesCalls.Add(call);
		}

		public void UnRegist(string key, Action<object> call)
		{
			if (eventAll.ContainsKey(key))
			{
				var noticesCalls = eventAll[key];

				for (var i = 0; i < noticesCalls.Count; ++i)
				{
					if (noticesCalls[i] == call)
					{
						noticesCalls.RemoveAt(i);
						break;
					}
				}
			}
		}

		public void Notices(string key, object paramar)
		{
			if (eventAll.ContainsKey(key))
			{
				var noticesCalls = eventAll[key];

				for (var i = 0; i < noticesCalls.Count; ++i)
				{
					noticesCalls[i](paramar);
				}
			}
		}
	}
	public class EventNotices
	{
		private Dictionary<string, EventNoticesData> eventNoticesData = new Dictionary<string, EventNoticesData>();

		public void ClearAll()
		{
			foreach (var kv in eventNoticesData)
			{
				kv.Value.ClearAll();
			}

			eventNoticesData.Clear();
		}

		public void Regist(string project, string key, Action<object> action)
		{
			EventNoticesData noticesData = null;

			if (eventNoticesData.ContainsKey(project))
			{
				noticesData = eventNoticesData[project];
			}
			else
			{
				noticesData = new EventNoticesData
				{
					project = project
				};

				eventNoticesData.Add(project, noticesData);
			}

			noticesData.Regist(key, action);
		}

		public void UnRegist(string project, string key, Action<object> action)
		{
			if (eventNoticesData.ContainsKey(project))
			{
				var noticesData = eventNoticesData[project];

				noticesData.UnRegist(key, action);
			}
		}

		public void Notices(string project,string key,object paramar)
		{
			if (eventNoticesData.ContainsKey(project))
			{
				var noticesData = eventNoticesData[project];

				noticesData.Notices(key, paramar);
			}
		}
	}

