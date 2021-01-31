// 此文件由协议导出插件自动生成
// ID : 00000]
//****游戏中的角色信息****
using System;
using System.Collections.Generic;
using System.IO;


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
