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
public class SC_ClubScoreBack : LantisBitProtocolBase {
/// <summary>
///房间ID
/// <\summary>
public Int32 roomId;
/// <summary>
///俱乐部ID
/// <\summary>
public String clubId;
/// <summary>
///游戏类型
/// <\summary>
public Byte gameType;
/// <summary>
///总局结算数据
/// <\summary>
public List<Byte> toldGameEndData;
/// <summary>
///补分的数量
/// <\summary>
public List<P_ClubScoreBackItem> clubChangeItem;
public SC_ClubScoreBack(){}

public SC_ClubScoreBack(Int32 _roomId, String _clubId, Byte _gameType, List<Byte> _toldGameEndData, List<P_ClubScoreBackItem> _clubChangeItem){
this.roomId = _roomId;
this.clubId = _clubId;
this.gameType = _gameType;
this.toldGameEndData = _toldGameEndData;
this.clubChangeItem = _clubChangeItem;
}
private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
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


private Byte[] get_gameType_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)gameType;
return outBuf;
}


private Byte[] get_toldGameEndData_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Byte> listbyte = (List<Byte>)toldGameEndData;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
Byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_clubChangeItem_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_ClubScoreBackItem> listBase = clubChangeItem;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
LantisBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
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
private int set_gameType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new Byte();
gameType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_toldGameEndData_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldGameEndData = new List<Byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
Byte[] data = new Byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
toldGameEndData = new List<Byte>(data);
curIndex += listCount;
}return curIndex;
}
private int set_clubChangeItem_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_toldGameEndData_fromBuf(sourceBuf,startOffset);
startOffset = set_clubChangeItem_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_gameType_json(){
if(gameType==null){return "";}String resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public String get_toldGameEndData_json(){
if(toldGameEndData==null){return "";}String resultJson = "\"toldGameEndData\":";resultJson += "[";List<Byte> listObj = (List<Byte>)toldGameEndData;
for(int i = 0;i < listObj.Count;++i){
Byte item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_clubChangeItem_json(){
if(clubChangeItem==null){return "";}String resultJson = "\"clubChangeItem\":";resultJson += "[";
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
gameType= Byte.Parse(jsonObj.ToString());
}


public void set_toldGameEndData_fromJson(LitJson.JsonData jsonObj){
toldGameEndData= new List<Byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
toldGameEndData.Add(Byte.Parse(jsonItem.ToString()));}

}


public void set_clubChangeItem_fromJson(LitJson.JsonData jsonObj){
clubChangeItem = new List<P_ClubScoreBackItem>();
foreach (LitJson.JsonData item in jsonObj){
P_ClubScoreBackItem addB = new P_ClubScoreBackItem();
clubChangeItem.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(roomId !=  null){
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

public override void DeserializerJson(String json){
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
