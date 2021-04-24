// 此文件由协议导出插件自动生成
// ID : 00001]
//****��ȡ��Ա��ϢS2C_GetMenbers_MsgType = 20012****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///��ȡ��Ա��ϢS2C_GetMenbers_MsgType = 20012
/// <\summary>
public class SC_GetMenbers : LantisBitProtocolBase {
/// <summary>
///������Ϣ 0ʧ�� 1�ɹ�
/// <\summary>
public Byte result;
/// <summary>
///ȺID
/// <\summary>
public String clubId;
/// <summary>
///��Ա��Ϣ
/// <\summary>
public List<P_Menber> menberList;
public SC_GetMenbers(){}

public SC_GetMenbers(Byte _result, String _clubId, List<P_Menber> _menberList){
this.result = _result;
this.clubId = _clubId;
this.menberList = _menberList;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
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


private Byte[] get_menberList_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_Menber> listBase = menberList;
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

private int set_result_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new Byte();
result = sourceBuf[curIndex];
curIndex++;
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
private int set_menberList_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
menberList = new List<P_Menber>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_Menber curTarget = new P_Menber();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
menberList.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(result !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_result_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(clubId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_clubId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(menberList !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_menberList_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_clubId_fromBuf(sourceBuf,startOffset);
startOffset = set_menberList_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public String get_clubId_json(){
if(clubId==null){return "";}String resultJson = "\"clubId\":";resultJson += "\"";resultJson += clubId.ToString();resultJson += "\"";return resultJson;
}


public String get_menberList_json(){
if(menberList==null){return "";}String resultJson = "\"menberList\":";resultJson += "[";
List<P_Menber> listObj = (List<P_Menber>)menberList;
for(int i = 0;i < listObj.Count;++i){
P_Menber item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}


public void set_clubId_fromJson(LitJson.JsonData jsonObj){
clubId= jsonObj.ToString();
}


public void set_menberList_fromJson(LitJson.JsonData jsonObj){
menberList = new List<P_Menber>();
foreach (LitJson.JsonData item in jsonObj){
P_Menber addB = new P_Menber();
menberList.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(clubId !=  null){
resultStr += ",";resultStr += get_clubId_json();
}
else {}if(menberList !=  null){
resultStr += ",";resultStr += get_menberList_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["clubId"] != null){
set_clubId_fromJson(jsonObj["clubId"]);
}
if(jsonObj["menberList"] != null){
set_menberList_fromJson(jsonObj["menberList"]);
}
}
}
}
