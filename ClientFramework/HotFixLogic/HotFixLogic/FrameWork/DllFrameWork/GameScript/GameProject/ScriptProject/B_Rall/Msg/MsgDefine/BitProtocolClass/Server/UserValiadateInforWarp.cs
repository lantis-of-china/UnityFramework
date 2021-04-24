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
public class UserValiadateInforWarp : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public UserValiadateInfor value;
/// <summary>
///
/// <\summary>
public Single hasTime;
/// <summary>
///代理码
/// <\summary>
public String agentCode;
/// <summary>
///后台管理码
/// <\summary>
public String backControlCode;
/// <summary>
///性别
/// <\summary>
public Int32 Sex;
/// <summary>
///金币
/// <\summary>
public Int32 Gold;
/// <summary>
///钻石
/// <\summary>
public Int32 RechargeCount;
/// <summary>
///银行钻石
/// <\summary>
public Int32 rechargeBank;
/// <summary>
///银行金币
/// <\summary>
public Int32 goldBank;
/// <summary>
///银行密码
/// <\summary>
public String bankPassword;
/// <summary>
///签到次数
/// <\summary>
public Int32 signTimes;
/// <summary>
///最近签到日期
/// <\summary>
public Int64 signDate;
/// <summary>
///名字
/// <\summary>
public String PikeName;
/// <summary>
///头像连接
/// <\summary>
public String headUrl;
/// <summary>
///是微信登陆
/// <\summary>
public Byte isWeiChat;
/// <summary>
///在哪个游戏中
/// <\summary>
public String serverId;
/// <summary>
///登录日期
/// <\summary>
public Int64 loginDate;
/// <summary>
///注册日期
/// <\summary>
public Int64 registDate;
/// <summary>
///游戏带入金币
/// <\summary>
public Int32 EntryGameGold;
/// <summary>
///游戏带入钻石
/// <\summary>
public Int32 EntryGameRechargeCount;
/// <summary>
///加入的组群队列
/// <\summary>
public List<String> groupList;
/// <summary>
///
/// <\summary>
public Int32 luckRecordScore;
/// <summary>
///点控值
/// <\summary>
public Int32 luckScore;
/// <summary>
///实名认证
/// <\summary>
public String realName;
/// <summary>
///实名ID
/// <\summary>
public String realId;
/// <summary>
///手机号
/// <\summary>
public String realPhone;
/// <summary>
///UDP IP地址
/// <\summary>
public String ipaddr;
/// <summary>
///UDP 接收端口
/// <\summary>
public String port;
/// <summary>
///纬度
/// <\summary>
public Single latitude;
/// <summary>
///经度
/// <\summary>
public Single longitude;
public UserValiadateInforWarp(){}

public UserValiadateInforWarp(UserValiadateInfor _value, Single _hasTime, String _agentCode, String _backControlCode, Int32 _Sex, Int32 _Gold, Int32 _RechargeCount, Int32 _rechargeBank, Int32 _goldBank, String _bankPassword, Int32 _signTimes, Int64 _signDate, String _PikeName, String _headUrl, Byte _isWeiChat, String _serverId, Int64 _loginDate, Int64 _registDate, Int32 _EntryGameGold, Int32 _EntryGameRechargeCount, List<String> _groupList, Int32 _luckRecordScore, Int32 _luckScore, String _realName, String _realId, String _realPhone, String _ipaddr, String _port, Single _latitude, Single _longitude){
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
this.luckRecordScore = _luckRecordScore;
this.luckScore = _luckScore;
this.realName = _realName;
this.realId = _realId;
this.realPhone = _realPhone;
this.ipaddr = _ipaddr;
this.port = _port;
this.latitude = _latitude;
this.longitude = _longitude;
}
private Byte[] get_value_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)value).Serializer();
return outBuf;
}


private Byte[] get_hasTime_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)hasTime);
return outBuf;
}


private Byte[] get_agentCode_encoding(){
Byte[] outBuf = null;
String str = (String)agentCode;
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


private Byte[] get_backControlCode_encoding(){
Byte[] outBuf = null;
String str = (String)backControlCode;
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


private Byte[] get_Sex_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Sex);
return outBuf;
}


private Byte[] get_Gold_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)Gold);
return outBuf;
}


private Byte[] get_RechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)RechargeCount);
return outBuf;
}


private Byte[] get_rechargeBank_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)rechargeBank);
return outBuf;
}


private Byte[] get_goldBank_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)goldBank);
return outBuf;
}


private Byte[] get_bankPassword_encoding(){
Byte[] outBuf = null;
String str = (String)bankPassword;
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


private Byte[] get_signTimes_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)signTimes);
return outBuf;
}


private Byte[] get_signDate_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)signDate);
return outBuf;
}


private Byte[] get_PikeName_encoding(){
Byte[] outBuf = null;
String str = (String)PikeName;
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


private Byte[] get_headUrl_encoding(){
Byte[] outBuf = null;
String str = (String)headUrl;
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


private Byte[] get_isWeiChat_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)isWeiChat;
return outBuf;
}


private Byte[] get_serverId_encoding(){
Byte[] outBuf = null;
String str = (String)serverId;
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


private Byte[] get_loginDate_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)loginDate);
return outBuf;
}


private Byte[] get_registDate_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)registDate);
return outBuf;
}


private Byte[] get_EntryGameGold_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)EntryGameGold);
return outBuf;
}


private Byte[] get_EntryGameRechargeCount_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)EntryGameRechargeCount);
return outBuf;
}


private Byte[] get_groupList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<String> listString = (List<String>)groupList;
memoryWrite.Write(BitConverter.GetBytes(listString.Count),0,4);
for(int i = 0;i < listString.Count;++i){
using(MemoryStream desStream = new MemoryStream()){
String str = listString[i];
Char[] charArray = str.ToCharArray();
Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
Byte[] bufLenght = BitConverter.GetBytes(length);
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
Byte[] strBytes = desStream.ToArray();
memoryWrite.Write(strBytes,0,strBytes.Length);
}
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_luckRecordScore_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)luckRecordScore);
return outBuf;
}


private Byte[] get_luckScore_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)luckScore);
return outBuf;
}


private Byte[] get_realName_encoding(){
Byte[] outBuf = null;
String str = (String)realName;
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


private Byte[] get_realId_encoding(){
Byte[] outBuf = null;
String str = (String)realId;
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


private Byte[] get_realPhone_encoding(){
Byte[] outBuf = null;
String str = (String)realPhone;
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


private Byte[] get_ipaddr_encoding(){
Byte[] outBuf = null;
String str = (String)ipaddr;
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
String str = (String)port;
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


private Byte[] get_latitude_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)latitude);
return outBuf;
}


private Byte[] get_longitude_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)longitude);
return outBuf;
}

private int set_value_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
value = new UserValiadateInfor();
curIndex = value.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_hasTime_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hasTime = new Single();
hasTime = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_agentCode_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_backControlCode_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_Sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Sex = new Int32();
Sex = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_Gold_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
Gold = new Int32();
Gold = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_RechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
RechargeCount = new Int32();
RechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_rechargeBank_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
rechargeBank = new Int32();
rechargeBank = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_goldBank_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
goldBank = new Int32();
goldBank = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_bankPassword_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_signTimes_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
signTimes = new Int32();
signTimes = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_signDate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
signDate = new Int64();
signDate = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_PikeName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_headUrl_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_isWeiChat_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
isWeiChat = new Byte();
isWeiChat = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_serverId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_loginDate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
loginDate = new Int64();
loginDate = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_registDate_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
registDate = new Int64();
registDate = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
}return curIndex;
}
private int set_EntryGameGold_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
EntryGameGold = new Int32();
EntryGameGold = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_EntryGameRechargeCount_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
EntryGameRechargeCount = new Int32();
EntryGameRechargeCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_groupList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
groupList = new List<String>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
String curTarget = "";
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
private int set_luckRecordScore_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
luckRecordScore = new Int32();
luckRecordScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_luckScore_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
luckScore = new Int32();
luckScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_realName_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_realId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_realPhone_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_ipaddr_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_port_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
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
private int set_latitude_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
latitude = new Single();
latitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_longitude_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
longitude = new Single();
longitude = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
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
}if(luckRecordScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_luckRecordScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(luckScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_luckScore_encoding();
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
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
startOffset = set_luckRecordScore_fromBuf(sourceBuf,startOffset);
startOffset = set_luckScore_fromBuf(sourceBuf,startOffset);
startOffset = set_realName_fromBuf(sourceBuf,startOffset);
startOffset = set_realId_fromBuf(sourceBuf,startOffset);
startOffset = set_realPhone_fromBuf(sourceBuf,startOffset);
startOffset = set_ipaddr_fromBuf(sourceBuf,startOffset);
startOffset = set_port_fromBuf(sourceBuf,startOffset);
startOffset = set_latitude_fromBuf(sourceBuf,startOffset);
startOffset = set_longitude_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_value_json(){
if(value==null){return "";}String resultJson = "\"value\":";resultJson += ((LantisBitProtocolBase)value).SerializerJson();return resultJson;
}


public String get_hasTime_json(){
if(hasTime==null){return "";}String resultJson = "\"hasTime\":";resultJson += "\"";resultJson += hasTime.ToString();resultJson += "\"";return resultJson;
}


public String get_agentCode_json(){
if(agentCode==null){return "";}String resultJson = "\"agentCode\":";resultJson += "\"";resultJson += agentCode.ToString();resultJson += "\"";return resultJson;
}


public String get_backControlCode_json(){
if(backControlCode==null){return "";}String resultJson = "\"backControlCode\":";resultJson += "\"";resultJson += backControlCode.ToString();resultJson += "\"";return resultJson;
}


public String get_Sex_json(){
if(Sex==null){return "";}String resultJson = "\"Sex\":";resultJson += "\"";resultJson += Sex.ToString();resultJson += "\"";return resultJson;
}


public String get_Gold_json(){
if(Gold==null){return "";}String resultJson = "\"Gold\":";resultJson += "\"";resultJson += Gold.ToString();resultJson += "\"";return resultJson;
}


public String get_RechargeCount_json(){
if(RechargeCount==null){return "";}String resultJson = "\"RechargeCount\":";resultJson += "\"";resultJson += RechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_rechargeBank_json(){
if(rechargeBank==null){return "";}String resultJson = "\"rechargeBank\":";resultJson += "\"";resultJson += rechargeBank.ToString();resultJson += "\"";return resultJson;
}


public String get_goldBank_json(){
if(goldBank==null){return "";}String resultJson = "\"goldBank\":";resultJson += "\"";resultJson += goldBank.ToString();resultJson += "\"";return resultJson;
}


public String get_bankPassword_json(){
if(bankPassword==null){return "";}String resultJson = "\"bankPassword\":";resultJson += "\"";resultJson += bankPassword.ToString();resultJson += "\"";return resultJson;
}


public String get_signTimes_json(){
if(signTimes==null){return "";}String resultJson = "\"signTimes\":";resultJson += "\"";resultJson += signTimes.ToString();resultJson += "\"";return resultJson;
}


public String get_signDate_json(){
if(signDate==null){return "";}String resultJson = "\"signDate\":";resultJson += "\"";resultJson += signDate.ToString();resultJson += "\"";return resultJson;
}


public String get_PikeName_json(){
if(PikeName==null){return "";}String resultJson = "\"PikeName\":";resultJson += "\"";resultJson += PikeName.ToString();resultJson += "\"";return resultJson;
}


public String get_headUrl_json(){
if(headUrl==null){return "";}String resultJson = "\"headUrl\":";resultJson += "\"";resultJson += headUrl.ToString();resultJson += "\"";return resultJson;
}


public String get_isWeiChat_json(){
if(isWeiChat==null){return "";}String resultJson = "\"isWeiChat\":";resultJson += "\"";resultJson += isWeiChat.ToString();resultJson += "\"";return resultJson;
}


public String get_serverId_json(){
if(serverId==null){return "";}String resultJson = "\"serverId\":";resultJson += "\"";resultJson += serverId.ToString();resultJson += "\"";return resultJson;
}


public String get_loginDate_json(){
if(loginDate==null){return "";}String resultJson = "\"loginDate\":";resultJson += "\"";resultJson += loginDate.ToString();resultJson += "\"";return resultJson;
}


public String get_registDate_json(){
if(registDate==null){return "";}String resultJson = "\"registDate\":";resultJson += "\"";resultJson += registDate.ToString();resultJson += "\"";return resultJson;
}


public String get_EntryGameGold_json(){
if(EntryGameGold==null){return "";}String resultJson = "\"EntryGameGold\":";resultJson += "\"";resultJson += EntryGameGold.ToString();resultJson += "\"";return resultJson;
}


public String get_EntryGameRechargeCount_json(){
if(EntryGameRechargeCount==null){return "";}String resultJson = "\"EntryGameRechargeCount\":";resultJson += "\"";resultJson += EntryGameRechargeCount.ToString();resultJson += "\"";return resultJson;
}


public String get_groupList_json(){
if(groupList==null){return "";}String resultJson = "\"groupList\":";resultJson += "[";List<String> listObj = (List<String>)groupList;
for(int i = 0;i < listObj.Count;++i){
String item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item;
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public String get_luckRecordScore_json(){
if(luckRecordScore==null){return "";}String resultJson = "\"luckRecordScore\":";resultJson += "\"";resultJson += luckRecordScore.ToString();resultJson += "\"";return resultJson;
}


public String get_luckScore_json(){
if(luckScore==null){return "";}String resultJson = "\"luckScore\":";resultJson += "\"";resultJson += luckScore.ToString();resultJson += "\"";return resultJson;
}


public String get_realName_json(){
if(realName==null){return "";}String resultJson = "\"realName\":";resultJson += "\"";resultJson += realName.ToString();resultJson += "\"";return resultJson;
}


public String get_realId_json(){
if(realId==null){return "";}String resultJson = "\"realId\":";resultJson += "\"";resultJson += realId.ToString();resultJson += "\"";return resultJson;
}


public String get_realPhone_json(){
if(realPhone==null){return "";}String resultJson = "\"realPhone\":";resultJson += "\"";resultJson += realPhone.ToString();resultJson += "\"";return resultJson;
}


public String get_ipaddr_json(){
if(ipaddr==null){return "";}String resultJson = "\"ipaddr\":";resultJson += "\"";resultJson += ipaddr.ToString();resultJson += "\"";return resultJson;
}


public String get_port_json(){
if(port==null){return "";}String resultJson = "\"port\":";resultJson += "\"";resultJson += port.ToString();resultJson += "\"";return resultJson;
}


public String get_latitude_json(){
if(latitude==null){return "";}String resultJson = "\"latitude\":";resultJson += "\"";resultJson += latitude.ToString();resultJson += "\"";return resultJson;
}


public String get_longitude_json(){
if(longitude==null){return "";}String resultJson = "\"longitude\":";resultJson += "\"";resultJson += longitude.ToString();resultJson += "\"";return resultJson;
}


public void set_value_fromJson(LitJson.JsonData jsonObj){
value= new UserValiadateInfor();
value.DeserializerJson(jsonObj.ToJson());}


public void set_hasTime_fromJson(LitJson.JsonData jsonObj){
hasTime= Single.Parse(jsonObj.ToString());
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
isWeiChat= Byte.Parse(jsonObj.ToString());
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
groupList= new List<String>();
foreach(LitJson.JsonData jsonItem in jsonObj){
groupList.Add(jsonItem.ToString());}

}


public void set_luckRecordScore_fromJson(LitJson.JsonData jsonObj){
luckRecordScore= Int32.Parse(jsonObj.ToString());
}


public void set_luckScore_fromJson(LitJson.JsonData jsonObj){
luckScore= Int32.Parse(jsonObj.ToString());
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
latitude= Single.Parse(jsonObj.ToString());
}


public void set_longitude_fromJson(LitJson.JsonData jsonObj){
longitude= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(value !=  null){
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
else {}if(luckRecordScore !=  null){
resultStr += ",";resultStr += get_luckRecordScore_json();
}
else {}if(luckScore !=  null){
resultStr += ",";resultStr += get_luckScore_json();
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

public override void DeserializerJson(String json){
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
if(jsonObj["luckRecordScore"] != null){
set_luckRecordScore_fromJson(jsonObj["luckRecordScore"]);
}
if(jsonObj["luckScore"] != null){
set_luckScore_fromJson(jsonObj["luckScore"]);
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
