// 此文件由协议导出插件自动生成
// ID : 00000]
//****技能信息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///技能信息
/// <\summary>
public class P_Skill : LantisBitProtocolBase {
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
public Single cdTime;
/// <summary>
///
/// <\summary>
public Int32 endTakeEventTimesRecord;
/// <summary>
///
/// <\summary>
public Int32 moveHitCount;
/// <summary>
///
/// <\summary>
public Single startX;
/// <summary>
///
/// <\summary>
public Single startY;
/// <summary>
///
/// <\summary>
public Single targetX;
/// <summary>
///
/// <\summary>
public Single targetY;
/// <summary>
///
/// <\summary>
public Byte state;
public P_Skill(){}

public P_Skill(Int32 _id, Int32 _cgfId, Int32 _bindUserId, Int64 _beginTime, Single _cdTime, Int32 _endTakeEventTimesRecord, Int32 _moveHitCount, Single _startX, Single _startY, Single _targetX, Single _targetY, Byte _state){
this.id = _id;
this.cgfId = _cgfId;
this.bindUserId = _bindUserId;
this.beginTime = _beginTime;
this.cdTime = _cdTime;
this.endTakeEventTimesRecord = _endTakeEventTimesRecord;
this.moveHitCount = _moveHitCount;
this.startX = _startX;
this.startY = _startY;
this.targetX = _targetX;
this.targetY = _targetY;
this.state = _state;
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


private Byte[] get_cdTime_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)cdTime);
return outBuf;
}


private Byte[] get_endTakeEventTimesRecord_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)endTakeEventTimesRecord);
return outBuf;
}


private Byte[] get_moveHitCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)moveHitCount);
return outBuf;
}


private Byte[] get_startX_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)startX);
return outBuf;
}


private Byte[] get_startY_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)startY);
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


private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)state;
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
private int set_cdTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
cdTime = new Single();
cdTime = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_endTakeEventTimesRecord_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
endTakeEventTimesRecord = new Int32();
endTakeEventTimesRecord = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_moveHitCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
moveHitCount = new Int32();
moveHitCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_startX_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
startX = new Single();
startX = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_startY_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
startY = new Single();
startY = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
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
private int set_state_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new Byte();
state = sourceBuf[curIndex];
curIndex++;
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
}if(cdTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_cdTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(endTakeEventTimesRecord !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_endTakeEventTimesRecord_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(moveHitCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_moveHitCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(startX !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_startX_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(startY !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_startY_encoding();
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
}if(state !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_state_encoding();
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
startOffset = set_cdTime_fromBuf(sourceBuf,startOffset);
startOffset = set_endTakeEventTimesRecord_fromBuf(sourceBuf,startOffset);
startOffset = set_moveHitCount_fromBuf(sourceBuf,startOffset);
startOffset = set_startX_fromBuf(sourceBuf,startOffset);
startOffset = set_startY_fromBuf(sourceBuf,startOffset);
startOffset = set_targetX_fromBuf(sourceBuf,startOffset);
startOffset = set_targetY_fromBuf(sourceBuf,startOffset);
startOffset = set_state_fromBuf(sourceBuf,startOffset);
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


public String get_cdTime_json(){
if(cdTime==null){return "";}String resultJson = "\"cdTime\":";resultJson += "\"";resultJson += cdTime.ToString();resultJson += "\"";return resultJson;
}


public String get_endTakeEventTimesRecord_json(){
if(endTakeEventTimesRecord==null){return "";}String resultJson = "\"endTakeEventTimesRecord\":";resultJson += "\"";resultJson += endTakeEventTimesRecord.ToString();resultJson += "\"";return resultJson;
}


public String get_moveHitCount_json(){
if(moveHitCount==null){return "";}String resultJson = "\"moveHitCount\":";resultJson += "\"";resultJson += moveHitCount.ToString();resultJson += "\"";return resultJson;
}


public String get_startX_json(){
if(startX==null){return "";}String resultJson = "\"startX\":";resultJson += "\"";resultJson += startX.ToString();resultJson += "\"";return resultJson;
}


public String get_startY_json(){
if(startY==null){return "";}String resultJson = "\"startY\":";resultJson += "\"";resultJson += startY.ToString();resultJson += "\"";return resultJson;
}


public String get_targetX_json(){
if(targetX==null){return "";}String resultJson = "\"targetX\":";resultJson += "\"";resultJson += targetX.ToString();resultJson += "\"";return resultJson;
}


public String get_targetY_json(){
if(targetY==null){return "";}String resultJson = "\"targetY\":";resultJson += "\"";resultJson += targetY.ToString();resultJson += "\"";return resultJson;
}


public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
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


public void set_cdTime_fromJson(LitJson.JsonData jsonObj){
cdTime= Single.Parse(jsonObj.ToString());
}


public void set_endTakeEventTimesRecord_fromJson(LitJson.JsonData jsonObj){
endTakeEventTimesRecord= Int32.Parse(jsonObj.ToString());
}


public void set_moveHitCount_fromJson(LitJson.JsonData jsonObj){
moveHitCount= Int32.Parse(jsonObj.ToString());
}


public void set_startX_fromJson(LitJson.JsonData jsonObj){
startX= Single.Parse(jsonObj.ToString());
}


public void set_startY_fromJson(LitJson.JsonData jsonObj){
startY= Single.Parse(jsonObj.ToString());
}


public void set_targetX_fromJson(LitJson.JsonData jsonObj){
targetX= Single.Parse(jsonObj.ToString());
}


public void set_targetY_fromJson(LitJson.JsonData jsonObj){
targetY= Single.Parse(jsonObj.ToString());
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Byte.Parse(jsonObj.ToString());
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
else {}if(cdTime !=  null){
resultStr += ",";resultStr += get_cdTime_json();
}
else {}if(endTakeEventTimesRecord !=  null){
resultStr += ",";resultStr += get_endTakeEventTimesRecord_json();
}
else {}if(moveHitCount !=  null){
resultStr += ",";resultStr += get_moveHitCount_json();
}
else {}if(startX !=  null){
resultStr += ",";resultStr += get_startX_json();
}
else {}if(startY !=  null){
resultStr += ",";resultStr += get_startY_json();
}
else {}if(targetX !=  null){
resultStr += ",";resultStr += get_targetX_json();
}
else {}if(targetY !=  null){
resultStr += ",";resultStr += get_targetY_json();
}
else {}if(state !=  null){
resultStr += ",";resultStr += get_state_json();
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
if(jsonObj["cdTime"] != null){
set_cdTime_fromJson(jsonObj["cdTime"]);
}
if(jsonObj["endTakeEventTimesRecord"] != null){
set_endTakeEventTimesRecord_fromJson(jsonObj["endTakeEventTimesRecord"]);
}
if(jsonObj["moveHitCount"] != null){
set_moveHitCount_fromJson(jsonObj["moveHitCount"]);
}
if(jsonObj["startX"] != null){
set_startX_fromJson(jsonObj["startX"]);
}
if(jsonObj["startY"] != null){
set_startY_fromJson(jsonObj["startY"]);
}
if(jsonObj["targetX"] != null){
set_targetX_fromJson(jsonObj["targetX"]);
}
if(jsonObj["targetY"] != null){
set_targetY_fromJson(jsonObj["targetY"]);
}
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
}
}
}
