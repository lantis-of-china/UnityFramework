// 此文件由协议导出插件自动生成
// ID : 00001]

//****竞技分改变****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///竞技分改变
/// <\summary>
public class SC_ClubScoreBack : CherishBitProtocolBase {
/// <summary>
///房间ID
/// <\summary>
public Int32 roomId;
/// <summary>
///俱乐部ID
/// <\summary>
public string clubId;
/// <summary>
///游戏类型
/// <\summary>
public byte gameType;
/// <summary>
///总局结算数据
/// <\summary>
public List<byte> toldGameEndData;
/// <summary>
///补分的数量
/// <\summary>
public List<P_ClubScoreBackItem> clubChangeItem;
public SC_ClubScoreBack(){}

public SC_ClubScoreBack(Int32 _roomId, string _clubId, byte _gameType, List<byte> _toldGameEndData, List<P_ClubScoreBackItem> _clubChangeItem){
this.roomId = _roomId;
this.clubId = _clubId;
this.gameType = _gameType;
this.toldGameEndData = _toldGameEndData;
this.clubChangeItem = _clubChangeItem;
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


private byte[] get_gameType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)gameType;
return outBuf;
}


private byte[] get_toldGameEndData_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<byte> listbyte = (List<byte>)toldGameEndData;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private byte[] get_clubChangeItem_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_ClubScoreBackItem> listBase = clubChangeItem;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
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
private int set_gameType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new byte();
gameType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_toldGameEndData_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldGameEndData = new List<byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
byte[] data = new byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
toldGameEndData = new List<byte>(data);
curIndex += listCount;
}return curIndex;
}
private int set_clubChangeItem_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubChangeItem = new List<P_ClubScoreBackItem>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_ClubScoreBackItem curTarget = new P_ClubScoreBackItem();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
clubChangeItem.Add(curTarget);
}
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
}if(gameType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toldGameEndData !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toldGameEndData_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubChangeItem !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubChangeItem_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_toldGameEndData_fromBuf(sourceBuf,startOffset);
startOffset = set_clubChangeItem_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_roomId_json(){
if(roomId==null){return "";}string resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_gameType_json(){
if(gameType==null){return "";}string resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public string get_toldGameEndData_json(){
if(toldGameEndData==null){return "";}string resultJson = "\"toldGameEndData\":";resultJson += "[";List<byte> listObj = (List<byte>)toldGameEndData;
for(int i = 0;i < listObj.Count;++i){
byte item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public string get_clubChangeItem_json(){
if(clubChangeItem==null){return "";}string resultJson = "\"clubChangeItem\":";resultJson += "[";
List<P_ClubScoreBackItem> listObj = (List<P_ClubScoreBackItem>)clubChangeItem;
for(int i = 0;i < listObj.Count;++i){
P_ClubScoreBackItem item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= byte.Parse(jsonObj.ToString());
}


public void set_toldGameEndData_fromJson(LitJson.JsonData jsonObj){
toldGameEndData= new List<byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
toldGameEndData.Add(byte.Parse(jsonItem.ToString()));}

}


public void set_clubChangeItem_fromJson(LitJson.JsonData jsonObj){
clubChangeItem = new List<P_ClubScoreBackItem>();
foreach (LitJson.JsonData item in jsonObj){
P_ClubScoreBackItem addB = new P_ClubScoreBackItem();
clubChangeItem.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override string SerializerJson(){
string resultStr = "{";if(roomId !=  null){
resultStr += get_roomId_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(gameType !=  null){
resultStr += ",";resultStr += get_gameType_json();
}
else {}if(toldGameEndData !=  null){
resultStr += ",";resultStr += get_toldGameEndData_json();
}
else {}if(clubChangeItem !=  null){
resultStr += ",";resultStr += get_clubChangeItem_json();
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
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["toldGameEndData"] != null){
set_toldGameEndData_fromJson(jsonObj["toldGameEndData"]);
}
if(jsonObj["clubChangeItem"] != null){
set_clubChangeItem_fromJson(jsonObj["clubChangeItem"]);
}
}
}
}
