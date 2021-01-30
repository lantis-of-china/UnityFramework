using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using ComponentAce.Compression.Libs.zlib;
using System.Runtime.InteropServices;
//using Ionic.Zlib;

public unsafe class CompressEncryption
{
    private static int offsetCount = 3;
    internal class RecordEncryption
    {
        public byte sameCount;
        public byte value;
    }

    /// <summary>
    /// 压缩加密
    /// </summary>
    /// <param name="sourceByte"></param>
    /// <returns></returns>
    public unsafe static byte[] CompressEncryptionData(byte[] sourceByte)
    {
        try
        {
            Debug.Log("数据压缩前长度 " + sourceByte.Length);
            MemoryStream stmOutTemp = new MemoryStream();
            ZOutputStream outZStream = new ZOutputStream(stmOutTemp, zlibConst.Z_DEFAULT_COMPRESSION);
            outZStream.Write(sourceByte, 0, sourceByte.Length);
            outZStream.finish();
            sourceByte = stmOutTemp.ToArray();
            Debug.Log("数据压缩后长度 " + sourceByte.Length);

            //IntPtr p = Marshal.AllocHGlobal(sourceByte.Length);
            //Marshal.Copy(sourceByte, 0, p, sourceByte.Length);
            //IntPtr backP = CompressEncryption_Encryption(p, sourceByte.Length);
            //Marshal.FreeHGlobal(p);

            //int newLength = sourceByte.Length + 10;
            //byte[] newBuf = new byte[newLength];
            //Marshal.Copy (backP, newBuf, 0, newLength);
            //Marshal.FreeHGlobal(backP);

            return sourceByte;
        }
        catch (Exception e)
        {
            Debug.LogError("压缩数据出现异常 " + e.ToString());
            return null;
        }
    }

    /// <summary>
    /// 解压缩解密
    /// </summary>
    /// <param name="sourceByte"></param>
    /// <returns></returns>
    public static byte[] UnCompressDecompressData(byte[] sourceByte)
    {
        try
        {
            //sourceByte = CompressEncryption_UnEncryption(sourceByte, sourceByte.Length);

            Debug.Log("解压前长度 " + sourceByte.Length);
            MemoryStream stmOutput = new MemoryStream();
            ZOutputStream outZStream = new ZOutputStream(stmOutput);
            outZStream.Write(sourceByte, 0, sourceByte.Length);
            outZStream.finish();
            sourceByte = stmOutput.ToArray();
            Debug.Log("解压后长度 " + sourceByte.Length);
            return sourceByte;
        }
        catch (Exception e)
        {
            Debug.LogError("解压数据出现异常 " + e.ToString());

            return null;
        }
    }


    public static string encryptionTag = "LantisEncryptionTag";
    public static byte[] Encryption(byte[] sourceByte)
    {
        if (sourceByte == null || sourceByte.Length == 0)
        {
            return sourceByte;
        }

        try
        {
            byte[] tagBytes = System.Text.Encoding.UTF8.GetBytes(encryptionTag);
            if (tagBytes.Length <= sourceByte.Length)
            {
                bool isSame = true;
                for (var i = 0; i < tagBytes.Length; ++i)
                {
                    if (sourceByte[i] != tagBytes[i])
                    {
                        isSame = false;
                        break;
                    }
                }
                if (isSame)
                {
                    Debug.LogError("字节流已经加密无法再次加密!");
                    return sourceByte;
                }
            }

            for (Int64 offestIndex = 0; offestIndex < sourceByte.Length - offsetCount; ++offestIndex)
            {
                byte curByte = sourceByte[offestIndex];

                sourceByte[offestIndex] = sourceByte[offestIndex + offsetCount];

                sourceByte[offestIndex + offsetCount] = curByte;
            }

            sourceByte = CompressEncryptionData(sourceByte);

            List<byte> resultBytes = new List<byte>(tagBytes);
            resultBytes.AddRange(sourceByte);
            return resultBytes.ToArray();
        }
        catch
        {
            return sourceByte;
        }
        //List<RecordEncryption> recordEncryptionList = new List<RecordEncryption>();

        //byte recordCount = 0;
        //byte recordByte = 0;

        //Int64 lengthSub1 = sourceByte.Length - 1;

        //for (Int64 index = 0; index < sourceByte.Length; ++index)
        //{
        //    byte curB = sourceByte[index];

        //    if (index != 0 && recordByte == curB && recordCount < 255 && index < lengthSub1)
        //    {
        //        recordCount++;
        //    }
        //    else
        //    {
        //        if (index != 0)
        //        {
        //            RecordEncryption re = new RecordEncryption();

        //            re.sameCount = recordCount;

        //            re.value = recordByte;

        //            recordEncryptionList.Add(re);
        //        }

        //        recordCount = 1;

        //        recordByte = curB;

        //        if (index == lengthSub1)
        //        {
        //            RecordEncryption re = new RecordEncryption();

        //            re.sameCount = recordCount;

        //            re.value = recordByte;

        //            recordEncryptionList.Add(re);
        //        }
        //    }
        //}

        //byte[] encryptionBuf = new byte[recordEncryptionList.Count * 2];

        //for (int index = 0; index < recordEncryptionList.Count; ++index)
        //{
        //    RecordEncryption re = recordEncryptionList[index];
        //    Int64 indexBuf = index * 2;
        //    encryptionBuf[indexBuf] = re.sameCount;
        //    encryptionBuf[indexBuf + 1] = re.value;
        //}

        //recordEncryptionList.Clear();

        //recordEncryptionList = null;

        //return encryptionBuf;
    }

    public static byte[] UnEncryption(byte[] encryByte)
    {
        if (encryByte == null || encryByte.Length == 0)
        {
            Debug.LogError("解密字节流不能为空!");
            return encryByte;
        }
        //List<RecordEncryption> recordEncryptionList = new List<RecordEncryption>();
        //Int64 recordCount = 0;

        //for (int index = 0; index < encryByte.Length / 2; ++index)
        //{
        //    RecordEncryption re = new RecordEncryption();

        //    int indexBuf = index * 2;

        //    re.sameCount = encryByte[indexBuf];

        //    re.value = encryByte[indexBuf + 1];

        //    recordEncryptionList.Add(re);

        //    recordCount += re.sameCount;
        //}


        //byte[] sourceBuf = new byte[recordCount];
        //Int64 bufCount = 0;

        //for (int index = 0; index < recordEncryptionList.Count; ++index)
        //{
        //    RecordEncryption re = recordEncryptionList[index];

        //    for (int indexSame = 0; indexSame < re.sameCount; ++indexSame)
        //    {
        //        sourceBuf[bufCount] = re.value;

        //        bufCount++;
        //    }
        //}


        try
        {
            byte[] tagBytes = System.Text.Encoding.UTF8.GetBytes(encryptionTag);
            if (encryByte.Length <= tagBytes.Length)
            {
                Debug.LogError("解密字节流长度错误");
                return encryByte;
            }
            for (var i = 0; i < tagBytes.Length; ++i)
            {
                if (tagBytes[i] != encryByte[i])
                {
                    Debug.LogError("解密字节流无法识别");
                    return encryByte;
                }
            }
            List<byte> rangList = new List<byte>(encryByte);
            rangList.RemoveRange(0, tagBytes.Length);
            encryByte = rangList.ToArray();

            byte[] sourceBuf = UnCompressDecompressData(encryByte);

            for (Int64 offestIndex = sourceBuf.Length - 1; offestIndex >= offsetCount; --offestIndex)
            {
                byte curByte = sourceBuf[offestIndex];

                sourceBuf[offestIndex] = sourceBuf[offestIndex - 3];

                sourceBuf[offestIndex - 3] = curByte;
            }

            return sourceBuf;
        }
        catch
        {
            return encryByte;
        }
    }












    public static byte[] EncryptionMsg(byte[] sourceByte)
    {
        if (sourceByte == null || sourceByte.Length == 0)
        {
            return sourceByte;
        }

        try
        {
            for (Int64 offestIndex = 0; offestIndex < sourceByte.Length - offsetCount; ++offestIndex)
            {
                byte curByte = sourceByte[offestIndex];

                sourceByte[offestIndex] = sourceByte[offestIndex + offsetCount];

                sourceByte[offestIndex + offsetCount] = curByte;
            }

            return sourceByte;
        }
        catch
        {
            return sourceByte;
        }
    }

    public static byte[] UnEncryptionMsg(byte[] encryByte)
    {
        if (encryByte == null || encryByte.Length == 0)
        {
            return encryByte;
        }

        try
        {
            byte[] sourceBuf = encryByte;

            for (Int64 offestIndex = sourceBuf.Length - 1; offestIndex >= offsetCount; --offestIndex)
            {
                byte curByte = sourceBuf[offestIndex];

                sourceBuf[offestIndex] = sourceBuf[offestIndex - 3];

                sourceBuf[offestIndex - 3] = curByte;
            }

            return sourceBuf;
        }
        catch
        {
            return encryByte;
        }
    }
}
