using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SingleMoba
{
    /// <summary>
    /// 相机跟随动画
    /// </summary>
    public class CameraFloowComp : CameraComp
    {
        private bool isStart = false;

        private GameObject target;

        private Camera cameraComp;

        private GameObject cameraObject;

        private float contollerSpeed = 35.0f;

        private float recordX = 75.0f;

        private float recordY = 0.0f;

        private float recordDistance = 13.0f;

        private float clampAngle = 50;

        private float maxXClamp = 180;

        private float minXClamp = -20;

        private float maxDistance = 160;

        private float minDistance = 15;

        private Vector3 cameraOffset;

        public void SetParamar(Camera camera)
        {
            cameraComp = camera;
            cameraObject = camera.gameObject;
        }

        public void SetFollowTarget(GameObject followTarget)
        {
            target = followTarget;
        }


        public override void OnEntry()
        {
            base.OnEntry();

            isStart = false;

            if (ConfigProject.lockCameraControl)
            {
                recordX = 55.0f;
            }
            else
            {
                recordX = 25.0f;
            }
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        /// <summary>
        /// 更新逻辑
        /// </summary>
        public override void UpLogic()
        {
            if (cameraObject != null && target == null)
            {
                DebugLoger.Log("UpCamera UpLogic Not Target");
                return;
            }

            if (ConfigProject.lockCameraControl)
            {
                cameraObject.transform.rotation = Quaternion.Euler(recordX, recordY, 0);
            }
            else
            {
                var diffAngle = target.transform.eulerAngles.y - cameraObject.transform.eulerAngles.y;

                if (Mathf.Abs(diffAngle) > clampAngle || !isStart)
                {
                    if (!isStart)
                    {
                        isStart = true;
                    }

                    var rotation = Quaternion.Euler(recordX, recordY + target.transform.localEulerAngles.y, 0);
                    cameraObject.transform.rotation = Quaternion.Slerp(cameraObject.transform.rotation, rotation, Time.deltaTime * 0.2f);
                }
            }

            //cameraOffset.y = 0;
            cameraOffset.z = -recordDistance;
            cameraObject.transform.position = target.transform.position + cameraObject.transform.rotation * cameraOffset;
        }
    }
}
