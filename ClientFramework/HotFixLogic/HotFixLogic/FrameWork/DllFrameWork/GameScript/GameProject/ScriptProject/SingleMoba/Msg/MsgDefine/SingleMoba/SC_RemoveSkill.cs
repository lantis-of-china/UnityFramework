// 此文件由协议导出插件自动生成
// ID : 00000]
//****移除技能****
using System;
using System.Collections.Generic;
using System.IO;


namespace SingleMoba{
/// <summary>
///移除技能
/// <\summary>
public class SC_RemoveSkill : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 skillId;
public SC_RemoveSkill(){}

public SC_RemoveSkill(Int32 _skillId){
this.skillId = _skillId;
}
private Byte[] get_skillId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)skillId);
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(skillId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skillId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_skillId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_skillId_json(){
if(skillId==null){return "";}String resultJson = "\"skillId\":";resultJson += "\"";resultJson += skillId.ToString();resultJson += "\"";return resultJson;
}


public void set_skillId_fromJson(LitJson.JsonData jsonObj){
skillId= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(skillId !=  null){
resultStr += get_skillId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["skillId"] != null){
set_skillId_fromJson(jsonObj["skillId"]);
}
}
}
}
