// 此文件由协议导出插件自动生成
// ID : 00001]
//****��Ϸ����****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��Ϸ����
/// <\summary>
public class P_GameSetting : CherishBitProtocolBase {
/// <summary>
///��Ϸ����
/// <\summary>
public Int32 gameType;
/// <summary>
///����
/// <\summary>
public byte roomValue;
/// <summary>
///�����趨
/// <\summary>
public List<Int32> pamarasSetting;
public P_GameSetting(){}

public P_GameSetting(Int32 _gameType, byte _roomValue, List<Int32> _pamarasSetting){
this.gameType = _gameType;
this.roomValue = _roomValue;
this.pamarasSetting = _pamarasSetting;
}
private byte[] get_gameType_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)gameType);
return outBuf;
}


private byte[] get_roomValue_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)roomValue;
return outBuf;
}


private byte[] get_pamarasSetting_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)pamarasSetting;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_gameType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new Int32();
gameType = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
private int set_pamarasSetting_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
pamarasSetting = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
pamarasSetting.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(gameType !=  null){
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
}if(pamarasSetting !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_pamarasSetting_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_roomValue_fromBuf(sourceBuf,startOffset);
startOffset = set_pamarasSetting_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_gameType_json(){
if(gameType==null){return "";}string resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public string get_roomValue_json(){
if(roomValue==null){return "";}string resultJson = "\"roomValue\":";resultJson += "\"";resultJson += roomValue.ToString();resultJson += "\"";return resultJson;
}


public string get_pamarasSetting_json(){
if(pamarasSetting==null){return "";}string resultJson = "\"pamarasSetting\":";resultJson += "[";List<Int32> listObj = (List<Int32>)pamarasSetting;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= Int32.Parse(jsonObj.ToString());
}


public void set_roomValue_fromJson(LitJson.JsonData jsonObj){
roomValue= byte.Parse(jsonObj.ToString());
}


public void set_pamarasSetting_fromJson(LitJson.JsonData jsonObj){
pamarasSetting= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
pamarasSetting.Add(Int32.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(gameType !=  null){
resultStr += get_gameType_json();
}
else {}if(roomValue !=  null){
resultStr += ",";resultStr += get_roomValue_json();
}
else {}if(pamarasSetting !=  null){
resultStr += ",";resultStr += get_pamarasSetting_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["roomValue"] != null){
set_roomValue_fromJson(jsonObj["roomValue"]);
}
if(jsonObj["pamarasSetting"] != null){
set_pamarasSetting_fromJson(jsonObj["pamarasSetting"]);
}
}
}
}
