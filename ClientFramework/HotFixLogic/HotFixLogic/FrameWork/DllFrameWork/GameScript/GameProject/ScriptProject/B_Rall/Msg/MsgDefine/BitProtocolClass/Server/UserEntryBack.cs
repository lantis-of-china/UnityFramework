// 此文件由协议导出插件自动生成
// ID : 00008]
   
//****用户进入服务器返回****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户进入服务器返回
/// <\summary>
public class UserEntryBack : LantisBitProtocolBase {
/// <summary>
///指示是否成功 1 成功 2 账号密码错误 3 验证超时重新登陆
/// <\summary>
public Int32 ResaultState;
/// <summary>
///游戏数据
/// <\summary>
public BaseDataAttribute.GameCoreData _gameCoreData;
/// <summary>
///
/// <\summary>
public Int64 unitxTime;
/// <summary>
///服务器id
/// <\summary>
public String serverId;
/// <summary>
///
/// <\summary>
public List<P_ConditionItem> conditionList;
public UserEntryBack(){}

public UserEntryBack(Int32 _ResaultState, BaseDataAttribute.GameCoreData __gameCoreData, Int64 _unitxTime, String _serverId, List<P_ConditionItem> _conditionList){
this.ResaultState = _ResaultState;
this._gameCoreData = __gameCoreData;
this.unitxTime = _unitxTime;
this.serverId = _serverId;
this.conditionList = _conditionList;
}
private Byte[] get_ResaultState_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)ResaultState);
return outBuf;
}


private Byte[] get__gameCoreData_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)_gameCoreData).Serializer();
return outBuf;
}


private Byte[] get_unitxTime_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)unitxTime);
return outBuf;
}


private Byte[] get_serverId_encoding(){
Byte[] outBuf = null;
String str = (String)serverId;
Char[] charArray = str.ToCharArray();
Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
Byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private Byte[] get_conditionList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_ConditionItem> listBase = conditionList;
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

private int set_ResaultState_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ResaultState = new Int32();
ResaultState = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set__gameCoreData_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_gameCoreData = new BaseDataAttribute.GameCoreData();
curIndex = _gameCoreData.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_unitxTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
unitxTime = new Int64();
unitxTime = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_serverId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
serverId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
serverId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_conditionList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
conditionList = new List<P_ConditionItem>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_ConditionItem curTarget = new P_ConditionItem();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
conditionList.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(ResaultState !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ResaultState_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(_gameCoreData !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__gameCoreData_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(unitxTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_unitxTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(serverId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_serverId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(conditionList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_conditionList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_ResaultState_fromBuf(sourceBuf,startOffset);
startOffset = set__gameCoreData_fromBuf(sourceBuf,startOffset);
startOffset = set_unitxTime_fromBuf(sourceBuf,startOffset);
startOffset = set_serverId_fromBuf(sourceBuf,startOffset);
startOffset = set_conditionList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_ResaultState_json(){
if(ResaultState==null){return "";}String resultJson = "\"ResaultState\":";resultJson += "\"";resultJson += ResaultState.ToString();resultJson += "\"";return resultJson;
}


public String get__gameCoreData_json(){
if(_gameCoreData==null){return "";}String resultJson = "\"_gameCoreData\":";resultJson += ((LantisBitProtocolBase)_gameCoreData).SerializerJson();return resultJson;
}


public String get_unitxTime_json(){
if(unitxTime==null){return "";}String resultJson = "\"unitxTime\":";resultJson += "\"";resultJson += unitxTime.ToString();resultJson += "\"";return resultJson;
}


public String get_serverId_json(){
if(serverId==null){return "";}String resultJson = "\"serverId\":";resultJson += "\"";resultJson += serverId.ToString();resultJson += "\"";return resultJson;
}


public String get_conditionList_json(){
if(conditionList==null){return "";}String resultJson = "\"conditionList\":";resultJson += "[";
List<P_ConditionItem> listObj = (List<P_ConditionItem>)conditionList;
for(int i = 0;i < listObj.Count;++i){
P_ConditionItem item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_ResaultState_fromJson(LitJson.JsonData jsonObj){
ResaultState= Int32.Parse(jsonObj.ToString());
}


public void set__gameCoreData_fromJson(LitJson.JsonData jsonObj){
_gameCoreData= new BaseDataAttribute.GameCoreData();
_gameCoreData.DeserializerJson(jsonObj.ToJson());}


public void set_unitxTime_fromJson(LitJson.JsonData jsonObj){
unitxTime= Int64.Parse(jsonObj.ToString());
}


public void set_serverId_fromJson(LitJson.JsonData jsonObj){
serverId= jsonObj.ToString();
}


public void set_conditionList_fromJson(LitJson.JsonData jsonObj){
conditionList = new List<P_ConditionItem>();
foreach (LitJson.JsonData item in jsonObj){
P_ConditionItem addB = new P_ConditionItem();
conditionList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(ResaultState !=  null){
resultStr += get_ResaultState_json();
}
else {}if(_gameCoreData !=  null){
resultStr += ",";resultStr += get__gameCoreData_json();
}
else {}if(unitxTime !=  null){
resultStr += ",";resultStr += get_unitxTime_json();
}
else {}if(serverId !=  null){
resultStr += ",";resultStr += get_serverId_json();
}
else {}if(conditionList !=  null){
resultStr += ",";resultStr += get_conditionList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["ResaultState"] != null){
set_ResaultState_fromJson(jsonObj["ResaultState"]);
}
if(jsonObj["_gameCoreData"] != null){
set__gameCoreData_fromJson(jsonObj["_gameCoreData"]);
}
if(jsonObj["unitxTime"] != null){
set_unitxTime_fromJson(jsonObj["unitxTime"]);
}
if(jsonObj["serverId"] != null){
set_serverId_fromJson(jsonObj["serverId"]);
}
if(jsonObj["conditionList"] != null){
set_conditionList_fromJson(jsonObj["conditionList"]);
}
}
}
}
