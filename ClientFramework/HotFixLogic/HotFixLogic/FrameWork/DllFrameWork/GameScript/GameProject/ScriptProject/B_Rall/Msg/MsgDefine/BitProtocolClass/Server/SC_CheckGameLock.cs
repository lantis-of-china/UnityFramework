// 此文件由协议导出插件自动生成
// ID : 00001]
//********
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///
/// <\summary>
public class SC_CheckGameLock : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Byte state;
/// <summary>
///0不能进入 1可以进入
/// <\summary>
public String id;
/// <summary>
///
/// <\summary>
public String ip;
/// <summary>
///
/// <\summary>
public Int32 port;
public SC_CheckGameLock(){}

public SC_CheckGameLock(Byte _state, String _id, String _ip, Int32 _port){
this.state = _state;
this.id = _id;
this.ip = _ip;
this.port = _port;
}
private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)state;
return outBuf;
}


private Byte[] get_id_encoding(){
Byte[] outBuf = null;
String str = (String)id;
Char[] charArray = str.ToCharArray();
Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
Byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private Byte[] get_ip_encoding(){
Byte[] outBuf = null;
String str = (String)ip;
Char[] charArray = str.ToCharArray();
Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
Byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private Byte[] get_port_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)port);
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
private int set_id_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
id = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
id = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ip_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ip = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ip = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_port_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
port = new Int32();
port = BitConverter.ToInt32(sourceBuf,curIndex);
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
}if(id !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_id_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ip !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ip_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(port !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_port_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_state_fromBuf(sourceBuf,startOffset);
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_ip_fromBuf(sourceBuf,startOffset);
startOffset = set_port_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public String get_id_json(){
if(id==null){return "";}String resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public String get_ip_json(){
if(ip==null){return "";}String resultJson = "\"ip\":";resultJson += "\"";resultJson += ip.ToString();resultJson += "\"";return resultJson;
}


public String get_port_json(){
if(port==null){return "";}String resultJson = "\"port\":";resultJson += "\"";resultJson += port.ToString();resultJson += "\"";return resultJson;
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Byte.Parse(jsonObj.ToString());
}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= jsonObj.ToString();
}


public void set_ip_fromJson(LitJson.JsonData jsonObj){
ip= jsonObj.ToString();
}


public void set_port_fromJson(LitJson.JsonData jsonObj){
port= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(state !=  null){
resultStr += get_state_json();
}
else {}if(id !=  null){
resultStr += ",";resultStr += get_id_json();
}
else {}if(ip !=  null){
resultStr += ",";resultStr += get_ip_json();
}
else {}if(port !=  null){
resultStr += ",";resultStr += get_port_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
if(jsonObj["id"] != null){
set_id_fromJson(jsonObj["id"]);
}
if(jsonObj["ip"] != null){
set_ip_fromJson(jsonObj["ip"]);
}
if(jsonObj["port"] != null){
set_port_fromJson(jsonObj["port"]);
}
}
}
}
