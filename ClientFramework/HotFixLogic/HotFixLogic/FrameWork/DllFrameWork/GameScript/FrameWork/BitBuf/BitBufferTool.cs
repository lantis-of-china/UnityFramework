using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ProtoBuf
{
    /// <summary>
    /// 解析规则
    /// 按照顺序解析
    /// 第一个Int类型 普通值记录描述数据长度 -1为空
    /// 数组为数组数量 特殊类型 List 记录数量 Dictionary 记录数量
    /// </summary>

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    class ProtoContractAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    class ProtoMemberAttribute : Attribute
    {
        /// <summary>
        /// id 1开始
        /// </summary>
        public int mId;

        public bool mIsArray;


        public ProtoMemberAttribute(int id, bool isArray = false)
        {
            mId = id;

            mIsArray = isArray;
        }
    }


    public class BitBufferTool
    {

        #region 用户接口
        /// <summary>
        /// 用户序列化接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataObject"></param>
        /// <param name="desStream"></param>
        /// <returns></returns>
        public static int BitSerializationUser<T>(T dataObject, out System.IO.MemoryStream desStream)
        {
            desStream = null;
            System.IO.Stream stream = null;
            int state = BitSerialization<T>(dataObject.GetType(), dataObject, out stream);

            if (stream != null)
            {
                byte[] buf = new byte[stream.Length];
                stream.Read(buf, 0, (int)stream.Length);
                desStream = new System.IO.MemoryStream(buf);
            }

            return state;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static T BitDeserialzationUser<T>(byte[] origin)
        {
            Type type = typeof(T);

            T elementObject = (T)type.Assembly.CreateInstance(type.FullName, false);

            BitDeserialzation<T>(origin, elementObject);

            return elementObject;
        }
        #endregion

        #region 序列化

        /// <summary>
        /// 序列化
        /// 0空
        /// 1成功
        /// -1 没有类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataObject"></param>
        /// <param name="desStream"></param>
        /// <returns></returns>
        public static int BitSerialization<T>(Type type, T dataObject, out System.IO.Stream desStream)
        {
            desStream = null;

            //是否是系统类型

            bool findAttribute = false;

            Type dataType = type;

            object[] attributeArray = dataType.GetCustomAttributes(false);

            //找到类序列化特性
            for (int loop = 0; loop < attributeArray.Length; ++loop)
            {
                object attributeObject = attributeArray[loop];

                if (attributeObject is ProtoContractAttribute)
                {
                    findAttribute = true;
                }
            }

            if (!findAttribute)
            {
                //Environment.
                if (!IsPrimitive(dataType) && !IsSystemIsPrimitive(dataObject))
                {
                    //创建流
                    desStream = new System.IO.MemoryStream();

                    if (WriteSpecialType(dataType, dataObject, desStream) == -1)
                    {
                        //错误 非基元类型
                        throw new Exception("Not Use BitBufferClassAttribute");
                    }
                }
                else
                {
                    //基元类型
                    desStream = new System.IO.MemoryStream();

                    //直接写
                    WritePrimitive<T>(null, dataObject, desStream);
                }

                return 0;
            }

            //创建流
            desStream = new System.IO.MemoryStream();

            object[] baseAttributeArray = dataType.BaseType.GetCustomAttributes(false);

            bool baseFind = false;

            for (int loop = 0; loop < baseAttributeArray.Length; ++loop)
            {
                object attributeObject = baseAttributeArray[loop];

                if (attributeObject is ProtoContractAttribute)
                {
                    baseFind = true;
                }
            }

            ///这里是找父类
            if (baseFind)
            {
                GetOtherTypeStream(dataType.BaseType, dataObject, desStream);
            }


            ///这里是找字段
            FieldInfo[] fieldArray = dataType.GetFields();

            int currentIndex = 0;

            for (int loop = 0; loop < fieldArray.Length; ++loop)
            {
                currentIndex++;

                FieldInfo field = fieldArray[loop];
                //非数组
                object[] objectAttributes = field.GetCustomAttributes(false);

                bool isBitBuffer = false;

                ProtoMemberAttribute bitFieldAttribute = null;

                for (int loopAttribute = 0; loopAttribute < objectAttributes.Length; ++loopAttribute)
                {
                    object objectAttribute = objectAttributes[loopAttribute];

                    if (objectAttribute is ProtoMemberAttribute)
                    {
                        bitFieldAttribute = objectAttribute as ProtoMemberAttribute;

                        if (bitFieldAttribute.mId == currentIndex)
                        {
                            isBitBuffer = true;

                            break;
                        }
                    }
                }


                if (isBitBuffer && bitFieldAttribute.mIsArray/*field.FieldType.IsArray*/)
                {
                    if (field.FieldType.IsArray)
                    {
                        //数组
                        object arrayType = field.GetValue(dataObject);

                        Array elementArray = arrayType as Array;

                        /*
                        保存数据长度类型
                        -1为空
                        */
                        System.Int32 arrayLength = elementArray == null ? -1 : elementArray.Length;

                        byte[] bufLenght = BitConverter.GetBytes(arrayLength);
                        /*
                        结束
                        */

                        desStream.Write(bufLenght, 0, bufLenght.Length);

                        for (int loopEle = 0; loopEle < arrayLength; ++loopEle)
                        {
                            object element = elementArray.GetValue(loopEle);

                            GetOtherTypeStream(element.GetType(), element, desStream);
                        }
                    }
                }
                else if (isBitBuffer)
                {
                    Type fieldType = field.FieldType;
                    object fieldObject = field.GetValue(dataObject);

                    if (IsPrimitive(fieldType) && IsSystemIsPrimitive(fieldObject))
                    {
                        ///系统类型
                        WritePrimitive<T>(field, dataObject, desStream);
                    }
                    else
                    {
                        object fieldValue = field.GetValue(dataObject);

                        if (fieldValue == null)
                        {
                            /*
                            保存数据长度类型
                            -1为空
                            */
                            byte[] notValueBuf = BitConverter.GetBytes(-1);

                            desStream.Write(notValueBuf, 0, notValueBuf.Length);
                            /*
                            结束
                            */
                            break;
                        }

                        ///写出类型不为空
                        /*
                        保存数据长度类型
                        -1为空
                        */
                        byte[] hasValueBuf = BitConverter.GetBytes(1);

                        desStream.Write(hasValueBuf, 0, hasValueBuf.Length);
                        /*
                        结束
                        */

                        ///玩家自定义类型
                        GetOtherTypeStream(fieldType, fieldValue, desStream);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// 特殊类型处理
        /// 0空
        /// 1成功
        /// -1 没有类型
        /// </summary>
        /// <returns></returns>
        private static int WriteSpecialType<T>(Type type, T dataObject, System.IO.Stream desStream)
        {
            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();

                if (genericType.FullName == "System.Collections.Generic.List`1")
                {
                    IList listObject = dataObject as System.Collections.IList;

                    if (listObject == null)
                    {
                        /*
                        保存数据长度类型
                        -1为空
                        */
                        byte[] notValue = BitConverter.GetBytes(-1);

                        desStream.Write(notValue, 0, 4);
                        /*
                        结束
                        */

                        return 0;
                    }

                    /*
                    保存数据长度类型
                    -1为空
                    */
                    int listCount = listObject.Count;

                    byte[] bufLenght = BitConverter.GetBytes(listCount);
                    /*
                    结束
                    */


                    ///写入长度
                    desStream.Write(bufLenght, 0, 4);

                    for (int loop = 0; loop < listObject.Count; ++loop)
                    {
                        object objValue = listObject[loop];

                        GetOtherTypeStream(objValue.GetType(), objValue, desStream);
                    }

                    return 1;
                }
                else if (genericType.FullName == "System.Collections.Generic.Dictionary`2")
                {
                    IDictionary dicObject = dataObject as System.Collections.IDictionary;

                    if (dicObject == null)
                    {
                        /*
                        保存数据长度类型
                        -1为空
                        */
                        byte[] notValue = BitConverter.GetBytes(-1);

                        desStream.Write(notValue, 0, 4);
                        /*
                        结束
                        */

                        return 0;
                    }

                    /*
                    保存数据长度类型
                    -1为空
                    */
                    int listCount = dicObject.Count;

                    byte[] bufLenght = BitConverter.GetBytes(listCount);
                    /*
                    结束
                    */
                    ///写入长度
                    desStream.Write(bufLenght, 0, 4);

                    ICollection keyArray = dicObject.Keys;

                    foreach (var key in keyArray)
                    {
                        object value = dicObject[key];

                        GetOtherTypeStream(key.GetType(), key, desStream);

                        GetOtherTypeStream(value.GetType(), value, desStream);
                    }

                    return 1;
                }
            }
            else
            {

            }

            return -1;
        }

        /// <summary>
        /// 获取其他类型的stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="dataObject"></param>
        /// <param name="desStream"></param>
        private static void GetOtherTypeStream<T>(Type dataType, T dataObject, System.IO.Stream desStream)
        {
            System.IO.Stream streamData;

            BitSerialization(dataType, dataObject, out streamData);

            if (streamData != null)
            {
                //写入
                streamData.Position = 0;

                byte[] buf = new byte[streamData.Length];

                streamData.Read(buf, 0, buf.Length);

                desStream.Write(buf, 0, buf.Length);

                buf = null;
            }
        }

        /// <summary>
        /// 通过属性字段直接写出基元类型
        /// </summary>
        private static void WritePrimitive<T>(FieldInfo field, T dataObject, System.IO.Stream desStream)
        {
            object dataType = field == null ? dataObject : field.GetValue(dataObject);

            if (dataType is System.String)
            {
                System.String fieldValue = dataType as System.String;

                System.Char[] charArray = charArray = fieldValue.ToCharArray();

                System.Int32 charLength = fieldValue == null ? -1 : charArray.Length;

                System.Int32 length = 0;

                if (charLength >= 0)
                {
                    length = charLength;// * 2;
                }

                byte[] bufLenght = BitConverter.GetBytes(length);

                desStream.Write(bufLenght, 0, bufLenght.Length);

                for (int loopChar = 0; loopChar < charLength; ++loopChar)
                {
                    System.Char charValue = charArray[loopChar];

                    System.Byte[] bufArray = BitConverter.GetBytes(charValue);

                    desStream.Write(bufArray, 0, bufArray.Length - 1);
                }
            }
            else if (dataType is System.Char)
            {
                System.Char fieldValue = (System.Char)dataType;

                System.Int32 length = 1;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Byte)
            {
                System.Byte fieldValue = (System.Byte)dataType;

                System.Int32 length = 1;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);

                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.SByte)
            {
                System.SByte fieldValue = (System.SByte)dataType;

                System.Int32 length = 1;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Boolean)
            {
                System.Boolean fieldValue = (System.Boolean)dataType;

                System.Int32 length = 1;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, bufArray.Length);
            }
            else if (dataType is System.Int16)
            {
                System.Int16 fieldValue = (System.Int16)dataType;

                System.Int32 length = 2;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);


                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.UInt16)
            {
                System.UInt16 fieldValue = (System.UInt16)dataType;

                System.Int32 length = 2;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Int32)
            {
                System.Int32 fieldValue = (System.Int32)dataType;

                System.Int32 length = 4;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.UInt32)
            {
                System.UInt32 fieldValue = (System.UInt32)dataType;

                System.Int32 length = 4;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Int64)
            {
                System.Int64 fieldValue = (System.Int64)dataType;

                System.Int32 length = 8;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.UInt64)
            {
                System.UInt64 fieldValue = (System.UInt64)dataType;

                System.Int32 length = 8;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Double)
            {
                System.Double fieldValue = (System.Double)dataType;

                System.Int32 length = 8;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);


                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Single)
            {
                System.Single fieldValue = (System.Single)dataType;

                System.Int32 length = 4;
                System.Byte[] lengthArray = BitConverter.GetBytes(length);
                desStream.Write(lengthArray, 0, lengthArray.Length);

                System.Byte[] bufArray = BitConverter.GetBytes(fieldValue);

                desStream.Write(bufArray, 0, length);
            }
            else if (dataType is System.Object)
            {
                throw new Exception(" System.Object Error");
            }
        }
        #endregion 序列化

        #region 反序列化

        /// <summary>
        /// 用户调用反序列化接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static void BitDeserialzation<T>(byte[] origin, T outDeserialObject)
        {
            int position = 0;

            outDeserialObject = (T)BitDeserialization(ref position, origin, outDeserialObject.GetType(), outDeserialObject);
        }

        private static object BitDeserialization(ref int position, byte[] origin, Type dataType, object desObject)
        {
            object[] attributeArray = dataType.GetCustomAttributes(false);

            bool findAttribute = false;

            //找到类序列化特性
            for (int loop = 0; loop < attributeArray.Length; ++loop)
            {
                object attributeObject = attributeArray[loop];

                if (attributeObject is ProtoContractAttribute)
                {
                    findAttribute = true;
                }
            }

            if (!findAttribute)
            {
                if (!IsPrimitive(dataType) && !IsSystemIsPrimitive(desObject))
                {
                    if (ReadSpecialType(ref position, origin, dataType, desObject) == -1)
                    {
                        //非基元类型  
                        //特殊类型处理
                        throw new Exception("Not find attribute type full name " + dataType.FullName + " obj type " + desObject.GetType().FullName);
                    }
                }
                else
                {
                    //基元类型
                    ReadPrimitive(ref position, origin, dataType, ref desObject);

                    return desObject;
                    //field.SetValue(fieldObject, desObject);
                }

            }

            object[] baseAttributeArray = dataType.BaseType.GetCustomAttributes(false);

            bool baseFind = false;

            for (int loop = 0; loop < baseAttributeArray.Length; ++loop)
            {
                object attributeObject = baseAttributeArray[loop];

                if (attributeObject is ProtoContractAttribute)
                {
                    baseFind = true;
                }
            }

            if (baseFind)
            {
                ///找到基类
                if (desObject != null)
                {
                    GetOtherType(ref position, origin, dataType.BaseType, ref desObject);
                }
            }



            ///这里是找字段
            FieldInfo[] fieldArray = dataType.GetFields();

            int currentIndex = 0;

            for (int loop = 0; loop < fieldArray.Length; ++loop)
            {
                currentIndex++;

                FieldInfo field = fieldArray[loop];
                //非数组
                object[] objectAttributes = field.GetCustomAttributes(false);

                bool isBitBuffer = false;

                ProtoMemberAttribute bitFieldAttribute = null;

                for (int loopAttribute = 0; loopAttribute < objectAttributes.Length; ++loopAttribute)
                {
                    object objectAttribute = objectAttributes[loopAttribute];

                    if (objectAttribute is ProtoMemberAttribute)
                    {
                        bitFieldAttribute = objectAttribute as ProtoMemberAttribute;

                        if (bitFieldAttribute.mId == currentIndex)
                        {
                            isBitBuffer = true;

                            break;
                        }
                    }
                }


                if (isBitBuffer && bitFieldAttribute.mIsArray/*field.FieldType.IsArray*/)
                {
                    if (field.FieldType.IsArray)
                    {
                        System.Int32 arrayLength = BitConverter.ToInt32(origin, position);

                        position = position + 4;

                        if (arrayLength > 0)
                        {
                            //有数组 
                            Type elementType = field.FieldType.GetElementType();

                            Array bitTypeArray = Array.CreateInstance(elementType, arrayLength);

                            for (int loopArray = 0; loopArray < arrayLength; ++loopArray)
                            {
                                object elementObject = null;

                                System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(elementType);

                                elementObject = tc.ConvertFromString("0");

                                GetOtherType(ref position, origin, elementType, ref elementObject);

                                bitTypeArray.SetValue(elementObject, loopArray);
                            }

                            field.SetValue(desObject, bitTypeArray);
                        }
                        else
                        {
                            //无数组 NULL
                        }
                    }
                }
                else if (isBitBuffer)
                {
                    Type fieldType = field.FieldType;

                    object fieldObject = field.GetValue(desObject);

                    if (IsPrimitive(fieldType) && IsSystemIsPrimitive(fieldObject))
                    {
                        //系统类型
                        ReadPrimitive(ref position, origin, fieldType, ref fieldObject);

                        field.SetValue(desObject, fieldObject);
                    }
                    else
                    {
                        System.Int32 length = BitConverter.ToInt32(origin, position);

                        position = position + 4;

                        if (length >= 0)
                        {
                            if (fieldObject != null)
                            {
                                GetOtherType(ref position, origin, fieldType, ref fieldObject);
                            }
                        }
                    }
                }
            }

            return desObject;
        }

        ///其他类型处理
        private static void GetOtherType(ref int position, byte[] origin, Type dataType, ref object dataObject)
        {
            if (dataObject == null)
            {
                dataObject = null;
            }
            dataObject = BitDeserialization(ref position, origin, dataType, dataObject);
        }

        ///特殊类型处理
        private static int ReadSpecialType(ref int position, byte[] origin, Type dataType, object dataObject)
        {
            if (dataType.IsGenericType)
            {
                Type genericType = dataType.GetGenericTypeDefinition();

                if (genericType.FullName == "System.Collections.Generic.List`1")
                {
                    System.Int32 length = BitConverter.ToInt32(origin, position);

                    position = position + 4;

                    if (length < 0)
                    {
                        return 0;
                    }

                    Type[] genericTypeArry = dataType.GetGenericArguments();

                    Type genType = genericTypeArry[0];

                    object listObject = dataType.Assembly.CreateInstance(dataType.FullName, false);

                    IList listInterFace = listObject as IList;

                    for (int loop = 0; loop < length; ++loop)
                    {

                        System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(genType);

                        //elementObject = tc.ConvertFromString("0");
                        object itemObject = tc.ConvertFromString("0"); //genType.Assembly.CreateInstance(genType.FullName, false);

                        GetOtherType(ref position, origin, genType, ref itemObject);

                        listInterFace.Add(itemObject);
                    }

                    return 1;
                }
                else if (genericType.FullName == "System.Collections.Generic.Dictionary`2")
                {
                    System.Int32 length = BitConverter.ToInt32(origin, position);

                    position = position + 4;

                    if (length < 0)
                    {
                        return 0;
                    }

                    Type[] genericTypeArry = dataType.GetGenericArguments();

                    Type genType_1 = genericTypeArry[0];

                    Type genType_2 = genericTypeArry[1];


                    object listObject = dataType.Assembly.CreateInstance(dataType.FullName, false);

                    IDictionary dicObject = listObject as IDictionary;

                    for (int loop = 0; loop < length; ++loop)
                    {
                        System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(genType_1);
                        object itemObject_key = tc.ConvertFromString("0"); //genType_1.Assembly.CreateInstance(genType_1.FullName, false);

                        tc = System.ComponentModel.TypeDescriptor.GetConverter(genType_2);
                        object itemObject_value = tc.ConvertFromString("0"); //genType_2.Assembly.CreateInstance(genType_2.FullName, false);

                        GetOtherType(ref position, origin, genType_1, ref itemObject_key);

                        GetOtherType(ref position, origin, genType_2, ref itemObject_value);

                        dicObject.Add(itemObject_key, itemObject_value);
                    }

                    return 1;
                }
            }
            else
            {

            }

            return -1;
        }

        /// <summary>
        /// 读入字节到实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <param name="desObject"></param>
        private static void ReadPrimitive(ref int position, byte[] origin, Type dataType, ref object desObject)
        {
            if (dataType == typeof(System.String))
            {
                System.String valueObject = null;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    System.Byte[] byteArray = new System.Byte[length];

                    for (int loopStrByte = 0; loopStrByte < length; ++loopStrByte)
                    {
                        byteArray[loopStrByte] = origin[position];

                        position = position + 1;
                    }

                    valueObject = System.Text.Encoding.UTF8.GetString(byteArray);
                    //valueObject = BitConverter.ToString(byteArray);
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Char))
            {
                System.Char valueObject = ' ';

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToChar(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Byte))
            {
                System.Byte valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;


                if (length >= 0)
                {
                    valueObject = origin[position];

                    position = position + length;
                }

                desObject = valueObject;

            }
            else if (dataType == typeof(System.SByte))
            {
                System.SByte valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    if (origin[position] > 127)
                    {
                        valueObject = (System.SByte)(origin[position] - 256);
                    }
                    else
                    {
                        valueObject = (System.SByte)(origin[position]);
                    }

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Boolean))
            {
                System.Boolean valueObject = false;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToBoolean(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Int16))
            {
                System.Int16 valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToInt16(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.UInt16))
            {
                System.UInt16 valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToUInt16(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Int32))
            {
                System.Int32 valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToInt32(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.UInt32))
            {
                System.UInt32 valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToUInt32(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Int64))
            {
                System.Int64 valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToInt64(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.UInt64))
            {
                System.UInt64 valueObject = 0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToUInt64(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Double))
            {
                System.Double valueObject = 0.0;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToDouble(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Single))
            {
                System.Single valueObject = 0.0f;

                System.Int32 length = BitConverter.ToInt32(origin, position);

                position = position + 4;

                if (length >= 0)
                {
                    valueObject = BitConverter.ToSingle(origin, position);

                    position = position + length;
                }

                desObject = valueObject;
            }
            else if (dataType == typeof(System.Object))
            {
                throw new Exception(" System.Object Error " + dataType.FullName);
            }
        }
        #endregion 反序列化


        /// <summary>
        /// 判断是否基元类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive;
        }

        /// <summary>
        /// 判断是否系统类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsSystemIsPrimitive(object type)
        {
            if (type.GetType().IsPrimitive)
            {
                return true;
            }
            else if (type is System.String)
            {
                return true;
            }
            else if (type is System.DateTime)
            {
                return true;
            }
            else if (type is System.Decimal)
            {
                return true;
            }

            return false;
        }
    }
}