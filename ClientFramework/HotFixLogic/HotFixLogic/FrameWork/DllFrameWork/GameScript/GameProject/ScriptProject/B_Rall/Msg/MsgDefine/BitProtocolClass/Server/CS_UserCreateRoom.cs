// 此文件由协议导出插件自动生成
// ID : 00001]

//****创建房间类型****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///创建房间类型
/// <\summary>
public class CS_UserCreateRoom : LantisBitProtocolBase {
/// <summary>
///房间的类型
/// <\summary>
public Int32 _roomType;
/// <summary>
///一般为局数
/// <\summary>
public Int32 _roomValue;
/// <summary>
///房间参数
/// <\summary>
public List<Int32> paramarsList;
/// <summary>
///俱乐部ID
/// <\summary>
public String clubId;
/// <summary>
///用户id
/// <\summary>
public Int32 createRoleId;
/// <summary>
///房主钻石
/// <\summary>
public Int32 recharge;
public CS_UserCreateRoom(){}

public CS_UserCreateRoom(Int32 __roomType, Int32 __roomValue, List<Int32> _paramarsList, String _clubId, Int32 _createRoleId, Int32 _recharge){
this._roomType = __roomType;
this._roomValue = __roomValue;
this.paramarsList = _paramarsList;
this.clubId = _clubId;
this.createRoleId = _createRoleId;
this.recharge = _recharge;
}
private Byte[] get__roomType_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)_roomType);
return outBuf;
}


private Byte[] get__roomValue_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)_roomValue);
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


private Byte[] get_clubId_encoding(){
Byte[] outBuf = null;
String str = (String)clubId;
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


private Byte[] get_createRoleId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)createRoleId);
return outBuf;
}


private Byte[] get_recharge_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)recharge);
return outBuf;
}

private int set__roomType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_roomType = new Int32();
_roomType = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set__roomValue_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_roomValue = new Int32();
_roomValue = BitConverter.ToInt32(sourceBuf,curIndex);
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
private int set_clubId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
clubId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_createRoleId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
createRoleId = new Int32();
createRoleId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_recharge_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recharge = new Int32();
recharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(_roomType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__roomType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(_roomValue !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__roomValue_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramarsList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramarsList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(createRoleId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_createRoleId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(recharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set__roomType_fromBuf(sourceBuf,startOffset);
startOffset = set__roomValue_fromBuf(sourceBuf,startOffset);
startOffset = set_paramarsList_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_createRoleId_fromBuf(sourceBuf,startOffset);
startOffset = set_recharge_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get__roomType_json(){
if(_roomType==null){return "";}String resultJson = "\"_roomType\":";resultJson += "\"";resultJson += _roomType.ToString();resultJson += "\"";return resultJson;
}


public String get__roomValue_json(){
if(_roomValue==null){return "";}String resultJson = "\"_roomValue\":";resultJson += "\"";resultJson += _roomValue.ToString();resultJson += "\"";return resultJson;
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


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_createRoleId_json(){
if(createRoleId==null){return "";}String resultJson = "\"createRoleId\":";resultJson += "\"";resultJson += createRoleId.ToString();resultJson += "\"";return resultJson;
}


public String get_recharge_json(){
if(recharge==null){return "";}String resultJson = "\"recharge\":";resultJson += "\"";resultJson += recharge.ToString();resultJson += "\"";return resultJson;
}


public void set__roomType_fromJson(LitJson.JsonData jsonObj){
_roomType= Int32.Parse(jsonObj.ToString());
}


public void set__roomValue_fromJson(LitJson.JsonData jsonObj){
_roomValue= Int32.Parse(jsonObj.ToString());
}


public void set_paramarsList_fromJson(LitJson.JsonData jsonObj){
paramarsList= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
paramarsList.Add(Int32.Parse(jsonItem.ToString()));}

}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_createRoleId_fromJson(LitJson.JsonData jsonObj){
createRoleId= Int32.Parse(jsonObj.ToString());
}


public void set_recharge_fromJson(LitJson.JsonData jsonObj){
recharge= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(_roomType !=  null){
resultStr += get__roomType_json();
}
else {}if(_roomValue !=  null){
resultStr += ",";resultStr += get__roomValue_json();
}
else {}if(paramarsList !=  null){
resultStr += ",";resultStr += get_paramarsList_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(createRoleId !=  null){
resultStr += ",";resultStr += get_createRoleId_json();
}
else {}if(recharge !=  null){
resultStr += ",";resultStr += get_recharge_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["_roomType"] != null){
set__roomType_fromJson(jsonObj["_roomType"]);
}
if(jsonObj["_roomValue"] != null){
set__roomValue_fromJson(jsonObj["_roomValue"]);
}
if(jsonObj["paramarsList"] != null){
set_paramarsList_fromJson(jsonObj["paramarsList"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["createRoleId"] != null){
set_createRoleId_fromJson(jsonObj["createRoleId"]);
}
if(jsonObj["recharge"] != null){
set_recharge_fromJson(jsonObj["recharge"]);
}
}
}
}
