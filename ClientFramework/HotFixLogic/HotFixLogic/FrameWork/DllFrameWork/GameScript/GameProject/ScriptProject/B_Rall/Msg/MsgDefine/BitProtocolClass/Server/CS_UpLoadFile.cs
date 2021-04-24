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
public class CS_UpLoadFile : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///文件名
/// <\summary>
public String fileName;
/// <summary>
///文件类型0-俱乐部图片 1-头像图片
/// <\summary>
public Byte fileType;
/// <summary>
///和文件类型相关
/// <\summary>
public List<Byte> otherBuf;
/// <summary>
///文件数据
/// <\summary>
public List<Byte> fileBuf;
public CS_UpLoadFile(){}

public CS_UpLoadFile(UserValiadateInfor _UserValiadate, String _fileName, Byte _fileType, List<Byte> _otherBuf, List<Byte> _fileBuf){
this.UserValiadate = _UserValiadate;
this.fileName = _fileName;
this.fileType = _fileType;
this.otherBuf = _otherBuf;
this.fileBuf = _fileBuf;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private Byte[] get_fileName_encoding(){
Byte[] outBuf = null;
String str = (String)fileName;
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


private Byte[] get_fileType_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)fileType;
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


private Byte[] get_fileBuf_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Byte> listbyte = (List<Byte>)fileBuf;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
Byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_fileName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_fileType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
fileType = new Byte();
fileType = sourceBuf[curIndex];
curIndex++;
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
private int set_fileBuf_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
fileBuf = new List<Byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
Byte[] data = new Byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
fileBuf = new List<Byte>(data);
curIndex += listCount;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_fileName_fromBuf(sourceBuf,startOffset);
startOffset = set_fileType_fromBuf(sourceBuf,startOffset);
startOffset = set_otherBuf_fromBuf(sourceBuf,startOffset);
startOffset = set_fileBuf_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_fileName_json(){
if(fileName==null){return "";}String resultJson = "\"fileName\":";resultJson += "\"";resultJson += fileName.ToString();resultJson += "\"";return resultJson;
}


public String get_fileType_json(){
if(fileType==null){return "";}String resultJson = "\"fileType\":";resultJson += "\"";resultJson += fileType.ToString();resultJson += "\"";return resultJson;
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


public String get_fileBuf_json(){
if(fileBuf==null){return "";}String resultJson = "\"fileBuf\":";resultJson += "[";List<Byte> listObj = (List<Byte>)fileBuf;
for(int i = 0;i < listObj.Count;++i){
Byte item = listObj[i];
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
fileType= Byte.Parse(jsonObj.ToString());
}


public void set_otherBuf_fromJson(LitJson.JsonData jsonObj){
otherBuf= new List<Byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
otherBuf.Add(Byte.Parse(jsonItem.ToString()));}

}


public void set_fileBuf_fromJson(LitJson.JsonData jsonObj){
fileBuf= new List<Byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
fileBuf.Add(Byte.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
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

public override void DeserializerJson(String json){
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
