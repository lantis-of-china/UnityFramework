using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace UINameSpace
{
    public class UIQuickJoin : UIObject
    {
        public GameObject animationNode;
        public Button btnBack;
        public List<Button> numberBtn = new List<Button>();
        public Button btnReset;
        public Button btnDelete;
        public List<int> numberList = new List<int>();
        public List<Image> numberImageList = new List<Image>();
        

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIQuickJoin_Rall, _className);

            return 1;
        }

        public UIQuickJoin()
        {
            assetsName = Rall.UIDefineName.UIQuickJoin_Rall;
        }
        
        public override void OnAwake()
        {
            animationNode = GenericityTool.GetObjectByPath(objectInstance, "achorNode/animationNode");
            for (int loop = 0; loop < 6;++loop)
            {
                Image ima = GenericityTool.GetComponentByPath<Image>(animationNode, "imageLine/number_bit_" + loop);
                numberImageList.Add(ima);
            }

            btnBack = GenericityTool.GetComponentByPath<Button>(animationNode, "btnClose");
            for (int i = 0; i < 10;++i)
            {
                Button btn = GenericityTool.GetComponentByPath<Button>(animationNode, "btnNumberList/btn_" + i);
                numberBtn.Add(btn);

            }

            numberBtn[0].onClick.AddListener(OnTapNumber_0);
            numberBtn[1].onClick.AddListener(OnTapNumber_1);
            numberBtn[2].onClick.AddListener(OnTapNumber_2);
            numberBtn[3].onClick.AddListener(OnTapNumber_3);
            numberBtn[4].onClick.AddListener(OnTapNumber_4);
            numberBtn[5].onClick.AddListener(OnTapNumber_5);
            numberBtn[6].onClick.AddListener(OnTapNumber_6);
            numberBtn[7].onClick.AddListener(OnTapNumber_7);
            numberBtn[8].onClick.AddListener(OnTapNumber_8);
            numberBtn[9].onClick.AddListener(OnTapNumber_9);

            btnReset = GenericityTool.GetComponentByPath<Button>(animationNode, "btnNumberList/btnReset");
            btnDelete = GenericityTool.GetComponentByPath<Button>(animationNode, "btnNumberList/btnDelete");

            btnReset.onClick.AddListener(OnReste);
            btnDelete.onClick.AddListener(OnDelete);
            btnBack.onClick.AddListener(OnClickClose);            
        }

        public override void OnEnable()
        {
            base.OnEnable();
            ResetInfo();
            CherishTweenScale.Begin(animationNode, Vector3.zero, new Vector3(1.25f,1.25f,1.0f), 0.2f, 0.2f);
        }

        private void UpNumberValue()
        {            
            for (int loop = 0; loop < numberList.Count; ++loop)
            {
                int numV = numberList[loop];
                Image curImage = numberImageList[loop];
                curImage.enabled = true;
                AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName,curImage, "number_" + numV);
            }

            for (int loop = 5; loop >= numberList.Count; --loop)
            {
                Image curImage = numberImageList[loop];
                curImage.enabled = false;
            }

            if(numberList.Count == 5)
            {
				if (string.IsNullOrEmpty(Rall.ConfigProject.currentRallName))
				{
					FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.ConfigProject.currentRallName, eCloseType.None);
				}

				if (string.IsNullOrEmpty(GoableData.reconnectExternUIName))
				{
					FrameWorkDrvice.UiManagerInstance.CloseUI(GoableData.reconnectExternUIName, eCloseType.None);
					GoableData.reconnectExternUIName = "";
				}
				GoableData.SetReconnectDisable();
				GoableData.reconnectIp = "";
				GoableData.ServerIpaddress.gameServerIp = "";
				GoableData.CloseHeart();
				GoableData.ServerIpaddress.isLoginGameServerSend = false;
				GoableData.ServerIpaddress.isLoginGameLogic = false;
				GoableData.ServerIpaddress.readyEntryRoomId = -1;
				GoableData.ServerIpaddress.clubId = "";
				if (UserNetWork.HasInstance())
				{
					UserNetWork.Instance.CloseSocket(Rall.MessageSend.LoginOut);
				}
				UINameSpace.UIWaitting.AddShowWaitting("QuickJame");
				string roomId = numberList[0].ToString() + numberList[1].ToString() + numberList[2].ToString() + numberList[3].ToString() + numberList[4].ToString();
				///发送消息给服务器  加入房间
				Rall.MessageSend.QuickJoinGame(int.Parse(roomId));

                CloseUI();
            }
        }

        public static void JoinRoom(int roomIdshow)
        {
			if (!string.IsNullOrEmpty(Rall.ConfigProject.currentRallName))
			{
				FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.ConfigProject.currentRallName, eCloseType.None);
			}

			if (!string.IsNullOrEmpty(GoableData.reconnectExternUIName))
			{
				FrameWorkDrvice.UiManagerInstance.CloseUI(GoableData.reconnectExternUIName, eCloseType.None);
				GoableData.reconnectExternUIName = "";
			}
			GoableData.SetReconnectDisable();
			GoableData.reconnectIp = "";
			GoableData.ServerIpaddress.gameServerIp = "";
			GoableData.CloseHeart();
			GoableData.ServerIpaddress.isLoginGameServerSend = false;
			GoableData.ServerIpaddress.isLoginGameLogic = false;
			GoableData.ServerIpaddress.readyEntryRoomId = -1;
			GoableData.ServerIpaddress.clubId = "";
			if (UserNetWork.HasInstance())
			{
				UserNetWork.Instance.CloseSocket(Rall.MessageSend.LoginOut);
			}
			UINameSpace.UIWaitting.AddShowWaitting("QuickJame");
			Rall.MessageSend.QuickJoinGame(roomIdshow);
        }

		private void ResetInfo()
		{
			numberList.Clear();
			UpNumberValue();
		}

		private void OnReste()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			ResetInfo();
		}

		private void OnDelete()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count > 0)
            {
                numberList.RemoveAt(numberList.Count - 1);
            }
            UpNumberValue();
        }

                     
        private void OnTapNumber_0()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(0);

                UpNumberValue();
            }
        }

        private void OnTapNumber_1()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(1);

                UpNumberValue();
            }
        }


        private void OnTapNumber_2()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(2);

                UpNumberValue();
            }
        }

        private void OnTapNumber_3()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(3);

                UpNumberValue();
            }
        }
        
        private void OnTapNumber_4()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(4);

                UpNumberValue();
            }
        }

        private void OnTapNumber_5()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(5);

                UpNumberValue();
            }
        }

        private void OnTapNumber_6()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(6);

                UpNumberValue();
            }
        }

        private void OnTapNumber_7()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(7);

                UpNumberValue();
            }
        }

        private void OnTapNumber_8()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(8);

                UpNumberValue();
            }
        }

        private void OnTapNumber_9()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (numberList.Count < 5)
            {
                numberList.Add(9);

                UpNumberValue();
            }
        }


        private void OnClickClose()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			CloseUI();
        }


        public void CloseUI()
        {
                      FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIQuickJoin_Rall, eCloseType.Queue);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            numberBtn[0].onClick.RemoveAllListeners();
            numberBtn[1].onClick.RemoveAllListeners();
            numberBtn[2].onClick.RemoveAllListeners();
            numberBtn[3].onClick.RemoveAllListeners();
            numberBtn[4].onClick.RemoveAllListeners();
            numberBtn[5].onClick.RemoveAllListeners();
            numberBtn[6].onClick.RemoveAllListeners();
            numberBtn[7].onClick.RemoveAllListeners();
            numberBtn[8].onClick.RemoveAllListeners();
            numberBtn[9].onClick.RemoveAllListeners();
            btnBack.onClick.RemoveAllListeners();
            btnReset.onClick.RemoveAllListeners();
            btnDelete.onClick.RemoveAllListeners();
        }
    }
}
