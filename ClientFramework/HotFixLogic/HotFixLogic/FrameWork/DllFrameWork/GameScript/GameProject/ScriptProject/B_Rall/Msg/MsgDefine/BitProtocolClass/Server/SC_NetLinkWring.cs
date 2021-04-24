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
public class SC_NetLinkWring : LantisBitProtocolBase {
/// <summary>
///1 断开连接 2 心跳超时断开连接 3 其他地方登陆 4 退出登陆
/// <\summary>
public Byte state;
public SC_NetLinkWring(){}

public SC_NetLinkWring(Byte _state){
this.state = _state;
}
private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)state;
return outBuf;
}

private int set_state_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new Byte();
state = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(state !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_state_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_state_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(state !=  null){
resultStr += get_state_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
}
}
}
