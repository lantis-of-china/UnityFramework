// 此文件由协议导出插件自动生成
// ID : 00004]
  
//****用户注册返回****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///用户注册返回
/// <\summary>
public class MessageRegistBack : CherishBitProtocolBase {
public MessageRegistBack(){}

public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
return startOffset;}
public override string SerializerJson(){
string resultStr = "{";resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
}
}
}
