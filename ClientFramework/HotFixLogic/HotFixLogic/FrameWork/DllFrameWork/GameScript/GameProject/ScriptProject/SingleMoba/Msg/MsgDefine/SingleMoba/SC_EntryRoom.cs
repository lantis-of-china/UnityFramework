// 此文件由协议导出插件自动生成
// ID : 00016]
//****进入房间返回****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///进入房间返回
/// <\summary>
public class SC_EntryRoom : LantisBitProtocolBase {
/// <summary>
///房间ID
/// <\summary>
public Int32 roomId;
/// <summary>
///房间参数
/// <\summary>
public List<Int32> paramarsList;
public SC_EntryRoom(){}

public SC_EntryRoom(Int32 _roomId, List<Int32> _paramarsList){
this.roomId = _roomId;
this.paramarsList = _paramarsList;
}
private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
return outBuf;
}


private Byte[] get_paramarsList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)paramarsList;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
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
private int set_paramarsList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramarsList = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
paramarsList.Add(curTarget);
curIndex += 4;
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
}if(paramarsList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramarsList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_paramarsList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public String get_paramarsList_json(){
if(paramarsList==null){return "";}String resultJson = "\"paramarsList\":";resultJson += "[";List<Int32> listObj = (List<Int32>)paramarsList;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_paramarsList_fromJson(LitJson.JsonData jsonObj){
paramarsList= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
paramarsList.Add(Int32.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(roomId !=  null){
resultStr += get_roomId_json();
}
else {}if(paramarsList !=  null){
resultStr += ",";resultStr += get_paramarsList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
if(jsonObj["paramarsList"] != null){
set_paramarsList_fromJson(jsonObj["paramarsList"]);
}
}
}
}
