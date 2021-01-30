// 此文件由协议导出插件自动生成
// ID : 00000]
//****玩家离开房间****
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
///玩家离开房间
/// <\summary>
public class P_SkillBuff : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 id;
/// <summary>
///
/// <\summary>
public Int32 cgfId;
/// <summary>
///
/// <\summary>
public Int32 bindUserId;
/// <summary>
///
/// <\summary>
public Int64 beginTime;
/// <summary>
///
/// <\summary>
public List<Int32> recordTypes;
/// <summary>
///
/// <\summary>
public List<Int32> recordValues;
/// <summary>
///
/// <\summary>
public Int32 eventTimesRecord;
public P_SkillBuff(){}

public P_SkillBuff(Int32 _id, Int32 _cgfId, Int32 _bindUserId, Int64 _beginTime, List<Int32> _recordTypes, List<Int32> _recordValues, Int32 _eventTimesRecord){
this.id = _id;
this.cgfId = _cgfId;
this.bindUserId = _bindUserId;
this.beginTime = _beginTime;
this.recordTypes = _recordTypes;
this.recordValues = _recordValues;
this.eventTimesRecord = _eventTimesRecord;
}
private Byte[] get_id_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)id);
return outBuf;
}


private Byte[] get_cgfId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)cgfId);
return outBuf;
}


private Byte[] get_bindUserId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)bindUserId);
return outBuf;
}


private Byte[] get_beginTime_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)beginTime);
return outBuf;
}


private Byte[] get_recordTypes_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)recordTypes;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_recordValues_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)recordValues;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_eventTimesRecord_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)eventTimesRecord);
return outBuf;
}

private int set_id_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
id = new Int32();
id = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_cgfId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
cgfId = new Int32();
cgfId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_bindUserId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
bindUserId = new Int32();
bindUserId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_beginTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
beginTime = new Int64();
beginTime = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_recordTypes_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recordTypes = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
recordTypes.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
private int set_recordValues_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recordValues = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
recordValues.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
private int set_eventTimesRecord_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
eventTimesRecord = new Int32();
eventTimesRecord = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(id !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_id_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(cgfId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_cgfId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(bindUserId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_bindUserId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(beginTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_beginTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(recordTypes !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recordTypes_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(recordValues !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recordValues_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(eventTimesRecord !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_eventTimesRecord_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_cgfId_fromBuf(sourceBuf,startOffset);
startOffset = set_bindUserId_fromBuf(sourceBuf,startOffset);
startOffset = set_beginTime_fromBuf(sourceBuf,startOffset);
startOffset = set_recordTypes_fromBuf(sourceBuf,startOffset);
startOffset = set_recordValues_fromBuf(sourceBuf,startOffset);
startOffset = set_eventTimesRecord_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_id_json(){
if(id==null){return "";}String resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public String get_cgfId_json(){
if(cgfId==null){return "";}String resultJson = "\"cgfId\":";resultJson += "\"";resultJson += cgfId.ToString();resultJson += "\"";return resultJson;
}


public String get_bindUserId_json(){
if(bindUserId==null){return "";}String resultJson = "\"bindUserId\":";resultJson += "\"";resultJson += bindUserId.ToString();resultJson += "\"";return resultJson;
}


public String get_beginTime_json(){
if(beginTime==null){return "";}String resultJson = "\"beginTime\":";resultJson += "\"";resultJson += beginTime.ToString();resultJson += "\"";return resultJson;
}


public String get_recordTypes_json(){
if(recordTypes==null){return "";}String resultJson = "\"recordTypes\":";resultJson += "[";List<Int32> listObj = (List<Int32>)recordTypes;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_recordValues_json(){
if(recordValues==null){return "";}String resultJson = "\"recordValues\":";resultJson += "[";List<Int32> listObj = (List<Int32>)recordValues;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_eventTimesRecord_json(){
if(eventTimesRecord==null){return "";}String resultJson = "\"eventTimesRecord\":";resultJson += "\"";resultJson += eventTimesRecord.ToString();resultJson += "\"";return resultJson;
}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= Int32.Parse(jsonObj.ToString());
}


public void set_cgfId_fromJson(LitJson.JsonData jsonObj){
cgfId= Int32.Parse(jsonObj.ToString());
}


public void set_bindUserId_fromJson(LitJson.JsonData jsonObj){
bindUserId= Int32.Parse(jsonObj.ToString());
}


public void set_beginTime_fromJson(LitJson.JsonData jsonObj){
beginTime= Int64.Parse(jsonObj.ToString());
}


public void set_recordTypes_fromJson(LitJson.JsonData jsonObj){
recordTypes= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
recordTypes.Add(Int32.Parse(jsonItem.ToString()));}

}


public void set_recordValues_fromJson(LitJson.JsonData jsonObj){
recordValues= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
recordValues.Add(Int32.Parse(jsonItem.ToString()));}

}


public void set_eventTimesRecord_fromJson(LitJson.JsonData jsonObj){
eventTimesRecord= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(id !=  null){
resultStr += get_id_json();
}
else {}if(cgfId !=  null){
resultStr += ",";resultStr += get_cgfId_json();
}
else {}if(bindUserId !=  null){
resultStr += ",";resultStr += get_bindUserId_json();
}
else {}if(beginTime !=  null){
resultStr += ",";resultStr += get_beginTime_json();
}
else {}if(recordTypes !=  null){
resultStr += ",";resultStr += get_recordTypes_json();
}
else {}if(recordValues !=  null){
resultStr += ",";resultStr += get_recordValues_json();
}
else {}if(eventTimesRecord !=  null){
resultStr += ",";resultStr += get_eventTimesRecord_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["id"] != null){
set_id_fromJson(jsonObj["id"]);
}
if(jsonObj["cgfId"] != null){
set_cgfId_fromJson(jsonObj["cgfId"]);
}
if(jsonObj["bindUserId"] != null){
set_bindUserId_fromJson(jsonObj["bindUserId"]);
}
if(jsonObj["beginTime"] != null){
set_beginTime_fromJson(jsonObj["beginTime"]);
}
if(jsonObj["recordTypes"] != null){
set_recordTypes_fromJson(jsonObj["recordTypes"]);
}
if(jsonObj["recordValues"] != null){
set_recordValues_fromJson(jsonObj["recordValues"]);
}
if(jsonObj["eventTimesRecord"] != null){
set_eventTimesRecord_fromJson(jsonObj["eventTimesRecord"]);
}
}
}
}
