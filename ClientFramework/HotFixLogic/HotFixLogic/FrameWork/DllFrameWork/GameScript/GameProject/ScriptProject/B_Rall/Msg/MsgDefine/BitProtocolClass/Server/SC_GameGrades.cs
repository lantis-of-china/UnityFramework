// 此文件由协议导出插件自动生成
// ID : 00001]
//****战绩推送****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///战绩推送
/// <\summary>
public class SC_GameGrades : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public List<P_GameLogicRecord> logicDatas;
public SC_GameGrades(){}

public SC_GameGrades(List<P_GameLogicRecord> _logicDatas){
this.logicDatas = _logicDatas;
}
private Byte[] get_logicDatas_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_GameLogicRecord> listBase = logicDatas;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
LantisBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_logicDatas_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
logicDatas = new List<P_GameLogicRecord>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_GameLogicRecord curTarget = new P_GameLogicRecord();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
logicDatas.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(logicDatas !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_logicDatas_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_logicDatas_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_logicDatas_json(){
if(logicDatas==null){return "";}String resultJson = "\"logicDatas\":";resultJson += "[";
List<P_GameLogicRecord> listObj = (List<P_GameLogicRecord>)logicDatas;
for(int i = 0;i < listObj.Count;++i){
P_GameLogicRecord item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_logicDatas_fromJson(LitJson.JsonData jsonObj){
logicDatas = new List<P_GameLogicRecord>();
foreach (LitJson.JsonData item in jsonObj){
P_GameLogicRecord addB = new P_GameLogicRecord();
logicDatas.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(logicDatas !=  null){
resultStr += get_logicDatas_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["logicDatas"] != null){
set_logicDatas_fromJson(jsonObj["logicDatas"]);
}
}
}
}
