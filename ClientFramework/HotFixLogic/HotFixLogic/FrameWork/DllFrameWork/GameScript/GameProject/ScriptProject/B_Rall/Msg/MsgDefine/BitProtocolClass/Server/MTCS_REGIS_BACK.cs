// 此文件由协议导出插件自动生成
// ID : 10001]
   
//****聊天服务器注册 返回结果数据****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///聊天服务器注册 返回结果数据
/// <\summary>
public class MTCS_REGIS_BACK : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public bool LoginSucess;
/// <summary>
///是否登陆成功
/// <\summary>
public GolabServerInfor GuestBookServerInfor;
/// <summary>
///留言板服务器注册信息
/// <\summary>
public GolabServerInfor DatingServerInfor;
/// <summary>
///聊天服务器列表
/// <\summary>
public List<GolabServerInfor> ChatServerList;
public MTCS_REGIS_BACK(){}

public MTCS_REGIS_BACK(bool _LoginSucess, GolabServerInfor _GuestBookServerInfor, GolabServerInfor _DatingServerInfor, List<GolabServerInfor> _ChatServerList){
this.LoginSucess = _LoginSucess;
this.GuestBookServerInfor = _GuestBookServerInfor;
this.DatingServerInfor = _DatingServerInfor;
this.ChatServerList = _ChatServerList;
}
private byte[] get_LoginSucess_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((bool)LoginSucess);
return outBuf;
}


private byte[] get_GuestBookServerInfor_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)GuestBookServerInfor).Serializer();
return outBuf;
}


private byte[] get_DatingServerInfor_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)DatingServerInfor).Serializer();
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

private int set_LoginSucess_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
LoginSucess = new bool();
LoginSucess = BitConverter.ToBoolean(sourceBuf,curIndex);
curIndex += 1;
}return curIndex;
}
private int set_GuestBookServerInfor_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
GuestBookServerInfor = new GolabServerInfor();
curIndex = GuestBookServerInfor.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_DatingServerInfor_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
DatingServerInfor = new GolabServerInfor();
curIndex = DatingServerInfor.Deserializer(sourceBuf,curIndex);
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
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(LoginSucess !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_LoginSucess_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(GuestBookServerInfor !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_GuestBookServerInfor_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(DatingServerInfor !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_DatingServerInfor_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ChatServerList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ChatServerList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_LoginSucess_fromBuf(sourceBuf,startOffset);
startOffset = set_GuestBookServerInfor_fromBuf(sourceBuf,startOffset);
startOffset = set_DatingServerInfor_fromBuf(sourceBuf,startOffset);
startOffset = set_ChatServerList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_LoginSucess_json(){
if(LoginSucess==null){return "";}string resultJson = "\"LoginSucess\":";resultJson += "\"";resultJson += LoginSucess.ToString();resultJson += "\"";return resultJson;
}


public string get_GuestBookServerInfor_json(){
if(GuestBookServerInfor==null){return "";}string resultJson = "\"GuestBookServerInfor\":";resultJson += ((CherishBitProtocolBase)GuestBookServerInfor).SerializerJson();return resultJson;
}


public string get_DatingServerInfor_json(){
if(DatingServerInfor==null){return "";}string resultJson = "\"DatingServerInfor\":";resultJson += ((CherishBitProtocolBase)DatingServerInfor).SerializerJson();return resultJson;
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


public void set_LoginSucess_fromJson(LitJson.JsonData jsonObj){
LoginSucess= bool.Parse(jsonObj.ToString());
}


public void set_GuestBookServerInfor_fromJson(LitJson.JsonData jsonObj){
GuestBookServerInfor= new GolabServerInfor();
GuestBookServerInfor.DeserializerJson(jsonObj.ToJson());}


public void set_DatingServerInfor_fromJson(LitJson.JsonData jsonObj){
DatingServerInfor= new GolabServerInfor();
DatingServerInfor.DeserializerJson(jsonObj.ToJson());}


public void set_ChatServerList_fromJson(LitJson.JsonData jsonObj){
ChatServerList = new List<GolabServerInfor>();
foreach (LitJson.JsonData item in jsonObj){
GolabServerInfor addB = new GolabServerInfor();
ChatServerList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override string SerializerJson(){
string resultStr = "{";if(LoginSucess !=  null){
resultStr += get_LoginSucess_json();
}
else {}if(GuestBookServerInfor !=  null){
resultStr += ",";resultStr += get_GuestBookServerInfor_json();
}
else {}if(DatingServerInfor !=  null){
resultStr += ",";resultStr += get_DatingServerInfor_json();
}
else {}if(ChatServerList !=  null){
resultStr += ",";resultStr += get_ChatServerList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["LoginSucess"] != null){
set_LoginSucess_fromJson(jsonObj["LoginSucess"]);
}
if(jsonObj["GuestBookServerInfor"] != null){
set_GuestBookServerInfor_fromJson(jsonObj["GuestBookServerInfor"]);
}
if(jsonObj["DatingServerInfor"] != null){
set_DatingServerInfor_fromJson(jsonObj["DatingServerInfor"]);
}
if(jsonObj["ChatServerList"] != null){
set_ChatServerList_fromJson(jsonObj["ChatServerList"]);
}
}
}
}
