﻿// 此文件由协议导出插件自动生成
// ID : 00001]
//****��Ա��ϸ�ṹ****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��Ա��ϸ�ṹ
/// <\summary>
public class P_MenberInfo : CherishBitProtocolBase {
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///����
/// <\summary>
public Int32 Score;
/// <summary>
///����ʱ��
/// <\summary>
public Int64 joinTime;
public P_MenberInfo(){}

public P_MenberInfo(Int32 _menberId, Int32 _Score, Int64 _joinTime){
this.menberId = _menberId;
this.Score = _Score;
this.joinTime = _joinTime;
}
private byte[] get_menberId_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private byte[] get_Score_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Score);
return outBuf;
}


private byte[] get_joinTime_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)joinTime);
return outBuf;
}

private int set_menberId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberId = new Int32();
menberId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_Score_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Score = new Int32();
Score = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_joinTime_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
joinTime = new Int64();
joinTime = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(menberId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Score !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Score_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(joinTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_joinTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_Score_fromBuf(sourceBuf,startOffset);
startOffset = set_joinTime_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_menberId_json(){
if(menberId==null){return "";}string resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public string get_Score_json(){
if(Score==null){return "";}string resultJson = "\"Score\":";resultJson += "\"";resultJson += Score.ToString();resultJson += "\"";return resultJson;
}


public string get_joinTime_json(){
if(joinTime==null){return "";}string resultJson = "\"joinTime\":";resultJson += "\"";resultJson += joinTime.ToString();resultJson += "\"";return resultJson;
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_Score_fromJson(LitJson.JsonData jsonObj){
Score= Int32.Parse(jsonObj.ToString());
}


public void set_joinTime_fromJson(LitJson.JsonData jsonObj){
joinTime= Int64.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(menberId !=  null){
resultStr += get_menberId_json();
}
else {}if(Score !=  null){
resultStr += ",";resultStr += get_Score_json();
}
else {}if(joinTime !=  null){
resultStr += ",";resultStr += get_joinTime_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["Score"] != null){
set_Score_fromJson(jsonObj["Score"]);
}
if(jsonObj["joinTime"] != null){
set_joinTime_fromJson(jsonObj["joinTime"]);
}
}
}
}