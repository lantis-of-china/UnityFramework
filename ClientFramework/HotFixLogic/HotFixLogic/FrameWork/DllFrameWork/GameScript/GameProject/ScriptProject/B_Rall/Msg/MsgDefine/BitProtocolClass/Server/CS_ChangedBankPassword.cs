// 此文件由协议导出插件自动生成
// ID : 00001]
//****修改银行密码****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///修改银行密码
/// <\summary>
public class CS_ChangedBankPassword : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///
/// <\summary>
public String oldPassword;
/// <summary>
///
/// <\summary>
public String newPassword;
public CS_ChangedBankPassword(){}

public CS_ChangedBankPassword(UserValiadateInfor _UserValiadate, String _oldPassword, String _newPassword){
this.UserValiadate = _UserValiadate;
this.oldPassword = _oldPassword;
this.newPassword = _newPassword;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private Byte[] get_oldPassword_encoding(){
Byte[] outBuf = null;
String str = (String)oldPassword;
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


private Byte[] get_newPassword_encoding(){
Byte[] outBuf = null;
String str = (String)newPassword;
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

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_oldPassword_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
oldPassword = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
oldPassword = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_newPassword_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
newPassword = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
newPassword = System.Text.Encoding.UTF8.GetString(byteArray);
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
}if(oldPassword !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_oldPassword_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(newPassword !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_newPassword_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_oldPassword_fromBuf(sourceBuf,startOffset);
startOffset = set_newPassword_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_oldPassword_json(){
if(oldPassword==null){return "";}String resultJson = "\"oldPassword\":";resultJson += "\"";resultJson += oldPassword.ToString();resultJson += "\"";return resultJson;
}


public String get_newPassword_json(){
if(newPassword==null){return "";}String resultJson = "\"newPassword\":";resultJson += "\"";resultJson += newPassword.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_oldPassword_fromJson(LitJson.JsonData jsonObj){
oldPassword= jsonObj.ToString();
}


public void set_newPassword_fromJson(LitJson.JsonData jsonObj){
newPassword= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(oldPassword !=  null){
resultStr += ",";resultStr += get_oldPassword_json();
}
else {}if(newPassword !=  null){
resultStr += ",";resultStr += get_newPassword_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["oldPassword"] != null){
set_oldPassword_fromJson(jsonObj["oldPassword"]);
}
if(jsonObj["newPassword"] != null){
set_newPassword_fromJson(jsonObj["newPassword"]);
}
}
}
}
