using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace IMClub
{
    /// <summary>
    /// 消息结点
    /// </summary>
    public class ClubChatMsgBase
    {
		/// <summary>
		/// 消息总节点
		/// </summary>
		public GameObject baseNode;
        /// <summary>
        /// 宽度
        /// </summary>
        public float widthBk;
        /// <summary>
        /// 高度
        /// </summary>
        public float heghtBk;
        /// <summary>
        /// 消息结点
        /// </summary>
        public GameObject msgNode;
        /// <summary>
        /// 背景
        /// </summary>
        public Image backGround;
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void GetUI(GameObject node,GameObject bnode)
        {
			baseNode = bnode;
			msgNode = node;

            backGround = GenericityTool.GetComponentByPath<Image>(msgNode, "imgBack");
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        public virtual void SetBackGroundSize(float width, float heigth)
        {
            widthBk = width;
            heghtBk = heigth;
        }

        /// <summary>
        /// 设置消息是否显示
        /// </summary>
        public virtual void SetMsgActive(bool active)
        {
            msgNode.SetActive(active);
        }

        /// <summary>
        /// 获取尺寸
        /// </summary>
        /// <returns></returns>
        public virtual Vector2 GetSize()
        {
            return Vector2.zero;
        }
    }

    /// <summary>
    /// 亲友圈文本消息
    /// </summary>
    public class ClubTextMsg : ClubChatMsgBase
    {
        /// <summary>
        /// 消息文本
        /// </summary>
        public InlineText msgText;
        public override void GetUI(GameObject node,GameObject bnode)
        {
            base.GetUI(node, bnode);

            msgText = GenericityTool.GetComponentByPath<InlineText>(msgNode, "txtMsg");
        }

        /// <summary>
        /// 设置消息显示
        /// </summary>
        /// <param name="text"></param>
        public void SetMsgShow(string text)
        {
            //正则匹配
            int invateX = 180;
            string pattern = @"\[(\-{0,1}\d{0,})#(.+?)\]";

            MatchCollection matchCollection = Regex.Matches(text, pattern);
            DebugLoger.Log("matchCollection.Count count " + matchCollection.Count);
            msgText.text = text;
			backGround.rectTransform.sizeDelta = new Vector2(msgText.preferredWidth + 58.0f, msgText.preferredHeight + 10.0f);
			backGround.gameObject.SetActive(false);
			IEnumeratorManager.Instance.StartCoroutine(UpSizeBack());
		}

		private IEnumerator UpSizeBack()
		{
			yield return new IEnumeratorManager.WaitForSeconds(0.01f);
			if (baseNode == null)
			{
				yield break;
			}
			backGround.rectTransform.sizeDelta = new Vector2(msgText.preferredWidth + 58.0f, msgText.preferredHeight + 10.0f);
			backGround.gameObject.SetActive(true);

			CherishTweenScale.Begin(baseNode, new Vector3(0.1f, 0.1f, 0.1f), Vector3.one, 0.2f, 0.0f);
		}


        /// <summary>
        /// 获取尺寸
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetSize()
        {
            return backGround.rectTransform.sizeDelta + new Vector2(0, 100.0f);
        }
    }

    /// <summary>
    /// 战绩消息
    /// </summary>
    public class ClubGradeMsgItem
    {
        public GameObject itemNode;
        public Image img_head;
        public Text txt_name;
        public Text txt_grade;

        public void GetUI(GameObject node)
		{
			itemNode = node;
            img_head = GenericityTool.GetComponentByPath<Image>(node, "imgHead");
            txt_name = GenericityTool.GetComponentByPath<Text>(node, "txtName");
            txt_grade = GenericityTool.GetComponentByPath<Text>(node, "txtGrade");
        }

        public void SetParent(GameObject parent)
        {
            itemNode.transform.SetParent(parent.transform);
        }

        public void SetPos()
        {
            itemNode.transform.localPosition = Vector3.zero;
            itemNode.transform.localScale = Vector3.one;
        }

        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
        }

        public void Despose()
        {
            if(itemNode != null)
            {
                GameObject.Destroy(itemNode);
            }
        }
    }

    /// <summary>
    /// 亲友圈战绩消息
    /// </summary>
    public class ClubGradeMsg : ClubChatMsgBase
    {
        public GameObject item_source;
        public Text txt_roomid;
        public Image gradeBackGround;

        public List<ClubGradeMsgItem> itemList = new List<ClubGradeMsgItem>();

        public override void GetUI(GameObject node,GameObject bnode)
        {
            base.GetUI(node,bnode);

            txt_roomid = GenericityTool.GetComponentByPath<Text>(node, "txtRoomID");
            gradeBackGround = GenericityTool.GetComponentByPath<Image>(node, "imgBack");
            item_source = GenericityTool.GetObjectByPath(node, "node/item_source");
            item_source.SetActive(false);
        }


        public void ClearItems()
        {
            for (int i = 0; i < itemList.Count; ++i)
            {
                itemList[i].Despose();
            }

            itemList.Clear();
        }

        public void SetMsgShow(string text)
        {
            ClearItems();

            Server.SC_ClubScoreBack clubScoreData = new Server.SC_ClubScoreBack();
            clubScoreData.DeserializerJson(text);

            txt_roomid.text = clubScoreData.roomId.ToString();
            for (int i = 0;i < clubScoreData.clubChangeItem.Count;++i)
            {
                GameObject item = GameObject.Instantiate(item_source);
                item.name = "item_" + i.ToString();
                ClubGradeMsgItem clubgrademsgitem = new ClubGradeMsgItem();
                clubgrademsgitem.GetUI(item);
                clubgrademsgitem.SetActive(true);
                clubgrademsgitem.SetParent(item_source.transform.parent.gameObject);
                clubgrademsgitem.SetPos();
                itemList.Add(clubgrademsgitem);

                P_Menber clubMenberInfo = GoableClubDataInfo.GetMenberFromGroup(clubScoreData.clubChangeItem[i].menberId, clubScoreData.clubId);
				
				if (clubMenberInfo == null)
				{
					UINameSpace.UITipMessage.PlayMessage(string.Format("结算时未获取到[{0}]信息", clubMenberInfo.menberName));
					AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName, clubgrademsgitem.img_head, "GameEnd10");
				}
				else
				{
					if (string.IsNullOrEmpty(clubMenberInfo.headUrl))
					{
						if (clubMenberInfo.sex == 1)
						{
							AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName, clubgrademsgitem.img_head, "GameEnd10");
						}
						else
						{
							AssetsParkManager.SetUguiImageThingIcon(Rall.ConfigProject.iconsName, clubgrademsgitem.img_head, "GameEnd9");
						}
					}
					else
					{
						SetImageForHttpbytes.SetImageFromUrl(clubgrademsgitem.img_head, clubMenberInfo.headUrl);
					}
				}

                clubgrademsgitem.txt_name.text = clubMenberInfo.menberName;
                clubgrademsgitem.txt_grade.text = (clubScoreData.clubChangeItem[i].backChangeScore - clubScoreData.clubChangeItem[i].deductionScore).ToString();
            }
            
            gradeBackGround.rectTransform.sizeDelta = new Vector2(500.0f,80.0f + clubScoreData.clubChangeItem.Count * 74.0f);
			CherishTweenScale.Begin(baseNode, new Vector3(0.1f, 0.1f, 0.1f), Vector3.one, 0.2f, 0.0f);
		}

        public override Vector2 GetSize()
        {
            return gradeBackGround.rectTransform.sizeDelta + new Vector2(0, 100.0f); ;
        }
    }

    /// <summary>
    /// 聊天节点
    /// </summary>
    public class ClubChatItem
    {
        /// <summary>
        /// 是否使用
        /// </summary>
        public bool isUse;
        /// <summary>
        /// 滚动容器
        /// </summary>
        public ScrollRect scrollRect;
        /// <summary>
        /// 是否己方
        /// </summary>
        public bool isSelf;
        /// <summary>
        /// 节点
        /// </summary>
        public GameObject itemNode;
        /// <summary>
        /// 己方节点
        /// </summary>
        public GameObject selfNode;
        /// <summary>
        /// 其他节点
        /// </summary>
        public GameObject otherNode;
        /// <summary>
        /// 头像
        /// </summary>
        public CircleImage imgHead;
        /// <summary>
        /// 名字
        /// </summary>
        public Text txtName;
        /// <summary>
        /// 亲友圈文本消息
        /// </summary>
        public ClubTextMsg clubTextMsg;


        public ClubGradeMsg clubGradeMsg;
        /// <summary>
        /// 高度
        /// </summary>
        public float height;

        /// <summary>
        /// 注册更新位置
        /// </summary>
        public void RegistUpPos()
        {
            scrollRect.onValueChanged.AddListener(UpPos);
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            itemNode.SetActive(active);
            isUse = active;
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="node"></param>
        public void GetUI(GameObject node, bool self)
        {
            isSelf = self;
            itemNode = node;

            selfNode = GenericityTool.GetObjectByPath(itemNode, "selfMsgNode");
            otherNode = GenericityTool.GetObjectByPath(itemNode, "otherMsgNode");

            GetResetComp();
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        private void GetResetComp()
        {
            GameObject useNode = null;
            if (isSelf)
            {
                useNode = selfNode;
                otherNode.SetActive(false);
            }
            else
            {
                useNode = otherNode;
                selfNode.SetActive(false);
            }
            useNode.SetActive(true);

            if (clubTextMsg == null)
            {
                clubTextMsg = new ClubTextMsg();

                clubTextMsg.GetUI(GenericityTool.GetObjectByPath(useNode, "msgNode/textMsg"), GenericityTool.GetObjectByPath(useNode, "msgNode"));

			}

            if(clubGradeMsg == null)
            {
                clubGradeMsg = new ClubGradeMsg();

                clubGradeMsg.GetUI(GenericityTool.GetObjectByPath(useNode, "msgNode/gradeMsg"), GenericityTool.GetObjectByPath(useNode, "msgNode"));
            }

            imgHead = GenericityTool.GetComponentByPath<CircleImage>(useNode, "headNode/img_head");
            txtName = GenericityTool.GetComponentByPath<Text>(useNode, "headNode/txt_name");

        }

        /// <summary>
        /// 显示消息返回Y
        /// </summary>
        public float ShowMsg(ClubMsgStruct msgData)
        {
            itemNode.SetActive(true);
            Vector2 size = Vector2.zero;
            clubTextMsg.SetMsgActive(false);
            clubGradeMsg.SetMsgActive(false);


            if (msgData.msgType == (byte)0)
            {
                clubTextMsg.SetMsgShow(msgData.txtMsg);
                size = clubTextMsg.GetSize();
                clubTextMsg.SetMsgActive(true);
            }
            else if (msgData.msgType == (byte)2)
            {
                clubGradeMsg.SetMsgShow(msgData.txtMsg);
                size = clubGradeMsg.GetSize();
                clubGradeMsg.SetMsgActive(true);
            }
            string name = "";
            string headUrl = "";
            byte sex = (byte)0;
            if (msgData.senderId == GoableData.userValiadateInfor.DatingNumber)
            {
                sex = (byte)GoableData.userValiadateInforWarp.Sex;
                name = GoableData.userValiadateInforWarp.PikeName;
                headUrl = GoableData.userValiadateInforWarp.headUrl;
            }
            else
            {
                P_Menber menberInfo = null;
                if (ClubItem.clubItemState.bindGwInfo.menberList.TryGetValue(int.Parse(msgData.senderId), out menberInfo))
                {
                    sex = (byte)menberInfo.sex;
                    name = menberInfo.menberName;
                    headUrl = menberInfo.headUrl;
                }
            }

            txtName.text = name;
            if (!string.IsNullOrEmpty(headUrl))
            {
                SetCircleImageForHttpbytes.SetCircleImageFromUrl(imgHead, headUrl);
            }
            else
            {
                if (sex == 1)
                {
                    ///男
                    AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, imgHead, "GameEnd10");
                }
                else
                {
                    ///女
                    AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, imgHead, "GameEnd9");
                }
            }



            return size.y;
        }

        /// <summary>
        /// 设置Y位置
        /// </summary>
        /// <param name="y"></param>
        public void SetPos(float y)
        {
            itemNode.transform.localPosition = new Vector3(0, y, 0);
        }


        /// <summary>
        /// 更新位置
        /// </summary>
        private void UpPos(Vector2 v2)
        {
			//FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");

			if (!isUse)
            {
                return;
            }
        }
    }
}
