// 此文件由协议导出插件自动生成
// ID : 00001]
//****���ֲ���ȡս������C2S_GetClubGrade_MsgType = 20026****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///���ֲ���ȡս������C2S_GetClubGrade_MsgType = 20026
/// <\summary>
public class CS_GetClubGrade : LantisBitProtocolBase {
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
/// <summary>
///ҳ��0��ʼ
/// <\summary>
public Byte page;
/// <summary>
///��Ϣ��֤
/// <\summary>
public UserValiadateInfor UserValiadate;
public CS_GetClubGrade(){}

public CS_GetClubGrade(String _clubId, Byte _page, UserValiadateInfor _UserValiadate){
this.clubId = _clubId;
this.page = _page;
this.UserValiadate = _UserValiadate;
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


private Byte[] get_UserValiadate_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)UserValiadate).Serializer();
return outBuf;
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
private int set_UserValiadate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(clubId !=  null){
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
}if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_page_fromBuf(sourceBuf,startOffset);
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_page_json(){
if(page==null){return "";}String resultJson = "\"page\":";resultJson += "\"";resultJson += page.ToString();resultJson += "\"";return resultJson;
}


public String get_UserValiadate_json(){
if(UserValiadate==null){return "";}String resultJson = "\"UserValiadate\":";resultJson += ((LantisBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_page_fromJson(LitJson.JsonData jsonObj){
page= Byte.Parse(jsonObj.ToString());
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(clubId !=  null){
resultStr += get_clubId_json();
}
else {}if(page !=  null){
resultStr += ",";resultStr += get_page_json();
}
else {}if(UserValiadate !=  null){
resultStr += ",";resultStr += get_UserValiadate_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["page"] != null){
set_page_fromJson(jsonObj["page"]);
}
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
}
}
}
