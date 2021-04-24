﻿// 此文件由协议导出插件自动生成
// ID : 10004]
   
//****用户退出****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户退出
/// <\summary>
public class MTCS_USER_LOGIN_OUT : LantisBitProtocolBase {
/// <summary>
///用户的关键值ID
/// <\summary>
public Int32 DatingNumber;
/// <summary>
///金币
/// <\summary>
public Int32 Gold;
/// <summary>
///钻石
/// <\summary>
public Int32 RechargeCount;
/// <summary>
///
/// <\summary>
public String ServerId;
public MTCS_USER_LOGIN_OUT(){}

public MTCS_USER_LOGIN_OUT(Int32 _DatingNumber, Int32 _Gold, Int32 _RechargeCount, String _ServerId){
this.DatingNumber = _DatingNumber;
this.Gold = _Gold;
this.RechargeCount = _RechargeCount;
this.ServerId = _ServerId;
}
private Byte[] get_DatingNumber_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)DatingNumber);
return outBuf;
}


private Byte[] get_Gold_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Gold);
return outBuf;
}


private Byte[] get_RechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)RechargeCount);
return outBuf;
}


private Byte[] get_ServerId_encoding(){
Byte[] outBuf = null;
String str = (String)ServerId;
Char[] charArray = str.ToCharArray();
Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
Byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}

private int set_DatingNumber_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
DatingNumber = new Int32();
DatingNumber = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_Gold_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Gold = new Int32();
Gold = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_RechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
RechargeCount = new Int32();
RechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_ServerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ServerId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ServerId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(DatingNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_DatingNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Gold !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Gold_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(RechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_RechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ServerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ServerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_DatingNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_Gold_fromBuf(sourceBuf,startOffset);
startOffset = set_RechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_ServerId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_DatingNumber_json(){
if(DatingNumber==null){return "";}String resultJson = "\"DatingNumber\":";resultJson += "\"";resultJson += DatingNumber.ToString();resultJson += "\"";return resultJson;
}


public String get_Gold_json(){
if(Gold==null){return "";}String resultJson = "\"Gold\":";resultJson += "\"";resultJson += Gold.ToString();resultJson += "\"";return resultJson;
}


public String get_RechargeCount_json(){
if(RechargeCount==null){return "";}String resultJson = "\"RechargeCount\":";resultJson += "\"";resultJson += RechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_ServerId_json(){
if(ServerId==null){return "";}String resultJson = "\"ServerId\":";resultJson += "\"";resultJson += ServerId.ToString();resultJson += "\"";return resultJson;
}


public void set_DatingNumber_fromJson(LitJson.JsonData jsonObj){
DatingNumber= Int32.Parse(jsonObj.ToString());
}


public void set_Gold_fromJson(LitJson.JsonData jsonObj){
Gold= Int32.Parse(jsonObj.ToString());
}


public void set_RechargeCount_fromJson(LitJson.JsonData jsonObj){
RechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_ServerId_fromJson(LitJson.JsonData jsonObj){
ServerId= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(DatingNumber !=  null){
resultStr += get_DatingNumber_json();
}
else {}if(Gold !=  null){
resultStr += ",";resultStr += get_Gold_json();
}
else {}if(RechargeCount !=  null){
resultStr += ",";resultStr += get_RechargeCount_json();
}
else {}if(ServerId !=  null){
resultStr += ",";resultStr += get_ServerId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["DatingNumber"] != null){
set_DatingNumber_fromJson(jsonObj["DatingNumber"]);
}
if(jsonObj["Gold"] != null){
set_Gold_fromJson(jsonObj["Gold"]);
}
if(jsonObj["RechargeCount"] != null){
set_RechargeCount_fromJson(jsonObj["RechargeCount"]);
}
if(jsonObj["ServerId"] != null){
set_ServerId_fromJson(jsonObj["ServerId"]);
}
}
}
}
