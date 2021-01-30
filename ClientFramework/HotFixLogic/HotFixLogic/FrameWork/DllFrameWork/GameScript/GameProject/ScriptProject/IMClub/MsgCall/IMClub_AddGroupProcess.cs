using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_AddGroupProcess : ProcessMessageBase
    {
        public IMClub_AddGroupProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_AddGroup_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_AddGroupProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("IMClub.NetMessageType.CS_AddGroup_MsgType");
            IMClub.SC_AddGroup messageBack = new IMClub.SC_AddGroup();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

            if (messageBack.result == (byte)1)
            {
				if (!IMClub.GoableClubDataInfo.isOpenClub)
				{
					return;
				}
                //这里创建一个亲友圈成功了 存到数据里面去
                IMClub.GoableClubDataInfo.AddGroup(messageBack.groupInfo);
				if (messageBack.thisRroomList != null)
				{
					IMClub.RoomManager.AdddRooms(messageBack.thisRroomList);
				}
				IMClub.ClubListPanel.UpdateClubList();

                //IMClub.P_Menber pm = new IMClub.P_Menber();
                //pm.headUrl = GoableData.userValiadateInforWarp.headUrl;
                //pm.menberName = GoableData.userValiadateInforWarp.PikeName;
                //pm.sex = (byte)GoableData.userValiadateInforWarp.Sex;
                //pm.menberId = int.Parse(GoableData.userValiadateInfor.DatingNumber);
                //byte[] menberBuf = pm.Serializer();
                //Dictionary<string, object> systemMsg = new Dictionary<string, object>();
                //systemMsg.Add("msgType", "system");
                //systemMsg.Add("cmd", "joinUser");
                //systemMsg.Add("content", Encoding.UTF8.GetString(menberBuf, 0, menberBuf.Length));

                //IMClub.ClubItem ci = IMClub.ClubListPanel.GetClubItem(messageBack.groupInfo.groupId);
                //if (ci != null)
                //{
                //    IMClub.IMCludWarp.SendTextMessageSystem(ci.conversationHandle, LitJson.JsonMapper.ToJson(systemMsg), null);
                //}
            }
        }
    }
}