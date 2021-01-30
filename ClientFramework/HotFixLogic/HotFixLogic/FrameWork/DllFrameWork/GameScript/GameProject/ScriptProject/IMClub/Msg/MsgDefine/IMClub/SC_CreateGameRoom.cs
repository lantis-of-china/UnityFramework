// 此文件由协议导出插件自动生成
// ID : 00001]
//****�������䷵��[createRoom]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�������䷵��[createRoom]
/// <\summary>
public class SC_CreateGameRoom : CherishBitProtocolBase {
/// <summary>
///����ֵ
/// <\summary>
public byte result;
/// <summary>
///������ID
/// <\summary>
public string serverId;
/// <summary>
///��Ϸ����
/// <\summary>
public byte gameType;
/// <summary>
///����ID
/// <\summary>
public Int32 roomId;
/// <summary>
///��ǰ��������
/// <\summary>
public byte curPlayerCount;
/// <summary>
///������������
/// <\summary>
public byte maxPlayerCount;
/// <summary>
///0�ȴ��� 1�Ѿ���ʼ
/// <\summary>
public byte state;
/// <summary>
///�йܶ�����ʯ
/// <\summary>
public byte useRecharge;
/// <summary>
///����
/// <\summary>
public byte times;
/// <summary>
///����
/// <\summary>
public byte toldTimes;
/// <summary>
///���ֲ�ID
/// <\summary>
public string clubId;
public SC_CreateGameRoom(){}

public SC_CreateGameRoom(byte _result, string _serverId, byte _gameType, Int32 _roomId, byte _curPlayerCount, byte _maxPlayerCount, byte _state, byte _useRecharge, byte _times, byte _toldTimes, string _clubId){
this.result = _result;
this.serverId = _serverId;
this.gameType = _gameType;
this.roomId = _roomId;
this.curPlayerCount = _curPlayerCount;
this.maxPlayerCount = _maxPlayerCount;
this.state = _state;
this.useRecharge = _useRecharge;
this.times = _times;
this.toldTimes = _toldTimes;
this.clubId = _clubId;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_serverId_encoding(){
byte[] outBuf = null;
string str = (string)serverId;
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


private byte[] get_roomId_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
return outBuf;
}


private byte[] get_curPlayerCount_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)curPlayerCount;
return outBuf;
}


private byte[] get_maxPlayerCount_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)maxPlayerCount;
return outBuf;
}


private byte[] get_state_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)state;
return outBuf;
}


private byte[] get_useRecharge_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)useRecharge;
return outBuf;
}


private byte[] get_times_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)times;
return outBuf;
}


private byte[] get_toldTimes_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)toldTimes;
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

private int set_result_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_serverId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
serverId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
serverId = System.Text.Encoding.UTF8.GetString(byteArray);
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
private int set_roomId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomId = new Int32();
roomId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_curPlayerCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curPlayerCount = new byte();
curPlayerCount = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_maxPlayerCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
maxPlayerCount = new byte();
maxPlayerCount = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_state_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new byte();
state = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_useRecharge_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
useRecharge = new byte();
useRecharge = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_times_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
times = new byte();
times = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_toldTimes_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldTimes = new byte();
toldTimes = sourceBuf[curIndex];
curIndex++;
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
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(result !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_result_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(serverId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_serverId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(roomId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(curPlayerCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_curPlayerCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(maxPlayerCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_maxPlayerCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(state !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_state_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(useRecharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_useRecharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(times !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_times_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toldTimes !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toldTimes_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_serverId_fromBuf(sourceBuf,startOffset);
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_curPlayerCount_fromBuf(sourceBuf,startOffset);
startOffset = set_maxPlayerCount_fromBuf(sourceBuf,startOffset);
startOffset = set_state_fromBuf(sourceBuf,startOffset);
startOffset = set_useRecharge_fromBuf(sourceBuf,startOffset);
startOffset = set_times_fromBuf(sourceBuf,startOffset);
startOffset = set_toldTimes_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_serverId_json(){
if(serverId==null){return "";}string resultJson = "\"serverId\":";resultJson += "\"";resultJson += serverId.ToString();resultJson += "\"";return resultJson;
}


public string get_gameType_json(){
if(gameType==null){return "";}string resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public string get_roomId_json(){
if(roomId==null){return "";}string resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public string get_curPlayerCount_json(){
if(curPlayerCount==null){return "";}string resultJson = "\"curPlayerCount\":";resultJson += "\"";resultJson += curPlayerCount.ToString();resultJson += "\"";return resultJson;
}


public string get_maxPlayerCount_json(){
if(maxPlayerCount==null){return "";}string resultJson = "\"maxPlayerCount\":";resultJson += "\"";resultJson += maxPlayerCount.ToString();resultJson += "\"";return resultJson;
}


public string get_state_json(){
if(state==null){return "";}string resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public string get_useRecharge_json(){
if(useRecharge==null){return "";}string resultJson = "\"useRecharge\":";resultJson += "\"";resultJson += useRecharge.ToString();resultJson += "\"";return resultJson;
}


public string get_times_json(){
if(times==null){return "";}string resultJson = "\"times\":";resultJson += "\"";resultJson += times.ToString();resultJson += "\"";return resultJson;
}


public string get_toldTimes_json(){
if(toldTimes==null){return "";}string resultJson = "\"toldTimes\":";resultJson += "\"";resultJson += toldTimes.ToString();resultJson += "\"";return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_serverId_fromJson(LitJson.JsonData jsonObj){
serverId= jsonObj.ToString();
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= byte.Parse(jsonObj.ToString());
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_curPlayerCount_fromJson(LitJson.JsonData jsonObj){
curPlayerCount= byte.Parse(jsonObj.ToString());
}


public void set_maxPlayerCount_fromJson(LitJson.JsonData jsonObj){
maxPlayerCount= byte.Parse(jsonObj.ToString());
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= byte.Parse(jsonObj.ToString());
}


public void set_useRecharge_fromJson(LitJson.JsonData jsonObj){
useRecharge= byte.Parse(jsonObj.ToString());
}


public void set_times_fromJson(LitJson.JsonData jsonObj){
times= byte.Parse(jsonObj.ToString());
}


public void set_toldTimes_fromJson(LitJson.JsonData jsonObj){
toldTimes= byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(serverId !=  null){
resultStr += ",";resultStr += get_serverId_json();
}
else {}if(gameType !=  null){
resultStr += ",";resultStr += get_gameType_json();
}
else {}if(roomId !=  null){
resultStr += ",";resultStr += get_roomId_json();
}
else {}if(curPlayerCount !=  null){
resultStr += ",";resultStr += get_curPlayerCount_json();
}
else {}if(maxPlayerCount !=  null){
resultStr += ",";resultStr += get_maxPlayerCount_json();
}
else {}if(state !=  null){
resultStr += ",";resultStr += get_state_json();
}
else {}if(useRecharge !=  null){
resultStr += ",";resultStr += get_useRecharge_json();
}
else {}if(times !=  null){
resultStr += ",";resultStr += get_times_json();
}
else {}if(toldTimes !=  null){
resultStr += ",";resultStr += get_toldTimes_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["serverId"] != null){
set_serverId_fromJson(jsonObj["serverId"]);
}
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
if(jsonObj["curPlayerCount"] != null){
set_curPlayerCount_fromJson(jsonObj["curPlayerCount"]);
}
if(jsonObj["maxPlayerCount"] != null){
set_maxPlayerCount_fromJson(jsonObj["maxPlayerCount"]);
}
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
if(jsonObj["useRecharge"] != null){
set_useRecharge_fromJson(jsonObj["useRecharge"]);
}
if(jsonObj["times"] != null){
set_times_fromJson(jsonObj["times"]);
}
if(jsonObj["toldTimes"] != null){
set_toldTimes_fromJson(jsonObj["toldTimes"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
}
}
}
