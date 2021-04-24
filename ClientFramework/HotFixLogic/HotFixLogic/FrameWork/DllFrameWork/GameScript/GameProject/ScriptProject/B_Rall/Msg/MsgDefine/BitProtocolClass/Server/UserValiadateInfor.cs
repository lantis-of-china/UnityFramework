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
public class UserValiadateInfor : LantisBitProtocolBase {
/// <summary>
///用户键值
/// <\summary>
public String DatingNumber;
/// <summary>
///验证字段
/// <\summary>
public String ValidateGUID;
public UserValiadateInfor(){}

public UserValiadateInfor(String _DatingNumber, String _ValidateGUID){
this.DatingNumber = _DatingNumber;
this.ValidateGUID = _ValidateGUID;
}
private Byte[] get_DatingNumber_encoding(){
Byte[] outBuf = null;
String str = (String)DatingNumber;
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


private Byte[] get_ValidateGUID_encoding(){
Byte[] outBuf = null;
String str = (String)ValidateGUID;
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
private int set_ValidateGUID_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_DatingNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_ValidateGUID_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_DatingNumber_json(){
if(DatingNumber==null){return "";}String resultJson = "\"DatingNumber\":";resultJson += "\"";resultJson += DatingNumber.ToString();resultJson += "\"";return resultJson;
}


public String get_ValidateGUID_json(){
if(ValidateGUID==null){return "";}String resultJson = "\"ValidateGUID\":";resultJson += "\"";resultJson += ValidateGUID.ToString();resultJson += "\"";return resultJson;
}


public void set_DatingNumber_fromJson(LitJson.JsonData jsonObj){
DatingNumber= jsonObj.ToString();
}


public void set_ValidateGUID_fromJson(LitJson.JsonData jsonObj){
ValidateGUID= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(DatingNumber !=  null){
resultStr += get_DatingNumber_json();
}
else {}if(ValidateGUID !=  null){
resultStr += ",";resultStr += get_ValidateGUID_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
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
