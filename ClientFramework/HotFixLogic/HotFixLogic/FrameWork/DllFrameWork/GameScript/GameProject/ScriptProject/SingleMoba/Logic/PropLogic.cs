using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	public class PropLogic
	{
		private const float checkTimeMax = 0.3f;
		private static float checkInvateTime = 0;
		private static float eatDistance = 2.5f;
		private static List<int> currentCheckKeys;
		public static List<int> eatPropList = new List<int>();
		public static LantisDictronaryList<int, SpawnLogic> datas = new LantisDictronaryList<int, SpawnLogic>();
		public static Dictionary<int, Dictionary<int, LantisDictronaryList<int, List<SpawnLogic>>>> lantisDatas = new Dictionary<int, Dictionary<int, LantisDictronaryList<int, List<SpawnLogic>>>>();

		public static void AddProps(List<P_Prop> props)
		{
			for (var i = 0; i < props.Count; ++i)
			{
				var prop = props[i];
				AddProp(prop);
			}
		}

		public static void AddProp(P_Prop prop)
		{
			var data = DefineConfig.GetConfig(prop.cfgId);
			SpawnLogic spawnLogic = null;

			if (data != null)
			{
				if (data.type == 0)
				{
					spawnLogic = new PropPowerSpawnLogic();
				}
				else if (data.type == 1)
				{
					spawnLogic = new PropSkillSpawnLogic();
				}

				spawnLogic.Init(prop);
				AddSpawnLogic(spawnLogic);
			}
			else
			{
				DebugLoger.LogError($"找不到配置数据CfgId:{prop.cfgId}");
			}
		}

		public static SpawnLogic GetPropLogic(int propid)
		{
			if (datas.HasKey(propid))
			{
				return datas[propid];
			}

			return null;
		}

		public static SpawnLogic RemovePropLogic(int propid)
		{
			if (datas.HasKey(propid))
			{
				var data = datas[propid];
				RemoveSpawnLogicXY((int)data.data.x, (int)data.data.y, data);
				datas.RemoveKey(propid);
				return data;
			}

			return null;
		}

		public static void AddSpawnLogic(SpawnLogic spawnLogic)
		{
			datas.AddValue(spawnLogic.data.id, spawnLogic);
			AddSpawnLogicXY((int)spawnLogic.data.x, (int)spawnLogic.data.y, spawnLogic);
		}

		public static List<SpawnLogic> GetSpawnLogicXY(int x, int y)
		{
			Dictionary<int, LantisDictronaryList<int, List<SpawnLogic>>> yDatas = null;

			if (lantisDatas.ContainsKey(x))
			{
				yDatas = lantisDatas[x];
			}
			else
			{
				yDatas = new Dictionary<int, LantisDictronaryList<int, List<SpawnLogic>>>();
				lantisDatas.Add(x, yDatas);
			}

			LantisDictronaryList<int, List<SpawnLogic>> lantisDictronaryList = null;

			if (yDatas.ContainsKey(y))
			{
				lantisDictronaryList = yDatas[y];
			}
			else
			{
				lantisDictronaryList = new LantisDictronaryList<int, List<SpawnLogic>>();
				lantisDictronaryList.AddValue(y, new List<SpawnLogic>());
				yDatas.Add(y, lantisDictronaryList);
			}

			return lantisDictronaryList[y];
		}

		public static void RemoveSpawnLogicXY(int x, int y, SpawnLogic spawnLogic)
		{
			var dataList = GetSpawnLogicXY(x, y);

			if (dataList.Contains(spawnLogic))
			{
				dataList.Remove(spawnLogic);
			}
		}

		public static void AddSpawnLogicXY(int x,int y, SpawnLogic spawnLogic)
		{
			var dataList = GetSpawnLogicXY(x,y);
			dataList.Add(spawnLogic);
		}

		public static void GetRangSpawnLogicXY(int centerX, int centerY, float rang,Vector3 characterPosition)
		{
			if (eatPropList.Count > 0)
			{
				eatPropList.Clear();
			}

			if (rang <= 0)
			{
				return;
			}

			var edge = (int)Math.Ceiling(rang);
			var minX = centerX - edge;
			var maxX = centerX + edge;
			var minY = centerY - edge;
			var maxY = centerY + edge;

			for (var x = minX; x <= maxX; x++)
			{
				for (var y = minY; y <= maxY; y++)
				{
					var logicList = GetSpawnLogicXY(x,y);

					for (var i = 0; i < logicList.Count; ++i)
					{
						var logic = logicList[i];
						var distanc = Vector3.Distance(logic.node.transform.localPosition, characterPosition);

						if (distanc < eatDistance)
						{
							eatPropList.Add(logic.data.id);
						}
					}
				}
			}
		}

		public static void Clear()
		{
			if (currentCheckKeys != null)
			{
				currentCheckKeys.Clear();
				currentCheckKeys = null;
			}

			if (eatPropList != null)
			{
				eatPropList.Clear();
				eatPropList = null;
			}

			if (datas != null)
			{
				datas.Clear();
				datas = null;
			}

			if (lantisDatas != null)
			{
				lantisDatas.Clear();
				lantisDatas = null;
			}
		}

		public static void CheckEatProp()
		{
			return;
			checkInvateTime += Time.fixedDeltaTime;

			if (checkInvateTime > checkTimeMax)
			{
				checkInvateTime = 0.0f;
				var mainCharacter = SingleMoba.CharacterManager.Instance.GetMainCharacter();

				if (mainCharacter != null)
				{
					var mainCharacterPosition = mainCharacter.GetNode().transform.localPosition;
					GetRangSpawnLogicXY((int)mainCharacterPosition.x, (int)mainCharacterPosition.z, eatDistance, mainCharacterPosition);

					if (eatPropList.Count > 0)
					{
						MessageSend.EatProp(eatPropList);
					}
				}
				else
				{
					DebugLoger.LogError("mainCharacter null");
				}
			}
		}

		public static void EatProps(int playerId, List<int> propIds, P_GamerStateChange gamerChange)
		{
			var character = CharacterManager.Instance.GetCharacter(playerId);

			if (character != null)
			{
				var moveCount = 0;

				CherishTween.ParamarCallFun moveEndCall = (paramar) =>
				{
					var spawnLogic = paramar as SpawnLogic;
					spawnLogic.Despawn();

					moveCount--;

					if (moveCount == 0)
					{
						
					}
				};

				character.SetDataChange(gamerChange);

				for (var i = 0; i < propIds.Count; ++i)
				{
					moveCount++;
					var propId = propIds[i];
					var spawnLogic = RemovePropLogic(propId);

					if (spawnLogic != null)
					{
						spawnLogic.MoveTo(character.GetNode(), moveEndCall);
					}
				}
			}
			else
			{
				DebugLoger.LogError("没有找到吃道具的角色玩家!");
			}
		}
	}

	public class SpawnLogic
	{
		public P_Prop data;
		public GameObject node;

		public virtual void Init(P_Prop setdata)
		{
			data = setdata;
			LoadMoudle();
		}

		public virtual void LoadMoudle()
		{
			var configData = DefineConfig.GetConfig(data.cfgId);

			if (configData != null)
			{
				node = LoadPrefab.SpawnPower(configData.assetName);

				if (node != null)
				{
					SetToPropNode();
				}
				else
				{
					DebugLoger.LogError($"找不到资源assetName:{configData.assetName}");
				}
			}
			else
			{
				DebugLoger.LogError($"找不到配置数据CfgId:{data.cfgId}");
			}
		}

		public void SetToPropNode()
		{
			if (FrameWorkDrvice.WorldManagerInstance.currentWorld != null)
			{
				var singleMoba = FrameWorkDrvice.WorldManagerInstance.currentWorld as WorldSpace.SingleMobaFightWorld;

				if (singleMoba != null)
				{
					if (singleMoba.propNode != null)
					{
						node.SetActive(true);
						node.transform.SetParent(singleMoba.propNode.transform);
						node.transform.localScale = Vector3.one;

						if (data != null)
						{
							node.transform.localPosition = new Vector3(data.x, 0.0f, data.y);
							Set3DPos();
						}
						else
						{
							DebugLoger.LogError("data null");
						}
					}
					else
					{
						DebugLoger.LogError("singlemobafightworld propnode null");
					}
				}
				else
				{
					DebugLoger.LogError("singlemobafightworld null");
				}
			}
			else
			{
				DebugLoger.LogError($"current world null");
			}
		}

		public void MoveTo(GameObject target, CherishTween.ParamarCallFun call)
		{
			CherishTweenFollowMove.Begin(node, node.transform.position, target, 0.3f, 0.0f, false, call,this);
		}

		public void Set3DPos()
		{
			if (ConfigProject.is3d)
			{
				if (BoxMap.Instance != null)
				{
					node.transform.localPosition = new Vector3(node.transform.localPosition.x, BoxMap.Instance.GetBoxMapNodeHeight(node.transform.localPosition.x, node.transform.localPosition.z), node.transform.localPosition.z);
				}
			}
		}

		public void Despawn()
		{
			LoadPrefab.DespawnPower(node);
		}
	}

	public class PropPowerSpawnLogic : SpawnLogic
	{
		public override void LoadMoudle()
		{
			base.LoadMoudle();
		}
	}

	public class PropSkillSpawnLogic : SpawnLogic
	{
		public override void LoadMoudle()
		{
			base.LoadMoudle();
		}
	}
}
