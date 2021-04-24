// 此文件由协议导出插件自动生成
// ID : 00000]
//****技能命中****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///技能命中
/// <\summary>
public class SC_HitSkill : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 skillId;
/// <summary>
///
/// <\summary>
public Int32 hitPlayerId;
public SC_HitSkill(){}

public SC_HitSkill(Int32 _skillId, Int32 _hitPlayerId){
this.skillId = _skillId;
this.hitPlayerId = _hitPlayerId;
}
private Byte[] get_skillId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)skillId);
return outBuf;
}


private Byte[] get_hitPlayerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)hitPlayerId);
return outBuf;
}

private int set_skillId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skillId = new Int32();
skillId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_hitPlayerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hitPlayerId = new Int32();
hitPlayerId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(skillId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skillId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hitPlayerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hitPlayerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_skillId_fromBuf(sourceBuf,startOffset);
startOffset = set_hitPlayerId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_skillId_json(){
if(skillId==null){return "";}String resultJson = "\"skillId\":";resultJson += "\"";resultJson += skillId.ToString();resultJson += "\"";return resultJson;
}


public String get_hitPlayerId_json(){
if(hitPlayerId==null){return "";}String resultJson = "\"hitPlayerId\":";resultJson += "\"";resultJson += hitPlayerId.ToString();resultJson += "\"";return resultJson;
}


public void set_skillId_fromJson(LitJson.JsonData jsonObj){
skillId= Int32.Parse(jsonObj.ToString());
}


public void set_hitPlayerId_fromJson(LitJson.JsonData jsonObj){
hitPlayerId= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(skillId !=  null){
resultStr += get_skillId_json();
}
else {}if(hitPlayerId !=  null){
resultStr += ",";resultStr += get_hitPlayerId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["skillId"] != null){
set_skillId_fromJson(jsonObj["skillId"]);
}
if(jsonObj["hitPlayerId"] != null){
set_hitPlayerId_fromJson(jsonObj["hitPlayerId"]);
}
}
}
}
