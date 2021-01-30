using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rall
{
	public class LogicDataSpace
	{
		/// <summary>
		/// 商品配置表
		/// </summary>
		public static ConfigGoodsData configGoodsData;

		public static void Load()
		{
			configGoodsData = new ConfigGoodsData(ConfigProject.projectFloderName, "data_goodsCompare");
		}

		/// <summary>
		/// 清理
		/// </summary>
		public static void Clear()
		{
			if (configGoodsData != null)
			{
				configGoodsData.ReleseData();
				configGoodsData = null;
			}
		}
	}
}
