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
public class SC_ChangeRechargeToClub : CherishBitProtocolBase {
/// <summary>
///��Ϣ��֤
/// <\summary>
public byte result;
/// <summary>
///���ֲ�ID
/// <\summary>
public string clubId;
/// <summary>
///Ⱥ��ID
/// <\summary>
public string masterId;
/// <summary>
///�ı�������
/// <\summary>
public Int32 changeRecharge;
/// <summary>
///���ֲ��ſ�����
/// <\summary>
public Int32 clubRecharge;
public SC_ChangeRechargeToClub(){}

public SC_ChangeRechargeToClub(byte _result, string _clubId, string _masterId, Int32 _changeRecharge, Int32 _clubRecharge){
this.result = _result;
this.clubId = _clubId;
this.masterId = _masterId;
this.changeRecharge = _changeRecharge;
this.clubRecharge = _clubRecharge;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_clubId_encoding(){
byte[] outBuf = null;
string str = (string)clubId;
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


private byte[] get_masterId_encoding(){
byte[] outBuf = null;
string str = (string)masterId;
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


private byte[] get_changeRecharge_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)changeRecharge);
return outBuf;
}


private byte[] get_clubRecharge_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)clubRecharge);
return outBuf;
}

private int set_result_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_clubId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_masterId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_changeRecharge_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
changeRecharge = new Int32();
changeRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_clubRecharge_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubRecharge = new Int32();
clubRecharge = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_masterId_fromBuf(sourceBuf,startOffset);
startOffset = set_changeRecharge_fromBuf(sourceBuf,startOffset);
startOffset = set_clubRecharge_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_masterId_json(){
if(masterId==null){return "";}string resultJson = "\"masterId\":";resultJson += "\"";resultJson += masterId.ToString();resultJson += "\"";return resultJson;
}


public string get_changeRecharge_json(){
if(changeRecharge==null){return "";}string resultJson = "\"changeRecharge\":";resultJson += "\"";resultJson += changeRecharge.ToString();resultJson += "\"";return resultJson;
}


public string get_clubRecharge_json(){
if(clubRecharge==null){return "";}string resultJson = "\"clubRecharge\":";resultJson += "\"";resultJson += clubRecharge.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
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

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
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

public override void DeserializerJson(string json){
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
