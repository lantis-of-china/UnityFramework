using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace IMClub
{
    /// <summary>
    /// 亲友圈面包男
    /// </summary>
    public class ClubChatPanel_Select : TablePanelItem
    {
        /// <summary>
        /// 亲友圈聊天面板
        /// </summary>
        public static ClubChatPanel_Select clubChatPanel;
        /// <summary>
        /// 聊天节点资源
        /// </summary>
        public GameObject msgNodeSource;
        /// <summary>
        /// 这里是滚动组件
        /// </summary>
        public ScrollRect scrollRect;
        /// <summary>
        /// 输入字段
        /// </summary>
        public InputField inputField;
        /// <summary>
        /// 发送
        /// </summary>
        public Button btnSend;
        /// <summary>
        /// 对话预制池
        /// </summary>
        public List<ClubChatItem> clubChatItemPoolList = new List<ClubChatItem>();
        /// <summary>
        /// 显示消息索引
        /// </summary>
        public int showIndex;
        public Vector2 v2Pos;
        /// <summary>
        /// 新的Y起点
        /// </summary>
        public float newY;

        /// <summary>
        /// 表情
        /// </summary>
        public Button btnEmoji;

        /// <summary>
        /// 获取面板信息
        /// </summary>
        public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();
            clubChatPanel = this;

            inputField = GenericityTool.GetComponentByPath<InputField>(tablePanel, "input_sendMsg");
            btnSend = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_Send");
            btnSend.onClick.AddListener(OnClickSend);

            msgNodeSource = GenericityTool.GetObjectByPath(tablePanel, "chatMsgPanel/Viewport/Content/msgNode");
            msgNodeSource.SetActive(false);
            scrollRect = GenericityTool.GetComponentByPath<ScrollRect>(tablePanel, "chatMsgPanel");

            btnEmoji = GenericityTool.GetComponentByPath<Button>(tablePanel, "btn_emoji");
            btnEmoji.onClick.AddListener(OnClickSendEmoji);

            EmojiPanel.GetUI(GenericityTool.GetObjectByPath(tablePanel, "emojiPanel"));

            //这里需要初始化6个出来
            for (int i = 0; i < 6; ++i)
            {
                AddNewMsgPrefabNode();
            }

            scrollRect.onValueChanged.AddListener(UpMsgShow);
        }

        public void OnClickSendEmoji()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			EmojiPanel.SetActive(true);
        }

        /// <summary>
        /// 添加一个新的消息预制体
        /// </summary>
        public ClubChatItem AddNewMsgPrefabNode()
        {
            GameObject msgPrefabNode = GameObject.Instantiate(msgNodeSource);
            msgPrefabNode.transform.SetParent(msgNodeSource.transform.parent);
            msgPrefabNode.transform.localScale = Vector3.one;
            msgPrefabNode.transform.localPosition = Vector3.zero;
            msgPrefabNode.SetActive(false);
            ClubChatItem clubChatItem = new ClubChatItem();
            clubChatItemPoolList.Add(clubChatItem);

            clubChatItem.itemNode = msgPrefabNode;
            clubChatItem.scrollRect = scrollRect;
            clubChatItem.RegistUpPos();
            clubChatItem.isUse = false;

            return clubChatItem;
        }

        /// <summary>
        /// 获取没有使用的亲友圈聊天条
        /// </summary>
        /// <returns></returns>
        public ClubChatItem GetNotUseClubChat()
        {
            for (int i = 0; i < clubChatItemPoolList.Count; ++i)
            {
                if (!clubChatItemPoolList[i].isUse)
                {
                    clubChatItemPoolList[i].isUse = true;
                    return clubChatItemPoolList[i];
                }
            }

            ClubChatItem msgPrefabItem = AddNewMsgPrefabNode();
            msgPrefabItem.isUse = true;

            return msgPrefabItem;
        }

        /// <summary>
        /// 回收所有的消息显示
        /// </summary>
        public void ClearClubChatPrefabs()
        {
            for (int i = 0; i < clubChatItemPoolList.Count; ++i)
            {
                if (clubChatItemPoolList[i].isUse)
                {
                    clubChatItemPoolList[i].isUse = false;
                    clubChatItemPoolList[i].SetActive(false);
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void OnClickSend()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			if (string.IsNullOrEmpty(inputField.text))
            {
                UINameSpace.UITipMessage.PlayMessage("发送的消息不能为空!");
                return;
            }

            if (ClubItem.clubItemState == null)
            {
                UINameSpace.UITipMessage.PlayMessage("选中的对话组群不存在!");
                return;
            }

            string sendText = inputField.text;
            inputField.text = "";
            Debug.Log("send message");
            IMCludWarp.SendTextMessage(sendText, ClubItem.clubItemState.SelfMessage);
        }

        /// <summary>
        /// 选中了这个Item
        /// </summary>
        public override void OnSelect()
        {
            base.OnSelect();

            ClearMsgShow();
            ResetShow();
        }

        /// <summary>
        /// 外部调用接口
        /// </summary>
        /// <param name="parmaras"></param>
        public override void ExitCall(object parmaras)
        {
            base.ExitCall(parmaras);
        }

        /// <summary>
        /// 清理消息显示
        /// </summary>
        public void ClearMsgShow()
        {
            newY = 0;
            showIndex = -1;
            ClearClubChatPrefabs();
            SeetPos();
        }

        /// <summary>
        /// 把位置设置到0
        /// </summary>
        public void SeetPos()
        {
            v2Pos = Vector2.zero;
            scrollRect.verticalNormalizedPosition = 1.0f;
            scrollRect.content.sizeDelta = Vector2.zero;
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        public void AddMsgRefence()
        {
            if (ClubItem.clubItemState == null || ClubItem.clubItemState.clubMsgList == null || (showIndex + 1) >= ClubItem.clubItemState.clubMsgList.Count)
            {
                return;
            }

            ClubMsgStruct clubMsg = ClubItem.clubItemState.clubMsgList[showIndex + 1];
            showIndex++;

            bool isSelf = false;
            if (clubMsg.senderId == GoableData.userValiadateInfor.DatingNumber)
            {
                isSelf = true;
            }

            ClubChatItem cci = GetNotUseClubChat();
            cci.itemNode.name = "showIndex_" + showIndex;
            cci.GetUI(cci.itemNode, isSelf);
            float height = cci.ShowMsg(clubMsg);
            cci.SetPos(newY);
            newY -= height;

            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, -newY);
            scrollRect.verticalNormalizedPosition = 0.0f;
        }

        /// <summary>
        /// 刷新显示数据
        /// </summary>
        public void UpMsgShow(Vector2 pos)
        {
            v2Pos = pos;
        }

        /// <summary>
        /// 重置显示
        /// </summary>
        public void ResetShow()
        {
            if (ClubItem.clubItemState == null || ClubItem.clubItemState.clubMsgList == null)
            {
                return;
            }

            ClearMsgShow();
            bool isFirst = true;
            while ((isFirst && showIndex < ClubItem.clubItemState.clubMsgList.Count)
                || (showIndex >= 0 && showIndex < (ClubItem.clubItemState.clubMsgList.Count - 1)))
            {
                isFirst = false;
                AddMsgRefence();
            }
        }
    }

    /// <summary>
    /// 表情界面
    /// </summary>
    public class EmojiPanel
    {
        /// <summary>
        /// 面板节点
        /// </summary>
        public static GameObject panelNode;

        public static GameObject gradNode;

        /// <summary>
        /// 表情按钮
        /// </summary>
        public static List<MsgEmojiItem> emoji = new List<MsgEmojiItem>();

        public static void GetUI(GameObject node)
        {
            panelNode = node;

            emoji.Clear();
            //初始化
            for (int i = 0; i < 34; i++)
            {
                gradNode = GenericityTool.GetObjectByPath(panelNode, "Scroll View/Viewport/Content/Grad");
                GameObject item = GenericityTool.GetObjectByPath(gradNode, "img_" + i.ToString());
                MsgEmojiItem msgEmojiItem = new MsgEmojiItem();
                msgEmojiItem.GetUI(item,i);
                msgEmojiItem.SetActive(true);
                emoji.Add(msgEmojiItem);
            }
        }

        public static void OnClickClose()
        {
            SetActive(false);
        }

        public static void SetActive(bool active)
        {
            panelNode.SetActive(active);
        }

    }
    /// <summary>
    /// 按钮属性
    /// </summary>
    public class MsgEmojiItem
    {
        public GameObject item_node;

        public Button selectBtn;

        public Image img;

        public int item_id;

        public string imgName;

        public void GetUI(GameObject node,int id)
        {
            item_node = node;
            item_id = id;
            img = item_node.GetComponent<Image>();
            imgName = img.sprite.name;
            selectBtn = item_node.GetComponent<Button>();
            selectBtn.onClick.AddListener(OnClickSendEmojiMsg);
        }

        public void SetActive(bool active)
        {
            item_node.SetActive(active);
        }

        public void OnClickSendEmojiMsg()
        {
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			EmojiPanel.OnClickClose();
            ClubChatPanel_Select.clubChatPanel.inputField.text +=  "[#" + imgName + "]";
        }
    }


}


