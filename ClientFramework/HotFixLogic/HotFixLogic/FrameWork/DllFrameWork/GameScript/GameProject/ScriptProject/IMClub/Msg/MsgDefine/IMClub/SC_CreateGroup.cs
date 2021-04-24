// 此文件由协议导出插件自动生成
// ID : 00001]
//****�������ֲ�S2C_CreateGroup_MsgType = 20002****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�������ֲ�S2C_CreateGroup_MsgType = 20002
/// <\summary>
public class SC_CreateGroup : LantisBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public Byte result;
/// <summary>
///Ⱥ��Ϣ
/// <\summary>
public P_GroupInfo groupInfo;
public SC_CreateGroup(){}

public SC_CreateGroup(Byte _result, P_GroupInfo _groupInfo){
this.result = _result;
this.groupInfo = _groupInfo;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
return outBuf;
}


private Byte[] get_groupInfo_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)groupInfo).Serializer();
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
private int set_groupInfo_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupInfo = new P_GroupInfo();
curIndex = groupInfo.Deserializer(sourceBuf,curIndex);
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
}if(groupInfo !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupInfo_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_groupInfo_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_groupInfo_json(){
if(groupInfo==null){return "";}String resultJson = "\"groupInfo\":";resultJson += ((LantisBitProtocolBase)groupInfo).SerializerJson();return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_groupInfo_fromJson(LitJson.JsonData jsonObj){
groupInfo= new P_GroupInfo();
groupInfo.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(groupInfo !=  null){
resultStr += ",";resultStr += get_groupInfo_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["groupInfo"] != null){
set_groupInfo_fromJson(jsonObj["groupInfo"]);
}
}
}
}
