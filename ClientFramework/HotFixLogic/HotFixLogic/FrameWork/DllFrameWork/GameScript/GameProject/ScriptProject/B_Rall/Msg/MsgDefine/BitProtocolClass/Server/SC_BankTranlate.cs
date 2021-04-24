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
public class SC_BankTranlate : LantisBitProtocolBase {
/// <summary>
///操作数量
/// <\summary>
public Byte result;
/// <summary>
///1成功 2失败
/// <\summary>
public Int32 rechargeBank;
/// <summary>
///
/// <\summary>
public Int32 goldBank;
/// <summary>
///
/// <\summary>
public Int32 rechargeCount;
/// <summary>
///
/// <\summary>
public Int32 goldCount;
public SC_BankTranlate(){}

public SC_BankTranlate(Byte _result, Int32 _rechargeBank, Int32 _goldBank, Int32 _rechargeCount, Int32 _goldCount){
this.result = _result;
this.rechargeBank = _rechargeBank;
this.goldBank = _goldBank;
this.rechargeCount = _rechargeCount;
this.goldCount = _goldCount;
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
}if(rechargeCount !=  null){
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
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_rechargeBank_fromBuf(sourceBuf,startOffset);
startOffset = set_goldBank_fromBuf(sourceBuf,startOffset);
startOffset = set_rechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_goldCount_fromBuf(sourceBuf,startOffset);
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


public String get_rechargeCount_json(){
if(rechargeCount==null){return "";}String resultJson = "\"rechargeCount\":";resultJson += "\"";resultJson += rechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_goldCount_json(){
if(goldCount==null){return "";}String resultJson = "\"goldCount\":";resultJson += "\"";resultJson += goldCount.ToString();resultJson += "\"";return resultJson;
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


public void set_rechargeCount_fromJson(LitJson.JsonData jsonObj){
rechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_goldCount_fromJson(LitJson.JsonData jsonObj){
goldCount= Int32.Parse(jsonObj.ToString());
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
else {}if(rechargeCount !=  null){
resultStr += ",";resultStr += get_rechargeCount_json();
}
else {}if(goldCount !=  null){
resultStr += ",";resultStr += get_goldCount_json();
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
if(jsonObj["rechargeCount"] != null){
set_rechargeCount_fromJson(jsonObj["rechargeCount"]);
}
if(jsonObj["goldCount"] != null){
set_goldCount_fromJson(jsonObj["goldCount"]);
}
}
}
}
