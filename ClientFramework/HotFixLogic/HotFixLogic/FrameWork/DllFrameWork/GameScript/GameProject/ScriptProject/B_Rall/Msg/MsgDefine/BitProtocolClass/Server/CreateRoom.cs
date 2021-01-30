// 此文件由协议导出插件自动生成
// ID : 00011]
   
//****创建房间****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///创建房间
/// <\summary>
public class CreateRoom : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
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
///绑定的俱乐部ID
/// <\summary>
public string bindClubId;
public CreateRoom(){}

public CreateRoom(UserValiadateInfor _UserValiadate, Int32 __roomType, Int32 __roomValue, List<Int32> _paramarsList, string _bindClubId){
this.UserValiadate = _UserValiadate;
this._roomType = __roomType;
this._roomValue = __roomValue;
this.paramarsList = _paramarsList;
this.bindClubId = _bindClubId;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get__roomType_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)_roomType);
return outBuf;
}


private byte[] get__roomValue_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)_roomValue);
return outBuf;
}


private byte[] get_paramarsList_encoding(){
byte[] outBuf = null;
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


private byte[] get_bindClubId_encoding(){
byte[] outBuf = null;
string str = (string)bindClubId;
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

private int set_UserValiadate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set__roomType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_roomType = new Int32();
_roomType = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set__roomValue_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_roomValue = new Int32();
_roomValue = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_paramarsList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_bindClubId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
bindClubId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
bindClubId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(_roomType !=  null){
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
}if(bindClubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_bindClubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set__roomType_fromBuf(sourceBuf,startOffset);
startOffset = set__roomValue_fromBuf(sourceBuf,startOffset);
startOffset = set_paramarsList_fromBuf(sourceBuf,startOffset);
startOffset = set_bindClubId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get__roomType_json(){
if(_roomType==null){return "";}string resultJson = "\"_roomType\":";resultJson += "\"";resultJson += _roomType.ToString();resultJson += "\"";return resultJson;
}


public string get__roomValue_json(){
if(_roomValue==null){return "";}string resultJson = "\"_roomValue\":";resultJson += "\"";resultJson += _roomValue.ToString();resultJson += "\"";return resultJson;
}


public string get_paramarsList_json(){
if(paramarsList==null){return "";}string resultJson = "\"paramarsList\":";resultJson += "[";List<Int32> listObj = (List<Int32>)paramarsList;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public string get_bindClubId_json(){
if(bindClubId==null){return "";}string resultJson = "\"bindClubId\":";resultJson += "\"";resultJson += bindClubId.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


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


public void set_bindClubId_fromJson(LitJson.JsonData jsonObj){
bindClubId= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(_roomType !=  null){
resultStr += ",";resultStr += get__roomType_json();
}
else {}if(_roomValue !=  null){
resultStr += ",";resultStr += get__roomValue_json();
}
else {}if(paramarsList !=  null){
resultStr += ",";resultStr += get_paramarsList_json();
}
else {}if(bindClubId !=  null){
resultStr += ",";resultStr += get_bindClubId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["_roomType"] != null){
set__roomType_fromJson(jsonObj["_roomType"]);
}
if(jsonObj["_roomValue"] != null){
set__roomValue_fromJson(jsonObj["_roomValue"]);
}
if(jsonObj["paramarsList"] != null){
set_paramarsList_fromJson(jsonObj["paramarsList"]);
}
if(jsonObj["bindClubId"] != null){
set_bindClubId_fromJson(jsonObj["bindClubId"]);
}
}
}
}
