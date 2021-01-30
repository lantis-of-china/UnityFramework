//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ProtoBuf;

//namespace Server.NetWork
//{
//    public class GameSerialzTool
//    {
//        /// <summary>
//        /// 序列化喜爱新
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="Date"></param>
//        /// <returns></returns>
//        static public byte[] Serializer<T>(T Date)
//        {

//            try
//            {
//                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
//                {
//                    ms.Position = 0;
//                    ProtoBuf.Serializer.SerializeWithLengthPrefix<T>(ms, Date, PrefixStyle.Base128);
//                    return ms.ToArray();
//                }
//            }
//            catch
//            {
//                DebugLoger.LogError("序列化数据异常");
//                return null;
//            }


//        }


//        /// <summary>
//        /// 反序列化消息
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="BufferDate"></param>
//        /// <returns></returns>
//        static public T Desrializer<T>(byte[] BufferDate)
//        {
//            try
//            {
//                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(BufferDate))
//                {
//                    ms.Position = 0;
//                    T InstanceDate = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(ms, PrefixStyle.Base128);

//                    return InstanceDate;
//                }
//            }
//            catch
//            {
//                DebugLoger.LogError("反序列化数据异常");
//                return default(T);
//            }
//        }
//    }

//}
