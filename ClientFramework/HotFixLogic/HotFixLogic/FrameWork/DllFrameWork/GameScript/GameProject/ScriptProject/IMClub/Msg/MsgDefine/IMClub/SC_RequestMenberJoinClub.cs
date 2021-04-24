// 此文件由协议导出插件自动生成
// ID : 00001]
//****���������ֲ�SC_RequestMenberJoinClub_MsgType = 20040****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using IMClub;
using Server;


namespace IMClub{
/// <summary>
///���������ֲ�SC_RequestMenberJoinClub_MsgType = 20040
/// <\summary>
public class SC_RequestMenberJoinClub : LantisBitProtocolBase {
/// <summary>
///��� 0ʧ�� 1�ɹ�
/// <\summary>
public Byte result;
public SC_RequestMenberJoinClub(){}

public SC_RequestMenberJoinClub(Byte _result){
this.result = _result;
}
private Byte[] get_result_encoding(){
Byte[] outBuf = null;
outBuf = new Byte[1];
outBuf[0] =(Byte)result;
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
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(result !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_result_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_result_json(){
if(result==null){return "";}String resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= Byte.Parse(jsonObj.ToString());
}

public override String SerializerJson(){
String resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
}
}
}
