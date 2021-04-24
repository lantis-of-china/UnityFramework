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
public class SC_RefenceInfo : LantisBitProtocolBase {
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
private Byte[] get_rechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeCount);
return outBuf;
}


private Byte[] get_goldCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)goldCount);
return outBuf;
}

private int set_rechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
rechargeCount = new Int32();
rechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_goldCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
goldCount = new Int32();
goldCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_rechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_goldCount_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_rechargeCount_json(){
if(rechargeCount==null){return "";}String resultJson = "\"rechargeCount\":";resultJson += "\"";resultJson += rechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_goldCount_json(){
if(goldCount==null){return "";}String resultJson = "\"goldCount\":";resultJson += "\"";resultJson += goldCount.ToString();resultJson += "\"";return resultJson;
}


public void set_rechargeCount_fromJson(LitJson.JsonData jsonObj){
rechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_goldCount_fromJson(LitJson.JsonData jsonObj){
goldCount= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(rechargeCount !=  null){
resultStr += get_rechargeCount_json();
}
else {}if(goldCount !=  null){
resultStr += ",";resultStr += get_goldCount_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
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
