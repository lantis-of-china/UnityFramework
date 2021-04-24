// 此文件由协议导出插件自动生成
// ID : 00005]

//****用户登陆****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户登陆
/// <\summary>
public class MessageUserLogin : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public String AccountNumber;
/// <summary>
///
/// <\summary>
public String PassWord;
/// <summary>
///
/// <\summary>
public String Unionid;
/// <summary>
///
/// <\summary>
public Boolean IsPhone;
/// <summary>
///版本
/// <\summary>
public Int32 version;
/// <summary>
///昵称
/// <\summary>
public String nickName;
/// <summary>
///性别
/// <\summary>
public Byte sex;
/// <summary>
///头像URL
/// <\summary>
public String headUrl;
/// <summary>
///1微信 2游客
/// <\summary>
public Byte isWeiChat;
/// <summary>
///纬度
/// <\summary>
public Single latitude;
/// <summary>
///经度
/// <\summary>
public Single longitude;
public MessageUserLogin(){}

public MessageUserLogin(String _AccountNumber, String _PassWord, String _Unionid, Boolean _IsPhone, Int32 _version, String _nickName, Byte _sex, String _headUrl, Byte _isWeiChat, Single _latitude, Single _longitude){
this.AccountNumber = _AccountNumber;
this.PassWord = _PassWord;
this.Unionid = _Unionid;
this.IsPhone = _IsPhone;
this.version = _version;
this.nickName = _nickName;
this.sex = _sex;
this.headUrl = _headUrl;
this.isWeiChat = _isWeiChat;
this.latitude = _latitude;
this.longitude = _longitude;
}
private Byte[] get_AccountNumber_encoding(){
Byte[] outBuf = null;
String str = (String)AccountNumber;
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


private Byte[] get_PassWord_encoding(){
Byte[] outBuf = null;
String str = (String)PassWord;
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


private Byte[] get_Unionid_encoding(){
Byte[] outBuf = null;
String str = (String)Unionid;
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


private Byte[] get_IsPhone_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Boolean)IsPhone);
return outBuf;
}


private Byte[] get_version_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)version);
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


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)sex;
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


private Byte[] get_isWeiChat_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)isWeiChat;
return outBuf;
}


private Byte[] get_latitude_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)latitude);
return outBuf;
}


private Byte[] get_longitude_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)longitude);
return outBuf;
}

private int set_AccountNumber_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
AccountNumber = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
AccountNumber = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_PassWord_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
PassWord = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
PassWord = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_Unionid_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Unionid = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
Unionid = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_IsPhone_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
IsPhone = new Boolean();
IsPhone = BitConverter.ToBoolean(sourceBuf,curIndex);
curIndex += 1;
}return curIndex;
}
private int set_version_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
version = new Int32();
version = BitConverter.ToInt32(sourceBuf,curIndex);
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
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Byte();
sex = sourceBuf[curIndex];
curIndex++;
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
private int set_isWeiChat_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isWeiChat = new Byte();
isWeiChat = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_latitude_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
latitude = new Single();
latitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_longitude_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
longitude = new Single();
longitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(AccountNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_AccountNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(PassWord !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_PassWord_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Unionid !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Unionid_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(IsPhone !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_IsPhone_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(version !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_version_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(nickName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_nickName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(isWeiChat !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_isWeiChat_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(latitude !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_latitude_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(longitude !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_longitude_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_AccountNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_PassWord_fromBuf(sourceBuf,startOffset);
startOffset = set_Unionid_fromBuf(sourceBuf,startOffset);
startOffset = set_IsPhone_fromBuf(sourceBuf,startOffset);
startOffset = set_version_fromBuf(sourceBuf,startOffset);
startOffset = set_nickName_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_isWeiChat_fromBuf(sourceBuf,startOffset);
startOffset = set_latitude_fromBuf(sourceBuf,startOffset);
startOffset = set_longitude_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_AccountNumber_json(){
if(AccountNumber==null){return "";}String resultJson = "\"AccountNumber\":";resultJson += "\"";resultJson += AccountNumber.ToString();resultJson += "\"";return resultJson;
}


public String get_PassWord_json(){
if(PassWord==null){return "";}String resultJson = "\"PassWord\":";resultJson += "\"";resultJson += PassWord.ToString();resultJson += "\"";return resultJson;
}


public String get_Unionid_json(){
if(Unionid==null){return "";}String resultJson = "\"Unionid\":";resultJson += "\"";resultJson += Unionid.ToString();resultJson += "\"";return resultJson;
}


public String get_IsPhone_json(){
if(IsPhone==null){return "";}String resultJson = "\"IsPhone\":";resultJson += "\"";resultJson += IsPhone.ToString();resultJson += "\"";return resultJson;
}


public String get_version_json(){
if(version==null){return "";}String resultJson = "\"version\":";resultJson += "\"";resultJson += version.ToString();resultJson += "\"";return resultJson;
}


public String get_nickName_json(){
if(nickName==null){return "";}String resultJson = "\"nickName\":";resultJson += "\"";resultJson += nickName.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_isWeiChat_json(){
if(isWeiChat==null){return "";}String resultJson = "\"isWeiChat\":";resultJson += "\"";resultJson += isWeiChat.ToString();resultJson += "\"";return resultJson;
}


public String get_latitude_json(){
if(latitude==null){return "";}String resultJson = "\"latitude\":";resultJson += "\"";resultJson += latitude.ToString();resultJson += "\"";return resultJson;
}


public String get_longitude_json(){
if(longitude==null){return "";}String resultJson = "\"longitude\":";resultJson += "\"";resultJson += longitude.ToString();resultJson += "\"";return resultJson;
}


public void set_AccountNumber_fromJson(LitJson.JsonData jsonObj){
AccountNumber= jsonObj.ToString();
}


public void set_PassWord_fromJson(LitJson.JsonData jsonObj){
PassWord= jsonObj.ToString();
}


public void set_Unionid_fromJson(LitJson.JsonData jsonObj){
Unionid= jsonObj.ToString();
}


public void set_IsPhone_fromJson(LitJson.JsonData jsonObj){
IsPhone= Boolean.Parse(jsonObj.ToString());
}


public void set_version_fromJson(LitJson.JsonData jsonObj){
version= Int32.Parse(jsonObj.ToString());
}


public void set_nickName_fromJson(LitJson.JsonData jsonObj){
nickName= jsonObj.ToString();
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Byte.Parse(jsonObj.ToString());
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_isWeiChat_fromJson(LitJson.JsonData jsonObj){
isWeiChat= Byte.Parse(jsonObj.ToString());
}


public void set_latitude_fromJson(LitJson.JsonData jsonObj){
latitude= Single.Parse(jsonObj.ToString());
}


public void set_longitude_fromJson(LitJson.JsonData jsonObj){
longitude= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(AccountNumber !=  null){
resultStr += get_AccountNumber_json();
}
else {}if(PassWord !=  null){
resultStr += ",";resultStr += get_PassWord_json();
}
else {}if(Unionid !=  null){
resultStr += ",";resultStr += get_Unionid_json();
}
else {}if(IsPhone !=  null){
resultStr += ",";resultStr += get_IsPhone_json();
}
else {}if(version !=  null){
resultStr += ",";resultStr += get_version_json();
}
else {}if(nickName !=  null){
resultStr += ",";resultStr += get_nickName_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(isWeiChat !=  null){
resultStr += ",";resultStr += get_isWeiChat_json();
}
else {}if(latitude !=  null){
resultStr += ",";resultStr += get_latitude_json();
}
else {}if(longitude !=  null){
resultStr += ",";resultStr += get_longitude_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["AccountNumber"] != null){
set_AccountNumber_fromJson(jsonObj["AccountNumber"]);
}
if(jsonObj["PassWord"] != null){
set_PassWord_fromJson(jsonObj["PassWord"]);
}
if(jsonObj["Unionid"] != null){
set_Unionid_fromJson(jsonObj["Unionid"]);
}
if(jsonObj["IsPhone"] != null){
set_IsPhone_fromJson(jsonObj["IsPhone"]);
}
if(jsonObj["version"] != null){
set_version_fromJson(jsonObj["version"]);
}
if(jsonObj["nickName"] != null){
set_nickName_fromJson(jsonObj["nickName"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["isWeiChat"] != null){
set_isWeiChat_fromJson(jsonObj["isWeiChat"]);
}
if(jsonObj["latitude"] != null){
set_latitude_fromJson(jsonObj["latitude"]);
}
if(jsonObj["longitude"] != null){
set_longitude_fromJson(jsonObj["longitude"]);
}
}
}
}
