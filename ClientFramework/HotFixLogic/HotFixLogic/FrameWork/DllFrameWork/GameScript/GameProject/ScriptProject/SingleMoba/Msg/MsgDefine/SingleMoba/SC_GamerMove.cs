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
public class SC_GamerMove : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public Single currentX;
/// <summary>
///
/// <\summary>
public Single currentY;
/// <summary>
///
/// <\summary>
public Single targetX;
/// <summary>
///
/// <\summary>
public Single targetY;
/// <summary>
///添加Ticks用于同步移动
/// <\summary>
public Int64 ticks;
public SC_GamerMove(){}

public SC_GamerMove(Int32 _playerId, Single _currentX, Single _currentY, Single _targetX, Single _targetY, Int64 _ticks){
this.playerId = _playerId;
this.currentX = _currentX;
this.currentY = _currentY;
this.targetX = _targetX;
this.targetY = _targetY;
this.ticks = _ticks;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
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


private Byte[] get_targetX_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)targetX);
return outBuf;
}


private Byte[] get_targetY_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)targetY);
return outBuf;
}


private Byte[] get_ticks_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)ticks);
return outBuf;
}

private int set_playerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
playerId = new Int32();
playerId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
private int set_targetX_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
targetX = new Single();
targetX = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_targetY_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
targetY = new Single();
targetY = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_ticks_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ticks = new Int64();
ticks = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(playerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_playerId_encoding();
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
}if(targetX !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_targetX_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(targetY !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_targetY_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ticks !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ticks_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_currentX_fromBuf(sourceBuf,startOffset);
startOffset = set_currentY_fromBuf(sourceBuf,startOffset);
startOffset = set_targetX_fromBuf(sourceBuf,startOffset);
startOffset = set_targetY_fromBuf(sourceBuf,startOffset);
startOffset = set_ticks_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_currentX_json(){
if(currentX==null){return "";}String resultJson = "\"currentX\":";resultJson += "\"";resultJson += currentX.ToString();resultJson += "\"";return resultJson;
}


public String get_currentY_json(){
if(currentY==null){return "";}String resultJson = "\"currentY\":";resultJson += "\"";resultJson += currentY.ToString();resultJson += "\"";return resultJson;
}


public String get_targetX_json(){
if(targetX==null){return "";}String resultJson = "\"targetX\":";resultJson += "\"";resultJson += targetX.ToString();resultJson += "\"";return resultJson;
}


public String get_targetY_json(){
if(targetY==null){return "";}String resultJson = "\"targetY\":";resultJson += "\"";resultJson += targetY.ToString();resultJson += "\"";return resultJson;
}


public String get_ticks_json(){
if(ticks==null){return "";}String resultJson = "\"ticks\":";resultJson += "\"";resultJson += ticks.ToString();resultJson += "\"";return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_currentX_fromJson(LitJson.JsonData jsonObj){
currentX= Single.Parse(jsonObj.ToString());
}


public void set_currentY_fromJson(LitJson.JsonData jsonObj){
currentY= Single.Parse(jsonObj.ToString());
}


public void set_targetX_fromJson(LitJson.JsonData jsonObj){
targetX= Single.Parse(jsonObj.ToString());
}


public void set_targetY_fromJson(LitJson.JsonData jsonObj){
targetY= Single.Parse(jsonObj.ToString());
}


public void set_ticks_fromJson(LitJson.JsonData jsonObj){
ticks= Int64.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(currentX !=  null){
resultStr += ",";resultStr += get_currentX_json();
}
else {}if(currentY !=  null){
resultStr += ",";resultStr += get_currentY_json();
}
else {}if(targetX !=  null){
resultStr += ",";resultStr += get_targetX_json();
}
else {}if(targetY !=  null){
resultStr += ",";resultStr += get_targetY_json();
}
else {}if(ticks !=  null){
resultStr += ",";resultStr += get_ticks_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["currentX"] != null){
set_currentX_fromJson(jsonObj["currentX"]);
}
if(jsonObj["currentY"] != null){
set_currentY_fromJson(jsonObj["currentY"]);
}
if(jsonObj["targetX"] != null){
set_targetX_fromJson(jsonObj["targetX"]);
}
if(jsonObj["targetY"] != null){
set_targetY_fromJson(jsonObj["targetY"]);
}
if(jsonObj["ticks"] != null){
set_ticks_fromJson(jsonObj["ticks"]);
}
}
}
}
