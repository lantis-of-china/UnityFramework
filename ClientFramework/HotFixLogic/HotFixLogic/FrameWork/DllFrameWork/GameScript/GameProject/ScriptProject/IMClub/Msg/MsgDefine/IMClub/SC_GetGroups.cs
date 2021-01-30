// 此文件由协议导出插件自动生成
// ID : 00001]
//****��ȡ��Ա��ϢS2C_GetGroups_MsgType = 20008****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��ȡ��Ա��ϢS2C_GetGroups_MsgType = 20008
/// <\summary>
public class SC_GetGroups : CherishBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public byte result;
/// <summary>
///Ⱥ���б���Ϣ
/// <\summary>
public List<P_GroupInfo> groupList;
/// <summary>
///�����б�
/// <\summary>
public List<P_RoomInfo> roomList;
/// <summary>
///�����б�
/// <\summary>
public List<P_RequestInfo> requestList;
public SC_GetGroups(){}

public SC_GetGroups(byte _result, List<P_GroupInfo> _groupList, List<P_RoomInfo> _roomList, List<P_RequestInfo> _requestList){
this.result = _result;
this.groupList = _groupList;
this.roomList = _roomList;
this.requestList = _requestList;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_groupList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_GroupInfo> listBase = groupList;
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


private byte[] get_roomList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_RoomInfo> listBase = roomList;
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


private byte[] get_requestList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_RequestInfo> listBase = requestList;
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
private int set_groupList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupList = new List<P_GroupInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_GroupInfo curTarget = new P_GroupInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
groupList.Add(curTarget);
}
}return curIndex;
}
private int set_roomList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roomList = new List<P_RoomInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_RoomInfo curTarget = new P_RoomInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
roomList.Add(curTarget);
}
}return curIndex;
}
private int set_requestList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
requestList = new List<P_RequestInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_RequestInfo curTarget = new P_RequestInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
requestList.Add(curTarget);
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
}if(groupList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(roomList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roomList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(requestList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_requestList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_groupList_fromBuf(sourceBuf,startOffset);
startOffset = set_roomList_fromBuf(sourceBuf,startOffset);
startOffset = set_requestList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_groupList_json(){
if(groupList==null){return "";}string resultJson = "\"groupList\":";resultJson += "[";
List<P_GroupInfo> listObj = (List<P_GroupInfo>)groupList;
for(int i = 0;i < listObj.Count;++i){
P_GroupInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public string get_roomList_json(){
if(roomList==null){return "";}string resultJson = "\"roomList\":";resultJson += "[";
List<P_RoomInfo> listObj = (List<P_RoomInfo>)roomList;
for(int i = 0;i < listObj.Count;++i){
P_RoomInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public string get_requestList_json(){
if(requestList==null){return "";}string resultJson = "\"requestList\":";resultJson += "[";
List<P_RequestInfo> listObj = (List<P_RequestInfo>)requestList;
for(int i = 0;i < listObj.Count;++i){
P_RequestInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_groupList_fromJson(LitJson.JsonData jsonObj){
groupList = new List<P_GroupInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_GroupInfo addB = new P_GroupInfo();
groupList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_roomList_fromJson(LitJson.JsonData jsonObj){
roomList = new List<P_RoomInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_RoomInfo addB = new P_RoomInfo();
roomList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_requestList_fromJson(LitJson.JsonData jsonObj){
requestList = new List<P_RequestInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_RequestInfo addB = new P_RequestInfo();
requestList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(groupList !=  null){
resultStr += ",";resultStr += get_groupList_json();
}
else {}if(roomList !=  null){
resultStr += ",";resultStr += get_roomList_json();
}
else {}if(requestList !=  null){
resultStr += ",";resultStr += get_requestList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["groupList"] != null){
set_groupList_fromJson(jsonObj["groupList"]);
}
if(jsonObj["roomList"] != null){
set_roomList_fromJson(jsonObj["roomList"]);
}
if(jsonObj["requestList"] != null){
set_requestList_fromJson(jsonObj["requestList"]);
}
}
}
}
