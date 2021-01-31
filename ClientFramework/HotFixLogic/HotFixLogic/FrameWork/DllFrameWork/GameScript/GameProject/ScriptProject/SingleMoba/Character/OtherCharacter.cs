using Cherish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	public class OtherCharacter : CharacterBase
	{
		private const float minOffsetDistance = 0.2f;
		private const int pingMoveTimes = 10;
		public List<KeyValue<KeyValue<int,long>,Vector3>> movaTaskList = new List<KeyValue<KeyValue<int,long>, Vector3>>();
		private bool isArrive = false;
		private float moveDistance = 0.0f;
		private Vector3 offset;
		private bool playMove;
		public void AddMoveTask(Vector3 currentPos,long ticks)
		{
			movaTaskList.Add(new KeyValue<KeyValue<int, long>, Vector3>()
			{
				key = new KeyValue<int, long> 
				{
					key = 0,
					value = ticks
				},
				value = currentPos
			});
		}

		public void SetMoveEndTask(Vector3 currentPos, long ticks)
		{
			RemoveTop();

			if (Vector3.Distance(currentPos,GetPos()) > minOffsetDistance)
			{
				movaTaskList.Add(new KeyValue<KeyValue<int, long>, Vector3>()
				{
					key = new KeyValue<int, long>
					{
						key = 0,
						value = ticks
					},
					value = currentPos
				});
			}
		}

		public void RemoveTop()
		{
			if (movaTaskList.Count > 0)
			{
				movaTaskList.RemoveAt(0);
			}
		}

		public bool HasNextMovePost()
		{
			if (movaTaskList.Count > 0)
			{
				return true;
			}

			return false;
		}

		public KeyValue<KeyValue<int,long>, Vector3> GetNextMovePos()
		{
			return movaTaskList[0];
		}

		public override void UpMove()
		{
			base.UpMove();
			playMove = false;

			if (CanMove())
			{
				if (HasNextMovePost())
				{
					var targetPos = GetNextMovePos();

					if (ConfigProject.is3d)
					{
						targetPos.value.y = BoxMap.Instance.GetBoxMapNodeHeight(targetPos.value.x, targetPos.value.z);
					}

					var pingTime = 0.0f;
					var node = GetNode();
					var direction = (targetPos.value - GetPos()).normalized;

					offset = Vector3.zero;
					moveDistance = 0.0f;

					if (targetPos.key.value != 0 && targetPos.key.key < pingMoveTimes)
					{
						targetPos.key.key++;
						pingTime = CSTools.TicksToSencend(GoableData.GetServerNowTime() - targetPos.key.value);
						moveDistance = GetSpeed() * (Time.deltaTime + pingTime / (pingMoveTimes + 2));
						moveDistance = GetSpeed() * Time.fixedDeltaTime;
						offset = direction * moveDistance;
					}
					else
					{
						moveDistance = GetSpeed() * Time.fixedDeltaTime;
						offset = direction * moveDistance;
					}

					SetDirection(direction);
					isArrive = false;

					if (Vector3.Distance(targetPos.value, GetPos()) <= moveDistance)
					{
						isArrive = true;
					}
					else
					{
						SetOffset(offset);
					}

					var newDirection = (targetPos.value - GetPos()).normalized;

					if (isArrive || (newDirection == Vector3.zero || direction != newDirection))
					{
						SetPos(targetPos.value);
						RemoveTop();
					}

					playMove = true;
				}
			}
			if (IsLife())
			{
				if (playMove)
				{
					PlayRun();
				}
				else
				{
					PlayIdea();
				}
			}
			else
			{
				PlayDead();
			}
		}
	}
}
