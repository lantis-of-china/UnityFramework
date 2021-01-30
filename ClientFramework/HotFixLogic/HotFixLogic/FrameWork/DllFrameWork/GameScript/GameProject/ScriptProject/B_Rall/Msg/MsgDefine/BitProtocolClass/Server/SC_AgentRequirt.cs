// 此文件由协议导出插件自动生成
// ID : 00001]
//****代理申请****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///代理申请
/// <\summary>
public class SC_AgentRequirt : CherishBitProtocolBase {
/// <summary>
///成功0失败 1成功
/// <\summary>
public byte result;
/// <summary>
///手机号
/// <\summary>
public string phoneNumber;
public SC_AgentRequirt(){}

public SC_AgentRequirt(byte _result, string _phoneNumber){
this.result = _result;
this.phoneNumber = _phoneNumber;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_phoneNumber_encoding(){
byte[] outBuf = null;
string str = (string)phoneNumber;
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
private int set_phoneNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
phoneNumber = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
phoneNumber = System.Text.Encoding.UTF8.GetString(byteArray);
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
}if(phoneNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_phoneNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_phoneNumber_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_phoneNumber_json(){
if(phoneNumber==null){return "";}string resultJson = "\"phoneNumber\":";resultJson += "\"";resultJson += phoneNumber.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_phoneNumber_fromJson(LitJson.JsonData jsonObj){
phoneNumber= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(phoneNumber !=  null){
resultStr += ",";resultStr += get_phoneNumber_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["phoneNumber"] != null){
set_phoneNumber_fromJson(jsonObj["phoneNumber"]);
}
}
}
}
