// 此文件由协议导出插件自动生成
// ID : 00001]
//****购买商品****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///购买商品
/// <\summary>
public class CS_BuyStore : CherishBitProtocolBase {
/// <summary>
///验证码
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///商品ID
/// <\summary>
public string id;
/// <summary>
///支付类型
/// <\summary>
public string payType;
/// <summary>
///代理号
/// <\summary>
public string agentCode;
public CS_BuyStore(){}

public CS_BuyStore(UserValiadateInfor _UserValiadate, string _id, string _payType, string _agentCode){
this.UserValiadate = _UserValiadate;
this.id = _id;
this.payType = _payType;
this.agentCode = _agentCode;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get_id_encoding(){
byte[] outBuf = null;
string str = (string)id;
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


private byte[] get_payType_encoding(){
byte[] outBuf = null;
string str = (string)payType;
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


private byte[] get_agentCode_encoding(){
byte[] outBuf = null;
string str = (string)agentCode;
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

private int set_UserValiadate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_id_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
id = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
id = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_payType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
payType = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
payType = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_agentCode_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
agentCode = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
agentCode = System.Text.Encoding.UTF8.GetString(byteArray);
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
}if(id !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_id_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(payType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_payType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(agentCode !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_agentCode_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_payType_fromBuf(sourceBuf,startOffset);
startOffset = set_agentCode_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get_id_json(){
if(id==null){return "";}string resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public string get_payType_json(){
if(payType==null){return "";}string resultJson = "\"payType\":";resultJson += "\"";resultJson += payType.ToString();resultJson += "\"";return resultJson;
}


public string get_agentCode_json(){
if(agentCode==null){return "";}string resultJson = "\"agentCode\":";resultJson += "\"";resultJson += agentCode.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= jsonObj.ToString();
}


public void set_payType_fromJson(LitJson.JsonData jsonObj){
payType= jsonObj.ToString();
}


public void set_agentCode_fromJson(LitJson.JsonData jsonObj){
agentCode= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(id !=  null){
resultStr += ",";resultStr += get_id_json();
}
else {}if(payType !=  null){
resultStr += ",";resultStr += get_payType_json();
}
else {}if(agentCode !=  null){
resultStr += ",";resultStr += get_agentCode_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["id"] != null){
set_id_fromJson(jsonObj["id"]);
}
if(jsonObj["payType"] != null){
set_payType_fromJson(jsonObj["payType"]);
}
if(jsonObj["agentCode"] != null){
set_agentCode_fromJson(jsonObj["agentCode"]);
}
}
}
}
