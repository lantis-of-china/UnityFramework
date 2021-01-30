using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleMoba
{
	/// <summary>
	/// Buff类型
	/// </summary>
	public enum BuffEventType : int
	{
		AddHp = 10000,
		Damage = 10100,
		TimeRangGetBuff = 10200,
		AttackDamage = 200000,
	}

	/// <summary>
	/// 设置主动类型判断
	/// </summary>
	public class BuffEventTypeTools
	{
		private static List<int> triggerBuffTypeList = new List<int>()
		{
			(int)BuffEventType.AddHp,
			(int)BuffEventType.Damage,
			(int)BuffEventType.TimeRangGetBuff
		};

		public static bool IsTriggerBuffType(int buffType)
		{
			return triggerBuffTypeList.Contains(buffType);
		}
	}
}