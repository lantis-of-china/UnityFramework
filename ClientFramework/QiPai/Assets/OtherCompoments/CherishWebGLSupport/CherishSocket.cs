using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherishWebGLSupport
{
	public class CherishSocket
	{
		public enum AddressFamily
		{
			Unknown = -1,
			Unspecified = 0,
			Unix = 1,
			InterNetwork = 2,
			ImpLink = 3,
			Pup = 4,
			Chaos = 5,
			NS = 6,
			Ipx = 6,
			Iso = 7,
			Osi = 7,
			Ecma = 8,
			DataKit = 9,
			Ccitt = 10,
			Sna = 11,
			DecNet = 12,
			DataLink = 13,
			Lat = 14,
			HyperChannel = 15,
			AppleTalk = 16,
			NetBios = 17,
			VoiceView = 18,
			FireFox = 19,
			Banyan = 21,
			Atm = 22,
			InterNetworkV6 = 23,
			Cluster = 24,
			Ieee12844 = 25,
			Irda = 26,
			NetworkDesigners = 28,
			Max = 29
		}

		public enum SocketType
		{
			Unknown = -1,
			Stream = 1,
			Dgram = 2,
			Raw = 3,
			Rdm = 4,
			Seqpacket = 5
		}

		public enum SocketShutdown
		{
			Receive = 0,
			Send = 1,
			Both = 2
		}

		public enum SocketFlags
		{
			None = 0,
			OutOfBand = 1,
			Peek = 2,
			DontRoute = 4,
			MaxIOVectorLength = 16,
			Truncated = 256,
			ControlDataTruncated = 512,
			Broadcast = 1024,
			Multicast = 2048,
			Partial = 32768
		}

		public enum ProtocolType
		{
			Unknown = -1,
			IP = 0,
			IPv6HopByHopOptions = 0,
			Unspecified = 0,
			Icmp = 1,
			Igmp = 2,
			Ggp = 3,
			IPv4 = 4,
			Tcp = 6,
			Pup = 12,
			Udp = 17,
			Idp = 22,
			IPv6 = 41,
			IPv6RoutingHeader = 43,
			IPv6FragmentHeader = 44,
			IPSecEncapsulatingSecurityPayload = 50,
			IPSecAuthenticationHeader = 51,
			IcmpV6 = 58,
			IPv6NoNextHeader = 59,
			IPv6DestinationOptions = 60,
			ND = 77,
			Raw = 255,
			Ipx = 1000,
			Spx = 1256,
			SpxII = 1257
		}

#if UNITY_WEBGL && !UNITY_EDITOR
        public System.Net.Sockets.Socket socket;
        public CherishSocket(AddressFamily addressFamily,SocketType socketType,ProtocolType protocolType)
		{
		}

		public int SendTimeout { get { return 0; } set{} }

		public int ReceiveTimeout { get { return 0; } set{} }

		public bool Blocking { get { return false; } set{} }

		public bool Connected { get { return false; } }

		public void Connect(string host, int port)
		{
		}

		public void Bind(string ip,int port)
		{
		}

		public void Shutdown(SocketShutdown how)
		{			
		}

		public void Close()
		{			
		}

		public int Send(byte[] buffer)
		{
			return 0;
		}

		public int SendTo(byte[] buffer, string ip,int port)
		{
			return 0;
		}

		public int Receive(byte[] buffer, int size,SocketFlags socketFlags)
		{
			return 0;
		}

		public static string DomainIp(string str, Action<string> actionCall, ref AddressFamily addFamily)
		{			
			return "";
		}
#else
        public System.Net.Sockets.Socket socket;
		public CherishSocket(AddressFamily addressFamily,SocketType socketType,ProtocolType protocolType)
		{
			socket = new System.Net.Sockets.Socket((System.Net.Sockets.AddressFamily)addressFamily, (System.Net.Sockets.SocketType)socketType, (System.Net.Sockets.ProtocolType)protocolType);
		}

		public int SendTimeout { get { return socket.SendTimeout; } set { socket.SendTimeout = value; } }

		public int ReceiveTimeout { get { return socket.ReceiveTimeout; } set { socket.ReceiveTimeout = value; } }

		public bool Blocking { get { return socket.Blocking; } set { socket.Blocking = value; } }

		public bool Connected { get { return socket.Connected; } }

		public void Connect(string host, int port)
		{
			socket.Connect(host, port);
		}

		public void Bind(string ip,int port)
		{
			socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port));
		}

		public void Shutdown(SocketShutdown how)
		{
			socket.Shutdown((System.Net.Sockets.SocketShutdown)how);
		}

		public void Close()
		{
			socket.Close();
		}

		public int Send(byte[] buffer)
		{
			return socket.Send(buffer);
		}

		public int SendTo(byte[] buffer, string ip,int port)
		{
			return socket.SendTo(buffer, new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port));
		}

		public int Receive(byte[] buffer, int size,SocketFlags socketFlags)
		{
			return socket.Receive(buffer,size,(System.Net.Sockets.SocketFlags)socketFlags);
		}

		public static string DomainIp(string str, Action<string> actionCall, ref AddressFamily addFamily)
		{
			addFamily = AddressFamily.InterNetwork;
			if (string.IsNullOrEmpty(str))
			{
				return "";
			}

			string _return = "";
			try
			{
				System.Net.IPAddress[] aryIP = System.Net.Dns.GetHostAddresses(str);

				addFamily = (AddressFamily)aryIP[0].AddressFamily;

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
#endif
	}
}
