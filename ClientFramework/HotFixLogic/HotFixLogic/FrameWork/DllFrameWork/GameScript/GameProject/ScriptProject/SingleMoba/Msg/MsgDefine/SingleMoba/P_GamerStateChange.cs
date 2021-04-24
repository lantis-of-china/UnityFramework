// 此文件由协议导出插件自动生成
// ID : 00000]
//****玩家的状态信息改变****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///玩家的状态信息改变
/// <\summary>
public class P_GamerStateChange : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public Int32 hp;
/// <summary>
///
/// <\summary>
public Int32 hpChnage;
/// <summary>
///
/// <\summary>
public Int32 power;
/// <summary>
///
/// <\summary>
public Int32 powerChange;
/// <summary>
///
/// <\summary>
public Int32 score;
/// <summary>
///
/// <\summary>
public Int32 scoreChange;
/// <summary>
///
/// <\summary>
public Int32 boom;
/// <summary>
///
/// <\summary>
public Byte canMove;
/// <summary>
///
/// <\summary>
public List<P_SkillBuff> getBuffs;
/// <summary>
///
/// <\summary>
public List<Int32> lostBuffs;
public P_GamerStateChange(){}

public P_GamerStateChange(Int32 _playerId, Int32 _hp, Int32 _hpChnage, Int32 _power, Int32 _powerChange, Int32 _score, Int32 _scoreChange, Int32 _boom, Byte _canMove, List<P_SkillBuff> _getBuffs, List<Int32> _lostBuffs){
this.playerId = _playerId;
this.hp = _hp;
this.hpChnage = _hpChnage;
this.power = _power;
this.powerChange = _powerChange;
this.score = _score;
this.scoreChange = _scoreChange;
this.boom = _boom;
this.canMove = _canMove;
this.getBuffs = _getBuffs;
this.lostBuffs = _lostBuffs;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
return outBuf;
}


private Byte[] get_hp_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)hp);
return outBuf;
}


private Byte[] get_hpChnage_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)hpChnage);
return outBuf;
}


private Byte[] get_power_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)power);
return outBuf;
}


private Byte[] get_powerChange_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)powerChange);
return outBuf;
}


private Byte[] get_score_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)score);
return outBuf;
}


private Byte[] get_scoreChange_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)scoreChange);
return outBuf;
}


private Byte[] get_boom_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)boom);
return outBuf;
}


private Byte[] get_canMove_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)canMove;
return outBuf;
}


private Byte[] get_getBuffs_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_SkillBuff> listBase = getBuffs;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
LantisBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_lostBuffs_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)lostBuffs;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_playerId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
playerId = new Int32();
playerId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_hp_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hp = new Int32();
hp = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_hpChnage_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hpChnage = new Int32();
hpChnage = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_power_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
power = new Int32();
power = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_powerChange_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
powerChange = new Int32();
powerChange = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_score_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
score = new Int32();
score = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_scoreChange_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
scoreChange = new Int32();
scoreChange = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_boom_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
boom = new Int32();
boom = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_canMove_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
canMove = new Byte();
canMove = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_getBuffs_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
getBuffs = new List<P_SkillBuff>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_SkillBuff curTarget = new P_SkillBuff();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
getBuffs.Add(curTarget);
}
}return curIndex;
}
private int set_lostBuffs_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
lostBuffs = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
lostBuffs.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(playerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_playerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hp !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hp_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hpChnage !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hpChnage_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(power !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_power_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(powerChange !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_powerChange_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(score !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_score_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(scoreChange !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_scoreChange_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(boom !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_boom_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(canMove !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_canMove_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(getBuffs !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_getBuffs_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(lostBuffs !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_lostBuffs_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_hp_fromBuf(sourceBuf,startOffset);
startOffset = set_hpChnage_fromBuf(sourceBuf,startOffset);
startOffset = set_power_fromBuf(sourceBuf,startOffset);
startOffset = set_powerChange_fromBuf(sourceBuf,startOffset);
startOffset = set_score_fromBuf(sourceBuf,startOffset);
startOffset = set_scoreChange_fromBuf(sourceBuf,startOffset);
startOffset = set_boom_fromBuf(sourceBuf,startOffset);
startOffset = set_canMove_fromBuf(sourceBuf,startOffset);
startOffset = set_getBuffs_fromBuf(sourceBuf,startOffset);
startOffset = set_lostBuffs_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_hp_json(){
if(hp==null){return "";}String resultJson = "\"hp\":";resultJson += "\"";resultJson += hp.ToString();resultJson += "\"";return resultJson;
}


public String get_hpChnage_json(){
if(hpChnage==null){return "";}String resultJson = "\"hpChnage\":";resultJson += "\"";resultJson += hpChnage.ToString();resultJson += "\"";return resultJson;
}


public String get_power_json(){
if(power==null){return "";}String resultJson = "\"power\":";resultJson += "\"";resultJson += power.ToString();resultJson += "\"";return resultJson;
}


public String get_powerChange_json(){
if(powerChange==null){return "";}String resultJson = "\"powerChange\":";resultJson += "\"";resultJson += powerChange.ToString();resultJson += "\"";return resultJson;
}


public String get_score_json(){
if(score==null){return "";}String resultJson = "\"score\":";resultJson += "\"";resultJson += score.ToString();resultJson += "\"";return resultJson;
}


public String get_scoreChange_json(){
if(scoreChange==null){return "";}String resultJson = "\"scoreChange\":";resultJson += "\"";resultJson += scoreChange.ToString();resultJson += "\"";return resultJson;
}


public String get_boom_json(){
if(boom==null){return "";}String resultJson = "\"boom\":";resultJson += "\"";resultJson += boom.ToString();resultJson += "\"";return resultJson;
}


public String get_canMove_json(){
if(canMove==null){return "";}String resultJson = "\"canMove\":";resultJson += "\"";resultJson += canMove.ToString();resultJson += "\"";return resultJson;
}


public String get_getBuffs_json(){
if(getBuffs==null){return "";}String resultJson = "\"getBuffs\":";resultJson += "[";
List<P_SkillBuff> listObj = (List<P_SkillBuff>)getBuffs;
for(int i = 0;i < listObj.Count;++i){
P_SkillBuff item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_lostBuffs_json(){
if(lostBuffs==null){return "";}String resultJson = "\"lostBuffs\":";resultJson += "[";List<Int32> listObj = (List<Int32>)lostBuffs;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_hp_fromJson(LitJson.JsonData jsonObj){
hp= Int32.Parse(jsonObj.ToString());
}


public void set_hpChnage_fromJson(LitJson.JsonData jsonObj){
hpChnage= Int32.Parse(jsonObj.ToString());
}


public void set_power_fromJson(LitJson.JsonData jsonObj){
power= Int32.Parse(jsonObj.ToString());
}


public void set_powerChange_fromJson(LitJson.JsonData jsonObj){
powerChange= Int32.Parse(jsonObj.ToString());
}


public void set_score_fromJson(LitJson.JsonData jsonObj){
score= Int32.Parse(jsonObj.ToString());
}


public void set_scoreChange_fromJson(LitJson.JsonData jsonObj){
scoreChange= Int32.Parse(jsonObj.ToString());
}


public void set_boom_fromJson(LitJson.JsonData jsonObj){
boom= Int32.Parse(jsonObj.ToString());
}


public void set_canMove_fromJson(LitJson.JsonData jsonObj){
canMove= Byte.Parse(jsonObj.ToString());
}


public void set_getBuffs_fromJson(LitJson.JsonData jsonObj){
getBuffs = new List<P_SkillBuff>();
foreach (LitJson.JsonData item in jsonObj){
P_SkillBuff addB = new P_SkillBuff();
getBuffs.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_lostBuffs_fromJson(LitJson.JsonData jsonObj){
lostBuffs= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
lostBuffs.Add(Int32.Parse(jsonItem.ToString()));}

}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(hp !=  null){
resultStr += ",";resultStr += get_hp_json();
}
else {}if(hpChnage !=  null){
resultStr += ",";resultStr += get_hpChnage_json();
}
else {}if(power !=  null){
resultStr += ",";resultStr += get_power_json();
}
else {}if(powerChange !=  null){
resultStr += ",";resultStr += get_powerChange_json();
}
else {}if(score !=  null){
resultStr += ",";resultStr += get_score_json();
}
else {}if(scoreChange !=  null){
resultStr += ",";resultStr += get_scoreChange_json();
}
else {}if(boom !=  null){
resultStr += ",";resultStr += get_boom_json();
}
else {}if(canMove !=  null){
resultStr += ",";resultStr += get_canMove_json();
}
else {}if(getBuffs !=  null){
resultStr += ",";resultStr += get_getBuffs_json();
}
else {}if(lostBuffs !=  null){
resultStr += ",";resultStr += get_lostBuffs_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["hp"] != null){
set_hp_fromJson(jsonObj["hp"]);
}
if(jsonObj["hpChnage"] != null){
set_hpChnage_fromJson(jsonObj["hpChnage"]);
}
if(jsonObj["power"] != null){
set_power_fromJson(jsonObj["power"]);
}
if(jsonObj["powerChange"] != null){
set_powerChange_fromJson(jsonObj["powerChange"]);
}
if(jsonObj["score"] != null){
set_score_fromJson(jsonObj["score"]);
}
if(jsonObj["scoreChange"] != null){
set_scoreChange_fromJson(jsonObj["scoreChange"]);
}
if(jsonObj["boom"] != null){
set_boom_fromJson(jsonObj["boom"]);
}
if(jsonObj["canMove"] != null){
set_canMove_fromJson(jsonObj["canMove"]);
}
if(jsonObj["getBuffs"] != null){
set_getBuffs_fromJson(jsonObj["getBuffs"]);
}
if(jsonObj["lostBuffs"] != null){
set_lostBuffs_fromJson(jsonObj["lostBuffs"]);
}
}
}
}
