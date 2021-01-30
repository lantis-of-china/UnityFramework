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
public class SC_SkillBuffTrigger : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public Int32 buffId;
/// <summary>
///
/// <\summary>
public Int32 buffEventType;
/// <summary>
///
/// <\summary>
public Int32 paramar1;
/// <summary>
///
/// <\summary>
public Int32 paramar2;
/// <summary>
///
/// <\summary>
public Int32 paramar3;
/// <summary>
///
/// <\summary>
public Single paramar4;
/// <summary>
///
/// <\summary>
public List<Int32> paramar5;
public SC_SkillBuffTrigger(){}

public SC_SkillBuffTrigger(Int32 _playerId, Int32 _buffId, Int32 _buffEventType, Int32 _paramar1, Int32 _paramar2, Int32 _paramar3, Single _paramar4, List<Int32> _paramar5){
this.playerId = _playerId;
this.buffId = _buffId;
this.buffEventType = _buffEventType;
this.paramar1 = _paramar1;
this.paramar2 = _paramar2;
this.paramar3 = _paramar3;
this.paramar4 = _paramar4;
this.paramar5 = _paramar5;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
return outBuf;
}


private Byte[] get_buffId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)buffId);
return outBuf;
}


private Byte[] get_buffEventType_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)buffEventType);
return outBuf;
}


private Byte[] get_paramar1_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)paramar1);
return outBuf;
}


private Byte[] get_paramar2_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)paramar2);
return outBuf;
}


private Byte[] get_paramar3_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)paramar3);
return outBuf;
}


private Byte[] get_paramar4_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)paramar4);
return outBuf;
}


private Byte[] get_paramar5_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)paramar5;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
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
private int set_buffId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
buffId = new Int32();
buffId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_buffEventType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
buffEventType = new Int32();
buffEventType = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramar1_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramar1 = new Int32();
paramar1 = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramar2_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramar2 = new Int32();
paramar2 = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramar3_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramar3 = new Int32();
paramar3 = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramar4_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramar4 = new Single();
paramar4 = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramar5_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramar5 = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
paramar5.Add(curTarget);
curIndex += 4;
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
}if(buffId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_buffId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(buffEventType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_buffEventType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramar1 !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramar1_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramar2 !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramar2_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramar3 !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramar3_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramar4 !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramar4_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramar5 !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramar5_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_buffId_fromBuf(sourceBuf,startOffset);
startOffset = set_buffEventType_fromBuf(sourceBuf,startOffset);
startOffset = set_paramar1_fromBuf(sourceBuf,startOffset);
startOffset = set_paramar2_fromBuf(sourceBuf,startOffset);
startOffset = set_paramar3_fromBuf(sourceBuf,startOffset);
startOffset = set_paramar4_fromBuf(sourceBuf,startOffset);
startOffset = set_paramar5_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_buffId_json(){
if(buffId==null){return "";}String resultJson = "\"buffId\":";resultJson += "\"";resultJson += buffId.ToString();resultJson += "\"";return resultJson;
}


public String get_buffEventType_json(){
if(buffEventType==null){return "";}String resultJson = "\"buffEventType\":";resultJson += "\"";resultJson += buffEventType.ToString();resultJson += "\"";return resultJson;
}


public String get_paramar1_json(){
if(paramar1==null){return "";}String resultJson = "\"paramar1\":";resultJson += "\"";resultJson += paramar1.ToString();resultJson += "\"";return resultJson;
}


public String get_paramar2_json(){
if(paramar2==null){return "";}String resultJson = "\"paramar2\":";resultJson += "\"";resultJson += paramar2.ToString();resultJson += "\"";return resultJson;
}


public String get_paramar3_json(){
if(paramar3==null){return "";}String resultJson = "\"paramar3\":";resultJson += "\"";resultJson += paramar3.ToString();resultJson += "\"";return resultJson;
}


public String get_paramar4_json(){
if(paramar4==null){return "";}String resultJson = "\"paramar4\":";resultJson += "\"";resultJson += paramar4.ToString();resultJson += "\"";return resultJson;
}


public String get_paramar5_json(){
if(paramar5==null){return "";}String resultJson = "\"paramar5\":";resultJson += "[";List<Int32> listObj = (List<Int32>)paramar5;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_buffId_fromJson(LitJson.JsonData jsonObj){
buffId= Int32.Parse(jsonObj.ToString());
}


public void set_buffEventType_fromJson(LitJson.JsonData jsonObj){
buffEventType= Int32.Parse(jsonObj.ToString());
}


public void set_paramar1_fromJson(LitJson.JsonData jsonObj){
paramar1= Int32.Parse(jsonObj.ToString());
}


public void set_paramar2_fromJson(LitJson.JsonData jsonObj){
paramar2= Int32.Parse(jsonObj.ToString());
}


public void set_paramar3_fromJson(LitJson.JsonData jsonObj){
paramar3= Int32.Parse(jsonObj.ToString());
}


public void set_paramar4_fromJson(LitJson.JsonData jsonObj){
paramar4= Single.Parse(jsonObj.ToString());
}


public void set_paramar5_fromJson(LitJson.JsonData jsonObj){
paramar5= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
paramar5.Add(Int32.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(buffId !=  null){
resultStr += ",";resultStr += get_buffId_json();
}
else {}if(buffEventType !=  null){
resultStr += ",";resultStr += get_buffEventType_json();
}
else {}if(paramar1 !=  null){
resultStr += ",";resultStr += get_paramar1_json();
}
else {}if(paramar2 !=  null){
resultStr += ",";resultStr += get_paramar2_json();
}
else {}if(paramar3 !=  null){
resultStr += ",";resultStr += get_paramar3_json();
}
else {}if(paramar4 !=  null){
resultStr += ",";resultStr += get_paramar4_json();
}
else {}if(paramar5 !=  null){
resultStr += ",";resultStr += get_paramar5_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["buffId"] != null){
set_buffId_fromJson(jsonObj["buffId"]);
}
if(jsonObj["buffEventType"] != null){
set_buffEventType_fromJson(jsonObj["buffEventType"]);
}
if(jsonObj["paramar1"] != null){
set_paramar1_fromJson(jsonObj["paramar1"]);
}
if(jsonObj["paramar2"] != null){
set_paramar2_fromJson(jsonObj["paramar2"]);
}
if(jsonObj["paramar3"] != null){
set_paramar3_fromJson(jsonObj["paramar3"]);
}
if(jsonObj["paramar4"] != null){
set_paramar4_fromJson(jsonObj["paramar4"]);
}
if(jsonObj["paramar5"] != null){
set_paramar5_fromJson(jsonObj["paramar5"]);
}
}
}
}
