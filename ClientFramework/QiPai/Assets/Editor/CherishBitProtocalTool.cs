/*
'***********************************************
'类 名 称 : CYErlangProtoToLuaDynamicData
'命名空间 : Assets.framework.Editor
'创建时间 : 2015/5/18 9:37:00
'作    者 : Cherish
'修改时间 :
'修 改 人 : 
'版 本 号 : v1.0.0
'*******Copyright (c) 2015, Cherish工作室********
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;

public class CherishBitProtocalTool
{
	public class ProtocalExportProjectPath
	{
		public string protoNameSpace;
		public string clientProjectPath;
		public string serverProjectPath;

		public static List<ProtocalExportProjectPath> exportProjectPathMap = new List<ProtocalExportProjectPath>()
		{
			new ProtocalExportProjectPath
			{
				protoNameSpace = "SingleMoba",
				clientProjectPath = "SingleMoba/Msg/MsgDefine",
				serverProjectPath = "SingleMoba/Msg/MsgDefine",
			}
		};
	}

	/// <summary>
	/// 字段定义
	/// </summary>
	public class FieldDefine
	{
		/// <summary>
		/// 注释
		/// </summary>
		public string vlaueDesc = "";
		/// <summary>
		/// 字段值
		/// </summary>
		public string valueName;
		/// <summary>
		/// 字段类型
		/// </summary>
		public string valueType;
		/// <summary>
		/// 数组
		/// </summary>
		public bool isArray;
	}

	/// <summary>
	/// 类的定义
	/// </summary>
	public class ClassDefine
	{
		/// <summary>
		/// 文件名
		/// </summary>
		public string dirName;
		/// <summary>
		/// 类名
		/// </summary>
		public string className;
		/// <summary>
		/// 类ID
		/// </summary>
		public string classId;
		/// <summary>
		/// 类型
		/// 0 s2c 1 c2s 2 p
		/// </summary>
		public int classType = 0;
		/// <summary>
		/// 注释
		/// </summary>
		public string classDesc = "";
		/// <summary>
		/// 属性
		/// </summary>
		public List<FieldDefine> valus = new List<FieldDefine>();
	}

	List<ClassDefine> mProtoClasses = new List<ClassDefine>();


	/// <summary>
	/// 清理路径
	/// </summary>
	[MenuItem("CherishBitProtocalTool/Tools/ClearPath")]	
	public static void ClearPath()
	{
		PlayerPrefs.DeleteKey("ProtoKey");
		PlayerPrefs.DeleteKey("ExportKey");
		PlayerPrefs.DeleteKey("ClientKey");
		PlayerPrefs.DeleteKey("ServerKey");
	}

	/// <summary>
	/// 导出C#类到目录
	/// </summary>
	/// <param name="dir"></param>
	/// <param name="needExportCall"></param>
	[MenuItem("CherishBitProtocalTool/ProtocalToClass")]
	public static void ProtocalToCSharp()
	{
		string dir = "";

		if (PlayerPrefs.HasKey("ProtoKey"))
		{
			dir = PlayerPrefs.GetString("ProtoKey");
		}
		else
		{
			// 首先询问选择文件夹
			dir = EditorUtility.OpenFolderPanel("选择你想要导出的协议文件夹", Application.dataPath, "");

			if (!string.IsNullOrEmpty(dir))
			{
				dir = dir.Replace("\\", "/");
				PlayerPrefs.SetString("ProtoKey",dir);
			}
		}

		if (string.IsNullOrEmpty(dir))
		{
			Debug.LogError("选择的协议目录路径为空,无法执行导出!");
			return;
		}

		CherishBitProtocalTool data = new CherishBitProtocalTool();
		string[] globalDirEntries = Directory.GetFiles(dir, "*.proto", SearchOption.AllDirectories);
		foreach (string fileName in globalDirEntries)
		{
			Debug.LogWarning(fileName);
			data.AnalyzeFile(fileName);
		}

		string savedir = "";

		if (PlayerPrefs.HasKey("ExportKey"))
		{
			savedir = PlayerPrefs.GetString("ExportKey");
		}
		else
		{
			// 首先询问选择文件夹
			savedir = EditorUtility.OpenFolderPanel("选择你想要保存的位置", Application.dataPath, "");

			if (!string.IsNullOrEmpty(savedir))
			{
				savedir = savedir.Replace("\\", "/");
				PlayerPrefs.SetString("ExportKey", savedir);
			}
		}

		if (string.IsNullOrEmpty(savedir))
		{
			Debug.LogError("选择的导出目录路径为空,无法执行导出!");
			return;
		}

		data.ExportAllCSharpClass(savedir, false);

		string clientDir = "";

		if (PlayerPrefs.HasKey("ClientKey"))
		{
			clientDir = PlayerPrefs.GetString("ClientKey");
		}
		else
		{
			// 首先询问选择文件夹
			clientDir = EditorUtility.OpenFolderPanel("选择逻辑代码ScriptProject目录", Application.dataPath, "");

			if (!string.IsNullOrEmpty(clientDir))
			{
				clientDir = clientDir.Replace("\\", "/");
				PlayerPrefs.SetString("ClientKey", clientDir);
			}
		}

		string serverDir = "";

		if (PlayerPrefs.HasKey("ServerKey"))
		{
			serverDir = PlayerPrefs.GetString("ServerKey");
		}
		else
		{
			// 首先询问选择文件夹
			serverDir = EditorUtility.OpenFolderPanel("选择逻辑代码GameProject目录", Application.dataPath, "");

			if (!string.IsNullOrEmpty(serverDir))
			{
				serverDir = serverDir.Replace("\\", "/");
				PlayerPrefs.SetString("ServerKey", serverDir);
			}
		}

		Debug.Log($"客户端拷贝目录{clientDir} 服务器拷贝目录{serverDir}");

		for (var i = 0; i < ProtocalExportProjectPath.exportProjectPathMap.Count; ++i)
		{
			var protocalExport = ProtocalExportProjectPath.exportProjectPathMap[i];
			var protoPath = savedir + "/OutCSharp/BitProtocolClass/" + protocalExport.protoNameSpace;

			if (Directory.Exists(protoPath))
			{
				if (!string.IsNullOrEmpty(clientDir))
				{
					var toClient = $"{clientDir}/{protocalExport.clientProjectPath}";

					if (!Directory.Exists(toClient))
					{
						Directory.CreateDirectory(toClient);
					}

					Debug.Log($"拷贝文件夹 从{protoPath} 到{clientDir}");
					EditorTool.CopyDirectory(protoPath, toClient);
				}

				if (!string.IsNullOrEmpty(serverDir))
				{
					var toServer = $"{serverDir}/{protocalExport.serverProjectPath}";

					if (!Directory.Exists(toServer))
					{
						Directory.CreateDirectory(toServer);
					}

					Debug.Log($"拷贝文件夹 从{protoPath} 到{toServer}");
					EditorTool.CopyDirectory(protoPath, toServer);
				}
			}
			else
			{
				Debug.LogError($"不存在文件目录protoPath:{protoPath}");
			}
		}
	}

	/// <summary>
	/// 导出C#类到目录
	/// </summary>
	/// <param name="dir"></param>
	/// <param name="needExportCall"></param>
	[MenuItem("CherishBitProtocalTool/Tools/ToOthers/ProtocalToC++")]
	public static void ProtocalToCPlus()
	{
		// 首先询问选择文件夹
		string dir = EditorUtility.OpenFolderPanel("选择你想要导出的协议文件夹", Application.dataPath, "");

		if (string.IsNullOrEmpty(dir))
			return;

		CherishBitProtocalTool data = new CherishBitProtocalTool();
		string[] globalDirEntries = Directory.GetFiles(dir, "*.proto", SearchOption.AllDirectories);
		foreach (string fileName in globalDirEntries)
		{
			Debug.LogWarning(fileName);
			data.AnalyzeFile(fileName);
		}

		string savedir = EditorUtility.OpenFolderPanel("选择你想要保存的位置", Application.dataPath, "");
		data.ExportAllCPlussPlussClass(savedir);
	}

	/// <summary>
	/// 加载文件到文本
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public string LoadData(string path)
	{
		return File.ReadAllText(path);
	}

	/// <summary>
	/// 在字符串中查找字符位置
	/// </summary>
	/// <param name="s"></param>
	/// <param name="pos"></param>
	/// <param name="c"></param>
	/// <returns></returns>
	int Scan(string s, int pos, char c)
	{
		int begin = pos;
		if (pos >= s.Length)
		{
			return 0;
		}

		while (pos < s.Length)
		{
			if (s[pos] == c)
				return pos - begin;
			pos++;
		}

		return 0;
	}

	/// <summary>
	/// 获取非空字符串
	/// </summary>
	/// <param name="s"></param>
	/// <param name="pos"></param>
	/// <returns></returns>
	string GetNoEmptyStr(string s, ref int pos)
	{
		while (pos < s.Length && (s[pos] == ' ' || s[pos] == '\t'))
			pos++;
		int start = pos;
		while (pos < s.Length && (s[pos] != ' ' && s[pos] != '\t' && s[pos] != '='))
			pos++;
		return s.Substring(start, pos - start).Trim();
	}

	/// <summary>
	/// 解析文件
	/// </summary>
	/// <param name="fileName"></param>
	public void AnalyzeFile(string fileName)
	{
		string str = LoadData(fileName);
		FileInfo info = new FileInfo(fileName);

		ClassDefine cData = null;
		FieldDefine vData = null;

		int begin = 0;
		int end = 0;
		int pos = 0;
		int descPos = 0;
		while (pos < str.Length)
		{
			char c = str[pos];
			if (c == ' ' || c == '\t')
			{
				pos++;
				continue;
			}

			if (cData == null)
			{
				int p = str.IndexOf("message", pos);
				if (p >= 0)
				{
					end = str.IndexOf('}', begin);

					// class注释
					string desc = str.Substring(begin, p - begin);

					cData = new ClassDefine();
					cData.classDesc = desc.Replace("//", "").Trim();
					cData.dirName = info.Name.Split('.')[0];
					pos = p + 7;

					// 先处理classname;
					int cNameLen = Scan(str, pos, '[');
					if (cNameLen > 0)
					{
						cData.className = str.Substring(pos, cNameLen).Trim();

						Debug.LogWarning("className = " + cData.className);
						if (cData.className[0] == 'c')
							cData.classType = 1;
						else if (cData.className[0] == 'p')
							cData.classType = 2;
						else
							cData.classType = 0;
						// 处理id
						pos += cNameLen;

						int l = Scan(str, pos, '=') + 1;
						int e = Scan(str, pos, '{') - 1;


						string ccid = str.Substring(pos + l, e - l);

						// 有可能有逗号
						cData.classId = ccid.Split(',')[0];

						pos = str.IndexOf('{', pos) - 1;

						int findDescPos = str.IndexOf("//", pos);
						int lastP = str.IndexOf(';', pos);

						if (findDescPos >= 0 && findDescPos < lastP)
						{
							descPos = findDescPos;
						}
						continue;
					}
				}
			}

			if (cData != null && c == '{' || c == ';')
			{
				int endfPos = str.IndexOf(';', pos + 1);
				if (endfPos < 0)
				{
					pos++;
					continue;
				}
				int requirPos = str.IndexOf("required", pos, endfPos - pos);
				int repeatPos = str.IndexOf("repeated", pos, endfPos - pos);

				int finalPos = 0;
				if (requirPos >= 0 && requirPos < end)
				{
					finalPos = requirPos;

				}
				bool isArray = false;
				if (repeatPos >= 0 && repeatPos < end)
				{
					isArray = true;
					finalPos = repeatPos;
				}

				if (finalPos == 0)
				{
					pos++;
					continue;
				}



				vData = new FieldDefine();
				cData.valus.Add(vData);
				if (vData != null && descPos > 0)
				{
					vData.vlaueDesc = str.Substring(descPos, finalPos - descPos).Replace("//", "").Trim();
				}




				finalPos += 8;
				vData.isArray = isArray;
				string typeStr = GetNoEmptyStr(str, ref finalPos);

				vData.valueType = typeStr;
				vData.valueName = GetNoEmptyStr(str, ref finalPos);
				Debug.LogWarning("propertyName = " + vData.valueName);
				int lineEndPos = str.IndexOf(';', finalPos);

				int lastP = str.IndexOf(';', lineEndPos + 1);
				int lastP2 = str.IndexOf('}', lineEndPos + 1);
				if (lastP < 0)
				{
					lastP = lastP2;
				}
				else
				{
					lastP = Math.Min(lastP, lastP2);
				}
				// 注释解析
				descPos = str.IndexOf("//", lineEndPos, lastP - lineEndPos);
			}

			if (c == '}')
			{
				if (vData != null && descPos >= 0)
					vData.vlaueDesc = str.Substring(descPos, pos - descPos).Replace("//", "").Trim();
				descPos = 0;
				mProtoClasses.Add(cData);
				cData = null;
				vData = null;
				begin = pos + 1;
			}

			pos++;
		}

	}


	#region CSharp
	/// <summary>
	/// 导出所有的C# 文件
	/// </summary>
	/// <param name="protocolPath"></param>
	public void ExportAllCSharpClass(string savePath, bool openAttr)
	{
		if (!Directory.Exists(savePath + "/OutCSharp/BitProtocolClass"))
		{
			Directory.CreateDirectory(savePath + "/OutCSharp/BitProtocolClass");
		}


		string bitProtocolBase_str = BuildCharpBitProtocolBase();

		if (!Directory.Exists(savePath + "/OutCSharp/BitProtocolClass/CherishBitProtocolBase"))
		{
			Directory.CreateDirectory(savePath + "/OutCSharp/BitProtocolClass/CherishBitProtocolBase");
		}

		File.WriteAllText(savePath + "/OutCSharp/BitProtocolClass/CherishBitProtocolBase/CherishBitProtocolBase.cs", bitProtocolBase_str, Encoding.UTF8);

		foreach (ClassDefine data in mProtoClasses)
		{
			if (!Directory.Exists(savePath + "/OutCSharp/BitProtocolClass/" + data.dirName))
			{
				Directory.CreateDirectory(savePath + "/OutCSharp/BitProtocolClass/" + data.dirName);
			}
			Debug.Log("spawn msg out  " + data.className);
			string curClassBody = ExportCSharpClass(data, openAttr);

			File.WriteAllText(savePath + "/OutCSharp/BitProtocolClass/" + data.dirName + "/" + data.className + ".cs", curClassBody, Encoding.UTF8);
		}
	}

	/// <summary>
	/// 构建工具类型
	/// </summary>
	/// <returns></returns>
	public string BuildCSharpBitProtocolTool()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ");
		sb.AppendLine("//****" + "CherishBitProtocolTool" + "****");

		#region 名字空间
		sb.AppendLine("using System;");
		sb.AppendLine("using System.Collections.Generic;");
		sb.AppendLine("using System.IO;");
		#endregion 名字空间

		sb.AppendLine("\n");

		///类定义
		string classDefine_str = "";
		classDefine_str = "public class CherishBitProtocolTool{\n";
		
		#region 获取从字段 字节数组
		classDefine_str += "static public Byte[] get_CherishBitProtobuf(object valueObject,String type){\n";
		classDefine_str += "Byte[] outBuf = null;\n";
		classDefine_str += "switch (type){\n";
		classDefine_str += "case \"SByte\":\n";
		classDefine_str += "outBuf = new Byte[1];\n";
		classDefine_str += "outBuf[0] = (Byte)((SByte)valueObject + 128);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Int16\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((short)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Int32\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((Int32)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Int64\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((Int64)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Byte\":\n";
		classDefine_str += "outBuf = new Byte[1];\n";
		classDefine_str += "outBuf[0] =(Byte)valueObject;\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"UInt16\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((UInt16)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"UInt32\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((UInt32)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"UInt64\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((UInt64)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Single\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((Single)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Double\":\n";
		classDefine_str += "outBuf = BitConverter.GetBytes((Double)valueObject);\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"String\":\n";
		classDefine_str += "String str = (String)valueObject;\n";
		classDefine_str += "Char[] charArray = str.ToCharArray();\n";
		classDefine_str += "Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);\n";
		classDefine_str += "Int32 length = strBuf.Length;\n";
		classDefine_str += "Byte[] bufLenght = BitConverter.GetBytes(length);\n";
		classDefine_str += "using(MemoryStream desStream = new MemoryStream()){\n";
		classDefine_str += "desStream.Write(bufLenght, 0, bufLenght.Length);\n";
		classDefine_str += "desStream.Write(strBuf, 0, strBuf.Length);\n";
		classDefine_str += "outBuf = desStream.ToArray();\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "default :\n";
		classDefine_str += "outBuf = ((CherishBitProtocolBase)valueObject).Serializer();\n";
		classDefine_str += "break;\n";
		classDefine_str += "}\n";
		classDefine_str += "return outBuf;\n";
		classDefine_str += "}\n";
		#endregion 获取从字段字节数组
		classDefine_str += "\n";

		#region 获取从字段数组 字节数组
		classDefine_str += "static public Byte[] get_CherishBitArrayProtobuf(object valueObject,String type){\n";
		classDefine_str += "MemoryStream memoryWrite = new MemoryStream();\n";
		classDefine_str += "switch (type){\n";
		classDefine_str += "case \"SByte\":\n";
		classDefine_str += "List<SByte> listSbyte = (List<SByte>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listSbyte.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listSbyte.Count;++i){\n";
		classDefine_str += "SByte sby = listSbyte[i];\n";
		classDefine_str += "memoryWrite.WriteByte((Byte)(sby + 128));\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Int16\":\n";
		classDefine_str += "List<short> listShort = (List<short>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listShort.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listShort.Count;++i){\n";
		classDefine_str += "short shr = listShort[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(shr),0,2);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Int32\":\n";
		classDefine_str += "List<Int32> listInt32 = (List<Int32>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listInt32.Count;++i){\n";
		classDefine_str += "Int32 in32 = listInt32[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(in32),0,4);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Int64\":\n";
		classDefine_str += "List<Int64> listInt64 = (List<Int64>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listInt64.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listInt64.Count;++i){\n";
		classDefine_str += "Int64 in64 = listInt64[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(in64),0,8);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Byte\":\n";
		classDefine_str += "List<Byte> listbyte = (List<Byte>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listbyte.Count;++i){\n";
		classDefine_str += "Byte by = listbyte[i];\n";
		classDefine_str += "memoryWrite.WriteByte(by);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"UInt16\":\n";
		classDefine_str += "List<ushort> listUShort = (List<ushort>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listUShort.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listUShort.Count;++i){\n";
		classDefine_str += "ushort ushr = listUShort[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(ushr),0,2);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"UInt32\":\n";
		classDefine_str += "List<UInt32> listUInt32 = (List<UInt32>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listUInt32.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listUInt32.Count;++i){\n";
		classDefine_str += "UInt32 uint32 = listUInt32[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(uint32),0,4);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"UInt64\":\n";
		classDefine_str += "List<UInt64> listUInt64 = (List<UInt64>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listUInt64.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listUInt64.Count;++i){\n";
		classDefine_str += "UInt64 uint64 = listUInt64[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(uint64),0,8);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Single\":\n";
		classDefine_str += "List<Single> listFloat = (List<Single>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listFloat.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listFloat.Count;++i){\n";
		classDefine_str += "Single flo = listFloat[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(flo),0,4);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"Double\":\n";
		classDefine_str += "List<Double> listDouble = (List<Double>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listDouble.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listDouble.Count;++i){\n";
		classDefine_str += "Double dou = listDouble[i];\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(dou),0,8);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "case \"String\":\n";
		classDefine_str += "List<String> listString = (List<String>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listString.Count),0,4);\n";
		classDefine_str += "using(MemoryStream desStream = new MemoryStream()){\n";
		classDefine_str += "for(int i = 0;i < listString.Count;++i){\n";
		classDefine_str += "String str = listString[i];\n";
		classDefine_str += "Char[] charArray = str.ToCharArray();\n";
		classDefine_str += "Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);\n";
		classDefine_str += "Int32 length = strBuf.Length;\n";
		classDefine_str += "Byte[] bufLenght = BitConverter.GetBytes(length);\n";
		classDefine_str += "desStream.Write(bufLenght, 0, bufLenght.Length);\n";
		classDefine_str += "desStream.Write(strBuf, 0, strBuf.Length);\n";
		classDefine_str += "Byte[] strBytes = desStream.ToArray();\n";
		classDefine_str += "memoryWrite.Write(strBytes,0,strBytes.Length);\n";
		classDefine_str += "}\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";
		classDefine_str += "default :\n";
		classDefine_str += "List<CherishBitProtocolBase> listBase = (List<CherishBitProtocolBase>)valueObject;\n";
		classDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);\n";
		classDefine_str += "for(int i = 0;i < listBase.Count;++i){\n";
		classDefine_str += "CherishBitProtocolBase baseObject = listBase[i];\n";
		classDefine_str += "Byte[] baseBuf = baseObject.Serializer();\n";
		classDefine_str += "memoryWrite.Write(baseBuf,0,baseBuf.Length);\n";
		classDefine_str += "}\n";
		classDefine_str += "break;\n";


		classDefine_str += "}\n";
		classDefine_str += "Byte[] bufResult = memoryWrite.ToArray();";
		classDefine_str += "memoryWrite.Dispose();\n";
		classDefine_str += "return bufResult;\n";
		classDefine_str += "}\n";
		#endregion 获取字节从数组字段

		classDefine_str += "\n";

		classDefine_str += "}";

		sb.AppendLine(classDefine_str);

		return sb.ToString();
	}

	/// <summary>
	/// 构建基类
	/// </summary>
	/// <returns></returns>
	public string BuildCharpBitProtocolBase()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ID : ");
		sb.AppendLine("//**** 基类 ****");

		#region 名字空间
		sb.AppendLine("using System;");
		sb.AppendLine("using System.Collections.Generic;");
		sb.AppendLine("using System.IO;");
		sb.AppendLine("using System.Text;");
		#endregion 名字空间

		sb.AppendLine("\n");
		///类定义      
		string classDefine_str = "public class CherishBitProtocolBase{\n";

		classDefine_str += "public virtual Byte[] Serializer(){";
		classDefine_str += "return null;\n";
		classDefine_str += "}\n";

		classDefine_str += "\n";

		classDefine_str += "public virtual int Deserializer(Byte[] sourceBuf,int startOffset){\n";
		classDefine_str += "return startOffset;\n";
		classDefine_str += "}\n";

		classDefine_str += "public virtual String SerializerJson(){";
		classDefine_str += "return \"\";\n";
		classDefine_str += "}\n";

		classDefine_str += "public virtual void DeserializerJson(String json){\n";
		classDefine_str += "}\n";

		classDefine_str += "}\n";

		sb.AppendLine(classDefine_str);

		return sb.ToString();
	}

	/// <summary>
	/// 导出C#类 文件
	/// </summary>
	public string ExportCSharpClass(ClassDefine data, bool openAttrbute)
	{
		/////////////类的注释
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ID : " + data.classId);
		sb.AppendLine("//****" + data.classDesc + "****");

		#region 名字空间
		sb.AppendLine("using System;");
		sb.AppendLine("using System.Collections.Generic;");
		sb.AppendLine("using System.IO;");
		if (openAttrbute)
		{
			sb.AppendLine("using UnityEngine;");
		}

		List<string> nameSpaceList = new List<string>();
		for (int i = 0; i < mProtoClasses.Count; ++i)
		{
			ClassDefine classDif = mProtoClasses[i];
			bool isHas = false;
			for (int ni = 0; ni < nameSpaceList.Count; ++ni)
			{
				if (nameSpaceList[ni] == classDif.dirName)
				{
					isHas = true;
					break;
				}
			}
			if (!isHas)
			{
				nameSpaceList.Add(classDif.dirName);
			}
		}

		for (int ni = 0; ni < nameSpaceList.Count; ++ni)
		{
			sb.AppendLine("using " + nameSpaceList[ni] + ";");
		}

		#endregion 名字空间
		sb.AppendLine("\n");


		sb.AppendLine("namespace " + data.dirName + "{");


		///类定义
		string classDefine_str = "";
		///字段定义
		string fieldDifine_str = "";
		///构造函数定义
		string createrFunDefine_str = "";
		///编码方法定义
		string encodinFunDefine_str = "";
		///还原字段方法
		string unencodingFunDefine_str = "";
		///序列化类型
		string encodingAllFunDefine_str = "";
		///反序列化
		string unencodingAllFunDefine_str = "";

		///Json序列化方法
		string encodingJsonFunDefine_str = "";
		///反序列化方法
		string unencodingJsonFunDefine_str = "";

		///Json序列化类
		string encodingJsonAllFunDefine_str = "";
		///Json反序列化类
		string unencodingJsonAllFunDefine_str = "";


		classDefine_str += "/// <summary>\n";
		classDefine_str += "///" + data.classDesc + "\n";
		classDefine_str += "/// <\\summary>\n";
		if (openAttrbute)
		{
			classDefine_str += "[Serializable]\n";
		}
		classDefine_str += "public class " + data.className + " : CherishBitProtocolBase {\n";

		#region 属性定义
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			fieldDifine_str += "/// <summary>\n";
			fieldDifine_str += "///" + v.vlaueDesc + "\n";
			fieldDifine_str += "/// <\\summary>\n";
			if (openAttrbute)
			{
				fieldDifine_str += "[SerializeField]\n";
			}
			if (v.isArray)
			{
				fieldDifine_str += "public List<" + v.valueType + "> " + v.valueName + ";\n";
			}
			else
			{
				fieldDifine_str += "public " + v.valueType + " " + v.valueName + ";\n";
			}
		}
		#endregion 属性定义

		#region 构造方法
		createrFunDefine_str += "public " + data.className + "(){}\n";
		createrFunDefine_str += "\n";

		if (data.valus.Count > 0)
		{   ///构造方法 方法头
			createrFunDefine_str += "public " + data.className + "(";
			bool isFirst = true;
			foreach (FieldDefine v in data.valus)
			{
				if (!isFirst)
				{
					createrFunDefine_str += ", ";
				}
				if (v.isArray)
				{
					createrFunDefine_str += "List<" + v.valueType + "> _" + v.valueName;
				}
				else
				{
					createrFunDefine_str += v.valueType + " _" + v.valueName;
				}

				isFirst = false;
			}
			createrFunDefine_str += "){\n";

			///构造方法体
			foreach (FieldDefine v in data.valus)
			{
				createrFunDefine_str += "this." + v.valueName + " = _" + v.valueName + ";\n";
			}

			createrFunDefine_str += "}";
		}
		#endregion 构造方法

		#region 获取字节
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			encodinFunDefine_str += "\n";
			encodinFunDefine_str += "private Byte[] get_" + v.valueName + "_encoding(){\n";
			encodinFunDefine_str += "Byte[] outBuf = null;\n";

			if (v.isArray)
			{
				encodinFunDefine_str += "using(MemoryStream memoryWrite = new MemoryStream()){\n";
				if (v.valueType == "SByte")
				{
					encodinFunDefine_str += "List<SByte> listSbyte = (List<SByte>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listSbyte.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listSbyte.Count;++i){\n";
					encodinFunDefine_str += "SByte sby = listSbyte[i];\n";
					encodinFunDefine_str += "memoryWrite.WriteByte((Byte)(sby + 128));\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int16")
				{
					encodinFunDefine_str += "List<short> listShort = (List<short>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listShort.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listShort.Count;++i){\n";
					encodinFunDefine_str += "short shr = listShort[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(shr),0,2);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int32")
				{
					encodinFunDefine_str += "List<Int32> listInt32 = (List<Int32>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listInt32.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listInt32.Count;++i){\n";
					encodinFunDefine_str += "Int32 in32 = listInt32[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(in32),0,4);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int64")
				{
					encodinFunDefine_str += "List<Int64> listInt64 = (List<Int64>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listInt64.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listInt64.Count;++i){\n";
					encodinFunDefine_str += "Int64 in64 = listInt64[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(in64),0,8);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Byte")
				{
					encodinFunDefine_str += "List<Byte> listbyte = (List<Byte>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listbyte.Count),0,4);\n";
					//encodinFunDefine_str += "for(int i = 0;i < listbyte.Count;++i){\n";
					//encodinFunDefine_str += "Byte by = listbyte[i];\n";
					//encodinFunDefine_str += "memoryWrite.WriteByte(by);\n";
					//encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "Byte[] listBuf = listbyte.ToArray();\n";
					encodinFunDefine_str += "memoryWrite.Write(listBuf,0,listBuf.Length);\n";
				}
				else if (v.valueType == "UInt16")
				{
					encodinFunDefine_str += "List<ushort> listUShort = (List<ushort>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listUShort.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listUShort.Count;++i){\n";
					encodinFunDefine_str += "ushort ushr = listUShort[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(ushort),0,2);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt32")
				{
					encodinFunDefine_str += "List<UInt32> listUInt32 = (List<UInt32>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listUInt32.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listUInt32.Count;++i){\n";
					encodinFunDefine_str += "UInt32 uint32 = listUInt32[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(uint32),0,4);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt64")
				{
					encodinFunDefine_str += "List<UInt64> listUInt64 = (List<UInt64>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listUInt64.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listUInt64.Count;++i){\n";
					encodinFunDefine_str += "UInt64 uint64 = listUInt64[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(uint64),0,8);\n";
					encodinFunDefine_str += "}\n";					
				}
				else if (v.valueType == "Single")
				{
					encodinFunDefine_str += "List<Single> listFloat = (List<Single>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listFloat.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listFloat.Count;++i){\n";
					encodinFunDefine_str += "Single flo = listFloat[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(flo),0,4);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Double")
				{
					encodinFunDefine_str += "List<Double> listDouble = (List<Double>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listDouble.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listDouble.Count;++i){\n";
					encodinFunDefine_str += "Double dou = listDouble[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(dou),0,8);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Boolean")
				{
					encodinFunDefine_str += "List<Boolean> listDouble = (List<Boolean>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listDouble.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listDouble.Count;++i){\n";
					encodinFunDefine_str += "Boolean dou = listDouble[i];\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(dou),0,1);\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "String")
				{
					encodinFunDefine_str += "List<String> listString = (List<String>)" + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listString.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listString.Count;++i){\n";
					encodinFunDefine_str += "using(MemoryStream desStream = new MemoryStream()){\n";
					encodinFunDefine_str += "String str = listString[i];\n";
					encodinFunDefine_str += "Char[] charArray = str.ToCharArray();\n";
					encodinFunDefine_str += "Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);\n";
					encodinFunDefine_str += "Int32 length = strBuf.Length;\n";
					encodinFunDefine_str += "Byte[] bufLenght = BitConverter.GetBytes(length);\n";
					encodinFunDefine_str += "desStream.Write(bufLenght, 0, bufLenght.Length);\n";
					encodinFunDefine_str += "desStream.Write(strBuf, 0, strBuf.Length);\n";
					encodinFunDefine_str += "Byte[] strBytes = desStream.ToArray();\n";
					encodinFunDefine_str += "memoryWrite.Write(strBytes,0,strBytes.Length);\n";
					encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "}\n";
				}
				else
				{

					encodinFunDefine_str += "List<" + v.valueType + "> listBase = " + v.valueName + ";\n";
					encodinFunDefine_str += "memoryWrite.Write(BitConverter.GetBytes(listBase.Count),0,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < listBase.Count;++i){\n";
					encodinFunDefine_str += "CherishBitProtocolBase baseObject = listBase[i];\n";
					encodinFunDefine_str += "Byte[] baseBuf = baseObject.Serializer();\n";
					encodinFunDefine_str += "memoryWrite.Write(baseBuf,0,baseBuf.Length);\n";
					encodinFunDefine_str += "}\n";
				}
				encodinFunDefine_str += "outBuf = memoryWrite.ToArray();\n";
				encodinFunDefine_str += "}\n";
			}
			else
			{
				if (v.valueType == "SByte")
				{
					encodinFunDefine_str += "outBuf = new Byte[1];\n";
					encodinFunDefine_str += "outBuf[0] = (Byte)((SByte)" + v.valueName + " + 128);\n";
				}
				else if (v.valueType == "Int16")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((short)" + v.valueName + ");\n";
				}
				else if (v.valueType == "Int32")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((Int32)" + v.valueName + ");\n";
				}
				else if (v.valueType == "Int64")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((Int64)" + v.valueName + ");\n";
				}
				else if (v.valueType == "Byte")
				{
					encodinFunDefine_str += "outBuf = new Byte[1];\n";
					encodinFunDefine_str += "outBuf[0] =(Byte)" + v.valueName + ";\n";
				}
				else if (v.valueType == "UInt16")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((UInt16)" + v.valueName + ");\n";
				}
				else if (v.valueType == "UInt32")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((UInt32)" + v.valueName + ");\n";
				}
				else if (v.valueType == "UInt64")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((UInt64)" + v.valueName + ");\n";
				}
				else if (v.valueType == "Single")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((Single)" + v.valueName + ");\n";
				}
				else if (v.valueType == "Double")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((Double)" + v.valueName + ");\n";
				}
				else if (v.valueType == "Boolean")
				{
					encodinFunDefine_str += "outBuf = BitConverter.GetBytes((Boolean)" + v.valueName + ");\n";
				}
				else if (v.valueType == "String")
				{
					encodinFunDefine_str += "String str = (String)" + v.valueName + ";\n";
					encodinFunDefine_str += "Char[] charArray = str.ToCharArray();\n";
					encodinFunDefine_str += "Byte[] strBuf = System.Text.UTF8Encoding.UTF8.GetBytes(charArray,0,charArray.Length);\n";
					encodinFunDefine_str += "Int32 length = strBuf.Length;\n";
					encodinFunDefine_str += "Byte[] bufLenght = BitConverter.GetBytes(length);\n";
					encodinFunDefine_str += "using(MemoryStream desStream = new MemoryStream()){\n";
					encodinFunDefine_str += "desStream.Write(bufLenght, 0, bufLenght.Length);\n";
					encodinFunDefine_str += "desStream.Write(strBuf, 0, strBuf.Length);\n";
					encodinFunDefine_str += "outBuf = desStream.ToArray();\n";
					encodinFunDefine_str += "}\n";
				}
				else
				{

					encodinFunDefine_str += "outBuf = ((CherishBitProtocolBase)" + v.valueName + ").Serializer();\n";
				}
			}

			encodinFunDefine_str += "return outBuf;\n";
			encodinFunDefine_str += "}\n";
			encodinFunDefine_str += "\n";
		}
		#endregion 获取字节

		#region 通过字节还原字段
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			///返回使用的字节数量
			unencodingFunDefine_str += "private int set_" + v.valueName + "_fromBuf(Byte[] sourceBuf,int curIndex){\n";
			unencodingFunDefine_str += "Byte tag = sourceBuf[curIndex];\n";
			unencodingFunDefine_str += "curIndex += 1;\n";
			unencodingFunDefine_str += "if(tag != 0){;\n";
			if (v.isArray)
			{
				unencodingFunDefine_str += v.valueName + " = new List<" + v.valueType + ">();\n";

				unencodingFunDefine_str += "int listCount = BitConverter.ToInt32(sourceBuf,curIndex);\n";
				unencodingFunDefine_str += "curIndex += 4;\n";
				if (v.valueType == "SByte")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = (SByte)(sourceBuf[curIndex] - 128);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int16")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToInt16(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int32")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToInt32(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int64")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToInt64(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Byte")
				{
					//unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					//unencodingFunDefine_str += v.valueType + " curTarget = sourceBuf[curIndex];\n";
					//unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					//unencodingFunDefine_str += "curIndex ++;\n";
					//unencodingFunDefine_str += "}\n";
					unencodingFunDefine_str += "Byte[] data = new Byte[listCount];\n";
					unencodingFunDefine_str += "Buffer.BlockCopy(sourceBuf, curIndex, data, 0, listCount);\n";
					unencodingFunDefine_str += v.valueName + " = new List<Byte>(data);\n";
					unencodingFunDefine_str += "curIndex += listCount;\n";
				}
				else if (v.valueType == "UInt16")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToUInt16(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt32")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToUInt32(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt64")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToUInt64(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Single")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToSingle(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Double")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToDouble(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Boolean")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = BitConverter.ToBoolean(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 1;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "String")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = \"\";\n";
					unencodingFunDefine_str += "int strLength = BitConverter.ToInt32(sourceBuf, curIndex);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "Byte[] byteArray = new Byte[strLength];\n";
					unencodingFunDefine_str += "for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){\n";
					unencodingFunDefine_str += "byteArray[loopStrByte] = sourceBuf[curIndex];\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
					unencodingFunDefine_str += "curTarget = System.Text.Encoding.UTF8.GetString(byteArray);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "}\n";
				}
				else
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = new " + v.valueType + "();\n";
					unencodingFunDefine_str += "curIndex = curTarget.Deserializer(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += v.valueName + ".Add(curTarget);\n";
					unencodingFunDefine_str += "}\n";
				}
			}
			else
			{
				if (v.valueType == "SByte")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = (SByte)(sourceBuf[curIndex] - 128);\n";
					unencodingFunDefine_str += "curIndex++;\n";
				}
				else if (v.valueType == "Int16")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToInt16(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
				}
				else if (v.valueType == "Int32")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToInt32(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
				}
				else if (v.valueType == "Int64")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToInt64(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
				}
				else if (v.valueType == "Byte")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = sourceBuf[curIndex];\n";
					unencodingFunDefine_str += "curIndex++;\n";
				}
				else if (v.valueType == "UInt16")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToUInt16(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
				}
				else if (v.valueType == "UInt32")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToUInt32(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
				}
				else if (v.valueType == "uInt64")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToUInt64(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
				}
				else if (v.valueType == "Single")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToSingle(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
				}
				else if (v.valueType == "Double")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToDouble(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
				}
				else if (v.valueType == "Boolean")
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += v.valueName + " = BitConverter.ToBoolean(sourceBuf,curIndex);\n";
					unencodingFunDefine_str += "curIndex += 1;\n";
				}
				else if (v.valueType == "String")
				{
					unencodingFunDefine_str += v.valueName + " = \"\";\n";
					unencodingFunDefine_str += "int strLength = BitConverter.ToInt32(sourceBuf, curIndex);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "Byte[] byteArray = new Byte[strLength];\n";
					unencodingFunDefine_str += "for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){\n";
					unencodingFunDefine_str += "byteArray[loopStrByte] = sourceBuf[curIndex];\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
					unencodingFunDefine_str += v.valueName + " = System.Text.Encoding.UTF8.GetString(byteArray);\n";
				}
				else
				{
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += "curIndex = " + v.valueName + ".Deserializer(sourceBuf,curIndex);\n";
				}
			}
			unencodingFunDefine_str += "}";
			unencodingFunDefine_str += "return curIndex;\n";
			unencodingFunDefine_str += "}";
			unencodingFunDefine_str += "\n";
		}
		#endregion 通过字节还原字段

		#region 序列化类型
		encodingAllFunDefine_str = "public override Byte[] Serializer(){\n";
		encodingAllFunDefine_str += "MemoryStream memoryWrite = new MemoryStream();\n";
		encodingAllFunDefine_str += "Byte[] byteBuf = null;\n";
		foreach (FieldDefine v in data.valus)
		{
			encodingAllFunDefine_str += "if(" + v.valueName + " !=  null){\n";
			///写入标志为 1 非空
			encodingAllFunDefine_str += "memoryWrite.WriteByte(1);\n";
			encodingAllFunDefine_str += "byteBuf = get_" + v.valueName + "_encoding();\n";
			encodingAllFunDefine_str += "memoryWrite.Write(byteBuf,0,byteBuf.Length);\n";
			encodingAllFunDefine_str += "}\n";
			encodingAllFunDefine_str += "else {";
			///写入标志为 0 空
			encodingAllFunDefine_str += "memoryWrite.WriteByte(0);\n";
			encodingAllFunDefine_str += "}";

		}
		encodingAllFunDefine_str += "Byte[] bufResult = memoryWrite.ToArray();";
		encodingAllFunDefine_str += "memoryWrite.Dispose();\n";
		encodingAllFunDefine_str += "return bufResult;\n";
		encodingAllFunDefine_str += "}\n";
		encodingAllFunDefine_str += "\n";
		#endregion 序列化类型

		#region 反序列化类型
		unencodingAllFunDefine_str = "public override int Deserializer(Byte[] sourceBuf,int startOffset){\n";
		foreach (FieldDefine v in data.valus)
		{
			unencodingAllFunDefine_str += "startOffset = set_" + v.valueName + "_fromBuf(sourceBuf,startOffset);\n";
		}
		unencodingAllFunDefine_str += "return startOffset;";
		unencodingAllFunDefine_str += "}\n";
		#endregion 反序列化类型




		#region Json序列化
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			encodingJsonFunDefine_str += "\n";
			encodingJsonFunDefine_str += "public String get_" + v.valueName + "_json(){\n";
			encodingJsonFunDefine_str += "if(" + v.valueName + "==null){return \"\";}";
			encodingJsonFunDefine_str += "String resultJson = \"\\\"" + v.valueName + "\\\":\";";
			if (v.isArray)
			{
				if (v.valueType == "SByte")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<SByte> listObj = (List<SByte>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "SByte item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Int16")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Int16> listObj = (List<Int16>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Int16 item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Int32")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Int32> listObj = (List<Int32>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Int32 item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Int64")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Int64> listObj = (List<Int64>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Int64 item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Byte")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Byte> listObj = (List<Byte>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Byte item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "UInt16")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<UInt16> listObj = (List<UInt16>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "UInt16 item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "UInt32")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<UInt32> listObj = (List<UInt32>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "UInt32 item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "UInt64")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<UInt64> listObj = (List<UInt64>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "UInt64 item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Single")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Single> listObj = (List<Single>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Single item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Double")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Double> listObj = (List<Double>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Double item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "Boolean")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<Boolean> listObj = (List<Boolean>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "Boolean item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item.ToString();\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else if (v.valueType == "String")
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";";
					encodingJsonFunDefine_str += "List<String> listObj = (List<String>)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += "String item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += item;\n";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				else
				{
					encodingJsonFunDefine_str += "resultJson += \"[\";\n";
					encodingJsonFunDefine_str += "List<" + v.valueType + "> listObj = (List<" + v.valueType + ">)" + v.valueName + ";\n";
					encodingJsonFunDefine_str += "for(int i = 0;i < listObj.Count;++i){\n";
					encodingJsonFunDefine_str += v.valueType + " item = listObj[i];\n";
					encodingJsonFunDefine_str += "if(i > 0){ resultJson += \",\"; }";
					encodingJsonFunDefine_str += "resultJson += item.SerializerJson();\n";
					encodingJsonFunDefine_str += "}\n";
					encodingJsonFunDefine_str += "resultJson += \"]\";";
				}
				encodingJsonFunDefine_str += "\n";
			}
			else
			{
				if (v.valueType == "SByte")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Int16")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Int32")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Int64")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Byte")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "UInt16")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "UInt32")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "UInt64")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Single")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Double")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "Boolean")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else if (v.valueType == "String")
				{
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
					encodingJsonFunDefine_str += "resultJson += " + v.valueName + ".ToString();";
					encodingJsonFunDefine_str += "resultJson += \"\\\"\";";
				}
				else
				{
					encodingJsonFunDefine_str += "resultJson += ((CherishBitProtocolBase)" + v.valueName + ").SerializerJson();";
				}
			}

			encodingJsonFunDefine_str += "return resultJson;\n";
			encodingJsonFunDefine_str += "}\n";
			encodingJsonFunDefine_str += "\n";
		}
		#endregion Json序列化

		#region Json反序列化
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			unencodingJsonFunDefine_str += "\n";
			unencodingJsonFunDefine_str += "public void set_" + v.valueName + "_fromJson(LitJson.JsonData jsonObj){\n";

			if (v.isArray)
			{
				if (v.valueType == "SByte")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<SByte>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(SByte.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int16")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Int16>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Int16.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int32")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Int32>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Int32.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int64")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Int64>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Int64.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Byte")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Byte>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Byte.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt16")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<UInt16>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(UInt16.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt32")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<UInt32>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(UInt32.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt64")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<UInt64>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(UInt64.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Single")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Single>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Single.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Double")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Double>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Double.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "Boolean")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<Boolean>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(Boolean.Parse(jsonItem.ToString()));";
					unencodingJsonFunDefine_str += "}\n";
				}
				else if (v.valueType == "String")
				{
					unencodingJsonFunDefine_str += v.valueName + "= new List<String>();\n";
					unencodingJsonFunDefine_str += "foreach(LitJson.JsonData jsonItem in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(jsonItem.ToString());";
					unencodingJsonFunDefine_str += "}\n";
				}
				else
				{
					unencodingJsonFunDefine_str += v.valueName + " = new List<" + v.valueType + ">();\n";
					unencodingJsonFunDefine_str += "foreach (LitJson.JsonData item in jsonObj){\n";
					unencodingJsonFunDefine_str += v.valueType + " addB = new " + v.valueType + "();\n";
					unencodingJsonFunDefine_str += v.valueName + ".Add(addB);\n";
					unencodingJsonFunDefine_str += "addB.DeserializerJson(item.ToJson());\n";
					unencodingJsonFunDefine_str += "}\n";
				}

				unencodingJsonFunDefine_str += "\n";
			}
			else
			{
				if (v.valueType == "SByte")
				{
					unencodingJsonFunDefine_str += v.valueName + "= SByte.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Int16")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Int16.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Int32")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Int32.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Int64")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Int64.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Byte")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Byte.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "UInt16")
				{
					unencodingJsonFunDefine_str += v.valueName + "= UInt16.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "UInt32")
				{
					unencodingJsonFunDefine_str += v.valueName + "= UInt32.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "UInt64")
				{
					unencodingJsonFunDefine_str += v.valueName + "= UInt64.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Single")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Single.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Double")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Double.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "Boolean")
				{
					unencodingJsonFunDefine_str += v.valueName + "= Boolean.Parse(jsonObj.ToString());\n";
				}
				else if (v.valueType == "String")
				{
					unencodingJsonFunDefine_str += v.valueName + "= jsonObj.ToString();\n";
				}
				else
				{
					unencodingJsonFunDefine_str += v.valueName + "= new " + v.valueType + "();\n";
					unencodingJsonFunDefine_str += v.valueName + ".DeserializerJson(jsonObj.ToJson());";
				}
			}

			unencodingJsonFunDefine_str += "}\n";
			unencodingJsonFunDefine_str += "\n";
		}
		#endregion Json反序列化

		#region Json序列化类型
		encodingJsonAllFunDefine_str = "public override String SerializerJson(){\n";
		encodingJsonAllFunDefine_str += "String resultStr = \"{\";";
		int iCount = 0;
		foreach (FieldDefine v in data.valus)
		{
			encodingJsonAllFunDefine_str += "if(" + v.valueName + " !=  null){\n";
			if (iCount != 0)
			{
				encodingJsonAllFunDefine_str += "resultStr += \",\";";
			}

			encodingJsonAllFunDefine_str += "resultStr += get_" + v.valueName + "_json();\n";
			encodingJsonAllFunDefine_str += "}\n";
			encodingJsonAllFunDefine_str += "else {";
			encodingJsonAllFunDefine_str += "}";

			iCount++;
		}
		encodingJsonAllFunDefine_str += "resultStr += \"}\";";
		encodingJsonAllFunDefine_str += "return resultStr;\n";
		encodingJsonAllFunDefine_str += "}\n";
		encodingJsonAllFunDefine_str += "\n";
		#endregion Json序列化类型

		#region Json反序列化类型
		unencodingJsonAllFunDefine_str = "public override void DeserializerJson(String json){\n";
		unencodingJsonAllFunDefine_str += "LitJson.JsonData jsonObj = CSTools.JsonToData(json);\n";
		foreach (FieldDefine v in data.valus)
		{
			unencodingJsonAllFunDefine_str += "if(jsonObj[\"" + v.valueName + "\"] != null){\n";
			unencodingJsonAllFunDefine_str += "set_" + v.valueName + "_fromJson(" + "jsonObj[\"" + v.valueName + "\"]);\n";
			unencodingJsonAllFunDefine_str += "}\n";
		}
		unencodingJsonAllFunDefine_str += "";
		unencodingJsonAllFunDefine_str += "}\n";
		#endregion Json反序列化类型

		sb.Append(classDefine_str);
		sb.Append(fieldDifine_str);
		sb.Append(createrFunDefine_str);
		sb.Append(encodinFunDefine_str);
		sb.Append(unencodingFunDefine_str);
		sb.Append(encodingAllFunDefine_str);
		sb.Append(unencodingAllFunDefine_str);


		sb.Append(encodingJsonFunDefine_str);
		sb.Append(unencodingJsonFunDefine_str);

		sb.Append(encodingJsonAllFunDefine_str);
		sb.Append(unencodingJsonAllFunDefine_str);

		sb.AppendLine("}");
		sb.AppendLine("}");
		return sb.ToString();
	}
	#endregion CSharp

	#region CPlussPluss
	public void ExportAllCPlussPlussClass(string savePath)
	{
		if (!Directory.Exists(savePath + "/OutCPlus"))
		{
			Directory.CreateDirectory(savePath + "/OutCPlus");
		}

		if (!Directory.Exists(savePath + "/OutCPlus/BitProtocolClass/"))
		{
			Directory.CreateDirectory(savePath + "/OutCPlus/BitProtocolClass");
		}
		if (!Directory.Exists(savePath + "/OutCPlus/BitProtocolClass/CherishBitProtocol"))
		{
			Directory.CreateDirectory(savePath + "/OutCPlus/BitProtocolClass/CherishBitProtocol");
		}

		string bitProtocolTool_str = BuildCPlussPlussBitProtocolTool();
		File.WriteAllText(savePath + "/OutCPlus/BitProtocolClass/CherishBitProtocol/CherishBitProtocolTool.h", bitProtocolTool_str, Encoding.UTF8);
		Debug.Log(savePath + "/OutCPlus/BitProtocolClass/CherishBitProtocol/CherishBitProtocolTool.h");
		string bitProtocolBase_str = ExportCPlussPlussBase();
		File.WriteAllText(savePath + "/OutCPlus/BitProtocolClass/CherishBitProtocol/CherishBitProtocolBase.h", bitProtocolBase_str, Encoding.UTF8);
		Debug.Log(savePath + "/OutCPlus/BitProtocolClass/CherishBitProtocol/CherishBitProtocolBase.h");

		foreach (ClassDefine data in mProtoClasses)
		{
			if (!Directory.Exists(savePath + "/OutCPlus/BitProtocolClass/" + data.dirName))
			{
				Directory.CreateDirectory(savePath + "/OutCPlus/BitProtocolClass/" + data.dirName);
			}
			Debug.Log("这里开始生成消息:" + data.dirName + "/" + data.className);
			string curClassH = ExportCPlussPlussClassH(data);
			string curClassC = ExportCPlussPlussClassC(data);
			File.WriteAllText(savePath + "/OutCPlus/BitProtocolClass/" + data.dirName + "/" + data.className + ".h", curClassH, Encoding.UTF8);
			File.WriteAllText(savePath + "/OutCPlus/BitProtocolClass/" + data.dirName + "/" + data.className + ".cpp", curClassC, Encoding.UTF8);

			Debug.Log(savePath + "/OutCPlus/BitProtocolClass/" + data.dirName + "/" + data.className + ".h");
			Debug.Log(savePath + "/OutCPlus/BitProtocolClass/" + data.dirName + "/" + data.className + ".cpp");
		}
	}

	public bool IsDefineType(string type)
	{
		List<string> defTypeList = new List<string>()
		{
			"Boolean",
			"Byte",
			"SByte",
			"Int16",
			"UInt16",
			"Int32",
			"UInt32",
			"Int64",
			"UInt64",
			"Single",
			"Double",
			"String",			
		};

		return defTypeList.Contains(type);
	}

	public string BuildCPlussPlussBitProtocolTool()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ");
		sb.AppendLine("//****" + "CherishBitProtocolTool" + "****");

		#region 文件包含
		sb.AppendLine("#pragma once");
		sb.AppendLine("#include <string>");
		sb.AppendLine("#include <vector>");
		#endregion 文件包含
		sb.AppendLine("////////////////////");
		sb.AppendLine("namespace CherishBitProtocolTool{");
		#region 命名重定义
		sb.AppendLine("#define null 0;");
		sb.AppendLine("typedef bool Boolean;");
		sb.AppendLine("typedef char SByte;");
		sb.AppendLine("typedef short Int16;");
		sb.AppendLine("typedef int Int32;");
		sb.AppendLine("typedef long long Int64;");
		sb.AppendLine("typedef unsigned char Byte;");
		sb.AppendLine("typedef unsigned short UInt16;");
		sb.AppendLine("typedef unsigned int UInt32;");
		sb.AppendLine("typedef unsigned long long UInt64;");
		sb.AppendLine("typedef float Single;");
		sb.AppendLine("typedef double Double;");
		sb.AppendLine("typedef std::string String;");
		#endregion 命名重定义
		sb.AppendLine("}");

		#region 申明类型
		Dictionary<string, List<string>> classNameSpace = new Dictionary<string, List<string>>();
		for (int i = 0; i < mProtoClasses.Count; ++i)
		{
			ClassDefine cd = mProtoClasses[i];
			if (classNameSpace.ContainsKey(cd.dirName))
			{
				classNameSpace[cd.dirName].Add(cd.className);
			}
			else
			{
				classNameSpace.Add(cd.dirName, new List<string>() { cd.className });
			}
		}

		foreach (var kv in classNameSpace)
		{
			sb.AppendLine("namespace " + kv.Key + " {");
			for (int i = 0; i < kv.Value.Count; ++i)
			{
				sb.AppendLine("class " + kv.Value[i] + ";");
			}			
			sb.AppendLine("}");
		}
		#endregion 申明类型
		return sb.ToString(); ;
	}

	public string ExportCPlussPlussBase()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ID : ");
		sb.AppendLine("//**** 基类 ****");

		sb.AppendLine("#pragma once");
		sb.AppendLine("#include \"CherishBitProtocolTool.h\"");

		#region 定义类型
		sb.AppendLine("namespace CherishBitProtocolTool{");
		sb.AppendLine("class CherishBitProtocolBase{");
		sb.AppendLine("public:");
		sb.AppendLine("virtual CherishBitProtocolTool::Byte* Serializer(int* nSize,std::vector<void*>* gcList) { return nullptr;};");
		sb.AppendLine("virtual int Deserializer(CherishBitProtocolTool::Byte* pBuf, int nOffset,std::vector<void*>* gcList){return 0;};");
		sb.AppendLine("virtual CherishBitProtocolTool::Byte* OutSerializer(int nSize){return nullptr;};");
		sb.AppendLine("virtual int OutDeserializer(Byte* pBuf, int nOffset){return 0;};");
		sb.AppendLine("};");
		sb.AppendLine("}");
		#endregion 定义类型

		sb.AppendLine("////////////////////");
		#region 定义全部包含文件
		for (int i = 0; i < mProtoClasses.Count; ++i)
		{
			ClassDefine cd = mProtoClasses[i];
			sb.AppendLine("#include \"../" + cd.dirName + "/" + cd.className + ".h\"");
		}
		#endregion 定义全部包含文件
		sb.AppendLine("////////////////////");

		return sb.ToString();
	}

	public string ExportCPlussPlussClassH(ClassDefine data)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ID : " + data.classId);
		sb.AppendLine("//****" + data.classDesc + "****");

		#region 头文件定义
		sb.AppendLine("#pragma once");
		sb.AppendLine("#include \"../CherishBitProtocol/CherishBitProtocolBase.h\"");
		sb.AppendLine("using namespace CherishBitProtocolTool;\n");
		#endregion 头文件定义

		sb.AppendLine("namespace " + data.dirName + "{");


		///类定义
		string classDefine_str = "";
		///字段定义
		string fieldDifine_str = "";
		///构造函数定义
		string createrFunDefine_str = "";
		///编码方法定义
		string encodinFunDefine_str = "";
		///还原字段方法
		string unencodingFunDefine_str = "";
		///序列化类型
		string encodingAllFunDefine_str = "";
		///反序列化
		string unencodingAllFunDefine_str = "";
		//对外输出序列化
		string outEncodingAllFunDefine_str = "";
		//对外输出反序列化
		string outUnencodingAllFunDefine_str = "";

		classDefine_str += "/// <summary>\n";
		classDefine_str += "///" + data.classDesc + "\n";
		classDefine_str += "/// <\\summary>\n";
		classDefine_str += "class " + data.className + " : CherishBitProtocolTool::CherishBitProtocolBase {\n";
		classDefine_str += "public:\n";

		#region 属性定义
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			fieldDifine_str += "/// <summary>\n";
			fieldDifine_str += "///" + v.vlaueDesc + "\n";
			fieldDifine_str += "/// <\\summary>\n";

			if (IsDefineType(v.valueType))
			{
				if (v.isArray)
				{
					fieldDifine_str += "std::vector<CherishBitProtocolTool::" + v.valueType + "> " + v.valueName + ";\n";
				}
				else
				{
					fieldDifine_str += "CherishBitProtocolTool::" + v.valueType + " " + v.valueName + ";\n";
				}
			}
			else
			{
				if (v.isArray)
				{
					fieldDifine_str += "std::vector<" + v.valueType + "*> " + v.valueName + ";\n";
				}
				else
				{
					fieldDifine_str += v.valueType + "* " + v.valueName + ";\n";
				}
			}
		}
		#endregion 属性定义

		#region 构造方法
		createrFunDefine_str += data.className + "();\n";
		createrFunDefine_str += "\n";

		if (data.valus.Count > 0)
		{   ///构造方法 方法头
			createrFunDefine_str += data.className + "(";
			bool isFirst = true;
			foreach (FieldDefine v in data.valus)
			{
				if (!isFirst)
				{
					createrFunDefine_str += ", ";
				}

				if (IsDefineType(v.valueType))
				{
					if (v.isArray)
					{
						createrFunDefine_str += "std::vector<CherishBitProtocolTool::" + v.valueType + "> _" + v.valueName;
					}
					else
					{
						createrFunDefine_str += "CherishBitProtocolTool::" + v.valueType + " _" + v.valueName;
					}
				}
				else
				{
					if (v.isArray)
					{
						createrFunDefine_str += "std::vector<" + v.valueType + "*> _" + v.valueName;
					}
					else
					{
						createrFunDefine_str += v.valueType + "* _" + v.valueName;
					}
				}

				isFirst = false;
			}
			createrFunDefine_str += ");\n";
		}
		#endregion 构造方法

		#region 获取字节
		encodinFunDefine_str += "private:\n";
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			encodinFunDefine_str += "\n";
			encodinFunDefine_str += "Byte* get_" + v.valueName + "_encoding(int* size,std::vector<void*>* gcList);\n";
		}
		#endregion 获取字节定义

		#region 通过字节还原字段
		unencodingFunDefine_str += "private:\n";
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			///返回使用的字节数量
			unencodingFunDefine_str += "int set_" + v.valueName + "_fromBuf(CherishBitProtocolTool::Byte* sourceBuf,int curInde,std::vector<void*>* gcList);\n";
		}
		#endregion 通过字节还原字段
				

		encodingAllFunDefine_str += "public:\n";
		encodingAllFunDefine_str += "virtual CherishBitProtocolTool::Byte* Serializer(int* nSize,std::vector<void*>* gcList);\n";
		unencodingAllFunDefine_str = "virtual int Deserializer(CherishBitProtocolTool::Byte* pBuf, int nOffset,std::vector<void*>* gcList);\n";
		
		outEncodingAllFunDefine_str += "public:\n";
		outEncodingAllFunDefine_str += "virtual CherishBitProtocolTool::Byte* OutSerializer(int* nSize);\n";
		outUnencodingAllFunDefine_str = "virtual int OutDeserializer(CherishBitProtocolTool::Byte* pBuf, int nOffset);\n";

		sb.Append(classDefine_str);
		sb.Append(fieldDifine_str);
		sb.Append(createrFunDefine_str);
		sb.Append(encodinFunDefine_str);
		sb.Append(unencodingFunDefine_str);
		sb.Append(encodingAllFunDefine_str);
		sb.Append(unencodingAllFunDefine_str);
		sb.Append(outEncodingAllFunDefine_str);
		sb.Append(outUnencodingAllFunDefine_str);
		sb.AppendLine("};");
		sb.AppendLine("}");

		return sb.ToString();
	}

	public string ExportCPlussPlussClassC(ClassDefine data)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("// 此文件由协议导出插件自动生成");
		sb.AppendLine("// ID : " + data.classId);
		sb.AppendLine("//****" + data.classDesc + "****");

		#region 头文件定义
		sb.AppendLine("#include \""+ data.className + ".h\"");
		#endregion 头文件定义

		sb.AppendLine("namespace " + data.dirName + "{");
		
		///构造函数定义
		string createrFunDefine_str = "";
		///编码方法定义
		string encodinFunDefine_str = "";
		///还原字段方法
		string unencodingFunDefine_str = "";
		///序列化类型
		string encodingAllFunDefine_str = "";
		///反序列化
		string unencodingAllFunDefine_str = "";
		//对外输出序列化
		string outEncodingAllFunDefine_str = "";
		//对外输出反序列化
		string outUnencodingAllFunDefine_str = "";

		#region 构造方法
		createrFunDefine_str += data.className +"::"+ data.className + "(){}\n";
		createrFunDefine_str += "\n";

		if (data.valus.Count > 0)
		{   ///构造方法 方法头
			createrFunDefine_str += data.className + "::"  + data.className + "(";
			bool isFirst = true;
			foreach (FieldDefine v in data.valus)
			{
				if (!isFirst)
				{
					createrFunDefine_str += ", ";
				}

				if (IsDefineType(v.valueType))
				{
					if (v.isArray)
					{
						createrFunDefine_str += "std::vector<CherishBitProtocolTool::" + v.valueType + "> _" + v.valueName;
					}
					else
					{
						createrFunDefine_str += "CherishBitProtocolTool::" + v.valueType + " _" + v.valueName;
					}
				}
				else
				{
					if (v.isArray)
					{
						createrFunDefine_str += "std::vector<" + v.valueType + "*> _" + v.valueName;
					}
					else
					{
						createrFunDefine_str += v.valueType + "* _" + v.valueName;
					}
				}
				isFirst = false;
			}
			createrFunDefine_str += "){\n";

			///构造方法体
			foreach (FieldDefine v in data.valus)
			{
				createrFunDefine_str += "this->" + v.valueName + " = _" + v.valueName + ";\n";
			}

			createrFunDefine_str += "}";
		}
		#endregion 构造方法

		#region 获取字节
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			encodinFunDefine_str += "\n";
			encodinFunDefine_str += "CherishBitProtocolTool::Byte* " + data.className + "::get_" + v.valueName + "_encoding(int* size,std::vector<void*>* gcList){\n";
			encodinFunDefine_str += "CherishBitProtocolTool::Byte* outBuf = nullptr;\n";

			if (v.isArray)
			{				
				if (v.valueType == "SByte")
				{
					encodinFunDefine_str += "int offset = 1;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "outBuf[i * offset  + 4] = " + v.valueName + "[i] + 128;\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int16")
				{
					encodinFunDefine_str += "int offset = 2;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i  * offset + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int32")
				{
					encodinFunDefine_str += "int offset = 4;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i  * offset + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int64")
				{
					encodinFunDefine_str += "int offset = 8;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i  * offset + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Byte")
				{
					encodinFunDefine_str += "int offset = 1;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset +4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt16")
				{
					encodinFunDefine_str += "int offset = 2;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset  + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt32")
				{
					encodinFunDefine_str += "int offset = 4;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset  + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt64")
				{
					encodinFunDefine_str += "int offset = 8;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset  + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Single")
				{
					encodinFunDefine_str += "int offset = 4;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Double")
				{
					encodinFunDefine_str += "int offset = 8;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "Boolean")
				{
					encodinFunDefine_str += "int offset = 1;\n";
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "*size = " + v.valueName + ".size() * offset;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "*size += 4;";
					encodinFunDefine_str += "memcpy(outBuf,&eleSize,4);\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();){\n";
					encodinFunDefine_str += "memcpy(&outBuf[i * offset + 4], &" + v.valueName + "[i], offset);\n";
					encodinFunDefine_str += "i += 1;\n";
					encodinFunDefine_str += "}\n";
				}
				else if (v.valueType == "String")
				{
					encodinFunDefine_str += "int elementCount = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "int strSize = 0;\n";
					encodinFunDefine_str += "for (int i = 0;i < " + v.valueName + ".size();++i){\n";
					encodinFunDefine_str += "strSize += " + v.valueName + "[i].size();\n";
					encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "int divSize = strSize + 4 + elementCount * 4;\n";
					encodinFunDefine_str += "*size = 0;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[strSize + 4 + elementCount * 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf,&elementCount,4);\n";
					encodinFunDefine_str += "*size += 4;\n";
					encodinFunDefine_str += "for(int i = 0;i < " + v.valueName + ".size();++i){\n";
					encodinFunDefine_str += "int str_size = " + v.valueName + "[i].size();\n";
					//encodinFunDefine_str += "if(str_size > 0 )\n{";
					//encodinFunDefine_str += "str_size += 1;\n";
					//encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "memcpy(&outBuf[*size], &str_size, 4);\n";
					encodinFunDefine_str += "*size += 4;\n";
					encodinFunDefine_str += "memcpy(&outBuf[*size]," + v.valueName + "[i].c_str(), str_size);\n";
					encodinFunDefine_str += "*size += str_size;\n";
					encodinFunDefine_str += "}\n";
				}
				else
				{
					encodinFunDefine_str += "int eleSize = " + v.valueName + ".size();\n";
					encodinFunDefine_str += "CherishBitProtocolTool::Byte eleLength[4];\n;";
					encodinFunDefine_str += "memcpy(&eleLength,&eleSize,4);\n";
					encodinFunDefine_str += "std::vector<int> lengthList;\n";
					encodinFunDefine_str += "std::vector<CherishBitProtocolTool::Byte*> dataList;\n";
					encodinFunDefine_str += "for(int i = 0;i < eleSize;++i)\n{";
					encodinFunDefine_str += v.valueType + "* baseObject = " + v.valueName + "[i];\n";
					encodinFunDefine_str += "int outSize = 0;\n";
					encodinFunDefine_str += "CherishBitProtocolTool::Byte* elemeData = baseObject->Serializer(&outSize,gcList);\n";
					encodinFunDefine_str += "lengthList.push_back(outSize);\n";
					encodinFunDefine_str += "dataList.push_back(elemeData);\n";
					encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "int toldBufSize = 0;\n";
					encodinFunDefine_str += "for(int i = 0;i < lengthList.size();++i){\n";
					encodinFunDefine_str += "toldBufSize += lengthList[i];\n";
					encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "*size = toldBufSize + 4;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, eleLength, 4);\n";
					encodinFunDefine_str += "int offsetCut = 4;\n";
					encodinFunDefine_str += "for(int i = 0;i < lengthList.size();++i){\n";
					encodinFunDefine_str += "int length = lengthList[i];\n";
					encodinFunDefine_str += "CherishBitProtocolTool::Byte* data = dataList[i];\n";
					encodinFunDefine_str += "memcpy(&outBuf[offsetCut], data, length);\n";
					encodinFunDefine_str += "offsetCut += length;\n";
					encodinFunDefine_str += "}\n";
				}
			}
			else
			{
				if (v.valueType == "SByte")
				{
					encodinFunDefine_str += "*size = 1;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "outBuf[0] = " + v.valueName + " + 128;\n";
				}
				else if (v.valueType == "Int16")
				{
					encodinFunDefine_str += "*size = 2;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "Int32")
				{
					encodinFunDefine_str += "*size = 4;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "Int64")
				{
					encodinFunDefine_str += "*size = 8;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "Byte")
				{
					encodinFunDefine_str += "*size = 1;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "outBuf[0] = " + v.valueName + ";\n";
				}
				else if (v.valueType == "UInt16")
				{
					encodinFunDefine_str += "*size = 2;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "UInt32")
				{
					encodinFunDefine_str += "*size = 4;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "UInt64")
				{
					encodinFunDefine_str += "*size = 8;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "Single")
				{
					encodinFunDefine_str += "*size = 4;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "Double")
				{
					encodinFunDefine_str += "*size = 8;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "Boolean")
				{
					encodinFunDefine_str += "*size = 1;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &" + v.valueName + ", *size);\n";
				}
				else if (v.valueType == "String")
				{
					encodinFunDefine_str += "int strSize = " + v.valueName + ".length();\n";
					encodinFunDefine_str += "if(strSize > 0 )\n{";
					//encodinFunDefine_str += "strSize += 1;\n";
					encodinFunDefine_str += "}\n";
					encodinFunDefine_str += "*size = strSize;\n";
					encodinFunDefine_str += "outBuf = new CherishBitProtocolTool::Byte[*size + 4];\n";
					encodinFunDefine_str += "gcList->push_back(outBuf);\n";
					encodinFunDefine_str += "memcpy(outBuf, &strSize, 4);\n";					
					encodinFunDefine_str += "memcpy(outBuf + 4, " + v.valueName + ".c_str(), *size);\n";
					encodinFunDefine_str += "*size += 4;\n";
				}
				else
				{

					encodinFunDefine_str += "outBuf = ((CherishBitProtocolBase*)" + v.valueName + ")->Serializer(size,gcList);\n";
				}
			}

			encodinFunDefine_str += "return outBuf;\n";
			encodinFunDefine_str += "}\n";
			encodinFunDefine_str += "\n";
		}
		#endregion 获取字节

		#region 通过字节还原字段
		///属性定义
		foreach (FieldDefine v in data.valus)
		{
			///返回使用的字节数量
			unencodingFunDefine_str += "int " + data.className + "::set_" + v.valueName + "_fromBuf(CherishBitProtocolTool::Byte* sourceBuf,int curIndex,std::vector<void*>* gcList){\n";
			unencodingFunDefine_str += "CherishBitProtocolTool::Byte tag = sourceBuf[curIndex];\n";
			unencodingFunDefine_str += "curIndex += 1;\n";
			unencodingFunDefine_str += "if(tag != 0){;\n";
			if (v.isArray)
			{
				//unencodingFunDefine_str += v.valueName + " = new std::<" + v.valueType + ">();\n";

				unencodingFunDefine_str += "int listCount = 0;\n";
				unencodingFunDefine_str += "memcpy(&listCount,sourceBuf + curIndex,4);\n";
				unencodingFunDefine_str += "curIndex += 4;\n";
				if (v.valueType == "SByte")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = (CherishBitProtocolTool::SByte)(sourceBuf[curIndex] - 128);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int16")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],2);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int32")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Int64")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],8);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Byte")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],1);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt16")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],2);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt32")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "UInt64")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],8);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Single")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Double")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],8);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "Boolean")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = 0;\n";
					unencodingFunDefine_str += "memcpy(&curTarget,&sourceBuf[curIndex],1);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "curIndex += 1;\n";
					unencodingFunDefine_str += "}\n";
				}
				else if (v.valueType == "String")
				{
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + " curTarget = \"\";\n";
					unencodingFunDefine_str += "int strLength = 0;\n";
					unencodingFunDefine_str += "memcpy(&strLength,&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "CherishBitProtocolTool::SByte* byteArray = new CherishBitProtocolTool::SByte[strLength + 1];\n";
					unencodingFunDefine_str += "gcList->push_back(byteArray);\n";
					unencodingFunDefine_str += "for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){\n";
					unencodingFunDefine_str += "byteArray[loopStrByte] = sourceBuf[curIndex];\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
					unencodingFunDefine_str += "byteArray[strLength] = 0;\n";
					unencodingFunDefine_str += "curTarget = byteArray;\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "}\n";
				}
				else
				{
					//这里是对字段进行初始 不需要进入GC
					unencodingFunDefine_str += "for(int index = 0;index < listCount;++index){\n";
					unencodingFunDefine_str += v.valueType + "* curTarget = new " + v.valueType + "();\n";
					unencodingFunDefine_str += "curIndex = curTarget->Deserializer(sourceBuf,curIndex,gcList);\n";
					unencodingFunDefine_str += v.valueName + ".push_back(curTarget);\n";
					unencodingFunDefine_str += "}\n";
				}
			}
			else
			{
				if (v.valueType == "SByte")
				{
					unencodingFunDefine_str += v.valueName + " = (CherishBitProtocolTool::SByte)(sourceBuf[curIndex] - 128);\n";
					unencodingFunDefine_str += "curIndex++;\n";
				}
				else if (v.valueType == "Int16")
				{
					unencodingFunDefine_str += "memcpy(&"+ v.valueName + ",&sourceBuf[curIndex],2);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
				}
				else if (v.valueType == "Int32")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
				}
				else if (v.valueType == "Int64")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],8);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
				}
				else if (v.valueType == "Byte")
				{
					unencodingFunDefine_str += v.valueName + " = sourceBuf[curIndex];\n";
					unencodingFunDefine_str += "curIndex++;\n";
				}
				else if (v.valueType == "UInt16")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],2);\n";
					unencodingFunDefine_str += "curIndex += 2;\n";
				}
				else if (v.valueType == "UInt32")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
				}
				else if (v.valueType == "UInt64")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],8);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
				}
				else if (v.valueType == "Single")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
				}
				else if (v.valueType == "Double")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],8);\n";
					unencodingFunDefine_str += "curIndex += 8;\n";
				}
				else if (v.valueType == "Boolean")
				{
					unencodingFunDefine_str += "memcpy(&" + v.valueName + ",&sourceBuf[curIndex],1);\n";
					unencodingFunDefine_str += "curIndex += 1;\n";
				}
				else if (v.valueType == "String")
				{
					unencodingFunDefine_str += v.valueName + " = \"\";\n";
					unencodingFunDefine_str += "int strLength = 0;\n";
					unencodingFunDefine_str += "memcpy(&strLength,&sourceBuf[curIndex],4);\n";
					unencodingFunDefine_str += "curIndex += 4;\n";
					unencodingFunDefine_str += "CherishBitProtocolTool::SByte* byteArray = new CherishBitProtocolTool::SByte[strLength + 1];\n";
					unencodingFunDefine_str += "gcList->push_back(byteArray);\n";
					unencodingFunDefine_str += "for (int loopStrByte = 0; loopStrByte < strLength; ++loopStrByte){\n";
					unencodingFunDefine_str += "byteArray[loopStrByte] = sourceBuf[curIndex];\n";
					unencodingFunDefine_str += "curIndex++;\n";
					unencodingFunDefine_str += "}\n";
					unencodingFunDefine_str += "byteArray[strLength] = 0;\n";
					unencodingFunDefine_str += v.valueName + " = byteArray;\n";
				}
				else
				{
					//这里对字段进行初始 不需要进入GC
					unencodingFunDefine_str += v.valueName + " = new " + v.valueType + "();\n";
					unencodingFunDefine_str += "curIndex = " + v.valueName + "->Deserializer(sourceBuf,curIndex,gcList);\n";
				}
			}
			unencodingFunDefine_str += "}";
			unencodingFunDefine_str += "return curIndex;\n";
			unencodingFunDefine_str += "}";
			unencodingFunDefine_str += "\n";
		}
		#endregion 通过字节还原字段

		#region 序列化类型
		encodingAllFunDefine_str = "CherishBitProtocolTool::Byte* " + data.className + "::Serializer(int* nSize,std::vector<void*>* gcList){\n";
		encodingAllFunDefine_str += "std::vector<Byte> memoryWrite;\n";
		encodingAllFunDefine_str += "CherishBitProtocolTool::Byte* byteBuf = nullptr;\n";
		foreach (FieldDefine v in data.valus)
		{
			if (IsDefineType(v.valueType) || v.isArray)
			{
				encodingAllFunDefine_str += "if(true){\n";
			}
			else
			{
				encodingAllFunDefine_str += "if(" + v.valueName + " !=  nullptr){\n";
				//encodingAllFunDefine_str += "if(true){\n";
			}
			
			///写入标志为 1 非空
			encodingAllFunDefine_str += "memoryWrite.push_back(1);\n";
			encodingAllFunDefine_str += "int bufSize = 0;";
			encodingAllFunDefine_str += "byteBuf = get_" + v.valueName + "_encoding(&bufSize,gcList);\n";
			encodingAllFunDefine_str += "if(bufSize > 0){\n";
			encodingAllFunDefine_str += "memoryWrite.insert(memoryWrite.end(),byteBuf,byteBuf + bufSize);\n";
			encodingAllFunDefine_str += "}\n";
			encodingAllFunDefine_str += "}\n";
			encodingAllFunDefine_str += "else {";
			///写入标志为 0 空
			encodingAllFunDefine_str += "memoryWrite.push_back(0);\n";
			encodingAllFunDefine_str += "}";
		}
		encodingAllFunDefine_str += "CherishBitProtocolTool::Byte* bufResult = new CherishBitProtocolTool::Byte[memoryWrite.size()];\n";
		encodingAllFunDefine_str += "gcList->push_back(bufResult);\n";
		encodingAllFunDefine_str += "memcpy(bufResult,memoryWrite.data(),memoryWrite.size());\n";
		encodingAllFunDefine_str += "*nSize = memoryWrite.size();\n";
		encodingAllFunDefine_str += "return bufResult;\n";
		encodingAllFunDefine_str += "}\n";
		encodingAllFunDefine_str += "\n";
		#endregion 序列化类型

		#region 反序列化类型
		unencodingAllFunDefine_str = "int " + data.className + "::Deserializer(Byte* sourceBuf,int startOffset,std::vector<void*>* gcList){\n";
		foreach (FieldDefine v in data.valus)
		{
			unencodingAllFunDefine_str += "startOffset = set_" + v.valueName + "_fromBuf(sourceBuf,startOffset,gcList);\n";
		}
		unencodingAllFunDefine_str += "return startOffset;";
		unencodingAllFunDefine_str += "}\n";
		#endregion 反序列化类型
		
		#region 对外输出序列化类型
		outEncodingAllFunDefine_str += "CherishBitProtocolTool::Byte* " + data.className + "::OutSerializer(int* nSize){\n";
		outEncodingAllFunDefine_str += "*nSize = 0;\n";
		outEncodingAllFunDefine_str += "std::vector<void*> gcList;\n";
		outEncodingAllFunDefine_str += "CherishBitProtocolTool::Byte* result = Serializer(nSize,&gcList);\n";
		outEncodingAllFunDefine_str += "CherishBitProtocolTool::Byte* outBuf = new CherishBitProtocolTool::Byte[*nSize];\n";
		outEncodingAllFunDefine_str += "memcpy(outBuf,result,*nSize);\n";
		outEncodingAllFunDefine_str += "for(int i = 0;i < gcList.size();++i){\n";
		outEncodingAllFunDefine_str += "void* ptrData = gcList[i];\n";
		outEncodingAllFunDefine_str += "delete ptrData;\n";
		outEncodingAllFunDefine_str += "}\n";
		outEncodingAllFunDefine_str += "return outBuf;\n";
		outEncodingAllFunDefine_str += "}";
		#endregion 对外输出序列化类型

		#region 对外输出反序列化类型
		outUnencodingAllFunDefine_str = "int " + data.className + "::OutDeserializer(CherishBitProtocolTool::Byte* sourceBuf,int startOffset){\n";
		outUnencodingAllFunDefine_str += "std::vector<void*> gcList;\n";
		outUnencodingAllFunDefine_str += "int nSize = Deserializer(sourceBuf,startOffset,&gcList);\n";
		outUnencodingAllFunDefine_str += "for(int i = 0;i < gcList.size();++i){\n";
		outUnencodingAllFunDefine_str += "void* ptrData = gcList[i];\n";
		outUnencodingAllFunDefine_str += "delete ptrData;\n";
		outUnencodingAllFunDefine_str += "}\n";
		outUnencodingAllFunDefine_str += "return nSize;\n";
		outUnencodingAllFunDefine_str += "}\n";
		#endregion 对外输出反序列化类型

		sb.Append(createrFunDefine_str);
		sb.Append(encodinFunDefine_str);
		sb.Append(unencodingFunDefine_str);
		sb.Append(encodingAllFunDefine_str);
		sb.Append(unencodingAllFunDefine_str);
		sb.Append(outEncodingAllFunDefine_str);
		sb.Append(outUnencodingAllFunDefine_str);

		sb.AppendLine("}");
		return sb.ToString();
	}
	#endregion CPlussPluss
	}

