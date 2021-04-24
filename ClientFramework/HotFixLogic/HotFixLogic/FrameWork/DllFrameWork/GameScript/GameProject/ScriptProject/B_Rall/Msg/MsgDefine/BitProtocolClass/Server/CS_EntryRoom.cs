// 此文件由协议导出插件自动生成
// ID : 00001]
//****进入房间****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///进入房间
/// <\summary>
public class CS_EntryRoom : LantisBitProtocolBase {
/// <summary>
///进入房间方式 1快速房间 2加入房间
/// <\summary>
public Byte entryType;
/// <summary>
///如果是加入房间带上房间ID
/// <\summary>
public Int32 roomId;
/// <summary>
///场次ID
/// <\summary>
public Int32 conditionId;
/// <summary>
///消息验证
/// <\summary>
public UserValiadateInfor UserValiadate;
public CS_EntryRoom(){}

public CS_EntryRoom(Byte _entryType, Int32 _roomId, Int32 _conditionId, UserValiadateInfor _UserValiadate){
this.entryType = _entryType;
this.roomId = _roomId;
this.conditionId = _conditionId;
this.UserValiadate = _UserValiadate;
}
private Byte[] get_entryType_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)entryType;
return outBuf;
}


private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
return outBuf;
}


private Byte[] get_conditionId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)conditionId);
return outBuf;
}


private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}

private int set_entryType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
entryType = new Byte();
entryType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_roomId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomId = new Int32();
roomId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_conditionId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
conditionId = new Int32();
conditionId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(entryType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_entryType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(roomId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(conditionId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_conditionId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_entryType_fromBuf(sourceBuf,startOffset);
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_conditionId_fromBuf(sourceBuf,startOffset);
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_entryType_json(){
if(entryType==null){return "";}String resultJson = "\"entryType\":";resultJson += "\"";resultJson += entryType.ToString();resultJson += "\"";return resultJson;
}


public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public String get_conditionId_json(){
if(conditionId==null){return "";}String resultJson = "\"conditionId\":";resultJson += "\"";resultJson += conditionId.ToString();resultJson += "\"";return resultJson;
}


public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public void set_entryType_fromJson(LitJson.JsonData jsonObj){
entryType= Byte.Parse(jsonObj.ToString());
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_conditionId_fromJson(LitJson.JsonData jsonObj){
conditionId= Int32.Parse(jsonObj.ToString());
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(entryType !=  null){
resultStr += get_entryType_json();
}
else {}if(roomId !=  null){
resultStr += ",";resultStr += get_roomId_json();
}
else {}if(conditionId !=  null){
resultStr += ",";resultStr += get_conditionId_json();
}
else {}if(UserValiadate !=  null){
resultStr += ",";resultStr += get_UserValiadate_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["entryType"] != null){
set_entryType_fromJson(jsonObj["entryType"]);
}
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
if(jsonObj["conditionId"] != null){
set_conditionId_fromJson(jsonObj["conditionId"]);
}
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
}
}
}
