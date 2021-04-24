// 此文件由协议导出插件自动生成
// ID : 00001]
//****新战绩****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///新战绩
/// <\summary>
public class SC_GameGradeAdd : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public P_GameLogicRecord logicData;
public SC_GameGradeAdd(){}

public SC_GameGradeAdd(P_GameLogicRecord _logicData){
this.logicData = _logicData;
}
private Byte[] get_logicData_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)logicData).Serializer();
return outBuf;
}

private int set_logicData_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
logicData = new P_GameLogicRecord();
curIndex = logicData.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(logicData !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_logicData_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_logicData_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_logicData_json(){
if(logicData==null){return "";}String resultJson = "\"logicData\":";resultJson += ((LantisBitProtocolBase)logicData).SerializerJson();return resultJson;
}


public void set_logicData_fromJson(LitJson.JsonData jsonObj){
logicData= new P_GameLogicRecord();
logicData.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(logicData !=  null){
resultStr += get_logicData_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["logicData"] != null){
set_logicData_fromJson(jsonObj["logicData"]);
}
}
}
}
