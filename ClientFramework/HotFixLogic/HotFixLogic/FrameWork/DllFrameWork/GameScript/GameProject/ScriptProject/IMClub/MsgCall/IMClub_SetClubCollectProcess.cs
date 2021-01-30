using System;
using System.Collections.Generic;
using System.Text;

namespace WordProcess
{

    /// <summary>
    /// 用户进入游戏返回 结果 包括游戏服务器信息
    /// </summary>
    public class IMClub_SetClubCollectProcess : ProcessMessageBase
    {
        public IMClub_SetClubCollectProcess()
        {
            ID = (int)IMClub.NetMessageType.SC_SetCollect_MsgType;
        }

        public static ProcessMessageBase _Instance;

        public static ProcessMessageBase GetProcessType()
        {
            if (_Instance == null)
            {
                _Instance = new IMClub_SetClubCollectProcess();
            }
            return _Instance;
        }

        //处理方法
        public override void Process(System.Net.Sockets.Socket NetSocket, string ip, int port, byte[] DateBuf)
        {
            UINameSpace.UIWaitting.RemoveShowWaitting("SC_SetCollect_MsgType");
            IMClub.SC_SetCollect messageBack = new IMClub.SC_SetCollect();
            try
            {
                messageBack.Deserializer(DateBuf, 0);
            }
			catch (Exception e)
			{
				DebugLoger.LogError("消息异常--------------" + e.ToString());
				return;
			}

			if (messageBack.result == 1)
            {
                //这里创建一个亲友圈成功了 存到数据里面去
                IMClub.GroupWarp gw = IMClub.GoableClubDataInfo.GetGroup(messageBack.clubId);
                if (gw != null)
                {
                    gw.groupInfo.clubSetting.collectMode = messageBack.collectMode;
                    gw.groupInfo.clubSetting.collectScale = messageBack.collectScale;
                    gw.groupInfo.clubSetting.collectScore = messageBack.collectScore;
                    gw.groupInfo.clubSetting.collectStart = messageBack.collectStart;
                    gw.groupInfo.clubSetting.collectTaxesType = messageBack.collectTaxesType;
                    gw.groupInfo.clubSetting.scoreLimit = messageBack.scoreLimit;

                    UINameSpace.UITipMessage.PlayMessage("设置牌局收分成功!");
                }
                else
                {
                    UINameSpace.UITipMessage.PlayMessage("系统错误!");
                }
            }
            else
            {
                UINameSpace.UITipMessage.PlayMessage("操作失败!");
            }
        }
    }
}