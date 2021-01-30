// 此文件由协议导出插件自动生成
// ID : 00001]
//****������Ϸ����[ʹ��֪ͨ=setGameSetting]****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///������Ϸ����[ʹ��֪ͨ=setGameSetting]
/// <\summary>
public class SC_SetGameSetting : CherishBitProtocolBase {
/// <summary>
///���ֲ�ID
/// <\summary>
public string clubId;
/// <summary>
///��Ϸ����
/// <\summary>
public P_GameSetting gameSetting;
/// <summary>
///���ò����б�
/// <\summary>
public List<Int32> paramars;
public SC_SetGameSetting(){}

public SC_SetGameSetting(string _clubId, P_GameSetting _gameSetting, List<Int32> _paramars){
this.clubId = _clubId;
this.gameSetting = _gameSetting;
this.paramars = _paramars;
}
private byte[] get_clubId_encoding(){
byte[] outBuf = null;
string str = (string)clubId;
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


private byte[] get_gameSetting_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)gameSetting).Serializer();
return outBuf;
}


private byte[] get_paramars_encoding(){
byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<Int32> listInt32 = (List<Int32>)paramars;
memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);
for(int i = 0;i < listInt32.Count;++i){
Int32 in32 = listInt32[i];
memoryWrite.Write(BitConverter.GetBytes(in32),0,4);
}
outBuf = memoryWrite.ToArray();
}
return outBuf;
}

private int set_clubId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
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
private int set_gameSetting_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
gameSetting = new P_GameSetting();
curIndex = gameSetting.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
private int set_paramars_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
paramars = new List<Int32>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
Int32 curTarget = BitConverter.ToInt32(sourceBuf,curIndex);
paramars.Add(curTarget);
curIndex += 4;
}
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(gameSetting !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_gameSetting_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(paramars !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_paramars_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_gameSetting_fromBuf(sourceBuf,startOffset);
startOffset = set_paramars_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_clubId_json(){
if(clubId==null){return "";}string resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public string get_gameSetting_json(){
if(gameSetting==null){return "";}string resultJson = "\"gameSetting\":";resultJson += ((CherishBitProtocolBase)gameSetting).SerializerJson();return resultJson;
}


public string get_paramars_json(){
if(paramars==null){return "";}string resultJson = "\"paramars\":";resultJson += "[";List<Int32> listObj = (List<Int32>)paramars;
for(int i = 0;i < listObj.Count;++i){
Int32 item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += "\"";resultJson += item.ToString();
resultJson += "\"";}
resultJson += "]";
return resultJson;
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_gameSetting_fromJson(LitJson.JsonData jsonObj){
gameSetting= new P_GameSetting();
gameSetting.DeserializerJson(jsonObj.ToJson());}


public void set_paramars_fromJson(LitJson.JsonData jsonObj){
paramars= new List<Int32>();
foreach(LitJson.JsonData jsonItem in jsonObj){
paramars.Add(Int32.Parse(jsonItem.ToString()));}

}

public override string SerializerJson(){
string resultStr = "{";if(clubId !=  null){
resultStr += get_clubId_json();
}
else {}if(gameSetting !=  null){
resultStr += ",";resultStr += get_gameSetting_json();
}
else {}if(paramars !=  null){
resultStr += ",";resultStr += get_paramars_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["gameSetting"] != null){
set_gameSetting_fromJson(jsonObj["gameSetting"]);
}
if(jsonObj["paramars"] != null){
set_paramars_fromJson(jsonObj["paramars"]);
}
}
}
}
