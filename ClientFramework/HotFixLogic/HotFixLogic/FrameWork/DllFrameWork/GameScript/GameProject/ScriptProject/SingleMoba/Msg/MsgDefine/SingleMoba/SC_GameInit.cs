// 此文件由协议导出插件自动生成
// ID : 00000]
//****开始游戏****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///开始游戏
/// <\summary>
public class SC_GameInit : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public List<P_PlayerInfo> playerDatas;
public SC_GameInit(){}

public SC_GameInit(List<P_PlayerInfo> _playerDatas){
this.playerDatas = _playerDatas;
}
private Byte[] get_playerDatas_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_PlayerInfo> listBase = playerDatas;
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

private int set_playerDatas_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
playerDatas = new List<P_PlayerInfo>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_PlayerInfo curTarget = new P_PlayerInfo();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
playerDatas.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(playerDatas !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_playerDatas_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_playerDatas_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_playerDatas_json(){
if(playerDatas==null){return "";}String resultJson = "\"playerDatas\":";resultJson += "[";
List<P_PlayerInfo> listObj = (List<P_PlayerInfo>)playerDatas;
for(int i = 0;i < listObj.Count;++i){
P_PlayerInfo item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_playerDatas_fromJson(LitJson.JsonData jsonObj){
playerDatas = new List<P_PlayerInfo>();
foreach (LitJson.JsonData item in jsonObj){
P_PlayerInfo addB = new P_PlayerInfo();
playerDatas.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(playerDatas !=  null){
resultStr += get_playerDatas_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["playerDatas"] != null){
set_playerDatas_fromJson(jsonObj["playerDatas"]);
}
}
}
}
