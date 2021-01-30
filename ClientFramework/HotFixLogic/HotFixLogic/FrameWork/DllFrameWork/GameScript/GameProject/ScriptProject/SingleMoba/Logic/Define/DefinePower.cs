using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleMoba
{
	public class DefineConfig
	{
		//0-能量类型 1-技能类型
		private static Dictionary<int,DefineConfig> defineList = new Dictionary<int,DefineConfig>()
		{
			{ 0 ,new DefineConfig{ cfgId = 0, type = 0, assetName = "powerwhite"} },
			{ 1, new DefineConfig{ cfgId = 1, type = 0, assetName = "powerred"} },
			{ 2, new DefineConfig{ cfgId = 2, type = 0, assetName = "powerorange"} },
			{ 3, new DefineConfig{ cfgId = 3, type = 0, assetName = "poweryellow"} },
			{ 4, new DefineConfig{ cfgId = 4, type = 0, assetName = "powergreen"} },
			{ 5, new DefineConfig{ cfgId = 5, type = 0, assetName = "powercyan"} },
			{ 6, new DefineConfig{ cfgId = 6, type = 0, assetName = "powerblue"} },
			{ 7, new DefineConfig{ cfgId = 7, type = 0, assetName = "powerblack"} },
			{ 8, new DefineConfig{ cfgId = 8, type = 1, assetName = "propskill"} },
		};

		public static Dictionary<int, DefineConfig> GetDatas()
		{
			return defineList;
		}

		public static DefineConfig GetConfig(int key)
		{
			if (defineList.ContainsKey(key))
			{
				return defineList[key];
			}

			return null;
		}

		public int cfgId;
		public int type;
		public string assetName;
	}
}
