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
public class UserValiadateInforWarp : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor value;
/// <summary>
///
/// <\summary>
public float hasTime;
/// <summary>
///代理码
/// <\summary>
public string agentCode;
/// <summary>
///后台管理码
/// <\summary>
public string backControlCode;
/// <summary>
///性别
/// <\summary>
public Int32 Sex;
/// <summary>
///
/// <\summary>
public Int32 Gold;
/// <summary>
///金币
/// <\summary>
public Int32 RechargeCount;
/// <summary>
///钻石
/// <\summary>
public Int32 rechargeBank;
/// <summary>
///银行钻石
/// <\summary>
public Int32 goldBank;
/// <summary>
///银行金币
/// <\summary>
public string bankPassword;
/// <summary>
///银行密码
/// <\summary>
public Int32 signTimes;
/// <summary>
///签到次数
/// <\summary>
public Int64 signDate;
/// <summary>
///最近签到日期
/// <\summary>
public string PikeName;
/// <summary>
///名字
/// <\summary>
public string headUrl;
/// <summary>
///头像连接
/// <\summary>
public byte isWeiChat;
/// <summary>
///是微信登陆
/// <\summary>
public string serverId;
/// <summary>
///在哪个游戏中
/// <\summary>
public Int64 loginDate;
/// <summary>
///登录日期
/// <\summary>
public Int64 registDate;
/// <summary>
///注册日期
/// <\summary>
public Int32 EntryGameGold;
/// <summary>
///游戏带入金币
/// <\summary>
public Int32 EntryGameRechargeCount;
/// <summary>
///游戏带入钻石
/// <\summary>
public List<string> groupList;
/// <summary>
///加入的组群队列
/// <\summary>
public Int32 winMinControlScale;
/// <summary>
///最大概率控制
/// <\summary>
public Int32 winMaxControlScale;
/// <summary>
///最大赢取阈值 以个人资产作为基数
/// <\summary>
public Int32 winMaxScale;
/// <summary>
///最小赢取阈值 以个人资产作为基数
/// <\summary>
public Int32 winMinScale;
/// <summary>
///最近概率控制
/// <\summary>
public Int32 curControlScale;
/// <summary>
///阈值控制
/// <\summary>
public Int32 curScale;
/// <summary>
///最大赢取阈值 以个人资产作为基数
/// <\summary>
public Int32 recordScore;
/// <summary>
///分数记录
/// <\summary>
public Int32 scoreChange;
/// <summary>
///实名认证
/// <\summary>
public string realName;
/// <summary>
///实名ID
/// <\summary>
public string realId;
/// <summary>
///手机号
/// <\summary>
public string realPhone;
/// <summary>
///UDP IP地址
/// <\summary>
public string ipaddr;
/// <summary>
///UDP 接收端口
/// <\summary>
public string port;
/// <summary>
///纬度
/// <\summary>
public float latitude;
/// <summary>
///经度
/// <\summary>
public float longitude;
public UserValiadateInforWarp(){}

public UserValiadateInforWarp(UserValiadateInfor _value, float _hasTime, string _agentCode, string _backControlCode, Int32 _Sex, Int32 _Gold, Int32 _RechargeCount, Int32 _rechargeBank, Int32 _goldBank, string _bankPassword, Int32 _signTimes, Int64 _signDate, string _PikeName, string _headUrl, byte _isWeiChat, string _serverId, Int64 _loginDate, Int64 _registDate, Int32 _EntryGameGold, Int32 _EntryGameRechargeCount, List<string> _groupList, Int32 _winMinControlScale, Int32 _winMaxControlScale, Int32 _winMaxScale, Int32 _winMinScale, Int32 _curControlScale, Int32 _curScale, Int32 _recordScore, Int32 _scoreChange, string _realName, string _realId, string _realPhone, string _ipaddr, string _port, float _latitude, float _longitude){
this.value = _value;
this.hasTime = _hasTime;
this.agentCode = _agentCode;
this.backControlCode = _backControlCode;
this.Sex = _Sex;
this.Gold = _Gold;
this.RechargeCount = _RechargeCount;
this.rechargeBank = _rechargeBank;
this.goldBank = _goldBank;
this.bankPassword = _bankPassword;
this.signTimes = _signTimes;
this.signDate = _signDate;
this.PikeName = _PikeName;
this.headUrl = _headUrl;
this.isWeiChat = _isWeiChat;
this.serverId = _serverId;
this.loginDate = _loginDate;
this.registDate = _registDate;
this.EntryGameGold = _EntryGameGold;
this.EntryGameRechargeCount = _EntryGameRechargeCount;
this.groupList = _groupList;
this.winMinControlScale = _winMinControlScale;
this.winMaxControlScale = _winMaxControlScale;
this.winMaxScale = _winMaxScale;
this.winMinScale = _winMinScale;
this.curControlScale = _curControlScale;
this.curScale = _curScale;
this.recordScore = _recordScore;
this.scoreChange = _scoreChange;
this.realName = _realName;
this.realId = _realId;
this.realPhone = _realPhone;
this.ipaddr = _ipaddr;
this.port = _port;
this.latitude = _latitude;
this.longitude = _longitude;
}
private byte[] get_value_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)value).Serializer();
return outBuf;
}


private byte[] get_hasTime_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((float)hasTime);
return outBuf;
}


private byte[] get_agentCode_encoding(){
byte[] outBuf = null;
string str = (string)agentCode;
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


private byte[] get_backControlCode_encoding(){
byte[] outBuf = null;
string str = (string)backControlCode;
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


private byte[] get_Sex_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Sex);
return outBuf;
}


private byte[] get_Gold_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Gold);
return outBuf;
}


private byte[] get_RechargeCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)RechargeCount);
return outBuf;
}


private byte[] get_rechargeBank_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeBank);
return outBuf;
}


private byte[] get_goldBank_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)goldBank);
return outBuf;
}


private byte[] get_bankPassword_encoding(){
byte[] outBuf = null;
string str = (string)bankPassword;
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


private byte[] get_signTimes_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)signTimes);
return outBuf;
}


private byte[] get_signDate_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)signDate);
return outBuf;
}


private byte[] get_PikeName_encoding(){
byte[] outBuf = null;
string str = (string)PikeName;
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


private byte[] get_headUrl_encoding(){
byte[] outBuf = null;
string str = (string)headUrl;
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


private byte[] get_isWeiChat_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)isWeiChat;
return outBuf;
}


private byte[] get_serverId_encoding(){
byte[] outBuf = null;
string str = (string)serverId;
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


private byte[] get_loginDate_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)loginDate);
return outBuf;
}


private byte[] get_registDate_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)registDate);
return outBuf;
}


private byte[] get_EntryGameGold_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)EntryGameGold);
return outBuf;
}


private byte[] get_EntryGameRechargeCount_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)EntryGameRechargeCount);
return outBuf;
}


private byte[] get_groupList_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<string> listString = (List<string>)groupList;
memoryWrite.Write(BitConverter.GetBytes(listString.Count),0,4);
for(int i = 0;i < listString.Count;++i){
using(MemoryStream desStream = new MemoryStream()){
string str = listString[i];
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
byte[] strBytes = desStream.ToArray();
memoryWrite.Write(strBytes,0,strBytes.Length);
}
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private byte[] get_winMinControlScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)winMinControlScale);
return outBuf;
}


private byte[] get_winMaxControlScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)winMaxControlScale);
return outBuf;
}


private byte[] get_winMaxScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)winMaxScale);
return outBuf;
}


private byte[] get_winMinScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)winMinScale);
return outBuf;
}


private byte[] get_curControlScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)curControlScale);
return outBuf;
}


private byte[] get_curScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)curScale);
return outBuf;
}


private byte[] get_recordScore_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)recordScore);
return outBuf;
}


private byte[] get_scoreChange_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)scoreChange);
return outBuf;
}


private byte[] get_realName_encoding(){
byte[] outBuf = null;
string str = (string)realName;
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


private byte[] get_realId_encoding(){
byte[] outBuf = null;
string str = (string)realId;
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


private byte[] get_realPhone_encoding(){
byte[] outBuf = null;
string str = (string)realPhone;
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


private byte[] get_ipaddr_encoding(){
byte[] outBuf = null;
string str = (string)ipaddr;
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
string str = (string)port;
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


private byte[] get_latitude_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((float)latitude);
return outBuf;
}


private byte[] get_longitude_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((float)longitude);
return outBuf;
}

private int set_value_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
value = new UserValiadateInfor();
curIndex = value.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_hasTime_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hasTime = new float();
hasTime = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_agentCode_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
agentCode = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
agentCode = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_backControlCode_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
backControlCode = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
backControlCode = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_Sex_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Sex = new Int32();
Sex = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_Gold_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Gold = new Int32();
Gold = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_RechargeCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
RechargeCount = new Int32();
RechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_rechargeBank_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
rechargeBank = new Int32();
rechargeBank = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_goldBank_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
goldBank = new Int32();
goldBank = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_bankPassword_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
bankPassword = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
bankPassword = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_signTimes_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
signTimes = new Int32();
signTimes = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_signDate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
signDate = new Int64();
signDate = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_PikeName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
PikeName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
PikeName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_headUrl_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
headUrl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
headUrl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_isWeiChat_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isWeiChat = new byte();
isWeiChat = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_serverId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
serverId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
serverId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_loginDate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
loginDate = new Int64();
loginDate = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_registDate_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
registDate = new Int64();
registDate = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_EntryGameGold_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
EntryGameGold = new Int32();
EntryGameGold = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_EntryGameRechargeCount_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
EntryGameRechargeCount = new Int32();
EntryGameRechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_groupList_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupList = new List<string>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
string curTarget = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
curTarget = System.Text.Encoding.UTF8.GetString(byteArray);
groupList.Add(curTarget);
}
}return curIndex;
}
private int set_winMinControlScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
winMinControlScale = new Int32();
winMinControlScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_winMaxControlScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
winMaxControlScale = new Int32();
winMaxControlScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_winMaxScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
winMaxScale = new Int32();
winMaxScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_winMinScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
winMinScale = new Int32();
winMinScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_curControlScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curControlScale = new Int32();
curControlScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_curScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
curScale = new Int32();
curScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_recordScore_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
recordScore = new Int32();
recordScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_scoreChange_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
scoreChange = new Int32();
scoreChange = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_realName_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
realName = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
realName = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_realId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
realId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
realId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_realPhone_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
realPhone = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
realPhone = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_ipaddr_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
ipaddr = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
ipaddr = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_port_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
port = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
port = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_latitude_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
latitude = new float();
latitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_longitude_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
longitude = new float();
longitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(value !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_value_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hasTime !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hasTime_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(agentCode !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_agentCode_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(backControlCode !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_backControlCode_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(Gold !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_Gold_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(RechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_RechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(rechargeBank !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_rechargeBank_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(goldBank !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_goldBank_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(bankPassword !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_bankPassword_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(signTimes !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_signTimes_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(signDate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_signDate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(PikeName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_PikeName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(isWeiChat !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_isWeiChat_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(serverId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_serverId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(loginDate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_loginDate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(registDate !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_registDate_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(EntryGameGold !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_EntryGameGold_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(EntryGameRechargeCount !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_EntryGameRechargeCount_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(groupList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_groupList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(winMinControlScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_winMinControlScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(winMaxControlScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_winMaxControlScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(winMaxScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_winMaxScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(winMinScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_winMinScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(curControlScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_curControlScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(curScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_curScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(recordScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_recordScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(scoreChange !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_scoreChange_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(realName !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_realName_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(realId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_realId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(realPhone !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_realPhone_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(ipaddr !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_ipaddr_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(port !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_port_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(latitude !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_latitude_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(longitude !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_longitude_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_value_fromBuf(sourceBuf,startOffset);
startOffset = set_hasTime_fromBuf(sourceBuf,startOffset);
startOffset = set_agentCode_fromBuf(sourceBuf,startOffset);
startOffset = set_backControlCode_fromBuf(sourceBuf,startOffset);
startOffset = set_Sex_fromBuf(sourceBuf,startOffset);
startOffset = set_Gold_fromBuf(sourceBuf,startOffset);
startOffset = set_RechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_rechargeBank_fromBuf(sourceBuf,startOffset);
startOffset = set_goldBank_fromBuf(sourceBuf,startOffset);
startOffset = set_bankPassword_fromBuf(sourceBuf,startOffset);
startOffset = set_signTimes_fromBuf(sourceBuf,startOffset);
startOffset = set_signDate_fromBuf(sourceBuf,startOffset);
startOffset = set_PikeName_fromBuf(sourceBuf,startOffset);
startOffset = set_headUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_isWeiChat_fromBuf(sourceBuf,startOffset);
startOffset = set_serverId_fromBuf(sourceBuf,startOffset);
startOffset = set_loginDate_fromBuf(sourceBuf,startOffset);
startOffset = set_registDate_fromBuf(sourceBuf,startOffset);
startOffset = set_EntryGameGold_fromBuf(sourceBuf,startOffset);
startOffset = set_EntryGameRechargeCount_fromBuf(sourceBuf,startOffset);
startOffset = set_groupList_fromBuf(sourceBuf,startOffset);
startOffset = set_winMinControlScale_fromBuf(sourceBuf,startOffset);
startOffset = set_winMaxControlScale_fromBuf(sourceBuf,startOffset);
startOffset = set_winMaxScale_fromBuf(sourceBuf,startOffset);
startOffset = set_winMinScale_fromBuf(sourceBuf,startOffset);
startOffset = set_curControlScale_fromBuf(sourceBuf,startOffset);
startOffset = set_curScale_fromBuf(sourceBuf,startOffset);
startOffset = set_recordScore_fromBuf(sourceBuf,startOffset);
startOffset = set_scoreChange_fromBuf(sourceBuf,startOffset);
startOffset = set_realName_fromBuf(sourceBuf,startOffset);
startOffset = set_realId_fromBuf(sourceBuf,startOffset);
startOffset = set_realPhone_fromBuf(sourceBuf,startOffset);
startOffset = set_ipaddr_fromBuf(sourceBuf,startOffset);
startOffset = set_port_fromBuf(sourceBuf,startOffset);
startOffset = set_latitude_fromBuf(sourceBuf,startOffset);
startOffset = set_longitude_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_value_json(){
if(value==null){return "";}string resultJson = "\"value\":";resultJson += ((CherishBitProtocolBase)value).SerializerJson();return resultJson;
}


public string get_hasTime_json(){
if(hasTime==null){return "";}string resultJson = "\"hasTime\":";resultJson += "\"";resultJson += hasTime.ToString();resultJson += "\"";return resultJson;
}


public string get_agentCode_json(){
if(agentCode==null){return "";}string resultJson = "\"agentCode\":";resultJson += "\"";resultJson += agentCode.ToString();resultJson += "\"";return resultJson;
}


public string get_backControlCode_json(){
if(backControlCode==null){return "";}string resultJson = "\"backControlCode\":";resultJson += "\"";resultJson += backControlCode.ToString();resultJson += "\"";return resultJson;
}


public string get_Sex_json(){
if(Sex==null){return "";}string resultJson = "\"Sex\":";resultJson += "\"";resultJson += Sex.ToString();resultJson += "\"";return resultJson;
}


public string get_Gold_json(){
if(Gold==null){return "";}string resultJson = "\"Gold\":";resultJson += "\"";resultJson += Gold.ToString();resultJson += "\"";return resultJson;
}


public string get_RechargeCount_json(){
if(RechargeCount==null){return "";}string resultJson = "\"RechargeCount\":";resultJson += "\"";resultJson += RechargeCount.ToString();resultJson += "\"";return resultJson;
}


public string get_rechargeBank_json(){
if(rechargeBank==null){return "";}string resultJson = "\"rechargeBank\":";resultJson += "\"";resultJson += rechargeBank.ToString();resultJson += "\"";return resultJson;
}


public string get_goldBank_json(){
if(goldBank==null){return "";}string resultJson = "\"goldBank\":";resultJson += "\"";resultJson += goldBank.ToString();resultJson += "\"";return resultJson;
}


public string get_bankPassword_json(){
if(bankPassword==null){return "";}string resultJson = "\"bankPassword\":";resultJson += "\"";resultJson += bankPassword.ToString();resultJson += "\"";return resultJson;
}


public string get_signTimes_json(){
if(signTimes==null){return "";}string resultJson = "\"signTimes\":";resultJson += "\"";resultJson += signTimes.ToString();resultJson += "\"";return resultJson;
}


public string get_signDate_json(){
if(signDate==null){return "";}string resultJson = "\"signDate\":";resultJson += "\"";resultJson += signDate.ToString();resultJson += "\"";return resultJson;
}


public string get_PikeName_json(){
if(PikeName==null){return "";}string resultJson = "\"PikeName\":";resultJson += "\"";resultJson += PikeName.ToString();resultJson += "\"";return resultJson;
}


public string get_headUrl_json(){
if(headUrl==null){return "";}string resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public string get_isWeiChat_json(){
if(isWeiChat==null){return "";}string resultJson = "\"isWeiChat\":";resultJson += "\"";resultJson += isWeiChat.ToString();resultJson += "\"";return resultJson;
}


public string get_serverId_json(){
if(serverId==null){return "";}string resultJson = "\"serverId\":";resultJson += "\"";resultJson += serverId.ToString();resultJson += "\"";return resultJson;
}


public string get_loginDate_json(){
if(loginDate==null){return "";}string resultJson = "\"loginDate\":";resultJson += "\"";resultJson += loginDate.ToString();resultJson += "\"";return resultJson;
}


public string get_registDate_json(){
if(registDate==null){return "";}string resultJson = "\"registDate\":";resultJson += "\"";resultJson += registDate.ToString();resultJson += "\"";return resultJson;
}


public string get_EntryGameGold_json(){
if(EntryGameGold==null){return "";}string resultJson = "\"EntryGameGold\":";resultJson += "\"";resultJson += EntryGameGold.ToString();resultJson += "\"";return resultJson;
}


public string get_EntryGameRechargeCount_json(){
if(EntryGameRechargeCount==null){return "";}string resultJson = "\"EntryGameRechargeCount\":";resultJson += "\"";resultJson += EntryGameRechargeCount.ToString();resultJson += "\"";return resultJson;
}


public string get_groupList_json(){
if(groupList==null){return "";}string resultJson = "\"groupList\":";resultJson += "[";List<string> listObj = (List<string>)groupList;
for(int i = 0;i < listObj.Count;++i){
string item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item;
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public string get_winMinControlScale_json(){
if(winMinControlScale==null){return "";}string resultJson = "\"winMinControlScale\":";resultJson += "\"";resultJson += winMinControlScale.ToString();resultJson += "\"";return resultJson;
}


public string get_winMaxControlScale_json(){
if(winMaxControlScale==null){return "";}string resultJson = "\"winMaxControlScale\":";resultJson += "\"";resultJson += winMaxControlScale.ToString();resultJson += "\"";return resultJson;
}


public string get_winMaxScale_json(){
if(winMaxScale==null){return "";}string resultJson = "\"winMaxScale\":";resultJson += "\"";resultJson += winMaxScale.ToString();resultJson += "\"";return resultJson;
}


public string get_winMinScale_json(){
if(winMinScale==null){return "";}string resultJson = "\"winMinScale\":";resultJson += "\"";resultJson += winMinScale.ToString();resultJson += "\"";return resultJson;
}


public string get_curControlScale_json(){
if(curControlScale==null){return "";}string resultJson = "\"curControlScale\":";resultJson += "\"";resultJson += curControlScale.ToString();resultJson += "\"";return resultJson;
}


public string get_curScale_json(){
if(curScale==null){return "";}string resultJson = "\"curScale\":";resultJson += "\"";resultJson += curScale.ToString();resultJson += "\"";return resultJson;
}


public string get_recordScore_json(){
if(recordScore==null){return "";}string resultJson = "\"recordScore\":";resultJson += "\"";resultJson += recordScore.ToString();resultJson += "\"";return resultJson;
}


public string get_scoreChange_json(){
if(scoreChange==null){return "";}string resultJson = "\"scoreChange\":";resultJson += "\"";resultJson += scoreChange.ToString();resultJson += "\"";return resultJson;
}


public string get_realName_json(){
if(realName==null){return "";}string resultJson = "\"realName\":";resultJson += "\"";resultJson += realName.ToString();resultJson += "\"";return resultJson;
}


public string get_realId_json(){
if(realId==null){return "";}string resultJson = "\"realId\":";resultJson += "\"";resultJson += realId.ToString();resultJson += "\"";return resultJson;
}


public string get_realPhone_json(){
if(realPhone==null){return "";}string resultJson = "\"realPhone\":";resultJson += "\"";resultJson += realPhone.ToString();resultJson += "\"";return resultJson;
}


public string get_ipaddr_json(){
if(ipaddr==null){return "";}string resultJson = "\"ipaddr\":";resultJson += "\"";resultJson += ipaddr.ToString();resultJson += "\"";return resultJson;
}


public string get_port_json(){
if(port==null){return "";}string resultJson = "\"port\":";resultJson += "\"";resultJson += port.ToString();resultJson += "\"";return resultJson;
}


public string get_latitude_json(){
if(latitude==null){return "";}string resultJson = "\"latitude\":";resultJson += "\"";resultJson += latitude.ToString();resultJson += "\"";return resultJson;
}


public string get_longitude_json(){
if(longitude==null){return "";}string resultJson = "\"longitude\":";resultJson += "\"";resultJson += longitude.ToString();resultJson += "\"";return resultJson;
}


public void set_value_fromJson(LitJson.JsonData jsonObj){
value= new UserValiadateInfor();
value.DeserializerJson(jsonObj.ToJson());}


public void set_hasTime_fromJson(LitJson.JsonData jsonObj){
hasTime= float.Parse(jsonObj.ToString());
}


public void set_agentCode_fromJson(LitJson.JsonData jsonObj){
agentCode= jsonObj.ToString();
}


public void set_backControlCode_fromJson(LitJson.JsonData jsonObj){
backControlCode= jsonObj.ToString();
}


public void set_Sex_fromJson(LitJson.JsonData jsonObj){
Sex= Int32.Parse(jsonObj.ToString());
}


public void set_Gold_fromJson(LitJson.JsonData jsonObj){
Gold= Int32.Parse(jsonObj.ToString());
}


public void set_RechargeCount_fromJson(LitJson.JsonData jsonObj){
RechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_rechargeBank_fromJson(LitJson.JsonData jsonObj){
rechargeBank= Int32.Parse(jsonObj.ToString());
}


public void set_goldBank_fromJson(LitJson.JsonData jsonObj){
goldBank= Int32.Parse(jsonObj.ToString());
}


public void set_bankPassword_fromJson(LitJson.JsonData jsonObj){
bankPassword= jsonObj.ToString();
}


public void set_signTimes_fromJson(LitJson.JsonData jsonObj){
signTimes= Int32.Parse(jsonObj.ToString());
}


public void set_signDate_fromJson(LitJson.JsonData jsonObj){
signDate= Int64.Parse(jsonObj.ToString());
}


public void set_PikeName_fromJson(LitJson.JsonData jsonObj){
PikeName= jsonObj.ToString();
}


public void set_headUrl_fromJson(LitJson.JsonData jsonObj){
headUrl= jsonObj.ToString();
}


public void set_isWeiChat_fromJson(LitJson.JsonData jsonObj){
isWeiChat= byte.Parse(jsonObj.ToString());
}


public void set_serverId_fromJson(LitJson.JsonData jsonObj){
serverId= jsonObj.ToString();
}


public void set_loginDate_fromJson(LitJson.JsonData jsonObj){
loginDate= Int64.Parse(jsonObj.ToString());
}


public void set_registDate_fromJson(LitJson.JsonData jsonObj){
registDate= Int64.Parse(jsonObj.ToString());
}


public void set_EntryGameGold_fromJson(LitJson.JsonData jsonObj){
EntryGameGold= Int32.Parse(jsonObj.ToString());
}


public void set_EntryGameRechargeCount_fromJson(LitJson.JsonData jsonObj){
EntryGameRechargeCount= Int32.Parse(jsonObj.ToString());
}


public void set_groupList_fromJson(LitJson.JsonData jsonObj){
groupList= new List<string>();
foreach(LitJson.JsonData jsonItem in jsonObj){
groupList.Add(jsonItem.ToString());}

}


public void set_winMinControlScale_fromJson(LitJson.JsonData jsonObj){
winMinControlScale= Int32.Parse(jsonObj.ToString());
}


public void set_winMaxControlScale_fromJson(LitJson.JsonData jsonObj){
winMaxControlScale= Int32.Parse(jsonObj.ToString());
}


public void set_winMaxScale_fromJson(LitJson.JsonData jsonObj){
winMaxScale= Int32.Parse(jsonObj.ToString());
}


public void set_winMinScale_fromJson(LitJson.JsonData jsonObj){
winMinScale= Int32.Parse(jsonObj.ToString());
}


public void set_curControlScale_fromJson(LitJson.JsonData jsonObj){
curControlScale= Int32.Parse(jsonObj.ToString());
}


public void set_curScale_fromJson(LitJson.JsonData jsonObj){
curScale= Int32.Parse(jsonObj.ToString());
}


public void set_recordScore_fromJson(LitJson.JsonData jsonObj){
recordScore= Int32.Parse(jsonObj.ToString());
}


public void set_scoreChange_fromJson(LitJson.JsonData jsonObj){
scoreChange= Int32.Parse(jsonObj.ToString());
}


public void set_realName_fromJson(LitJson.JsonData jsonObj){
realName= jsonObj.ToString();
}


public void set_realId_fromJson(LitJson.JsonData jsonObj){
realId= jsonObj.ToString();
}


public void set_realPhone_fromJson(LitJson.JsonData jsonObj){
realPhone= jsonObj.ToString();
}


public void set_ipaddr_fromJson(LitJson.JsonData jsonObj){
ipaddr= jsonObj.ToString();
}


public void set_port_fromJson(LitJson.JsonData jsonObj){
port= jsonObj.ToString();
}


public void set_latitude_fromJson(LitJson.JsonData jsonObj){
latitude= float.Parse(jsonObj.ToString());
}


public void set_longitude_fromJson(LitJson.JsonData jsonObj){
longitude= float.Parse(jsonObj.ToString());
}

public override string SerializerJson(){
string resultStr = "{";if(value !=  null){
resultStr += get_value_json();
}
else {}if(hasTime !=  null){
resultStr += ",";resultStr += get_hasTime_json();
}
else {}if(agentCode !=  null){
resultStr += ",";resultStr += get_agentCode_json();
}
else {}if(backControlCode !=  null){
resultStr += ",";resultStr += get_backControlCode_json();
}
else {}if(Sex !=  null){
resultStr += ",";resultStr += get_Sex_json();
}
else {}if(Gold !=  null){
resultStr += ",";resultStr += get_Gold_json();
}
else {}if(RechargeCount !=  null){
resultStr += ",";resultStr += get_RechargeCount_json();
}
else {}if(rechargeBank !=  null){
resultStr += ",";resultStr += get_rechargeBank_json();
}
else {}if(goldBank !=  null){
resultStr += ",";resultStr += get_goldBank_json();
}
else {}if(bankPassword !=  null){
resultStr += ",";resultStr += get_bankPassword_json();
}
else {}if(signTimes !=  null){
resultStr += ",";resultStr += get_signTimes_json();
}
else {}if(signDate !=  null){
resultStr += ",";resultStr += get_signDate_json();
}
else {}if(PikeName !=  null){
resultStr += ",";resultStr += get_PikeName_json();
}
else {}if(headUrl !=  null){
resultStr += ",";resultStr += get_headUrl_json();
}
else {}if(isWeiChat !=  null){
resultStr += ",";resultStr += get_isWeiChat_json();
}
else {}if(serverId !=  null){
resultStr += ",";resultStr += get_serverId_json();
}
else {}if(loginDate !=  null){
resultStr += ",";resultStr += get_loginDate_json();
}
else {}if(registDate !=  null){
resultStr += ",";resultStr += get_registDate_json();
}
else {}if(EntryGameGold !=  null){
resultStr += ",";resultStr += get_EntryGameGold_json();
}
else {}if(EntryGameRechargeCount !=  null){
resultStr += ",";resultStr += get_EntryGameRechargeCount_json();
}
else {}if(groupList !=  null){
resultStr += ",";resultStr += get_groupList_json();
}
else {}if(winMinControlScale !=  null){
resultStr += ",";resultStr += get_winMinControlScale_json();
}
else {}if(winMaxControlScale !=  null){
resultStr += ",";resultStr += get_winMaxControlScale_json();
}
else {}if(winMaxScale !=  null){
resultStr += ",";resultStr += get_winMaxScale_json();
}
else {}if(winMinScale !=  null){
resultStr += ",";resultStr += get_winMinScale_json();
}
else {}if(curControlScale !=  null){
resultStr += ",";resultStr += get_curControlScale_json();
}
else {}if(curScale !=  null){
resultStr += ",";resultStr += get_curScale_json();
}
else {}if(recordScore !=  null){
resultStr += ",";resultStr += get_recordScore_json();
}
else {}if(scoreChange !=  null){
resultStr += ",";resultStr += get_scoreChange_json();
}
else {}if(realName !=  null){
resultStr += ",";resultStr += get_realName_json();
}
else {}if(realId !=  null){
resultStr += ",";resultStr += get_realId_json();
}
else {}if(realPhone !=  null){
resultStr += ",";resultStr += get_realPhone_json();
}
else {}if(ipaddr !=  null){
resultStr += ",";resultStr += get_ipaddr_json();
}
else {}if(port !=  null){
resultStr += ",";resultStr += get_port_json();
}
else {}if(latitude !=  null){
resultStr += ",";resultStr += get_latitude_json();
}
else {}if(longitude !=  null){
resultStr += ",";resultStr += get_longitude_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["value"] != null){
set_value_fromJson(jsonObj["value"]);
}
if(jsonObj["hasTime"] != null){
set_hasTime_fromJson(jsonObj["hasTime"]);
}
if(jsonObj["agentCode"] != null){
set_agentCode_fromJson(jsonObj["agentCode"]);
}
if(jsonObj["backControlCode"] != null){
set_backControlCode_fromJson(jsonObj["backControlCode"]);
}
if(jsonObj["Sex"] != null){
set_Sex_fromJson(jsonObj["Sex"]);
}
if(jsonObj["Gold"] != null){
set_Gold_fromJson(jsonObj["Gold"]);
}
if(jsonObj["RechargeCount"] != null){
set_RechargeCount_fromJson(jsonObj["RechargeCount"]);
}
if(jsonObj["rechargeBank"] != null){
set_rechargeBank_fromJson(jsonObj["rechargeBank"]);
}
if(jsonObj["goldBank"] != null){
set_goldBank_fromJson(jsonObj["goldBank"]);
}
if(jsonObj["bankPassword"] != null){
set_bankPassword_fromJson(jsonObj["bankPassword"]);
}
if(jsonObj["signTimes"] != null){
set_signTimes_fromJson(jsonObj["signTimes"]);
}
if(jsonObj["signDate"] != null){
set_signDate_fromJson(jsonObj["signDate"]);
}
if(jsonObj["PikeName"] != null){
set_PikeName_fromJson(jsonObj["PikeName"]);
}
if(jsonObj["headUrl"] != null){
set_headUrl_fromJson(jsonObj["headUrl"]);
}
if(jsonObj["isWeiChat"] != null){
set_isWeiChat_fromJson(jsonObj["isWeiChat"]);
}
if(jsonObj["serverId"] != null){
set_serverId_fromJson(jsonObj["serverId"]);
}
if(jsonObj["loginDate"] != null){
set_loginDate_fromJson(jsonObj["loginDate"]);
}
if(jsonObj["registDate"] != null){
set_registDate_fromJson(jsonObj["registDate"]);
}
if(jsonObj["EntryGameGold"] != null){
set_EntryGameGold_fromJson(jsonObj["EntryGameGold"]);
}
if(jsonObj["EntryGameRechargeCount"] != null){
set_EntryGameRechargeCount_fromJson(jsonObj["EntryGameRechargeCount"]);
}
if(jsonObj["groupList"] != null){
set_groupList_fromJson(jsonObj["groupList"]);
}
if(jsonObj["winMinControlScale"] != null){
set_winMinControlScale_fromJson(jsonObj["winMinControlScale"]);
}
if(jsonObj["winMaxControlScale"] != null){
set_winMaxControlScale_fromJson(jsonObj["winMaxControlScale"]);
}
if(jsonObj["winMaxScale"] != null){
set_winMaxScale_fromJson(jsonObj["winMaxScale"]);
}
if(jsonObj["winMinScale"] != null){
set_winMinScale_fromJson(jsonObj["winMinScale"]);
}
if(jsonObj["curControlScale"] != null){
set_curControlScale_fromJson(jsonObj["curControlScale"]);
}
if(jsonObj["curScale"] != null){
set_curScale_fromJson(jsonObj["curScale"]);
}
if(jsonObj["recordScore"] != null){
set_recordScore_fromJson(jsonObj["recordScore"]);
}
if(jsonObj["scoreChange"] != null){
set_scoreChange_fromJson(jsonObj["scoreChange"]);
}
if(jsonObj["realName"] != null){
set_realName_fromJson(jsonObj["realName"]);
}
if(jsonObj["realId"] != null){
set_realId_fromJson(jsonObj["realId"]);
}
if(jsonObj["realPhone"] != null){
set_realPhone_fromJson(jsonObj["realPhone"]);
}
if(jsonObj["ipaddr"] != null){
set_ipaddr_fromJson(jsonObj["ipaddr"]);
}
if(jsonObj["port"] != null){
set_port_fromJson(jsonObj["port"]);
}
if(jsonObj["latitude"] != null){
set_latitude_fromJson(jsonObj["latitude"]);
}
if(jsonObj["longitude"] != null){
set_longitude_fromJson(jsonObj["longitude"]);
}
}
}
}
