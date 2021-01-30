// 此文件由协议导出插件自动生成
// ID : 00000]
//****技能命中****
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
///技能命中
/// <\summary>
public class CS_HitSkill : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///
/// <\summary>
public Int32 skillId;
/// <summary>
///
/// <\summary>
public Int32 hitPlayerId;
/// <summary>
///
/// <\summary>
public Single skillX;
/// <summary>
///
/// <\summary>
public Single skillY;
public CS_HitSkill(){}

public CS_HitSkill(UserValiadateInfor _UserValiadate, Int32 _skillId, Int32 _hitPlayerId, Single _skillX, Single _skillY){
this.UserValiadate = _UserValiadate;
this.skillId = _skillId;
this.hitPlayerId = _hitPlayerId;
this.skillX = _skillX;
this.skillY = _skillY;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
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

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(skillId !=  null){
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_skillId_fromBuf(sourceBuf,startOffset);
startOffset = set_hitPlayerId_fromBuf(sourceBuf,startOffset);
startOffset = set_skillX_fromBuf(sourceBuf,startOffset);
startOffset = set_skillY_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_skillId_json(){
if(skillId==null){return "";}String resultJson = "\"skillId\":";resultJson += "\"";resultJson += skillId.ToString();resultJson += "\"";return resultJson;
}


public String get_hitPlayerId_json(){
if(hitPlayerId==null){return "";}String resultJson = "\"hitPlayerId\":";resultJson += "\"";resultJson += hitPlayerId.ToString();resultJson += "\"";return resultJson;
}


public String get_skillX_json(){
if(skillX==null){return "";}String resultJson = "\"skillX\":";resultJson += "\"";resultJson += skillX.ToString();resultJson += "\"";return resultJson;
}


public String get_skillY_json(){
if(skillY==null){return "";}String resultJson = "\"skillY\":";resultJson += "\"";resultJson += skillY.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_skillId_fromJson(LitJson.JsonData jsonObj){
skillId= Int32.Parse(jsonObj.ToString());
}


public void set_hitPlayerId_fromJson(LitJson.JsonData jsonObj){
hitPlayerId= Int32.Parse(jsonObj.ToString());
}


public void set_skillX_fromJson(LitJson.JsonData jsonObj){
skillX= Single.Parse(jsonObj.ToString());
}


public void set_skillY_fromJson(LitJson.JsonData jsonObj){
skillY= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(skillId !=  null){
resultStr += ",";resultStr += get_skillId_json();
}
else {}if(hitPlayerId !=  null){
resultStr += ",";resultStr += get_hitPlayerId_json();
}
else {}if(skillX !=  null){
resultStr += ",";resultStr += get_skillX_json();
}
else {}if(skillY !=  null){
resultStr += ",";resultStr += get_skillY_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["skillId"] != null){
set_skillId_fromJson(jsonObj["skillId"]);
}
if(jsonObj["hitPlayerId"] != null){
set_hitPlayerId_fromJson(jsonObj["hitPlayerId"]);
}
if(jsonObj["skillX"] != null){
set_skillX_fromJson(jsonObj["skillX"]);
}
if(jsonObj["skillY"] != null){
set_skillY_fromJson(jsonObj["skillY"]);
}
}
}
}
