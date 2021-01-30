using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SingleMoba
{    
    public class MessageSend
    {
        /// <summary>
        /// 发送加入房间信息
        /// </summary>
        /// <param name="entryType"></param>
        /// <param name="roomId"></param>
        public static void EntryRoom(byte entryType, int roomId)
        {
            CS_EntryRoom sendMsg = new CS_EntryRoom();
            sendMsg.entryType = entryType;
            sendMsg.roomId = roomId;

            sendMsg.UserValiadate = GoableData.userValiadateInfor;
            UserNetWork.Instance.SendMessageUdp<CS_EntryRoom>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, sendMsg, (int)NetMessageType.CS_EntryRoom_MsgType);
        }

        /// <summary>
        /// 发送加入房间信息
        /// </summary>
        /// <param name="entryType"></param>
        /// <param name="roomId"></param>
        public static void LeaveRoom()
        {
            CS_RoleLeaveRoom sendMsg = new CS_RoleLeaveRoom();
            sendMsg.UserValiadate = GoableData.userValiadateInfor;

            UserNetWork.Instance.SendMessageUdp<CS_RoleLeaveRoom>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, sendMsg, (int)NetMessageType.CS_RoleLeaveRoom_MsgType);
        }

        /// <summary>
        /// 进入场景完成 告诉服务器
        /// </summary>
        public static void AssetsLoadFinish()
        {
            CS_LoadRoomAssetsFinish msgSend = new CS_LoadRoomAssetsFinish();
            msgSend.UserValiadate = GoableData.userValiadateInfor;

            UserNetWork.Instance.SendMessageUdp<CS_LoadRoomAssetsFinish>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, msgSend, (int)NetMessageType.CS_AssetsLoadFinish_MsgType);
        }

        /// <summary>
        /// 游戏移动
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        public static void GamerMove(float x,float y,float targetX,float targetY)
        {
            CS_GamerMove msgSend = new CS_GamerMove();
            msgSend.UserValiadate = GoableData.userValiadateInfor;
            msgSend.currentX = x;
            msgSend.currentY = y;
            msgSend.targetX = targetX;
            msgSend.targetY = targetY;
            msgSend.ticks = GoableData.GetServerNowTime();

            UserNetWork.Instance.SendMessageUdp<CS_GamerMove>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, msgSend, (int)NetMessageType.CS_GamerMove_MsgType);
        }

        /// <summary>
        /// 游戏玩家停止移动
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void GamerMoveStop(float x, float y)
        {
            CS_GamerMoveStop msgSend = new CS_GamerMoveStop();
            msgSend.UserValiadate = GoableData.userValiadateInfor;
            msgSend.currentX = x;
            msgSend.currentY = y;

            UserNetWork.Instance.SendMessageUdp<CS_GamerMoveStop>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, msgSend, (int)NetMessageType.CS_GamerMoveStop_MsgType);
        }

        /// <summary>
        /// 吃到道具
        /// </summary>
        /// <param name="propIds"></param>
        public static void EatProp(List<int> propIds)
        {
            CS_EatProp msgSend = new CS_EatProp();
            msgSend.UserValiadate = GoableData.userValiadateInfor;
            msgSend.propId = propIds;
            UserNetWork.Instance.SendMessageUdp<CS_EatProp>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, msgSend, (int)NetMessageType.CS_EatProp_MsgType);
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        public static void UseSkill(int skillId,float currentX,float currentY,float targetX,float targetY)
        {
            CS_UseSkill msgSend = new CS_UseSkill();
            msgSend.UserValiadate = GoableData.userValiadateInfor;
            msgSend.skillId = skillId;
            msgSend.currentX = currentX;
            msgSend.currentY = currentY;
            msgSend.targetX = targetX;
            msgSend.targetY = targetY;
            UserNetWork.Instance.SendMessageUdp<CS_UseSkill>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, msgSend, (int)NetMessageType.CS_UseSkill_MsgType);
        }

        /// <summary>
        /// 技能命中
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="hitPlayerId"></param>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        public static void HitSkill(int skillId, int hitPlayerId,float currentX, float currentY)
        {
            CS_HitSkill msgSend = new CS_HitSkill();
            msgSend.UserValiadate = GoableData.userValiadateInfor;
            msgSend.skillId = skillId;
            msgSend.hitPlayerId = hitPlayerId;
            msgSend.skillX = currentX;
            msgSend.skillY = currentY;
            UserNetWork.Instance.SendMessageUdp<CS_HitSkill>(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort, msgSend, (int)NetMessageType.CS_HitSkill_MsgType);
        }
    }
}