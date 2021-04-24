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
public class P_Menber : LantisBitProtocolBase {
/// <summary>
///���ݿ����Ƿ����������¼ 1���� 0������
/// <\summary>
public Byte insertTag;
/// <summary>
///���� 0�� 1��
/// <\summary>
public Byte credit;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///��Ա������
/// <\summary>
public String menberName;
/// <summary>
///�Ա� 1�� 0Ů
/// <\summary>
public Byte sex;
/// <summary>
///ͷ���ַ
/// <\summary>
public String headUrl;
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
public Byte black;
/// <summary>
///����ʱ��
/// <\summary>
public Int64 joinTime;
public P_Menber(){}

public P_Menber(Byte _insertTag, Byte _credit, Int32 _menberId, String _menberName, Byte _sex, String _headUrl, Int32 _Score, Int32 _UpScore, Byte _black, Int64 _joinTime){
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
private Byte[] get_insertTag_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)insertTag;
return outBuf;
}


private Byte[] get_credit_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)credit;
return outBuf;
}


private Byte[] get_menberId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private Byte[] get_menberName_encoding(){
Byte[] outBuf = null;
String str = (String)menberName;
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


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)sex;
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


private Byte[] get_Score_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Score);
return outBuf;
}


private Byte[] get_UpScore_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)UpScore);
return outBuf;
}


private Byte[] get_black_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)black;
return outBuf;
}


private Byte[] get_joinTime_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)joinTime);
return outBuf;
}

private int set_insertTag_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
insertTag = new Byte();
insertTag = sourceBuf[curIndex];
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
private int set_menberId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberId = new Int32();
menberId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_menberName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Byte();
sex = sourceBuf[curIndex];
curIndex++;
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
private int set_Score_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Score = new Int32();
Score = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_UpScore_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UpScore = new Int32();
UpScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_black_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
black = new Byte();
black = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_joinTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
joinTime = new Int64();
joinTime = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
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

public String get_insertTag_json(){
if(insertTag==null){return "";}String resultJson = "\"insertTag\":";resultJson += "\"";resultJson += insertTag.ToString();resultJson += "\"";return resultJson;
}


public String get_credit_json(){
if(credit==null){return "";}String resultJson = "\"credit\":";resultJson += "\"";resultJson += credit.ToString();resultJson += "\"";return resultJson;
}


public String get_menberId_json(){
if(menberId==null){return "";}String resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public String get_menberName_json(){
if(menberName==null){return "";}String resultJson = "\"menberName\":";resultJson += "\"";resultJson += menberName.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_Score_json(){
if(Score==null){return "";}String resultJson = "\"Score\":";resultJson += "\"";resultJson += Score.ToString();resultJson += "\"";return resultJson;
}


public String get_UpScore_json(){
if(UpScore==null){return "";}String resultJson = "\"UpScore\":";resultJson += "\"";resultJson += UpScore.ToString();resultJson += "\"";return resultJson;
}


public String get_black_json(){
if(black==null){return "";}String resultJson = "\"black\":";resultJson += "\"";resultJson += black.ToString();resultJson += "\"";return resultJson;
}


public String get_joinTime_json(){
if(joinTime==null){return "";}String resultJson = "\"joinTime\":";resultJson += "\"";resultJson += joinTime.ToString();resultJson += "\"";return resultJson;
}


public void set_insertTag_fromJson(LitJson.JsonData jsonObj){
insertTag= Byte.Parse(jsonObj.ToString());
}


public void set_credit_fromJson(LitJson.JsonData jsonObj){
credit= Byte.Parse(jsonObj.ToString());
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_menberName_fromJson(LitJson.JsonData jsonObj){
menberName= jsonObj.ToString();
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Byte.Parse(jsonObj.ToString());
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
black= Byte.Parse(jsonObj.ToString());
}


public void set_joinTime_fromJson(LitJson.JsonData jsonObj){
joinTime= Int64.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(insertTag !=  null){
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

public override void DeserializerJson(String json){
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
