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
public class SC_ChangeMenberScore : LantisBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public Byte result;
/// <summary>
///ȺID
/// <\summary>
public String clubId;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///�ı�ķ�ֵ
/// <\summary>
public Int32 scoreChange;
/// <summary>
///�ı���ֵ
/// <\summary>
public Int32 score;
/// <summary>
///���������� 0ֱ������ 1��� 2����
/// <\summary>
public Byte controlType;
/// <summary>
///���� 0�� 1��
/// <\summary>
public Byte credit;
public SC_ChangeMenberScore(){}

public SC_ChangeMenberScore(Byte _result, String _clubId, Int32 _menberId, Int32 _scoreChange, Int32 _score, Byte _controlType, Byte _credit){
this.result = _result;
this.clubId = _clubId;
this.menberId = _menberId;
this.scoreChange = _scoreChange;
this.score = _score;
this.controlType = _controlType;
this.credit = _credit;
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


private Byte[] get_score_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)score);
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
private int set_score_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
score = new Int32();
score = BitConverter.ToInt32(sourceBuf,curIndex);
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_scoreChange_fromBuf(sourceBuf,startOffset);
startOffset = set_score_fromBuf(sourceBuf,startOffset);
startOffset = set_controlType_fromBuf(sourceBuf,startOffset);
startOffset = set_credit_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_menberId_json(){
if(menberId==null){return "";}String resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public String get_scoreChange_json(){
if(scoreChange==null){return "";}String resultJson = "\"scoreChange\":";resultJson += "\"";resultJson += scoreChange.ToString();resultJson += "\"";return resultJson;
}


public String get_score_json(){
if(score==null){return "";}String resultJson = "\"score\":";resultJson += "\"";resultJson += score.ToString();resultJson += "\"";return resultJson;
}


public String get_controlType_json(){
if(controlType==null){return "";}String resultJson = "\"controlType\":";resultJson += "\"";resultJson += controlType.ToString();resultJson += "\"";return resultJson;
}


public String get_credit_json(){
if(credit==null){return "";}String resultJson = "\"credit\":";resultJson += "\"";resultJson += credit.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
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
controlType= Byte.Parse(jsonObj.ToString());
}


public void set_credit_fromJson(LitJson.JsonData jsonObj){
credit= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
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

public override void DeserializerJson(String json){
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
