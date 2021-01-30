using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	/// <summary>
	/// 对应的动画状态
	/// </summary>
	public enum eAnimationState : int
	{
		DeadState = -1,
		IdleState = 0,
		RunState = 1,
		SkillState1 = 101,
		SkillState2 = 102,
		SkillState3 = 103,
	}

	public class CharacterBase
	{
		private P_PlayerInfo playerData;
		private GameObject node;
		private float speed;
		private Animator animator;

		public void SetNode(GameObject setNode, P_PlayerInfo setPlayerData)
		{
			playerData = setPlayerData;
			node = setNode;
			animator = node.GetComponent<Animator>();

			if (playerData.hp > 0)
			{
				PlayIdea();
			}
			else
			{
				PlayDead();
			}
		}

		public void SetToLocal(GameObject parent)
		{
			node.transform.SetParent(parent.transform);
			node.transform.localScale = Vector3.one;
		}

		public GameObject GetNode()
		{
			return node;
		}

		public void SetSpeed(float setSpeed)
		{
			speed = setSpeed;
		}

		public float GetSpeed()
		{
			return speed;
		}
		public P_PlayerInfo GetPlayerData()
		{
			return playerData;
		}

		public void SetDataChange(P_GamerStateChange stateChange)
		{
			LogicDataSpace.SetPlayerChangeState(stateChange);
		}

		public void FixedUpdate()
		{
			UpMove();
		}

		public virtual void UpMove()
		{ }

		public void SetOffset(Vector3 offset)
		{
			node.transform.localPosition += offset;
			Set3DPos();
		}

		public void SetPos(Vector3 currentPos)
		{
			node.transform.localPosition = currentPos;
			Set3DPos();
		}
		public Vector3 GetPos()
		{
			return node.transform.localPosition;
		}

		public void SetDirection(Vector3 direction)
		{
			node.transform.forward = direction;
		}

		public Vector3 GetDirection()
		{
			return node.transform.forward;
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

		public virtual void OnDispose()
		{
			GameObject.Destroy(node);
		}

		public void PlayRun()
		{
			if (!IsAnimatorPlayWithName("Run"))
			{
				PlayAnimation(eAnimationState.RunState);
			}
		}

		public void PlayHit()
		{ }

		public void PlaySkill1()
		{
			if (!IsAnimatorPlayWithName("Skill3"))
			{
				PlayAnimation(eAnimationState.SkillState3);
			}
			else
			{
				PlayAnimationReset("Skill3");
			}
		}

		public void PlaySkill2()
		{
			if (!IsAnimatorPlayWithName("Skill2"))
			{
				PlayAnimation(eAnimationState.SkillState2);
			}
			else
			{
				PlayAnimationReset("Skill2");
			}
		}

		public void PlaySkill3()
		{
			if (!IsAnimatorPlayWithName("Skill1"))
			{
				PlayAnimation(eAnimationState.SkillState1);
			}
			else
			{
				PlayAnimationReset("Skill1");
			}
		}

		public void PlayIdea()
		{
			if (!IsAnimatorPlayWithName("Idle"))
			{
				PlayAnimation(eAnimationState.IdleState);
			}
		}

		public void PlayDead()
		{
			if (!IsAnimatorPlayWithName("Dead"))
			{
				PlayAnimation(eAnimationState.DeadState);
			}
		}
		
		public bool IsAnimatorPlayWithName(string name)
		{
			if (animator != null)
			{
				AnimatorStateInfo animatorStateInfor = animator.GetCurrentAnimatorStateInfo(0);

				if (animatorStateInfor.IsName(name))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 动画是否播放完毕
		/// </summary>
		/// <returns></returns>
		public bool IsAnimatorPlayEnd()
		{
			if (animator != null)
			{
				AnimatorStateInfo animatorStateInfor = animator.GetCurrentAnimatorStateInfo(0);

				if (animatorStateInfor.normalizedTime >= 0.9f)
				{
					return true;
				}
			}

			return false;
		}

		public void PlayAnimationReset(string animationName)
		{
			if (animator != null)
			{
				animator.Play(animationName);
			}
		}

		public void PlayAnimation(eAnimationState state)
		{
			if (animator != null)
			{
				animator.SetInteger("state", (int)state);
			}
		}

		public void SetValueChange(ChactarPropyteType propyteType,int currentValue, int changeValue)
		{
			LocalEventNotices.Notices(EventNoticesDefine.ChactarPropyteType, new object[] { playerData.playerId, propyteType,currentValue, changeValue });

			switch (propyteType)
			{
				case ChactarPropyteType.Hp:
				{
					break;
				}
			}
		}

		public bool IsLife()
		{
			if (playerData != null && playerData.hp > 0)
			{
				return true;
			}

			return false;
		}

		public bool CanMove()
		{
			if (IsLife() && playerData.canMove == 1)
			{
				return true;
			}

			return false;
		}
	}

	public class CharacterManager
	{
		private static CharacterManager __instance;
		public static CharacterManager Instance 
		{
			get 
			{
				if (__instance == null)
				{
					__instance = new CharacterManager();
				}

				return __instance;
			}
		}

		private CharacterBase mainCharacter;
		private List<CharacterBase> characterList = new List<CharacterBase>();

		public void Clear()
		{
			for (var i = 0; i < characterList.Count; ++i)
			{
				characterList[i].OnDispose();
			}

			characterList.Clear();
		}

		public void AddPlayerInfo(P_PlayerInfo playerData)
		{
			var fightWorld = FrameWorkDrvice.WorldManagerInstance.currentWorld as WorldSpace.SingleMobaFightWorld;

			if (fightWorld != null)
			{
				var playerNode = GameObject.Instantiate(fightWorld.heroItem);

				if (playerData.playerId == int.Parse(GoableData.userValiadateInfor.DatingNumber))
				{
					var mainPlayer = new SingleMoba.MainCharacter();
					mainPlayer.SetNode(playerNode, playerData);
					mainPlayer.SetToLocal(fightWorld.heroNode);
					mainPlayer.SetSpeed(3.0f);
					mainPlayer.SetPos(new Vector3(playerData.x, 0.0f, playerData.y));
					SingleMoba.CharacterManager.Instance.SetMainCharacter(mainPlayer);
					SingleMoba.CameraManager.Instance.SetCameraTarget(playerNode);
					LocalEventNotices.Notices(EventNoticesDefine.ChactarJoin, mainPlayer);
				}
				else
				{
					var otherPlayer = new SingleMoba.OtherCharacter();
					otherPlayer.SetNode(playerNode, playerData);
					otherPlayer.SetToLocal(fightWorld.heroNode);
					otherPlayer.SetSpeed(3.0f);
					otherPlayer.SetPos(new Vector3(playerData.x, 0.0f, playerData.y));
					SingleMoba.CharacterManager.Instance.AddCharacter(otherPlayer);
					LocalEventNotices.Notices(EventNoticesDefine.ChactarJoin, otherPlayer);
				}
			}
		}

		public void SetMainCharacter(CharacterBase character)
		{
			mainCharacter = character;
			AddCharacter(character);
		}

		public MainCharacter GetMainCharacter()
		{
			return mainCharacter as MainCharacter;
		}

		public void AddCharacter(CharacterBase character)
		{
			characterList.Add(character);
		}

		public CharacterBase GetCharacter(int playerId)
		{
			for (var i = 0; i < characterList.Count; ++i)
			{
				if (characterList[i].GetPlayerData().playerId == playerId)
				{
					return characterList[i];
				}
			}

			return null;
		}

		public List<CharacterBase> GetCharacterAll()
		{
			return characterList;
		}

		public void SetCharacterMove(SingleMoba.SC_GamerMove moveData)
		{
			var characterBase = GetCharacter(moveData.playerId);

			if (characterBase != null)
			{
				var characterOther = characterBase as OtherCharacter;

				if (characterOther != null)
				{
					characterOther.RemoveTop();
					characterOther.AddMoveTask(new Vector3(moveData.targetX, 0, moveData.targetY), moveData.ticks);
				}
				else
				{
					DebugLoger.LogError("SetCharacterMove characterOther null");
				}
			}
			else
			{
				DebugLoger.LogError("SetCharacterMove characterBase null");
			}
		}

		public void SetCharacterMoveStop(SingleMoba.SC_GamerMoveStop moveStopData)
		{
			var characterBase = GetCharacter(moveStopData.playerId);

			if (characterBase != null)
			{
				var characterOther = characterBase as OtherCharacter;

				if (characterOther != null)
				{
					characterOther.SetMoveEndTask(new Vector3(moveStopData.currentX, 0, moveStopData.currentY), 0);
				}
				else
				{
					DebugLoger.LogError("SetCharacterMoveStop characterOther null");
				}
			}
			else
			{
				DebugLoger.LogError("SetCharacterMoveStop characterBase null");
			}
		}

		public void FixedUpdate()
		{
			for (var i = 0; i < characterList.Count; ++i)
			{
				characterList[i].FixedUpdate();
			}
		}
	}
}
