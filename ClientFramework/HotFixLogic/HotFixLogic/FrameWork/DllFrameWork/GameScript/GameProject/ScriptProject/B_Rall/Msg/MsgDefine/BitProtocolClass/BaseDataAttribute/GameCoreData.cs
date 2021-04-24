// 此文件由协议导出插件自动生成
// ID : 10036]
   
//****游戏核心数据****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace BaseDataAttribute{
/// <summary>
///游戏核心数据
/// <\summary>
public class GameCoreData : LantisBitProtocolBase {
/// <summary>
///角色信息
/// <\summary>
public RoleInfor _roleInfor;
public GameCoreData(){}

public GameCoreData(RoleInfor __roleInfor){
this._roleInfor = __roleInfor;
}
private Byte[] get__roleInfor_encoding(){
Byte[] outBuf = null;
outBuf = ((LantisBitProtocolBase)_roleInfor).Serializer();
return outBuf;
}

private int set__roleInfor_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_roleInfor = new RoleInfor();
curIndex = _roleInfor.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(_roleInfor !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__roleInfor_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set__roleInfor_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get__roleInfor_json(){
if(_roleInfor==null){return "";}String resultJson = "\"_roleInfor\":";resultJson += ((LantisBitProtocolBase)_roleInfor).SerializerJson();return resultJson;
}


public void set__roleInfor_fromJson(LitJson.JsonData jsonObj){
_roleInfor= new RoleInfor();
_roleInfor.DeserializerJson(jsonObj.ToJson());}

public override String SerializerJson(){
String resultStr = "{";if(_roleInfor !=  null){
resultStr += get__roleInfor_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["_roleInfor"] != null){
set__roleInfor_fromJson(jsonObj["_roleInfor"]);
}
}
}
}
