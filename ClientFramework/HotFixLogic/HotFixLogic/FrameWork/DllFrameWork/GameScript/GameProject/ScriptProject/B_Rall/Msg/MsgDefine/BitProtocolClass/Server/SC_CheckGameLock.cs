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
public class SC_CheckGameLock : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public byte state;
/// <summary>
///0不能进入 1可以进入
/// <\summary>
public string id;
/// <summary>
///
/// <\summary>
public string ip;
/// <summary>
///
/// <\summary>
public Int32 port;
public SC_CheckGameLock(){}

public SC_CheckGameLock(byte _state, string _id, string _ip, Int32 _port){
this.state = _state;
this.id = _id;
this.ip = _ip;
this.port = _port;
}
private byte[] get_state_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)state;
return outBuf;
}


private byte[] get_id_encoding(){
byte[] outBuf = null;
string str = (string)id;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_ip_encoding(){
byte[] outBuf = null;
string str = (string)ip;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_port_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)port);
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
private int set_id_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_ip_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_port_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
port = new Int32();
port = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_state_fromBuf(sourceBuf,startOffset);
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_ip_fromBuf(sourceBuf,startOffset);
startOffset = set_port_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_state_json(){
if(state==null){return "";}string resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public string get_id_json(){
if(id==null){return "";}string resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public string get_ip_json(){
if(ip==null){return "";}string resultJson = "\"ip\":";resultJson += "\"";resultJson += ip.ToString();resultJson += "\"";return resultJson;
}


public string get_port_json(){
if(port==null){return "";}string resultJson = "\"port\":";resultJson += "\"";resultJson += port.ToString();resultJson += "\"";return resultJson;
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= byte.Parse(jsonObj.ToString());
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

public override string SerializerJson(){
string resultStr = "{";if(state !=  null){
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

public override void DeserializerJson(string json){
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
