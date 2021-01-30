// 此文件由协议导出插件自动生成
// ID : 00001]
//****上传文件****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///上传文件
/// <\summary>
public class CS_UpLoadFile : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///文件名
/// <\summary>
public string fileName;
/// <summary>
///文件类型0-俱乐部图片 1-头像图片
/// <\summary>
public byte fileType;
/// <summary>
///和文件类型相关
/// <\summary>
public List<byte> otherBuf;
/// <summary>
///文件数据
/// <\summary>
public List<byte> fileBuf;
public CS_UpLoadFile(){}

public CS_UpLoadFile(UserValiadateInfor _UserValiadate, string _fileName, byte _fileType, List<byte> _otherBuf, List<byte> _fileBuf){
this.UserValiadate = _UserValiadate;
this.fileName = _fileName;
this.fileType = _fileType;
this.otherBuf = _otherBuf;
this.fileBuf = _fileBuf;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get_fileName_encoding(){
byte[] outBuf = null;
string str = (string)fileName;
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


private byte[] get_fileType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)fileType;
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


private byte[] get_fileBuf_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<byte> listbyte = (List<byte>)fileBuf;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_UserValiadate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_fileName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
fileName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
fileName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_fileType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
fileType = new byte();
fileType = sourceBuf[curIndex];
curIndex++;
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
private int set_fileBuf_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
fileBuf = new List<byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
byte[] data = new byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
fileBuf = new List<byte>(data);
curIndex += listCount;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(fileName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_fileName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(fileType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_fileType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(otherBuf !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_otherBuf_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(fileBuf !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_fileBuf_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_fileName_fromBuf(sourceBuf,startOffset);
startOffset = set_fileType_fromBuf(sourceBuf,startOffset);
startOffset = set_otherBuf_fromBuf(sourceBuf,startOffset);
startOffset = set_fileBuf_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get_fileName_json(){
if(fileName==null){return "";}string resultJson = "\"fileName\":";resultJson += "\"";resultJson += fileName.ToString();resultJson += "\"";return resultJson;
}


public string get_fileType_json(){
if(fileType==null){return "";}string resultJson = "\"fileType\":";resultJson += "\"";resultJson += fileType.ToString();resultJson += "\"";return resultJson;
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


public string get_fileBuf_json(){
if(fileBuf==null){return "";}string resultJson = "\"fileBuf\":";resultJson += "[";List<byte> listObj = (List<byte>)fileBuf;
for(int i = 0;i < listObj.Count;++i){
byte item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_fileName_fromJson(LitJson.JsonData jsonObj){
fileName= jsonObj.ToString();
}


public void set_fileType_fromJson(LitJson.JsonData jsonObj){
fileType= byte.Parse(jsonObj.ToString());
}


public void set_otherBuf_fromJson(LitJson.JsonData jsonObj){
otherBuf= new List<byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
otherBuf.Add(byte.Parse(jsonItem.ToString()));}

}


public void set_fileBuf_fromJson(LitJson.JsonData jsonObj){
fileBuf= new List<byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
fileBuf.Add(byte.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(fileName !=  null){
resultStr += ",";resultStr += get_fileName_json();
}
else {}if(fileType !=  null){
resultStr += ",";resultStr += get_fileType_json();
}
else {}if(otherBuf !=  null){
resultStr += ",";resultStr += get_otherBuf_json();
}
else {}if(fileBuf !=  null){
resultStr += ",";resultStr += get_fileBuf_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["fileName"] != null){
set_fileName_fromJson(jsonObj["fileName"]);
}
if(jsonObj["fileType"] != null){
set_fileType_fromJson(jsonObj["fileType"]);
}
if(jsonObj["otherBuf"] != null){
set_otherBuf_fromJson(jsonObj["otherBuf"]);
}
if(jsonObj["fileBuf"] != null){
set_fileBuf_fromJson(jsonObj["fileBuf"]);
}
}
}
}
