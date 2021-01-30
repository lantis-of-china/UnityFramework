// 此文件由协议导出插件自动生成
// ID : 00001]

//****��Ա��ϸ�ṹ****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub
{
	/// <summary>
	///��Ա��ϸ�ṹ
	/// <\summary>
	public class P_ClubGradeInfo : CherishBitProtocolBase
	{
		/// <summary>
		///
		/// <\summary>
		public List<Int32> menberIdList;
		/// <summary>
		///ʱ��
		/// <\summary>
		public Int64 time;
		/// <summary>
		///���ֲ�ս��
		/// <\summary>
		public Server.SC_ClubScoreBack clubGrade;
		public P_ClubGradeInfo() { }

		public P_ClubGradeInfo(List<Int32> _menberIdList, Int64 _time, Server.SC_ClubScoreBack _clubGrade)
		{
			this.menberIdList = _menberIdList;
			this.time = _time;
			this.clubGrade = _clubGrade;
		}
		private byte[] get_menberIdList_encoding()
		{
			byte[] outBuf = null;
			using (MemoryStream memoryWrite = new MemoryStream())
			{
				List<Int32> listInt32 = (List<Int32>)menberIdList;
				memoryWrite.Write(BitConverter.GetBytes(listInt32.Count), 0, 4);
				for (int i = 0; i < listInt32.Count; ++i)
				{
					Int32 in32 = listInt32[i];
					memoryWrite.Write(BitConverter.GetBytes(in32), 0, 4);
				}
				outBuf = memoryWrite.ToArray();
			}
			return outBuf;
		}


		private byte[] get_time_encoding()
		{
			byte[] outBuf = null;
			outBuf = BitConverter.GetBytes((Int64)time);
			return outBuf;
		}


		private byte[] get_clubGrade_encoding()
		{
			byte[] outBuf = null;
			outBuf = ((CherishBitProtocolBase)clubGrade).Serializer();
			return outBuf;
		}

		private int set_menberIdList_fromBuf(byte[] sourceBuf, int curIndex)
		{
			byte tag = sourceBuf[curIndex];
			curIndex += 1;
			if (tag != 0)
			{
				;
				menberIdList = new List<Int32>();
				int listCount = BitConverter.ToInt32(sourceBuf, curIndex);
				curIndex += 4;
				for (int index = 0; index < listCount; ++index)
				{
					Int32 curTarget = BitConverter.ToInt32(sourceBuf, curIndex);
					menberIdList.Add(curTarget);
					curIndex += 4;
				}
			}
			return curIndex;
		}
		private int set_time_fromBuf(byte[] sourceBuf, int curIndex)
		{
			byte tag = sourceBuf[curIndex];
			curIndex += 1;
			if (tag != 0)
			{
				;
				time = new Int64();
				time = BitConverter.ToInt64(sourceBuf, curIndex);
				curIndex += 8;
			}
			return curIndex;
		}
		private int set_clubGrade_fromBuf(byte[] sourceBuf, int curIndex)
		{
			byte tag = sourceBuf[curIndex];
			curIndex += 1;
			if (tag != 0)
			{
				;
				clubGrade = new Server.SC_ClubScoreBack();
				curIndex = clubGrade.Deserializer(sourceBuf, curIndex);
			}
			return curIndex;
		}
		public override byte[] Serializer()
		{
			MemoryStream memoryWrite = new MemoryStream();
			byte[] byteBuf = null;
			if (menberIdList != null)
			{
				memoryWrite.WriteByte(1);
				byteBuf = get_menberIdList_encoding();
				memoryWrite.Write(byteBuf, 0, byteBuf.Length);
			}
			else
			{
				memoryWrite.WriteByte(0);
			}
			if (time != null)
			{
				memoryWrite.WriteByte(1);
				byteBuf = get_time_encoding();
				memoryWrite.Write(byteBuf, 0, byteBuf.Length);
			}
			else
			{
				memoryWrite.WriteByte(0);
			}
			if (clubGrade != null)
			{
				memoryWrite.WriteByte(1);
				byteBuf = get_clubGrade_encoding();
				memoryWrite.Write(byteBuf, 0, byteBuf.Length);
			}
			else
			{
				memoryWrite.WriteByte(0);
			}
			byte[] bufResult = memoryWrite.ToArray(); memoryWrite.Dispose();
			return bufResult;
		}

		public override int Deserializer(byte[] sourceBuf, int startOffset)
		{
			startOffset = set_menberIdList_fromBuf(sourceBuf, startOffset);
			startOffset = set_time_fromBuf(sourceBuf, startOffset);
			startOffset = set_clubGrade_fromBuf(sourceBuf, startOffset);
			return startOffset;
		}

		public string get_menberIdList_json()
		{
			if (menberIdList == null) { return ""; }
			string resultJson = "\"menberIdList\":"; resultJson += "["; List<Int32> listObj = (List<Int32>)menberIdList;
			for (int i = 0; i < listObj.Count; ++i)
			{
				Int32 item = listObj[i];
				if (i > 0) { resultJson += ","; }
				resultJson += "\""; resultJson += item.ToString();
				resultJson += "\"";
			}
			resultJson += "]";
			return resultJson;
		}


		public string get_time_json()
		{
			if (time == null) { return ""; }
			string resultJson = "\"time\":"; resultJson += "\""; resultJson += time.ToString(); resultJson += "\""; return resultJson;
		}


		public string get_clubGrade_json()
		{
			if (clubGrade == null) { return ""; }
			string resultJson = "\"clubGrade\":"; resultJson += ((CherishBitProtocolBase)clubGrade).SerializerJson(); return resultJson;
		}


		public void set_menberIdList_fromJson(LitJson.JsonData jsonObj)
		{
			menberIdList = new List<Int32>();
			foreach (LitJson.JsonData jsonItem in jsonObj)
			{
				menberIdList.Add(Int32.Parse(jsonItem.ToString()));
			}

		}


		public void set_time_fromJson(LitJson.JsonData jsonObj)
		{
			time = Int64.Parse(jsonObj.ToString());
		}


		public void set_clubGrade_fromJson(LitJson.JsonData jsonObj)
		{
			clubGrade = new Server.SC_ClubScoreBack();
			clubGrade.DeserializerJson(jsonObj.ToJson());
		}

		public override string SerializerJson()
		{
			string resultStr = "{"; if (menberIdList != null)
			{
				resultStr += get_menberIdList_json();
			}
			else { }
			if (time != null)
			{
				resultStr += ","; resultStr += get_time_json();
			}
			else { }
			if (clubGrade != null)
			{
				resultStr += ","; resultStr += get_clubGrade_json();
			}
			else { }
			resultStr += "}"; return resultStr;
		}

		public override void DeserializerJson(string json)
		{
			LitJson.JsonData jsonObj = CSTools.JsonToData(json);
			if (jsonObj["menberIdList"] != null)
			{
				set_menberIdList_fromJson(jsonObj["menberIdList"]);
			}
			if (jsonObj["time"] != null)
			{
				set_time_fromJson(jsonObj["time"]);
			}
			if (jsonObj["clubGrade"] != null)
			{
				set_clubGrade_fromJson(jsonObj["clubGrade"]);
			}
		}
	}
}
