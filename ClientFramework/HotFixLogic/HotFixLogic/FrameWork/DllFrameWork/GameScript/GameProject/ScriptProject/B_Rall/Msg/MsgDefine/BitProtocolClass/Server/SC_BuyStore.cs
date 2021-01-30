// 此文件由协议导出插件自动生成
// ID : 00001]
//****购买商品****
using System;
using System.Collections.Generic;
using System.IO;
using BaseDataAttribute;
using Server;


namespace Server{
/// <summary>
///购买商品
/// <\summary>
public class SC_BuyStore : CherishBitProtocolBase {
/// <summary>
///0失败 1成功
/// <\summary>
public byte result;
/// <summary>
///订单ID
/// <\summary>
public string orderId;
/// <summary>
///支付价格
/// <\summary>
public string price;
/// <summary>
///商品ID
/// <\summary>
public string id;
/// <summary>
///商户Key支付
/// <\summary>
public string payKey;
/// <summary>
///商户Id
/// <\summary>
public string payUserId;
/// <summary>
///支付类型
/// <\summary>
public string payType;
/// <summary>
///支付路径
/// <\summary>
public string payUrl;
/// <summary>
///异步通知地址
/// <\summary>
public string url;
/// <summary>
///同步通知地址
/// <\summary>
public string hrefurl;
public SC_BuyStore(){}

public SC_BuyStore(byte _result, string _orderId, string _price, string _id, string _payKey, string _payUserId, string _payType, string _payUrl, string _url, string _hrefurl){
this.result = _result;
this.orderId = _orderId;
this.price = _price;
this.id = _id;
this.payKey = _payKey;
this.payUserId = _payUserId;
this.payType = _payType;
this.payUrl = _payUrl;
this.url = _url;
this.hrefurl = _hrefurl;
}
private byte[] get_result_encoding(){
byte[] outBuf = null;
outBuf = new byte[1];
outBuf[0] =(byte)result;
return outBuf;
}


private byte[] get_orderId_encoding(){
byte[] outBuf = null;
string str = (string)orderId;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_price_encoding(){
byte[] outBuf = null;
string str = (string)price;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_id_encoding(){
byte[] outBuf = null;
string str = (string)id;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_payKey_encoding(){
byte[] outBuf = null;
string str = (string)payKey;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_payUserId_encoding(){
byte[] outBuf = null;
string str = (string)payUserId;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_payType_encoding(){
byte[] outBuf = null;
string str = (string)payType;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_payUrl_encoding(){
byte[] outBuf = null;
string str = (string)payUrl;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_url_encoding(){
byte[] outBuf = null;
string str = (string)url;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}


private byte[] get_hrefurl_encoding(){
byte[] outBuf = null;
string str = (string)hrefurl;
Char[] charArray = str.ToCharArray();
byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);
Int32 length = strBuf.Length;
byte[] bufLenght = BitConverter.GetBytes(length);
using(MemoryStream desStream = new MemoryStream()){
desStream.Write(bufLenght, 0, bufLenght.Length);
desStream.Write(strBuf, 0, strBuf.Length);
outBuf = desStream.ToArray();
}
return outBuf;
}

private int set_result_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
result = new byte();
result = sourceBuf[curIndex];
curIndex++;
}return curIndex;
}
private int set_orderId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
orderId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
orderId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_price_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
price = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
price = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_id_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
id = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
id = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_payKey_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
payKey = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
payKey = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_payUserId_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
payUserId = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
payUserId = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_payType_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
payType = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
payType = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_payUrl_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
payUrl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
payUrl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_url_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
url = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
url = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
private int set_hrefurl_fromBuf(byte[] sourceBuf,int curIndex){
byte tag = sourceBuf[curIndex];
curIndex += 1;
if(tag != 0){;
hrefurl = "";
int strLength = BitConverter.ToInt32(sourceBuf, curIndex);
curIndex += 4;
Byte[] byteArray = new Byte[strLength];
for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){
byteArray[loopStrByte] = sourceBuf[curIndex];
curIndex++;
}
hrefurl = System.Text.Encoding.UTF8.GetString(byteArray);
}return curIndex;
}
public override byte[] Serializer(){
MemoryStream memoryWrite = new MemoryStream();
byte[] byteBuf = null;
if(result !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_result_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(orderId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_orderId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(price !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_price_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(id !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_id_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(payKey !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_payKey_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(payUserId !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_payUserId_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(payType !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_payType_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(payUrl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_payUrl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(url !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_url_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}if(hrefurl !=  null){
memoryWrite.WriteByte(1);
byteBuf = get_hrefurl_encoding();
memoryWrite.Write(byteBuf,0,byteBuf.Length);
}
else {memoryWrite.WriteByte(0);
}byte[] bufResult = memoryWrite.ToArray();memoryWrite.Dispose();
return bufResult;
}

public override int Deserializer(byte[] sourceBuf,int startOffset){
startOffset = set_result_fromBuf(sourceBuf,startOffset);
startOffset = set_orderId_fromBuf(sourceBuf,startOffset);
startOffset = set_price_fromBuf(sourceBuf,startOffset);
startOffset = set_id_fromBuf(sourceBuf,startOffset);
startOffset = set_payKey_fromBuf(sourceBuf,startOffset);
startOffset = set_payUserId_fromBuf(sourceBuf,startOffset);
startOffset = set_payType_fromBuf(sourceBuf,startOffset);
startOffset = set_payUrl_fromBuf(sourceBuf,startOffset);
startOffset = set_url_fromBuf(sourceBuf,startOffset);
startOffset = set_hrefurl_fromBuf(sourceBuf,startOffset);
return startOffset;}

public string get_result_json(){
if(result==null){return "";}string resultJson = "\"result\":";resultJson += "\"";resultJson += result.ToString();resultJson += "\"";return resultJson;
}


public string get_orderId_json(){
if(orderId==null){return "";}string resultJson = "\"orderId\":";resultJson += "\"";resultJson += orderId.ToString();resultJson += "\"";return resultJson;
}


public string get_price_json(){
if(price==null){return "";}string resultJson = "\"price\":";resultJson += "\"";resultJson += price.ToString();resultJson += "\"";return resultJson;
}


public string get_id_json(){
if(id==null){return "";}string resultJson = "\"id\":";resultJson += "\"";resultJson += id.ToString();resultJson += "\"";return resultJson;
}


public string get_payKey_json(){
if(payKey==null){return "";}string resultJson = "\"payKey\":";resultJson += "\"";resultJson += payKey.ToString();resultJson += "\"";return resultJson;
}


public string get_payUserId_json(){
if(payUserId==null){return "";}string resultJson = "\"payUserId\":";resultJson += "\"";resultJson += payUserId.ToString();resultJson += "\"";return resultJson;
}


public string get_payType_json(){
if(payType==null){return "";}string resultJson = "\"payType\":";resultJson += "\"";resultJson += payType.ToString();resultJson += "\"";return resultJson;
}


public string get_payUrl_json(){
if(payUrl==null){return "";}string resultJson = "\"payUrl\":";resultJson += "\"";resultJson += payUrl.ToString();resultJson += "\"";return resultJson;
}


public string get_url_json(){
if(url==null){return "";}string resultJson = "\"url\":";resultJson += "\"";resultJson += url.ToString();resultJson += "\"";return resultJson;
}


public string get_hrefurl_json(){
if(hrefurl==null){return "";}string resultJson = "\"hrefurl\":";resultJson += "\"";resultJson += hrefurl.ToString();resultJson += "\"";return resultJson;
}


public void set_result_fromJson(LitJson.JsonData jsonObj){
result= byte.Parse(jsonObj.ToString());
}


public void set_orderId_fromJson(LitJson.JsonData jsonObj){
orderId= jsonObj.ToString();
}


public void set_price_fromJson(LitJson.JsonData jsonObj){
price= jsonObj.ToString();
}


public void set_id_fromJson(LitJson.JsonData jsonObj){
id= jsonObj.ToString();
}


public void set_payKey_fromJson(LitJson.JsonData jsonObj){
payKey= jsonObj.ToString();
}


public void set_payUserId_fromJson(LitJson.JsonData jsonObj){
payUserId= jsonObj.ToString();
}


public void set_payType_fromJson(LitJson.JsonData jsonObj){
payType= jsonObj.ToString();
}


public void set_payUrl_fromJson(LitJson.JsonData jsonObj){
payUrl= jsonObj.ToString();
}


public void set_url_fromJson(LitJson.JsonData jsonObj){
url= jsonObj.ToString();
}


public void set_hrefurl_fromJson(LitJson.JsonData jsonObj){
hrefurl= jsonObj.ToString();
}

public override string SerializerJson(){
string resultStr = "{";if(result !=  null){
resultStr += get_result_json();
}
else {}if(orderId !=  null){
resultStr += ",";resultStr += get_orderId_json();
}
else {}if(price !=  null){
resultStr += ",";resultStr += get_price_json();
}
else {}if(id !=  null){
resultStr += ",";resultStr += get_id_json();
}
else {}if(payKey !=  null){
resultStr += ",";resultStr += get_payKey_json();
}
else {}if(payUserId !=  null){
resultStr += ",";resultStr += get_payUserId_json();
}
else {}if(payType !=  null){
resultStr += ",";resultStr += get_payType_json();
}
else {}if(payUrl !=  null){
resultStr += ",";resultStr += get_payUrl_json();
}
else {}if(url !=  null){
resultStr += ",";resultStr += get_url_json();
}
else {}if(hrefurl !=  null){
resultStr += ",";resultStr += get_hrefurl_json();
}
else {}resultStr += "}";return resultStr;
}

public override void DeserializerJson(string json){
LitJson.JsonData jsonObj = CSTools.JsonToData(json);
if(jsonObj["result"] != null){
set_result_fromJson(jsonObj["result"]);
}
if(jsonObj["orderId"] != null){
set_orderId_fromJson(jsonObj["orderId"]);
}
if(jsonObj["price"] != null){
set_price_fromJson(jsonObj["price"]);
}
if(jsonObj["id"] != null){
set_id_fromJson(jsonObj["id"]);
}
if(jsonObj["payKey"] != null){
set_payKey_fromJson(jsonObj["payKey"]);
}
if(jsonObj["payUserId"] != null){
set_payUserId_fromJson(jsonObj["payUserId"]);
}
if(jsonObj["payType"] != null){
set_payType_fromJson(jsonObj["payType"]);
}
if(jsonObj["payUrl"] != null){
set_payUrl_fromJson(jsonObj["payUrl"]);
}
if(jsonObj["url"] != null){
set_url_fromJson(jsonObj["url"]);
}
if(jsonObj["hrefurl"] != null){
set_hrefurl_fromJson(jsonObj["hrefurl"]);
}
}
}
}
