using UnityEngine;
using System.Collections;

namespace SingleMoba
{
    public class CameraManager
    {
        private static CameraManager _instance;
        public static CameraManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CameraManager();
                }
                return _instance;
            }
        }

        private GameObject mainCameraObj;
        private Camera mainCameraComp;
        private CameraFloowComp mainCameraFloowComp;
        private CameraComp currentCameraComp;

        /// <summary>
        /// 相机
        /// </summary>
        public void SetMainCamera(GameObject camera)
        {
            mainCameraObj = camera;

            if (mainCameraObj == null)
            {
                DebugLoger.Log("SetMainCamera mainCameraObj null");
            }

            mainCameraComp = mainCameraObj.GetComponent<Camera>();
            SetMainCameraActive(true);
            ChangeCameraCompToFloow();
        }

        /// <summary>
        /// 激活MainCamera和非激活切换
        /// </summary>
        /// <param name="active">If set to <c>true</c> active.</param>
        public void SetMainCameraActive(bool active)
        {
            if (mainCameraObj != null)
            {
                mainCameraObj.SetActive(active);
            }
        }

        /// <summary>
        /// 切换到跟随摄像机
        /// </summary>
        public void ChangeCameraCompToFloow()
        {
            if (mainCameraFloowComp == null)
            {
                mainCameraFloowComp = new CameraFloowComp();
                mainCameraFloowComp.SetParamar(mainCameraComp);
            }

            ChangeCameraComp(mainCameraFloowComp);
        }

        public void SetCameraTarget(GameObject target)
        {
            mainCameraFloowComp.SetFollowTarget(target);
        }


        /// <summary>
        /// 切换相机脚本
        /// </summary>
        /// <param name="_cameraComp">_camera comp.</param>
        public void ChangeCameraComp(CameraComp _cameraComp)
        {
            if (currentCameraComp == _cameraComp)
            {
                return;
            }

            if (currentCameraComp != null)
            {
                currentCameraComp.OnLeave();
            }

            currentCameraComp = _cameraComp;

            currentCameraComp.OnEntry();
        }

        public void LateUpdate()
        {
            if (currentCameraComp != null)
            {
                currentCameraComp.LateUpdateUpCameraFrame();
            }
        }


        public GameObject GetMainCameraObj()
        {
            return mainCameraObj;
        }

        public Camera GetMainCameraComp()
        {
            return mainCameraComp;
        }
    }


    /// <summary>
    /// 相机脚本基础
    /// </summary>
    public class CameraComp
    {
        private bool isStayThisComp = false;

        public virtual void OnEntry()
        {
            isStayThisComp = true;
        }

        public virtual void OnLeave()
        {
            isStayThisComp = false;
        }

        public virtual void LateUpdateUpCameraFrame()
        {
            if (isStayThisComp)
            {
                UpLogic();
            }
        }

        public virtual void UpLogic()
        {

        }
    }
}
