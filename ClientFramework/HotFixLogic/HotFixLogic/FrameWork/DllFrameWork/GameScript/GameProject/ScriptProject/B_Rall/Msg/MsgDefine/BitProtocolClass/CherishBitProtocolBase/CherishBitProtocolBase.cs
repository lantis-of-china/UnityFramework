// 此文件由协议导出插件自动生成
// ID : 
//**** 基类 ****
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class CherishBitProtocolBase{
public virtual byte[] Serializer(){return null;
}

public virtual int Deserializer(byte[] sourceBuf,int startOffset){
return startOffset;
}
public virtual string SerializerJson(){return "";
}
public virtual void DeserializerJson(string json){
}
}

