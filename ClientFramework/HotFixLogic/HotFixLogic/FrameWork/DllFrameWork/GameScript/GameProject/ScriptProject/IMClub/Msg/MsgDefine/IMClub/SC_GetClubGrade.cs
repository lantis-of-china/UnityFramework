// 此文件由协议导出插件自动生成
// ID : 00001]
//****��ȡ���ֲ�ս������S2C_GetClubGrade_MsgType = 20027****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��ȡ���ֲ�ս������S2C_GetClubGrade_MsgType = 20027
/// <\summary>
public class SC_GetClubGrade : LantisBitProtocolBase {
/// <summary>
///���
/// <\summary>
public Byte result;
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
/// <summary>
///ҳ��0��ʼ
/// <\summary>
public Byte page;
/// <summary>
///���ֲ��б�
/// <\summary>
public List<P_ClubGradeInfo> clubGradeList;
public SC_GetClubGrade(){}

public SC_GetClubGrade(Byte _result, String _clubId, Byte _page, List<P_ClubGradeInfo> _clubGradeList){
this.result = _result;
this.clubId = _clubId;
this.page = _page;
this.clubGradeList = _clubGradeList;
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


private Byte[] get_page_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)page;
return outBuf;
}


private Byte[] get_clubGradeList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_ClubGradeInfo> listBase = clubGradeList;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
LantisBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
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
private int set_page_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
page = new Byte();
page = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_clubGradeList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubGradeList = new List<P_ClubGradeInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_ClubGradeInfo curTarget = new P_ClubGradeInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
clubGradeList.Add(curTarget);
}
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
}if(page !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_page_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubGradeList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubGradeList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_page_fromBuf(sourceBuf,startOffset);
startOffset = set_clubGradeList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_page_json(){
if(page==null){return "";}String resultJson = "\"page\":";resultJson += "\"";resultJson += page.ToString();resultJson += "\"";return resultJson;
}


public String get_clubGradeList_json(){
if(clubGradeList==null){return "";}String resultJson = "\"clubGradeList\":";resultJson += "[";
List<P_ClubGradeInfo> listObj = (List<P_ClubGradeInfo>)clubGradeList;
for(int i = 0;i < listObj.Count;++i){
P_ClubGradeInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_page_fromJson(LitJson.JsonData jsonObj){
page= Byte.Parse(jsonObj.ToString());
}


public void set_clubGradeList_fromJson(LitJson.JsonData jsonObj){
clubGradeList = new List<P_ClubGradeInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_ClubGradeInfo addB = new P_ClubGradeInfo();
clubGradeList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(page !=  null){
resultStr += ",";resultStr += get_page_json();
}
else {}if(clubGradeList !=  null){
resultStr += ",";resultStr += get_clubGradeList_json();
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
if(jsonObj["page"] != null){
set_page_fromJson(jsonObj["page"]);
}
if(jsonObj["clubGradeList"] != null){
set_clubGradeList_fromJson(jsonObj["clubGradeList"]);
}
}
}
}
