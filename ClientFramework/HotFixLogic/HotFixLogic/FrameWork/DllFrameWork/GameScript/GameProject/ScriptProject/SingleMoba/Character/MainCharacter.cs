using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleMoba
{
	public class MainCharacter : CharacterBase
	{
		public const float upSendTimeMax = 0.5f;
		public const float upSendTimeMoveMax = 0.55f;
		private bool isMove;
		private float upSendTime;

		public override void UpMove()
		{
			base.UpMove();
			
			var uiInstance = UINameSpace.UISingleMobaFight.GetInstance();

			if (uiInstance != null)
			{
				if (CanMove())
				{
					var inputOffset = uiInstance.GetMovePosition();
					var cameraObject = SingleMoba.CameraManager.Instance.GetMainCameraObj();

					if (inputOffset != Vector3.zero && cameraObject != null)
					{
						inputOffset = inputOffset.normalized;

						if (Mathf.Abs(inputOffset.x) < 0.3f)
						{
							inputOffset.x = 0.0f;
						}
						
						if (!isMove)
						{
							isMove = true;
							upSendTime = 0.0f;
						}

						if (upSendTime > upSendTimeMax)
						{
							upSendTime = 0.0f;
						}

						var node = GetNode();
						var cameraFoward = (node.transform.localPosition - new Vector3(cameraObject.transform.localPosition.x, node.transform.localPosition.y, cameraObject.transform.localPosition.z));
						cameraFoward = cameraFoward.normalized;
						var angleInput = UtilityTool.DirectionToAngle(new Vector3(inputOffset.x,0,inputOffset.y));
						var cameraAngle = UtilityTool.DirectionToAngle(cameraFoward);
						var allAngle = cameraAngle.eulerAngles.y + angleInput.eulerAngles.y;

						while (Mathf.Abs(allAngle) >= 360.0f)
						{
							if (allAngle > 0)
							{
								allAngle -= 360.0f;
							}
							else
							{
								allAngle += 360.0f;
							}
						}

						var direction = UtilityTool.AngleToDirection(allAngle);

						if (ConfigProject.lockCameraControl)
						{
							SetDirection(direction);
						}
						else
						{
							SetDirection(Vector3.Lerp(GetDirection(), direction, Time.fixedDeltaTime * 3.0f));
						}

						var offsetx = GetDirection() * GetSpeed() * Time.fixedDeltaTime;
						SetOffset(offsetx);
						var oldDirection = GetDirection();

						if (Math.Abs(GetDirection().x - oldDirection.x) > 0.5f || Math.Abs(GetDirection().y - oldDirection.y) > 0.5f)
						{
							SendCurrentMove();
						}
						else if (upSendTime == 0.0f)
						{
							SendCurrentMove();
						}

						upSendTime += Time.fixedDeltaTime;
					}
					else
					{
						if (isMove)
						{
							upSendTime = 0.0f;
							isMove = false;
							SendMoveStop();
						}
					}
				}

			}

			if (IsLife())
			{
				if (isMove)
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

		public void SendCurrentMove()
		{
			var currentPos = GetPos();
			var direction = GetDirection();
			var targetPos = direction * GetSpeed() * upSendTimeMoveMax + GetPos();
			MessageSend.GamerMove(currentPos.x,currentPos.z,targetPos.x, targetPos.z);
		}

		public void SendMoveStop()
		{
			var currentPos = GetPos();
			MessageSend.GamerMoveStop(currentPos.x, currentPos.z);
		}
	}
}
