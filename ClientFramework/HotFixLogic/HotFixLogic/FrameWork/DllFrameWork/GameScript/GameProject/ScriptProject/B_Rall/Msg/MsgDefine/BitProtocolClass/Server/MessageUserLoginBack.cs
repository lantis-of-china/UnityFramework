// 此文件由协议导出插件自动生成
// ID : 00006]
   
//****用户登录返回****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户登录返回
/// <\summary>
public class MessageUserLoginBack : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor UserValiadate;
/// <summary>
///
/// <\summary>
public UserValiadateInforWarp UserValiadateWarp;
/// <summary>
///
/// <\summary>
public List<GolabServerInfor> ChatServerList;
/// <summary>
///0 是失败 1是成功
/// <\summary>
public Int32 LoginState;
public MessageUserLoginBack(){}

public MessageUserLoginBack(UserValiadateInfor _UserValiadate, UserValiadateInforWarp _UserValiadateWarp, List<GolabServerInfor> _ChatServerList, Int32 _LoginState){
this.UserValiadate = _UserValiadate;
this.UserValiadateWarp = _UserValiadateWarp;
this.ChatServerList = _ChatServerList;
this.LoginState = _LoginState;
}
private byte[] get_UserValiadate_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadate).Serializer();
return outBuf;
}


private byte[] get_UserValiadateWarp_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)UserValiadateWarp).Serializer();
return outBuf;
}


private byte[] get_ChatServerList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<GolabServerInfor> listBase = ChatServerList;
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


private byte[] get_LoginState_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)LoginState);
return outBuf;
}

private int set_UserValiadate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadate = new UserValiadateInfor();
curIndex = UserValiadate.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_UserValiadateWarp_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UserValiadateWarp = new UserValiadateInforWarp();
curIndex = UserValiadateWarp.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_ChatServerList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ChatServerList = new List<GolabServerInfor>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
GolabServerInfor curTarget = new GolabServerInfor();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
ChatServerList.Add(curTarget);
}
}return curIndex;
}
private int set_LoginState_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
LoginState = new Int32();
LoginState = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(UserValiadate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(UserValiadateWarp !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UserValiadateWarp_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ChatServerList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ChatServerList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(LoginState !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_LoginState_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_UserValiadate_fromBuf(sourceBuf,startOffset);
startOffset = set_UserValiadateWarp_fromBuf(sourceBuf,startOffset);
startOffset = set_ChatServerList_fromBuf(sourceBuf,startOffset);
startOffset = set_LoginState_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_UserValiadate_json(){
if(UserValiadate==null){return "";}string resultJson = "\"UserValiadate\":";resultJson += ((CherishBitProtocolBase)UserValiadate).SerializerJson();return resultJson;
}


public string get_UserValiadateWarp_json(){
if(UserValiadateWarp==null){return "";}string resultJson = "\"UserValiadateWarp\":";resultJson += ((CherishBitProtocolBase)UserValiadateWarp).SerializerJson();return resultJson;
}


public string get_ChatServerList_json(){
if(ChatServerList==null){return "";}string resultJson = "\"ChatServerList\":";resultJson += "[";
List<GolabServerInfor> listObj = (List<GolabServerInfor>)ChatServerList;
for(int i = 0;i < listObj.Count;++i){
GolabServerInfor item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public string get_LoginState_json(){
if(LoginState==null){return "";}string resultJson = "\"LoginState\":";resultJson += "\"";resultJson += LoginState.ToString();resultJson += "\"";return resultJson;
}


public void set_UserValiadate_fromJson(LitJson.JsonData jsonObj){
UserValiadate= new UserValiadateInfor();
UserValiadate.DeserializerJson(jsonObj.ToJson());}


public void set_UserValiadateWarp_fromJson(LitJson.JsonData jsonObj){
UserValiadateWarp= new UserValiadateInforWarp();
UserValiadateWarp.DeserializerJson(jsonObj.ToJson());}


public void set_ChatServerList_fromJson(LitJson.JsonData jsonObj){
ChatServerList = new List<GolabServerInfor>();
foreach (LitJson.JsonData item in jsonObj){
GolabServerInfor addB = new GolabServerInfor();
ChatServerList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_LoginState_fromJson(LitJson.JsonData jsonObj){
LoginState= Int32.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(UserValiadate !=  null){
resultStr += get_UserValiadate_json();
}
else {}if(UserValiadateWarp !=  null){
resultStr += ",";resultStr += get_UserValiadateWarp_json();
}
else {}if(ChatServerList !=  null){
resultStr += ",";resultStr += get_ChatServerList_json();
}
else {}if(LoginState !=  null){
resultStr += ",";resultStr += get_LoginState_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["UserValiadate"] != null){
set_UserValiadate_fromJson(jsonObj["UserValiadate"]);
}
if(jsonObj["UserValiadateWarp"] != null){
set_UserValiadateWarp_fromJson(jsonObj["UserValiadateWarp"]);
}
if(jsonObj["ChatServerList"] != null){
set_ChatServerList_fromJson(jsonObj["ChatServerList"]);
}
if(jsonObj["LoginState"] != null){
set_LoginState_fromJson(jsonObj["LoginState"]);
}
}
}
}
