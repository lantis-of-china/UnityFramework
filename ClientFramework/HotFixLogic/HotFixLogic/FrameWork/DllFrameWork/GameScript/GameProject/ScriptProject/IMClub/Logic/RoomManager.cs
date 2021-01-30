using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMClub
{
    /// <summary>
    /// 房间管理器
    /// </summary>
    public class RoomManager
    {
		public static string generalClub = "generalClub";
		/// <summary>
		/// 房间列表
		/// </summary>
		public static Dictionary<string, Dictionary<int, P_RoomInfo>> roomMap = new Dictionary<string, Dictionary<int,P_RoomInfo>>();
        
        /// <summary>
        /// 添加房间列表
        /// </summary>
        /// <param name="roomList"></param>
        public static void AdddRooms(List<P_RoomInfo> roomList)
        {
            for(int i = 0;i < roomList.Count;++i)
            {
                AddRoom(roomList[i]);
            }
        }

        /// <summary>
        /// 添加一个房间
        /// </summary>
        /// <param name="roomInfo"></param>
        public static void AddRoom(P_RoomInfo roomInfo)
        {
            if (roomMap.ContainsKey(roomInfo.clubId))
            {
                Dictionary<int,IMClub.P_RoomInfo> roomList = roomMap[roomInfo.clubId];
                roomList.Add(roomInfo.roomId, roomInfo);
            }
            else
            {
                Dictionary<int, IMClub.P_RoomInfo> roomList = new Dictionary<int, P_RoomInfo>();
                roomList.Add(roomInfo.roomId, roomInfo);
                roomMap.Add(roomInfo.clubId, roomList);
            }
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static P_RoomInfo GetRoom(string clubId,int roomId)
        {
            if (roomMap.ContainsKey(clubId))
            {
                Dictionary<int, IMClub.P_RoomInfo> roomList = roomMap[clubId];
                if (roomList.ContainsKey(roomId))
                {
                    return roomList[roomId];
                }


            }
            return null;
        }

		/// <summary>
		/// 获取房间
		/// </summary>
		/// <param name="roomId"></param>
		/// <returns></returns>
		public static P_RoomInfo GetRoom(int roomId)
		{
			foreach (var kv in roomMap)
			{
				Dictionary<int, IMClub.P_RoomInfo> itemRoomInfo = kv.Value;
				if (itemRoomInfo.ContainsKey(roomId))
				{
					return itemRoomInfo[roomId];
				}
			}

			return null;
		}

		/// <summary>
		/// 移除房间
		/// </summary>
		/// <param name="roomId"></param>
		public static void RemoveRoom(string clubId,int roomId)
        {
            if (roomMap.ContainsKey(clubId))
            {
                Dictionary<int, IMClub.P_RoomInfo> roomList = roomMap[clubId];
                if (roomList.ContainsKey(roomId))
                {
                    roomList.Remove(roomId);
                }

                if(roomList.Count == 0)
                {
                    roomMap.Remove(clubId);
                }
            }
        }

        /// <summary>
        /// 清理全部
        /// </summary>
        public static void ClearRooms()
        {
            roomMap.Clear();
        }

        /// <summary>
        /// 获取亲友圈列表
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        public static List<P_RoomInfo> GetClubRoomInfoList(string clubId)
        {
            DebugLoger.Log("club clubId " + clubId);
            foreach (var kv in roomMap)
            {
                DebugLoger.Log("club id " + kv.Key);
            }
            if (roomMap.ContainsKey(clubId))
            {
                Dictionary<int, IMClub.P_RoomInfo> roomList = roomMap[clubId];

                List<P_RoomInfo> clubRoomList = new List<P_RoomInfo>(roomList.Values);

                return clubRoomList;
            }

            return null;
        }
    }
}
