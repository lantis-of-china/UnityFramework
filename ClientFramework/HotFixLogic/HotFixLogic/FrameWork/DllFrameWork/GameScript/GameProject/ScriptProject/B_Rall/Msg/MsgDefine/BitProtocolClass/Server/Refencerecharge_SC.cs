// 此文件由协议导出插件自动生成
// ID : 00010]
//****刷新房卡数量****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///刷新房卡数量
/// <\summary>
public class Refencerecharge_SC : CherishBitProtocolBase {
/// <summary>
///房卡数量
/// <\summary>
public Int32 rechargeCount;
/// <summary>
///刷新前数量
/// <\summary>
public Int32 oldRechargeCount;
public Refencerecharge_SC(){}

public Refencerecharge_SC(Int32 _rechargeCount, Int32 _oldRechargeCount){
this.rechargeCount = _rechargeCount;
this.oldRechargeCount = _oldRechargeCount;
}
private byte[] get_rechargeCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeCount);
return outBuf;
}


private byte[] get_oldRechargeCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)oldRechargeCount);
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
private int set_oldRechargeCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
oldRechargeCount = new Int32();
oldRechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
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
}if(oldRechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_oldRechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_rechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_oldRechargeCount_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_rechargeCount_json(){
if(rechargeCount==null){return "";}string resultJson = "\"rechargeCount\":";resultJson += "\"";resultJson += rechargeCount.ToString();resultJson += "\"";return resultJson;
}


public string get_oldRechargeCount_json(){
if(oldRechargeCount==null){return "";}string resultJson = "\"oldRechargeCount\":";resultJson += "\"";resultJson += oldRechargeCount.ToString();resultJson += "\"";return resultJson;
}


public void set_rechargeCount_fromJson(LitJson.JsonData jsonObj){
rechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_oldRechargeCount_fromJson(LitJson.JsonData jsonObj){
oldRechargeCount= Int32.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(rechargeCount !=  null){
resultStr += get_rechargeCount_json();
}
else {}if(oldRechargeCount !=  null){
resultStr += ",";resultStr += get_oldRechargeCount_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["rechargeCount"] != null){
set_rechargeCount_fromJson(jsonObj["rechargeCount"]);
}
if(jsonObj["oldRechargeCount"] != null){
set_oldRechargeCount_fromJson(jsonObj["oldRechargeCount"]);
}
}
}
}
