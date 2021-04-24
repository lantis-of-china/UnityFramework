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
public class SC_CreateGameRoom : LantisBitProtocolBase {
/// <summary>
///����ֵ
/// <\summary>
public Byte result;
/// <summary>
///������ID
/// <\summary>
public String serverId;
/// <summary>
///��Ϸ����
/// <\summary>
public Byte gameType;
/// <summary>
///����ID
/// <\summary>
public Int32 roomId;
/// <summary>
///��ǰ�������
/// <\summary>
public Byte curPlayerCount;
/// <summary>
///����������
/// <\summary>
public Byte maxPlayerCount;
/// <summary>
///0�ȴ��� 1�Ѿ���ʼ
/// <\summary>
public Byte state;
/// <summary>
///�йܶ�����ʯ
/// <\summary>
public Byte useRecharge;
/// <summary>
///����
/// <\summary>
public Byte times;
/// <summary>
///���
/// <\summary>
public Byte toldTimes;
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
public SC_CreateGameRoom(){}

public SC_CreateGameRoom(Byte _result, String _serverId, Byte _gameType, Int32 _roomId, Byte _curPlayerCount, Byte _maxPlayerCount, Byte _state, Byte _useRecharge, Byte _times, Byte _toldTimes, String _clubId){
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
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
return outBuf;
}


private Byte[] get_serverId_encoding(){
Byte[] outBuf = null;
String str = (String)serverId;
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


private Byte[] get_gameType_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)gameType;
return outBuf;
}


private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
return outBuf;
}


private Byte[] get_curPlayerCount_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)curPlayerCount;
return outBuf;
}


private Byte[] get_maxPlayerCount_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)maxPlayerCount;
return outBuf;
}


private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)state;
return outBuf;
}


private Byte[] get_useRecharge_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)useRecharge;
return outBuf;
}


private Byte[] get_times_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)times;
return outBuf;
}


private Byte[] get_toldTimes_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)toldTimes;
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

private int set_result_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new Byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_serverId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_gameType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new Byte();
gameType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_roomId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomId = new Int32();
roomId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_curPlayerCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curPlayerCount = new Byte();
curPlayerCount = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_maxPlayerCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
maxPlayerCount = new Byte();
maxPlayerCount = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_state_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new Byte();
state = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_useRecharge_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
useRecharge = new Byte();
useRecharge = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_times_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
times = new Byte();
times = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_toldTimes_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldTimes = new Byte();
toldTimes = sourceBuf[curIndex];
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
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

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_serverId_json(){
if(serverId==null){return "";}String resultJson = "\"serverId\":";resultJson += "\"";resultJson += serverId.ToString();resultJson += "\"";return resultJson;
}


public String get_gameType_json(){
if(gameType==null){return "";}String resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public String get_curPlayerCount_json(){
if(curPlayerCount==null){return "";}String resultJson = "\"curPlayerCount\":";resultJson += "\"";resultJson += curPlayerCount.ToString();resultJson += "\"";return resultJson;
}


public String get_maxPlayerCount_json(){
if(maxPlayerCount==null){return "";}String resultJson = "\"maxPlayerCount\":";resultJson += "\"";resultJson += maxPlayerCount.ToString();resultJson += "\"";return resultJson;
}


public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public String get_useRecharge_json(){
if(useRecharge==null){return "";}String resultJson = "\"useRecharge\":";resultJson += "\"";resultJson += useRecharge.ToString();resultJson += "\"";return resultJson;
}


public String get_times_json(){
if(times==null){return "";}String resultJson = "\"times\":";resultJson += "\"";resultJson += times.ToString();resultJson += "\"";return resultJson;
}


public String get_toldTimes_json(){
if(toldTimes==null){return "";}String resultJson = "\"toldTimes\":";resultJson += "\"";resultJson += toldTimes.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_serverId_fromJson(LitJson.JsonData jsonObj){
serverId= jsonObj.ToString();
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= Byte.Parse(jsonObj.ToString());
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_curPlayerCount_fromJson(LitJson.JsonData jsonObj){
curPlayerCount= Byte.Parse(jsonObj.ToString());
}


public void set_maxPlayerCount_fromJson(LitJson.JsonData jsonObj){
maxPlayerCount= Byte.Parse(jsonObj.ToString());
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Byte.Parse(jsonObj.ToString());
}


public void set_useRecharge_fromJson(LitJson.JsonData jsonObj){
useRecharge= Byte.Parse(jsonObj.ToString());
}


public void set_times_fromJson(LitJson.JsonData jsonObj){
times= Byte.Parse(jsonObj.ToString());
}


public void set_toldTimes_fromJson(LitJson.JsonData jsonObj){
toldTimes= Byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
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

public override void DeserializerJson(String json){
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
