// 此文件由协议导出插件自动生成
// ID : 00001]
//****�޸ĳ�Ա��Ϣ[ʹ��֪ͨ=changeMenberScore]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�޸ĳ�Ա��Ϣ[ʹ��֪ͨ=changeMenberScore]
/// <\summary>
public class SC_ChangeMenberScore : CherishBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public byte result;
/// <summary>
///ȺID
/// <\summary>
public string clubId;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///�ı��ķ�ֵ
/// <\summary>
public Int32 scoreChange;
/// <summary>
///�ı�����ֵ
/// <\summary>
public Int32 score;
/// <summary>
///���������� 0ֱ������ 1���� 2����
/// <\summary>
public byte controlType;
/// <summary>
///���� 0�� 1��
/// <\summary>
public byte credit;
public SC_ChangeMenberScore(){}

public SC_ChangeMenberScore(byte _result, string _clubId, Int32 _menberId, Int32 _scoreChange, Int32 _score, byte _controlType, byte _credit){
this.result = _result;
this.clubId = _clubId;
this.menberId = _menberId;
this.scoreChange = _scoreChange;
this.score = _score;
this.controlType = _controlType;
this.credit = _credit;
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


private byte[] get_menberId_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private byte[] get_scoreChange_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)scoreChange);
return outBuf;
}


private byte[] get_score_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)score);
return outBuf;
}


private byte[] get_controlType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)controlType;
return outBuf;
}


private byte[] get_credit_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)credit;
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
private int set_menberId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberId = new Int32();
menberId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_scoreChange_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
scoreChange = new Int32();
scoreChange = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_score_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
score = new Int32();
score = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_controlType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
controlType = new byte();
controlType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_credit_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
credit = new byte();
credit = sourceBuf[curIndex];
curIndex++;
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
}if(score !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_score_encoding();
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_scoreChange_fromBuf(sourceBuf,startOffset);
startOffset = set_score_fromBuf(sourceBuf,startOffset);
startOffset = set_controlType_fromBuf(sourceBuf,startOffset);
startOffset = set_credit_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_menberId_json(){
if(menberId==null){return "";}string resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public string get_scoreChange_json(){
if(scoreChange==null){return "";}string resultJson = "\"scoreChange\":";resultJson += "\"";resultJson += scoreChange.ToString();resultJson += "\"";return resultJson;
}


public string get_score_json(){
if(score==null){return "";}string resultJson = "\"score\":";resultJson += "\"";resultJson += score.ToString();resultJson += "\"";return resultJson;
}


public string get_controlType_json(){
if(controlType==null){return "";}string resultJson = "\"controlType\":";resultJson += "\"";resultJson += controlType.ToString();resultJson += "\"";return resultJson;
}


public string get_credit_json(){
if(credit==null){return "";}string resultJson = "\"credit\":";resultJson += "\"";resultJson += credit.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_scoreChange_fromJson(LitJson.JsonData jsonObj){
scoreChange= Int32.Parse(jsonObj.ToString());
}


public void set_score_fromJson(LitJson.JsonData jsonObj){
score= Int32.Parse(jsonObj.ToString());
}


public void set_controlType_fromJson(LitJson.JsonData jsonObj){
controlType= byte.Parse(jsonObj.ToString());
}


public void set_credit_fromJson(LitJson.JsonData jsonObj){
credit= byte.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(menberId !=  null){
resultStr += ",";resultStr += get_menberId_json();
}
else {}if(scoreChange !=  null){
resultStr += ",";resultStr += get_scoreChange_json();
}
else {}if(score !=  null){
resultStr += ",";resultStr += get_score_json();
}
else {}if(controlType !=  null){
resultStr += ",";resultStr += get_controlType_json();
}
else {}if(credit !=  null){
resultStr += ",";resultStr += get_credit_json();
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
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["scoreChange"] != null){
set_scoreChange_fromJson(jsonObj["scoreChange"]);
}
if(jsonObj["score"] != null){
set_score_fromJson(jsonObj["score"]);
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
