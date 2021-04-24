// 此文件由协议导出插件自动生成
// ID : 00001]
//****�޸ĳ�Ա��ϢC2S_ChangeMenberScore_MsgType = 20015****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�޸ĳ�Ա��ϢC2S_ChangeMenberScore_MsgType = 20015
/// <\summary>
public class CS_ChangeMenberScore : LantisBitProtocolBase {
/// <summary>
///��Ϣ��֤
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///ȺID
/// <\summary>
public String clubId;
/// <summary>
///Ⱥ����ID
/// <\summary>
public Int32 groupMasterId;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///Ҫ�ı�ķ�ֵ
/// <\summary>
public Int32 scoreChange;
/// <summary>
///���������� 0ֱ������ 1��� 2����
/// <\summary>
public Byte controlType;
/// <summary>
///���� 0�� 1��
/// <\summary>
public Byte credit;
public CS_ChangeMenberScore(){}

public CS_ChangeMenberScore(UserValiadateInfor _UserValiadate, String _clubId, Int32 _groupMasterId, Int32 _menberId, Int32 _scoreChange, Byte _controlType, Byte _credit){
this.UserValiadate = _UserValiadate;
this.clubId = _clubId;
this.groupMasterId = _groupMasterId;
this.menberId = _menberId;
this.scoreChange = _scoreChange;
this.controlType = _controlType;
this.credit = _credit;
}
private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
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


private Byte[] get_groupMasterId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)groupMasterId);
return outBuf;
}


private Byte[] get_menberId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private Byte[] get_scoreChange_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)scoreChange);
return outBuf;
}


private Byte[] get_controlType_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)controlType;
return outBuf;
}


private Byte[] get_credit_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)credit;
return outBuf;
}

private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
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
private int set_groupMasterId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupMasterId = new Int32();
groupMasterId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_menberId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberId = new Int32();
menberId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_scoreChange_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
scoreChange = new Int32();
scoreChange = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_controlType_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
controlType = new Byte();
controlType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_credit_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
credit = new Byte();
credit = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(groupMasterId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupMasterId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(scoreChange !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_scoreChange_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(controlType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_controlType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(credit !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_credit_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_groupMasterId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_scoreChange_fromBuf(sourceBuf,startOffset);
startOffset = set_controlType_fromBuf(sourceBuf,startOffset);
startOffset = set_credit_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_groupMasterId_json(){
if(groupMasterId==null){return "";}String resultJson = "\"groupMasterId\":";resultJson += "\"";resultJson += groupMasterId.ToString();resultJson += "\"";return resultJson;
}


public String get_menberId_json(){
if(menberId==null){return "";}String resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public String get_scoreChange_json(){
if(scoreChange==null){return "";}String resultJson = "\"scoreChange\":";resultJson += "\"";resultJson += scoreChange.ToString();resultJson += "\"";return resultJson;
}


public String get_controlType_json(){
if(controlType==null){return "";}String resultJson = "\"controlType\":";resultJson += "\"";resultJson += controlType.ToString();resultJson += "\"";return resultJson;
}


public String get_credit_json(){
if(credit==null){return "";}String resultJson = "\"credit\":";resultJson += "\"";resultJson += credit.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_groupMasterId_fromJson(LitJson.JsonData jsonObj){
groupMasterId= Int32.Parse(jsonObj.ToString());
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_scoreChange_fromJson(LitJson.JsonData jsonObj){
scoreChange= Int32.Parse(jsonObj.ToString());
}


public void set_controlType_fromJson(LitJson.JsonData jsonObj){
controlType= Byte.Parse(jsonObj.ToString());
}


public void set_credit_fromJson(LitJson.JsonData jsonObj){
credit= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(groupMasterId !=  null){
resultStr += ",";resultStr += get_groupMasterId_json();
}
else {}if(menberId !=  null){
resultStr += ",";resultStr += get_menberId_json();
}
else {}if(scoreChange !=  null){
resultStr += ",";resultStr += get_scoreChange_json();
}
else {}if(controlType !=  null){
resultStr += ",";resultStr += get_controlType_json();
}
else {}if(credit !=  null){
resultStr += ",";resultStr += get_credit_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["groupMasterId"] != null){
set_groupMasterId_fromJson(jsonObj["groupMasterId"]);
}
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["scoreChange"] != null){
set_scoreChange_fromJson(jsonObj["scoreChange"]);
}
if(jsonObj["controlType"] != null){
set_controlType_fromJson(jsonObj["controlType"]);
}
if(jsonObj["credit"] != null){
set_credit_fromJson(jsonObj["credit"]);
}
}
}
}
