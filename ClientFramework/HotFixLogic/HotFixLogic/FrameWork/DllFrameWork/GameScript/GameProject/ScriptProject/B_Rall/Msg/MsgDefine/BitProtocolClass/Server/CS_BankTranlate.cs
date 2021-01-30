// 此文件由协议导出插件自动生成
// ID : 00001]
//********
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///
/// <\summary>
public class CS_BankTranlate : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///操作类型 1 存入 2 取出
/// <\summary>
public byte controlType;
/// <summary>
///货币类型 1 钻石 2 金币
/// <\summary>
public byte pointType;
/// <summary>
///操作数量
/// <\summary>
public Int32 count;
public CS_BankTranlate(){}

public CS_BankTranlate(UserValiadateInfor _UserValiadate, byte _controlType, byte _pointType, Int32 _count){
this.UserValiadate = _UserValiadate;
this.controlType = _controlType;
this.pointType = _pointType;
this.count = _count;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get_controlType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)controlType;
return outBuf;
}


private byte[] get_pointType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)pointType;
return outBuf;
}


private byte[] get_count_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)count);
return outBuf;
}

private int set_UserValiadate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_controlType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
controlType = new byte();
controlType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_pointType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
pointType = new byte();
pointType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_count_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
count = new Int32();
count = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(controlType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_controlType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(pointType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_pointType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(count !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_count_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_controlType_fromBuf(sourceBuf,startOffset);
startOffset = set_pointType_fromBuf(sourceBuf,startOffset);
startOffset = set_count_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get_controlType_json(){
if(controlType==null){return "";}string resultJson = "\"controlType\":";resultJson += "\"";resultJson += controlType.ToString();resultJson += "\"";return resultJson;
}


public string get_pointType_json(){
if(pointType==null){return "";}string resultJson = "\"pointType\":";resultJson += "\"";resultJson += pointType.ToString();resultJson += "\"";return resultJson;
}


public string get_count_json(){
if(count==null){return "";}string resultJson = "\"count\":";resultJson += "\"";resultJson += count.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_controlType_fromJson(LitJson.JsonData jsonObj){
controlType= byte.Parse(jsonObj.ToString());
}


public void set_pointType_fromJson(LitJson.JsonData jsonObj){
pointType= byte.Parse(jsonObj.ToString());
}


public void set_count_fromJson(LitJson.JsonData jsonObj){
count= Int32.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(controlType !=  null){
resultStr += ",";resultStr += get_controlType_json();
}
else {}if(pointType !=  null){
resultStr += ",";resultStr += get_pointType_json();
}
else {}if(count !=  null){
resultStr += ",";resultStr += get_count_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["controlType"] != null){
set_controlType_fromJson(jsonObj["controlType"]);
}
if(jsonObj["pointType"] != null){
set_pointType_fromJson(jsonObj["pointType"]);
}
if(jsonObj["count"] != null){
set_count_fromJson(jsonObj["count"]);
}
}
}
}
