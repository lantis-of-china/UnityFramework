// 此文件由协议导出插件自动生成
// ID : 00000]
//****游戏中的角色信息****
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
///游戏中的角色信息
/// <\summary>
public class P_GamePlayerInfo : CherishBitProtocolBase {
public P_GamePlayerInfo(){}

public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
return startOffset;}
public override String SerializerJson(){
String resultStr = "{";resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
}
}
}
