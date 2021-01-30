using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	public class EffectLogic
	{
		public static List<EffectLogic> effectLogicList = new List<EffectLogic>();

		public static void AddEffect(string effectName,float life,GameObject target)
		{
			var effectLogic = new EffectLogic();
			effectLogic.Start(effectName,life, target);
			effectLogicList.Add(effectLogic);
		}

		public static void Remove(EffectLogic item)
		{
			if (effectLogicList.Contains(item))
			{
				effectLogicList.Remove(item);
			}

			item.Despawn();
		}

		public static void Clear()
		{
			for (var i = 0; i < effectLogicList.Count; ++i)
			{
				effectLogicList[i].Despawn();
			}

			effectLogicList.Clear();
		}

		public GameObject node;

		public void Start(string effectName,float life,GameObject target)
		{
			node = LoadPrefab.SpawnFightSkill(effectName);

			if (node != null)
			{
				var world = FrameWorkDrvice.WorldManagerInstance.currentWorld as WorldSpace.SingleMobaFightWorld;

				if (world != null)
				{
					node.transform.SetParent(world.propNode.transform);
					node.transform.localScale = Vector3.one;
					node.transform.localPosition = target.transform.localPosition;
				}
			}

			IEnumeratorManager.Instance.StartCoroutine(WaitLife(life));	
		}

		private IEnumerator WaitLife(float life)
		{
			yield return new IEnumeratorManager.WaitForSeconds(life);
			Remove(this);
		}

		private void Despawn()
		{
			if (node != null)
			{
				LoadPrefab.DespawnFightSkill(node);
				node = null;
			}
		}
	}
}
