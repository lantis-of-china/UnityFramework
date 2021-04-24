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
public class SC_RealName : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Byte result;
/// <summary>
///实名认证
/// <\summary>
public String realName;
/// <summary>
///实名ID
/// <\summary>
public String realId;
/// <summary>
///手机号
/// <\summary>
public String realPhone;
public SC_RealName(){}

public SC_RealName(Byte _result, String _realName, String _realId, String _realPhone){
this.result = _result;
this.realName = _realName;
this.realId = _realId;
this.realPhone = _realPhone;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
return outBuf;
}


private Byte[] get_realName_encoding(){
Byte[] outBuf = null;
String str = (String)realName;
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


private Byte[] get_realId_encoding(){
Byte[] outBuf = null;
String str = (String)realId;
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


private Byte[] get_realPhone_encoding(){
Byte[] outBuf = null;
String str = (String)realPhone;
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

private int set_result_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new Byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_realName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
realName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
realName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_realId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
realId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
realId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_realPhone_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
realPhone = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
realPhone = System.Text.Encoding.UTF8.GetString(byteArray);
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
}if(realName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_realName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(realId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_realId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(realPhone !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_realPhone_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_realName_fromBuf(sourceBuf,startOffset);
startOffset = set_realId_fromBuf(sourceBuf,startOffset);
startOffset = set_realPhone_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_realName_json(){
if(realName==null){return "";}String resultJson = "\"realName\":";resultJson += "\"";resultJson += realName.ToString();resultJson += "\"";return resultJson;
}


public String get_realId_json(){
if(realId==null){return "";}String resultJson = "\"realId\":";resultJson += "\"";resultJson += realId.ToString();resultJson += "\"";return resultJson;
}


public String get_realPhone_json(){
if(realPhone==null){return "";}String resultJson = "\"realPhone\":";resultJson += "\"";resultJson += realPhone.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_realName_fromJson(LitJson.JsonData jsonObj){
realName= jsonObj.ToString();
}


public void set_realId_fromJson(LitJson.JsonData jsonObj){
realId= jsonObj.ToString();
}


public void set_realPhone_fromJson(LitJson.JsonData jsonObj){
realPhone= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(realName !=  null){
resultStr += ",";resultStr += get_realName_json();
}
else {}if(realId !=  null){
resultStr += ",";resultStr += get_realId_json();
}
else {}if(realPhone !=  null){
resultStr += ",";resultStr += get_realPhone_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["realName"] != null){
set_realName_fromJson(jsonObj["realName"]);
}
if(jsonObj["realId"] != null){
set_realId_fromJson(jsonObj["realId"]);
}
if(jsonObj["realPhone"] != null){
set_realPhone_fromJson(jsonObj["realPhone"]);
}
}
}
}
