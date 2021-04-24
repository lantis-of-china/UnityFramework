﻿// 此文件由协议导出插件自动生成
// ID : 00007]
   
//****用户进入服务器****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户进入服务器
/// <\summary>
public class UserEntry : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///昵称
/// <\summary>
public String nickName;
/// <summary>
///性别GolabServerInfor
/// <\summary>
public Byte sex;
/// <summary>
///头像URL
/// <\summary>
public String headUrl;
/// <summary>
///1微信 2游客
/// <\summary>
public Byte isWeiChat;
/// <summary>
///纬度
/// <\summary>
public Single latitude;
/// <summary>
///经度
/// <\summary>
public Single longitude;
public UserEntry(){}

public UserEntry(UserValiadateInfor _UserValiadate, String _nickName, Byte _sex, String _headUrl, Byte _isWeiChat, Single _latitude, Single _longitude){
this.UserValiadate = _UserValiadate;
this.nickName = _nickName;
this.sex = _sex;
this.headUrl = _headUrl;
this.isWeiChat = _isWeiChat;
this.latitude = _latitude;
this.longitude = _longitude;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private Byte[] get_nickName_encoding(){
Byte[] outBuf = null;
String str = (String)nickName;
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


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)sex;
return outBuf;
}


private Byte[] get_headUrl_encoding(){
Byte[] outBuf = null;
String str = (String)headUrl;
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


private Byte[] get_isWeiChat_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)isWeiChat;
return outBuf;
}


private Byte[] get_latitude_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)latitude);
return outBuf;
}


private Byte[] get_longitude_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)longitude);
return outBuf;
}

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_nickName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
nickName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
nickName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Byte();
sex = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_headUrl_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
headUrl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
headUrl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_isWeiChat_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isWeiChat = new Byte();
isWeiChat = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_latitude_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
latitude = new Single();
latitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_longitude_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
longitude = new Single();
longitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(nickName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_nickName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(isWeiChat !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_isWeiChat_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(latitude !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_latitude_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(longitude !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_longitude_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_nickName_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_isWeiChat_fromBuf(sourceBuf,startOffset);
startOffset = set_latitude_fromBuf(sourceBuf,startOffset);
startOffset = set_longitude_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_nickName_json(){
if(nickName==null){return "";}String resultJson = "\"nickName\":";resultJson += "\"";resultJson += nickName.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_isWeiChat_json(){
if(isWeiChat==null){return "";}String resultJson = "\"isWeiChat\":";resultJson += "\"";resultJson += isWeiChat.ToString();resultJson += "\"";return resultJson;
}


public String get_latitude_json(){
if(latitude==null){return "";}String resultJson = "\"latitude\":";resultJson += "\"";resultJson += latitude.ToString();resultJson += "\"";return resultJson;
}


public String get_longitude_json(){
if(longitude==null){return "";}String resultJson = "\"longitude\":";resultJson += "\"";resultJson += longitude.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_nickName_fromJson(LitJson.JsonData jsonObj){
nickName= jsonObj.ToString();
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Byte.Parse(jsonObj.ToString());
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_isWeiChat_fromJson(LitJson.JsonData jsonObj){
isWeiChat= Byte.Parse(jsonObj.ToString());
}


public void set_latitude_fromJson(LitJson.JsonData jsonObj){
latitude= Single.Parse(jsonObj.ToString());
}


public void set_longitude_fromJson(LitJson.JsonData jsonObj){
longitude= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(nickName !=  null){
resultStr += ",";resultStr += get_nickName_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(isWeiChat !=  null){
resultStr += ",";resultStr += get_isWeiChat_json();
}
else {}if(latitude !=  null){
resultStr += ",";resultStr += get_latitude_json();
}
else {}if(longitude !=  null){
resultStr += ",";resultStr += get_longitude_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["nickName"] != null){
set_nickName_fromJson(jsonObj["nickName"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["isWeiChat"] != null){
set_isWeiChat_fromJson(jsonObj["isWeiChat"]);
}
if(jsonObj["latitude"] != null){
set_latitude_fromJson(jsonObj["latitude"]);
}
if(jsonObj["longitude"] != null){
set_longitude_fromJson(jsonObj["longitude"]);
}
}
}
}
