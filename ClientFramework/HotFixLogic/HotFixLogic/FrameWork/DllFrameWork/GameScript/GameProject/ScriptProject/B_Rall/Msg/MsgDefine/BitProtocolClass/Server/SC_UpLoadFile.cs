// 此文件由协议导出插件自动生成
// ID : 00001]
//****上传成功****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///上传成功
/// <\summary>
public class SC_UpLoadFile : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public byte result;
/// <summary>
///
/// <\summary>
public string strInfo;
/// <summary>
///和文件类型相关
/// <\summary>
public List<byte> otherBuf;
public SC_UpLoadFile(){}

public SC_UpLoadFile(byte _result, string _strInfo, List<byte> _otherBuf){
this.result = _result;
this.strInfo = _strInfo;
this.otherBuf = _otherBuf;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_strInfo_encoding(){
byte[] outBuf = null;
string str = (string)strInfo;
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


private byte[] get_otherBuf_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<byte> listbyte = (List<byte>)otherBuf;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
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
private int set_strInfo_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
strInfo = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
strInfo = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_otherBuf_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
otherBuf = new List<byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
byte[] data = new byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
otherBuf = new List<byte>(data);
curIndex += listCount;
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
}if(strInfo !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_strInfo_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(otherBuf !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_otherBuf_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_strInfo_fromBuf(sourceBuf,startOffset);
startOffset = set_otherBuf_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_strInfo_json(){
if(strInfo==null){return "";}string resultJson = "\"strInfo\":";resultJson += "\"";resultJson += strInfo.ToString();resultJson += "\"";return resultJson;
}


public string get_otherBuf_json(){
if(otherBuf==null){return "";}string resultJson = "\"otherBuf\":";resultJson += "[";List<byte> listObj = (List<byte>)otherBuf;
for(int i = 0;i < listObj.Count;++i){
byte item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_strInfo_fromJson(LitJson.JsonData jsonObj){
strInfo= jsonObj.ToString();
}


public void set_otherBuf_fromJson(LitJson.JsonData jsonObj){
otherBuf= new List<byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
otherBuf.Add(byte.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(strInfo !=  null){
resultStr += ",";resultStr += get_strInfo_json();
}
else {}if(otherBuf !=  null){
resultStr += ",";resultStr += get_otherBuf_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["strInfo"] != null){
set_strInfo_fromJson(jsonObj["strInfo"]);
}
if(jsonObj["otherBuf"] != null){
set_otherBuf_fromJson(jsonObj["otherBuf"]);
}
}
}
}
