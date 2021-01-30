// 此文件由协议导出插件自动生成
// ID : 00003]

//****用户注册****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户注册
/// <\summary>
public class MessageUserRegist : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public string Account;
/// <summary>
///
/// <\summary>
public string PassWord;
/// <summary>
///
/// <\summary>
public string Email;
public MessageUserRegist(){}

public MessageUserRegist(string _Account, string _PassWord, string _Email){
this.Account = _Account;
this.PassWord = _PassWord;
this.Email = _Email;
}
private byte[] get_Account_encoding(){
byte[] outBuf = null;
string str = (string)Account;
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


private byte[] get_PassWord_encoding(){
byte[] outBuf = null;
string str = (string)PassWord;
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


private byte[] get_Email_encoding(){
byte[] outBuf = null;
string str = (string)Email;
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

private int set_Account_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Account = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
Account = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_PassWord_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
PassWord = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
PassWord = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_Email_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Email = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
Email = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(Account !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Account_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(PassWord !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_PassWord_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Email !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Email_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_Account_fromBuf(sourceBuf,startOffset);
startOffset = set_PassWord_fromBuf(sourceBuf,startOffset);
startOffset = set_Email_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_Account_json(){
if(Account==null){return "";}string resultJson = "\"Account\":";resultJson += "\"";resultJson += Account.ToString();resultJson += "\"";return resultJson;
}


public string get_PassWord_json(){
if(PassWord==null){return "";}string resultJson = "\"PassWord\":";resultJson += "\"";resultJson += PassWord.ToString();resultJson += "\"";return resultJson;
}


public string get_Email_json(){
if(Email==null){return "";}string resultJson = "\"Email\":";resultJson += "\"";resultJson += Email.ToString();resultJson += "\"";return resultJson;
}


public void set_Account_fromJson(LitJson.JsonData jsonObj){
Account= jsonObj.ToString();
}


public void set_PassWord_fromJson(LitJson.JsonData jsonObj){
PassWord= jsonObj.ToString();
}


public void set_Email_fromJson(LitJson.JsonData jsonObj){
Email= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(Account !=  null){
resultStr += get_Account_json();
}
else {}if(PassWord !=  null){
resultStr += ",";resultStr += get_PassWord_json();
}
else {}if(Email !=  null){
resultStr += ",";resultStr += get_Email_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["Account"] != null){
set_Account_fromJson(jsonObj["Account"]);
}
if(jsonObj["PassWord"] != null){
set_PassWord_fromJson(jsonObj["PassWord"]);
}
if(jsonObj["Email"] != null){
set_Email_fromJson(jsonObj["Email"]);
}
}
}
}
