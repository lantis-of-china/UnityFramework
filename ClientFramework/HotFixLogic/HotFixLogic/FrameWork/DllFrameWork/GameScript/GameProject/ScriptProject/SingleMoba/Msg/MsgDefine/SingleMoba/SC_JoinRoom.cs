// 此文件由协议导出插件自动生成
// ID : 00016]
//****加入房间****
using System;
using System.Collections.Generic;
using System.IO;


namespace SingleMoba{
/// <summary>
///加入房间
/// <\summary>
public class SC_JoinRoom : CherishBitProtocolBase {
/// <summary>
///房间中的玩家
/// <\summary>
public P_PlayerInfo joinPlayerInfo;
public SC_JoinRoom(){}

public SC_JoinRoom(P_PlayerInfo _joinPlayerInfo){
this.joinPlayerInfo = _joinPlayerInfo;
}
private Byte[] get_joinPlayerInfo_encoding(){
Byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)joinPlayerInfo).Serializer();
return outBuf;
}

private int set_joinPlayerInfo_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
joinPlayerInfo = new P_PlayerInfo();
curIndex = joinPlayerInfo.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(joinPlayerInfo !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_joinPlayerInfo_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_joinPlayerInfo_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_joinPlayerInfo_json(){
if(joinPlayerInfo==null){return "";}String resultJson = "\"joinPlayerInfo\":";resultJson += ((CherishBitProtocolBase)joinPlayerInfo).SerializerJson();return resultJson;
}


public void set_joinPlayerInfo_fromJson(LitJson.JsonData jsonObj){
joinPlayerInfo= new P_PlayerInfo();
joinPlayerInfo.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(joinPlayerInfo !=  null){
resultStr += get_joinPlayerInfo_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["joinPlayerInfo"] != null){
set_joinPlayerInfo_fromJson(jsonObj["joinPlayerInfo"]);
}
}
}
}
