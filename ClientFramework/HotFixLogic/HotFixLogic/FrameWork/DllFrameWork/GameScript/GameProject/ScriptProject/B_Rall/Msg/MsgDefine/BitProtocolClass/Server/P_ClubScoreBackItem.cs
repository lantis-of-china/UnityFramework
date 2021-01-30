// 此文件由协议导出插件自动生成
// ID : 00001]

//****俱乐部分数变化****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///俱乐部分数变化
/// <\summary>
public class P_ClubScoreBackItem : CherishBitProtocolBase {
/// <summary>
///成员ID
/// <\summary>
public Int32 menberId;
/// <summary>
///成员名
/// <\summary>
public string menberName;
/// <summary>
///返回的输赢的值
/// <\summary>
public Int32 backChangeScore;
/// <summary>
///补分supplement
/// <\summary>
public Int32 supplementScore;
/// <summary>
///扣除
/// <\summary>
public Int32 deductionScore;
public P_ClubScoreBackItem(){}

public P_ClubScoreBackItem(Int32 _menberId, string _menberName, Int32 _backChangeScore, Int32 _supplementScore, Int32 _deductionScore){
this.menberId = _menberId;
this.menberName = _menberName;
this.backChangeScore = _backChangeScore;
this.supplementScore = _supplementScore;
this.deductionScore = _deductionScore;
}
private byte[] get_menberId_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private byte[] get_menberName_encoding(){
byte[] outBuf = null;
string str = (string)menberName;
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


private byte[] get_backChangeScore_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)backChangeScore);
return outBuf;
}


private byte[] get_supplementScore_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)supplementScore);
return outBuf;
}


private byte[] get_deductionScore_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)deductionScore);
return outBuf;
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
private int set_menberName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
menberName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_backChangeScore_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
backChangeScore = new Int32();
backChangeScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_supplementScore_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
supplementScore = new Int32();
supplementScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_deductionScore_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
deductionScore = new Int32();
deductionScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(menberId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(backChangeScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_backChangeScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(supplementScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_supplementScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(deductionScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_deductionScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberName_fromBuf(sourceBuf,startOffset);
startOffset = set_backChangeScore_fromBuf(sourceBuf,startOffset);
startOffset = set_supplementScore_fromBuf(sourceBuf,startOffset);
startOffset = set_deductionScore_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_menberId_json(){
if(menberId==null){return "";}string resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public string get_menberName_json(){
if(menberName==null){return "";}string resultJson = "\"menberName\":";resultJson += "\"";resultJson += menberName.ToString();resultJson += "\"";return resultJson;
}


public string get_backChangeScore_json(){
if(backChangeScore==null){return "";}string resultJson = "\"backChangeScore\":";resultJson += "\"";resultJson += backChangeScore.ToString();resultJson += "\"";return resultJson;
}


public string get_supplementScore_json(){
if(supplementScore==null){return "";}string resultJson = "\"supplementScore\":";resultJson += "\"";resultJson += supplementScore.ToString();resultJson += "\"";return resultJson;
}


public string get_deductionScore_json(){
if(deductionScore==null){return "";}string resultJson = "\"deductionScore\":";resultJson += "\"";resultJson += deductionScore.ToString();resultJson += "\"";return resultJson;
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_menberName_fromJson(LitJson.JsonData jsonObj){
menberName= jsonObj.ToString();
}


public void set_backChangeScore_fromJson(LitJson.JsonData jsonObj){
backChangeScore= Int32.Parse(jsonObj.ToString());
}


public void set_supplementScore_fromJson(LitJson.JsonData jsonObj){
supplementScore= Int32.Parse(jsonObj.ToString());
}


public void set_deductionScore_fromJson(LitJson.JsonData jsonObj){
deductionScore= Int32.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(menberId !=  null){
resultStr += get_menberId_json();
}
else {}if(menberName !=  null){
resultStr += ",";resultStr += get_menberName_json();
}
else {}if(backChangeScore !=  null){
resultStr += ",";resultStr += get_backChangeScore_json();
}
else {}if(supplementScore !=  null){
resultStr += ",";resultStr += get_supplementScore_json();
}
else {}if(deductionScore !=  null){
resultStr += ",";resultStr += get_deductionScore_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["menberName"] != null){
set_menberName_fromJson(jsonObj["menberName"]);
}
if(jsonObj["backChangeScore"] != null){
set_backChangeScore_fromJson(jsonObj["backChangeScore"]);
}
if(jsonObj["supplementScore"] != null){
set_supplementScore_fromJson(jsonObj["supplementScore"]);
}
if(jsonObj["deductionScore"] != null){
set_deductionScore_fromJson(jsonObj["deductionScore"]);
}
}
}
}
