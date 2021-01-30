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
                MemoryStream stmOutTemp = new MemoryStream();
                ZOutputStream outZStream = new ZOutputStream(stmOutTemp, zlibConst.Z_DEFAULT_COMPRESSION);
                outZStream.Write(sourceByte, 0, sourceByte.Length);
                outZStream.finish();
                sourceByte = stmOutTemp.ToArray();

                return sourceByte;
            }
            catch(Exception e)
            {
                Debug.LogError("异常 " + e.ToString());
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
            MemoryStream stmOutput = new MemoryStream();
            ZOutputStream outZStream = new ZOutputStream(stmOutput);
            outZStream.Write(sourceByte, 0, sourceByte.Length);
            outZStream.finish();
            sourceByte = stmOutput.ToArray();
            return sourceByte;
        }
        catch(Exception e)
        {
            Debug.LogError("异常 " + e.ToString());

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

            sourceByte = CompressEncryptionCore.EncryptionEx(sourceByte);
            sourceByte = CompressEncryptionData(sourceByte);
           
            List<byte> resultBytes = new List<byte>(tagBytes);
            resultBytes.AddRange(sourceByte);
            return resultBytes.ToArray();
        }
        catch
        {
            return sourceByte;
        }
    }

    public static byte[] UnEncryption(byte[] encryByte)
    {
        if (encryByte == null || encryByte.Length == 0)
        {
            Debug.LogError("解密字节流不能为空!");
            return encryByte;
        }

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
            sourceBuf = CompressEncryptionCore.UnEncryptionEx(sourceBuf);

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
