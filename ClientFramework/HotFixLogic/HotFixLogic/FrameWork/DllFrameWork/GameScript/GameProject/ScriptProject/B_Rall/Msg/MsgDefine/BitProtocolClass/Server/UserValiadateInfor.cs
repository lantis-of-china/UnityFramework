// 此文件由协议导出插件自动生成
// ID : 00002]

//****用户验证信息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户验证信息
/// <\summary>
public class UserValiadateInfor : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public string DatingNumber;
/// <summary>
///验证字段
/// <\summary>
public string ValidateGUID;
public UserValiadateInfor(){}

public UserValiadateInfor(string _DatingNumber, string _ValidateGUID){
this.DatingNumber = _DatingNumber;
this.ValidateGUID = _ValidateGUID;
}
private byte[] get_DatingNumber_encoding(){
byte[] outBuf = null;
string str = (string)DatingNumber;
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


private byte[] get_ValidateGUID_encoding(){
byte[] outBuf = null;
string str = (string)ValidateGUID;
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

private int set_DatingNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
DatingNumber = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
DatingNumber = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ValidateGUID_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ValidateGUID = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ValidateGUID = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(DatingNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_DatingNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ValidateGUID !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ValidateGUID_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_DatingNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_ValidateGUID_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_DatingNumber_json(){
if(DatingNumber==null){return "";}string resultJson = "\"DatingNumber\":";resultJson += "\"";resultJson += DatingNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_ValidateGUID_json(){
if(ValidateGUID==null){return "";}string resultJson = "\"ValidateGUID\":";resultJson += "\"";resultJson += ValidateGUID.ToString();resultJson += "\"";return resultJson;
}


public void set_DatingNumber_fromJson(LitJson.JsonData jsonObj){
DatingNumber= jsonObj.ToString();
}


public void set_ValidateGUID_fromJson(LitJson.JsonData jsonObj){
ValidateGUID= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(DatingNumber !=  null){
resultStr += get_DatingNumber_json();
}
else {}if(ValidateGUID !=  null){
resultStr += ",";resultStr += get_ValidateGUID_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["DatingNumber"] != null){
set_DatingNumber_fromJson(jsonObj["DatingNumber"]);
}
if(jsonObj["ValidateGUID"] != null){
set_ValidateGUID_fromJson(jsonObj["ValidateGUID"]);
}
}
}
}
