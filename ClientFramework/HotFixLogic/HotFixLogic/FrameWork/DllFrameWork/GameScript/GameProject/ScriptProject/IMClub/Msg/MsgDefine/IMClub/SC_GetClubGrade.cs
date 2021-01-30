﻿// 此文件由协议导出插件自动生成
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
public class SC_GetClubGrade : CherishBitProtocolBase {
/// <summary>
///����
/// <\summary>
public byte result;
/// <summary>
///���ֲ�ID
/// <\summary>
public string clubId;
/// <summary>
///ҳ��0��ʼ
/// <\summary>
public byte page;
/// <summary>
///���ֲ��б�
/// <\summary>
public List<P_ClubGradeInfo> clubGradeList;
public SC_GetClubGrade(){}

public SC_GetClubGrade(byte _result, string _clubId, byte _page, List<P_ClubGradeInfo> _clubGradeList){
this.result = _result;
this.clubId = _clubId;
this.page = _page;
this.clubGradeList = _clubGradeList;
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


private byte[] get_page_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)page;
return outBuf;
}


private byte[] get_clubGradeList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_ClubGradeInfo> listBase = clubGradeList;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
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
private int set_page_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
page = new byte();
page = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_clubGradeList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_page_fromBuf(sourceBuf,startOffset);
startOffset = set_clubGradeList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_page_json(){
if(page==null){return "";}string resultJson = "\"page\":";resultJson += "\"";resultJson += page.ToString();resultJson += "\"";return resultJson;
}


public string get_clubGradeList_json(){
if(clubGradeList==null){return "";}string resultJson = "\"clubGradeList\":";resultJson += "[";
List<P_ClubGradeInfo> listObj = (List<P_ClubGradeInfo>)clubGradeList;
for(int i = 0;i < listObj.Count;++i){
P_ClubGradeInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_page_fromJson(LitJson.JsonData jsonObj){
page= byte.Parse(jsonObj.ToString());
}


public void set_clubGradeList_fromJson(LitJson.JsonData jsonObj){
clubGradeList = new List<P_ClubGradeInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_ClubGradeInfo addB = new P_ClubGradeInfo();
clubGradeList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
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

public override void DeserializerJson(string json){
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