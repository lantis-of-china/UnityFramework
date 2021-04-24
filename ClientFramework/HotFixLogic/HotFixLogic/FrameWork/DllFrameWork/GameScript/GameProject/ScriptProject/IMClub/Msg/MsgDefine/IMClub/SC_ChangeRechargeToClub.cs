// 此文件由协议导出插件自动生成
// ID : 00001]
//****���÷�������[changeClubRecharge]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///���÷�������[changeClubRecharge]
/// <\summary>
public class SC_ChangeRechargeToClub : LantisBitProtocolBase {
/// <summary>
///��Ϣ��֤
/// <\summary>
public Byte result;
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
/// <summary>
///Ⱥ��ID
/// <\summary>
public String masterId;
/// <summary>
///�ı������
/// <\summary>
public Int32 changeRecharge;
/// <summary>
///���ֲ��ſ�����
/// <\summary>
public Int32 clubRecharge;
public SC_ChangeRechargeToClub(){}

public SC_ChangeRechargeToClub(Byte _result, String _clubId, String _masterId, Int32 _changeRecharge, Int32 _clubRecharge){
this.result = _result;
this.clubId = _clubId;
this.masterId = _masterId;
this.changeRecharge = _changeRecharge;
this.clubRecharge = _clubRecharge;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
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


private Byte[] get_masterId_encoding(){
Byte[] outBuf = null;
String str = (String)masterId;
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


private Byte[] get_changeRecharge_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)changeRecharge);
return outBuf;
}


private Byte[] get_clubRecharge_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)clubRecharge);
return outBuf;
}

private int set_result_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new Byte();
result = sourceBuf[curIndex];
curIndex++;
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
private int set_masterId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
masterId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
masterId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_changeRecharge_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
changeRecharge = new Int32();
changeRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_clubRecharge_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubRecharge = new Int32();
clubRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(result !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_result_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(masterId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_masterId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(changeRecharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_changeRecharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubRecharge !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubRecharge_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_masterId_fromBuf(sourceBuf,startOffset);
startOffset = set_changeRecharge_fromBuf(sourceBuf,startOffset);
startOffset = set_clubRecharge_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_masterId_json(){
if(masterId==null){return "";}String resultJson = "\"masterId\":";resultJson += "\"";resultJson += masterId.ToString();resultJson += "\"";return resultJson;
}


public String get_changeRecharge_json(){
if(changeRecharge==null){return "";}String resultJson = "\"changeRecharge\":";resultJson += "\"";resultJson += changeRecharge.ToString();resultJson += "\"";return resultJson;
}


public String get_clubRecharge_json(){
if(clubRecharge==null){return "";}String resultJson = "\"clubRecharge\":";resultJson += "\"";resultJson += clubRecharge.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_masterId_fromJson(LitJson.JsonData jsonObj){
masterId= jsonObj.ToString();
}


public void set_changeRecharge_fromJson(LitJson.JsonData jsonObj){
changeRecharge= Int32.Parse(jsonObj.ToString());
}


public void set_clubRecharge_fromJson(LitJson.JsonData jsonObj){
clubRecharge= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(masterId !=  null){
resultStr += ",";resultStr += get_masterId_json();
}
else {}if(changeRecharge !=  null){
resultStr += ",";resultStr += get_changeRecharge_json();
}
else {}if(clubRecharge !=  null){
resultStr += ",";resultStr += get_clubRecharge_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["masterId"] != null){
set_masterId_fromJson(jsonObj["masterId"]);
}
if(jsonObj["changeRecharge"] != null){
set_changeRecharge_fromJson(jsonObj["changeRecharge"]);
}
if(jsonObj["clubRecharge"] != null){
set_clubRecharge_fromJson(jsonObj["clubRecharge"]);
}
}
}
}
