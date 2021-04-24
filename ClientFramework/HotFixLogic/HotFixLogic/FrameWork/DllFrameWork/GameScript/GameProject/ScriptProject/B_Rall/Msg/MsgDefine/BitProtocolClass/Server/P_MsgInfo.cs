// 此文件由协议导出插件自动生成
// ID : 00001]
//****消息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///消息
/// <\summary>
public class P_MsgInfo : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public String token;
/// <summary>
///
/// <\summary>
public String title;
/// <summary>
///
/// <\summary>
public String msg;
public P_MsgInfo(){}

public P_MsgInfo(String _token, String _title, String _msg){
this.token = _token;
this.title = _title;
this.msg = _msg;
}
private Byte[] get_token_encoding(){
Byte[] outBuf = null;
String str = (String)token;
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


private Byte[] get_title_encoding(){
Byte[] outBuf = null;
String str = (String)title;
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


private Byte[] get_msg_encoding(){
Byte[] outBuf = null;
String str = (String)msg;
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

private int set_token_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
token = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
token = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_title_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
title = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
title = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_msg_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
msg = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
msg = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(token !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_token_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(title !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_title_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(msg !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_msg_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_token_fromBuf(sourceBuf,startOffset);
startOffset = set_title_fromBuf(sourceBuf,startOffset);
startOffset = set_msg_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_token_json(){
if(token==null){return "";}String resultJson = "\"token\":";resultJson += "\"";resultJson += token.ToString();resultJson += "\"";return resultJson;
}


public String get_title_json(){
if(title==null){return "";}String resultJson = "\"title\":";resultJson += "\"";resultJson += title.ToString();resultJson += "\"";return resultJson;
}


public String get_msg_json(){
if(msg==null){return "";}String resultJson = "\"msg\":";resultJson += "\"";resultJson += msg.ToString();resultJson += "\"";return resultJson;
}


public void set_token_fromJson(LitJson.JsonData jsonObj){
token= jsonObj.ToString();
}


public void set_title_fromJson(LitJson.JsonData jsonObj){
title= jsonObj.ToString();
}


public void set_msg_fromJson(LitJson.JsonData jsonObj){
msg= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(token !=  null){
resultStr += get_token_json();
}
else {}if(title !=  null){
resultStr += ",";resultStr += get_title_json();
}
else {}if(msg !=  null){
resultStr += ",";resultStr += get_msg_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["token"] != null){
set_token_fromJson(jsonObj["token"]);
}
if(jsonObj["title"] != null){
set_title_fromJson(jsonObj["title"]);
}
if(jsonObj["msg"] != null){
set_msg_fromJson(jsonObj["msg"]);
}
}
}
}
