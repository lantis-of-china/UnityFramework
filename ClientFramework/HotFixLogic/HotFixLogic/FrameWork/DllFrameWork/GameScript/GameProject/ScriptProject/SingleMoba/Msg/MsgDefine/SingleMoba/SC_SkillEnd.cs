// 此文件由协议导出插件自动生成
// ID : 00000]
//****技能结束****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///技能结束
/// <\summary>
public class SC_SkillEnd : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 skillId;
/// <summary>
///
/// <\summary>
public Single skillX;
/// <summary>
///
/// <\summary>
public Single skillY;
/// <summary>
///
/// <\summary>
public List<Int32> hitPlayerIds;
public SC_SkillEnd(){}

public SC_SkillEnd(Int32 _skillId, Single _skillX, Single _skillY, List<Int32> _hitPlayerIds){
this.skillId = _skillId;
this.skillX = _skillX;
this.skillY = _skillY;
this.hitPlayerIds = _hitPlayerIds;
}
private Byte[] get_skillId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)skillId);
return outBuf;
}


private Byte[] get_skillX_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)skillX);
return outBuf;
}


private Byte[] get_skillY_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)skillY);
return outBuf;
}


private Byte[] get_hitPlayerIds_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)hitPlayerIds;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
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
private int set_skillX_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skillX = new Single();
skillX = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_skillY_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skillY = new Single();
skillY = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_hitPlayerIds_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hitPlayerIds = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
hitPlayerIds.Add(curTarget);
curIndex += 4;
}
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
}if(skillX !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skillX_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(skillY !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skillY_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hitPlayerIds !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hitPlayerIds_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_skillId_fromBuf(sourceBuf,startOffset);
startOffset = set_skillX_fromBuf(sourceBuf,startOffset);
startOffset = set_skillY_fromBuf(sourceBuf,startOffset);
startOffset = set_hitPlayerIds_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_skillId_json(){
if(skillId==null){return "";}String resultJson = "\"skillId\":";resultJson += "\"";resultJson += skillId.ToString();resultJson += "\"";return resultJson;
}


public String get_skillX_json(){
if(skillX==null){return "";}String resultJson = "\"skillX\":";resultJson += "\"";resultJson += skillX.ToString();resultJson += "\"";return resultJson;
}


public String get_skillY_json(){
if(skillY==null){return "";}String resultJson = "\"skillY\":";resultJson += "\"";resultJson += skillY.ToString();resultJson += "\"";return resultJson;
}


public String get_hitPlayerIds_json(){
if(hitPlayerIds==null){return "";}String resultJson = "\"hitPlayerIds\":";resultJson += "[";List<Int32> listObj = (List<Int32>)hitPlayerIds;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_skillId_fromJson(LitJson.JsonData jsonObj){
skillId= Int32.Parse(jsonObj.ToString());
}


public void set_skillX_fromJson(LitJson.JsonData jsonObj){
skillX= Single.Parse(jsonObj.ToString());
}


public void set_skillY_fromJson(LitJson.JsonData jsonObj){
skillY= Single.Parse(jsonObj.ToString());
}


public void set_hitPlayerIds_fromJson(LitJson.JsonData jsonObj){
hitPlayerIds= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
hitPlayerIds.Add(Int32.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(skillId !=  null){
resultStr += get_skillId_json();
}
else {}if(skillX !=  null){
resultStr += ",";resultStr += get_skillX_json();
}
else {}if(skillY !=  null){
resultStr += ",";resultStr += get_skillY_json();
}
else {}if(hitPlayerIds !=  null){
resultStr += ",";resultStr += get_hitPlayerIds_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["skillId"] != null){
set_skillId_fromJson(jsonObj["skillId"]);
}
if(jsonObj["skillX"] != null){
set_skillX_fromJson(jsonObj["skillX"]);
}
if(jsonObj["skillY"] != null){
set_skillY_fromJson(jsonObj["skillY"]);
}
if(jsonObj["hitPlayerIds"] != null){
set_hitPlayerIds_fromJson(jsonObj["hitPlayerIds"]);
}
}
}
}
