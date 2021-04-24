// 此文件由协议导出插件自动生成
// ID : 00000]
//****状态改变****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///状态改变
/// <\summary>
public class SC_GamerStateChange : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public P_GamerStateChange stateChanges;
public SC_GamerStateChange(){}

public SC_GamerStateChange(P_GamerStateChange _stateChanges){
this.stateChanges = _stateChanges;
}
private Byte[] get_stateChanges_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)stateChanges).Serializer();
return outBuf;
}

private int set_stateChanges_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
stateChanges = new P_GamerStateChange();
curIndex = stateChanges.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(stateChanges !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_stateChanges_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_stateChanges_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_stateChanges_json(){
if(stateChanges==null){return "";}String resultJson = "\"stateChanges\":";resultJson += ((LantisBitProtocolBase)stateChanges).SerializerJson();return resultJson;
}


public void set_stateChanges_fromJson(LitJson.JsonData jsonObj){
stateChanges= new P_GamerStateChange();
stateChanges.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(stateChanges !=  null){
resultStr += get_stateChanges_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["stateChanges"] != null){
set_stateChanges_fromJson(jsonObj["stateChanges"]);
}
}
}
}
