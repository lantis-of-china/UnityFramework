// 此文件由协议导出插件自动生成
// ID : 00001]
//****�������ݽṹ****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�������ݽṹ
/// <\summary>
public class P_RequestInfo : LantisBitProtocolBase {
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///�Ա�
/// <\summary>
public Byte sex;
/// <summary>
///��Ա��
/// <\summary>
public String menberName;
/// <summary>
///ͷ���ַ
/// <\summary>
public String headUrl;
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
public P_RequestInfo(){}

public P_RequestInfo(Int32 _menberId, Byte _sex, String _menberName, String _headUrl, String _clubId){
this.menberId = _menberId;
this.sex = _sex;
this.menberName = _menberName;
this.headUrl = _headUrl;
this.clubId = _clubId;
}
private Byte[] get_menberId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)sex;
return outBuf;
}


private Byte[] get_menberName_encoding(){
Byte[] outBuf = null;
String str = (String)menberName;
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


private Byte[] get_headUrl_encoding(){
Byte[] outBuf = null;
String str = (String)headUrl;
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


private Byte[] get_clubId_encoding(){
Byte[] outBuf = null;
String str = (String)clubId;
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

private int set_menberId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberId = new Int32();
menberId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Byte();
sex = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_menberName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
menberName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_headUrl_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
headUrl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
headUrl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_clubId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
clubId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(menberId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
startOffset = set_menberName_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_menberId_json(){
if(menberId==null){return "";}String resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public String get_menberName_json(){
if(menberName==null){return "";}String resultJson = "\"menberName\":";resultJson += "\"";resultJson += menberName.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Byte.Parse(jsonObj.ToString());
}


public void set_menberName_fromJson(LitJson.JsonData jsonObj){
menberName= jsonObj.ToString();
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(menberId !=  null){
resultStr += get_menberId_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}if(menberName !=  null){
resultStr += ",";resultStr += get_menberName_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
if(jsonObj["menberName"] != null){
set_menberName_fromJson(jsonObj["menberName"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
}
}
}
