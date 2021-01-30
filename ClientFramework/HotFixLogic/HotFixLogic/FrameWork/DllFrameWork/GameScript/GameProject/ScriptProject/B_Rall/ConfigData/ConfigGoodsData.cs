using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rall
{
	public class GoodsThingData
	{
		/// <summary>
		/// 键
		/// </summary>
		public string key;
		/// <summary>
		/// 物品类型 1-房卡 2-钻石
		/// </summary>
		public byte goodsType;
		/// <summary>
		/// 数量
		/// </summary>
		public int count;
		/// <summary>
		/// 价格
		/// </summary>
		public int price;
		/// <summary>
		/// 代理价格
		/// </summary>
		public int agentPrice;
	}


	public class ConfigGoodsData : ConfigDataBase
	{
		public GoodsThingData currentData;

		public System.Collections.Generic.Dictionary<string, GoodsThingData> dataList = new System.Collections.Generic.Dictionary<string, GoodsThingData>();

		public ConfigGoodsData(string _projectName, string _configName)
			: base(_projectName, _configName)
		{
		}

		public override void CreateData(string _key)
		{
			currentData = new GoodsThingData();

			currentData.key = _key;

			dataList.Add(_key, currentData);
		}

		public override void AppendAttribute(string _key, string _name, string _value)
		{
			switch (_name)
			{
				case "Key":
					currentData.key = _value;
					break;
				case "GoodsType":
					currentData.goodsType = byte.Parse(_value);
					break;
				case "Count":
					currentData.count = int.Parse(_value);
					break;
				case "Price":
					currentData.price = int.Parse(_value);
					break;
				case "AgentPrice":
					currentData.agentPrice = int.Parse(_value);
					break;
				default:
					break;
			}
		}

		public GoodsThingData GetKey(string _key)
		{
			GoodsThingData _outData = null;

			dataList.TryGetValue(_key, out _outData);

			return _outData;
		}
	}
}