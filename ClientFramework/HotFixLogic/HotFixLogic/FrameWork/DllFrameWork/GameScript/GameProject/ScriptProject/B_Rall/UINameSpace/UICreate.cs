using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace UINameSpace
{
    public class UICreate : UIObject
    {

        public GameObject animationNode_LB;
        public GameObject animationNode_RT;
        public GameObject animationNode_RB;

        public InputField _inputField;
        public Button _btnSelectBoy;
        public Button _btnSelectGirl;
        public Button _btnSubmit;
        public Button _btnRandom;
        public GameObject selectObject;
        public Text text_Tip;
        private short _sex = 1;

        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UICreate_Rall, _className);

            return 1;
        }

        public UICreate()
        {
            assetsName = Rall.UIDefineName.UICreate_Rall;
        }
        
        public override void OnAwake()
        {
            animationNode_LB = GenericityTool.GetObjectByPath(objectInstance, "anchorLB/animationNode");
            animationNode_RT = GenericityTool.GetObjectByPath(objectInstance, "anchorRT/animationNode");
            animationNode_RB = GenericityTool.GetObjectByPath(objectInstance, "anchorRB/animationNode");

            _inputField = GenericityTool.GetComponentByPath<InputField>(animationNode_RB, "inputName");
            _btnSelectBoy = GenericityTool.GetComponentByPath<Button>(animationNode_RB, "btnSelectBoy");
            _btnSelectGirl = GenericityTool.GetComponentByPath<Button>(animationNode_RB, "btnSelectGirl");
            _btnSubmit = GenericityTool.GetComponentByPath<Button>(animationNode_RB, "btnSubmit");
            _btnRandom = GenericityTool.GetComponentByPath<Button>(animationNode_RB, "brnRadom");
            selectObject = GenericityTool.GetObjectByPath(animationNode_RB, "curSelect");
            text_Tip = GenericityTool.GetComponentByPath<Text>(animationNode_RB, "text_tip");

            if (_btnSelectBoy != null) _btnSelectBoy.onClick.AddListener(OnSelectBoy);
            if (_btnSelectGirl != null) _btnSelectGirl.onClick.AddListener(OnSelectGirl);
            if (_btnSubmit != null) _btnSubmit.onClick.AddListener(OnSubmit);
            _btnRandom.onClick.AddListener(GetRandomName);

            SetSelectPos();
            text_Tip.text = "";

            GetRandomName();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            PlayIn(0.5f);
        }

        /// <summary>
        /// 播放进入动画
        /// </summary>
        public void PlayIn(float waitTime)
        {
            CherishTweenMove.Begin(animationNode_LB, new Vector3(-666, 0, 0), Vector3.zero, 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_RT, new Vector3(0, 300, 0), Vector3.zero, 0.3f, waitTime, true);
            CherishTweenMove.Begin(animationNode_RB, new Vector3(0, -800, 0), Vector3.zero, 0.3f, waitTime, true);
        }







        public void TipShow(int state)
        {
            if (state == 1)
            {
                //角色已经存在
                text_Tip.text = "角色已经存在";
                FrameWorkDrvice.UiManagerInstance.OpenUI(Rall.ConfigProject.projectFloderName, Rall.UIDefineName.UIRall_Rall, true);
                FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UICreate_Rall, eCloseType.Queue);
            }
            else if (state == 2)
            {
                //角色名被占用
                text_Tip.text = "角色名被占用";
            }
            else
            {
                //知道错误
                text_Tip.text = "未知错误";
            }
        }

        /// <summary>
        /// 获取游客名字
        /// </summary>
        public void GetRandomName()
        {
            string nickName = "游客_"
                + System.DateTime.Now.Year.ToString().Replace("201", "")
                + System.DateTime.Now.Month.ToString()
                + System.DateTime.Now.Day.ToString()
                + System.DateTime.Now.Hour.ToString()
                + System.DateTime.Now.Minute.ToString()
                + System.DateTime.Now.Second.ToString()
                + Random.Range(1,9);

            _inputField.text = nickName;
        }


        private void OnSelectBoy()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            _sex = 1;
            SetSelectPos();
        }

        private void OnSelectGirl()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            _sex = 2;
            SetSelectPos();
        }

        private void SetSelectPos()
        {
            if(_sex == 1)
            {
                selectObject.transform.position = _btnSelectBoy.gameObject.transform.position;
            }
            else
            {
                selectObject.transform.position = _btnSelectGirl.gameObject.transform.position;
            }
        }

        private void OnSubmit()
        {
            FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
            GoableData.userValiadateInforWarp.Sex = _sex;
            GoableData.userValiadateInforWarp.PikeName = _inputField.text;

			Rall.MessageSend.CreateRole(_inputField.text, _sex);
        }

    }
}
