// 此文件由协议导出插件自动生成
// ID : 00001]

//****强制解散房间****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///强制解散房间
/// <\summary>
public class CS_FoceUnReleseRoom : LantisBitProtocolBase {
/// <summary>
///房间ID
/// <\summary>
public Int32 roomId;
public CS_FoceUnReleseRoom(){}

public CS_FoceUnReleseRoom(Int32 _roomId){
this.roomId = _roomId;
}
private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(roomId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(roomId !=  null){
resultStr += get_roomId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
}
}
}
