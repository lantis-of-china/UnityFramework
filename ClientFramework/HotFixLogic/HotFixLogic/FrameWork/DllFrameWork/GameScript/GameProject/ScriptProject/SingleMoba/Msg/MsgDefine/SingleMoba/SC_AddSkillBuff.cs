// 此文件由协议导出插件自动生成
// ID : 000000]
//********
using System;
using System.Collections.Generic;
using System.IO;
using AskDao;
using BaseDataAttribute;
using Server;
using Baccarat;
using BingShangQuGunQiu;
using BuYu;
using CheXuan;
using CMSloto;
using IMClub;
using LaoHuJi;
using MaJiang_QuanZhou;
using MaJiang_XueZhan;
using PaoDeKuai;
using SingleMoba;
using Template;
using WuXingJingCai;


namespace SingleMoba{
/// <summary>
///
/// <\summary>
public class SC_AddSkillBuff : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public List<P_SkillBuff> getBuffs;
public SC_AddSkillBuff(){}

public SC_AddSkillBuff(Int32 _playerId, List<P_SkillBuff> _getBuffs){
this.playerId = _playerId;
this.getBuffs = _getBuffs;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
return outBuf;
}


private Byte[] get_getBuffs_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_SkillBuff> listBase = getBuffs;
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

private int set_playerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
playerId = new Int32();
playerId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_getBuffs_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
getBuffs = new List<P_SkillBuff>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_SkillBuff curTarget = new P_SkillBuff();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
getBuffs.Add(curTarget);
}
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
}if(getBuffs !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_getBuffs_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_getBuffs_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_getBuffs_json(){
if(getBuffs==null){return "";}String resultJson = "\"getBuffs\":";resultJson += "[";
List<P_SkillBuff> listObj = (List<P_SkillBuff>)getBuffs;
for(int i = 0;i < listObj.Count;++i){
P_SkillBuff item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_getBuffs_fromJson(LitJson.JsonData jsonObj){
getBuffs = new List<P_SkillBuff>();
foreach (LitJson.JsonData item in jsonObj){
P_SkillBuff addB = new P_SkillBuff();
getBuffs.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(getBuffs !=  null){
resultStr += ",";resultStr += get_getBuffs_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["getBuffs"] != null){
set_getBuffs_fromJson(jsonObj["getBuffs"]);
}
}
}
}
