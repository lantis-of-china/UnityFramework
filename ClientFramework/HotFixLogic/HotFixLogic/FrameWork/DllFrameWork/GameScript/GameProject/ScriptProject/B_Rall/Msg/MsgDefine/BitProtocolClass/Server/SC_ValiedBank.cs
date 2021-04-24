// 此文件由协议导出插件自动生成
// ID : 00001]
//****银行登录返回****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///银行登录返回
/// <\summary>
public class SC_ValiedBank : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Byte result;
/// <summary>
///1密码正确 0密码错误
/// <\summary>
public Int32 rechargeBank;
/// <summary>
///
/// <\summary>
public Int32 goldBank;
public SC_ValiedBank(){}

public SC_ValiedBank(Byte _result, Int32 _rechargeBank, Int32 _goldBank){
this.result = _result;
this.rechargeBank = _rechargeBank;
this.goldBank = _goldBank;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
return outBuf;
}


private Byte[] get_rechargeBank_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeBank);
return outBuf;
}


private Byte[] get_goldBank_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)goldBank);
return outBuf;
}

private int set_result_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new Byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_rechargeBank_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
rechargeBank = new Int32();
rechargeBank = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_goldBank_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
goldBank = new Int32();
goldBank = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(result !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_result_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(rechargeBank !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_rechargeBank_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(goldBank !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_goldBank_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_rechargeBank_fromBuf(sourceBuf,startOffset);
startOffset = set_goldBank_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_rechargeBank_json(){
if(rechargeBank==null){return "";}String resultJson = "\"rechargeBank\":";resultJson += "\"";resultJson += rechargeBank.ToString();resultJson += "\"";return resultJson;
}


public String get_goldBank_json(){
if(goldBank==null){return "";}String resultJson = "\"goldBank\":";resultJson += "\"";resultJson += goldBank.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_rechargeBank_fromJson(LitJson.JsonData jsonObj){
rechargeBank= Int32.Parse(jsonObj.ToString());
}


public void set_goldBank_fromJson(LitJson.JsonData jsonObj){
goldBank= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(rechargeBank !=  null){
resultStr += ",";resultStr += get_rechargeBank_json();
}
else {}if(goldBank !=  null){
resultStr += ",";resultStr += get_goldBank_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["rechargeBank"] != null){
set_rechargeBank_fromJson(jsonObj["rechargeBank"]);
}
if(jsonObj["goldBank"] != null){
set_goldBank_fromJson(jsonObj["goldBank"]);
}
}
}
}
