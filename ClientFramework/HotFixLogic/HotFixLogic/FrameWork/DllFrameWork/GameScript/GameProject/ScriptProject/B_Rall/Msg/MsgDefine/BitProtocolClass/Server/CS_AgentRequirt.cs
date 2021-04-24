// 此文件由协议导出插件自动生成
// ID : 00001]
//****代理申请****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///代理申请
/// <\summary>
public class CS_AgentRequirt : LantisBitProtocolBase {
/// <summary>
///验证码
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///手机号
/// <\summary>
public String phoneNumber;
public CS_AgentRequirt(){}

public CS_AgentRequirt(UserValiadateInfor _UserValiadate, String _phoneNumber){
this.UserValiadate = _UserValiadate;
this.phoneNumber = _phoneNumber;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private Byte[] get_phoneNumber_encoding(){
Byte[] outBuf = null;
String str = (String)phoneNumber;
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

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_phoneNumber_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
phoneNumber = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
phoneNumber = System.Text.Encoding.UTF8.GetString(byteArray);
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
}if(phoneNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_phoneNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_phoneNumber_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_phoneNumber_json(){
if(phoneNumber==null){return "";}String resultJson = "\"phoneNumber\":";resultJson += "\"";resultJson += phoneNumber.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_phoneNumber_fromJson(LitJson.JsonData jsonObj){
phoneNumber= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(phoneNumber !=  null){
resultStr += ",";resultStr += get_phoneNumber_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["phoneNumber"] != null){
set_phoneNumber_fromJson(jsonObj["phoneNumber"]);
}
}
}
}
