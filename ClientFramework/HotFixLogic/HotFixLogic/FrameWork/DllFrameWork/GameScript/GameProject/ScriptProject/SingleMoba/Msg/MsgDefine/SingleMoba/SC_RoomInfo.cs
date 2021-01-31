// 此文件由协议导出插件自动生成
// ID : 00000]
//********
using System;
using System.Collections.Generic;
using System.IO;


namespace SingleMoba{
/// <summary>
///
/// <\summary>
public class SC_RoomInfo : CherishBitProtocolBase {
/// <summary>
///房间ID
/// <\summary>
public Int32 roomId;
/// <summary>
///房间参数
/// <\summary>
public List<Int32> paramarsList;
/// <summary>
///房间中的玩家
/// <\summary>
public List<P_PlayerInfo> roomPlayerInfoList;
/// <summary>
///
/// <\summary>
public List<P_Prop> props;
/// <summary>
///
/// <\summary>
public List<P_Skill> skills;
/// <summary>
///
/// <\summary>
public List<P_SkillBuff> skillBuffs;
public SC_RoomInfo(){}

public SC_RoomInfo(Int32 _roomId, List<Int32> _paramarsList, List<P_PlayerInfo> _roomPlayerInfoList, List<P_Prop> _props, List<P_Skill> _skills, List<P_SkillBuff> _skillBuffs){
this.roomId = _roomId;
this.paramarsList = _paramarsList;
this.roomPlayerInfoList = _roomPlayerInfoList;
this.props = _props;
this.skills = _skills;
this.skillBuffs = _skillBuffs;
}
private Byte[] get_roomId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roomId);
return outBuf;
}


private Byte[] get_paramarsList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)paramarsList;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_roomPlayerInfoList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_PlayerInfo> listBase = roomPlayerInfoList;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_props_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_Prop> listBase = props;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_skills_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_Skill> listBase = skills;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_skillBuffs_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_SkillBuff> listBase = skillBuffs;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_roomId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomId = new Int32();
roomId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramarsList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramarsList = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
paramarsList.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
private int set_roomPlayerInfoList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomPlayerInfoList = new List<P_PlayerInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_PlayerInfo curTarget = new P_PlayerInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
roomPlayerInfoList.Add(curTarget);
}
}return curIndex;
}
private int set_props_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
props = new List<P_Prop>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_Prop curTarget = new P_Prop();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
props.Add(curTarget);
}
}return curIndex;
}
private int set_skills_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skills = new List<P_Skill>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_Skill curTarget = new P_Skill();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
skills.Add(curTarget);
}
}return curIndex;
}
private int set_skillBuffs_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skillBuffs = new List<P_SkillBuff>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_SkillBuff curTarget = new P_SkillBuff();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
skillBuffs.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(roomId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramarsList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramarsList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(roomPlayerInfoList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomPlayerInfoList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(props !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_props_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(skills !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skills_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(skillBuffs !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skillBuffs_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_roomId_fromBuf(sourceBuf,startOffset);
startOffset = set_paramarsList_fromBuf(sourceBuf,startOffset);
startOffset = set_roomPlayerInfoList_fromBuf(sourceBuf,startOffset);
startOffset = set_props_fromBuf(sourceBuf,startOffset);
startOffset = set_skills_fromBuf(sourceBuf,startOffset);
startOffset = set_skillBuffs_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_roomId_json(){
if(roomId==null){return "";}String resultJson = "\"roomId\":";resultJson += "\"";resultJson += roomId.ToString();resultJson += "\"";return resultJson;
}


public String get_paramarsList_json(){
if(paramarsList==null){return "";}String resultJson = "\"paramarsList\":";resultJson += "[";List<Int32> listObj = (List<Int32>)paramarsList;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_roomPlayerInfoList_json(){
if(roomPlayerInfoList==null){return "";}String resultJson = "\"roomPlayerInfoList\":";resultJson += "[";
List<P_PlayerInfo> listObj = (List<P_PlayerInfo>)roomPlayerInfoList;
for(int i = 0;i < listObj.Count;++i){
P_PlayerInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_props_json(){
if(props==null){return "";}String resultJson = "\"props\":";resultJson += "[";
List<P_Prop> listObj = (List<P_Prop>)props;
for(int i = 0;i < listObj.Count;++i){
P_Prop item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_skills_json(){
if(skills==null){return "";}String resultJson = "\"skills\":";resultJson += "[";
List<P_Skill> listObj = (List<P_Skill>)skills;
for(int i = 0;i < listObj.Count;++i){
P_Skill item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_skillBuffs_json(){
if(skillBuffs==null){return "";}String resultJson = "\"skillBuffs\":";resultJson += "[";
List<P_SkillBuff> listObj = (List<P_SkillBuff>)skillBuffs;
for(int i = 0;i < listObj.Count;++i){
P_SkillBuff item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_roomId_fromJson(LitJson.JsonData jsonObj){
roomId= Int32.Parse(jsonObj.ToString());
}


public void set_paramarsList_fromJson(LitJson.JsonData jsonObj){
paramarsList= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
paramarsList.Add(Int32.Parse(jsonItem.ToString()));}

}


public void set_roomPlayerInfoList_fromJson(LitJson.JsonData jsonObj){
roomPlayerInfoList = new List<P_PlayerInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_PlayerInfo addB = new P_PlayerInfo();
roomPlayerInfoList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_props_fromJson(LitJson.JsonData jsonObj){
props = new List<P_Prop>();
foreach (LitJson.JsonData item in jsonObj){
P_Prop addB = new P_Prop();
props.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_skills_fromJson(LitJson.JsonData jsonObj){
skills = new List<P_Skill>();
foreach (LitJson.JsonData item in jsonObj){
P_Skill addB = new P_Skill();
skills.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_skillBuffs_fromJson(LitJson.JsonData jsonObj){
skillBuffs = new List<P_SkillBuff>();
foreach (LitJson.JsonData item in jsonObj){
P_SkillBuff addB = new P_SkillBuff();
skillBuffs.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(roomId !=  null){
resultStr += get_roomId_json();
}
else {}if(paramarsList !=  null){
resultStr += ",";resultStr += get_paramarsList_json();
}
else {}if(roomPlayerInfoList !=  null){
resultStr += ",";resultStr += get_roomPlayerInfoList_json();
}
else {}if(props !=  null){
resultStr += ",";resultStr += get_props_json();
}
else {}if(skills !=  null){
resultStr += ",";resultStr += get_skills_json();
}
else {}if(skillBuffs !=  null){
resultStr += ",";resultStr += get_skillBuffs_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roomId"] != null){
set_roomId_fromJson(jsonObj["roomId"]);
}
if(jsonObj["paramarsList"] != null){
set_paramarsList_fromJson(jsonObj["paramarsList"]);
}
if(jsonObj["roomPlayerInfoList"] != null){
set_roomPlayerInfoList_fromJson(jsonObj["roomPlayerInfoList"]);
}
if(jsonObj["props"] != null){
set_props_fromJson(jsonObj["props"]);
}
if(jsonObj["skills"] != null){
set_skills_fromJson(jsonObj["skills"]);
}
if(jsonObj["skillBuffs"] != null){
set_skillBuffs_fromJson(jsonObj["skillBuffs"]);
}
}
}
}
