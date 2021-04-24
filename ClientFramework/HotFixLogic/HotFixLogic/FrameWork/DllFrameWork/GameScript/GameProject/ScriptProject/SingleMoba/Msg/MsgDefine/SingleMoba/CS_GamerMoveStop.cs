// 此文件由协议导出插件自动生成
// ID : 00000]
//****位移消息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///位移消息
/// <\summary>
public class CS_GamerMoveStop : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///
/// <\summary>
public Single currentX;
/// <summary>
///
/// <\summary>
public Single currentY;
public CS_GamerMoveStop(){}

public CS_GamerMoveStop(UserValiadateInfor _UserValiadate, Single _currentX, Single _currentY){
this.UserValiadate = _UserValiadate;
this.currentX = _currentX;
this.currentY = _currentY;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private Byte[] get_currentX_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)currentX);
return outBuf;
}


private Byte[] get_currentY_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)currentY);
return outBuf;
}

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_currentX_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
currentX = new Single();
currentX = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_currentY_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
currentY = new Single();
currentY = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(currentX !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_currentX_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(currentY !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_currentY_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_currentX_fromBuf(sourceBuf,startOffset);
startOffset = set_currentY_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_currentX_json(){
if(currentX==null){return "";}String resultJson = "\"currentX\":";resultJson += "\"";resultJson += currentX.ToString();resultJson += "\"";return resultJson;
}


public String get_currentY_json(){
if(currentY==null){return "";}String resultJson = "\"currentY\":";resultJson += "\"";resultJson += currentY.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_currentX_fromJson(LitJson.JsonData jsonObj){
currentX= Single.Parse(jsonObj.ToString());
}


public void set_currentY_fromJson(LitJson.JsonData jsonObj){
currentY= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(currentX !=  null){
resultStr += ",";resultStr += get_currentX_json();
}
else {}if(currentY !=  null){
resultStr += ",";resultStr += get_currentY_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["currentX"] != null){
set_currentX_fromJson(jsonObj["currentX"]);
}
if(jsonObj["currentY"] != null){
set_currentY_fromJson(jsonObj["currentY"]);
}
}
}
}
