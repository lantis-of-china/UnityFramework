// 此文件由协议导出插件自动生成
// ID : 10001]

//****俱乐部推送通知****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///俱乐部推送通知
/// <\summary>
public class UDPNotice : CherishBitProtocolBase {
/// <summary>
///消息类型
/// <\summary>
public string type;
/// <summary>
///消息文本
/// <\summary>
public string content;
public UDPNotice(){}

public UDPNotice(string _type, string _content){
this.type = _type;
this.content = _content;
}
private byte[] get_type_encoding(){
byte[] outBuf = null;
string str = (string)type;
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


private byte[] get_content_encoding(){
byte[] outBuf = null;
string str = (string)content;
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

private int set_type_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
type = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
type = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_content_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
content = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
content = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(type !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_type_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(content !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_content_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_type_fromBuf(sourceBuf,startOffset);
startOffset = set_content_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_type_json(){
if(type==null){return "";}string resultJson = "\"type\":";resultJson += "\"";resultJson += type.ToString();resultJson += "\"";return resultJson;
}


public string get_content_json(){
if(content==null){return "";}string resultJson = "\"content\":";resultJson += "\"";resultJson += content.ToString();resultJson += "\"";return resultJson;
}


public void set_type_fromJson(LitJson.JsonData jsonObj){
type= jsonObj.ToString();
}


public void set_content_fromJson(LitJson.JsonData jsonObj){
content= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(type !=  null){
resultStr += get_type_json();
}
else {}if(content !=  null){
resultStr += ",";resultStr += get_content_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["type"] != null){
set_type_fromJson(jsonObj["type"]);
}
if(jsonObj["content"] != null){
set_content_fromJson(jsonObj["content"]);
}
}
}
}
