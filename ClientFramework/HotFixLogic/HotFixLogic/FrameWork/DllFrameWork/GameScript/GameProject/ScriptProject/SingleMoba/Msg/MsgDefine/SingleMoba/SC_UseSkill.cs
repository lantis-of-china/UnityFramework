// 此文件由协议导出插件自动生成
// ID : 00000]
//****施放技能****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///施放技能
/// <\summary>
public class SC_UseSkill : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public P_Skill skill;
/// <summary>
///
/// <\summary>
public List<P_GamerStateChange> gamerChanges;
/// <summary>
///
/// <\summary>
public Single targetX;
/// <summary>
///
/// <\summary>
public Single targetY;
public SC_UseSkill(){}

public SC_UseSkill(Int32 _playerId, P_Skill _skill, List<P_GamerStateChange> _gamerChanges, Single _targetX, Single _targetY){
this.playerId = _playerId;
this.skill = _skill;
this.gamerChanges = _gamerChanges;
this.targetX = _targetX;
this.targetY = _targetY;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
return outBuf;
}


private Byte[] get_skill_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)skill).Serializer();
return outBuf;
}


private Byte[] get_gamerChanges_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_GamerStateChange> listBase = gamerChanges;
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


private Byte[] get_targetX_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)targetX);
return outBuf;
}


private Byte[] get_targetY_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)targetY);
return outBuf;
}

private int set_playerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
playerId = new Int32();
playerId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_skill_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skill = new P_Skill();
curIndex = skill.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_gamerChanges_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gamerChanges = new List<P_GamerStateChange>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_GamerStateChange curTarget = new P_GamerStateChange();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
gamerChanges.Add(curTarget);
}
}return curIndex;
}
private int set_targetX_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
targetX = new Single();
targetX = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_targetY_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
targetY = new Single();
targetY = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(playerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_playerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(skill !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skill_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gamerChanges !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gamerChanges_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(targetX !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_targetX_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(targetY !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_targetY_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_skill_fromBuf(sourceBuf,startOffset);
startOffset = set_gamerChanges_fromBuf(sourceBuf,startOffset);
startOffset = set_targetX_fromBuf(sourceBuf,startOffset);
startOffset = set_targetY_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_skill_json(){
if(skill==null){return "";}String resultJson = "\"skill\":";resultJson += ((LantisBitProtocolBase)skill).SerializerJson();return resultJson;
}


public String get_gamerChanges_json(){
if(gamerChanges==null){return "";}String resultJson = "\"gamerChanges\":";resultJson += "[";
List<P_GamerStateChange> listObj = (List<P_GamerStateChange>)gamerChanges;
for(int i = 0;i < listObj.Count;++i){
P_GamerStateChange item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_targetX_json(){
if(targetX==null){return "";}String resultJson = "\"targetX\":";resultJson += "\"";resultJson += targetX.ToString();resultJson += "\"";return resultJson;
}


public String get_targetY_json(){
if(targetY==null){return "";}String resultJson = "\"targetY\":";resultJson += "\"";resultJson += targetY.ToString();resultJson += "\"";return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_skill_fromJson(LitJson.JsonData jsonObj){
skill= new P_Skill();
skill.DeserializerJson(jsonObj.ToJson());}


public void set_gamerChanges_fromJson(LitJson.JsonData jsonObj){
gamerChanges = new List<P_GamerStateChange>();
foreach (LitJson.JsonData item in jsonObj){
P_GamerStateChange addB = new P_GamerStateChange();
gamerChanges.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_targetX_fromJson(LitJson.JsonData jsonObj){
targetX= Single.Parse(jsonObj.ToString());
}


public void set_targetY_fromJson(LitJson.JsonData jsonObj){
targetY= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(skill !=  null){
resultStr += ",";resultStr += get_skill_json();
}
else {}if(gamerChanges !=  null){
resultStr += ",";resultStr += get_gamerChanges_json();
}
else {}if(targetX !=  null){
resultStr += ",";resultStr += get_targetX_json();
}
else {}if(targetY !=  null){
resultStr += ",";resultStr += get_targetY_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["skill"] != null){
set_skill_fromJson(jsonObj["skill"]);
}
if(jsonObj["gamerChanges"] != null){
set_gamerChanges_fromJson(jsonObj["gamerChanges"]);
}
if(jsonObj["targetX"] != null){
set_targetX_fromJson(jsonObj["targetX"]);
}
if(jsonObj["targetY"] != null){
set_targetY_fromJson(jsonObj["targetY"]);
}
}
}
}
