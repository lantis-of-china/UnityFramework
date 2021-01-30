// 此文件由协议导出插件自动生成
// ID : 10003]
   
//****用户登陆验证结果****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户登陆验证结果
/// <\summary>
public class MTCS_USER_LOGIN_VALIDATE_BACK : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public bool ValidatePass;
/// <summary>
///
/// <\summary>
public string DatingNumber;
/// <summary>
///
/// <\summary>
public string ValidateGUID;
/// <summary>
///
/// <\summary>
public UserValiadateInforWarp ValiadateInfor;
public MTCS_USER_LOGIN_VALIDATE_BACK(){}

public MTCS_USER_LOGIN_VALIDATE_BACK(bool _ValidatePass, string _DatingNumber, string _ValidateGUID, UserValiadateInforWarp _ValiadateInfor){
this.ValidatePass = _ValidatePass;
this.DatingNumber = _DatingNumber;
this.ValidateGUID = _ValidateGUID;
this.ValiadateInfor = _ValiadateInfor;
}
private byte[] get_ValidatePass_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((bool)ValidatePass);
return outBuf;
}


private byte[] get_DatingNumber_encoding(){
byte[] outBuf = null;
string str = (string)DatingNumber;
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


private byte[] get_ValidateGUID_encoding(){
byte[] outBuf = null;
string str = (string)ValidateGUID;
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


private byte[] get_ValiadateInfor_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)ValiadateInfor).Serializer();
return outBuf;
}

private int set_ValidatePass_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ValidatePass = new bool();
ValidatePass = BitConverter.ToBoolean(sourceBuf,curIndex);
curIndex += 1;
}return curIndex;
}
private int set_DatingNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
DatingNumber = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
DatingNumber = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ValidateGUID_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ValidateGUID = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ValidateGUID = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ValiadateInfor_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ValiadateInfor = new UserValiadateInforWarp();
curIndex = ValiadateInfor.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(ValidatePass !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ValidatePass_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(DatingNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_DatingNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ValidateGUID !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ValidateGUID_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ValiadateInfor !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ValiadateInfor_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_ValidatePass_fromBuf(sourceBuf,startOffset);
startOffset = set_DatingNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_ValidateGUID_fromBuf(sourceBuf,startOffset);
startOffset = set_ValiadateInfor_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_ValidatePass_json(){
if(ValidatePass==null){return "";}string resultJson = "\"ValidatePass\":";resultJson += "\"";resultJson += ValidatePass.ToString();resultJson += "\"";return resultJson;
}


public string get_DatingNumber_json(){
if(DatingNumber==null){return "";}string resultJson = "\"DatingNumber\":";resultJson += "\"";resultJson += DatingNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_ValidateGUID_json(){
if(ValidateGUID==null){return "";}string resultJson = "\"ValidateGUID\":";resultJson += "\"";resultJson += ValidateGUID.ToString();resultJson += "\"";return resultJson;
}


public string get_ValiadateInfor_json(){
if(ValiadateInfor==null){return "";}string resultJson = "\"ValiadateInfor\":";resultJson += ((CherishBitProtocolBase)ValiadateInfor).SerializerJson();return resultJson;
}


public void set_ValidatePass_fromJson(LitJson.JsonData jsonObj){
ValidatePass= bool.Parse(jsonObj.ToString());
}


public void set_DatingNumber_fromJson(LitJson.JsonData jsonObj){
DatingNumber= jsonObj.ToString();
}


public void set_ValidateGUID_fromJson(LitJson.JsonData jsonObj){
ValidateGUID= jsonObj.ToString();
}


public void set_ValiadateInfor_fromJson(LitJson.JsonData jsonObj){
ValiadateInfor= new UserValiadateInforWarp();
ValiadateInfor.DeserializerJson(jsonObj.ToJson());}

public override string SerializerJson(){
string resultStr = "{";if(ValidatePass !=  null){
resultStr += get_ValidatePass_json();
}
else {}if(DatingNumber !=  null){
resultStr += ",";resultStr += get_DatingNumber_json();
}
else {}if(ValidateGUID !=  null){
resultStr += ",";resultStr += get_ValidateGUID_json();
}
else {}if(ValiadateInfor !=  null){
resultStr += ",";resultStr += get_ValiadateInfor_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["ValidatePass"] != null){
set_ValidatePass_fromJson(jsonObj["ValidatePass"]);
}
if(jsonObj["DatingNumber"] != null){
set_DatingNumber_fromJson(jsonObj["DatingNumber"]);
}
if(jsonObj["ValidateGUID"] != null){
set_ValidateGUID_fromJson(jsonObj["ValidateGUID"]);
}
if(jsonObj["ValiadateInfor"] != null){
set_ValiadateInfor_fromJson(jsonObj["ValiadateInfor"]);
}
}
}
}
