// 此文件由协议导出插件自动生成
// ID : 00000]
//****生成道具****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;
using SingleMoba;


namespace SingleMoba{
/// <summary>
///生成道具
/// <\summary>
public class SC_SpawnProps : LantisBitProtocolBase {
/// <summary>
///
/// <\summary>
public List<P_Prop> props;
public SC_SpawnProps(){}

public SC_SpawnProps(List<P_Prop> _props){
this.props = _props;
}
private Byte[] get_props_encoding(){
Byte[] outBuf = null;
using(MemoryStream memoryWrite = new MemoryStream()){
List<P_Prop> listBase = props;
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

private int set_props_fromBuf(Byte[] sourceBuf,int curIndex){
Byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
props = new List<P_Prop>();
int listCount = BitConverter.ToInt32(sourceBuf,curIndex);
curIndex += 4;
for(int index = 0;index < listCount;++index){
P_Prop curTarget = new P_Prop();
curIndex = curTarget.Deserializer(sourceBuf,curIndex);
props.Add(curTarget);
}
}return curIndex;
}
public override Byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
Byte[] byteBuf = null;
if(props !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_props_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}Byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(Byte[] sourceBuf,int startOffset){
startOffset = set_props_fromBuf(sourceBuf,startOffset);
return startOffset;}

public String get_props_json(){
if(props==null){return "";}String resultJson = "\"props\":";resultJson += "[";
List<P_Prop> listObj = (List<P_Prop>)props;
for(int i = 0;i < listObj.Count;++i){
P_Prop item = listObj[i];
if(i > 0){ resultJson += ","; }resultJson += item.SerializerJson();
}
resultJson += "]";
return resultJson;
}


public void set_props_fromJson(LitJson.JsonData jsonObj){
props = new List<P_Prop>();
foreach (LitJson.JsonData item in jsonObj){
P_Prop addB = new P_Prop();
props.Add(addB);
addB.DeserializerJson(item.ToJson());
}

}

public override String SerializerJson(){
String resultStr = "{";if(props !=  null){
resultStr += get_props_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(String json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["props"] != null){
set_props_fromJson(jsonObj["props"]);
}
}
}
}
