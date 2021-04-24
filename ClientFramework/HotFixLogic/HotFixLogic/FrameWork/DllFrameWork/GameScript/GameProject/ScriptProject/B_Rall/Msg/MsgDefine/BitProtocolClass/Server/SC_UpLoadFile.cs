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
public class SC_UpLoadFile : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Byte result;
/// <summary>
///
/// <\summary>
public String strInfo;
/// <summary>
///和文件类型相关
/// <\summary>
public List<Byte> otherBuf;
public SC_UpLoadFile(){}

public SC_UpLoadFile(Byte _result, String _strInfo, List<Byte> _otherBuf){
this.result = _result;
this.strInfo = _strInfo;
this.otherBuf = _otherBuf;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
return outBuf;
}


private Byte[] get_strInfo_encoding(){
Byte[] outBuf = null;
String str = (String)strInfo;
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


private Byte[] get_otherBuf_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Byte> listbyte = (List<Byte>)otherBuf;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
Byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
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
private int set_strInfo_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_otherBuf_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
otherBuf = new List<Byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
Byte[] data = new Byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
otherBuf = new List<Byte>(data);
curIndex += listCount;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_strInfo_fromBuf(sourceBuf,startOffset);
startOffset = set_otherBuf_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_strInfo_json(){
if(strInfo==null){return "";}String resultJson = "\"strInfo\":";resultJson += "\"";resultJson += strInfo.ToString();resultJson += "\"";return resultJson;
}


public String get_otherBuf_json(){
if(otherBuf==null){return "";}String resultJson = "\"otherBuf\":";resultJson += "[";List<Byte> listObj = (List<Byte>)otherBuf;
for(int i = 0;i < listObj.Count;++i){
Byte item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_strInfo_fromJson(LitJson.JsonData jsonObj){
strInfo= jsonObj.ToString();
}


public void set_otherBuf_fromJson(LitJson.JsonData jsonObj){
otherBuf= new List<Byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
otherBuf.Add(Byte.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
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

public override void DeserializerJson(String json){
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
