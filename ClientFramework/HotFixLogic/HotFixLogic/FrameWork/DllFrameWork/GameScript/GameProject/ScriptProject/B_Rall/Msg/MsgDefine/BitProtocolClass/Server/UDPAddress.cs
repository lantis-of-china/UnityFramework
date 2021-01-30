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
public class UDPAddress : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor value;
public UDPAddress(){}

public UDPAddress(UserValiadateInfor _value){
this.value = _value;
}
private byte[] get_value_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)value).Serializer();
return outBuf;
}

private int set_value_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
value = new UserValiadateInfor();
curIndex = value.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(value !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_value_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_value_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_value_json(){
if(value==null){return "";}string resultJson = "\"value\":";resultJson += ((CherishBitProtocolBase)value).SerializerJson();return resultJson;
}


public void set_value_fromJson(LitJson.JsonData jsonObj){
value= new UserValiadateInfor();
value.DeserializerJson(jsonObj.ToJson());}

public override string SerializerJson(){
string resultStr = "{";if(value !=  null){
resultStr += get_value_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["value"] != null){
set_value_fromJson(jsonObj["value"]);
}
}
}
}
