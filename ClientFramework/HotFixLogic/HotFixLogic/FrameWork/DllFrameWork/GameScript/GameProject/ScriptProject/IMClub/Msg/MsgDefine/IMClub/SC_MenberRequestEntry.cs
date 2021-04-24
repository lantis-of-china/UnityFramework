// 此文件由协议导出插件自动生成
// ID : 00001]
//****�������SC_MenberRequestEntry_MsgType = 20036****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�������SC_MenberRequestEntry_MsgType = 20036
/// <\summary>
public class SC_MenberRequestEntry : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public P_RequestInfo request;
public SC_MenberRequestEntry(){}

public SC_MenberRequestEntry(P_RequestInfo _request){
this.request = _request;
}
private Byte[] get_request_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)request).Serializer();
return outBuf;
}

private int set_request_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
request = new P_RequestInfo();
curIndex = request.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(request !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_request_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_request_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_request_json(){
if(request==null){return "";}String resultJson = "\"request\":";resultJson += ((LantisBitProtocolBase)request).SerializerJson();return resultJson;
}


public void set_request_fromJson(LitJson.JsonData jsonObj){
request= new P_RequestInfo();
request.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(request !=  null){
resultStr += get_request_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["request"] != null){
set_request_fromJson(jsonObj["request"]);
}
}
}
}
