// 此文件由协议导出插件自动生成
// ID : 00000]
//****停止移动****
using System;
using System.Collections.Generic;
using System.IO;


namespace SingleMoba{
/// <summary>
///停止移动
/// <\summary>
public class SC_GamerMoveStop : CherishBitProtocolBase {
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
public SC_GamerMoveStop(){}

public SC_GamerMoveStop(Int32 _playerId, Single _currentX, Single _currentY){
this.playerId = _playerId;
this.currentX = _currentX;
this.currentY = _currentY;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_currentX_fromBuf(sourceBuf,startOffset);
startOffset = set_currentY_fromBuf(sourceBuf,startOffset);
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


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_currentX_fromJson(LitJson.JsonData jsonObj){
currentX= Single.Parse(jsonObj.ToString());
}


public void set_currentY_fromJson(LitJson.JsonData jsonObj){
currentY= Single.Parse(jsonObj.ToString());
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
}
}
}
