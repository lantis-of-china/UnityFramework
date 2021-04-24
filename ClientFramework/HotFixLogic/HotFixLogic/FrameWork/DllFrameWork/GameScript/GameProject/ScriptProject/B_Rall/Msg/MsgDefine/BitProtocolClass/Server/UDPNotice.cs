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
public class UDPNotice : LantisBitProtocolBase {
/// <summary>
///消息类型
/// <\summary>
public String type;
/// <summary>
///消息文本
/// <\summary>
public String content;
public UDPNotice(){}

public UDPNotice(String _type, String _content){
this.type = _type;
this.content = _content;
}
private Byte[] get_type_encoding(){
Byte[] outBuf = null;
String str = (String)type;
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


private Byte[] get_content_encoding(){
Byte[] outBuf = null;
String str = (String)content;
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

private int set_type_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_content_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_type_fromBuf(sourceBuf,startOffset);
startOffset = set_content_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_type_json(){
if(type==null){return "";}String resultJson = "\"type\":";resultJson += "\"";resultJson += type.ToString();resultJson += "\"";return resultJson;
}


public String get_content_json(){
if(content==null){return "";}String resultJson = "\"content\":";resultJson += "\"";resultJson += content.ToString();resultJson += "\"";return resultJson;
}


public void set_type_fromJson(LitJson.JsonData jsonObj){
type= jsonObj.ToString();
}


public void set_content_fromJson(LitJson.JsonData jsonObj){
content= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(type !=  null){
resultStr += get_type_json();
}
else {}if(content !=  null){
resultStr += ",";resultStr += get_content_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
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
