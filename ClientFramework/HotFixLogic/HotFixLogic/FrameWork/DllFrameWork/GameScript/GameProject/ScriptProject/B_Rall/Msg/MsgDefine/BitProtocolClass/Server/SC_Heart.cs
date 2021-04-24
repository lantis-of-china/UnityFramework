// 此文件由协议导出插件自动生成
// ID : 00008]

//****服务器心跳接收****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using IMClub;


using SingleMoba;
using Template;



namespace Server
{
	/// <summary>
	///服务器心跳接收
	/// <\summary>
	public class SC_Heart : CherishBitProtocolBase
	{
		/// <summary>
		///
		/// <\summary>
		public Int64 unitxTime;
		/// <summary>
		///
		/// <\summary>
		public Int64 ticks;
		public SC_Heart() { }

		public SC_Heart(Int64 _unitxTime, Int64 _ticks)
		{
			this.unitxTime = _unitxTime;
			this.ticks = _ticks;
		}
		private Byte[] get_unitxTime_encoding()
		{
			Byte[] outBuf = null;
			outBuf = BitConverter.GetBytes((Int64)unitxTime);
			return outBuf;
		}


		private Byte[] get_ticks_encoding()
		{
			Byte[] outBuf = null;
			outBuf = BitConverter.GetBytes((Int64)ticks);
			return outBuf;
		}

		private int set_unitxTime_fromBuf(Byte[] sourceBuf, int curIndex)
		{
			Byte tag = sourceBuf[curIndex];
			curIndex += 1;
			if (tag != 0)
			{
				;
				unitxTime = new Int64();
				unitxTime = BitConverter.ToInt64(sourceBuf, curIndex);
				curIndex += 8;
			}
			return curIndex;
		}
		private int set_ticks_fromBuf(Byte[] sourceBuf, int curIndex)
		{
			Byte tag = sourceBuf[curIndex];
			curIndex += 1;
			if (tag != 0)
			{
				;
				ticks = new Int64();
				ticks = BitConverter.ToInt64(sourceBuf, curIndex);
				curIndex += 8;
			}
			return curIndex;
		}
		public override Byte[] Serializer()
		{
			MemoryStream memoryWrite = new MemoryStream();
			Byte[] byteBuf = null;
			if (unitxTime != null)
			{
				memoryWrite.WriteByte(1);
				byteBuf = get_unitxTime_encoding();
				memoryWrite.Write(byteBuf, 0, byteBuf.Length);
			}
			else
			{
				memoryWrite.WriteByte(0);
			}
			if (ticks != null)
			{
				memoryWrite.WriteByte(1);
				byteBuf = get_ticks_encoding();
				memoryWrite.Write(byteBuf, 0, byteBuf.Length);
			}
			else
			{
				memoryWrite.WriteByte(0);
			}
			Byte[] bufResult = memoryWrite.ToArray(); memoryWrite.Dispose();
			return bufResult;
		}

		public override int Deserializer(Byte[] sourceBuf, int startOffset)
		{
			startOffset = set_unitxTime_fromBuf(sourceBuf, startOffset);
			startOffset = set_ticks_fromBuf(sourceBuf, startOffset);
			return startOffset;
		}

		public String get_unitxTime_json()
		{
			if (unitxTime == null) { return ""; }
			String resultJson = "\"unitxTime\":"; resultJson += "\""; resultJson += unitxTime.ToString(); resultJson += "\""; return resultJson;
		}


		public String get_ticks_json()
		{
			if (ticks == null) { return ""; }
			String resultJson = "\"ticks\":"; resultJson += "\""; resultJson += ticks.ToString(); resultJson += "\""; return resultJson;
		}


		public void set_unitxTime_fromJson(LitJson.JsonData jsonObj)
		{
			unitxTime = Int64.Parse(jsonObj.ToString());
		}


		public void set_ticks_fromJson(LitJson.JsonData jsonObj)
		{
			ticks = Int64.Parse(jsonObj.ToString());
		}

		public override String SerializerJson()
		{
			String resultStr = "{"; if (unitxTime != null)
			{
				resultStr += get_unitxTime_json();
			}
			else { }
			if (ticks != null)
			{
				resultStr += ","; resultStr += get_ticks_json();
			}
			else { }
			resultStr += "}"; return resultStr;
		}

		public override void DeserializerJson(String json)
		{
			LitJson.JsonData jsonObj = CSTools.JsonToData(json);
			if (jsonObj["unitxTime"] != null)
			{
				set_unitxTime_fromJson(jsonObj["unitxTime"]);
			}
			if (jsonObj["ticks"] != null)
			{
				set_ticks_fromJson(jsonObj["ticks"]);
			}
		}
	}
}
