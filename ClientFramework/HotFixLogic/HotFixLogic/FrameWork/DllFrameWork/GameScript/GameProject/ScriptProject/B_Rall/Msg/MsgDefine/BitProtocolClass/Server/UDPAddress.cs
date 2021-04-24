// 此文件由协议导出插件自动生成
// ID : 10001]

//****主要开启一条通道****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///主要开启一条通道
/// <\summary>
public class UDPAddress : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor value;
public UDPAddress(){}

public UDPAddress(UserValiadateInfor _value){
this.value = _value;
}
private Byte[] get_value_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)value).Serializer();
return outBuf;
}

private int set_value_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
value = new UserValiadateInfor();
curIndex = value.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(value !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_value_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_value_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_value_json(){
if(value==null){return "";}String resultJson = "\"value\":";resultJson += ((LantisBitProtocolBase)value).SerializerJson();return resultJson;
}


public void set_value_fromJson(LitJson.JsonData jsonObj){
value= new UserValiadateInfor();
value.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(value !=  null){
resultStr += get_value_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["value"] != null){
set_value_fromJson(jsonObj["value"]);
}
}
}
}
