// 此文件由协议导出插件自动生成
// ID : 00000]
//****道具****
using System;
using System.Collections.Generic;
using System.IO;
using AskDao;
using BaseDataAttribute;
using Server;
using Baccarat;
using BingShangQuGunQiu;
using BuYu;
using CheXuan;
using CMSloto;
using IMClub;
using LaoHuJi;
using MaJiang_QuanZhou;
using MaJiang_XueZhan;
using PaoDeKuai;
using SingleMoba;
using Template;
using WuXingJingCai;


namespace SingleMoba{
/// <summary>
///道具
/// <\summary>
public class P_Prop : CherishBitProtocolBase {
/// <summary>
///
/// <\summary>
public Int32 id;
/// <summary>
///
/// <\summary>
public Int32 cfgId;
/// <summary>
///
/// <\summary>
public Single x;
/// <summary>
///
/// <\summary>
public Single y;
public P_Prop(){}

public P_Prop(Int32 _id, Int32 _cfgId, Single _x, Single _y){
this.id = _id;
this.cfgId = _cfgId;
this.x = _x;
this.y = _y;
}
private Byte[] get_id_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)id);
return outBuf;
}


private Byte[] get_cfgId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)cfgId);
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

private int set_id_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
id = new Int32();
id = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
private int set_cfgId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
cfgId = new Int32();
cfgId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(id !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_id_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(cfgId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_cfgId_encoding();
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
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_cfgId_fromBuf(sourceBuf,startOffset);
startOffset = set_x_fromBuf(sourceBuf,startOffset);
startOffset = set_y_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_id_json(){
if(id==null){return "";}String resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public String get_cfgId_json(){
if(cfgId==null){return "";}String resultJson = "\"cfgId\":";resultJson += "\"";resultJson += cfgId.ToString();resultJson += "\"";return resultJson;
}


public String get_x_json(){
if(x==null){return "";}String resultJson = "\"x\":";resultJson += "\"";resultJson += x.ToString();resultJson += "\"";return resultJson;
}


public String get_y_json(){
if(y==null){return "";}String resultJson = "\"y\":";resultJson += "\"";resultJson += y.ToString();resultJson += "\"";return resultJson;
}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= Int32.Parse(jsonObj.ToString());
}


public void set_cfgId_fromJson(LitJson.JsonData jsonObj){
cfgId= Int32.Parse(jsonObj.ToString());
}


public void set_x_fromJson(LitJson.JsonData jsonObj){
x= Single.Parse(jsonObj.ToString());
}


public void set_y_fromJson(LitJson.JsonData jsonObj){
y= Single.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(id !=  null){
resultStr += get_id_json();
}
else {}if(cfgId !=  null){
resultStr += ",";resultStr += get_cfgId_json();
}
else {}if(x !=  null){
resultStr += ",";resultStr += get_x_json();
}
else {}if(y !=  null){
resultStr += ",";resultStr += get_y_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["id"] != null){
set_id_fromJson(jsonObj["id"]);
}
if(jsonObj["cfgId"] != null){
set_cfgId_fromJson(jsonObj["cfgId"]);
}
if(jsonObj["x"] != null){
set_x_fromJson(jsonObj["x"]);
}
if(jsonObj["y"] != null){
set_y_fromJson(jsonObj["y"]);
}
}
}
}
