using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System;

namespace WorldSpace
{
    public class SingleMobaFightWorld : WorldSystem
    {
        /// <summary>
        /// 回调到这个方法
        /// </summary>
        /// <param name="_className"></param>
        public static void RegistSystem(string _className)
        {
            FrameWorkDrvice.WorldManagerInstance.RegistCallFun(SingleMoba.WorldAssetsNameDefine.FightSence, _className);
        }

        public SingleMobaFightWorld()
        {
            isEntry = false;
            base.OnEntryWorldCall = OnEntryWorld;
            base.OnLeaveWorldCall = OnLeaveWorld;
            base.OnFixedUpdateCall = OnFixedUpdate;
            base.OnLateUpdateCall = OnLateUpdate;
        }

        public GameObject heroNode;
        public GameObject propNode;
        public GameObject heroItem;
        private bool isEntry = false;


        /// <summary
        /// 进入场景
        /// </summary>
        /// <param name="worldName"></param>
        public void OnEntryWorld(string worldName)
        {
            SingleMoba.LoadPrefab.PrefabLoadAll();
            SingleMoba.LoadPrefab.InitPool();
            UINameSpace.UIRall.GetInstance().SetActive(false);
            UINameSpace.UISingleMobaRall.GetInstance().SetActive(false);
            SingleMoba.CharacterManager.Instance.Clear();
            SingleMoba.CameraManager.Instance.SetMainCamera(GenericityTool.GetObjectByPath(senceRoot, "buildingNode/Main Camera"));
            propNode = GenericityTool.GetObjectByPath(senceRoot, "buildingNode/propNode");
            heroNode = GenericityTool.GetObjectByPath(senceRoot, "buildingNode/heroNode");
            heroItem = GenericityTool.GetObjectByPath(senceRoot, "buildingNode/heroNode/Hoshi");
            FrameWorkDrvice.UiManagerInstance.RegistSenceCamera(SingleMoba.CameraManager.Instance.GetMainCameraComp());
            InitWorldData();
            isEntry = true;
        }

        /// <summary>
        /// 退出场景
        /// </summary>
        /// <param name="worldName"></param>
        private void OnLeaveWorld(string worldName)
        {
            FrameWorkDrvice.UiManagerInstance.CloseUI(SingleMoba.UIDefineName.UIFight,eCloseType.Queue);
            SingleMoba.EffectLogic.Clear();
            SingleMoba.PropLogic.Clear();
            SingleMoba.SkillBuffLogic.Clear();
            SingleMoba.SkillLogic.Clear();
            SingleMoba.CharacterManager.Instance.Clear();
            SingleMoba.LoadPrefab.DesposePool();
            SingleMoba.LoadPrefab.ReleseAllPrefab();
            FrameWorkDrvice.UiManagerInstance.UnRegistSenceCamera();
        }

        /// <summary>
        /// 持续刷新
        /// </summary>
        /// <param name="deltaTime"></param>
        private void OnFixedUpdate(float deltaTime)
        {
            if (isEntry)
            {
                SingleMoba.CharacterManager.Instance.FixedUpdate();
                SingleMoba.PropLogic.CheckEatProp();
                SingleMoba.SkillLogic.FixedUpdate();
                SingleMoba.SkillBuffLogic.FixedUpdate();
                SingleMoba.CameraManager.Instance.LateUpdate();
            }
        }

        /// <summary>
        /// 相机刷新
        /// </summary>
        /// <param name="deltaTime"></param>
        private void OnLateUpdate(float deltaTime)
        {
        }

        private void InitWorldData()
        {          
            SingleMoba.MessageSend.AssetsLoadFinish();
        }

        public void InitSenceData(SingleMoba.SC_RoomInfo roomInfo)
        {
            var playerList = roomInfo.roomPlayerInfoList;

            for (var i = 0; i < playerList.Count; ++i)
            {
                var playerData = playerList[i];
                SingleMoba.CharacterManager.Instance.AddPlayerInfo(playerData);
            }
            
            try
            {
                SingleMoba.PropLogic.AddProps(roomInfo.props);
                SingleMoba.SkillLogic.AddSkills(roomInfo.skills);
                SingleMoba.SkillBuffLogic.AddSkillBuffs(roomInfo.skillBuffs);
            }
            catch (Exception e)
            {
                DebugLoger.LogError("", e);
            }

            FrameWorkDrvice.UiManagerInstance.OpenUI(SingleMoba.ConfigProject.projectFloderName, SingleMoba.UIDefineName.UIFight,true);
        }
    }
}

