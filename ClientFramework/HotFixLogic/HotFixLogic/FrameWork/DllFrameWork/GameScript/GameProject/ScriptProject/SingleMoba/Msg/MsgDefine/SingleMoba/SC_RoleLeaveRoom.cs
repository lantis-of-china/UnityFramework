﻿// 此文件由协议导出插件自动生成
// ID : 00020]
   
//****玩家离开房间****
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
///玩家离开房间
/// <\summary>
public class SC_RoleLeaveRoom : CherishBitProtocolBase {
/// <summary>
///离开玩家的Id
/// <\summary>
public Int32 roleId;
public SC_RoleLeaveRoom(){}

public SC_RoleLeaveRoom(Int32 _roleId){
this.roleId = _roleId;
}
private Byte[] get_roleId_encoding(){
Byte[] outBuf = null;
outBuf = BitConverter.GetBytes((Int32)roleId);
return outBuf;
}

private int set_roleId_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
roleId = new Int32();
roleId = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(roleId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_roleId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_roleId_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_roleId_json(){
if(roleId==null){return "";}String resultJson = "\"roleId\":";resultJson += "\"";resultJson += roleId.ToString();resultJson += "\"";return resultJson;
}


public void set_roleId_fromJson(LitJson.JsonData jsonObj){
roleId= Int32.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(roleId !=  null){
resultStr += get_roleId_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["roleId"] != null){
set_roleId_fromJson(jsonObj["roleId"]);
}
}
}
}