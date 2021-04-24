// 此文件由协议导出插件自动生成
// ID : 10037]
   
//****角色属性****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace BaseDataAttribute{
/// <summary>
///角色属性
/// <\summary>
public class RoleInfor : LantisBitProtocolBase {
/// <summary>
///角色Id
/// <\summary>
public Int32 roleId;
/// <summary>
///角色性别
/// <\summary>
public Int16 sex;
/// <summary>
///角色名字
/// <\summary>
public String name;
/// <summary>
///房卡数量
/// <\summary>
public Int32 rechargeCount;
/// <summary>
///金币数量
/// <\summary>
public Int32 goldCount;
/// <summary>
///创建日期
/// <\summary>
public String createTime;
/// <summary>
///头像连接
/// <\summary>
public String headUrl;
/// <summary>
///是微信登陆
/// <\summary>
public Byte isWeiChat;
/// <summary>
///1开启隐藏功能 0不开启隐藏功能
/// <\summary>
public Byte openHiddent;
public RoleInfor(){}

public RoleInfor(Int32 _roleId, Int16 _sex, String _name, Int32 _rechargeCount, Int32 _goldCount, String _createTime, String _headUrl, Byte _isWeiChat, Byte _openHiddent){
this.roleId = _roleId;
this.sex = _sex;
this.name = _name;
this.rechargeCount = _rechargeCount;
this.goldCount = _goldCount;
this.createTime = _createTime;
this.headUrl = _headUrl;
this.isWeiChat = _isWeiChat;
this.openHiddent = _openHiddent;
}
private Byte[] get_roleId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roleId);
return outBuf;
}


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((short)sex);
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


private Byte[] get_rechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeCount);
return outBuf;
}


private Byte[] get_goldCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)goldCount);
return outBuf;
}


private Byte[] get_createTime_encoding(){
Byte[] outBuf = null;
String str = (String)createTime;
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


private Byte[] get_headUrl_encoding(){
Byte[] outBuf = null;
String str = (String)headUrl;
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


private Byte[] get_isWeiChat_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)isWeiChat;
return outBuf;
}


private Byte[] get_openHiddent_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)openHiddent;
return outBuf;
}

private int set_roleId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roleId = new Int32();
roleId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Int16();
sex = BitConverter.ToInt16(sourceBuf,curIndex);
curIndex += 2;
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
private int set_rechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
rechargeCount = new Int32();
rechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_goldCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
goldCount = new Int32();
goldCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_createTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
createTime = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
createTime = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_headUrl_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
headUrl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
headUrl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_isWeiChat_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isWeiChat = new Byte();
isWeiChat = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_openHiddent_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
openHiddent = new Byte();
openHiddent = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(roleId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roleId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(name !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_name_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(rechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_rechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(goldCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_goldCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(createTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_createTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(isWeiChat !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_isWeiChat_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(openHiddent !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_openHiddent_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_roleId_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
startOffset = set_name_fromBuf(sourceBuf,startOffset);
startOffset = set_rechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_goldCount_fromBuf(sourceBuf,startOffset);
startOffset = set_createTime_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_isWeiChat_fromBuf(sourceBuf,startOffset);
startOffset = set_openHiddent_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_roleId_json(){
if(roleId==null){return "";}String resultJson = "\"roleId\":";resultJson += "\"";resultJson += roleId.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public String get_name_json(){
if(name==null){return "";}String resultJson = "\"name\":";resultJson += "\"";resultJson += name.ToString();resultJson += "\"";return resultJson;
}


public String get_rechargeCount_json(){
if(rechargeCount==null){return "";}String resultJson = "\"rechargeCount\":";resultJson += "\"";resultJson += rechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_goldCount_json(){
if(goldCount==null){return "";}String resultJson = "\"goldCount\":";resultJson += "\"";resultJson += goldCount.ToString();resultJson += "\"";return resultJson;
}


public String get_createTime_json(){
if(createTime==null){return "";}String resultJson = "\"createTime\":";resultJson += "\"";resultJson += createTime.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_isWeiChat_json(){
if(isWeiChat==null){return "";}String resultJson = "\"isWeiChat\":";resultJson += "\"";resultJson += isWeiChat.ToString();resultJson += "\"";return resultJson;
}


public String get_openHiddent_json(){
if(openHiddent==null){return "";}String resultJson = "\"openHiddent\":";resultJson += "\"";resultJson += openHiddent.ToString();resultJson += "\"";return resultJson;
}


public void set_roleId_fromJson(LitJson.JsonData jsonObj){
roleId= Int32.Parse(jsonObj.ToString());
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Int16.Parse(jsonObj.ToString());
}


public void set_name_fromJson(LitJson.JsonData jsonObj){
name= jsonObj.ToString();
}


public void set_rechargeCount_fromJson(LitJson.JsonData jsonObj){
rechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_goldCount_fromJson(LitJson.JsonData jsonObj){
goldCount= Int32.Parse(jsonObj.ToString());
}


public void set_createTime_fromJson(LitJson.JsonData jsonObj){
createTime= jsonObj.ToString();
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_isWeiChat_fromJson(LitJson.JsonData jsonObj){
isWeiChat= Byte.Parse(jsonObj.ToString());
}


public void set_openHiddent_fromJson(LitJson.JsonData jsonObj){
openHiddent= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(roleId !=  null){
resultStr += get_roleId_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}if(name !=  null){
resultStr += ",";resultStr += get_name_json();
}
else {}if(rechargeCount !=  null){
resultStr += ",";resultStr += get_rechargeCount_json();
}
else {}if(goldCount !=  null){
resultStr += ",";resultStr += get_goldCount_json();
}
else {}if(createTime !=  null){
resultStr += ",";resultStr += get_createTime_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(isWeiChat !=  null){
resultStr += ",";resultStr += get_isWeiChat_json();
}
else {}if(openHiddent !=  null){
resultStr += ",";resultStr += get_openHiddent_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roleId"] != null){
set_roleId_fromJson(jsonObj["roleId"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
if(jsonObj["name"] != null){
set_name_fromJson(jsonObj["name"]);
}
if(jsonObj["rechargeCount"] != null){
set_rechargeCount_fromJson(jsonObj["rechargeCount"]);
}
if(jsonObj["goldCount"] != null){
set_goldCount_fromJson(jsonObj["goldCount"]);
}
if(jsonObj["createTime"] != null){
set_createTime_fromJson(jsonObj["createTime"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["isWeiChat"] != null){
set_isWeiChat_fromJson(jsonObj["isWeiChat"]);
}
if(jsonObj["openHiddent"] != null){
set_openHiddent_fromJson(jsonObj["openHiddent"]);
}
}
}
}
