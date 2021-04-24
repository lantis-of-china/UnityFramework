// 此文件由协议导出插件自动生成
// ID : 00001]
//****��Ա����[ʹ��֪ͨ=joinUser]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��Ա����[ʹ��֪ͨ=joinUser]
/// <\summary>
public class SC_MenberJoin : LantisBitProtocolBase {
/// <summary>
///Ⱥ��ID
/// <\summary>
public String groupId;
/// <summary>
///���Ƴ�Ⱥ�ĳ�ԱID
/// <\summary>
public P_Menber menberInfo;
public SC_MenberJoin(){}

public SC_MenberJoin(String _groupId, P_Menber _menberInfo){
this.groupId = _groupId;
this.menberInfo = _menberInfo;
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


private Byte[] get_menberInfo_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)menberInfo).Serializer();
return outBuf;
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
private int set_menberInfo_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberInfo = new P_Menber();
curIndex = menberInfo.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(groupId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberInfo !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberInfo_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_groupId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberInfo_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_groupId_json(){
if(groupId==null){return "";}String resultJson = "\"groupId\":";resultJson += "\"";resultJson += groupId.ToString();resultJson += "\"";return resultJson;
}


public String get_menberInfo_json(){
if(menberInfo==null){return "";}String resultJson = "\"menberInfo\":";resultJson += ((LantisBitProtocolBase)menberInfo).SerializerJson();return resultJson;
}


public void set_groupId_fromJson(LitJson.JsonData jsonObj){
groupId= jsonObj.ToString();
}


public void set_menberInfo_fromJson(LitJson.JsonData jsonObj){
menberInfo= new P_Menber();
menberInfo.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(groupId !=  null){
resultStr += get_groupId_json();
}
else {}if(menberInfo !=  null){
resultStr += ",";resultStr += get_menberInfo_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["groupId"] != null){
set_groupId_fromJson(jsonObj["groupId"]);
}
if(jsonObj["menberInfo"] != null){
set_menberInfo_fromJson(jsonObj["menberInfo"]);
}
}
}
}
