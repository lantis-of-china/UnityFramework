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
public class SC_CreateGroup : CherishBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public byte result;
/// <summary>
///Ⱥ��Ϣ
/// <\summary>
public P_GroupInfo groupInfo;
public SC_CreateGroup(){}

public SC_CreateGroup(byte _result, P_GroupInfo _groupInfo){
this.result = _result;
this.groupInfo = _groupInfo;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_groupInfo_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)groupInfo).Serializer();
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
private int set_groupInfo_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupInfo = new P_GroupInfo();
curIndex = groupInfo.Deserializer(sourceBuf,curIndex);
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
}if(groupInfo !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupInfo_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_groupInfo_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_groupInfo_json(){
if(groupInfo==null){return "";}string resultJson = "\"groupInfo\":";resultJson += ((CherishBitProtocolBase)groupInfo).SerializerJson();return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_groupInfo_fromJson(LitJson.JsonData jsonObj){
groupInfo= new P_GroupInfo();
groupInfo.DeserializerJson(jsonObj.ToJson());}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(groupInfo !=  null){
resultStr += ",";resultStr += get_groupInfo_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
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
