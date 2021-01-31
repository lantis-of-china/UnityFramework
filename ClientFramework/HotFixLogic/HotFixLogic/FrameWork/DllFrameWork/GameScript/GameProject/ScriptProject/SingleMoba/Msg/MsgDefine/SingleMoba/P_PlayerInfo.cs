// 此文件由协议导出插件自动生成
// ID : 00001]
//****Moba中玩家信息****
using System;
using System.Collections.Generic;
using System.IO;


namespace SingleMoba{
/// <summary>
///Moba中玩家信息
/// <\summary>
public class P_PlayerInfo : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 playerId;
/// <summary>
///
/// <\summary>
public Byte sex;
/// <summary>
///
/// <\summary>
public Byte level;
/// <summary>
///
/// <\summary>
public String name;
/// <summary>
///
/// <\summary>
public Int32 state;
/// <summary>
///
/// <\summary>
public Int32 score;
/// <summary>
///
/// <\summary>
public String headIcon;
/// <summary>
///
/// <\summary>
public Int32 kill;
/// <summary>
///
/// <\summary>
public Int32 power;
/// <summary>
///
/// <\summary>
public Int32 boom;
/// <summary>
///
/// <\summary>
public Byte moudleId;
/// <summary>
///
/// <\summary>
public List<P_Skill> skills;
/// <summary>
///
/// <\summary>
public List<P_SkillBuff> buffs;
/// <summary>
///
/// <\summary>
public Single x;
/// <summary>
///
/// <\summary>
public Single y;
/// <summary>
///
/// <\summary>
public Single targetX;
/// <summary>
///
/// <\summary>
public Single targetY;
/// <summary>
///
/// <\summary>
public Int64 lastMoveTicks;
/// <summary>
///
/// <\summary>
public Int32 hp;
/// <summary>
///
/// <\summary>
public Int32 maxHp;
/// <summary>
///
/// <\summary>
public Int32 site;
/// <summary>
///
/// <\summary>
public Byte canMove;
public P_PlayerInfo(){}

public P_PlayerInfo(Int32 _playerId, Byte _sex, Byte _level, String _name, Int32 _state, Int32 _score, String _headIcon, Int32 _kill, Int32 _power, Int32 _boom, Byte _moudleId, List<P_Skill> _skills, List<P_SkillBuff> _buffs, Single _x, Single _y, Single _targetX, Single _targetY, Int64 _lastMoveTicks, Int32 _hp, Int32 _maxHp, Int32 _site, Byte _canMove){
this.playerId = _playerId;
this.sex = _sex;
this.level = _level;
this.name = _name;
this.state = _state;
this.score = _score;
this.headIcon = _headIcon;
this.kill = _kill;
this.power = _power;
this.boom = _boom;
this.moudleId = _moudleId;
this.skills = _skills;
this.buffs = _buffs;
this.x = _x;
this.y = _y;
this.targetX = _targetX;
this.targetY = _targetY;
this.lastMoveTicks = _lastMoveTicks;
this.hp = _hp;
this.maxHp = _maxHp;
this.site = _site;
this.canMove = _canMove;
}
private Byte[] get_playerId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)playerId);
return outBuf;
}


private Byte[] get_sex_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)sex;
return outBuf;
}


private Byte[] get_level_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)level;
return outBuf;
}


private Byte[] get_name_encoding(){
Byte[] outBuf = null;
String str = (String)name;
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


private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)state);
return outBuf;
}


private Byte[] get_score_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)score);
return outBuf;
}


private Byte[] get_headIcon_encoding(){
Byte[] outBuf = null;
String str = (String)headIcon;
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


private Byte[] get_kill_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)kill);
return outBuf;
}


private Byte[] get_power_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)power);
return outBuf;
}


private Byte[] get_boom_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)boom);
return outBuf;
}


private Byte[] get_moudleId_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)moudleId;
return outBuf;
}


private Byte[] get_skills_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_Skill> listBase = skills;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_buffs_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_SkillBuff> listBase = buffs;
memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);
for(int i = 0;i < listBase.Count;++i){
CherishBitProtocolBase baseObject = listBase[i];
Byte[] baseBuf = baseObject.Serializer();
memoryWrite.Write(baseBuf,0,baseBuf.Length);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}


private Byte[] get_x_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)x);
return outBuf;
}


private Byte[] get_y_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)y);
return outBuf;
}


private Byte[] get_targetX_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)targetX);
return outBuf;
}


private Byte[] get_targetY_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Single)targetY);
return outBuf;
}


private Byte[] get_lastMoveTicks_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int64)lastMoveTicks);
return outBuf;
}


private Byte[] get_hp_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)hp);
return outBuf;
}


private Byte[] get_maxHp_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)maxHp);
return outBuf;
}


private Byte[] get_site_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)site);
return outBuf;
}


private Byte[] get_canMove_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)canMove;
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
private int set_sex_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
sex = new Byte();
sex = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_level_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
level = new Byte();
level = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_name_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
name = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
name = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
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
private int set_score_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
score = new Int32();
score = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_headIcon_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
headIcon = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
headIcon = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_kill_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
kill = new Int32();
kill = BitConverter.ToInt32(sourceBuf,curIndex);
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
private int set_boom_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
boom = new Int32();
boom = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_moudleId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
moudleId = new Byte();
moudleId = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_skills_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
skills = new List<P_Skill>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_Skill curTarget = new P_Skill();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
skills.Add(curTarget);
}
}return curIndex;
}
private int set_buffs_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
buffs = new List<P_SkillBuff>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_SkillBuff curTarget = new P_SkillBuff();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
buffs.Add(curTarget);
}
}return curIndex;
}
private int set_x_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
x = new Single();
x = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_y_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
y = new Single();
y = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_targetX_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
targetX = new Single();
targetX = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_targetY_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
targetY = new Single();
targetY = BitConverter.ToSingle(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_lastMoveTicks_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
lastMoveTicks = new Int64();
lastMoveTicks = BitConverter.ToInt64(sourceBuf,curIndex);
curIndex += 8;
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
private int set_maxHp_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
maxHp = new Int32();
maxHp = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_site_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
site = new Int32();
site = BitConverter.ToInt32(sourceBuf,curIndex);
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(playerId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_playerId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(sex !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_sex_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(level !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_level_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(name !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_name_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(state !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_state_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(score !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_score_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(headIcon !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_headIcon_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(kill !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_kill_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(power !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_power_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(boom !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_boom_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(moudleId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_moudleId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(skills !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_skills_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(buffs !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_buffs_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(x !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_x_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(y !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_y_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(targetX !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_targetX_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(targetY !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_targetY_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(lastMoveTicks !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_lastMoveTicks_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hp !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hp_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(maxHp !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_maxHp_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(site !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_site_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(canMove !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_canMove_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerId_fromBuf(sourceBuf,startOffset);
startOffset = set_sex_fromBuf(sourceBuf,startOffset);
startOffset = set_level_fromBuf(sourceBuf,startOffset);
startOffset = set_name_fromBuf(sourceBuf,startOffset);
startOffset = set_state_fromBuf(sourceBuf,startOffset);
startOffset = set_score_fromBuf(sourceBuf,startOffset);
startOffset = set_headIcon_fromBuf(sourceBuf,startOffset);
startOffset = set_kill_fromBuf(sourceBuf,startOffset);
startOffset = set_power_fromBuf(sourceBuf,startOffset);
startOffset = set_boom_fromBuf(sourceBuf,startOffset);
startOffset = set_moudleId_fromBuf(sourceBuf,startOffset);
startOffset = set_skills_fromBuf(sourceBuf,startOffset);
startOffset = set_buffs_fromBuf(sourceBuf,startOffset);
startOffset = set_x_fromBuf(sourceBuf,startOffset);
startOffset = set_y_fromBuf(sourceBuf,startOffset);
startOffset = set_targetX_fromBuf(sourceBuf,startOffset);
startOffset = set_targetY_fromBuf(sourceBuf,startOffset);
startOffset = set_lastMoveTicks_fromBuf(sourceBuf,startOffset);
startOffset = set_hp_fromBuf(sourceBuf,startOffset);
startOffset = set_maxHp_fromBuf(sourceBuf,startOffset);
startOffset = set_site_fromBuf(sourceBuf,startOffset);
startOffset = set_canMove_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerId_json(){
if(playerId==null){return "";}String resultJson = "\"playerId\":";resultJson += "\"";resultJson += playerId.ToString();resultJson += "\"";return resultJson;
}


public String get_sex_json(){
if(sex==null){return "";}String resultJson = "\"sex\":";resultJson += "\"";resultJson += sex.ToString();resultJson += "\"";return resultJson;
}


public String get_level_json(){
if(level==null){return "";}String resultJson = "\"level\":";resultJson += "\"";resultJson += level.ToString();resultJson += "\"";return resultJson;
}


public String get_name_json(){
if(name==null){return "";}String resultJson = "\"name\":";resultJson += "\"";resultJson += name.ToString();resultJson += "\"";return resultJson;
}


public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public String get_score_json(){
if(score==null){return "";}String resultJson = "\"score\":";resultJson += "\"";resultJson += score.ToString();resultJson += "\"";return resultJson;
}


public String get_headIcon_json(){
if(headIcon==null){return "";}String resultJson = "\"headIcon\":";resultJson += "\"";resultJson += headIcon.ToString();resultJson += "\"";return resultJson;
}


public String get_kill_json(){
if(kill==null){return "";}String resultJson = "\"kill\":";resultJson += "\"";resultJson += kill.ToString();resultJson += "\"";return resultJson;
}


public String get_power_json(){
if(power==null){return "";}String resultJson = "\"power\":";resultJson += "\"";resultJson += power.ToString();resultJson += "\"";return resultJson;
}


public String get_boom_json(){
if(boom==null){return "";}String resultJson = "\"boom\":";resultJson += "\"";resultJson += boom.ToString();resultJson += "\"";return resultJson;
}


public String get_moudleId_json(){
if(moudleId==null){return "";}String resultJson = "\"moudleId\":";resultJson += "\"";resultJson += moudleId.ToString();resultJson += "\"";return resultJson;
}


public String get_skills_json(){
if(skills==null){return "";}String resultJson = "\"skills\":";resultJson += "[";
List<P_Skill> listObj = (List<P_Skill>)skills;
for(int i = 0;i < listObj.Count;++i){
P_Skill item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_buffs_json(){
if(buffs==null){return "";}String resultJson = "\"buffs\":";resultJson += "[";
List<P_SkillBuff> listObj = (List<P_SkillBuff>)buffs;
for(int i = 0;i < listObj.Count;++i){
P_SkillBuff item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public String get_x_json(){
if(x==null){return "";}String resultJson = "\"x\":";resultJson += "\"";resultJson += x.ToString();resultJson += "\"";return resultJson;
}


public String get_y_json(){
if(y==null){return "";}String resultJson = "\"y\":";resultJson += "\"";resultJson += y.ToString();resultJson += "\"";return resultJson;
}


public String get_targetX_json(){
if(targetX==null){return "";}String resultJson = "\"targetX\":";resultJson += "\"";resultJson += targetX.ToString();resultJson += "\"";return resultJson;
}


public String get_targetY_json(){
if(targetY==null){return "";}String resultJson = "\"targetY\":";resultJson += "\"";resultJson += targetY.ToString();resultJson += "\"";return resultJson;
}


public String get_lastMoveTicks_json(){
if(lastMoveTicks==null){return "";}String resultJson = "\"lastMoveTicks\":";resultJson += "\"";resultJson += lastMoveTicks.ToString();resultJson += "\"";return resultJson;
}


public String get_hp_json(){
if(hp==null){return "";}String resultJson = "\"hp\":";resultJson += "\"";resultJson += hp.ToString();resultJson += "\"";return resultJson;
}


public String get_maxHp_json(){
if(maxHp==null){return "";}String resultJson = "\"maxHp\":";resultJson += "\"";resultJson += maxHp.ToString();resultJson += "\"";return resultJson;
}


public String get_site_json(){
if(site==null){return "";}String resultJson = "\"site\":";resultJson += "\"";resultJson += site.ToString();resultJson += "\"";return resultJson;
}


public String get_canMove_json(){
if(canMove==null){return "";}String resultJson = "\"canMove\":";resultJson += "\"";resultJson += canMove.ToString();resultJson += "\"";return resultJson;
}


public void set_playerId_fromJson(LitJson.JsonData jsonObj){
playerId= Int32.Parse(jsonObj.ToString());
}


public void set_sex_fromJson(LitJson.JsonData jsonObj){
sex= Byte.Parse(jsonObj.ToString());
}


public void set_level_fromJson(LitJson.JsonData jsonObj){
level= Byte.Parse(jsonObj.ToString());
}


public void set_name_fromJson(LitJson.JsonData jsonObj){
name= jsonObj.ToString();
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Int32.Parse(jsonObj.ToString());
}


public void set_score_fromJson(LitJson.JsonData jsonObj){
score= Int32.Parse(jsonObj.ToString());
}


public void set_headIcon_fromJson(LitJson.JsonData jsonObj){
headIcon= jsonObj.ToString();
}


public void set_kill_fromJson(LitJson.JsonData jsonObj){
kill= Int32.Parse(jsonObj.ToString());
}


public void set_power_fromJson(LitJson.JsonData jsonObj){
power= Int32.Parse(jsonObj.ToString());
}


public void set_boom_fromJson(LitJson.JsonData jsonObj){
boom= Int32.Parse(jsonObj.ToString());
}


public void set_moudleId_fromJson(LitJson.JsonData jsonObj){
moudleId= Byte.Parse(jsonObj.ToString());
}


public void set_skills_fromJson(LitJson.JsonData jsonObj){
skills = new List<P_Skill>();
foreach (LitJson.JsonData item in jsonObj){
P_Skill addB = new P_Skill();
skills.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_buffs_fromJson(LitJson.JsonData jsonObj){
buffs = new List<P_SkillBuff>();
foreach (LitJson.JsonData item in jsonObj){
P_SkillBuff addB = new P_SkillBuff();
buffs.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}


public void set_x_fromJson(LitJson.JsonData jsonObj){
x= Single.Parse(jsonObj.ToString());
}


public void set_y_fromJson(LitJson.JsonData jsonObj){
y= Single.Parse(jsonObj.ToString());
}


public void set_targetX_fromJson(LitJson.JsonData jsonObj){
targetX= Single.Parse(jsonObj.ToString());
}


public void set_targetY_fromJson(LitJson.JsonData jsonObj){
targetY= Single.Parse(jsonObj.ToString());
}


public void set_lastMoveTicks_fromJson(LitJson.JsonData jsonObj){
lastMoveTicks= Int64.Parse(jsonObj.ToString());
}


public void set_hp_fromJson(LitJson.JsonData jsonObj){
hp= Int32.Parse(jsonObj.ToString());
}


public void set_maxHp_fromJson(LitJson.JsonData jsonObj){
maxHp= Int32.Parse(jsonObj.ToString());
}


public void set_site_fromJson(LitJson.JsonData jsonObj){
site= Int32.Parse(jsonObj.ToString());
}


public void set_canMove_fromJson(LitJson.JsonData jsonObj){
canMove= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(playerId !=  null){
resultStr += get_playerId_json();
}
else {}if(sex !=  null){
resultStr += ",";resultStr += get_sex_json();
}
else {}if(level !=  null){
resultStr += ",";resultStr += get_level_json();
}
else {}if(name !=  null){
resultStr += ",";resultStr += get_name_json();
}
else {}if(state !=  null){
resultStr += ",";resultStr += get_state_json();
}
else {}if(score !=  null){
resultStr += ",";resultStr += get_score_json();
}
else {}if(headIcon !=  null){
resultStr += ",";resultStr += get_headIcon_json();
}
else {}if(kill !=  null){
resultStr += ",";resultStr += get_kill_json();
}
else {}if(power !=  null){
resultStr += ",";resultStr += get_power_json();
}
else {}if(boom !=  null){
resultStr += ",";resultStr += get_boom_json();
}
else {}if(moudleId !=  null){
resultStr += ",";resultStr += get_moudleId_json();
}
else {}if(skills !=  null){
resultStr += ",";resultStr += get_skills_json();
}
else {}if(buffs !=  null){
resultStr += ",";resultStr += get_buffs_json();
}
else {}if(x !=  null){
resultStr += ",";resultStr += get_x_json();
}
else {}if(y !=  null){
resultStr += ",";resultStr += get_y_json();
}
else {}if(targetX !=  null){
resultStr += ",";resultStr += get_targetX_json();
}
else {}if(targetY !=  null){
resultStr += ",";resultStr += get_targetY_json();
}
else {}if(lastMoveTicks !=  null){
resultStr += ",";resultStr += get_lastMoveTicks_json();
}
else {}if(hp !=  null){
resultStr += ",";resultStr += get_hp_json();
}
else {}if(maxHp !=  null){
resultStr += ",";resultStr += get_maxHp_json();
}
else {}if(site !=  null){
resultStr += ",";resultStr += get_site_json();
}
else {}if(canMove !=  null){
resultStr += ",";resultStr += get_canMove_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerId"] != null){
set_playerId_fromJson(jsonObj["playerId"]);
}
if(jsonObj["sex"] != null){
set_sex_fromJson(jsonObj["sex"]);
}
if(jsonObj["level"] != null){
set_level_fromJson(jsonObj["level"]);
}
if(jsonObj["name"] != null){
set_name_fromJson(jsonObj["name"]);
}
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
if(jsonObj["score"] != null){
set_score_fromJson(jsonObj["score"]);
}
if(jsonObj["headIcon"] != null){
set_headIcon_fromJson(jsonObj["headIcon"]);
}
if(jsonObj["kill"] != null){
set_kill_fromJson(jsonObj["kill"]);
}
if(jsonObj["power"] != null){
set_power_fromJson(jsonObj["power"]);
}
if(jsonObj["boom"] != null){
set_boom_fromJson(jsonObj["boom"]);
}
if(jsonObj["moudleId"] != null){
set_moudleId_fromJson(jsonObj["moudleId"]);
}
if(jsonObj["skills"] != null){
set_skills_fromJson(jsonObj["skills"]);
}
if(jsonObj["buffs"] != null){
set_buffs_fromJson(jsonObj["buffs"]);
}
if(jsonObj["x"] != null){
set_x_fromJson(jsonObj["x"]);
}
if(jsonObj["y"] != null){
set_y_fromJson(jsonObj["y"]);
}
if(jsonObj["targetX"] != null){
set_targetX_fromJson(jsonObj["targetX"]);
}
if(jsonObj["targetY"] != null){
set_targetY_fromJson(jsonObj["targetY"]);
}
if(jsonObj["lastMoveTicks"] != null){
set_lastMoveTicks_fromJson(jsonObj["lastMoveTicks"]);
}
if(jsonObj["hp"] != null){
set_hp_fromJson(jsonObj["hp"]);
}
if(jsonObj["maxHp"] != null){
set_maxHp_fromJson(jsonObj["maxHp"]);
}
if(jsonObj["site"] != null){
set_site_fromJson(jsonObj["site"]);
}
if(jsonObj["canMove"] != null){
set_canMove_fromJson(jsonObj["canMove"]);
}
}
}
}
