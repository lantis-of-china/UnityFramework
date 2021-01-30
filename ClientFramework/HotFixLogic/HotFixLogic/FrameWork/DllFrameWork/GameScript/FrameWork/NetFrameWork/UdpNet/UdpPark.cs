using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class FrameUdpCenter
{
	public static void StartRun()
	{
		ComplateRecorder.Start();
		UdpSubmit.Start();
		UdpLineParkTool.Start();
	}

	public static void Close()
	{
		ComplateRecorder.Close();
		UdpSubmit.Close();
		UdpLineParkTool.Close();
	}
}

/// <summary>
/// UDP应答管理
/// </summary>
public class UdpSubmit
{
	/// <summary>
	/// 需要应答的包
	/// </summary>
	public class UdpSubmitData
	{
		/// <summary>
		/// 地址
		/// </summary>
		public string ip;
		/// <summary>
		/// 端口
		/// </summary>
		public int port;
		/// <summary>
		/// 消息队列
		/// </summary>
		public List<UdpPark> parkList;
		/// <summary>
		/// 发送时间Ms
		/// </summary>
		public DateTime createTime;
		/// <summary>
		/// 发送次数
		/// </summary>
		public int count;
	}

	/// <summary>
	/// UDP应答包
	/// </summary>
	public class UdpSubmitComplate
	{
		/// <summary>
		/// 消息包编码
		/// </summary>
		public long parkGroupCode;
		/// <summary>
		/// 1-完成 0-失败
		/// </summary>
		public byte complate;
		/// <summary>
		/// 失败的列表索引
		/// </summary>
		public List<int> getList = new List<int>();

		/// <summary>
		/// 获取字节
		/// </summary>
		/// <returns></returns>
		public byte[] GetBytes()
		{
			List<byte> dataBuf = new List<byte>();
			byte[] codeBuf = BitConverter.GetBytes(parkGroupCode);
			dataBuf.Add(codeBuf[0]);
			dataBuf.Add(codeBuf[1]);
			dataBuf.Add(codeBuf[2]);
			dataBuf.Add(codeBuf[3]);
			dataBuf.Add(codeBuf[4]);
			dataBuf.Add(codeBuf[5]);
			dataBuf.Add(codeBuf[6]);
			dataBuf.Add(codeBuf[7]);

			dataBuf.Add(complate);
			byte[] countList = BitConverter.GetBytes(getList.Count);
			for (int i = 0; i < countList.Length; ++i)
			{
				dataBuf.Add(countList[i]);
			}

			for (int i = 0; i < getList.Count; ++i)
			{
				byte[] indexBuf = BitConverter.GetBytes(getList[i]);
				dataBuf.Add(indexBuf[0]);
				dataBuf.Add(indexBuf[1]);
				dataBuf.Add(indexBuf[2]);
				dataBuf.Add(indexBuf[3]);
			}

			return dataBuf.ToArray();
		}

		/// <summary>
		/// 设置数据
		/// </summary>
		public void SetData(byte[] data)
		{
			parkGroupCode = BitConverter.ToInt64(data, 0);

			complate = data[8];

			if (complate == 0)
			{
				int indexCount = BitConverter.ToInt32(data, 9);

				for (int i = 0; i < indexCount; ++i)
				{
					int index = BitConverter.ToInt32(data, i * 4 + 13);

					getList.Add(index);
				}
			}
		}
	}

	/// <summary>
	/// 超时时间Ms
	/// </summary>
	public static int outTime = 3000;

	/// <summary>
	/// 发送过的包
	/// </summary>
	public static Dictionary<long, UdpSubmitData> messagePardList = new Dictionary<long, UdpSubmitData>();

	public static object lockObj = new object();

	/// <summary>
	/// 运行
	/// </summary>
	public static bool run;
	/// <summary>
	/// 开始
	/// </summary>
	public static void Start()
	{
		run = true;
		new Thread(() =>
		{
			while (run)
			{
				UpData();

				Thread.Sleep(500);
			}
		}).Start();
	}
	/// <summary>
	/// 关闭
	/// </summary>
	public static void Close()
	{
		run = false;
	}

	/// <summary>
	/// 记住应答
	/// </summary>
	/// <param name="data"></param>
	public static void RecordData(List<UdpPark> data, string ip, int port)
	{
		if (data == null || data.Count == 0)
		{
			DebugLoger.LogError("要记录的数据空");
			return;
		}

		UdpSubmitData udpSubmitData = null;
		long key = data[0]._ParkGroupCode;
		lock (lockObj)
		{
			if (messagePardList.ContainsKey(key))
			{
				udpSubmitData = messagePardList[key];
			}
			else
			{
				udpSubmitData = new UdpSubmitData();
				messagePardList.Add(key, udpSubmitData);
			}

			udpSubmitData.parkList = data;
			udpSubmitData.createTime = DateTime.Now;
			udpSubmitData.count = 0;
			udpSubmitData.ip = ip;
			udpSubmitData.port = port;
		}
	}

	/// <summary>
	/// 移除应答
	/// </summary>
	/// <param name="key"></param>
	public static void RemoveSubmit(long key)
	{
		lock (lockObj)
		{
			if (messagePardList.ContainsKey(key))
			{
				messagePardList.Remove(key);
			}
		}
	}

	/// <summary>
	/// 获取应答
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public static UdpSubmitData GetSubmit(long key)
	{
		lock (lockObj)
		{
			if (messagePardList.ContainsKey(key))
			{
				return messagePardList[key];
			}
		}

		return null;
	}

	/// <summary>
	/// 清理记录数据
	/// </summary>
	public static void ClearRecordData()
	{
		messagePardList.Clear();
	}

	/// <summary>Update
	/// 刷新应答
	/// </summary>
	public static void UpData()
	{
		List<UdpSubmitData> needSendParkList = new List<UdpSubmitData>();
		List<long> needRemoveLong = new List<long>();
		List<long> messageKeys = null;

		lock (lockObj)
		{
			messageKeys = new List<long>(messagePardList.Keys);
		}

		for (int index = 0; index < messageKeys.Count; ++index)
		{
			UdpSubmitData usd = GetSubmit(messageKeys[index]);
			if (usd != null)
			{
				TimeSpan span = System.DateTime.Now - usd.createTime;

				if (span.TotalMilliseconds > outTime)
				{
					if (usd.count < 4)
					{
						usd.createTime = System.DateTime.Now;
						usd.count++;
						needSendParkList.Add(usd);
					}
					else
					{
						needRemoveLong.Add(messageKeys[index]);
					}
				}
			}
		}

		for (int i = 0; i < needRemoveLong.Count; ++i)
		{
			RemoveSubmit(needRemoveLong[i]);
		}

		//DebugLoger.Log("需要重发的包 数量:" + needSendParkList.Count + " 总数量:" + messageKeys.Count + " 超时移除:" + needRemoveLong.Count);
		//重发
		for (int i = 0; i < needSendParkList.Count; ++i)
		{
			UdpSubmitData udpParkList = needSendParkList[i];

			if (UdpNetWork.HasInstance())
			{
				UdpNetWork.Instance.SendParkList(udpParkList.parkList, udpParkList.ip, udpParkList.port);
			}
		}
	}
}

/// <summary>
/// Udp分包 组包线管理器
/// </summary>
public class UdpLineParkTool
{
	/// <summary>
	/// 流线包组合工具
	/// </summary>
	public class UdpLineParkGroup
	{
		/// <summary>
		/// 包号
		/// </summary>
		public long _ParkGroupCode;
		/// <summary>
		/// Ip地址
		/// </summary>
		public string _IpString;
		/// <summary>
		/// 端口号
		/// </summary>
		public int _Port;
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime _CreateTime;
		/// <summary>
		/// 包列
		/// </summary>
		public List<UdpPark> ParkList;
		/// <summary>
		/// 重抓次数
		/// </summary>
		public int _ReGetCount;
	}
	/// <summary>
	/// 组包管线
	/// </summary>
	public static Dictionary<long, UdpLineParkGroup> udpLineParkManager = new Dictionary<long, UdpLineParkGroup>();
	public static object lockObj = new object();
	/// <summary>
	/// 运行
	/// </summary>
	public static bool run;
	/// <summary>
	/// 开始
	/// </summary>
	public static void Start()
	{
		run = true;
		new Thread(() =>
		{
			while (run)
			{
				UpData();

				Thread.Sleep(500);
			}
		}).Start();
	}

	/// <summary>
	/// 关闭
	/// </summary>
	public static void Close()
	{
		run = false;
	}

	/// <summary>
	/// 添加到数据包流线分包管理器中 并且查看包是否可以组合  如果能组合就返回组合后数据  否则空
	/// </summary>
	/// <param name="ipStr"></param>
	/// <param name="port"></param>
	/// <param name="udpPark"></param>
	/// <returns></returns>
	public static byte[] AddPark(string ipStr, int port, UdpPark udpPark)
	{
		byte[] bufferByte = null;
		lock (lockObj)
		{
			UdpLineParkGroup udpLineParkGroup = null; //udpLineParkManager.Find(item => item._IpString == ipStr && item._Port == port && item._ParkGroupCode == udpPark._ParkGroupCode);

			if (udpLineParkManager.ContainsKey(udpPark._ParkGroupCode))
			{
				udpLineParkGroup = udpLineParkManager[udpPark._ParkGroupCode];
			}

			if (udpLineParkGroup == null)
			{
				//创建新的
				udpLineParkGroup = new UdpLineParkGroup();
				udpLineParkGroup._IpString = ipStr;
				udpLineParkGroup._Port = port;
				udpLineParkGroup._ParkGroupCode = udpPark._ParkGroupCode;
				udpLineParkGroup._CreateTime = DateTime.Now;
				udpLineParkGroup._ReGetCount = 0;

				udpLineParkGroup.ParkList = udpLineParkGroup.ParkList == null ? new List<UdpPark>() : udpLineParkGroup.ParkList;

				udpLineParkManager.Add(udpLineParkGroup._ParkGroupCode, udpLineParkGroup);
			}

			if (udpLineParkGroup.ParkList.Find(item => item._ParkIndex == udpPark._ParkIndex) != null)
			{
				return null;
			}

			udpLineParkGroup.ParkList.Add(udpPark);

			bufferByte = UdpParkTool.Combination(udpLineParkGroup.ParkList);

			if (bufferByte != null)
			{
				udpLineParkManager.Remove(udpLineParkGroup._ParkGroupCode);
			}

		}

		return bufferByte;
	}

	/// <summary>
	/// 获取已经
	/// </summary>
	/// <param name="groupCode"></param>
	/// <returns></returns>
	public static UdpLineParkGroup GetPark(long groupCode)
	{
		UdpLineParkGroup udpLineParkGroup = null; //udpLineParkManager.Find(item => item._IpString == ipStr && item._Port == port && item._ParkGroupCode == udpPark._ParkGroupCode);
		lock (lockObj)
		{
			if (udpLineParkManager.ContainsKey(groupCode))
			{
				udpLineParkGroup = udpLineParkManager[groupCode];
			}
		}
		return udpLineParkGroup;
	}


	/// <summary>
	/// 清理数据
	/// </summary>
	public static void Clear()
	{
		lock (lockObj)
		{
			udpLineParkManager.Clear();
		}
	}
	/// <summary>
	/// 更新数据
	/// </summary>
	public static void UpData()
	{
		List<long> keys = null;
		lock (lockObj)
		{
			keys = new List<long>(udpLineParkManager.Keys);
		}
		List<UdpLineParkGroup> sendParks = new List<UdpLineParkGroup>();
		List<long> removeParkKeys = new List<long>();
		for (int i = 0; i < keys.Count; ++i)
		{
			long key = keys[i];

			lock (lockObj)
			{
				if (udpLineParkManager.ContainsKey(key))
				{
					UdpLineParkGroup udpGroup = udpLineParkManager[key];

					if ((DateTime.Now - udpGroup._CreateTime).TotalMilliseconds > UdpSubmit.outTime)
					{
						if (udpGroup._ReGetCount < 4)
						{
							udpGroup._ReGetCount++;
							sendParks.Add(udpGroup);
						}
						else
						{
							removeParkKeys.Add(key);
						}
					}
				}
			}
		}

		for (int i = 0; i < removeParkKeys.Count; ++i)
		{
			lock (lockObj)
			{
				if (udpLineParkManager.ContainsKey(removeParkKeys[i]))
				{
					udpLineParkManager.Remove(removeParkKeys[i]);
				}
			}
		}

		for (int i = 0; i < sendParks.Count; ++i)
		{
			UdpLineParkGroup udpGroup = sendParks[i];

			List<int> indexList = new List<int>();
			if (udpGroup.ParkList != null)
			{
				for (int ind = 0; ind < udpGroup.ParkList.Count; ++ind)
				{
					indexList.Add(udpGroup.ParkList[ind]._ParkIndex);
				}
			}

			udpGroup._CreateTime = DateTime.Now;
			SendComplate(udpGroup._IpString, udpGroup._Port, udpGroup._ParkGroupCode, 0, indexList);
		}
		sendParks.Clear();
	}

	/// <summary>
	/// 发送完成数据
	/// </summary>
	/// <param name="code">包编码</param>
	/// <param name="complate">0没有完成 1完成</param>
	/// <param name="indexs">完成接收的Index</param>
	public static void SendComplate(string ip, int port, long code, byte complate, List<int> indexs)
	{
		UdpSubmit.UdpSubmitComplate complateData = new UdpSubmit.UdpSubmitComplate();
		complateData.parkGroupCode = code;
		complateData.complate = complate;

		if (indexs != null)
		{
			complateData.getList = indexs;
		}

		byte[] complateBuf = complateData.GetBytes();

		UdpNetWork.Instance.SendMessageUdpCompalte(ip, port, complateBuf, 1);
	}
}

/// <summary>
/// 完成管理器 过滤多包作用
/// </summary>
public class ComplateRecorder
{
	public static object lockObj = new object();

	public static Dictionary<long, long> complateRecorder = new Dictionary<long, long>();

	/// <summary>
	/// 运行
	/// </summary>
	public static bool run;
	/// <summary>
	/// 开始
	/// </summary>
	public static void Start()
	{
		run = true;
		new Thread(() =>
		{
			while (run)
			{
				UpData();

				Thread.Sleep(500);
			}
		}).Start();
	}
	/// <summary>
	/// 关闭
	/// </summary>
	public static void Close()
	{
		run = false;
	}

	/// <summary>
	/// 是否能接收
	/// </summary>
	/// <returns></returns>
	public static bool CanRecive(long id)
	{
		lock (lockObj)
		{
			if (complateRecorder.ContainsKey(id))
			{
				return false;
			}
		}

		return true;
	}
	/// <summary>
	/// 添加记录
	/// </summary>
	/// <param name="id"></param>
	public static void AddRecorder(long id)
	{
		lock (lockObj)
		{
			if (!complateRecorder.ContainsKey(id))
			{
				complateRecorder.Add(id, DateTime.Now.Ticks);
			}
		}
	}

	/// <summary>
	/// 移除记录
	/// </summary>
	/// <param name="id"></param>
	public static void RemoverRecorder(long id)
	{
		lock (lockObj)
		{
			if (complateRecorder.ContainsKey(id))
			{
				complateRecorder.Remove(id);
			}
		}
	}

	/// <summary>
	/// 获取记录
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public static long GetRecorder(long id)
	{
		lock (lockObj)
		{
			if (complateRecorder.ContainsKey(id))
			{
				return complateRecorder[id];
			}
		}
		return 0;
	}

	/// <summary>
	/// 更新清理
	/// </summary>
	public static void UpData()
	{
		List<long> keys = null;
		lock (lockObj)
		{
			keys = new List<long>(complateRecorder.Keys);
		}

		long timeOut = UdpSubmit.outTime * 5;
		for (int i = 0; i < keys.Count; ++i)
		{
			long timeTicks = GetRecorder(keys[i]);
			if (timeTicks != 0)
			{
				long timeSencend = (DateTime.Now.Ticks - timeTicks) / 10000000;
				if (timeSencend > timeOut)
				{
					RemoverRecorder(keys[i]);
				}
			}
		}
	}
}

/// <summary>
/// 封包信息
/// </summary>
public class UdpPark
{
	//包的分组码
	public long _ParkGroupCode;
	//包的索引
	public int _ParkIndex;
	//包结束标志
	public int _ParkEndTag;
	//需要1-游戏消息(需要应答) 0-应答消息(应答消息)
	public byte _NeedComplate;
	//二进制数据
	public byte[] _MsgDate;

	public UdpPark(long parkGroupCode, int parkIndex, int parkEndTag, byte needComplate, byte[] msgDate)
	{
		_ParkGroupCode = parkGroupCode;

		_ParkIndex = parkIndex;

		_ParkEndTag = parkEndTag;

		_NeedComplate = needComplate;

		_MsgDate = msgDate;
	}
}

/// <summary>
/// UDP工具
/// </summary>
public class UdpParkTool
{
	private static object lockObj = new object();
	private static long id = 0;
	public static long GetLongId()
	{
		long value = 0;
		lock (lockObj)
		{
			id++;
			value = id;
		}
		return value;
	}

	/// <summary>
	/// 分割数据为包
	/// </summary>
	/// <param name="msgBuf"></param>
	/// <param name="bufferLenght"></param>
	/// <returns></returns>
	public static List<UdpPark> Separate(byte[] msgBuf, int bufferLenght, byte needComplate)
	{
		int messageLenght = bufferLenght - 17;

		int Toald = msgBuf.Length / messageLenght;

		int Remainder = msgBuf.Length % messageLenght;

		if (Remainder > 0)
		{
			Toald++;
		}

		Random Rd = new Random();

		long parkGroupCode = GetLongId();

		List<UdpPark> parkList = new List<UdpPark>();

		//循环拆包
		for (int parkIndex = 0; parkIndex < Toald; parkIndex++)
		{
			//0标志 不结束
			int endTag = 0;

			byte[] chunk = null;

			if (Remainder > 0 && (parkIndex + 1) >= Toald)
			{
				//结束包
				chunk = new byte[Remainder + 17];

				endTag = 1;

				byte[] groupCodebuf = BitConverter.GetBytes(parkGroupCode);
				byte[] indexBuf = BitConverter.GetBytes(parkIndex);
				byte[] endBuf = BitConverter.GetBytes(endTag);

				Buffer.BlockCopy(groupCodebuf, 0, chunk, 0, groupCodebuf.Length);
				Buffer.BlockCopy(indexBuf, 0, chunk, 8, indexBuf.Length);
				Buffer.BlockCopy(endBuf, 0, chunk, 12, endBuf.Length);
				chunk[16] = needComplate;

				Buffer.BlockCopy(msgBuf, parkIndex * messageLenght, chunk, 17, Remainder);
			}
			else
			{
				chunk = new byte[bufferLenght];
				byte[] groupCodebuf = BitConverter.GetBytes(parkGroupCode);
				byte[] indexBuf = BitConverter.GetBytes(parkIndex);
				byte[] endBuf = BitConverter.GetBytes(endTag);

				Buffer.BlockCopy(groupCodebuf, 0, chunk, 0, groupCodebuf.Length);
				Buffer.BlockCopy(indexBuf, 0, chunk, 8, indexBuf.Length);
				Buffer.BlockCopy(endBuf, 0, chunk, 12, endBuf.Length);
				chunk[16] = needComplate;

				Buffer.BlockCopy(msgBuf, parkIndex * messageLenght, chunk, 17, messageLenght);
			}

			parkList.Add(new UdpPark(parkGroupCode, parkIndex, endTag, needComplate, chunk));
		}

		return parkList;
	}

	/// <summary>
	/// 组合从网洛接收到的数据包
	/// </summary>
	/// <param name="sourceParkList"></param>
	/// <returns></returns>
	public static byte[] Combination(List<UdpPark> sourceParkList)
	{
		if (sourceParkList == null)
		{
			return null;
		}

		if (sourceParkList.Count == 0)
		{
			DebugLoger.LogError("sourceParkList count 0");
			return null;
		}
		UdpPark firstDate = sourceParkList.Find(item => item._ParkIndex == 0);

		UdpPark endParkDate = sourceParkList.Find(item => item._ParkEndTag == 1);


		//验证完整性
		if (endParkDate == null || firstDate == null)
		{
			if (endParkDate == null)
			{
				//DebugLoger.LogError("验证完整状态失败 endParkDate null");
			}

			if (firstDate == null)
			{
				//DebugLoger.LogError("验证完整状态失败 firstDate null");
			}
			//验证不了
			return null;
		}

		if ((endParkDate._ParkIndex + 1) > sourceParkList.Count)
		{
			//索引加1 数据包不足
			//DebugLoger.LogError("验证完整状态失败 二");
			return null;
		}

		int msgLenght = firstDate._MsgDate.Length;

		//全部数据包长度  数据条数-1 个 数据长度 加上尾包长度
		int messageCount = msgLenght * (sourceParkList.Count - 1) + endParkDate._MsgDate.Length;

		byte[] messageDate = new byte[messageCount];

		for (int parkIndex = 0; parkIndex < sourceParkList.Count; parkIndex++)
		{
			//循环组合
			UdpPark udpPark = sourceParkList[parkIndex];

			///直接按照索引对齐包数据
			udpPark._MsgDate.CopyTo(messageDate, udpPark._ParkIndex * msgLenght);
		}

		return messageDate;
	}

	/// <summary>
	/// 通过数据 实例化包  从网络接收到的数据就用来实例化包
	/// </summary>
	/// <param name="buffer"></param>
	public static UdpPark InstancePark(byte[] buffer)
	{
		if (buffer == null || buffer.Length < 16) return null;

		long parkGroupCode = BitConverter.ToInt64(buffer, 0);//8位

		int parkIndex = BitConverter.ToInt32(buffer, 8);//4位

		int parkEndTag = BitConverter.ToInt32(buffer, 12);//4位

		byte needComplate = buffer[buffer[16]];

		int remainderCount = buffer.Length - 17;

		byte[] msgDate = new byte[remainderCount];

		Buffer.BlockCopy(buffer, 17, msgDate, 0, remainderCount);

		UdpPark up = new UdpPark(parkGroupCode, parkIndex, parkEndTag, needComplate, msgDate);

		return up;
	}
}

