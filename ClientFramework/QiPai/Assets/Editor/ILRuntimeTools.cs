using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;

public class ILRuntimeTools
{
	[MenuItem("ILRuntime/Generate CLR Binding Code by Analysis")]
	static void GenerateCLRBindingByAnalysis()
	{
		byte[] addinStream = null;
		ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();

		using (System.IO.FileStream fs = new System.IO.FileStream("Assets/StreamingAssets/raw.data", System.IO.FileMode.Open, System.IO.FileAccess.Read))
		{
			addinStream = new byte[fs.Length];
			fs.Read(addinStream, 0, addinStream.Length);
			addinStream = CompressEncryption.UnEncryption(addinStream);
		}
		var fsdata = new System.IO.MemoryStream(addinStream);
		domain.LoadAssembly(fsdata);

		//Crossbind Adapter is needed to generate the correct binding code
		ILRuntimeRegistAdapter.RegisterAdapter(domain);
		ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/ILRuntime/Generated");
	}

	[MenuItem("ILRuntime/Spawn Unity Engine All TypeLink")]
	static public void SpawnUnityEngineAllTypeLink()
	{
		string outputPath = "Assets/ILRuntime/UnityEngineAllTypeLink";

		byte[] addinStream = null;

		using (System.IO.FileStream fs = new System.IO.FileStream("Assets/StreamingAssets/raw.data", System.IO.FileMode.Open, System.IO.FileAccess.Read))
		{
			addinStream = new byte[fs.Length];
			fs.Read(addinStream, 0, addinStream.Length);
			addinStream = CompressEncryption.UnEncryption(addinStream);
		}

		var assembly = Assembly.Load(addinStream);
		var refencedAssemblies = assembly.GetReferencedAssemblies();
		List<string> namespaceList = new List<string>();

		//for (var i = 0; i < refencedAssemblies.Length; ++i)
		//{
		//	Debug.Log($"refencedAssemblies {i}:{refencedAssemblies[i].FullName}");
		//	namespaceList.Add(refencedAssemblies[i].FullName);
		//}
		//namespaceList.Add("UnityEngine.TrailRenderer");
		//namespaceList.Add("UnityEngine.MeshRenderer");
		//namespaceList.Add("UnityEngine.SkinnedMeshRenderer");
		//namespaceList.Add("RenderOrderLayerSet");
		//namespaceList.Add("UnityEngine.LineRenderer");
		//namespaceList.Add("UnityEngine");
		//namespaceList.Add("UnityEngine.ARModule");
		//namespaceList.Add("UnityEngine.AIModule");
		////namespaceList.Add("UnityEngine.CoreModule");
		//namespaceList.Add("UnityEngine.AccessibilityModule");
		//namespaceList.Add("UnityEngine.AnimationModule");
		////namespaceList.Add("UnityEngine.AudioModule");
		//namespaceList.Add("UnityEngine.ClothModule");
		//namespaceList.Add("UnityEngine.VideoModule");
		//namespaceList.Add("UnityEngine.UI");
		//namespaceList.Add("UnityEngine.UnityWebRequestWWWModule");
		//namespaceList.Add("UnityEngine.ClusterInputModule");
		//namespaceList.Add("UnityEngine.ClusterRendererModule");
		//namespaceList.Add("UnityEngine.DirectorModule");
		//namespaceList.Add("UnityEngine.GridModule");
		//namespaceList.Add("UnityEngine.GameCenterModule");
		//namespaceList.Add("UnityEngine.ImageConversionModule");
		//namespaceList.Add("UnityEngine.JSONSerializeModule");
		//namespaceList.Add("UnityEngine.ParticlesLegacyModule");
		//namespaceList.Add("UnityEngine.ParticleSystemModule");

		//namespaceList.Add("UnityEngine.PerformanceReportingModule");
		//namespaceList.Add("UnityEngine.Physics2DModule");
		//namespaceList.Add("UnityEngine.PhysicsModule");
		//namespaceList.Add("UnityEngine.ScreenCaptureModule");
		//namespaceList.Add("UnityEngine.SpriteMaskModule");
		//namespaceList.Add("UnityEngine.TextRenderingModule");
		//namespaceList.Add("UnityEngine.VehiclesModule");
		//namespaceList.Add("LitJson");
		//namespaceList.Add("AssemblyLister");
		//namespaceList.Add("LitJson");
		//namespaceList.Add("I18N.CJK");
		//namespaceList.Add("I18N");

		Dictionary<string,int> mapHas = new Dictionary<string, int>();
		StringBuilder sb = new StringBuilder();
		sb.Append("public class SpawnUnityEngineAllTypeLink{\n");

		for (int i = 0; i < namespaceList.Count; ++i)
		{
			Type[] types = Assembly.Load(namespaceList[i]).GetTypes();

			for (int t = 0; t < types.Length; ++t)
			{
				Type type = types[t];
				
				if (type.IsAbstract 
					|| type.IsNotPublic 
					/*|| type.IsSealed*/
					|| type.IsNestedPublic
					|| type.IsNestedPrivate
					|| !type.IsVisible
					|| type.IsGenericTypeDefinition)
				{
					continue;
				}

				bool isObsolete = false;
				object[] attType = type.GetCustomAttributes(true);
				for (int iat = 0; iat < attType.Length; ++iat)
				{
					if (attType[iat] is ObsoleteAttribute)
					{
						isObsolete = true;
						break;
					}
				}


				if (isObsolete)
				{
					continue;
				}

				if (type.IsSealed)
				{
					string typeKey = type.FullName.Replace('+', '.').Replace('.', '_').Replace('`', '_');
					if (mapHas.ContainsKey(typeKey))
					{
						mapHas[typeKey]++;
					}
					else
					{
						mapHas.Add(typeKey, 1);
					}
					sb.Append(string.Format("public System.Type p_typeof_{0} = typeof({1});//{2}\n", typeKey+"_"+ mapHas[typeKey], type.FullName.Replace('+', '.'), namespaceList[i]));
					
					continue;
				}


				string typeAKey = type.Name;
				if (mapHas.ContainsKey(typeAKey))
				{
					mapHas[typeAKey]++;
				}
				else
				{
					mapHas.Add(typeAKey, 1);
				}
				sb.Append(string.Format("public {0} p_{1}; //{2}\n", type.FullName.Replace('+', '.'), typeAKey + "_" + mapHas[typeAKey], namespaceList[i]));
			}
		}

		sb.Append("}");

		if (!System.IO.Directory.Exists(outputPath))
			System.IO.Directory.CreateDirectory(outputPath);

		using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputPath + "/Links.cs", false, new UTF8Encoding(false)))
		{
			sw.Write(sb.ToString());
			sw.Flush();
		}
	}
}


