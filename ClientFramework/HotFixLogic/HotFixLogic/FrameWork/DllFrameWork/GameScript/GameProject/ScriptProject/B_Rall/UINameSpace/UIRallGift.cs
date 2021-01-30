using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UINameSpace
{
    public class GiftItem
    {
        public GameObject objectInstance;

        public void GetComp(GameObject instance)
        {
            objectInstance = instance;
        }
    }

    public class UIRallGift : UIObject
    {

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIRallGift_Rall, _className);
            return 1;
        }


        public UIRallGift()
        {
            assetsName = Rall.UIDefineName.UIRallGift_Rall;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public Button btnClose;

        /// <summary>
        /// 显示的列表
        /// </summary>
        public List<GiftItem> giftList = new List<GiftItem>();

        /// <summary>
        /// 剩余数量信息
        /// </summary>
        public Text lb_hasTimesInfo;

        /// <summary>
        /// 进入获取奖品界面
        /// </summary>
        public Button btnEntryGetGif;

        /// <summary>
        /// 礼品跟节点
        /// </summary>
        public GameObject objGiftRoundRoot;

        /// <summary>
        /// 礼品节点
        /// </summary>
        public GameObject objGiftRound;

        /// <summary>
        /// 运行或者停止
        /// </summary>
        public Button btnStartOrStop;

        /// <summary>
        /// 是否开始抽奖
        /// </summary>
        private bool startRun = false;
        private bool runEnd = true;

        public override void OnAwake()
        {
            base.OnAwake();
            startRun = false;
            runEnd = true;

            GiftItem centerItem_1 = new GiftItem();
            centerItem_1.GetComp(GenericityTool.GetObjectByPath(objectInstance, "Gifts/ItemCloneCenter"));

            GiftItem centerItem_2 = new GiftItem();
            centerItem_2.GetComp(GenericityTool.GetObjectByPath(objectInstance, "Gifts/ItemCloneLeft"));

            GiftItem centerItem_3 = new GiftItem();
            centerItem_3.GetComp(GenericityTool.GetObjectByPath(objectInstance, "Gifts/ItemCloneRight"));

            giftList.Add(centerItem_1);
            giftList.Add(centerItem_2);
            giftList.Add(centerItem_3);

            btnEntryGetGif = GenericityTool.GetComponentByPath<Button>(objectInstance, "Gifts/btnEntryGetGif");

            lb_hasTimesInfo = GenericityTool.GetComponentByPath<Text>(objectInstance, "topInfo/lb_hasTimeInfo");

            objGiftRoundRoot = GenericityTool.GetObjectByPath(objectInstance, "CheckItem");

            objGiftRound = GenericityTool.GetObjectByPath(objectInstance, "CheckItem/Image");

            btnStartOrStop = GenericityTool.GetComponentByPath<Button>(objectInstance,"CheckItem/btnStarAntStop");

            btnEntryGetGif.onClick.AddListener(OnEntryGift);
            btnStartOrStop.onClick.AddListener(OnStartOrStop);

            btnClose = GenericityTool.GetComponentByPath<Button>(objectInstance, "btnClose");
            btnClose.onClick.AddListener(CloseUI);


            objGiftRoundRoot.SetActive(false);
        }

        private void CloseUI()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIRallGift_Rall,eCloseType.None);
        }

        /// <summary>
        /// 点击进入抽奖
        /// </summary>
        private void OnEntryGift()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			btnEntryGetGif.gameObject.SetActive(false);
            objGiftRoundRoot.SetActive(true);

            //giftList[1].objectInstance.SetActive(false);
            //giftList[2].objectInstance.SetActive(false);
            CherishTweenMove.Begin(giftList[1].objectInstance, giftList[1].objectInstance.transform.localPosition, giftList[1].objectInstance.transform.localPosition + new Vector3(0,1000,0), 0.3f, 0.0f, true);
            CherishTweenMove.Begin(giftList[2].objectInstance, giftList[2].objectInstance.transform.localPosition, giftList[2].objectInstance.transform.localPosition + new Vector3(0, 1000, 0), 0.3f, 0.0f, true);


            CherishTweenMove.Begin(giftList[0].objectInstance, giftList[0].objectInstance.transform.localPosition, giftList[1].objectInstance.transform.localPosition, 0.3f, 0.0f, true);

            CherishTweenScale.Begin(giftList[0].objectInstance, giftList[0].objectInstance.transform.localScale, giftList[1].objectInstance.transform.localScale, 0.3f, 0.0f);


            CherishTweenMove.Begin(objGiftRound, objGiftRound.transform.localPosition, Vector3.zero, 0.3f, 0.0f, true);
        }



       
        /// <summary>
        /// 开始停止抽奖
        /// </summary>
        private void OnStartOrStop()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			if (startRun)
            {
                startRun = false;
            }
            else if(!startRun)
            {
                //判断次数
                startRun = true;
                runEnd = false;
            }
        }


        public float maxSpeed = 500.0f;

        public float addSpeed = 100.0f;
        /// <summary>
        /// 速度
        /// </summary>
        public float speed;
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (!runEnd)
            {
                if (startRun == true)
                {
                    if (speed != maxSpeed)
                    {
                        if (speed < maxSpeed)
                        {
                            speed += addSpeed * Time.deltaTime;
                        }
                        else
                        {
                            speed = maxSpeed;
                        }
                    }
                }
                else
                {
                    if (speed != 0)
                    {
                        if (speed > 0)
                        {
                            speed -= addSpeed * Time.deltaTime;
                        }
                        else
                        {
                            speed = 0;
                            runEnd = true;
                        }
                    }
                }

                objGiftRound.gameObject.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
            }
        }
    }
}
