// 此文件由协议导出插件自动生成
// ID : 00001]
//****Ⱥ����Ϣ****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///Ⱥ����Ϣ
/// <\summary>
public class P_GroupInfo : LantisBitProtocolBase {
/// <summary>
///���ݿ����Ƿ����������¼ 1���� 0������
/// <\summary>
public Byte insertTag;
/// <summary>
///�ڲ���ȺID
/// <\summary>
public String clubId;
/// <summary>
///ȺID ��Ӧ������ID
/// <\summary>
public String groupId;
/// <summary>
///ͼ���ַ
/// <\summary>
public String iconUrl;
/// <summary>
///Ⱥ�������
/// <\summary>
public String groupName;
/// <summary>
///ǩ��
/// <\summary>
public String sign;
/// <summary>
///Ⱥ��ID
/// <\summary>
public Int32 groupMasterId;
/// <summary>
///��Ա����
/// <\summary>
public Int32 groupMenberCount;
/// <summary>
///��������
/// <\summary>
public Int32 rechargeCount;
/// <summary>
///�������ķ�������
/// <\summary>
public Int32 toDayUseRechargeCount;
/// <summary>
///�ܹ����ķ�������
/// <\summary>
public Int32 toldUseRechargeCount;
/// <summary>
///���ջ�������
/// <\summary>
public Int32 toDayCollectScoreCount;
/// <summary>
///�ܹ���������
/// <\summary>
public Int32 toldCollectScoreCount;
/// <summary>
///����ʱ��
/// <\summary>
public Int64 createTime;
/// <summary>
///���ֲ�����
/// <\summary>
public P_ClubSetting clubSetting;
public P_GroupInfo(){}

public P_GroupInfo(Byte _insertTag, String _clubId, String _groupId, String _iconUrl, String _groupName, String _sign, Int32 _groupMasterId, Int32 _groupMenberCount, Int32 _rechargeCount, Int32 _toDayUseRechargeCount, Int32 _toldUseRechargeCount, Int32 _toDayCollectScoreCount, Int32 _toldCollectScoreCount, Int64 _createTime, P_ClubSetting _clubSetting){
this.insertTag = _insertTag;
this.clubId = _clubId;
this.groupId = _groupId;
this.iconUrl = _iconUrl;
this.groupName = _groupName;
this.sign = _sign;
this.groupMasterId = _groupMasterId;
this.groupMenberCount = _groupMenberCount;
this.rechargeCount = _rechargeCount;
this.toDayUseRechargeCount = _toDayUseRechargeCount;
this.toldUseRechargeCount = _toldUseRechargeCount;
this.toDayCollectScoreCount = _toDayCollectScoreCount;
this.toldCollectScoreCount = _toldCollectScoreCount;
this.createTime = _createTime;
this.clubSetting = _clubSetting;
}
private Byte[] get_insertTag_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)insertTag;
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


private Byte[] get_groupId_encoding(){
Byte[] outBuf = null;
String str = (String)groupId;
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


private Byte[] get_iconUrl_encoding(){
Byte[] outBuf = null;
String str = (String)iconUrl;
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


private Byte[] get_groupName_encoding(){
Byte[] outBuf = null;
String str = (String)groupName;
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


private Byte[] get_sign_encoding(){
Byte[] outBuf = null;
String str = (String)sign;
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


private Byte[] get_groupMenberCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)groupMenberCount);
return outBuf;
}


private Byte[] get_rechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeCount);
return outBuf;
}


private Byte[] get_toDayUseRechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)toDayUseRechargeCount);
return outBuf;
}


private Byte[] get_toldUseRechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)toldUseRechargeCount);
return outBuf;
}


private Byte[] get_toDayCollectScoreCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)toDayCollectScoreCount);
return outBuf;
}


private Byte[] get_toldCollectScoreCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)toldCollectScoreCount);
return outBuf;
}


private Byte[] get_createTime_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)createTime);
return outBuf;
}


private Byte[] get_clubSetting_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)clubSetting).Serializer();
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
private int set_groupId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
groupId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_iconUrl_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
iconUrl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
iconUrl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_groupName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
groupName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_sign_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sign = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
sign = System.Text.Encoding.UTF8.GetString(byteArray);
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
private int set_groupMenberCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupMenberCount = new Int32();
groupMenberCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
private int set_toDayUseRechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toDayUseRechargeCount = new Int32();
toDayUseRechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_toldUseRechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldUseRechargeCount = new Int32();
toldUseRechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_toDayCollectScoreCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toDayCollectScoreCount = new Int32();
toDayCollectScoreCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_toldCollectScoreCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
toldCollectScoreCount = new Int32();
toldCollectScoreCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_createTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
createTime = new Int64();
createTime = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_clubSetting_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubSetting = new P_ClubSetting();
curIndex = clubSetting.Deserializer(sourceBuf,curIndex);
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
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(groupId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(iconUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_iconUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(groupName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sign !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sign_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(groupMasterId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupMasterId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(groupMenberCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupMenberCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(rechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_rechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toDayUseRechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toDayUseRechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toldUseRechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toldUseRechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toDayCollectScoreCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toDayCollectScoreCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(toldCollectScoreCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_toldCollectScoreCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(createTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_createTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubSetting !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubSetting_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_insertTag_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_groupId_fromBuf(sourceBuf,startOffset);
startOffset = set_iconUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_groupName_fromBuf(sourceBuf,startOffset);
startOffset = set_sign_fromBuf(sourceBuf,startOffset);
startOffset = set_groupMasterId_fromBuf(sourceBuf,startOffset);
startOffset = set_groupMenberCount_fromBuf(sourceBuf,startOffset);
startOffset = set_rechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_toDayUseRechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_toldUseRechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_toDayCollectScoreCount_fromBuf(sourceBuf,startOffset);
startOffset = set_toldCollectScoreCount_fromBuf(sourceBuf,startOffset);
startOffset = set_createTime_fromBuf(sourceBuf,startOffset);
startOffset = set_clubSetting_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_insertTag_json(){
if(insertTag==null){return "";}String resultJson = "\"insertTag\":";resultJson += "\"";resultJson += insertTag.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_groupId_json(){
if(groupId==null){return "";}String resultJson = "\"groupId\":";resultJson += "\"";resultJson += groupId.ToString();resultJson += "\"";return resultJson;
}


public String get_iconUrl_json(){
if(iconUrl==null){return "";}String resultJson = "\"iconUrl\":";resultJson += "\"";resultJson += iconUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_groupName_json(){
if(groupName==null){return "";}String resultJson = "\"groupName\":";resultJson += "\"";resultJson += groupName.ToString();resultJson += "\"";return resultJson;
}


public String get_sign_json(){
if(sign==null){return "";}String resultJson = "\"sign\":";resultJson += "\"";resultJson += sign.ToString();resultJson += "\"";return resultJson;
}


public String get_groupMasterId_json(){
if(groupMasterId==null){return "";}String resultJson = "\"groupMasterId\":";resultJson += "\"";resultJson += groupMasterId.ToString();resultJson += "\"";return resultJson;
}


public String get_groupMenberCount_json(){
if(groupMenberCount==null){return "";}String resultJson = "\"groupMenberCount\":";resultJson += "\"";resultJson += groupMenberCount.ToString();resultJson += "\"";return resultJson;
}


public String get_rechargeCount_json(){
if(rechargeCount==null){return "";}String resultJson = "\"rechargeCount\":";resultJson += "\"";resultJson += rechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_toDayUseRechargeCount_json(){
if(toDayUseRechargeCount==null){return "";}String resultJson = "\"toDayUseRechargeCount\":";resultJson += "\"";resultJson += toDayUseRechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_toldUseRechargeCount_json(){
if(toldUseRechargeCount==null){return "";}String resultJson = "\"toldUseRechargeCount\":";resultJson += "\"";resultJson += toldUseRechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_toDayCollectScoreCount_json(){
if(toDayCollectScoreCount==null){return "";}String resultJson = "\"toDayCollectScoreCount\":";resultJson += "\"";resultJson += toDayCollectScoreCount.ToString();resultJson += "\"";return resultJson;
}


public String get_toldCollectScoreCount_json(){
if(toldCollectScoreCount==null){return "";}String resultJson = "\"toldCollectScoreCount\":";resultJson += "\"";resultJson += toldCollectScoreCount.ToString();resultJson += "\"";return resultJson;
}


public String get_createTime_json(){
if(createTime==null){return "";}String resultJson = "\"createTime\":";resultJson += "\"";resultJson += createTime.ToString();resultJson += "\"";return resultJson;
}


public String get_clubSetting_json(){
if(clubSetting==null){return "";}String resultJson = "\"clubSetting\":";resultJson += ((LantisBitProtocolBase)clubSetting).SerializerJson();return resultJson;
}


public void set_insertTag_fromJson(LitJson.JsonData jsonObj){
insertTag= Byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_groupId_fromJson(LitJson.JsonData jsonObj){
groupId= jsonObj.ToString();
}


public void set_iconUrl_fromJson(LitJson.JsonData jsonObj){
iconUrl= jsonObj.ToString();
}


public void set_groupName_fromJson(LitJson.JsonData jsonObj){
groupName= jsonObj.ToString();
}


public void set_sign_fromJson(LitJson.JsonData jsonObj){
sign= jsonObj.ToString();
}


public void set_groupMasterId_fromJson(LitJson.JsonData jsonObj){
groupMasterId= Int32.Parse(jsonObj.ToString());
}


public void set_groupMenberCount_fromJson(LitJson.JsonData jsonObj){
groupMenberCount= Int32.Parse(jsonObj.ToString());
}


public void set_rechargeCount_fromJson(LitJson.JsonData jsonObj){
rechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_toDayUseRechargeCount_fromJson(LitJson.JsonData jsonObj){
toDayUseRechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_toldUseRechargeCount_fromJson(LitJson.JsonData jsonObj){
toldUseRechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_toDayCollectScoreCount_fromJson(LitJson.JsonData jsonObj){
toDayCollectScoreCount= Int32.Parse(jsonObj.ToString());
}


public void set_toldCollectScoreCount_fromJson(LitJson.JsonData jsonObj){
toldCollectScoreCount= Int32.Parse(jsonObj.ToString());
}


public void set_createTime_fromJson(LitJson.JsonData jsonObj){
createTime= Int64.Parse(jsonObj.ToString());
}


public void set_clubSetting_fromJson(LitJson.JsonData jsonObj){
clubSetting= new P_ClubSetting();
clubSetting.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(insertTag !=  null){
resultStr += get_insertTag_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(groupId !=  null){
resultStr += ",";resultStr += get_groupId_json();
}
else {}if(iconUrl !=  null){
resultStr += ",";resultStr += get_iconUrl_json();
}
else {}if(groupName !=  null){
resultStr += ",";resultStr += get_groupName_json();
}
else {}if(sign !=  null){
resultStr += ",";resultStr += get_sign_json();
}
else {}if(groupMasterId !=  null){
resultStr += ",";resultStr += get_groupMasterId_json();
}
else {}if(groupMenberCount !=  null){
resultStr += ",";resultStr += get_groupMenberCount_json();
}
else {}if(rechargeCount !=  null){
resultStr += ",";resultStr += get_rechargeCount_json();
}
else {}if(toDayUseRechargeCount !=  null){
resultStr += ",";resultStr += get_toDayUseRechargeCount_json();
}
else {}if(toldUseRechargeCount !=  null){
resultStr += ",";resultStr += get_toldUseRechargeCount_json();
}
else {}if(toDayCollectScoreCount !=  null){
resultStr += ",";resultStr += get_toDayCollectScoreCount_json();
}
else {}if(toldCollectScoreCount !=  null){
resultStr += ",";resultStr += get_toldCollectScoreCount_json();
}
else {}if(createTime !=  null){
resultStr += ",";resultStr += get_createTime_json();
}
else {}if(clubSetting !=  null){
resultStr += ",";resultStr += get_clubSetting_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["insertTag"] != null){
set_insertTag_fromJson(jsonObj["insertTag"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["groupId"] != null){
set_groupId_fromJson(jsonObj["groupId"]);
}
if(jsonObj["iconUrl"] != null){
set_iconUrl_fromJson(jsonObj["iconUrl"]);
}
if(jsonObj["groupName"] != null){
set_groupName_fromJson(jsonObj["groupName"]);
}
if(jsonObj["sign"] != null){
set_sign_fromJson(jsonObj["sign"]);
}
if(jsonObj["groupMasterId"] != null){
set_groupMasterId_fromJson(jsonObj["groupMasterId"]);
}
if(jsonObj["groupMenberCount"] != null){
set_groupMenberCount_fromJson(jsonObj["groupMenberCount"]);
}
if(jsonObj["rechargeCount"] != null){
set_rechargeCount_fromJson(jsonObj["rechargeCount"]);
}
if(jsonObj["toDayUseRechargeCount"] != null){
set_toDayUseRechargeCount_fromJson(jsonObj["toDayUseRechargeCount"]);
}
if(jsonObj["toldUseRechargeCount"] != null){
set_toldUseRechargeCount_fromJson(jsonObj["toldUseRechargeCount"]);
}
if(jsonObj["toDayCollectScoreCount"] != null){
set_toDayCollectScoreCount_fromJson(jsonObj["toDayCollectScoreCount"]);
}
if(jsonObj["toldCollectScoreCount"] != null){
set_toldCollectScoreCount_fromJson(jsonObj["toldCollectScoreCount"]);
}
if(jsonObj["createTime"] != null){
set_createTime_fromJson(jsonObj["createTime"]);
}
if(jsonObj["clubSetting"] != null){
set_clubSetting_fromJson(jsonObj["clubSetting"]);
}
}
}
}
