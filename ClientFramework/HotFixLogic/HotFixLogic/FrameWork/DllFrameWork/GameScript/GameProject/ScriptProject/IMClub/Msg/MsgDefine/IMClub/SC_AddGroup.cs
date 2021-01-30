// 此文件由协议导出插件自动生成
// ID : 00001]
//****�������ֲ�S2C_AddGroup_MsgType = 20006****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�������ֲ�S2C_AddGroup_MsgType = 20006
/// <\summary>
public class SC_AddGroup : CherishBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public byte result;
/// <summary>
///Ⱥ����Ϣ
/// <\summary>
public P_GroupInfo groupInfo;
/// <summary>
///�����б�
/// <\summary>
public List<P_RoomInfo> thisRroomList;
public SC_AddGroup(){}

public SC_AddGroup(byte _result, P_GroupInfo _groupInfo, List<P_RoomInfo> _thisRroomList){
this.result = _result;
this.groupInfo = _groupInfo;
this.thisRroomList = _thisRroomList;
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


private byte[] get_thisRroomList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_RoomInfo> listBase = thisRroomList;
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
private int set_groupInfo_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupInfo = new P_GroupInfo();
curIndex = groupInfo.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_thisRroomList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
thisRroomList = new List<P_RoomInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_RoomInfo curTarget = new P_RoomInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
thisRroomList.Add(curTarget);
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
}if(groupInfo !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupInfo_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(thisRroomList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_thisRroomList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_groupInfo_fromBuf(sourceBuf,startOffset);
startOffset = set_thisRroomList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_groupInfo_json(){
if(groupInfo==null){return "";}string resultJson = "\"groupInfo\":";resultJson += ((CherishBitProtocolBase)groupInfo).SerializerJson();return resultJson;
}


public string get_thisRroomList_json(){
if(thisRroomList==null){return "";}string resultJson = "\"thisRroomList\":";resultJson += "[";
List<P_RoomInfo> listObj = (List<P_RoomInfo>)thisRroomList;
for(int i = 0;i < listObj.Count;++i){
P_RoomInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_groupInfo_fromJson(LitJson.JsonData jsonObj){
groupInfo= new P_GroupInfo();
groupInfo.DeserializerJson(jsonObj.ToJson());}


public void set_thisRroomList_fromJson(LitJson.JsonData jsonObj){
thisRroomList = new List<P_RoomInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_RoomInfo addB = new P_RoomInfo();
thisRroomList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(groupInfo !=  null){
resultStr += ",";resultStr += get_groupInfo_json();
}
else {}if(thisRroomList !=  null){
resultStr += ",";resultStr += get_thisRroomList_json();
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
if(jsonObj["thisRroomList"] != null){
set_thisRroomList_fromJson(jsonObj["thisRroomList"]);
}
}
}
}
