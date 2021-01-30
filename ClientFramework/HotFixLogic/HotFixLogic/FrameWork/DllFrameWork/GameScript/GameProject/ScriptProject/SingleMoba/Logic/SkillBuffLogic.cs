using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	public class SkillBuffLogic
	{
		private static List<SkillBuffLogic> skillBuffLogicList = new List<SkillBuffLogic>();

		public static void AddSkillBuff(SC_AddSkillBuff addSkillBuff)
		{
			for (var i = 0; i < addSkillBuff.getBuffs.Count; ++i)
			{
				AddSkillBuff(addSkillBuff.playerId, addSkillBuff.getBuffs[i]);
			}
		}

		public static void AddSkillBuffs(List<P_SkillBuff> skillBuffs)
		{
			for (var i = 0; i < skillBuffs.Count; ++i)
			{
				var skillBuff = skillBuffs[i];
				AddSkillBuff(skillBuff.bindUserId, skillBuff);
			}
		}

		public static void AddSkillBuff(int playerId,P_SkillBuff setSkillBuffInfo)
		{
			var buffCharacter = SingleMoba.CharacterManager.Instance.GetCharacter(playerId);

			if (buffCharacter != null)
			{
				var skillBuffLogic = new SkillBuffLogic();
				skillBuffLogic.SetBind(buffCharacter, setSkillBuffInfo);
				skillBuffLogicList.Add(skillBuffLogic);
			}
		}

		public static void Clear()
		{
			for (var i = 0; i < skillBuffLogicList.Count; ++i)
			{
				skillBuffLogicList[i].Dispose();
			}

			skillBuffLogicList.Clear();
		}

		public static void RemoveSkillBuff(SC_RemoveSkillBuff removeSkillBuff)
		{
			if (removeSkillBuff.buffIds != null)
			{
				for (var i = 0; i < removeSkillBuff.buffIds.Count; ++i)
				{
					var buffId = removeSkillBuff.buffIds[i];
					var skillBuffLogic = GetBuffLogic(buffId);
					RemoveSkillBuff(buffId);

					if (skillBuffLogic != null)
					{
						skillBuffLogic.Dispose();
					}
				}
			}
		}

		public static void RemoveSkillBuff(int id)
		{
			for (var i = 0; i < skillBuffLogicList.Count; ++i)
			{
				if (skillBuffLogicList[i].skillBuffInfo.id == id)
				{
					skillBuffLogicList.RemoveAt(i);
					break;
				}
			}
		}

		public static SkillBuffLogic GetBuffLogic(int skillId)
		{
			for (var i = 0; i < skillBuffLogicList.Count; ++i)
			{
				if (skillBuffLogicList[i].skillBuffInfo.id == skillId)
				{
					return skillBuffLogicList[i];
				}
			}

			return null;
		}

		public static void SkillBuffTrigger(SC_SkillBuffTrigger skillBuffTrigger)
		{
			var character = CharacterManager.Instance.GetCharacter(skillBuffTrigger.playerId);

			if (character != null)
			{
				var skillBuffLogic = GetBuffLogic(skillBuffTrigger.buffId);

				if (skillBuffLogic != null)
				{
					switch ((BuffEventType)skillBuffTrigger.buffEventType)
					{
						case BuffEventType.AddHp:
						{
							character.SetValueChange(ChactarPropyteType.Hp, skillBuffTrigger.paramar1, skillBuffTrigger.paramar2);
							break;
						}
						case BuffEventType.Damage:
						{
							character.SetValueChange(ChactarPropyteType.Hp, skillBuffTrigger.paramar1, skillBuffTrigger.paramar2);
							character.PlayHit();
							break;
						}
						case BuffEventType.TimeRangGetBuff:
						{
							break;
						}
					}
				}
			}
		}

		public static void FixedUpdate()
		{
			for (var i = 0; i < skillBuffLogicList.Count; ++i)
			{
				skillBuffLogicList[i].FixedUpdateLogic();
			}
		}

		public CharacterBase characterBase;
		public P_SkillBuff skillBuffInfo;
		public ConfigSkillBuffData skillBuffConfigData;
		public GameObject item;

		public void SetBind(CharacterBase setCharacter, P_SkillBuff setSkillBuffInfo)
		{
			characterBase = setCharacter;
			skillBuffInfo = setSkillBuffInfo;
			skillBuffConfigData = LocalConfigLoader.configSkillBuff.GetKey(skillBuffInfo.cgfId);

			if (skillBuffConfigData != null)
			{
				if (!string.IsNullOrEmpty(skillBuffConfigData.item))
				{
					item = LoadPrefab.SpawnFightSkill(skillBuffConfigData.item);
					var singleMoba = FrameWorkDrvice.WorldManagerInstance.currentWorld as WorldSpace.SingleMobaFightWorld;

					if (singleMoba != null)
					{
						if (singleMoba.propNode != null)
						{
							item.transform.SetParent(singleMoba.propNode.transform);
							item.transform.localScale = Vector3.one;
							SetBuffWithCharacter();
						}
					}
				}
			}
		}

		public void SetBuffWithCharacter()
		{
			if (characterBase != null && item != null)
			{
				item.transform.localPosition = characterBase.GetPos();
			}
		}

		public void FixedUpdateLogic()
		{
			SetBuffWithCharacter();
		}

		public void Dispose()
		{
			if (item != null)
			{
				LoadPrefab.DespawnFightSkill(item);
				item = null;
			}
		}
	}
}
