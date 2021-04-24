// 此文件由协议导出插件自动生成
// ID : 
//**** 基类 ****
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class LantisBitProtocolBase{
public virtual Byte[] Serializer(){return null;
}

public virtual int Deserializer(Byte[] sourceBuf,int startOffset){
return startOffset;
}
public virtual String SerializerJson(){return "";
}
public virtual void DeserializerJson(String json){
}
}

