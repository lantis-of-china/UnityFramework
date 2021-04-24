// 此文件由协议导出插件自动生成
// ID : 00001]
//****������Ϣ****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///������Ϣ
/// <\summary>
public class P_RoomInfo : LantisBitProtocolBase {
/// <summary>
///�������ڵķ�����ID
/// <\summary>
public String serverId;
/// <summary>
///�ڲ���ȺID
/// <\summary>
public String clubId;
/// <summary>
///����ID
/// <\summary>
public Int32 roomId;
/// <summary>
///��Ϸ����
/// <\summary>
public Byte gameType;
/// <summary>
///�������
/// <\summary>
public Byte maxCount;
/// <summary>
///��ǰ����
/// <\summary>
public Byte curCount;
/// <summary>
///��ǰ����
/// <\summary>
public Byte curTimes;
/// <summary>
///�ܾ���
/// <\summary>
public Byte toldTimes;
/// <summary>
///״̬
/// <\summary>
public Byte state;
/// <summary>
///����
/// <\summary>
public Int32 costPay;
/// <summary>
///�����г�Ա�б�
/// <\summary>
public List<Int32> menberList;
public P_RoomInfo(){}

public P_RoomInfo(String _serverId, String _clubId, Int32 _roomId, Byte _gameType, Byte _maxCount, Byte _curCount, Byte _curTimes, Byte _toldTimes, Byte _state, Int32 _costPay, List<Int32> _menberList){
this.serverId = _serverId;
this.clubId = _clubId;
this.roomId = _roomId;
this.gameType = _gameType;
this.maxCount = _maxCount;
this.curCount = _curCount;
this.curTimes = _curTimes;
this.toldTimes = _toldTimes;
this.state = _state;
this.costPay = _costPay;
this.menberList = _menberList;
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


private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
return outBuf;
}


private Byte[] get_gameType_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)gameType;
return outBuf;
}


private Byte[] get_maxCount_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)maxCount;
return outBuf;
}


private Byte[] get_curCount_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)curCount;
return outBuf;
}


private Byte[] get_curTimes_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)curTimes;
return outBuf;
}


private Byte[] get_toldTimes_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)toldTimes;
return outBuf;
}


private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)state;
return outBuf;
}


private Byte[] get_costPay_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)costPay);
return outBuf;
}


private Byte[] get_menberList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)menberList;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
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
private int set_roomId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomId = new Int32();
roomId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
private int set_maxCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
maxCount = new Byte();
maxCount = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_curCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curCount = new Byte();
curCount = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_curTimes_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curTimes = new Byte();
curTimes = sourceBuf[curIndex];
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
private int set_state_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new Byte();
state = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_costPay_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
costPay = new Int32();
costPay = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_menberList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberList = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
menberList.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(serverId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_serverId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(roomId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(maxCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_maxCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(curCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_curCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(curTimes !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_curTimes_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toldTimes !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toldTimes_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(state !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_state_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(costPay !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_costPay_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_serverId_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_maxCount_fromBuf(sourceBuf,startOffset);
startOffset = set_curCount_fromBuf(sourceBuf,startOffset);
startOffset = set_curTimes_fromBuf(sourceBuf,startOffset);
startOffset = set_toldTimes_fromBuf(sourceBuf,startOffset);
startOffset = set_state_fromBuf(sourceBuf,startOffset);
startOffset = set_costPay_fromBuf(sourceBuf,startOffset);
startOffset = set_menberList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_serverId_json(){
if(serverId==null){return "";}String resultJson = "\"serverId\":";resultJson += "\"";resultJson += serverId.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public String get_gameType_json(){
if(gameType==null){return "";}String resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public String get_maxCount_json(){
if(maxCount==null){return "";}String resultJson = "\"maxCount\":";resultJson += "\"";resultJson += maxCount.ToString();resultJson += "\"";return resultJson;
}


public String get_curCount_json(){
if(curCount==null){return "";}String resultJson = "\"curCount\":";resultJson += "\"";resultJson += curCount.ToString();resultJson += "\"";return resultJson;
}


public String get_curTimes_json(){
if(curTimes==null){return "";}String resultJson = "\"curTimes\":";resultJson += "\"";resultJson += curTimes.ToString();resultJson += "\"";return resultJson;
}


public String get_toldTimes_json(){
if(toldTimes==null){return "";}String resultJson = "\"toldTimes\":";resultJson += "\"";resultJson += toldTimes.ToString();resultJson += "\"";return resultJson;
}


public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public String get_costPay_json(){
if(costPay==null){return "";}String resultJson = "\"costPay\":";resultJson += "\"";resultJson += costPay.ToString();resultJson += "\"";return resultJson;
}


public String get_menberList_json(){
if(menberList==null){return "";}String resultJson = "\"menberList\":";resultJson += "[";List<Int32> listObj = (List<Int32>)menberList;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_serverId_fromJson(LitJson.JsonData jsonObj){
serverId= jsonObj.ToString();
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= Byte.Parse(jsonObj.ToString());
}


public void set_maxCount_fromJson(LitJson.JsonData jsonObj){
maxCount= Byte.Parse(jsonObj.ToString());
}


public void set_curCount_fromJson(LitJson.JsonData jsonObj){
curCount= Byte.Parse(jsonObj.ToString());
}


public void set_curTimes_fromJson(LitJson.JsonData jsonObj){
curTimes= Byte.Parse(jsonObj.ToString());
}


public void set_toldTimes_fromJson(LitJson.JsonData jsonObj){
toldTimes= Byte.Parse(jsonObj.ToString());
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Byte.Parse(jsonObj.ToString());
}


public void set_costPay_fromJson(LitJson.JsonData jsonObj){
costPay= Int32.Parse(jsonObj.ToString());
}


public void set_menberList_fromJson(LitJson.JsonData jsonObj){
menberList= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
menberList.Add(Int32.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(serverId !=  null){
resultStr += get_serverId_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(roomId !=  null){
resultStr += ",";resultStr += get_roomId_json();
}
else {}if(gameType !=  null){
resultStr += ",";resultStr += get_gameType_json();
}
else {}if(maxCount !=  null){
resultStr += ",";resultStr += get_maxCount_json();
}
else {}if(curCount !=  null){
resultStr += ",";resultStr += get_curCount_json();
}
else {}if(curTimes !=  null){
resultStr += ",";resultStr += get_curTimes_json();
}
else {}if(toldTimes !=  null){
resultStr += ",";resultStr += get_toldTimes_json();
}
else {}if(state !=  null){
resultStr += ",";resultStr += get_state_json();
}
else {}if(costPay !=  null){
resultStr += ",";resultStr += get_costPay_json();
}
else {}if(menberList !=  null){
resultStr += ",";resultStr += get_menberList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["serverId"] != null){
set_serverId_fromJson(jsonObj["serverId"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["maxCount"] != null){
set_maxCount_fromJson(jsonObj["maxCount"]);
}
if(jsonObj["curCount"] != null){
set_curCount_fromJson(jsonObj["curCount"]);
}
if(jsonObj["curTimes"] != null){
set_curTimes_fromJson(jsonObj["curTimes"]);
}
if(jsonObj["toldTimes"] != null){
set_toldTimes_fromJson(jsonObj["toldTimes"]);
}
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
if(jsonObj["costPay"] != null){
set_costPay_fromJson(jsonObj["costPay"]);
}
if(jsonObj["menberList"] != null){
set_menberList_fromJson(jsonObj["menberList"]);
}
}
}
}
