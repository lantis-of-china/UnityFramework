// 此文件由协议导出插件自动生成
// ID : 00001]

//****场次信息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///场次信息
/// <\summary>
public class P_ConditionItem : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 id;
/// <summary>
///
/// <\summary>
public String name;
/// <summary>
///
/// <\summary>
public Int32 minLimit;
/// <summary>
///
/// <\summary>
public Int32 maxLimit;
/// <summary>
///
/// <\summary>
public Byte isEnable;
/// <summary>
///
/// <\summary>
public Int32 cheatRate;
/// <summary>
///
/// <\summary>
public List<Int32> chipList;
public P_ConditionItem(){}

public P_ConditionItem(Int32 _id, String _name, Int32 _minLimit, Int32 _maxLimit, Byte _isEnable, Int32 _cheatRate, List<Int32> _chipList){
this.id = _id;
this.name = _name;
this.minLimit = _minLimit;
this.maxLimit = _maxLimit;
this.isEnable = _isEnable;
this.cheatRate = _cheatRate;
this.chipList = _chipList;
}
private Byte[] get_id_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)id);
return outBuf;
}


private Byte[] get_name_encoding(){
Byte[] outBuf = null;
String str = (String)name;
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


private Byte[] get_minLimit_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)minLimit);
return outBuf;
}


private Byte[] get_maxLimit_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)maxLimit);
return outBuf;
}


private Byte[] get_isEnable_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)isEnable;
return outBuf;
}


private Byte[] get_cheatRate_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)cheatRate);
return outBuf;
}


private Byte[] get_chipList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)chipList;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
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
private int set_name_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
name = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
name = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_minLimit_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
minLimit = new Int32();
minLimit = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_maxLimit_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
maxLimit = new Int32();
maxLimit = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_isEnable_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isEnable = new Byte();
isEnable = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_cheatRate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
cheatRate = new Int32();
cheatRate = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_chipList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
chipList = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
chipList.Add(curTarget);
curIndex += 4;
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
}if(name !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_name_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(minLimit !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_minLimit_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(maxLimit !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_maxLimit_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(isEnable !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_isEnable_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(cheatRate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_cheatRate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(chipList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_chipList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_name_fromBuf(sourceBuf,startOffset);
startOffset = set_minLimit_fromBuf(sourceBuf,startOffset);
startOffset = set_maxLimit_fromBuf(sourceBuf,startOffset);
startOffset = set_isEnable_fromBuf(sourceBuf,startOffset);
startOffset = set_cheatRate_fromBuf(sourceBuf,startOffset);
startOffset = set_chipList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_id_json(){
if(id==null){return "";}String resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public String get_name_json(){
if(name==null){return "";}String resultJson = "\"name\":";resultJson += "\"";resultJson += name.ToString();resultJson += "\"";return resultJson;
}


public String get_minLimit_json(){
if(minLimit==null){return "";}String resultJson = "\"minLimit\":";resultJson += "\"";resultJson += minLimit.ToString();resultJson += "\"";return resultJson;
}


public String get_maxLimit_json(){
if(maxLimit==null){return "";}String resultJson = "\"maxLimit\":";resultJson += "\"";resultJson += maxLimit.ToString();resultJson += "\"";return resultJson;
}


public String get_isEnable_json(){
if(isEnable==null){return "";}String resultJson = "\"isEnable\":";resultJson += "\"";resultJson += isEnable.ToString();resultJson += "\"";return resultJson;
}


public String get_cheatRate_json(){
if(cheatRate==null){return "";}String resultJson = "\"cheatRate\":";resultJson += "\"";resultJson += cheatRate.ToString();resultJson += "\"";return resultJson;
}


public String get_chipList_json(){
if(chipList==null){return "";}String resultJson = "\"chipList\":";resultJson += "[";List<Int32> listObj = (List<Int32>)chipList;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= Int32.Parse(jsonObj.ToString());
}


public void set_name_fromJson(LitJson.JsonData jsonObj){
name= jsonObj.ToString();
}


public void set_minLimit_fromJson(LitJson.JsonData jsonObj){
minLimit= Int32.Parse(jsonObj.ToString());
}


public void set_maxLimit_fromJson(LitJson.JsonData jsonObj){
maxLimit= Int32.Parse(jsonObj.ToString());
}


public void set_isEnable_fromJson(LitJson.JsonData jsonObj){
isEnable= Byte.Parse(jsonObj.ToString());
}


public void set_cheatRate_fromJson(LitJson.JsonData jsonObj){
cheatRate= Int32.Parse(jsonObj.ToString());
}


public void set_chipList_fromJson(LitJson.JsonData jsonObj){
chipList= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
chipList.Add(Int32.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(id !=  null){
resultStr += get_id_json();
}
else {}if(name !=  null){
resultStr += ",";resultStr += get_name_json();
}
else {}if(minLimit !=  null){
resultStr += ",";resultStr += get_minLimit_json();
}
else {}if(maxLimit !=  null){
resultStr += ",";resultStr += get_maxLimit_json();
}
else {}if(isEnable !=  null){
resultStr += ",";resultStr += get_isEnable_json();
}
else {}if(cheatRate !=  null){
resultStr += ",";resultStr += get_cheatRate_json();
}
else {}if(chipList !=  null){
resultStr += ",";resultStr += get_chipList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["id"] != null){
set_id_fromJson(jsonObj["id"]);
}
if(jsonObj["name"] != null){
set_name_fromJson(jsonObj["name"]);
}
if(jsonObj["minLimit"] != null){
set_minLimit_fromJson(jsonObj["minLimit"]);
}
if(jsonObj["maxLimit"] != null){
set_maxLimit_fromJson(jsonObj["maxLimit"]);
}
if(jsonObj["isEnable"] != null){
set_isEnable_fromJson(jsonObj["isEnable"]);
}
if(jsonObj["cheatRate"] != null){
set_cheatRate_fromJson(jsonObj["cheatRate"]);
}
if(jsonObj["chipList"] != null){
set_chipList_fromJson(jsonObj["chipList"]);
}
}
}
}
