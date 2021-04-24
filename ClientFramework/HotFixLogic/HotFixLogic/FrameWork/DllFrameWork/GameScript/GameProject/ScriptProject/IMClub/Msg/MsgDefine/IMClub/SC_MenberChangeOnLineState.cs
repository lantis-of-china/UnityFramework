// 此文件由协议导出插件自动生成
// ID : 00001]
//****�л���Ա����״̬[menberChangeOnLineState]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///�л���Ա����״̬[menberChangeOnLineState]
/// <\summary>
public class SC_MenberChangeOnLineState : LantisBitProtocolBase {
/// <summary>
///��Ա״̬ 0���� 1����
/// <\summary>
public Byte state;
/// <summary>
///��ԱID
/// <\summary>
public Int32 menberId;
/// <summary>
///���ֲ�ID
/// <\summary>
public String clubId;
public SC_MenberChangeOnLineState(){}

public SC_MenberChangeOnLineState(Byte _state, Int32 _menberId, String _clubId){
this.state = _state;
this.menberId = _menberId;
this.clubId = _clubId;
}
private Byte[] get_state_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)state;
return outBuf;
}


private Byte[] get_menberId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)menberId);
return outBuf;
}


private Byte[] get_clubId_encoding(){
Byte[] outBuf = null;
String str = (String)clubId;
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

private int set_state_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
state = new Byte();
state = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_menberId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberId = new Int32();
menberId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_clubId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
clubId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
clubId = System.Text.Encoding.UTF8.GetString(byteArray);
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
}if(menberId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_state_fromBuf(sourceBuf,startOffset);
startOffset = set_menberId_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_state_json(){
if(state==null){return "";}String resultJson = "\"state\":";resultJson += "\"";resultJson += state.ToString();resultJson += "\"";return resultJson;
}


public String get_menberId_json(){
if(menberId==null){return "";}String resultJson = "\"menberId\":";resultJson += "\"";resultJson += menberId.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public void set_state_fromJson(LitJson.JsonData jsonObj){
state= Byte.Parse(jsonObj.ToString());
}


public void set_menberId_fromJson(LitJson.JsonData jsonObj){
menberId= Int32.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}

public override String SerializerJson(){
String resultStr = "{";if(state !=  null){
resultStr += get_state_json();
}
else {}if(menberId !=  null){
resultStr += ",";resultStr += get_menberId_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["state"] != null){
set_state_fromJson(jsonObj["state"]);
}
if(jsonObj["menberId"] != null){
set_menberId_fromJson(jsonObj["menberId"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
}
}
}
