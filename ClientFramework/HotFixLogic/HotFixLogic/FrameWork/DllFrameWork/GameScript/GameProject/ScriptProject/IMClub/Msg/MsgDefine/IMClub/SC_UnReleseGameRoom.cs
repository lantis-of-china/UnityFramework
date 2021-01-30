// 此文件由协议导出插件自动生成
// ID : 00001]
//****��ɢ����[unReleseRoom]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��ɢ����[unReleseRoom]
/// <\summary>
public class SC_UnReleseGameRoom : CherishBitProtocolBase {
/// <summary>
///����ID
/// <\summary>
public Int32 roomId;
/// <summary>
///���ֲ�ID
/// <\summary>
public string clubId;
/// <summary>
///��ǰ����
/// <\summary>
public Int32 curRecharge;
/// <summary>
///�����ķ���
/// <\summary>
public Int32 toldUseRecharge;
/// <summary>
///�������ķ���
/// <\summary>
public Int32 todayUseRecharge;
public SC_UnReleseGameRoom(){}

public SC_UnReleseGameRoom(Int32 _roomId, string _clubId, Int32 _curRecharge, Int32 _toldUseRecharge, Int32 _todayUseRecharge){
this.roomId = _roomId;
this.clubId = _clubId;
this.curRecharge = _curRecharge;
this.toldUseRecharge = _toldUseRecharge;
this.todayUseRecharge = _todayUseRecharge;
}
private byte[] get_roomId_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
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


private byte[] get_curRecharge_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)curRecharge);
return outBuf;
}


private byte[] get_toldUseRecharge_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)toldUseRecharge);
return outBuf;
}


private byte[] get_todayUseRecharge_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)todayUseRecharge);
return outBuf;
}

private int set_roomId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomId = new Int32();
roomId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
private int set_curRecharge_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curRecharge = new Int32();
curRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_toldUseRecharge_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldUseRecharge = new Int32();
toldUseRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_todayUseRecharge_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
todayUseRecharge = new Int32();
todayUseRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(roomId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(curRecharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_curRecharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toldUseRecharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toldUseRecharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(todayUseRecharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_todayUseRecharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_curRecharge_fromBuf(sourceBuf,startOffset);
startOffset = set_toldUseRecharge_fromBuf(sourceBuf,startOffset);
startOffset = set_todayUseRecharge_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_roomId_json(){
if(roomId==null){return "";}string resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_curRecharge_json(){
if(curRecharge==null){return "";}string resultJson = "\"curRecharge\":";resultJson += "\"";resultJson += curRecharge.ToString();resultJson += "\"";return resultJson;
}


public string get_toldUseRecharge_json(){
if(toldUseRecharge==null){return "";}string resultJson = "\"toldUseRecharge\":";resultJson += "\"";resultJson += toldUseRecharge.ToString();resultJson += "\"";return resultJson;
}


public string get_todayUseRecharge_json(){
if(todayUseRecharge==null){return "";}string resultJson = "\"todayUseRecharge\":";resultJson += "\"";resultJson += todayUseRecharge.ToString();resultJson += "\"";return resultJson;
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_curRecharge_fromJson(LitJson.JsonData jsonObj){
curRecharge= Int32.Parse(jsonObj.ToString());
}


public void set_toldUseRecharge_fromJson(LitJson.JsonData jsonObj){
toldUseRecharge= Int32.Parse(jsonObj.ToString());
}


public void set_todayUseRecharge_fromJson(LitJson.JsonData jsonObj){
todayUseRecharge= Int32.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(roomId !=  null){
resultStr += get_roomId_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(curRecharge !=  null){
resultStr += ",";resultStr += get_curRecharge_json();
}
else {}if(toldUseRecharge !=  null){
resultStr += ",";resultStr += get_toldUseRecharge_json();
}
else {}if(todayUseRecharge !=  null){
resultStr += ",";resultStr += get_todayUseRecharge_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["curRecharge"] != null){
set_curRecharge_fromJson(jsonObj["curRecharge"]);
}
if(jsonObj["toldUseRecharge"] != null){
set_toldUseRecharge_fromJson(jsonObj["toldUseRecharge"]);
}
if(jsonObj["todayUseRecharge"] != null){
set_todayUseRecharge_fromJson(jsonObj["todayUseRecharge"]);
}
}
}
}
