// 此文件由协议导出插件自动生成
// ID : 00001]
//****��Ա�򵥽ṹ****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��Ա�򵥽ṹ
/// <\summary>
public class P_Menber : CherishBitProtocolBase {
/// <summary>
///���ݿ����Ƿ�����������¼ 1���� 0������
/// <\summary>
public byte insertTag;
/// <summary>
///���� 0�� 1��
/// <\summary>
public byte credit;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///��Ա������
/// <\summary>
public string menberName;
/// <summary>
///�Ա� 1�� 0Ů
/// <\summary>
public byte sex;
/// <summary>
///ͷ����ַ
/// <\summary>
public string headUrl;
/// <summary>
///����
/// <\summary>
public Int32 Score;
/// <summary>
///����
/// <\summary>
public Int32 UpScore;
/// <summary>
///������
/// <\summary>
public byte black;
/// <summary>
///����ʱ��
/// <\summary>
public Int64 joinTime;
public P_Menber(){}

public P_Menber(byte _insertTag, byte _credit, Int32 _menberId, string _menberName, byte _sex, string _headUrl, Int32 _Score, Int32 _UpScore, byte _black, Int64 _joinTime){
this.insertTag = _insertTag;
this.credit = _credit;
this.menberId = _menberId;
this.menberName = _menberName;
this.sex = _sex;
this.headUrl = _headUrl;
this.Score = _Score;
this.UpScore = _UpScore;
this.black = _black;
this.joinTime = _joinTime;
}
private byte[] get_insertTag_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)insertTag;
return outBuf;
}


private byte[] get_credit_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)credit;
return outBuf;
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


private byte[] get_sex_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)sex;
return outBuf;
}


private byte[] get_headUrl_encoding(){
byte[] outBuf = null;
string str = (string)headUrl;
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


private byte[] get_Score_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Score);
return outBuf;
}


private byte[] get_UpScore_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)UpScore);
return outBuf;
}


private byte[] get_black_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)black;
return outBuf;
}


private byte[] get_joinTime_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)joinTime);
return outBuf;
}

private int set_insertTag_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
insertTag = new byte();
insertTag = sourceBuf[curIndex];
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
private int set_sex_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new byte();
sex = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_headUrl_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_Score_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Score = new Int32();
Score = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_UpScore_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UpScore = new Int32();
UpScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_black_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
black = new byte();
black = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_joinTime_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
joinTime = new Int64();
joinTime = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(insertTag !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_insertTag_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(credit !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_credit_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberId !=  null){
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
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Score !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Score_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(UpScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UpScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(black !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_black_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(joinTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_joinTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_insertTag_fromBuf(sourceBuf,startOffset);
startOffset = set_credit_fromBuf(sourceBuf,startOffset);
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberName_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_Score_fromBuf(sourceBuf,startOffset);
startOffset = set_UpScore_fromBuf(sourceBuf,startOffset);
startOffset = set_black_fromBuf(sourceBuf,startOffset);
startOffset = set_joinTime_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_insertTag_json(){
if(insertTag==null){return "";}string resultJson = "\"insertTag\":";resultJson += "\"";resultJson += insertTag.ToString();resultJson += "\"";return resultJson;
}


public string get_credit_json(){
if(credit==null){return "";}string resultJson = "\"credit\":";resultJson += "\"";resultJson += credit.ToString();resultJson += "\"";return resultJson;
}


public string get_menberId_json(){
if(menberId==null){return "";}string resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public string get_menberName_json(){
if(menberName==null){return "";}string resultJson = "\"menberName\":";resultJson += "\"";resultJson += menberName.ToString();resultJson += "\"";return resultJson;
}


public string get_sex_json(){
if(sex==null){return "";}string resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public string get_headUrl_json(){
if(headUrl==null){return "";}string resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public string get_Score_json(){
if(Score==null){return "";}string resultJson = "\"Score\":";resultJson += "\"";resultJson += Score.ToString();resultJson += "\"";return resultJson;
}


public string get_UpScore_json(){
if(UpScore==null){return "";}string resultJson = "\"UpScore\":";resultJson += "\"";resultJson += UpScore.ToString();resultJson += "\"";return resultJson;
}


public string get_black_json(){
if(black==null){return "";}string resultJson = "\"black\":";resultJson += "\"";resultJson += black.ToString();resultJson += "\"";return resultJson;
}


public string get_joinTime_json(){
if(joinTime==null){return "";}string resultJson = "\"joinTime\":";resultJson += "\"";resultJson += joinTime.ToString();resultJson += "\"";return resultJson;
}


public void set_insertTag_fromJson(LitJson.JsonData jsonObj){
insertTag= byte.Parse(jsonObj.ToString());
}


public void set_credit_fromJson(LitJson.JsonData jsonObj){
credit= byte.Parse(jsonObj.ToString());
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_menberName_fromJson(LitJson.JsonData jsonObj){
menberName= jsonObj.ToString();
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= byte.Parse(jsonObj.ToString());
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_Score_fromJson(LitJson.JsonData jsonObj){
Score= Int32.Parse(jsonObj.ToString());
}


public void set_UpScore_fromJson(LitJson.JsonData jsonObj){
UpScore= Int32.Parse(jsonObj.ToString());
}


public void set_black_fromJson(LitJson.JsonData jsonObj){
black= byte.Parse(jsonObj.ToString());
}


public void set_joinTime_fromJson(LitJson.JsonData jsonObj){
joinTime= Int64.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(insertTag !=  null){
resultStr += get_insertTag_json();
}
else {}if(credit !=  null){
resultStr += ",";resultStr += get_credit_json();
}
else {}if(menberId !=  null){
resultStr += ",";resultStr += get_menberId_json();
}
else {}if(menberName !=  null){
resultStr += ",";resultStr += get_menberName_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(Score !=  null){
resultStr += ",";resultStr += get_Score_json();
}
else {}if(UpScore !=  null){
resultStr += ",";resultStr += get_UpScore_json();
}
else {}if(black !=  null){
resultStr += ",";resultStr += get_black_json();
}
else {}if(joinTime !=  null){
resultStr += ",";resultStr += get_joinTime_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["insertTag"] != null){
set_insertTag_fromJson(jsonObj["insertTag"]);
}
if(jsonObj["credit"] != null){
set_credit_fromJson(jsonObj["credit"]);
}
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["menberName"] != null){
set_menberName_fromJson(jsonObj["menberName"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["Score"] != null){
set_Score_fromJson(jsonObj["Score"]);
}
if(jsonObj["UpScore"] != null){
set_UpScore_fromJson(jsonObj["UpScore"]);
}
if(jsonObj["black"] != null){
set_black_fromJson(jsonObj["black"]);
}
if(jsonObj["joinTime"] != null){
set_joinTime_fromJson(jsonObj["joinTime"]);
}
}
}
}
