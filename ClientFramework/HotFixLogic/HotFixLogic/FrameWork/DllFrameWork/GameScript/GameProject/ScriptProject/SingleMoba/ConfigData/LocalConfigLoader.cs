using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleMoba
{
	public class LocalConfigLoader
	{
		public static ConfigSkill configSkill;
		public static ConfigSkillBuff configSkillBuff;

		public static void Load()
		{
			configSkill = new ConfigSkill(ConfigProject.projectFloderName, "data_skill");
			configSkillBuff = new ConfigSkillBuff(ConfigProject.projectFloderName, "data_skillBuff");
		}
	}
}
