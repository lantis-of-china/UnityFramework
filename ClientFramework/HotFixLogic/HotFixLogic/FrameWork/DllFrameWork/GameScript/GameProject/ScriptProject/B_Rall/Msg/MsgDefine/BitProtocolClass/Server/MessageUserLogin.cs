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
public class MessageUserLogin : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public string AccountNumber;
/// <summary>
///
/// <\summary>
public string PassWord;
/// <summary>
///
/// <\summary>
public string Unionid;
/// <summary>
///
/// <\summary>
public bool IsPhone;
/// <summary>
///版本
/// <\summary>
public Int32 version;
/// <summary>
///昵称
/// <\summary>
public string nickName;
/// <summary>
///性别
/// <\summary>
public byte sex;
/// <summary>
///头像URL
/// <\summary>
public string headUrl;
/// <summary>
///1微信 2游客
/// <\summary>
public byte isWeiChat;
/// <summary>
///纬度
/// <\summary>
public float latitude;
/// <summary>
///经度
/// <\summary>
public float longitude;
public MessageUserLogin(){}

public MessageUserLogin(string _AccountNumber, string _PassWord, string _Unionid, bool _IsPhone, Int32 _version, string _nickName, byte _sex, string _headUrl, byte _isWeiChat, float _latitude, float _longitude){
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
private byte[] get_AccountNumber_encoding(){
byte[] outBuf = null;
string str = (string)AccountNumber;
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


private byte[] get_PassWord_encoding(){
byte[] outBuf = null;
string str = (string)PassWord;
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


private byte[] get_Unionid_encoding(){
byte[] outBuf = null;
string str = (string)Unionid;
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


private byte[] get_IsPhone_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((bool)IsPhone);
return outBuf;
}


private byte[] get_version_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)version);
return outBuf;
}


private byte[] get_nickName_encoding(){
byte[] outBuf = null;
string str = (string)nickName;
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


private byte[] get_sex_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)sex;
return outBuf;
}


private byte[] get_headUrl_encoding(){
byte[] outBuf = null;
string str = (string)headUrl;
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


private byte[] get_isWeiChat_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)isWeiChat;
return outBuf;
}


private byte[] get_latitude_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((float)latitude);
return outBuf;
}


private byte[] get_longitude_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((float)longitude);
return outBuf;
}

private int set_AccountNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_PassWord_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_Unionid_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_IsPhone_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
IsPhone = new bool();
IsPhone = BitConverter.ToBoolean(sourceBuf,curIndex);
curIndex += 1;
}return curIndex;
}
private int set_version_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
version = new Int32();
version = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_nickName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_sex_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new byte();
sex = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_headUrl_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_isWeiChat_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isWeiChat = new byte();
isWeiChat = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_latitude_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
latitude = new float();
latitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_longitude_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
longitude = new float();
longitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
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

public string get_AccountNumber_json(){
if(AccountNumber==null){return "";}string resultJson = "\"AccountNumber\":";resultJson += "\"";resultJson += AccountNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_PassWord_json(){
if(PassWord==null){return "";}string resultJson = "\"PassWord\":";resultJson += "\"";resultJson += PassWord.ToString();resultJson += "\"";return resultJson;
}


public string get_Unionid_json(){
if(Unionid==null){return "";}string resultJson = "\"Unionid\":";resultJson += "\"";resultJson += Unionid.ToString();resultJson += "\"";return resultJson;
}


public string get_IsPhone_json(){
if(IsPhone==null){return "";}string resultJson = "\"IsPhone\":";resultJson += "\"";resultJson += IsPhone.ToString();resultJson += "\"";return resultJson;
}


public string get_version_json(){
if(version==null){return "";}string resultJson = "\"version\":";resultJson += "\"";resultJson += version.ToString();resultJson += "\"";return resultJson;
}


public string get_nickName_json(){
if(nickName==null){return "";}string resultJson = "\"nickName\":";resultJson += "\"";resultJson += nickName.ToString();resultJson += "\"";return resultJson;
}


public string get_sex_json(){
if(sex==null){return "";}string resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public string get_headUrl_json(){
if(headUrl==null){return "";}string resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public string get_isWeiChat_json(){
if(isWeiChat==null){return "";}string resultJson = "\"isWeiChat\":";resultJson += "\"";resultJson += isWeiChat.ToString();resultJson += "\"";return resultJson;
}


public string get_latitude_json(){
if(latitude==null){return "";}string resultJson = "\"latitude\":";resultJson += "\"";resultJson += latitude.ToString();resultJson += "\"";return resultJson;
}


public string get_longitude_json(){
if(longitude==null){return "";}string resultJson = "\"longitude\":";resultJson += "\"";resultJson += longitude.ToString();resultJson += "\"";return resultJson;
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
IsPhone= bool.Parse(jsonObj.ToString());
}


public void set_version_fromJson(LitJson.JsonData jsonObj){
version= Int32.Parse(jsonObj.ToString());
}


public void set_nickName_fromJson(LitJson.JsonData jsonObj){
nickName= jsonObj.ToString();
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= byte.Parse(jsonObj.ToString());
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_isWeiChat_fromJson(LitJson.JsonData jsonObj){
isWeiChat= byte.Parse(jsonObj.ToString());
}


public void set_latitude_fromJson(LitJson.JsonData jsonObj){
latitude= float.Parse(jsonObj.ToString());
}


public void set_longitude_fromJson(LitJson.JsonData jsonObj){
longitude= float.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(AccountNumber !=  null){
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

public override void DeserializerJson(string json){
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
