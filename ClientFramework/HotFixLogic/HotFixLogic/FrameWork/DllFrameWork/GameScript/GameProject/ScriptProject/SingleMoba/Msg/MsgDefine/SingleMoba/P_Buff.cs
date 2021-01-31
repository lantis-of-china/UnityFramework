// 此文件由协议导出插件自动生成
// ID : 00000]
//****玩家离开房间****
using System;
using System.Collections.Generic;
using System.IO;


namespace SingleMoba{
/// <summary>
///玩家离开房间
/// <\summary>
public class P_Buff : CherishBitProtocolBase {
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
public Single recordLife;
/// <summary>
///
/// <\summary>
public List<Int16> recordConditionTypes;
/// <summary>
///
/// <\summary>
public List<Int16> recordConditionValues;
public P_Buff(){}

public P_Buff(Int32 _id, Int32 _cgfId, Single _recordLife, List<Int16> _recordConditionTypes, List<Int16> _recordConditionValues){
this.id = _id;
this.cgfId = _cgfId;
this.recordLife = _recordLife;
this.recordConditionTypes = _recordConditionTypes;
this.recordConditionValues = _recordConditionValues;
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


private Byte[] get_recordLife_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)recordLife);
return outBuf;
}


private Byte[] get_recordConditionTypes_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<short> listShort = (List<short>)recordConditionTypes;
memoryWrite.Write(BitConverter.GetBytes(listShort.Count),0,4);
for(int i = 0;i < listShort.Count;++i){
short shr = listShort[i];
memoryWrite.Write(BitConverter.GetBytes(shr),0,2);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_recordConditionValues_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<short> listShort = (List<short>)recordConditionValues;
memoryWrite.Write(BitConverter.GetBytes(listShort.Count),0,4);
for(int i = 0;i < listShort.Count;++i){
short shr = listShort[i];
memoryWrite.Write(BitConverter.GetBytes(shr),0,2);
}
outBuf = memoryWrite.ToArray();
}
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
private int set_recordLife_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recordLife = new Single();
recordLife = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_recordConditionTypes_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recordConditionTypes = new List<Int16>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int16 curTarget = BitConverter.ToInt16(sourceBuf,curIndex);
recordConditionTypes.Add(curTarget);
curIndex += 2;
}
}return curIndex;
}
private int set_recordConditionValues_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recordConditionValues = new List<Int16>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int16 curTarget = BitConverter.ToInt16(sourceBuf,curIndex);
recordConditionValues.Add(curTarget);
curIndex += 2;
}
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
}if(recordLife !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recordLife_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(recordConditionTypes !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recordConditionTypes_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(recordConditionValues !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recordConditionValues_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_cgfId_fromBuf(sourceBuf,startOffset);
startOffset = set_recordLife_fromBuf(sourceBuf,startOffset);
startOffset = set_recordConditionTypes_fromBuf(sourceBuf,startOffset);
startOffset = set_recordConditionValues_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_id_json(){
if(id==null){return "";}String resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public String get_cgfId_json(){
if(cgfId==null){return "";}String resultJson = "\"cgfId\":";resultJson += "\"";resultJson += cgfId.ToString();resultJson += "\"";return resultJson;
}


public String get_recordLife_json(){
if(recordLife==null){return "";}String resultJson = "\"recordLife\":";resultJson += "\"";resultJson += recordLife.ToString();resultJson += "\"";return resultJson;
}


public String get_recordConditionTypes_json(){
if(recordConditionTypes==null){return "";}String resultJson = "\"recordConditionTypes\":";resultJson += "[";List<Int16> listObj = (List<Int16>)recordConditionTypes;
for(int i = 0;i < listObj.Count;++i){
Int16 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_recordConditionValues_json(){
if(recordConditionValues==null){return "";}String resultJson = "\"recordConditionValues\":";resultJson += "[";List<Int16> listObj = (List<Int16>)recordConditionValues;
for(int i = 0;i < listObj.Count;++i){
Int16 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= Int32.Parse(jsonObj.ToString());
}


public void set_cgfId_fromJson(LitJson.JsonData jsonObj){
cgfId= Int32.Parse(jsonObj.ToString());
}


public void set_recordLife_fromJson(LitJson.JsonData jsonObj){
recordLife= Single.Parse(jsonObj.ToString());
}


public void set_recordConditionTypes_fromJson(LitJson.JsonData jsonObj){
recordConditionTypes= new List<Int16>();
foreach(LitJson.JsonData jsonItem in jsonObj){
recordConditionTypes.Add(Int16.Parse(jsonItem.ToString()));}

}


public void set_recordConditionValues_fromJson(LitJson.JsonData jsonObj){
recordConditionValues= new List<Int16>();
foreach(LitJson.JsonData jsonItem in jsonObj){
recordConditionValues.Add(Int16.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(id !=  null){
resultStr += get_id_json();
}
else {}if(cgfId !=  null){
resultStr += ",";resultStr += get_cgfId_json();
}
else {}if(recordLife !=  null){
resultStr += ",";resultStr += get_recordLife_json();
}
else {}if(recordConditionTypes !=  null){
resultStr += ",";resultStr += get_recordConditionTypes_json();
}
else {}if(recordConditionValues !=  null){
resultStr += ",";resultStr += get_recordConditionValues_json();
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
if(jsonObj["recordLife"] != null){
set_recordLife_fromJson(jsonObj["recordLife"]);
}
if(jsonObj["recordConditionTypes"] != null){
set_recordConditionTypes_fromJson(jsonObj["recordConditionTypes"]);
}
if(jsonObj["recordConditionValues"] != null){
set_recordConditionValues_fromJson(jsonObj["recordConditionValues"]);
}
}
}
}
