// 此文件由协议导出插件自动生成
// ID : 00001]
//****系统消息推送****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///系统消息推送
/// <\summary>
public class SC_RoleMsg : LantisBitProtocolBase {
/// <summary>
///消息列表
/// <\summary>
public List<P_MsgInfo> msgList;
public SC_RoleMsg(){}

public SC_RoleMsg(List<P_MsgInfo> _msgList){
this.msgList = _msgList;
}
private Byte[] get_msgList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_MsgInfo> listBase = msgList;
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

private int set_msgList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
msgList = new List<P_MsgInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_MsgInfo curTarget = new P_MsgInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
msgList.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(msgList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_msgList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_msgList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_msgList_json(){
if(msgList==null){return "";}String resultJson = "\"msgList\":";resultJson += "[";
List<P_MsgInfo> listObj = (List<P_MsgInfo>)msgList;
for(int i = 0;i < listObj.Count;++i){
P_MsgInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_msgList_fromJson(LitJson.JsonData jsonObj){
msgList = new List<P_MsgInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_MsgInfo addB = new P_MsgInfo();
msgList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(msgList !=  null){
resultStr += get_msgList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["msgList"] != null){
set_msgList_fromJson(jsonObj["msgList"]);
}
}
}
}
