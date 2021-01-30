// 此文件由协议导出插件自动生成
// ID : 00001]

//****服务器信息****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///服务器信息
/// <\summary>
public class GolabServerInfor : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public string ServerIp;
/// <summary>
///登陆服务器的IP
/// <\summary>
public Int32 TcpServerPort;
/// <summary>
///登陆服务器对外端口
/// <\summary>
public Int32 UdpServerPort;
/// <summary>
///Udp协议 对外端口
/// <\summary>
public Int32 ClientCount;
/// <summary>
///服务器在线数量
/// <\summary>
public string LoginPassWord;
/// <summary>
///登陆服务器配置的登陆密码
/// <\summary>
public string ServerId;
/// <summary>
///登陆服务器对外的Id
/// <\summary>
public string ServerName;
/// <summary>
///登陆服务器名字
/// <\summary>
public Int32 MinDatingNumber;
/// <summary>
///最小的约号
/// <\summary>
public Int32 MaxDatingNumber;
/// <summary>
///最大的约号
/// <\summary>
public byte gameType;
/// <summary>
///游戏类型
/// <\summary>
public byte gameMode;
/// <summary>
///0房卡模式 1金币场
/// <\summary>
public Int32 ServerMinRoomNumber;
/// <summary>
///房间号 低位
/// <\summary>
public Int32 ServerMaxRoomNumber;
/// <summary>
///房间参数定
/// <\summary>
public List<Int32> RoomParmars;
public GolabServerInfor(){}

public GolabServerInfor(string _ServerIp, Int32 _TcpServerPort, Int32 _UdpServerPort, Int32 _ClientCount, string _LoginPassWord, string _ServerId, string _ServerName, Int32 _MinDatingNumber, Int32 _MaxDatingNumber, byte _gameType, byte _gameMode, Int32 _ServerMinRoomNumber, Int32 _ServerMaxRoomNumber, List<Int32> _RoomParmars){
this.ServerIp = _ServerIp;
this.TcpServerPort = _TcpServerPort;
this.UdpServerPort = _UdpServerPort;
this.ClientCount = _ClientCount;
this.LoginPassWord = _LoginPassWord;
this.ServerId = _ServerId;
this.ServerName = _ServerName;
this.MinDatingNumber = _MinDatingNumber;
this.MaxDatingNumber = _MaxDatingNumber;
this.gameType = _gameType;
this.gameMode = _gameMode;
this.ServerMinRoomNumber = _ServerMinRoomNumber;
this.ServerMaxRoomNumber = _ServerMaxRoomNumber;
this.RoomParmars = _RoomParmars;
}
private byte[] get_ServerIp_encoding(){
byte[] outBuf = null;
string str = (string)ServerIp;
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


private byte[] get_TcpServerPort_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)TcpServerPort);
return outBuf;
}


private byte[] get_UdpServerPort_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)UdpServerPort);
return outBuf;
}


private byte[] get_ClientCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)ClientCount);
return outBuf;
}


private byte[] get_LoginPassWord_encoding(){
byte[] outBuf = null;
string str = (string)LoginPassWord;
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


private byte[] get_ServerId_encoding(){
byte[] outBuf = null;
string str = (string)ServerId;
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


private byte[] get_ServerName_encoding(){
byte[] outBuf = null;
string str = (string)ServerName;
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


private byte[] get_MinDatingNumber_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)MinDatingNumber);
return outBuf;
}


private byte[] get_MaxDatingNumber_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)MaxDatingNumber);
return outBuf;
}


private byte[] get_gameType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)gameType;
return outBuf;
}


private byte[] get_gameMode_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)gameMode;
return outBuf;
}


private byte[] get_ServerMinRoomNumber_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)ServerMinRoomNumber);
return outBuf;
}


private byte[] get_ServerMaxRoomNumber_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)ServerMaxRoomNumber);
return outBuf;
}


private byte[] get_RoomParmars_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)RoomParmars;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_ServerIp_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ServerIp = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ServerIp = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_TcpServerPort_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
TcpServerPort = new Int32();
TcpServerPort = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_UdpServerPort_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
UdpServerPort = new Int32();
UdpServerPort = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_ClientCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ClientCount = new Int32();
ClientCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_LoginPassWord_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
LoginPassWord = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
LoginPassWord = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ServerId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ServerId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ServerId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ServerName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ServerName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ServerName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_MinDatingNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
MinDatingNumber = new Int32();
MinDatingNumber = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_MaxDatingNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
MaxDatingNumber = new Int32();
MaxDatingNumber = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_gameType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameType = new byte();
gameType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_gameMode_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameMode = new byte();
gameMode = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_ServerMinRoomNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ServerMinRoomNumber = new Int32();
ServerMinRoomNumber = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_ServerMaxRoomNumber_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ServerMaxRoomNumber = new Int32();
ServerMaxRoomNumber = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_RoomParmars_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
RoomParmars = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
RoomParmars.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(ServerIp !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ServerIp_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(TcpServerPort !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_TcpServerPort_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(UdpServerPort !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_UdpServerPort_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ClientCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ClientCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(LoginPassWord !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_LoginPassWord_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ServerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ServerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ServerName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ServerName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(MinDatingNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_MinDatingNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(MaxDatingNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_MaxDatingNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameMode !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameMode_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ServerMinRoomNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ServerMinRoomNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ServerMaxRoomNumber !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ServerMaxRoomNumber_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(RoomParmars !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_RoomParmars_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_ServerIp_fromBuf(sourceBuf,startOffset);
startOffset = set_TcpServerPort_fromBuf(sourceBuf,startOffset);
startOffset = set_UdpServerPort_fromBuf(sourceBuf,startOffset);
startOffset = set_ClientCount_fromBuf(sourceBuf,startOffset);
startOffset = set_LoginPassWord_fromBuf(sourceBuf,startOffset);
startOffset = set_ServerId_fromBuf(sourceBuf,startOffset);
startOffset = set_ServerName_fromBuf(sourceBuf,startOffset);
startOffset = set_MinDatingNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_MaxDatingNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_gameType_fromBuf(sourceBuf,startOffset);
startOffset = set_gameMode_fromBuf(sourceBuf,startOffset);
startOffset = set_ServerMinRoomNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_ServerMaxRoomNumber_fromBuf(sourceBuf,startOffset);
startOffset = set_RoomParmars_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_ServerIp_json(){
if(ServerIp==null){return "";}string resultJson = "\"ServerIp\":";resultJson += "\"";resultJson += ServerIp.ToString();resultJson += "\"";return resultJson;
}


public string get_TcpServerPort_json(){
if(TcpServerPort==null){return "";}string resultJson = "\"TcpServerPort\":";resultJson += "\"";resultJson += TcpServerPort.ToString();resultJson += "\"";return resultJson;
}


public string get_UdpServerPort_json(){
if(UdpServerPort==null){return "";}string resultJson = "\"UdpServerPort\":";resultJson += "\"";resultJson += UdpServerPort.ToString();resultJson += "\"";return resultJson;
}


public string get_ClientCount_json(){
if(ClientCount==null){return "";}string resultJson = "\"ClientCount\":";resultJson += "\"";resultJson += ClientCount.ToString();resultJson += "\"";return resultJson;
}


public string get_LoginPassWord_json(){
if(LoginPassWord==null){return "";}string resultJson = "\"LoginPassWord\":";resultJson += "\"";resultJson += LoginPassWord.ToString();resultJson += "\"";return resultJson;
}


public string get_ServerId_json(){
if(ServerId==null){return "";}string resultJson = "\"ServerId\":";resultJson += "\"";resultJson += ServerId.ToString();resultJson += "\"";return resultJson;
}


public string get_ServerName_json(){
if(ServerName==null){return "";}string resultJson = "\"ServerName\":";resultJson += "\"";resultJson += ServerName.ToString();resultJson += "\"";return resultJson;
}


public string get_MinDatingNumber_json(){
if(MinDatingNumber==null){return "";}string resultJson = "\"MinDatingNumber\":";resultJson += "\"";resultJson += MinDatingNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_MaxDatingNumber_json(){
if(MaxDatingNumber==null){return "";}string resultJson = "\"MaxDatingNumber\":";resultJson += "\"";resultJson += MaxDatingNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_gameType_json(){
if(gameType==null){return "";}string resultJson = "\"gameType\":";resultJson += "\"";resultJson += gameType.ToString();resultJson += "\"";return resultJson;
}


public string get_gameMode_json(){
if(gameMode==null){return "";}string resultJson = "\"gameMode\":";resultJson += "\"";resultJson += gameMode.ToString();resultJson += "\"";return resultJson;
}


public string get_ServerMinRoomNumber_json(){
if(ServerMinRoomNumber==null){return "";}string resultJson = "\"ServerMinRoomNumber\":";resultJson += "\"";resultJson += ServerMinRoomNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_ServerMaxRoomNumber_json(){
if(ServerMaxRoomNumber==null){return "";}string resultJson = "\"ServerMaxRoomNumber\":";resultJson += "\"";resultJson += ServerMaxRoomNumber.ToString();resultJson += "\"";return resultJson;
}


public string get_RoomParmars_json(){
if(RoomParmars==null){return "";}string resultJson = "\"RoomParmars\":";resultJson += "[";List<Int32> listObj = (List<Int32>)RoomParmars;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_ServerIp_fromJson(LitJson.JsonData jsonObj){
ServerIp= jsonObj.ToString();
}


public void set_TcpServerPort_fromJson(LitJson.JsonData jsonObj){
TcpServerPort= Int32.Parse(jsonObj.ToString());
}


public void set_UdpServerPort_fromJson(LitJson.JsonData jsonObj){
UdpServerPort= Int32.Parse(jsonObj.ToString());
}


public void set_ClientCount_fromJson(LitJson.JsonData jsonObj){
ClientCount= Int32.Parse(jsonObj.ToString());
}


public void set_LoginPassWord_fromJson(LitJson.JsonData jsonObj){
LoginPassWord= jsonObj.ToString();
}


public void set_ServerId_fromJson(LitJson.JsonData jsonObj){
ServerId= jsonObj.ToString();
}


public void set_ServerName_fromJson(LitJson.JsonData jsonObj){
ServerName= jsonObj.ToString();
}


public void set_MinDatingNumber_fromJson(LitJson.JsonData jsonObj){
MinDatingNumber= Int32.Parse(jsonObj.ToString());
}


public void set_MaxDatingNumber_fromJson(LitJson.JsonData jsonObj){
MaxDatingNumber= Int32.Parse(jsonObj.ToString());
}


public void set_gameType_fromJson(LitJson.JsonData jsonObj){
gameType= byte.Parse(jsonObj.ToString());
}


public void set_gameMode_fromJson(LitJson.JsonData jsonObj){
gameMode= byte.Parse(jsonObj.ToString());
}


public void set_ServerMinRoomNumber_fromJson(LitJson.JsonData jsonObj){
ServerMinRoomNumber= Int32.Parse(jsonObj.ToString());
}


public void set_ServerMaxRoomNumber_fromJson(LitJson.JsonData jsonObj){
ServerMaxRoomNumber= Int32.Parse(jsonObj.ToString());
}


public void set_RoomParmars_fromJson(LitJson.JsonData jsonObj){
RoomParmars= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
RoomParmars.Add(Int32.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(ServerIp !=  null){
resultStr += get_ServerIp_json();
}
else {}if(TcpServerPort !=  null){
resultStr += ",";resultStr += get_TcpServerPort_json();
}
else {}if(UdpServerPort !=  null){
resultStr += ",";resultStr += get_UdpServerPort_json();
}
else {}if(ClientCount !=  null){
resultStr += ",";resultStr += get_ClientCount_json();
}
else {}if(LoginPassWord !=  null){
resultStr += ",";resultStr += get_LoginPassWord_json();
}
else {}if(ServerId !=  null){
resultStr += ",";resultStr += get_ServerId_json();
}
else {}if(ServerName !=  null){
resultStr += ",";resultStr += get_ServerName_json();
}
else {}if(MinDatingNumber !=  null){
resultStr += ",";resultStr += get_MinDatingNumber_json();
}
else {}if(MaxDatingNumber !=  null){
resultStr += ",";resultStr += get_MaxDatingNumber_json();
}
else {}if(gameType !=  null){
resultStr += ",";resultStr += get_gameType_json();
}
else {}if(gameMode !=  null){
resultStr += ",";resultStr += get_gameMode_json();
}
else {}if(ServerMinRoomNumber !=  null){
resultStr += ",";resultStr += get_ServerMinRoomNumber_json();
}
else {}if(ServerMaxRoomNumber !=  null){
resultStr += ",";resultStr += get_ServerMaxRoomNumber_json();
}
else {}if(RoomParmars !=  null){
resultStr += ",";resultStr += get_RoomParmars_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["ServerIp"] != null){
set_ServerIp_fromJson(jsonObj["ServerIp"]);
}
if(jsonObj["TcpServerPort"] != null){
set_TcpServerPort_fromJson(jsonObj["TcpServerPort"]);
}
if(jsonObj["UdpServerPort"] != null){
set_UdpServerPort_fromJson(jsonObj["UdpServerPort"]);
}
if(jsonObj["ClientCount"] != null){
set_ClientCount_fromJson(jsonObj["ClientCount"]);
}
if(jsonObj["LoginPassWord"] != null){
set_LoginPassWord_fromJson(jsonObj["LoginPassWord"]);
}
if(jsonObj["ServerId"] != null){
set_ServerId_fromJson(jsonObj["ServerId"]);
}
if(jsonObj["ServerName"] != null){
set_ServerName_fromJson(jsonObj["ServerName"]);
}
if(jsonObj["MinDatingNumber"] != null){
set_MinDatingNumber_fromJson(jsonObj["MinDatingNumber"]);
}
if(jsonObj["MaxDatingNumber"] != null){
set_MaxDatingNumber_fromJson(jsonObj["MaxDatingNumber"]);
}
if(jsonObj["gameType"] != null){
set_gameType_fromJson(jsonObj["gameType"]);
}
if(jsonObj["gameMode"] != null){
set_gameMode_fromJson(jsonObj["gameMode"]);
}
if(jsonObj["ServerMinRoomNumber"] != null){
set_ServerMinRoomNumber_fromJson(jsonObj["ServerMinRoomNumber"]);
}
if(jsonObj["ServerMaxRoomNumber"] != null){
set_ServerMaxRoomNumber_fromJson(jsonObj["ServerMaxRoomNumber"]);
}
if(jsonObj["RoomParmars"] != null){
set_RoomParmars_fromJson(jsonObj["RoomParmars"]);
}
}
}
}
