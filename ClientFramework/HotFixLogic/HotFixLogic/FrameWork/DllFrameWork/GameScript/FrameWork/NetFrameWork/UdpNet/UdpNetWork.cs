using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net.Sockets;
using System.Net;


//需要UDP
public class UdpNetWork
{
	public bool run;
	
	public Socket CenterNetServer;

	public int port = 6789;
	public static bool openExpecation = false;

	public static int count = 0;

	public static bool HasInstance()
	{
		if (_instance == null)
		{
			return false;
		}

		return true;
	}

	private static UdpNetWork _instance;
	public static UdpNetWork Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new UdpNetWork();
			}
			return _instance;
		}
	}

	/// <summary>
	/// 实例化一个NetWork类
	/// </summary>
	public UdpNetWork()
	{
		openExpecation = true;

		CenterNetServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		//Start();
	}


	public void Start()
	{
		run = true;
		this.BindAddress();

		Thread tnew = new Thread(new ThreadStart(delegate
		{
			while (run)
			{
				Thread.Sleep(5);
				ReciveDateUdp(CenterNetServer);
			}
		}));

		tnew.Start();
	}

	/// <summary>
	/// 绑定端点  启动侦听
	/// </summary>
	public void BindAddress()
	{
        //DebugLoger.Log(" Network.player.ipAddress = " + Network.player.ipAddress);
#if UNITY_WEBGL && !UNITY_EDITOR
        IPAddress anyIp = IPAddress.Any;
#else
        //string ip = IPAddress.Any;       
		IPAddress anyIp = IPAddress.Any; //IPAddress.Parse(ip);
#endif
        port = ServerRandom.GetRandomIntValue(8090, 45643);
		System.Net.IPEndPoint IEP = new System.Net.IPEndPoint(anyIp, port);
		try
		{
			CenterNetServer.Bind(IEP);
		}
		catch(Exception e)
		{
			//DebugLoger.Log(e.ToString());
			BindExpection();
			port = ServerRandom.GetRandomIntValue(8090, 45643);
			IEnumeratorManager.Instance.StartCoroutine(InvokeBind());
		}
	}

	System.Collections.IEnumerator InvokeBind()
	{
		yield return new IEnumeratorManager.WaitForSeconds(0.1f);
		BindAddress();
	}

	private void BindExpection()
	{
		//DebugLoger.LogError(string.Format("端口{0}绑定失败",port));
	}

	private void AcceptExpection()
	{
		//Console.WriteLine("异常请求进入");
	}

	private static void SocketExpection(string e)
	{
		DebugLoger.LogError("UDP异常 " + e);
	}


	/// <summary>
	/// UDP接收数据
	/// </summary>
	/// <param name="clientSocket"></param>
	private void ReciveDateUdp(Socket clientSocket)
	{
		try
		{
			byte[] readByte = new byte[clientSocket.ReceiveBufferSize];

			System.Net.IPEndPoint iep = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

			System.Net.EndPoint ep = iep;

			while (true)
			{
				int reciveCount = clientSocket.ReceiveFrom(readByte, ref ep);

				byte[] msgBuf = new byte[reciveCount];

				Buffer.BlockCopy(readByte, 0, msgBuf, 0, reciveCount);

				UdpPark udpPark = UdpParkTool.InstancePark(msgBuf);

				if (udpPark == null)
				{
					//封包解析失败
					//DebugLoger.LogError("严重问题 Udp包解析失败失败");
					return;
				}

				string ipStr = (ep as System.Net.IPEndPoint).Address.ToString();

				int port = (ep as System.Net.IPEndPoint).Port;

				//DebugLoger.Log("udp entry ip " + ipStr + " port " + port);

				//组合后数据
				byte[] msgComDate = UdpLineParkTool.AddPark(ipStr, port, udpPark);

				if (msgComDate == null)
				{
					//组合失败
					//DebugLoger.LogError("严重问题 组合Udp包失败");
					continue;
				}

				byte[] MessageByte = msgComDate;

				int msgType = BitConverter.ToInt32(MessageByte, 0);
				if (msgType == 1)
				{
					byte[] dateMsg = new byte[MessageByte.Length - 4];
					Buffer.BlockCopy(MessageByte, 4, dateMsg, 0, dateMsg.Length);
					UdpSubmit.UdpSubmitComplate suc = new UdpSubmit.UdpSubmitComplate();
					suc.SetData(dateMsg);

					if (suc.complate == 1)
					{
						UdpSubmit.RemoveSubmit(suc.parkGroupCode);
					}
					else
					{
						//重发
						UdpSubmit.UdpSubmitData usd = UdpSubmit.GetSubmit(suc.parkGroupCode);

						if (suc.getList != null && usd != null)
						{
							List<UdpPark> parkList = new List<UdpPark>();
							for (int i = 0; i < usd.parkList.Count; ++i)
							{
								bool isFind = false;
								usd.createTime = DateTime.Now;
								UdpPark sendPark = usd.parkList[i];
								for (int getIndex = 0; getIndex < suc.getList.Count; ++getIndex)
								{
									if (sendPark._ParkIndex == suc.getList[getIndex])
									{
										isFind = true;
										break;
									}
								}

								if (!isFind)
								{
									parkList.Add(sendPark);
									SendParkList(parkList, ipStr, port);
								}
							}
						}
					}
					continue;
				}
				else
				{
					if (ComplateRecorder.CanRecive(udpPark._ParkGroupCode))
					{
						//发送确认包
						UdpLineParkTool.SendComplate(ipStr, port, udpPark._ParkGroupCode, 1, null);
					}
				}

				//处理消息调度
				Server.Process.MessageDriver.AddMessage(MessageByte, null, ipStr, port);
			}
		}
		catch (Exception ex)
		{
			//DebugLoger.LogError(ex.ToString());
		}
	}

	/// 发送消息到客户端UDP 
	/// </summary>
	/// <typeparam name="T">发送的消息类型</typeparam>
	/// <param name="ClientSocket">接受消息的客户端套接字</param>
	/// <param name="Date">发送的消息的实例</param>
	/// <param name="messageType">消息的类型</param>
	public void SendMessageUdpCompalte(string ip, int port, byte[] bufferDate, int messageType)
	{
		byte[] TypeDate = System.BitConverter.GetBytes((int)messageType);

		int Lenght = TypeDate.Length + bufferDate.Length;

		byte[] sendBuffer = new byte[Lenght];

		TypeDate.CopyTo(sendBuffer, 0);

		bufferDate.CopyTo(sendBuffer, 4);

		List<UdpPark> ParkList = UdpParkTool.Separate(sendBuffer, 512, 0);

		SendParkList(ParkList, ip, port);
	}


	/// <summary>
	/// 发送消息到客户端UDP 
	/// </summary>
	/// <typeparam name="T">发送的消息类型</typeparam>
	/// <param name="ClientSocket">接受消息的客户端套接字</param>
	/// <param name="Date">发送的消息的实例</param>
	/// <param name="messageType">消息的类型</param>
	public void SendMessageUdp(string ip, int port, byte[] bufferDate, int messageType)
	{
		byte[] TypeDate = System.BitConverter.GetBytes(messageType);

		int Lenght = TypeDate.Length + bufferDate.Length;

		byte[] sendBuffer = new byte[Lenght];

		TypeDate.CopyTo(sendBuffer, 0);

		bufferDate.CopyTo(sendBuffer, 4);

		List<UdpPark> ParkList = UdpParkTool.Separate(sendBuffer, 512, 1);

		SendParkList(ParkList, ip, port);

	}


	/// <summary>
	/// 发送消息到客户端UDP 
	/// </summary>
	/// <typeparam name="T">发送的消息类型</typeparam>
	/// <param name="ClientSocket">接受消息的客户端套接字</param>
	/// <param name="Date">发送的消息的实例</param>
	/// <param name="messageType">消息的类型</param>
	public void SendMessageUdp(string ip, int port, byte[] bufferDate, NetMessageType messageType)
	{
		byte[] TypeDate = System.BitConverter.GetBytes((int)messageType);

		int Lenght = TypeDate.Length + bufferDate.Length;

		byte[] sendBuffer = new byte[Lenght];

		TypeDate.CopyTo(sendBuffer, 0);

		bufferDate.CopyTo(sendBuffer, 4);

		List<UdpPark> ParkList = UdpParkTool.Separate(sendBuffer, 512,1);

		SendParkList(ParkList, ip, port);

	}

	/// <summary>
	/// 发送消息到客户端UDP 
	/// </summary>
	/// <typeparam name="T">发送的消息类型</typeparam>
	/// <param name="ClientSocket">接受消息的客户端套接字</param>
	/// <param name="Date">发送的消息的实例</param>
	/// <param name="messageType">消息的类型</param>
	public void SendMessageUdp<T>(string ip, int port, T Date, NetMessageType messageType) where T : LantisBitProtocolBase
	{
		CherishWebGLSupport.CherishSocket.AddressFamily useFamily = CherishWebGLSupport.CherishSocket.AddressFamily.AppleTalk;
		ip = NetDataManager.DomainIp(ip, NetDataManager.DomainExctption, ref useFamily);

		byte[] TypeDate = System.BitConverter.GetBytes((int)messageType);

		byte[] bufferDate = Date.Serializer();

		int Lenght = TypeDate.Length + bufferDate.Length;

		byte[] sendBuffer = new byte[Lenght];

		TypeDate.CopyTo(sendBuffer, 0);

		bufferDate.CopyTo(sendBuffer, 4);

		List<UdpPark> ParkList = UdpParkTool.Separate(sendBuffer, 512,1);

		SendParkList(ParkList, ip, port);

	}

	public void SendParkList(List<UdpPark> parkList, string ip, int port)
	{
		try
		{
			for (int index = 0; index < parkList.Count; index++)
			{
				byte[] sendMessageBuf = parkList[index]._MsgDate;

				CenterNetServer.SendTo(sendMessageBuf, (int)sendMessageBuf.Length, SocketFlags.None, new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port));
			}
		}
		catch (Exception ex)
		{
			//DebugLoger.LogError(ex.ToString());
		}
	}


	public void StopSocket()
	{
		openExpecation = false;
		run = false;
		_instance = null;
	}


	public void CloseSocket()
	{
		openExpecation = false;
		run = false;
		_instance = null;

		SafeClose(CenterNetServer);
	}

	/// <summary>  
	/// Close the socket safely.  
	/// </summary>  
	/// <param name="socket">The socket.</param>  
	public static void SafeClose(Socket socket)
	{
		if (socket == null)
			return;


		try
		{
			socket.Shutdown(SocketShutdown.Both);
		}
		catch (Exception e)
		{
			DebugLoger.LogError(e.ToString());
		}

		try
		{
			socket.Close();
		}
		catch(Exception e)
		{
			DebugLoger.LogError(e.ToString());
		}
	}
}


