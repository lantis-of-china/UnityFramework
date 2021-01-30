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
public class GameCoreData : CherishBitProtocolBase {
/// <summary>
///角色信息
/// <\summary>
public RoleInfor _roleInfor;
public GameCoreData(){}

public GameCoreData(RoleInfor __roleInfor){
this._roleInfor = __roleInfor;
}
private byte[] get__roleInfor_encoding(){
byte[] outBuf = null;
outBuf = ((CherishBitProtocolBase)_roleInfor).Serializer();
return outBuf;
}

private int set__roleInfor_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
_roleInfor = new RoleInfor();
curIndex = _roleInfor.Deserializer(sourceBuf,curIndex);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(_roleInfor !=  null){
memoryWrite.WriteByte(1);
byteBuf = get__roleInfor_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set__roleInfor_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get__roleInfor_json(){
if(_roleInfor==null){return "";}string resultJson = "\"_roleInfor\":";resultJson += ((CherishBitProtocolBase)_roleInfor).SerializerJson();return resultJson;
}


public void set__roleInfor_fromJson(LitJson.JsonData jsonObj){
_roleInfor= new RoleInfor();
_roleInfor.DeserializerJson(jsonObj.ToJson());}

public override string SerializerJson(){
string resultStr = "{";if(_roleInfor !=  null){
resultStr += get__roleInfor_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["_roleInfor"] != null){
set__roleInfor_fromJson(jsonObj["_roleInfor"]);
}
}
}
}
