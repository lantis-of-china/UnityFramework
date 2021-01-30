using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CSTools
{
	/// <summary>
	/// 获取MD5
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
    public static string GetMD5(string value, System.Text.Encoding encoding)
	{
		System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] fromData = encoding.GetBytes(value);
		byte[] targetData = md5.ComputeHash(fromData);
		//string byte2String = null;

		//for (int i = 0; i < targetData.Length; i++)
		//{
		//	byte2String += targetData[i].ToString("x");
		//}
		string byte2String = System.BitConverter.ToString(targetData);
		byte2String = byte2String.Replace("-", "").ToLower();
		return byte2String;
	}


	static public byte[] FloatArrayToByteArray(float[] sampleArray)
    {
        byte[] bytes = new byte[sampleArray.Length * 4];
        Buffer.BlockCopy(sampleArray, 0, bytes, 0, bytes.Length);

        return bytes;
    }

    static public float[] ByteArrayToFloatArray(byte[] byteArray)
    {
        float[] data = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, data, 0, byteArray.Length);

        return data;
    }

    static public float[] SetFloatVosice(float[] datas,float vosice)
    {
        for (var i = 0; i < datas.Length; ++i)
        {
            datas[i] *= vosice;
        }

        return datas;
    }


    static public LitJson.JsonData JsonToData(string json)
    {
        LitJson.JsonData jsdata = LitJson.JsonMapper.ToObject(json);
        return jsdata;
    }

    static public T DeserializeObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    static public string SerializeObject(object jsonData)
    {
        return JsonConvert.SerializeObject(jsonData);
    }

    static public void RegistDebugLogEvent(Application.LogCallback eventCall)
	{
		Application.logMessageReceived += eventCall;
	}

    static public string GetLastNameSpaceName(string spaceName)
    {
        if (string.IsNullOrEmpty(spaceName))
        {
            return spaceName;
        }

        if (spaceName.Contains('.'))
        {
            var spaceNames = spaceName.Split('.');
            return spaceNames[spaceNames.Length - 1];
        }

        return spaceName;
    }

    static public float TicksToSencend(long ticks)
    {
        return ticks / 10000000.0f;
    }
}
