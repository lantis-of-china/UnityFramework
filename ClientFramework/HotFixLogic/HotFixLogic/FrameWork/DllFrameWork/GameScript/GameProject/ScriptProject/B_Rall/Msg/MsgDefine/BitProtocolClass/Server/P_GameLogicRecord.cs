// 此文件由协议导出插件自动生成
// ID : 00001]
//****推送给玩家的战绩消息结构****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///推送给玩家的战绩消息结构
/// <\summary>
public class P_GameLogicRecord : CherishBitProtocolBase {
/// <summary>
///玩家Id
/// <\summary>
public Int32 roleId;
/// <summary>
///时间日期
/// <\summary>
public string timeTicks;
/// <summary>
///改变分值 > 0 胜利 <0 失败  可以是具体分值
/// <\summary>
public Int32 changeCount;
/// <summary>
///对应具体游戏房间类型
/// <\summary>
public Int32 logicType;
/// <summary>
///逻辑数据
/// <\summary>
public List<byte> logicData;
public P_GameLogicRecord(){}

public P_GameLogicRecord(Int32 _roleId, string _timeTicks, Int32 _changeCount, Int32 _logicType, List<byte> _logicData){
this.roleId = _roleId;
this.timeTicks = _timeTicks;
this.changeCount = _changeCount;
this.logicType = _logicType;
this.logicData = _logicData;
}
private byte[] get_roleId_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roleId);
return outBuf;
}


private byte[] get_timeTicks_encoding(){
byte[] outBuf = null;
string str = (string)timeTicks;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_changeCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)changeCount);
return outBuf;
}


private byte[] get_logicType_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)logicType);
return outBuf;
}


private byte[] get_logicData_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<byte> listbyte = (List<byte>)logicData;
memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);
byte[] listBuf = listbyte.ToArray();
memoryWrite.Write(listBuf,0,listBuf.Length);
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_roleId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roleId = new Int32();
roleId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_timeTicks_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
timeTicks = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
timeTicks = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_changeCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
changeCount = new Int32();
changeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_logicType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
logicType = new Int32();
logicType = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_logicData_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
logicData = new List<byte>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
byte[] data = new byte[listCount];
Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);
logicData = new List<byte>(data);
curIndex += listCount;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(roleId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roleId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(timeTicks !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_timeTicks_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(changeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_changeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(logicType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_logicType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(logicData !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_logicData_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_roleId_fromBuf(sourceBuf,startOffset);
startOffset = set_timeTicks_fromBuf(sourceBuf,startOffset);
startOffset = set_changeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_logicType_fromBuf(sourceBuf,startOffset);
startOffset = set_logicData_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_roleId_json(){
if(roleId==null){return "";}string resultJson = "\"roleId\":";resultJson += "\"";resultJson += roleId.ToString();resultJson += "\"";return resultJson;
}


public string get_timeTicks_json(){
if(timeTicks==null){return "";}string resultJson = "\"timeTicks\":";resultJson += "\"";resultJson += timeTicks.ToString();resultJson += "\"";return resultJson;
}


public string get_changeCount_json(){
if(changeCount==null){return "";}string resultJson = "\"changeCount\":";resultJson += "\"";resultJson += changeCount.ToString();resultJson += "\"";return resultJson;
}


public string get_logicType_json(){
if(logicType==null){return "";}string resultJson = "\"logicType\":";resultJson += "\"";resultJson += logicType.ToString();resultJson += "\"";return resultJson;
}


public string get_logicData_json(){
if(logicData==null){return "";}string resultJson = "\"logicData\":";resultJson += "[";List<byte> listObj = (List<byte>)logicData;
for(int i = 0;i < listObj.Count;++i){
byte item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_roleId_fromJson(LitJson.JsonData jsonObj){
roleId= Int32.Parse(jsonObj.ToString());
}


public void set_timeTicks_fromJson(LitJson.JsonData jsonObj){
timeTicks= jsonObj.ToString();
}


public void set_changeCount_fromJson(LitJson.JsonData jsonObj){
changeCount= Int32.Parse(jsonObj.ToString());
}


public void set_logicType_fromJson(LitJson.JsonData jsonObj){
logicType= Int32.Parse(jsonObj.ToString());
}


public void set_logicData_fromJson(LitJson.JsonData jsonObj){
logicData= new List<byte>();
foreach(LitJson.JsonData jsonItem in jsonObj){
logicData.Add(byte.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(roleId !=  null){
resultStr += get_roleId_json();
}
else {}if(timeTicks !=  null){
resultStr += ",";resultStr += get_timeTicks_json();
}
else {}if(changeCount !=  null){
resultStr += ",";resultStr += get_changeCount_json();
}
else {}if(logicType !=  null){
resultStr += ",";resultStr += get_logicType_json();
}
else {}if(logicData !=  null){
resultStr += ",";resultStr += get_logicData_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roleId"] != null){
set_roleId_fromJson(jsonObj["roleId"]);
}
if(jsonObj["timeTicks"] != null){
set_timeTicks_fromJson(jsonObj["timeTicks"]);
}
if(jsonObj["changeCount"] != null){
set_changeCount_fromJson(jsonObj["changeCount"]);
}
if(jsonObj["logicType"] != null){
set_logicType_fromJson(jsonObj["logicType"]);
}
if(jsonObj["logicData"] != null){
set_logicData_fromJson(jsonObj["logicData"]);
}
}
}
}
