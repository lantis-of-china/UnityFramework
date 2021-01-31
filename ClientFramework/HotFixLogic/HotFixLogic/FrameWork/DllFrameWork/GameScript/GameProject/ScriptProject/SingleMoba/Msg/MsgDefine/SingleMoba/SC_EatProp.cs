// 此文件由协议导出插件自动生成
// ID : 00000]
//****吃道具****
using System;
using System.Collections.Generic;
using System.IO;

namespace SingleMoba{
/// <summary>
///吃道具
/// <\summary>
public class SC_EatProp : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public List<Int32> propId;
/// <summary>
///
/// <\summary>
public P_GamerStateChange players;
public SC_EatProp(){}

public SC_EatProp(Int32 _playerId, List<Int32> _propId, P_GamerStateChange _players){
this.playerId = _playerId;
this.propId = _propId;
this.players = _players;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
return outBuf;
}


private Byte[] get_propId_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)propId;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_players_encoding(){
Byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)players).Serializer();
return outBuf;
}

private int set_playerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
playerId = new Int32();
playerId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_propId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
propId = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
propId.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
private int set_players_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
players = new P_GamerStateChange();
curIndex = players.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(playerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_playerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(propId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_propId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(players !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_players_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_propId_fromBuf(sourceBuf,startOffset);
startOffset = set_players_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_propId_json(){
if(propId==null){return "";}String resultJson = "\"propId\":";resultJson += "[";List<Int32> listObj = (List<Int32>)propId;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_players_json(){
if(players==null){return "";}String resultJson = "\"players\":";resultJson += ((CherishBitProtocolBase)players).SerializerJson();return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_propId_fromJson(LitJson.JsonData jsonObj){
propId= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
propId.Add(Int32.Parse(jsonItem.ToString()));}

}


public void set_players_fromJson(LitJson.JsonData jsonObj){
players= new P_GamerStateChange();
players.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(propId !=  null){
resultStr += ",";resultStr += get_propId_json();
}
else {}if(players !=  null){
resultStr += ",";resultStr += get_players_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["propId"] != null){
set_propId_fromJson(jsonObj["propId"]);
}
if(jsonObj["players"] != null){
set_players_fromJson(jsonObj["players"]);
}
}
}
}
