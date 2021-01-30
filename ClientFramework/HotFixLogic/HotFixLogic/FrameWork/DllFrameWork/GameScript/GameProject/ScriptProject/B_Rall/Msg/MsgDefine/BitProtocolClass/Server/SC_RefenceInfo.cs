// 此文件由协议导出插件自动生成
// ID : 00001]
//****返回刷新信息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///返回刷新信息
/// <\summary>
public class SC_RefenceInfo : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 rechargeCount;
/// <summary>
///
/// <\summary>
public Int32 goldCount;
public SC_RefenceInfo(){}

public SC_RefenceInfo(Int32 _rechargeCount, Int32 _goldCount){
this.rechargeCount = _rechargeCount;
this.goldCount = _goldCount;
}
private byte[] get_rechargeCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeCount);
return outBuf;
}


private byte[] get_goldCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)goldCount);
return outBuf;
}

private int set_rechargeCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
rechargeCount = new Int32();
rechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_goldCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
goldCount = new Int32();
goldCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(rechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_rechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(goldCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_goldCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_rechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_goldCount_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_rechargeCount_json(){
if(rechargeCount==null){return "";}string resultJson = "\"rechargeCount\":";resultJson += "\"";resultJson += rechargeCount.ToString();resultJson += "\"";return resultJson;
}


public string get_goldCount_json(){
if(goldCount==null){return "";}string resultJson = "\"goldCount\":";resultJson += "\"";resultJson += goldCount.ToString();resultJson += "\"";return resultJson;
}


public void set_rechargeCount_fromJson(LitJson.JsonData jsonObj){
rechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_goldCount_fromJson(LitJson.JsonData jsonObj){
goldCount= Int32.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(rechargeCount !=  null){
resultStr += get_rechargeCount_json();
}
else {}if(goldCount !=  null){
resultStr += ",";resultStr += get_goldCount_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["rechargeCount"] != null){
set_rechargeCount_fromJson(jsonObj["rechargeCount"]);
}
if(jsonObj["goldCount"] != null){
set_goldCount_fromJson(jsonObj["goldCount"]);
}
}
}
}
