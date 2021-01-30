// 此文件由协议导出插件自动生成
// ID : 00010]
//****提示消息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///提示消息
/// <\summary>
public class TipMessage_SC : CherishBitProtocolBase {
/// <summary>
///提示消息参数
/// <\summary>
public Int32 messageType;
/// <summary>
///提示消息参数
/// <\summary>
public List<Int32> messagePamars;
public TipMessage_SC(){}

public TipMessage_SC(Int32 _messageType, List<Int32> _messagePamars){
this.messageType = _messageType;
this.messagePamars = _messagePamars;
}
private byte[] get_messageType_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)messageType);
return outBuf;
}


private byte[] get_messagePamars_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)messagePamars;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_messageType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
messageType = new Int32();
messageType = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_messagePamars_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
messagePamars = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
messagePamars.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(messageType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_messageType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(messagePamars !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_messagePamars_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_messageType_fromBuf(sourceBuf,startOffset);
startOffset = set_messagePamars_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_messageType_json(){
if(messageType==null){return "";}string resultJson = "\"messageType\":";resultJson += "\"";resultJson += messageType.ToString();resultJson += "\"";return resultJson;
}


public string get_messagePamars_json(){
if(messagePamars==null){return "";}string resultJson = "\"messagePamars\":";resultJson += "[";List<Int32> listObj = (List<Int32>)messagePamars;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_messageType_fromJson(LitJson.JsonData jsonObj){
messageType= Int32.Parse(jsonObj.ToString());
}


public void set_messagePamars_fromJson(LitJson.JsonData jsonObj){
messagePamars= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
messagePamars.Add(Int32.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(messageType !=  null){
resultStr += get_messageType_json();
}
else {}if(messagePamars !=  null){
resultStr += ",";resultStr += get_messagePamars_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["messageType"] != null){
set_messageType_fromJson(jsonObj["messageType"]);
}
if(jsonObj["messagePamars"] != null){
set_messagePamars_fromJson(jsonObj["messagePamars"]);
}
}
}
}
