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
public class SC_GameGradeAdd : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public P_GameLogicRecord logicData;
public SC_GameGradeAdd(){}

public SC_GameGradeAdd(P_GameLogicRecord _logicData){
this.logicData = _logicData;
}
private byte[] get_logicData_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)logicData).Serializer();
return outBuf;
}

private int set_logicData_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
logicData = new P_GameLogicRecord();
curIndex = logicData.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(logicData !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_logicData_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_logicData_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_logicData_json(){
if(logicData==null){return "";}string resultJson = "\"logicData\":";resultJson += ((CherishBitProtocolBase)logicData).SerializerJson();return resultJson;
}


public void set_logicData_fromJson(LitJson.JsonData jsonObj){
logicData= new P_GameLogicRecord();
logicData.DeserializerJson(jsonObj.ToJson());}

public override string SerializerJson(){
string resultStr = "{";if(logicData !=  null){
resultStr += get_logicData_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["logicData"] != null){
set_logicData_fromJson(jsonObj["logicData"]);
}
}
}
}
