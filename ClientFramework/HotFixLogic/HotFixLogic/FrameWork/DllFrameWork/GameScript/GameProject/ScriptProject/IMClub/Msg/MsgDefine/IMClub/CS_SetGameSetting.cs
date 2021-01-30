﻿// 此文件由协议导出插件自动生成
// ID : 00001]
//****������Ϸ����C2S_SetGameSetting_MsgType = 20019****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///������Ϸ����C2S_SetGameSetting_MsgType = 20019
/// <\summary>
public class CS_SetGameSetting : CherishBitProtocolBase {
/// <summary>
///��Ϣ��֤
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///���ֲ�ID
/// <\summary>
public string clubId;
/// <summary>
///��Ϸ����
/// <\summary>
public byte gameType;
/// <summary>
///����
/// <\summary>
public byte roomValue;
/// <summary>
///���ò����б�
/// <\summary>
public List<Int32> paramars;
public CS_SetGameSetting(){}

public CS_SetGameSetting(UserValiadateInfor _UserValiadate, string _clubId, byte _gameType, byte _roomValue, List<Int32> _paramars){
this.UserValiadate = _UserValiadate;
this.clubId = _clubId;
this.gameType = _gameType;
this.roomValue = _roomValue;
this.paramars = _paramars;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get_clubId_encoding(){
byte[] outBuf = null;
string str = (string)clubId;
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


private byte[] get_gameType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)gameType;
return outBuf;
}


private byte[] get_roomValue_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)roomValue;
return outBuf;
}


private byte[] get_paramars_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)paramars;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
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
private int set_clubId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_gameType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new byte();
gameType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_roomValue_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomValue = new byte();
roomValue = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_paramars_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramars = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
paramars.Add(curTarget);
curIndex += 4;
}
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
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(roomValue !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomValue_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramars !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramars_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_roomValue_fromBuf(sourceBuf,startOffset);
startOffset = set_paramars_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_gameType_json(){
if(gameType==null){return "";}string resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public string get_roomValue_json(){
if(roomValue==null){return "";}string resultJson = "\"roomValue\":";resultJson += "\"";resultJson += roomValue.ToString();resultJson += "\"";return resultJson;
}


public string get_paramars_json(){
if(paramars==null){return "";}string resultJson = "\"paramars\":";resultJson += "[";List<Int32> listObj = (List<Int32>)paramars;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= byte.Parse(jsonObj.ToString());
}


public void set_roomValue_fromJson(LitJson.JsonData jsonObj){
roomValue= byte.Parse(jsonObj.ToString());
}


public void set_paramars_fromJson(LitJson.JsonData jsonObj){
paramars= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
paramars.Add(Int32.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(gameType !=  null){
resultStr += ",";resultStr += get_gameType_json();
}
else {}if(roomValue !=  null){
resultStr += ",";resultStr += get_roomValue_json();
}
else {}if(paramars !=  null){
resultStr += ",";resultStr += get_paramars_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["roomValue"] != null){
set_roomValue_fromJson(jsonObj["roomValue"]);
}
if(jsonObj["paramars"] != null){
set_paramars_fromJson(jsonObj["paramars"]);
}
}
}
}
