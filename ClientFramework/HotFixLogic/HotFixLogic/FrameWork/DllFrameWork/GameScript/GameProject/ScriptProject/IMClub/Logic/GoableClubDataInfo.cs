using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClub
{
    /// <summary>
    /// 组群结构
    /// </summary>
    public class GroupWarp
    {
        /// <summary>
        /// 亲友圈信息
        /// </summary>
        public P_GroupInfo groupInfo;
        /// <summary>
        /// 绑定的亲友圈对象
        /// </summary>
        public ClubItem bindClubItem;
        /// <summary>
        /// 时候调用过获取成员
        /// </summary>
        public bool isGetMenbers;
        /// <summary>
        /// 成员列表
        /// </summary>
        public Dictionary<int, P_Menber> menberList = new Dictionary<int, P_Menber>();
		/// <summary>
		/// 请求列表
		/// </summary>
		public Dictionary<int, P_RequestInfo> requestInfoList = new Dictionary<int, P_RequestInfo>();
		/// <summary>
		///页码0开始
		/// <\summary>
		public byte page;
		/// <summary>
		///战绩列表
		/// <\summary>
		public List<P_ClubGradeInfo> clubGradeList;
	}


    /// <summary>
    /// 亲友圈数据
    /// </summary>
    public class GoableClubDataInfo
    {
        /// <summary>
        /// 开启过亲友圈
        /// </summary>
        public static bool isOpenClub;
        /// <summary>
        /// 亲友圈列表
        /// </summary>
        public static List<GroupWarp> groupInfoList = new List<GroupWarp>();

        /// <summary>
        /// 清理数据
        /// </summary>
        public static void ClearData()
        {
			ClubItem.clubItemState = null;
			isOpenClub = false;
            ClearGroups();
        }

        /// <summary>
        /// 清理组
        /// </summary>
        public static void ClearGroups()
        {
            groupInfoList.Clear();
        }

        /// <summary>
        /// 添加组列表
        /// </summary>
        /// <param name="groupInfoList"></param>
        public static void AddGroups(List<P_GroupInfo> groups)
        {
            for (int i = 0; i < groups.Count; ++i)
            {
                AddGroup(groups[i]);
            }
        }

        /// <summary>
        /// 添加组信息
        /// </summary>
        /// <param name="groupInfo"></param>
        public static void AddGroup(P_GroupInfo groupInfo)
        {
            GroupWarp gw = new GroupWarp();
            gw.groupInfo = groupInfo;
            gw.bindClubItem = null;
            gw.isGetMenbers = false;
            groupInfoList.Add(gw);
        }

		#region 添加请求到亲友圈
		/// <summary>
		/// 添加请求列表
		/// </summary>
		/// <param name="requestList"></param>
		public static void AddRequests(List<P_RequestInfo> requestList)
		{
			for (int i = 0; i < requestList.Count; ++i)
			{
				AddRequestToGroup(requestList[i]);
			}
		}

		/// <summary>
		/// 添加请求到亲友圈
		/// </summary>
		public static void AddRequestToGroup(P_RequestInfo request)
		{
			GroupWarp groupWarp = GetGroup(request.clubId);
			if (groupWarp != null)
			{
				if (!groupWarp.requestInfoList.ContainsKey(request.menberId))
				{
					groupWarp.requestInfoList.Add(request.menberId, request);
				}
			}
		}

		/// <summary>
		/// 移除请求到亲友圈
		/// </summary>
		public static void RemoveRequestToGroup(string clubId,int menberId)
		{
			GroupWarp groupWarp = GetGroup(clubId);
			if (groupWarp != null)
			{
				if (groupWarp.requestInfoList.ContainsKey(menberId))
				{
					groupWarp.requestInfoList.Remove(menberId);
				}
			}
		}
		#endregion 添加请求到亲友圈

		/// <summary>
		/// 获取全部请求
		/// </summary>
		/// <returns></returns>
		public static List<P_RequestInfo> GetAllGroupRequest()
		{
			int selfId = int.Parse(GoableData.userValiadateInfor.DatingNumber);
			List < P_RequestInfo > requestList = new List<P_RequestInfo>();
			for (int i = 0; i < groupInfoList.Count; ++i)
			{
				GroupWarp gw = groupInfoList[i];
				if (gw.groupInfo.groupMasterId == selfId)
				{
					foreach (var kv in gw.requestInfoList)
					{
						requestList.Add(kv.Value);
					}
				}
			}

			return requestList;
		}

		/// <summary>
		/// 获取亲友圈
		/// </summary>
		/// <param name="clubId"></param>
		public static GroupWarp GetGroup(string clubId)
        {
            for (int i = 0; i < groupInfoList.Count; ++i)
            {
                if (groupInfoList[i].groupInfo.clubId == clubId)
                {
                    return groupInfoList[i];
                }
            }
            return null;
        }

		/// <summary>
		/// 获取亲友圈
		/// </summary>
		/// <param name="clubId"></param>
		public static GroupWarp GetGroupWithGroupId(string groupId)
		{
			for (int i = 0; i < groupInfoList.Count; ++i)
			{
				if (groupInfoList[i].groupInfo.groupId == groupId)
				{
					return groupInfoList[i];
				}
			}
			return null;
		}

		/// <summary>
		/// 移除组
		/// </summary>
		/// <param name="groupId"></param>
		public static void RemoveGroup(string clubId)
        {
            for(int i = 0;i < groupInfoList.Count;++i)
            {
                if (groupInfoList[i].groupInfo.clubId == clubId)
                {
                    groupInfoList.RemoveAt(i);
                    break;
                }
            }
        }


        /// <summary>
        /// 添加成员组到
        /// </summary>
        public static GroupWarp AddMenbersToGroup(List<P_Menber> menbers,string clubId)
        {
            GroupWarp gw = null;
            for (int i = 0; i < groupInfoList.Count; ++i)
            {
                DebugLoger.Log("groupInfoList[i].groupInfo.clubId " + groupInfoList[i].groupInfo.clubId + " clubId " + clubId);
                if (groupInfoList[i].groupInfo.clubId == clubId)
                {
                    gw = groupInfoList[i];
                    break;
                }
            }

            if(gw != null)
            {
                for(int i = 0;i < menbers.Count;++i)
                {
                    if (!gw.menberList.ContainsKey(menbers[i].menberId))
                    {
						gw.menberList.Add(menbers[i].menberId, menbers[i]);
                    }
                }
            }
            else
            {
                DebugLoger.LogError("AddMenbersToGroup不能添加成员");
				return null;
            }

			return gw;
        }

        /// <summary>
        /// 添加成员到组
        /// </summary>
        /// <param name="menber"></param>
        /// <param name="clubId"></param>
        public static void AddMenberToGroup(P_Menber menber,string groutId)
        {
            GroupWarp gw = null;
            for (int i = 0; i < groupInfoList.Count; ++i)
            {
                if (groupInfoList[i].groupInfo.groupId == groutId)
                {
                    gw = groupInfoList[i];
                    break;
                }
            }

            if (gw != null)
            {
                if (!gw.menberList.ContainsKey(menber.menberId))
                {
					gw.menberList.Add(menber.menberId, menber);
                }
                else
                {
                    DebugLoger.LogError("添加成员 成员已存在");
                }
            }
            else
            {
                DebugLoger.LogError("AddMenbersToGroup不能添加成员");
            }
        }

        /// <summary>
        /// 从组移除成员数据
        /// </summary>
        /// <param name="menberId"></param>
        /// <param name="clubId"></param>
        public static P_Menber RemoveMenberFromGroup(int menberId,string clubId)
        {
            for (int i = 0; i < groupInfoList.Count; ++i)
            {
                if (groupInfoList[i].groupInfo.clubId == clubId)
                {
                    GroupWarp gw = groupInfoList[i];

                    foreach (var kv in gw.menberList)
                    {
                        if(kv.Value.menberId == menberId)
                        {
							gw.menberList.Remove(menberId);

                            return kv.Value;
                        }
                    }
                    break;
                }
            }

            return null;
        }


        /// <summary>
        /// 获取成员从亲友圈
        /// </summary>
        /// <param name="menberId"></param>
        /// <param name="clubId"></param>
        public static P_Menber GetMenberFromGroup(int menberId, string clubId)
        {
            for (int i = 0; i < groupInfoList.Count; ++i)
            {
                if (groupInfoList[i].groupInfo.clubId == clubId)
                {
                    GroupWarp gw = groupInfoList[i];

                    foreach (var kv in gw.menberList)
                    {
                        if (kv.Value.menberId == menberId)
                        {
                            return kv.Value;
                        }
                    }
                }
            }

            return null;
        }

		/// <summary>
		/// 获取成员从亲友圈
		/// </summary>
		/// <param name="menberId"></param>
		/// <param name="clubId"></param>
		public static P_Menber GetMenberFromGroupWithGroupId(int menberId, string groupId)
		{
			for (int i = 0; i < groupInfoList.Count; ++i)
			{
				if (groupInfoList[i].groupInfo.groupId == groupId)
				{
					GroupWarp gw = groupInfoList[i];

					foreach (var kv in gw.menberList)
					{
						if (kv.Value.menberId == menberId)
						{
							return kv.Value;
						}
					}
				}
			}

			return null;
		}


		/// <summary>
		/// 获取组群数量
		/// </summary>
		/// <returns></returns>
		public static int GetGroupCount()
        {
            return groupInfoList.Count;
        }
    }

}
