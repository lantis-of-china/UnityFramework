using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SingleMoba;
using CherishDelay;

namespace UINameSpace
{
    /// <summary>
    /// 纸牌水果机
    /// </summary>
    public class UISingleMobaFight : UIObject
    {
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(UIDefineName.UIFight, _className);

            return 1;
        }

        #region 单例
        private static UISingleMobaFight __Instance;

        public static UISingleMobaFight GetInstance()
        {
            return __Instance;
        }
        public override void SetInstance(UIObject target)
        {
            __Instance = target as UISingleMobaFight;
        }
        #endregion 单例

        #region CherishFrameworkInterface
        private Text rankLow;
        private Text rankHight;
        private GameObject rankMaxPosNode;
        private GameObject rankItemNode;
        private Vector3 movePositionOffset;
        private Vector3 touchDownPos;
        private RectTransform moveHandleRectTransform;
        private EventTrigger moveHandleTrigger;
        private GameObject moveTouch;
        private GameObject moveTouchGround;
        private GameObject moveTouchSport;

        private Text killCount;
        private Text scoreCount;
        private Image hpBar;
        private Text hpValue;
        private Image powerBar;
        private Text powerValue;
        private Text boomCount;
        private Button boomButton;
        private Button buttonAttack;
        private Button buttonJumpMove;

        public override void OnRegistEvent()
        {
            base.OnRegistEvent();
            LocalEventNotices.Regist(EventNoticesDefine.PlayerStateChange,RefenceData);
            LocalEventNotices.Regist(EventNoticesDefine.ChactarJoin, CharacterJoin);
        }

        public override void OnUnRegistEvent()
        {
            base.OnUnRegistEvent();
            LocalEventNotices.UnRegist(EventNoticesDefine.PlayerStateChange, RefenceData);
            LocalEventNotices.UnRegist(EventNoticesDefine.ChactarJoin, CharacterJoin);
        }

        public override void OnAwake()
        {
            rankLow = GenericityTool.GetComponentByPath<Text>(objectInstance, "rankView/rankLow");
            rankHight = GenericityTool.GetComponentByPath<Text>(objectInstance, "rankView/rankHight");
            rankMaxPosNode = GenericityTool.GetObjectByPath(objectInstance, "rankView/rankGamer/gamerHight");
            rankItemNode = GenericityTool.GetObjectByPath(objectInstance, "rankView/rankGamer/gamerView");
            moveHandleRectTransform = GenericityTool.GetComponentByPath<RectTransform>(objectInstance, "moveHandle");
            moveHandleTrigger = GenericityTool.GetComponentByPath<EventTrigger>(objectInstance, "moveHandle/eventBack");
            moveTouch = GenericityTool.GetObjectByPath(objectInstance, "moveHandle/moveTouch");
            moveTouchGround = GenericityTool.GetObjectByPath(objectInstance, "moveHandle/moveTouch/moveEventTrigger");
            moveTouchSport = GenericityTool.GetObjectByPath(objectInstance, "moveHandle/moveTouch/touch");
            AddEventTriggerEvent(moveHandleTrigger, EventTriggerType.PointerDown, OnTriggerTouchDown);
            AddEventTriggerEvent(moveHandleTrigger, EventTriggerType.PointerUp, OnTriggerTouchUp);
            AddEventTriggerEvent(moveHandleTrigger, EventTriggerType.Drag, OnTriggerTouchDrag);
            CloseTouch();

            killCount = GenericityTool.GetComponentByPath<Text>(objectInstance, "killView/killCount");
            scoreCount = GenericityTool.GetComponentByPath<Text>(objectInstance, "boomView/boomCount");
            hpBar = GenericityTool.GetComponentByPath<Image>(objectInstance, "hpNode/bar");
            hpValue = GenericityTool.GetComponentByPath<Text>(objectInstance, "hpNode/text");
            powerBar = GenericityTool.GetComponentByPath<Image>(objectInstance, "propView/powerBar");
            powerValue = GenericityTool.GetComponentByPath<Text>(objectInstance, "propView/powerValur");
            boomCount = GenericityTool.GetComponentByPath<Text>(objectInstance, "propView/hasBoomCount");
            boomButton = GenericityTool.GetComponentByPath<Button>(objectInstance, "propView/useData/imgIcon");
            buttonAttack = GenericityTool.GetComponentByPath<Button>(objectInstance, "skillView/skillLeft");
            buttonJumpMove = GenericityTool.GetComponentByPath<Button>(objectInstance, "skillView/skillRight");

            boomButton.onClick.AddListener(OnClickBoom);
            buttonAttack.onClick.AddListener(OnClickAttack);
            buttonJumpMove.onClick.AddListener(OnClickJumpMove);
        }

        public override void OnEnable()
        {
            RefenceData(null);
            LoadCharacterRankItem();
        }

        public override void OnUpdate()
        {
            FsmSystem.FsmSystemDiv.Update();
        }

        public override void OnDisable()
        {
        }

        public override void OnDispose()
        {
        }

        public override void OnClose()
        {
        }

        public Vector3 GetMovePosition()
        {
            return movePositionOffset;
        }

        public void CloseTouch()
        {
            moveTouch.SetActive(false);
            movePositionOffset = Vector3.zero;
        }

        public void SetTouchPostDownNew(Vector3 downPos)
        {
            moveTouchGround.GetComponent<RectTransform>().anchoredPosition = downPos - moveTouch.GetComponent<RectTransform>().anchoredPosition3D;
            touchDownPos = downPos;
        }

        public void SetTouchToPos(Vector3 position)
        {
            moveTouch.SetActive(true);
            moveTouchGround.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            touchDownPos = position;
            moveTouch.GetComponent<RectTransform>().anchoredPosition = position;
            moveTouchSport.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        public void SetTouchOffset(Vector3 offset)
        {
            movePositionOffset = offset - touchDownPos;
            moveTouchSport.GetComponent<RectTransform>().anchoredPosition = offset - moveTouch.GetComponent<RectTransform>().anchoredPosition3D;
        }

        public void LoadCharacterRankItem()
        {
            var characterList = SingleMoba.CharacterManager.Instance.GetCharacterAll();

            if (characterList != null)
            {
                for (var i = 0; i < characterList.Count; ++i)
                {
                    var character = characterList[i];
                    CharacterJoin(character);
                }
            }
        }

        public void CharacterJoin(object paramar)
        {
            SingleMoba.UIRangItem.AddItem((CharacterBase)paramar, rankItemNode);
            RefenceRankItemPosition();
        }

        public void RefenceData(object paramar)
        {
            var selfPlayerInfo = LogicDataSpace.GetSelfData();
            killCount.text = selfPlayerInfo.kill.ToString();
            scoreCount.text = selfPlayerInfo.score.ToString();
            hpValue.text = $"{selfPlayerInfo.hp}/{selfPlayerInfo.maxHp}";
            hpBar.fillAmount = (float)selfPlayerInfo.hp / selfPlayerInfo.maxHp;
            boomCount.text = selfPlayerInfo.boom.ToString();
            powerValue.text = $"{selfPlayerInfo.power}/1000";
            powerBar.fillAmount = (float)selfPlayerInfo.power / 1000;
            RefenceRankItemPosition();
        }

        public void RefenceRankItemPosition()
        {
            SingleMoba.UIRangItem.UpdateSetPosition(0.0f,rankMaxPosNode.transform.localPosition.x,rankLow,rankHight);
        }
        #endregion CherishFrameworkInterface

        #region 事件
        public void OnTriggerTouchDown(BaseEventData baseEvent)
        {
            PointerEventData ped = baseEvent as PointerEventData;

            if (ped == null)
            {
                return;
            }

            SetTouchToPos(ped.position);
        }

        /// <summary>
        /// 拖拽
        /// </summary>
        /// <param name="baseEvent"></param>
        public void OnTriggerTouchDrag(BaseEventData baseEvent)
        {
            PointerEventData ped = baseEvent as PointerEventData;

            var currentSportPos = new Vector3(ped.position.x, ped.position.y, 0.0f);
            var offset =  touchDownPos - currentSportPos;
            var distance = Vector3.Distance(currentSportPos, touchDownPos);

            if (distance > 200)
            {
                var newTouchDownPos = offset.normalized * 200 + currentSportPos;
                SetTouchPostDownNew(newTouchDownPos);
            }
            
            SetTouchOffset(currentSportPos);
        }

        public void OnTriggerTouchUp(BaseEventData baseEvent)
        {
            CloseTouch();
        }

        private void OnClickBoom()
        {
            TriggerSkill(10002);
        }

        private void OnClickAttack()
        {
            TriggerSkill(10001);
        }

        private void OnClickJumpMove()
        {
            TriggerSkill(10003);
        }

        private void TriggerSkill(int skillId)
        {
            var pos = SingleMoba.CharacterManager.Instance.GetMainCharacter().GetPos();
            var dir = SingleMoba.CharacterManager.Instance.GetMainCharacter().GetDirection();
            var configSkillData = LocalConfigLoader.configSkill.GetKey(skillId);

            if (configSkillData != null)
            {
                var targetPos = pos;                

                if (configSkillData.moveType == 0)
                {
                    targetPos = pos;
                }
                else if(configSkillData.moveType == 1)
                {
                    targetPos = dir * configSkillData.moveDistance + pos;
                }
                else if (configSkillData.moveType == 2)
                {
                    targetPos = -dir * configSkillData.moveDistance + pos;
                }

                MessageSend.UseSkill(configSkillData.key, pos.x, pos.z, targetPos.x, targetPos.z);
            }
        }
        #endregion 事件

        #region NetEvent
        #endregion NetEvent
    }
}

namespace SingleMoba
{
    public class UIRangItem
    {
        private static int minScore = -1;
        private static int maxScore = -1;
        private static List<UIRangItem> items = new List<UIRangItem>();

        public static void AddItem(CharacterBase character,GameObject itemSource)
        {
            var itemWrap = new UIRangItem();
            itemWrap.bindCharacter = character;
            var itemNode = GameObject.Instantiate(itemSource);
            itemNode.transform.SetParent(itemSource.transform.parent);
            itemNode.transform.localScale = Vector3.one;
            itemWrap.GetUI(itemNode);
            items.Add(itemWrap);
        }

        public static void UpdateSetPosition(float minX,float maxX,Text textLow,Text textHeight)
        {
            minScore = 0;
            maxScore = 0;

            for (var i = 0; i < items.Count; ++i)
            {
                var item = items[i];

                if (item.bindCharacter.IsLife())
                {
                    var score = item.bindCharacter.GetPlayerData().power;

                    if (minScore == -1 && maxScore == -1)
                    {
                        maxScore = score;
                        minScore = score;
                    }
                    else
                    {
                        if (score < minScore)
                        {
                            minScore = score;
                        }

                        if (score > maxScore)
                        {
                            maxScore = score;
                        }
                    }
                }
            }

            textLow.text = $"最低 {minScore}";
            textHeight.text = $"最高 {maxScore}";
            var limit = maxScore - minScore;
            var xSpace = maxX - minX;

            for (var i = 0; i < items.Count; ++i)
            {
                var item = items[i];

                if (item.bindCharacter.IsLife())
                {
                    var score = item.bindCharacter.GetPlayerData().power;
                    var scale = (float)(score - minScore) / (float)limit;
                    var offsetX = scale * xSpace + minX;
                    item.SetPosition(offsetX);
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }

        public GameObject node;

        public Image icon;

        public Image array;

        public CharacterBase bindCharacter;

        public void GetUI(GameObject setNode)
        {
            node = setNode;
        }

        public void SetPosition(float x)
        {
            CherishTweenMove.Begin(node, node.transform.localPosition, new Vector3(x, 0.0f, 0.0f), 0.5f, 0.0f, true); 
        }

        public void SetActive(bool active)
        {
            node.SetActive(active);
        }

        public void Dispose()
        {
            if (node != null)
            {
                node.SetActive(false);
                GameObject.Destroy(node);
            }
        }
    }
}