﻿// 此文件由协议导出插件自动生成
// ID : 00001]
//****���ֲ�����****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///���ֲ�����
/// <\summary>
public class P_ClubSetting : CherishBitProtocolBase {
/// <summary>
///������Ϸ������ ���ͷ���
/// <\summary>
public Int32 scoreLimit;
/// <summary>
///��˰���� 0��Ӯ�� 1������ 2������
/// <\summary>
public byte collectTaxesType;
/// <summary>
///��Ӯ�ҵ�ʱ�� ��ʼ��ȡ���ż�
/// <\summary>
public Int32 collectStart;
/// <summary>
///����
/// <\summary>
public Int32 collectScale;
/// <summary>
///����
/// <\summary>
public Int32 collectScore;
/// <summary>
///��ȡģʽ0���� 1����
/// <\summary>
public byte collectMode;
/// <summary>
///��Ϸ����Щ
/// <\summary>
public List<P_GameSetting> gamesSetting;
public P_ClubSetting(){}

public P_ClubSetting(Int32 _scoreLimit, byte _collectTaxesType, Int32 _collectStart, Int32 _collectScale, Int32 _collectScore, byte _collectMode, List<P_GameSetting> _gamesSetting){
this.scoreLimit = _scoreLimit;
this.collectTaxesType = _collectTaxesType;
this.collectStart = _collectStart;
this.collectScale = _collectScale;
this.collectScore = _collectScore;
this.collectMode = _collectMode;
this.gamesSetting = _gamesSetting;
}
private byte[] get_scoreLimit_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)scoreLimit);
return outBuf;
}


private byte[] get_collectTaxesType_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)collectTaxesType;
return outBuf;
}


private byte[] get_collectStart_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)collectStart);
return outBuf;
}


private byte[] get_collectScale_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)collectScale);
return outBuf;
}


private byte[] get_collectScore_encoding(){
byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)collectScore);
return outBuf;
}


private byte[] get_collectMode_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)collectMode;
return outBuf;
}


private byte[] get_gamesSetting_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_GameSetting> listBase = gamesSetting;
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

private int set_scoreLimit_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
scoreLimit = new Int32();
scoreLimit = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_collectTaxesType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
collectTaxesType = new byte();
collectTaxesType = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_collectStart_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
collectStart = new Int32();
collectStart = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_collectScale_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
collectScale = new Int32();
collectScale = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_collectScore_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
collectScore = new Int32();
collectScore = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_collectMode_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
collectMode = new byte();
collectMode = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_gamesSetting_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gamesSetting = new List<P_GameSetting>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_GameSetting curTarget = new P_GameSetting();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
gamesSetting.Add(curTarget);
}
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(scoreLimit !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_scoreLimit_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(collectTaxesType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_collectTaxesType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(collectStart !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_collectStart_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(collectScale !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_collectScale_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(collectScore !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_collectScore_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(collectMode !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_collectMode_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gamesSetting !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gamesSetting_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_scoreLimit_fromBuf(sourceBuf,startOffset);
startOffset = set_collectTaxesType_fromBuf(sourceBuf,startOffset);
startOffset = set_collectStart_fromBuf(sourceBuf,startOffset);
startOffset = set_collectScale_fromBuf(sourceBuf,startOffset);
startOffset = set_collectScore_fromBuf(sourceBuf,startOffset);
startOffset = set_collectMode_fromBuf(sourceBuf,startOffset);
startOffset = set_gamesSetting_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_scoreLimit_json(){
if(scoreLimit==null){return "";}string resultJson = "\"scoreLimit\":";resultJson += "\"";resultJson += scoreLimit.ToString();resultJson += "\"";return resultJson;
}


public string get_collectTaxesType_json(){
if(collectTaxesType==null){return "";}string resultJson = "\"collectTaxesType\":";resultJson += "\"";resultJson += collectTaxesType.ToString();resultJson += "\"";return resultJson;
}


public string get_collectStart_json(){
if(collectStart==null){return "";}string resultJson = "\"collectStart\":";resultJson += "\"";resultJson += collectStart.ToString();resultJson += "\"";return resultJson;
}


public string get_collectScale_json(){
if(collectScale==null){return "";}string resultJson = "\"collectScale\":";resultJson += "\"";resultJson += collectScale.ToString();resultJson += "\"";return resultJson;
}


public string get_collectScore_json(){
if(collectScore==null){return "";}string resultJson = "\"collectScore\":";resultJson += "\"";resultJson += collectScore.ToString();resultJson += "\"";return resultJson;
}


public string get_collectMode_json(){
if(collectMode==null){return "";}string resultJson = "\"collectMode\":";resultJson += "\"";resultJson += collectMode.ToString();resultJson += "\"";return resultJson;
}


public string get_gamesSetting_json(){
if(gamesSetting==null){return "";}string resultJson = "\"gamesSetting\":";resultJson += "[";
List<P_GameSetting> listObj = (List<P_GameSetting>)gamesSetting;
for(int i = 0;i < listObj.Count;++i){
P_GameSetting item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_scoreLimit_fromJson(LitJson.JsonData jsonObj){
scoreLimit= Int32.Parse(jsonObj.ToString());
}


public void set_collectTaxesType_fromJson(LitJson.JsonData jsonObj){
collectTaxesType= byte.Parse(jsonObj.ToString());
}


public void set_collectStart_fromJson(LitJson.JsonData jsonObj){
collectStart= Int32.Parse(jsonObj.ToString());
}


public void set_collectScale_fromJson(LitJson.JsonData jsonObj){
collectScale= Int32.Parse(jsonObj.ToString());
}


public void set_collectScore_fromJson(LitJson.JsonData jsonObj){
collectScore= Int32.Parse(jsonObj.ToString());
}


public void set_collectMode_fromJson(LitJson.JsonData jsonObj){
collectMode= byte.Parse(jsonObj.ToString());
}


public void set_gamesSetting_fromJson(LitJson.JsonData jsonObj){
gamesSetting = new List<P_GameSetting>();
foreach (LitJson.JsonData item in jsonObj){
P_GameSetting addB = new P_GameSetting();
gamesSetting.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override string SerializerJson(){
string resultStr = "{";if(scoreLimit !=  null){
resultStr += get_scoreLimit_json();
}
else {}if(collectTaxesType !=  null){
resultStr += ",";resultStr += get_collectTaxesType_json();
}
else {}if(collectStart !=  null){
resultStr += ",";resultStr += get_collectStart_json();
}
else {}if(collectScale !=  null){
resultStr += ",";resultStr += get_collectScale_json();
}
else {}if(collectScore !=  null){
resultStr += ",";resultStr += get_collectScore_json();
}
else {}if(collectMode !=  null){
resultStr += ",";resultStr += get_collectMode_json();
}
else {}if(gamesSetting !=  null){
resultStr += ",";resultStr += get_gamesSetting_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["scoreLimit"] != null){
set_scoreLimit_fromJson(jsonObj["scoreLimit"]);
}
if(jsonObj["collectTaxesType"] != null){
set_collectTaxesType_fromJson(jsonObj["collectTaxesType"]);
}
if(jsonObj["collectStart"] != null){
set_collectStart_fromJson(jsonObj["collectStart"]);
}
if(jsonObj["collectScale"] != null){
set_collectScale_fromJson(jsonObj["collectScale"]);
}
if(jsonObj["collectScore"] != null){
set_collectScore_fromJson(jsonObj["collectScore"]);
}
if(jsonObj["collectMode"] != null){
set_collectMode_fromJson(jsonObj["collectMode"]);
}
if(jsonObj["gamesSetting"] != null){
set_gamesSetting_fromJson(jsonObj["gamesSetting"]);
}
}
}
}