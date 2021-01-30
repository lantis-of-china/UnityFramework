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
public class SC_RealName : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public byte result;
/// <summary>
///实名认证
/// <\summary>
public string realName;
/// <summary>
///实名ID
/// <\summary>
public string realId;
/// <summary>
///手机号
/// <\summary>
public string realPhone;
public SC_RealName(){}

public SC_RealName(byte _result, string _realName, string _realId, string _realPhone){
this.result = _result;
this.realName = _realName;
this.realId = _realId;
this.realPhone = _realPhone;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_realName_encoding(){
byte[] outBuf = null;
string str = (string)realName;
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


private byte[] get_realId_encoding(){
byte[] outBuf = null;
string str = (string)realId;
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


private byte[] get_realPhone_encoding(){
byte[] outBuf = null;
string str = (string)realPhone;
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

private int set_result_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_realName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_realId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_realPhone_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_realName_fromBuf(sourceBuf,startOffset);
startOffset = set_realId_fromBuf(sourceBuf,startOffset);
startOffset = set_realPhone_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_realName_json(){
if(realName==null){return "";}string resultJson = "\"realName\":";resultJson += "\"";resultJson += realName.ToString();resultJson += "\"";return resultJson;
}


public string get_realId_json(){
if(realId==null){return "";}string resultJson = "\"realId\":";resultJson += "\"";resultJson += realId.ToString();resultJson += "\"";return resultJson;
}


public string get_realPhone_json(){
if(realPhone==null){return "";}string resultJson = "\"realPhone\":";resultJson += "\"";resultJson += realPhone.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
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

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
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

public override void DeserializerJson(string json){
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
