// 此文件由协议导出插件自动生成
// ID : 00010]
   
//****创建角色返回****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///创建角色返回
/// <\summary>
public class CreateRoleBack : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 state;
public CreateRoleBack(){}

public CreateRoleBack(Int32 _state){
this.state = _state;
}
private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)state);
return outBuf;
}

private int set_state_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new Int32();
state = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
state= Int32.Parse(jsonObj.ToString());
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
