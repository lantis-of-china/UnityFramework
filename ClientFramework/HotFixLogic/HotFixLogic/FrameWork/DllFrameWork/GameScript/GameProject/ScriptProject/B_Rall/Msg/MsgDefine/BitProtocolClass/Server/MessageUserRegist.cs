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
public class MessageUserRegist : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public String Account;
/// <summary>
///
/// <\summary>
public String PassWord;
/// <summary>
///
/// <\summary>
public String Email;
public MessageUserRegist(){}

public MessageUserRegist(String _Account, String _PassWord, String _Email){
this.Account = _Account;
this.PassWord = _PassWord;
this.Email = _Email;
}
private Byte[] get_Account_encoding(){
Byte[] outBuf = null;
String str = (String)Account;
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


private Byte[] get_PassWord_encoding(){
Byte[] outBuf = null;
String str = (String)PassWord;
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


private Byte[] get_Email_encoding(){
Byte[] outBuf = null;
String str = (String)Email;
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

private int set_Account_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_PassWord_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_Email_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_Account_fromBuf(sourceBuf,startOffset);
startOffset = set_PassWord_fromBuf(sourceBuf,startOffset);
startOffset = set_Email_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_Account_json(){
if(Account==null){return "";}String resultJson = "\"Account\":";resultJson += "\"";resultJson += Account.ToString();resultJson += "\"";return resultJson;
}


public String get_PassWord_json(){
if(PassWord==null){return "";}String resultJson = "\"PassWord\":";resultJson += "\"";resultJson += PassWord.ToString();resultJson += "\"";return resultJson;
}


public String get_Email_json(){
if(Email==null){return "";}String resultJson = "\"Email\":";resultJson += "\"";resultJson += Email.ToString();resultJson += "\"";return resultJson;
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

public override String SerializerJson(){
String resultStr = "{";if(Account !=  null){
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

public override void DeserializerJson(String json){
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
