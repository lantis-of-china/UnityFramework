using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using CherishWebGLSupport;

	//需要UDP
public  class UserNetWork
{
        public bool run;        

        public CherishSocket CenterNetServer;

        public bool isConnect = false;

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

        private static UserNetWork _instance;
        public static UserNetWork Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserNetWork();
                    count++;
                }
                return _instance;
            }
        }

        /// <summary>
        /// 实例化一个NetWork类
        /// </summary>
        public UserNetWork()
        {
            GoableData.ServerIpaddress.gameServerIpV6 = GoableData.ServerIpaddress.gameServerIp;
            openExpecation = true;
			CherishSocket.AddressFamily useFamily = CherishSocket.AddressFamily.InterNetwork;


            GoableData.ServerIpaddress.gameServerIpV6 = NetDataManager.DomainIp(GoableData.ServerIpaddress.gameServerIpV6, NetDataManager.DomainExctption, ref useFamily);
            CenterNetServer = new CherishSocket(useFamily, CherishSocket.SocketType.Stream, CherishSocket.ProtocolType.Tcp);

            /* 上两句话代替了下面的代码
             * 作用是支持Ipv6域名解析
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string newIp = GoableData.ServerIpaddress.gameServerIpV6;
                IpConvert.getIPType(GoableData.ServerIpaddress.gameServerIp, GoableData.ServerIpaddress.gameServerPort.ToString(), out newIp, out useFamily);
                GoableData.ServerIpaddress.gameServerIpV6 = newIp;
                
                if (useFamily == AddressFamily.InterNetworkV6)
                {
                    //ipv6 Iphone 使用
                    CenterNetServer = new Socket(useFamily, SocketType.Stream, ProtocolType.Tcp);
                }
                else
                {
                    CenterNetServer = new Socket(useFamily, SocketType.Stream, ProtocolType.Tcp);
                }
            }
            else
            {
                CenterNetServer = new Socket(useFamily, SocketType.Stream, ProtocolType.Tcp);
            }
             * */
            CenterNetServer.Blocking = true;
            CenterNetServer.SendTimeout = 3000;
            CenterNetServer.ReceiveTimeout = 0;
            //CenterNetServer.ReceiveBufferSize = 2048;
            //StartAsyn();
            Start();
        }

        #region 同步Scoekt
        public void Start()
        {
            isConnect = false;
            run = true;

            Thread tnew = new Thread(new ThreadStart(delegate
            {
                try 
                {
                    DebugLoger.Log(GoableData.ServerIpaddress.gameServerIpV6 + ":" + GoableData.ServerIpaddress.gameServerPort);
                    CenterNetServer.Connect(GoableData.ServerIpaddress.gameServerIpV6, GoableData.ServerIpaddress.gameServerPort);
                }
                catch(Exception e)
                {
                    UINameSpace.UILogin.loginError = true;
                    Debug.LogError(e.ToString());
                    return;
                }

                isConnect = true;

                while (run)
                {
					Thread.Sleep(5);
                    ReciveDateUdp(CenterNetServer);
                }
            }));
            tnew.Start();
            
        }
        public static void ReciveDateTcp(CherishSocket clientSocket)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] lengthBuf = new byte[4];
            byte[] LenghtByte = new byte[4];
            byte[] DateBuffer = new byte[1024];
            int DateLenght = 0;
            int CurrentLenght = 0;
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

                    for (int i = 0; i < curRecCount; ++i)
                    {
                        LenghtByte[reciveStartLength + i] = lengthBuf[i];
                    }

                    reciveStartLength += curRecCount;
                }
                catch
                {
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

                    ReadLenght = clientSocket.Receive(DateBuffer, ReadLenght, CherishSocket.SocketFlags.None);
                    CurrentLenght += ReadLenght;

                    if (ReadLenght <= 0)
                    {
                        break;
                    }
                }
                catch
                {
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

            ms.Flush();
            byte[] MessageByte = ms.ToArray();
            ms.Close();


            //IPEndPoint removeIep = (clientSocket.RemoteEndPoint as IPEndPoint);


            //处理消息调度
            //GameMain.Instance.clrHotMain.CallMethod<object>(GameMain.Instance.clrHotMain.threadContext, null, "Server.Process.MessageDriver", "AddMessage", MessageByte, clientSocket, removeIep.Address.ToString(), removeIep.Port);
            Server.Process.MessageDriver.AddMessage(MessageByte, clientSocket, ""/*removeIep.Address.ToString()*/, /*removeIep.Port*/0);
        }

        /// <summary>
        /// UDP接收数据
        /// </summary>
        /// <param name="clientSocket"></param>
        private void ReciveDateUdp(CherishSocket clientSocket)
        {
            ReciveDateTcp(clientSocket);
            //byte[] readByte = new byte[clientSocket.ReceiveBufferSize];

            //System.Net.IPEndPoint iep = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

            //System.Net.EndPoint ep = iep;

            //while (true)
            //{
            //    int reciveCount = clientSocket.ReceiveFrom(readByte, ref ep);
            //    DebugLoger.LogError("reciveCount " + reciveCount);
            //    byte[] msgBuf = new byte[reciveCount];

            //    Buffer.BlockCopy(readByte, 0, msgBuf, 0, reciveCount);

            //    UdpPark udpPark = UdpParkTool.InstancePark(msgBuf);

            //    if (udpPark == null)
            //    {
            //        //封包解析失败
            //        DebugLoger.LogError("严重问题 Udp包解析失败失败");
            //        return;
            //    }

            //    string ipStr = (ep as System.Net.IPEndPoint).Address.ToString();

            //    int port = (ep as System.Net.IPEndPoint).Port;

            //    //组合后数据
            //    byte[] msgComDate = UdpLineParkTool.AddPark(UdpLineParkManager, ipStr, port, udpPark);

            //    if (msgComDate == null)
            //    {
            //        //组合失败
            //        DebugLoger.LogError("严重问题 组合Udp包失败");
            //        continue;
            //    }

            //    byte[] MessageByte = msgComDate;

            //    //处理消息调度
            //    //GameMain.Instance.clrHotMain.CallMethod<object>(GameMain.Instance.clrHotMain.threadContext, null, "Server.Process.MessageDriver", "AddMessage", MessageByte, null, ipStr, port);
            //    Server.Process.MessageDriver.AddMessage(MessageByte, null, ipStr, port);
            //}
        }
        #endregion 同步Socket

        private static void SocketExpection()
        {
            if (!openExpecation)
            {
                return;
            }
            openExpecation = false;

            if (HasInstance())
            {
                UserNetWork.Instance.CloseSocket();
            }

            UINameSpace.UILogin.OutLineSys();
        }





        /// <summary>
        /// 发送消息到客户端UDP 
        /// </summary>
        /// <typeparam name="T">发送的消息类型</typeparam>
        /// <param name="ClientSocket">接受消息的客户端套接字</param>
        /// <param name="Date">发送的消息的实例</param>
        /// <param name="messageType">消息的类型</param>
        public  void SendMessageTcp<T>(string ip,int port, T Date,NetMessageType messageType) where T :  LantisBitProtocolBase
        {
            NetDataManager.SendMessageTcp<T>(CenterNetServer, Date, (int)messageType, false);
            //new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            //{
            //    byte[] TypeDate = System.BitConverter.GetBytes((int)messageType);

            //    byte[] bufferDate = GameSerialzTool.Serializer<T>(Date); 

            //    int Lenght = TypeDate.Length + bufferDate.Length;

            //    byte[] sendBuffer = new byte[Lenght];
               
            //    TypeDate.CopyTo(sendBuffer, 0); 
                  
            //    bufferDate.CopyTo(sendBuffer, 4);


            //    List<UdpPark> ParkList = UdpParkTool.Separate(sendBuffer, 2048);

            //    SendParkList(ParkList, ip, port);
                
            //})).Start();
            
        }

        public void SendParkList(List<UdpPark> parkList,string ip,int port)
        {
            for (int index = 0; index < parkList.Count; index++)
            {
                byte[] sendMessageBuf = parkList[index]._MsgDate;

                CenterNetServer.SendTo(sendMessageBuf,ip, port);
            }
        }


        /// <summary>
        /// 发送消息到客户端UDP 
        /// </summary>
        /// <typeparam name="T">发送的消息类型</typeparam>
        /// <param name="ClientSocket">接受消息的客户端套接字</param>
        /// <param name="Date">发送的消息的实例</param>
        /// <param name="messageType">消息的类型</param>
        public void SendMessageUdp<T>(string ip, int port, T Date, int messageType) where T : LantisBitProtocolBase
    {
            byte[] _typeDate = System.BitConverter.GetBytes((int)messageType);
            byte[] _bufferDate = Date.Serializer();
            int Lenght = _typeDate.Length + _bufferDate.Length;
            byte[] sendBuffer = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.IO.BinaryWriter bs = new System.IO.BinaryWriter(ms))
                {
                    bs.Write(Lenght);
                    bs.Write(_typeDate);
                    bs.Write(_bufferDate);
                }

                ms.Flush();
                sendBuffer = ms.ToArray();
            }


            Server.Process.MessageDriver.AddSendMessage(CenterNetServer, sendBuffer);
            //NetDataManager.SendMessageTcp<T>(CenterNetServer, Date, messageType, false);
        }

        public void StopSocket()
        {
            openExpecation = false;
            run = false;
            _instance = null;            
        }


        public void CloseSocket(Action loginOutCall = null)
        {
		if (loginOutCall != null)
		{
			loginOutCall();
		}
            openExpecation = false;
            run = false;
            _instance = null;
			DebugLoger.Log("关闭子游戏网络连接");
            TcpGoable.SafeClose(CenterNetServer);
        }
    }