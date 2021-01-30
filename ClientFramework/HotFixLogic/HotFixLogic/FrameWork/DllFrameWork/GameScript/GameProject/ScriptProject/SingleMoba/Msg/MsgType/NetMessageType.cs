using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleMoba
{
    /// <summary>
    /// 模板实现网络消息
    /// </summary>
    public enum NetMessageType
    {
        /// <summary>
        /// 加入房间
        /// </summary>
        CS_EntryRoom_MsgType = 1102001,
        /// <summary>
        /// 进入房间
        /// </summary>
        SC_EntryRoom_MsgType = 1102002,
        /// <summary>
        /// 玩家加入房间
        /// </summary>
        SC_JoinRoom_MsgType = 1102003,
        /// <summary>
        /// 进入房间资源加载完毕
        /// </summary>
        CS_AssetsLoadFinish_MsgType = 1102004,
        /// <summary>
        /// 返回房间信息
        /// </summary>
        SC_RoomInfo_MsgType = 1102005,
        /// <summary>
        /// 离开请求
        /// </summary>
        CS_RoleLeaveRoom_MsgType = 1102006,
        /// <summary>
        /// 离开
        /// </summary>
        SC_RoleLeaveRoom_MsgType = 1102007,
        /// <summary>
        /// 匹配房间
        /// </summary>
        CS_Matching_MsgType = 1102011,
        /// <summary>
        /// 匹配房间
        /// </summary>
        SC_Matching_MsgType = 1102012,
        /// <summary>
        /// 玩家移动
        /// </summary>
        CS_GamerMove_MsgType = 1102013,
        /// <summary>
        /// 玩家移动
        /// </summary>
        SC_GamerMove_MsgType = 1102014,
        /// <summary>
        /// 玩家移动停止
        /// </summary>
        CS_GamerMoveStop_MsgType = 1102021,
        /// <summary>
        /// 玩家移动停止
        /// </summary>
        SC_GamerMoveStop_MsgType = 1102022,
        /// <summary>
        /// 吃道具
        /// </summary>
        CS_EatProp_MsgType = 1102015,
        /// <summary>
        /// 吃道具
        /// </summary>
        SC_EatProp_MsgType = 1102016,
        /// <summary>
        /// 施放技能
        /// </summary>
        CS_UseSkill_MsgType = 1102017,
        /// <summary>
        /// 施放技能
        /// </summary>
        SC_UseSkill_MsgType = 1102018,
        /// <summary>
        /// 玩家状态变化
        /// </summary>
        SC_GamerStateChange_MsgType = 1102019,
        /// <summary>
        /// 产生一批道具
        /// </summary>
        SC_SpawnProp_MsgType = 1102020,
        /// <summary>
        /// 技能命中
        /// </summary>
        CS_HitSkill_MsgType = 1102023,
        /// <summary>
        /// 技能命中
        /// </summary>
        SC_HitSkill_MsgType = 1102024,
        /// <summary>
        /// 技能结束
        /// </summary>
        SC_SkillEnd_MsgType = 1102025,
        /// <summary>
        /// 技能移除
        /// </summary>
        SC_RemoveSkill_MsgType = 1102026,
        /// <summary>
        /// 添加Buff
        /// </summary>
        SC_AddSkillBuff_MsgType = 1102027,
        /// <summary>
        /// 移除Buff
        /// </summary>
        SC_RemoveSkillBuff_MsgType = 1102028,
        /// <summary>
        /// Buff触发
        /// </summary>
        SC_SkillBuffTrigger_MsgType = 1102029,
        /// <summary>
        /// 解散房间
        /// </summary>
        SC_UnReleseRoom_MsgType = 1102030,
    }
}
