// 此文件由协议导出插件自动生成
// ID : 00008]
//****游戏的网络连接推送****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///游戏的网络连接推送
/// <\summary>
public class SC_NetLinkWring : CherishBitProtocolBase {
/// <summary>
///1 断开连接 2 心跳超时断开连接 3 其他地方登陆 4 退出登陆
/// <\summary>
public byte state;
public SC_NetLinkWring(){}

public SC_NetLinkWring(byte _state){
this.state = _state;
}
private byte[] get_state_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)state;
return outBuf;
}

private int set_state_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new byte();
state = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(state !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_state_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_state_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_state_json(){
if(state==null){return "";}string resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= byte.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(state !=  null){
resultStr += get_state_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
}
}
}
