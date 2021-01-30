using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Server;
using System;


public delegate void PingCallBack(long pingTicks);
public delegate void ClearCallBack();

public class GoableData
{
	public static Action RefenceGameEven;
    /// <summary>
    /// 清理事件
    /// </summary>
    public static List<ClearCallBack> ClearCallEvent = new List<ClearCallBack>();
    /// <summary>
    /// 定义游戏数据
    /// </summary>
    public class GoableDataItem
    {
        /// <summary>
        /// 清理
        /// </summary>
        public virtual void ClearData()
        {

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public virtual void LodingDate()
        {

        }
    }

    /// <summary>
    /// 注册进入的游戏数据Item
    /// </summary>
    public static List<CherishKeyValue<string, GoableDataItem>> LogicDataList = new List<CherishKeyValue<string,GoableDataItem>>();

    /// <summary>
    /// 是否注册过游戏逻辑数据空间
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool HasRegistLogicData(string name)
    {
        for(int i = 0;i < LogicDataList.Count;++i)
        {
            if(LogicDataList[i].key == name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 移除注册过游戏逻辑数据空间
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool RemoveLogicData(string name)
    {
        for (int i = 0; i < LogicDataList.Count; ++i)
        {
            if (LogicDataList[i].key == name)
            {
                LogicDataList.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 注册数据
    /// </summary>
    /// <param name="dataItem"></param>
    public static void RegistLogicDataList(string name,GoableDataItem dataItem)
    {
        if (!HasRegistLogicData(name))
        {
            LogicDataList.Add(new CherishKeyValue<string, GoableDataItem>() { key = name, value = dataItem });
            dataItem.LodingDate();
        }
        else
        {
            DebugLoger.LogError("已经注册过该游戏数据" + dataItem.GetType());
        }
    }

    /// <summary>
    /// 注销数据
    /// </summary>
    /// <param name="dataItem"></param>
    public static void UnRegistLogicDataList(string name)
    {
        if (HasRegistLogicData(name))
        {
            RemoveLogicData(name);
        }
        else
        {
            DebugLoger.LogError("尚未注册该游戏数据" + name);
        }
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataName"></param>
    /// <returns></returns>
    public static T GetLogicData<T>(string name) where T : GoableDataItem
    {
        for (int i = 0; i < LogicDataList.Count; ++i)
        {
            CherishKeyValue<string,GoableDataItem> dataItem = LogicDataList[i];

            if (dataItem.key == name)
            {
                return (T)dataItem.value;
            }
        }
        return default(T);
    }

    /// <summary>
    /// 清理游戏逻辑的数据
    /// </summary>
    public static void ClearLogicsData()
    {
        for (int i = 0; i < LogicDataList.Count; ++i)
        {
            CherishKeyValue<string, GoableDataItem> dataItem = LogicDataList[i];
            dataItem.value.ClearData();
        }

        LogicDataList.Clear();
    }

    /// <summary>
    /// 重连IP
    /// </summary>
    public static string reconnectIp;
    /// <summary>
    /// 重连端口
    /// </summary>
    public static int reconnectPort;
    /// <summary>
    /// 重连关闭排除UI
    /// </summary>
    public static string reconnectExternUIName;
    /// <summary>
    /// 需要重连
    /// </summary>
    public static bool needReconnect = false;

    /// <summary>
    /// 设置允许重连状态
    /// </summary>
    public static void SetReconnectEnable()
    {
        needReconnect = true;
    }

    /// <summary>
    /// 设置重连关闭状态
    /// </summary>
    public static void SetReconnectDisable()
    {
        DebugLoger.Log("设置 ReconnectDisable");
        needReconnect = false;
    }

    /// <summary>
    /// 游戏需要断开重连是否
    /// </summary>
    /// <returns></returns>
    public static bool NeedReconnect()
    {
        return needReconnect;
    }

    /// <summary>
    /// 清理登陆信息
    /// </summary>
    public static void ClearnLoginData()
    {
        userValiadateInfor = null;
        //userValiadateInforWarp = null;
        ServerIpaddress.ClearnData();
    }

	/// <summary>
	/// 清理数据
	/// </summary>
	public static void ClearnData(bool hasClub = true)
    {
        userValiadateInfor = null;

        //userValiadateInforWarp = null;

        gameCoreData = null;

        ClearLogicsData();

        ServerIpaddress.ClearnData();

		DepachMsgData.ClearData();
	
		UISystemMsg.ClearData();

		if (hasClub)
		{
			ClearEventData();
		}
	}


	public static void ClearEventData()
	{
		if (ClearCallEvent != null)
		{
			//ClearCallEvent();
			for (int i = 0; i < ClearCallEvent.Count; ++i)
			{
				ClearCallEvent[i]();
			}
		}
	}

    /// <summary>
    /// 验证数据
    /// </summary>
    public static Server.UserValiadateInfor userValiadateInfor;

    public static Server.UserValiadateInforWarp userValiadateInforWarp;

    /// <summary>
    /// 游戏核心数据
    /// </summary>
    public static BaseDataAttribute.GameCoreData gameCoreData;
    /// <summary>
    /// 服务器拉取到的服务器列表
    /// <\summary>
    public static List<P_ConditionItem> conditionList;
    /// <summary>
    /// 最小的ping值
    /// </summary>
    public static float minPing;
    /// <summary>
    /// 当前的ping值
    /// </summary>
    public static float currentPing;
    /// <summary>
    /// 服务器时间
    /// </summary>
    public static long utcTimeOffset;
    /// <summary>
    /// 发送心跳间隔时间
    /// </summary>
    public static float sendHeartInvate = 8.0f;
    /// <summary>
    /// 记录心跳次数
    /// </summary>
    public static long heartTimes = 0;
    /// <summary>
    /// 发送心跳时间倒计时
    /// </summary>
    public static float sendHeartRem;
    /// <summary>
    /// 心跳时间已经发送
    /// </summary>
    public static bool isSend = true;
    /// <summary>
    /// 心跳是否开启
    /// </summary>
    public static bool heartOpen = false;
    /// <summary>
    ///发送后多久要收到消息回来
    /// </summary>
    public static float outTime = 30.0f;
    /// <summary>
    /// 当前接收间隔
    /// </summary>
    public static float curOutTime = 0.0f;
    /// <summary>
    /// 延时通知事件
    /// </summary>
    public static List<PingCallBack> PingCallFun = new List<PingCallBack>();
    
    /// <summary>
    /// 关闭心跳
    /// </summary>
    public static void CloseHeart()
    {
        heartOpen = false;
        isSend = true;
        sendHeartRem = sendHeartInvate;
    }

    /// <summary>
    /// 开启心跳
    /// </summary>
    public static void OpenHeart()
    {
        heartOpen = true;
        isSend = false;
        sendHeartRem = 0;
        utcTimeOffset = -1;
        minPing = -0.0001f;
        heartTimes = 0;
        curOutTime = outTime;
    }

    /// <summary>
    /// 更新心跳时间
    /// </summary>
    /// <param name="unixT"></param>
    public static void UpHeart(SC_Heart heart)
    {
        var pingValue = (DateTime.UtcNow.Ticks - heart.ticks) / 2;
        var offsetValue = (DateTime.UtcNow.Ticks - heart.unitxTime) + pingValue;
        curOutTime = sendHeartInvate + outTime;
        currentPing = pingValue / 10000000.0f;

        if (minPing < 0 || minPing > pingValue)
        {
            DebugLoger.Log($"最后更新 lastPing:{minPing} ping:{pingValue / 10000000.0f} offsetValue:{offsetValue}");

            minPing = pingValue;
            utcTimeOffset = offsetValue;

        }

        if (PingCallFun != null)
        {
			for (int i = 0; i < PingCallFun.Count; ++i)
			{
				PingCallFun[i](pingValue);
			}
        }
    }

    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns></returns>
    public static long GetServerNowTime()
    {
        return DateTime.UtcNow.Ticks - utcTimeOffset;
    }

    /// <summary>
    /// 全局数据更新
    /// </summary>
    /// <param name="detaTime"></param>
    public static void Update()
    {
        UINameSpace.UILogin.UpLine();

        if(heartOpen)
        {
            if(sendHeartRem > 0)
            {
                sendHeartRem -= Time.deltaTime;
            }
            else
            {
                if (heartTimes < 5)
                {
                    sendHeartRem = 1.0f ;
                    heartTimes++;
                }
                else if (heartTimes < 10)
                {
                    sendHeartRem = 2.0f;
                    heartTimes++;
                }
                else if (heartTimes < 20)
                {
                    sendHeartRem = 3.0f;
                    heartTimes++;
                }
                else
                {
                    sendHeartRem = sendHeartInvate;
                }

                ///这里发送一个心跳时间
                Rall.MessageSend.SpawnHeart(ServerIpaddress.gameServerIp, ServerIpaddress.gameServerPort);                    
            }

            if(curOutTime >= 0)
            {
                curOutTime -= Time.deltaTime;

                if(curOutTime < 0)
                {
                    DebugLoger.Log("hert end");
                    //这里就掉线
                    UINameSpace.UILogin.OutLine();
                }
            }
        }
    }


    /// <summary>
    /// 服务器地址信息
    /// </summary>
    public class ServerIpaddress
    {
        /// <summary>
        /// IPV6
        /// </summary>
        public static string gameServerIpV6;
        /// <summary>
        /// 选择的服务器IP
        /// </summary>
        public static string gameServerIp = "";
        /// <summary>
        /// 选择的服务器地址
        /// </summary>
        public static int gameServerPort = 0;
        /// <summary>
        /// 准备进入的房间ID
        /// </summary>
        public static int readyEntryRoomId = -1;
		/// <summary>
		/// 亲友圈ID
		/// </summary>
		public static string clubId = "";
        /// <summary>
        /// 是否登陆登陆服务器记录
        /// </summary>
        public static bool isLoginLogServerSend = false;
        /// <summary>
        /// 是否登陆游戏服务器记录
        /// </summary>
        public static bool isLoginGameServerSend = false;
        /// <summary>
        /// 需要重连
        /// </summary>
        public static bool needReconnect = false;
        /// <summary>
        /// 是否前往获取微信登录权限
        /// </summary>
        public static bool getWeiChatAuth = false;
        /// <summary>
        /// 是否第三方连接离开游戏
        /// </summary>
        public static bool getSdkOut = false;
        /// <summary>
        /// 是否连接到游戏逻辑
        /// </summary>
        public static bool isLoginGameLogic = false;
		/// <summary>
		///纬度
		/// <\summary>
		public static float latitude;
		/// <summary>
		///经度
		/// <\summary>
		public static float longitude;

		/// <summary>
		/// 服务器地址列表
		/// </summary>
		public static List<Server.GolabServerInfor> mServerData;

        /// <summary>
        /// 设置前往获取微信登录权限 状态
        /// </summary>
        public static void SetGetWeiChatAuthState()
        {
            getWeiChatAuth = true;
        }


        /// <summary>
        /// 取消前往获取微信登录权限 状态
        /// </summary>
        public static void ClearGetWeihatAuthState()
        {
            getWeiChatAuth = false;
        }



        /// <summary>
        /// 设置重连
        /// </summary>
        /// <returns></returns>
        public static void SetReconnect()
        {
            needReconnect = true;
        }

        /// <summary>
        /// 设置第三方SDK跳出游戏
        /// </summary>
        public static void SetSdkOutGame()
        {
            getSdkOut = true;
        }

        /// <summary>
        /// 设置第三方SDK跳出游戏
        /// </summary>
        public static void ClearSdkOutGame()
        {
            getSdkOut = false;
        }

        /// <summary>
        /// 清理战斗数据
        /// </summary>
        public static void ClearnData()
        {
			readyEntryRoomId = -1;

            gameServerIp = "";

            gameServerPort = 0;

            isLoginLogServerSend = false;

            isLoginGameServerSend = false;

            needReconnect = false;

            isLoginGameLogic = false;

            ClearGetWeihatAuthState();

            ClearSdkOutGame();
        }
    }

    /// <summary>
    /// 游戏设置
    /// </summary>
    public class UIGameSettingData
    {
        /// <summary>
        /// 音效
        /// </summary>
        public static float soundValue = 1.0f;
        /// <summary>
        /// 背景音
        /// </summary>
        public static float backgroundSoundValue = 0.4f;
        /// <summary>
        /// 地方方言
        /// </summary>
        public static bool locakGenerilLanauge = true;

        /// <summary>
        /// 清理数据
        /// </summary>
        public static void ClearnUserInput()
        {
        }
    }


    /// <summary>
    /// 系统消息
    /// </summary>
    public class UISystemMsg
    {
        /// <summary>
        /// 消息列表
        /// </summary>
        public static List<P_MsgInfo> systemMsgList;

        /// <summary>
        /// 清理数据
        /// </summary>
        public static void ClearData()
        {
            systemMsgList = null;
        }
    }


    /// <summary>
    /// 推送消息 公告滚动
    /// </summary>
    public class DepachMsgData
    {
		public static int msgIndex = -1;

        public static string defaultStr = "";

        public static List<string> depachMsgList = new List<string>();

		public static void AddDepachMsg(Server.SC_RoleMsg msg)
		{
			if (msg.msgList == null)
			{
				return;
			}

			for (int i = 0; i < msg.msgList.Count; ++i)
			{
				P_MsgInfo msgInfo = msg.msgList[i];

				depachMsgList.Add(msgInfo.msg);
			}
		}

		public static string GetNextMsg()
		{
			if (depachMsgList.Count == 0)
			{
				return defaultStr;
			}

			msgIndex++;

			if (msgIndex >= depachMsgList.Count)
			{
				msgIndex = 0;
			}

			return depachMsgList[msgIndex];
		}

        /// <summary>
        /// 清理数据
        /// </summary>
        public static void ClearData()
        {
			msgIndex = -1;

			depachMsgList.Clear();
        }
    }





    /// <summary>
    /// 微信用户数据
    /// </summary>
    public class WeiChatUserData
    {
        /// <summary>
        /// 微信对应公众号唯一ID
        /// </summary>
        public static string openId;
		/// <summary>
		/// 企业绑定ID
		/// </summary>
		public static string unionid;
		/// <summary>
		/// 微信昵称
		/// </summary>
		public static string nickname;
        /// <summary>
        /// 性别
        /// </summary>
        public static byte sex;
        /// <summary>
        /// 头像地址
        /// </summary>
        public static string headimgurl;
        /// <summary>
        /// 是否设置了微信数据
        /// </summary>
        public static bool hasUserData;

        public static void ClearData()
        {
            hasUserData = false;
        }
    }
}
