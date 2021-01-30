using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	public class SkillLogic
	{
		private static List<SkillLogic> skillLogicList = new List<SkillLogic>();
		public static void UseSkill(SC_UseSkill useSkill)
		{
			var character = SingleMoba.CharacterManager.Instance.GetCharacter(useSkill.playerId);

			if (character != null)
			{
				var position = character.GetPos();
				AddSkill(useSkill.skill, position, useSkill.targetX, useSkill.targetY);

				if (useSkill.skill.cgfId == 10001)
				{
					character.PlaySkill1();
				}
				else if (useSkill.skill.cgfId == 10002)
				{
					character.PlaySkill2();
				}
				else if (useSkill.skill.cgfId == 10003)
				{
					character.PlaySkill3();
				}
			}

			if (useSkill.gamerChanges != null)
			{
				for (var i = 0; i < useSkill.gamerChanges.Count; ++i)
				{
					var changeData = useSkill.gamerChanges[i];
					var characterChange = SingleMoba.CharacterManager.Instance.GetCharacter(changeData.playerId);

					if (characterChange != null)
					{
						character.SetDataChange(changeData);
					}
				}
			}
		}

		public static void SkillHit(SC_HitSkill hitSkill)
		{
			var skillLogic = GetSkill(hitSkill.skillId);

			if (skillLogic != null)
			{
				var hitCharacter = SingleMoba.CharacterManager.Instance.GetCharacter(hitSkill.hitPlayerId);

				if (hitCharacter != null)
				{
					hitCharacter.PlayHit();
				}
			}
		}

		public static void SkillEnd(SC_SkillEnd skillEnd)
		{
			var skillLogic = GetSkill(skillEnd.skillId);

			if (skillLogic != null)
			{
				skillLogic.isEndTake = true;

				if (!string.IsNullOrEmpty(skillLogic.skillConfigData.endTakeEffect))
				{
					EffectLogic.AddEffect(skillLogic.skillConfigData.endTakeEffect, 2.0f, skillLogic.item);
				}
			}
		}

		public static void SkillRemove(SC_RemoveSkill skillRemove)
		{
			var skillLogic = GetSkill(skillRemove.skillId);

			if (skillLogic != null)
			{
				RemoveSkill(skillRemove.skillId);

				skillLogic.Dispose();
			}
			else
			{

			}
		}

		public static SkillLogic GetSkill(int skillId)
		{
			for (var i = 0; i < skillLogicList.Count; ++i)
			{
				if (skillLogicList[i].skillInfo.id == skillId)
				{
					return skillLogicList[i];
				}
			}

			return null;
		}

		public static void RemoveSkill(int skillId)
		{
			for (var i = 0; i < skillLogicList.Count; ++i)
			{
				if (skillLogicList[i].skillInfo.id == skillId)
				{
					skillLogicList.RemoveAt(i);
					break;
				}
			}
		}

		public static void AddSkills(List<P_Skill> skills)
		{
			for (var i = 0; i < skills.Count; ++i)
			{
				var skill = skills[i];
				AddSkill(skill,new Vector3(skill.startX,0,skill.startY),skill.targetX,skill.targetY);
			}
		}

		public static void AddSkill(P_Skill skill,Vector3 fromPos,float toPosX,float toPosZ)
		{
			var logic = new SkillLogic();
			logic.skillInfo = skill;
			//logic.skillInfo.beginTime = GoableData.GetServerNowTime();
			logic.startPos = fromPos;
			logic.targetPos = new Vector3(toPosX, 0, toPosZ);
			logic.skillConfigData = LocalConfigLoader.configSkill.GetKey(skill.cgfId);

			if (logic.skillConfigData != null)
			{

				if (logic.skillConfigData.itemType == 0)
				{
					if (!string.IsNullOrEmpty(logic.skillConfigData.item))
					{
						logic.item = LoadPrefab.SpawnFightSkill(logic.skillConfigData.item);
						var singleMoba = FrameWorkDrvice.WorldManagerInstance.currentWorld as WorldSpace.SingleMobaFightWorld;

						if (singleMoba != null)
						{
							if (singleMoba.propNode != null)
							{
								logic.item.transform.SetParent(singleMoba.propNode.transform);
								logic.item.transform.localScale = Vector3.one;
								logic.item.transform.localPosition = fromPos;
								logic.Set3DPos();
								logic.FixedUpdateMove();
							}
						}
					}
				}
				else if(logic.skillConfigData.itemType == 1)
				{
					var bindCharacter = CharacterManager.Instance.GetCharacter(logic.skillInfo.bindUserId);

					if (bindCharacter != null)
					{
						logic.item = bindCharacter.GetNode();
					}
				}
			}

			skillLogicList.Add(logic);
		}

		public static void FixedUpdate()
		{
			for (var i = 0; i < skillLogicList.Count; ++i)
			{
				skillLogicList[i].FixedUpdateMove();
			}
		}

		public static void JobIndex(object paramars,int index)
		{
			var datas = (List<SkillLogic>)(paramars);
		}


		public static void Clear()
		{
			for (var i = 0; i < skillLogicList.Count; ++i)
			{
				skillLogicList[i].Dispose();
			}

			skillLogicList.Clear();
		}

		public ConfigSkillData skillConfigData;
		public P_Skill skillInfo;
		public GameObject item;
		public Vector3 startPos;
		public Vector3 targetPos;
		private List<int> hitPlayerIdList = new List<int>();
		private bool isEndTake = false;

		public void Execute()
		{ 
		}

		public void FixedUpdateMove()
		{
			if (isEndTake)
			{
				return;
			}

			if (skillConfigData != null)
			{
				if (skillConfigData.moveType == 1 || skillConfigData.moveType == 2)
				{
					var sencend = (GoableData.GetServerNowTime() - skillInfo.beginTime) / 10000000.0f;

					item.transform.forward = (targetPos - startPos).normalized;

					if (sencend < skillConfigData.lifeTime)
					{
						item.transform.localPosition = (targetPos - startPos).normalized * sencend / skillConfigData.lifeTime * Vector3.Distance(startPos, targetPos) + startPos;
						Set3DPos();
					}
					else
					{
						item.transform.localPosition = targetPos;
						Set3DPos();
					}
				}
			}

			//var mainCharacter = SingleMoba.CharacterManager.Instance.GetMainCharacter();

			//if (skillInfo != null && mainCharacter != null && skillInfo.bindUserId == mainCharacter.GetPlayerData().playerId)
			//{
			//	CheckHit();
			//}
		}

		public void CheckHit()
		{
			var characterAll = SingleMoba.CharacterManager.Instance.GetCharacterAll();

			for (var i = 0; i < characterAll.Count; ++i)
			{
				var character = characterAll[i];

				if (IsHit(character) && !HasHitPlayer(character.GetPlayerData().playerId))
				{
					AddHitPlayer(character.GetPlayerData().playerId);
					MessageSend.HitSkill(skillInfo.id, character.GetPlayerData().playerId, item.transform.localPosition.x, item.transform.localPosition.z);
				}
			}
		}

		public bool IsHit(CharacterBase character)
		{
			if (skillConfigData != null)
			{
				bool testHit = false;

				if (skillConfigData.moveHitTeam == 0)
				{
					testHit = true;
				}
				else if (skillConfigData.moveHitTeam == 1)
				{
					if (character.GetPlayerData()?.site != CharacterManager.Instance.GetMainCharacter()?.GetPlayerData()?.site)
					{
						testHit = true;
					}
				}
				else if (skillConfigData.moveHitTeam == 2)
				{
					if (character.GetPlayerData()?.site == CharacterManager.Instance.GetMainCharacter()?.GetPlayerData()?.site)
					{
						testHit = true;
					}
				}

				if (testHit && skillConfigData.moveHitType == 1)
				{
					if (Vector3.Distance(character.GetPos(), item.transform.localPosition) <= skillConfigData.moveHitRange)
					{
						return true;
					}
				}
			}

			return false;
		}

		public bool HasHitPlayer(int playerId)
		{
			return hitPlayerIdList.Contains(playerId);
		}

		public void AddHitPlayer(int playerId)
		{
			hitPlayerIdList.Add(playerId);
		}

		public void Set3DPos()
		{
			if (ConfigProject.is3d)
			{
				if (BoxMap.Instance != null)
				{
					item.transform.localPosition = new Vector3(item.transform.localPosition.x, BoxMap.Instance.GetBoxMapNodeHeight(item.transform.localPosition.x, item.transform.localPosition.z), item.transform.localPosition.z);
				}
			}
		}

		public void Dispose()
		{
			if (item != null)
			{
				if (skillConfigData.itemType == 0)
				{
					LoadPrefab.DespawnFightSkill(item);
				}
				else if (skillConfigData.itemType == 1)
				{
					//这里的item是角色
				}
			}
		}
	}
}
