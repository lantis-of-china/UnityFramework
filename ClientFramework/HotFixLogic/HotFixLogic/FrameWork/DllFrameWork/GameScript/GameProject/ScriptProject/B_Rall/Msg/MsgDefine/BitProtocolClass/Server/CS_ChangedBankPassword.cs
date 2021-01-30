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
public class CS_ChangedBankPassword : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///
/// <\summary>
public string oldPassword;
/// <summary>
///
/// <\summary>
public string newPassword;
public CS_ChangedBankPassword(){}

public CS_ChangedBankPassword(UserValiadateInfor _UserValiadate, string _oldPassword, string _newPassword){
this.UserValiadate = _UserValiadate;
this.oldPassword = _oldPassword;
this.newPassword = _newPassword;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get_oldPassword_encoding(){
byte[] outBuf = null;
string str = (string)oldPassword;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_newPassword_encoding(){
byte[] outBuf = null;
string str = (string)newPassword;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}

private int set_UserValiadate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_oldPassword_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_newPassword_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_oldPassword_fromBuf(sourceBuf,startOffset);
startOffset = set_newPassword_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get_oldPassword_json(){
if(oldPassword==null){return "";}string resultJson = "\"oldPassword\":";resultJson += "\"";resultJson += oldPassword.ToString();resultJson += "\"";return resultJson;
}


public string get_newPassword_json(){
if(newPassword==null){return "";}string resultJson = "\"newPassword\":";resultJson += "\"";resultJson += newPassword.ToString();resultJson += "\"";return resultJson;
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

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
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

public override void DeserializerJson(string json){
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
