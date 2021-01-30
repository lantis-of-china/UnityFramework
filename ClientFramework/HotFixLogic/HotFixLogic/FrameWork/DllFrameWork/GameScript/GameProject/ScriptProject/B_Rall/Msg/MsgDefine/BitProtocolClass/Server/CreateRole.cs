// 此文件由协议导出插件自动生成
// ID : 00009]
   
//****创建角色****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///创建角色
/// <\summary>
public class CreateRole : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///
/// <\summary>
public string _name;
/// <summary>
///
/// <\summary>
public Int16 _sex;
/// <summary>
///
/// <\summary>
public Int16 _wuXing;
public CreateRole(){}

public CreateRole(UserValiadateInfor _UserValiadate, string __name, Int16 __sex, Int16 __wuXing){
this.UserValiadate = _UserValiadate;
this._name = __name;
this._sex = __sex;
this._wuXing = __wuXing;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get__name_encoding(){
byte[] outBuf = null;
string str = (string)_name;
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


private byte[] get__sex_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((short)_sex);
return outBuf;
}


private byte[] get__wuXing_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((short)_wuXing);
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
private int set__name_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_name = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
_name = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set__sex_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_sex = new Int16();
_sex = BitConverter.ToInt16(sourceBuf,curIndex);
curIndex += 2;
}return curIndex;
}
private int set__wuXing_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_wuXing = new Int16();
_wuXing = BitConverter.ToInt16(sourceBuf,curIndex);
curIndex += 2;
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
}if(_name !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__name_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(_sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(_wuXing !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__wuXing_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set__name_fromBuf(sourceBuf,startOffset);
startOffset = set__sex_fromBuf(sourceBuf,startOffset);
startOffset = set__wuXing_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get__name_json(){
if(_name==null){return "";}string resultJson = "\"_name\":";resultJson += "\"";resultJson += _name.ToString();resultJson += "\"";return resultJson;
}


public string get__sex_json(){
if(_sex==null){return "";}string resultJson = "\"_sex\":";resultJson += "\"";resultJson += _sex.ToString();resultJson += "\"";return resultJson;
}


public string get__wuXing_json(){
if(_wuXing==null){return "";}string resultJson = "\"_wuXing\":";resultJson += "\"";resultJson += _wuXing.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set__name_fromJson(LitJson.JsonData jsonObj){
_name= jsonObj.ToString();
}


public void set__sex_fromJson(LitJson.JsonData jsonObj){
_sex= Int16.Parse(jsonObj.ToString());
}


public void set__wuXing_fromJson(LitJson.JsonData jsonObj){
_wuXing= Int16.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(_name !=  null){
resultStr += ",";resultStr += get__name_json();
}
else {}if(_sex !=  null){
resultStr += ",";resultStr += get__sex_json();
}
else {}if(_wuXing !=  null){
resultStr += ",";resultStr += get__wuXing_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["_name"] != null){
set__name_fromJson(jsonObj["_name"]);
}
if(jsonObj["_sex"] != null){
set__sex_fromJson(jsonObj["_sex"]);
}
if(jsonObj["_wuXing"] != null){
set__wuXing_fromJson(jsonObj["_wuXing"]);
}
}
}
}
