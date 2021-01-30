using CherishWebGLSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server.Process
{

    public class MessageParkInfor
    {
        public byte[] sourceData;
        public CherishSocket clientSocket;
        public string ip;
        public int port;
    }


    public class MessageDriver
    {
        public static bool runUpSend;
        public static Queue<MessageParkInfor> messageSendList = new Queue<MessageParkInfor>();

        public static void SendException(string str)
        {
            DebugLoger.Log("sendMsg " + str);
        }

        public static void AddSendMessage(CherishSocket socket,byte[] sourceDate)
        {

			Monitor.Enter(((ICollection)messageSendList).SyncRoot);            
            messageSendList.Enqueue(new MessageParkInfor() { clientSocket = socket, sourceData = sourceDate });
			Monitor.Exit(((ICollection)messageSendList).SyncRoot);

		}

        public static void UpSendMessage()
        {
            runUpSend = true;            
            new CherishWebGLSupport.CherishThread(RunSend).Start();            
        }

        [AOT.MonoPInvokeCallback(typeof(System.Threading.ThreadStart))]
        public static void RunSend()
        {
            while (runUpSend)
            {
                CherishWebGLSupport.CherishThread.Sleep(5);
                try
                {
                    while (messageSendList.Count > 0)
                    {
                        MessageParkInfor messageInfor = messageSendList.Dequeue();
                        int msgId = BitConverter.ToInt32(messageInfor.sourceData, 4);
                        if (messageInfor.clientSocket.Connected)
                        {
                            Monitor.Enter(messageInfor.clientSocket);
                            //{
                            int remSendCount = 0;
                            byte[] byteBuf = new byte[2048];
                            while (remSendCount < messageInfor.sourceData.Length)
                            {
                                if ((messageInfor.sourceData.Length - remSendCount) > 2048)
                                {
                                    Array.Copy(messageInfor.sourceData, remSendCount, byteBuf, 0, byteBuf.Length);
                                    remSendCount += 2048;
                                    messageInfor.clientSocket.Send(byteBuf);
                                }
                                else
                                {
                                    byte[] sendRemBuf = new byte[messageInfor.sourceData.Length - remSendCount];
                                    Array.Copy(messageInfor.sourceData, remSendCount, sendRemBuf, 0, sendRemBuf.Length);
                                    messageInfor.clientSocket.Send(sendRemBuf);
                                    remSendCount = messageInfor.sourceData.Length;
                                }
                            }
                            //}
                            Monitor.Exit(messageInfor.clientSocket);

                        }
                        else
                        {
                            DebugLoger.LogError("客户端已经断开连接 消息发送失败 " + msgId);
                        }
                    }
                }
                catch (Exception e)
                {
                    DebugLoger.LogError(e.ToString());
                }
            }
        }

        public static void CloseRunUpSend()
        {
            runUpSend = false;
        }


        public static Queue<MessageParkInfor> messageParkInforList = new Queue<MessageParkInfor>();

        public static void AddMessage(byte[] SourceDate, CherishSocket ClientSocket, string ip, int port)
        {
            MessageParkInfor messageInfor = new MessageParkInfor();
            messageInfor.sourceData = SourceDate;
            messageInfor.clientSocket = ClientSocket;
            messageInfor.ip = ip;
            messageInfor.port = port;
			Monitor.Enter(((ICollection)messageParkInforList).SyncRoot);
            //{
                messageParkInforList.Enqueue(messageInfor);
			//}
			Monitor.Exit(((ICollection)messageParkInforList).SyncRoot);

		}
        



        public static void UpMessage()
        {
            //Server.Process.MessageDriver.UpSendMessage();

            try
            {
                while (messageParkInforList.Count > 0)
                {
                    MessageParkInfor messageInfor = messageParkInforList.Dequeue();

                    MessageDrivice(messageInfor.sourceData, messageInfor.clientSocket, messageInfor.ip, messageInfor.port);
                }
            }
            catch(Exception e)
            {
                DebugLoger.LogError(e.ToString());
            }
        }



        public static void MessageDrivice(byte[] SourceDate, CherishSocket ClientSocket, string ip, int port)
        {
			try
			{
				int TypeSize = 4;
				byte[] TypeBuf = new byte[TypeSize];
				int MsgType = 0;
				byte[] MsgDate = new byte[SourceDate.Length - TypeSize];

				System.Array.Copy(SourceDate, TypeBuf, TypeSize);
				MsgType = System.BitConverter.ToInt32(TypeBuf, 0);

                DebugLoger.Log("收到消息:" + MsgType);

				System.Array.ConstrainedCopy(SourceDate, TypeSize, MsgDate, 0, MsgDate.Length);
				
				if (MessageProcess.Instance.ProcessMap.ContainsKey(MsgType))
				{
					if (MessageProcess.Instance.ProcessMap[MsgType].ID == MsgType)
					{
						UINameSpace.UIWaitting.RemoveShowWaitting(MsgType.ToString());

						if (ClientSocket != null)
						{
							MessageProcess.Instance.ProcessMap[MsgType].Process(ClientSocket.socket, ip, port, MsgDate);
						}
						else
						{
							MessageProcess.Instance.ProcessMap[MsgType].Process(null, ip, port, MsgDate);
						}
					}
				}
			}
			catch (Exception e)
			{
				DebugLoger.LogError(e.ToString(),e);
			}
        }
    }
}
