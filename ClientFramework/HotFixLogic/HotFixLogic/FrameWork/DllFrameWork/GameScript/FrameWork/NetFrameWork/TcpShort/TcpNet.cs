using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Collections.Generic;
using CherishWebGLSupport;

public class TcpGoable
{
	
	public static List<CherishSocket> socketList = new List<CherishSocket>();

    public static void AddSocket(CherishSocket _socket)
    {
        socketList.Add(_socket);
    }

    public static void Distance()
    {
        for (short loop = 0; loop < socketList.Count; ++loop)
        {
			CherishSocket _socket = socketList[loop];

            try
            {
                SafeClose(_socket);
            }
            catch
            {
                DebugLoger.LogError("Tcp WWW error Disconnect");
            }
        }
        socketList.Clear();
    }
    
    /// <summary>  
    /// Close the socket safely.  
    /// </summary>  
    /// <param name="socket">The socket.</param>  
    public static void SafeClose(CherishSocket socket)
    {
        if (socket == null)
            return;

        if (!socket.Connected)
            return;

        try
        {
            socket.Shutdown(CherishSocket.SocketShutdown.Both);
        }
        catch
        {
        }

        try
        {
            socket.Close();
        }
        catch
        {
        }
    }  
}

public class TcpNet<T> where T : CherishBitProtocolBase
{
    public static int OutTime = 10000;

    public TcpNet(string IpAddress, int Port, MonoBehaviour BehaviourInstance, T dataPark,int netMessageType)
    {
		TcpNetSend.Send(OutTime,IpAddress, Port, BehaviourInstance, dataPark, netMessageType);
    }
}

public class TcpNetSend
{
	public static void Send<T>(int outTime,string IpAddress, int Port, MonoBehaviour BehaviourInstance, T dataPark, int netMessageType) where T : CherishBitProtocolBase
	{
		CherishSocket.AddressFamily useFamily = CherishSocket.AddressFamily.InterNetwork;
		IpAddress = NetDataManager.DomainIp(IpAddress, NetDataManager.DomainExctption, ref useFamily);

		if (string.IsNullOrEmpty(IpAddress))
		{

			GoableData.ServerIpaddress.isLoginLogServerSend = false;
			return;
		}

		new Thread(new ThreadStart(delegate
		{
			CherishSocket clent_net = null;
			clent_net = new CherishSocket(useFamily, CherishSocket.SocketType.Stream, CherishSocket.ProtocolType.Tcp);
			TcpGoable.AddSocket(clent_net);

			//连接远程主机
			try
			{
				clent_net.SendTimeout = outTime;
				clent_net.ReceiveTimeout = outTime;
				clent_net.Blocking = true;
				clent_net.Connect(IpAddress, Port);
				NetDataManager.NotThreadSendMessageTcp<T>(clent_net, dataPark, (int)netMessageType);

			}
			catch (Exception e)
			{
				//DebugLoger.LogError("连接异常" + e.ToString());
				UINameSpace.UILogin.loginError = true;
				clent_net.Close();
				TcpGoable.socketList.Remove(clent_net);
			}
		})).Start();
	}
}

public class NetDataManager
{
	public static void NotThreadSendMessageTcp<T>(CherishSocket ClientSocket, T Date, int messageType, bool needRecaveCall = true) where T : CherishBitProtocolBase
	{
		byte[] _typeDate = System.BitConverter.GetBytes((int)messageType);
		byte[] _bufferDate = Date.Serializer();

		object[] objParams = new object[2];
		objParams[0] = (object)_typeDate;
		objParams[1] = (object)_bufferDate;

		if (ClientSocket.Connected)
		{
			object[] objectArray = objParams;
			byte[] TypeDate = (byte[])objectArray[0];
			byte[] bufferDate = (byte[])objectArray[1];

			Monitor.Enter(((ICollection)bufferDate).SyncRoot);
			//lock (((ICollection)bufferDate).SyncRoot)
			//{
			int Lenght = TypeDate.Length + bufferDate.Length;

			byte[] LenghtDate = System.BitConverter.GetBytes(Lenght);

			byte[] sendBuffer = null;

			using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
			{
				using (System.IO.BinaryWriter bs = new System.IO.BinaryWriter(ms))
				{
					bs.Write(Lenght);
					bs.Write(TypeDate);
					bs.Write(bufferDate);
				}

				ms.Flush();
				sendBuffer = ms.ToArray();
			}

			Monitor.Enter(((ICollection)sendBuffer).SyncRoot);
			//lock (((ICollection)sendBuffer).SyncRoot)
			//{
			Monitor.Enter(ClientSocket);
			//lock (ClientSocket)
			//{
			int remSendCount = 0;
			byte[] byteBuf = new byte[2048];
			while (remSendCount < sendBuffer.Length)
			{
				if ((sendBuffer.Length - remSendCount) > 2048)
				{
					Array.Copy(sendBuffer, remSendCount, byteBuf, 0, byteBuf.Length);
					remSendCount += 2048;
					ClientSocket.Send(byteBuf);
				}
				else
				{
					byte[] sendRemBuf = new byte[sendBuffer.Length - remSendCount];
					Array.Copy(sendBuffer, remSendCount, sendRemBuf, 0, sendRemBuf.Length);
					ClientSocket.Send(sendRemBuf);
					remSendCount = sendBuffer.Length;
				}
			}
			//}
			Monitor.Exit(ClientSocket);
			//}
			Monitor.Exit(((ICollection)sendBuffer).SyncRoot);
			//}
			Monitor.Exit(((ICollection)bufferDate).SyncRoot);


			if (needRecaveCall)
			{
				ReciveDateTcp(ClientSocket);
			}

		}
		else
		{
			DebugLoger.Log("SendMessageTcp Connected error");
		}
	}
	
	public static void SendMessageTcp<T>(CherishSocket ClientSocket, T Date, int messageType, bool needRecaveCall = true) where T : CherishBitProtocolBase
	{
		byte[] _typeDate = System.BitConverter.GetBytes((int)messageType);
		byte[] _bufferDate = Date.Serializer();

		object[] objParams = new object[2];
		objParams[0] = (object)_typeDate;
		objParams[1] = (object)_bufferDate;

		Thread thread = new Thread(new ParameterizedThreadStart(delegate (object objData)
		{
			if (ClientSocket.Connected)
			{
				object[] objectArray = (object[])objData;
				byte[] TypeDate = (byte[])objectArray[0];
				byte[] bufferDate = (byte[])objectArray[1];

				Monitor.Enter(((ICollection)bufferDate).SyncRoot);
				//lock (((ICollection)bufferDate).SyncRoot)
				//{
					int Lenght = TypeDate.Length + bufferDate.Length;

					byte[] LenghtDate = System.BitConverter.GetBytes(Lenght);

					byte[] sendBuffer = null;

					using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
					{
						using (System.IO.BinaryWriter bs = new System.IO.BinaryWriter(ms))
						{
							bs.Write(Lenght);
							bs.Write(TypeDate);
							bs.Write(bufferDate);
						}

						ms.Flush();
						sendBuffer = ms.ToArray();
					}

					Monitor.Enter(((ICollection)sendBuffer).SyncRoot);
					//lock (((ICollection)sendBuffer).SyncRoot)
					//{
						Monitor.Enter(ClientSocket);
						//lock (ClientSocket)
						//{
							int remSendCount = 0;
							byte[] byteBuf = new byte[2048];
							while (remSendCount < sendBuffer.Length)
							{
								if ((sendBuffer.Length - remSendCount) > 2048)
								{
									Array.Copy(sendBuffer, remSendCount, byteBuf, 0, byteBuf.Length);
									remSendCount += 2048;
									ClientSocket.Send(byteBuf);
								}
								else
								{
									byte[] sendRemBuf = new byte[sendBuffer.Length - remSendCount];
									Array.Copy(sendBuffer, remSendCount, sendRemBuf, 0, sendRemBuf.Length);
									ClientSocket.Send(sendRemBuf);
									remSendCount = sendBuffer.Length;
								}
							}
						//}
						Monitor.Exit(ClientSocket);
					//}
					Monitor.Exit(((ICollection)sendBuffer).SyncRoot);
				//}
				Monitor.Exit(((ICollection)bufferDate).SyncRoot);


				if (needRecaveCall)
				{
					ReciveDateTcp(ClientSocket);
				}

			}
			else
			{
				DebugLoger.Log("SendMessageTcp Connected error");
			}
		}));

		thread.Start(objParams);
	}

    public static void ReciveDateTcp(CherishSocket clientSocket)
    {
        byte[] lengthBuf = new byte[4];
        byte[] LenghtByte = new byte[4];
        byte[] DateBuffer = new byte[1024];
        int DateLenght = 0;
        int CurrentLenght = 0;


        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        ms.Position = 0;
        int curRecCount = 0;

        CurrentLenght = 0;
        DateLenght = 0;

        int reciveStartLength = 0;
        while (reciveStartLength < 4)
        {
            try
            {
                curRecCount = clientSocket.Receive(lengthBuf, 4 - reciveStartLength, CherishSocket.SocketFlags.None);
                if (curRecCount <= 0)
                {
                    Thread.Sleep(5);
                }
                for (int i = 0; i < curRecCount; ++i)
                {
                    LenghtByte[reciveStartLength + i] = lengthBuf[i];
                }
                reciveStartLength += curRecCount;
            }
            catch(Exception e)
            {
				DebugLoger.LogError(e.ToString());
                SocketExpection();
                return;
            }
        }

        DateLenght = System.BitConverter.ToInt32(LenghtByte, 0);
		
        while (CurrentLenght < DateLenght)
        {
            int ReadLenght = 0;
            try
            {
                if ((DateLenght - CurrentLenght) > DateBuffer.Length)
                {
                    ReadLenght = DateBuffer.Length;
                }
                else
                {
                    ReadLenght = DateLenght - CurrentLenght;
                }

                ReadLenght  = clientSocket.Receive(DateBuffer, ReadLenght, CherishSocket.SocketFlags.None);
                CurrentLenght += ReadLenght;

                if (CurrentLenght <= 0)
                {
                    DebugLoger.Log("准备break 1");
                    break;
                }

            }
            catch
            {
                DebugLoger.Log("准备break 2");
                SocketExpection();
                break;
            }

            ms.Write(DateBuffer, 0, ReadLenght);
        }

        if (CurrentLenght <= 0)
        {
            ms.Close();
            ms.Flush();
            return;
        }
        ms.SetLength(CurrentLenght);
        ms.Position = 0;

        byte[] MessageByte = ms.ToArray();

        ms.Close();
        ms.Flush();

        //IPEndPoint removeIep = (clientSocket.RemoteEndPoint as IPEndPoint);

        try
        {
            clientSocket.Shutdown(CherishSocket.SocketShutdown.Both);
            clientSocket.Close();
            DebugLoger.Log("Close Socket");
        }
        catch
        {
            DebugLoger.LogError("Close Socket Exception");
        }

        //处理消息调度
        //GameMain.Instance.clrHotMain.CallMethod<object>(GameMain.Instance.clrHotMain.threadContext, null, "Server.Process.MessageDriver", "AddMessage", MessageByte, clientSocket, removeIep.Address.ToString(), removeIep.Port);
        Server.Process.MessageDriver.AddMessage(MessageByte, clientSocket, /*removeIep.Address.ToString()*/"", /*removeIep.Port*/0);
    }
	
	public static string DomainIp(string str, Action<string> actionCall, ref CherishSocket.AddressFamily addFamily)
	{
		addFamily = CherishSocket.AddressFamily.InterNetwork;
		if (string.IsNullOrEmpty(str))
		{
			return "";
		}

		string _return = "";
		try
		{
			System.Net.IPAddress[] aryIP = System.Net.Dns.GetHostAddresses(str);

			addFamily = (CherishSocket.AddressFamily)aryIP[0].AddressFamily;

			_return = aryIP[0].ToString();
		}
		catch (Exception e)
		{
			_return = e.Message;
			if (actionCall != null)
			{
				actionCall(_return);
			}
			_return = "";
		}
		return _return;
	}

	/// <summary>
	/// 动态域名解析失败
	/// </summary>
	/// <param name="msg"></param>
	public static void DomainExctption(string msg)
	{
		UINameSpace.UITipMessage.PlayMessage("服务器域名解析失败!");
		UINameSpace.UIWaitting.RemoveShowWaitting(((int)NetMessageType.LoginWithUser_Login_Back).ToString());
		DebugLoger.LogError("解析域名错误" + msg);
	}

    /// <summary>
    /// Socket异常 需要重新登录
    /// </summary>
    public static void SocketExpection()
    {
    }
}