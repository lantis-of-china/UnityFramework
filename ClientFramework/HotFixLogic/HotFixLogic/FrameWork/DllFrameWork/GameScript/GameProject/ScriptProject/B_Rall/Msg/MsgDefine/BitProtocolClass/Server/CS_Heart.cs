// 此文件由协议导出插件自动生成
// ID : 00008]

//****客户端心跳****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using IMClub;
using SingleMoba;
using Template;
namespace WuXingJingCai { }
namespace LaoHuJi { }
namespace MaJiang_QuanZhou { }
namespace MaJiang_XueZhan { }
namespace PaoDeKuai { }
namespace Baccarat { }
namespace BingShangQuGunQiu { }
namespace BuYu { }
namespace CheXuan { }
namespace CMSloto { }
namespace AskDao { }

namespace Server
{
	/// <summary>
	///客户端心跳
	/// <\summary>
	public class CS_Heart : CherishBitProtocolBase
	{
		/// <summary>
		///
		/// <\summary>
		public UserValiadateInfor UserValiadate;
		/// <summary>
		///
		/// <\summary>
		public Int64 ticks;
		public CS_Heart() { }

		public CS_Heart(UserValiadateInfor _UserValiadate, Int64 _ticks)
		{
			this.UserValiadate = _UserValiadate;
			this.ticks = _ticks;
		}
		private Byte[] get_UserValiadate_encoding()
		{
			Byte[] outBuf = null;
			outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
			return outBuf;
		}


		private Byte[] get_ticks_encoding()
		{
			Byte[] outBuf = null;
			outBuf = BitConverter.GetBytes((Int64)ticks);
			return outBuf;
		}

		private int set_UserValiadate_fromBuf(Byte[] sourceBuf, int curIndex)
		{
			Byte tag = sourceBuf[curIndex];
			curIndex += 1;
			if (tag != 0)
			{
				;
				UserValiadate = new UserValiadateInfor();
				curIndex = UserValiadate.Deserializer(sourceBuf, curIndex);
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
			if (UserValiadate != null)
			{
				memoryWrite.WriteByte(1);
				byteBuf = get_UserValiadate_encoding();
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
			startOffset = set_UserValiadate_fromBuf(sourceBuf, startOffset);
			startOffset = set_ticks_fromBuf(sourceBuf, startOffset);
			return startOffset;
		}

		public String get_UserValiadate_json()
		{
			if (UserValiadate == null) { return ""; }
			String resultJson = "\"UserValiadate\":"; resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson(); return resultJson;
		}


		public String get_ticks_json()
		{
			if (ticks == null) { return ""; }
			String resultJson = "\"ticks\":"; resultJson += "\""; resultJson += ticks.ToString(); resultJson += "\""; return resultJson;
		}


		public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj)
		{
			UserValiadate = new UserValiadateInfor();
			UserValiadate.DeserializerJson(jsonObj.ToJson());
		}


		public void set_ticks_fromJson(LitJson.JsonData jsonObj)
		{
			ticks = Int64.Parse(jsonObj.ToString());
		}

		public override String SerializerJson()
		{
			String resultStr = "{"; if (UserValiadate != null)
			{
				resultStr += get_UserValiadate_json();
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
			if (jsonObj["UserValiadate"] != null)
			{
				set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
			}
			if (jsonObj["ticks"] != null)
			{
				set_ticks_fromJson(jsonObj["ticks"]);
			}
		}
	}
}
