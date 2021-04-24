// 此文件由协议导出插件自动生成
// ID : 00001]
//****��ȡ�û���Ϣ �Ƿ��ھ��ֲ���SC_GetUserInfo_MsgType = 20038****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��ȡ�û���Ϣ �Ƿ��ھ��ֲ���SC_GetUserInfo_MsgType = 20038
/// <\summary>
public class SC_GetUserInfo : LantisBitProtocolBase {
/// <summary>
///��� 0ʧ�� 1�ɹ�
/// <\summary>
public Byte result;
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
/// <summary>
///0���ھ��ֲ��� 1�ھ��ֲ���
/// <\summary>
public Byte inClub;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///����
/// <\summary>
public String nickName;
/// <summary>
///ͷ���ַ
/// <\summary>
public String headUrl;
/// <summary>
///�Ա� 0Ů 1��
/// <\summary>
public Byte sex;
public SC_GetUserInfo(){}

public SC_GetUserInfo(Byte _result, String _clubId, Byte _inClub, Int32 _menberId, String _nickName, String _headUrl, Byte _sex){
this.result = _result;
this.clubId = _clubId;
this.inClub = _inClub;
this.menberId = _menberId;
this.nickName = _nickName;
this.headUrl = _headUrl;
this.sex = _sex;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
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


private Byte[] get_inClub_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)inClub;
return outBuf;
}


private Byte[] get_menberId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private Byte[] get_nickName_encoding(){
Byte[] outBuf = null;
String str = (String)nickName;
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


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)sex;
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
private int set_inClub_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
inClub = new Byte();
inClub = sourceBuf[curIndex];
curIndex++;
}return curIndex;
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
private int set_nickName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
nickName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
nickName = System.Text.Encoding.UTF8.GetString(byteArray);
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
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Byte();
sex = sourceBuf[curIndex];
curIndex++;
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
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(inClub !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_inClub_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(nickName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_nickName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_inClub_fromBuf(sourceBuf,startOffset);
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_nickName_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_inClub_json(){
if(inClub==null){return "";}String resultJson = "\"inClub\":";resultJson += "\"";resultJson += inClub.ToString();resultJson += "\"";return resultJson;
}


public String get_menberId_json(){
if(menberId==null){return "";}String resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public String get_nickName_json(){
if(nickName==null){return "";}String resultJson = "\"nickName\":";resultJson += "\"";resultJson += nickName.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_inClub_fromJson(LitJson.JsonData jsonObj){
inClub= Byte.Parse(jsonObj.ToString());
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_nickName_fromJson(LitJson.JsonData jsonObj){
nickName= jsonObj.ToString();
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(inClub !=  null){
resultStr += ",";resultStr += get_inClub_json();
}
else {}if(menberId !=  null){
resultStr += ",";resultStr += get_menberId_json();
}
else {}if(nickName !=  null){
resultStr += ",";resultStr += get_nickName_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["inClub"] != null){
set_inClub_fromJson(jsonObj["inClub"]);
}
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["nickName"] != null){
set_nickName_fromJson(jsonObj["nickName"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
}
}
}
