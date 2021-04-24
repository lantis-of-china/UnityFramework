// 此文件由协议导出插件自动生成
// ID : 00001]

//****血池和奖池****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///血池和奖池
/// <\summary>
public class BloodPoolInfo : CherishBitProtocolBase {
/// <summary>
///游戏类型
/// <\summary>
public byte gameType;
/// <summary>
///场次ID
/// <\summary>
public Int32 cid;
/// <summary>
///最小放分
/// <\summary>
public Int64 poolSpitMin;
/// <summary>
///最大放分
/// <\summary>
public Int64 poolSpitMax;
/// <summary>
///最小吃分
/// <\summary>
public Int64 poolEatMin;
/// <summary>
///最大吃分
/// <\summary>
public Int64 poolEatMax;
/// <summary>
///游戏奖池
/// <\summary>
public Int64 gameValue;
/// <summary>
///奖池
/// <\summary>
public Int64 awardValue;
public BloodPoolInfo(){}

public BloodPoolInfo(byte _gameType, Int32 _cid, Int64 _poolSpitMin, Int64 _poolSpitMax, Int64 _poolEatMin, Int64 _poolEatMax, Int64 _gameValue, Int64 _awardValue){
this.gameType = _gameType;
this.cid = _cid;
this.poolSpitMin = _poolSpitMin;
this.poolSpitMax = _poolSpitMax;
this.poolEatMin = _poolEatMin;
this.poolEatMax = _poolEatMax;
this.gameValue = _gameValue;
this.awardValue = _awardValue;
}
private byte[] get_gameType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)gameType;
return outBuf;
}


private byte[] get_cid_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)cid);
return outBuf;
}


private byte[] get_poolSpitMin_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)poolSpitMin);
return outBuf;
}


private byte[] get_poolSpitMax_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)poolSpitMax);
return outBuf;
}


private byte[] get_poolEatMin_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)poolEatMin);
return outBuf;
}


private byte[] get_poolEatMax_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)poolEatMax);
return outBuf;
}


private byte[] get_gameValue_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)gameValue);
return outBuf;
}


private byte[] get_awardValue_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)awardValue);
return outBuf;
}

private int set_gameType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new byte();
gameType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_cid_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
cid = new Int32();
cid = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_poolSpitMin_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
poolSpitMin = new Int64();
poolSpitMin = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_poolSpitMax_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
poolSpitMax = new Int64();
poolSpitMax = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_poolEatMin_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
poolEatMin = new Int64();
poolEatMin = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_poolEatMax_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
poolEatMax = new Int64();
poolEatMax = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_gameValue_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameValue = new Int64();
gameValue = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_awardValue_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
awardValue = new Int64();
awardValue = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(gameType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(cid !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_cid_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(poolSpitMin !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_poolSpitMin_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(poolSpitMax !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_poolSpitMax_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(poolEatMin !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_poolEatMin_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(poolEatMax !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_poolEatMax_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameValue !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameValue_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(awardValue !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_awardValue_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_cid_fromBuf(sourceBuf,startOffset);
startOffset = set_poolSpitMin_fromBuf(sourceBuf,startOffset);
startOffset = set_poolSpitMax_fromBuf(sourceBuf,startOffset);
startOffset = set_poolEatMin_fromBuf(sourceBuf,startOffset);
startOffset = set_poolEatMax_fromBuf(sourceBuf,startOffset);
startOffset = set_gameValue_fromBuf(sourceBuf,startOffset);
startOffset = set_awardValue_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_gameType_json(){
if(gameType==null){return "";}string resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public string get_cid_json(){
if(cid==null){return "";}string resultJson = "\"cid\":";resultJson += "\"";resultJson += cid.ToString();resultJson += "\"";return resultJson;
}


public string get_poolSpitMin_json(){
if(poolSpitMin==null){return "";}string resultJson = "\"poolSpitMin\":";resultJson += "\"";resultJson += poolSpitMin.ToString();resultJson += "\"";return resultJson;
}


public string get_poolSpitMax_json(){
if(poolSpitMax==null){return "";}string resultJson = "\"poolSpitMax\":";resultJson += "\"";resultJson += poolSpitMax.ToString();resultJson += "\"";return resultJson;
}


public string get_poolEatMin_json(){
if(poolEatMin==null){return "";}string resultJson = "\"poolEatMin\":";resultJson += "\"";resultJson += poolEatMin.ToString();resultJson += "\"";return resultJson;
}


public string get_poolEatMax_json(){
if(poolEatMax==null){return "";}string resultJson = "\"poolEatMax\":";resultJson += "\"";resultJson += poolEatMax.ToString();resultJson += "\"";return resultJson;
}


public string get_gameValue_json(){
if(gameValue==null){return "";}string resultJson = "\"gameValue\":";resultJson += "\"";resultJson += gameValue.ToString();resultJson += "\"";return resultJson;
}


public string get_awardValue_json(){
if(awardValue==null){return "";}string resultJson = "\"awardValue\":";resultJson += "\"";resultJson += awardValue.ToString();resultJson += "\"";return resultJson;
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= byte.Parse(jsonObj.ToString());
}


public void set_cid_fromJson(LitJson.JsonData jsonObj){
cid= Int32.Parse(jsonObj.ToString());
}


public void set_poolSpitMin_fromJson(LitJson.JsonData jsonObj){
poolSpitMin= Int64.Parse(jsonObj.ToString());
}


public void set_poolSpitMax_fromJson(LitJson.JsonData jsonObj){
poolSpitMax= Int64.Parse(jsonObj.ToString());
}


public void set_poolEatMin_fromJson(LitJson.JsonData jsonObj){
poolEatMin= Int64.Parse(jsonObj.ToString());
}


public void set_poolEatMax_fromJson(LitJson.JsonData jsonObj){
poolEatMax= Int64.Parse(jsonObj.ToString());
}


public void set_gameValue_fromJson(LitJson.JsonData jsonObj){
gameValue= Int64.Parse(jsonObj.ToString());
}


public void set_awardValue_fromJson(LitJson.JsonData jsonObj){
awardValue= Int64.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(gameType !=  null){
resultStr += get_gameType_json();
}
else {}if(cid !=  null){
resultStr += ",";resultStr += get_cid_json();
}
else {}if(poolSpitMin !=  null){
resultStr += ",";resultStr += get_poolSpitMin_json();
}
else {}if(poolSpitMax !=  null){
resultStr += ",";resultStr += get_poolSpitMax_json();
}
else {}if(poolEatMin !=  null){
resultStr += ",";resultStr += get_poolEatMin_json();
}
else {}if(poolEatMax !=  null){
resultStr += ",";resultStr += get_poolEatMax_json();
}
else {}if(gameValue !=  null){
resultStr += ",";resultStr += get_gameValue_json();
}
else {}if(awardValue !=  null){
resultStr += ",";resultStr += get_awardValue_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["cid"] != null){
set_cid_fromJson(jsonObj["cid"]);
}
if(jsonObj["poolSpitMin"] != null){
set_poolSpitMin_fromJson(jsonObj["poolSpitMin"]);
}
if(jsonObj["poolSpitMax"] != null){
set_poolSpitMax_fromJson(jsonObj["poolSpitMax"]);
}
if(jsonObj["poolEatMin"] != null){
set_poolEatMin_fromJson(jsonObj["poolEatMin"]);
}
if(jsonObj["poolEatMax"] != null){
set_poolEatMax_fromJson(jsonObj["poolEatMax"]);
}
if(jsonObj["gameValue"] != null){
set_gameValue_fromJson(jsonObj["gameValue"]);
}
if(jsonObj["awardValue"] != null){
set_awardValue_fromJson(jsonObj["awardValue"]);
}
}
}
}
