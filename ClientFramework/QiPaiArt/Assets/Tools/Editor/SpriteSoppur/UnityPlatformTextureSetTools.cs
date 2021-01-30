using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnityPlatformTextureSetTools : MonoBehaviour
{
	public static List<Object> allControlls = new List<Object>();

	public static void CollectModel<T>(Object fold) where T : Object
	{
		allControlls.Clear();
		string relatepath = AssetDatabase.GetAssetPath(fold);
		string folderpath = Path.GetFullPath(relatepath);
		CollectAllFiles<T>(folderpath.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
	}

	public static void CollectSkinOrAnimator<T>(Object fold) where T : Object
	{
		allControlls.Clear();
		string relatepath = AssetDatabase.GetAssetPath(fold);
		string folderpath = Path.GetFullPath(relatepath);
		CollectAnimatorFiles<T>(folderpath.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
	}
	public static void CollectAnimatorFiles<T>(string path) where T : Object
	{
		string[] localfiles = Directory.GetFiles(path);
		string[] dirs = Directory.GetDirectories(path);
		for (int i = 0; i < localfiles.Length; i++)
		{
			string filepath = localfiles[i];
			if (filepath.Contains(".meta")) continue;
			Object obj = AssetDatabase.LoadAssetAtPath<T>(filepath);
			if (!allControlls.Contains(obj))
				allControlls.Add(obj);
		}
		for (int j = 0; j < dirs.Length; j++)
		{
			CollectAnimatorFiles<T>(dirs[j]);
		}
	}

	public static void CollectAllFiles<T>(string path) where T : Object
	{
		string[] localfiles = Directory.GetFiles(path);
		string[] dirs = Directory.GetDirectories(path);
		for (int i = 0; i < localfiles.Length; i++)
		{
			string filepath = localfiles[i];
			if (filepath.Contains(".meta")) continue;
			if (!IsPicture(filepath)) continue;
			Object obj = AssetDatabase.LoadAssetAtPath<T>(filepath);
			if (!allControlls.Contains(obj))
				allControlls.Add(obj);
		}
		for (int j = 0; j < dirs.Length; j++)
		{
			CollectAllFiles<T>(dirs[j]);
		}
	}

	public static bool IsPicture(string filePath)
	{
		try
		{
			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(fs);
			string fileClass;
			byte buffer;
			buffer = reader.ReadByte();
			fileClass = buffer.ToString();
			buffer = reader.ReadByte();
			fileClass += buffer.ToString();
			reader.Close();
			fs.Close();
			if (fileClass == "255216" || fileClass == "7173" || fileClass == "13780" || fileClass == "6677")
			//255216是jpg;7173是gif;6677是BMP,13780是PNG
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		catch
		{
			return false;
		}
	}

	private static void onPreprocessTexture(string path, TextureImportData textureImportData,string platform)
	{
		//自动设置类型;  
		TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
		textureImporter.textureType = textureImportData.textureImporterType;
		textureImporter.textureShape = textureImportData.ShapeType;
		textureImporter.isReadable = textureImportData.isReadable;
		textureImporter.mipmapEnabled = textureImportData.mipmapEnabled;

		TextureImporterPlatformSettings settingAndroid = textureImporter.GetPlatformTextureSettings(platform);
		settingAndroid.overridden = true;
		settingAndroid.format = textureImportData.textureImporterFormat;  //设置格式

		textureImporter.SetPlatformTextureSettings(settingAndroid);

		textureImporter.SaveAndReimport();
	}

	private static void onSetPackerTexture(string path,string packer)
	{
		TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
		textureImporter.spritePackingTag = packer;
		textureImporter.SaveAndReimport();
	}

	[MenuItem("Assets/TextureTool/Android => Sprite ETC2")]
	public static void SetAndroidEtc2()
	{
		TextureImportData textureImportData = new TextureImportData();
		textureImportData.textureImporterFormat = TextureImporterFormat.ETC2_RGBA8;
		textureImportData.textureImporterType = TextureImporterType.Sprite;
		UnityEngine.Object[] arr = Selection.GetFiltered<Object>(SelectionMode.Assets);

		if (arr.Length == 1)
		{
			CollectModel<Object>(arr[0]);
		}

		for (var i = 0; i < allControlls.Count; ++i)
		{
			var imgObject = allControlls[i];
			onPreprocessTexture(AssetDatabase.GetAssetPath(imgObject),textureImportData,"Android");
		}
	}

	[MenuItem("Assets/TextureTool/IOS => Sprite PVRTC_RGBA4")]
	public static void SetIOSEtc2()
	{
		TextureImportData textureImportData = new TextureImportData();
		textureImportData.textureImporterFormat = TextureImporterFormat.PVRTC_RGBA4;
		textureImportData.textureImporterType = TextureImporterType.Sprite;
		UnityEngine.Object[] arr = Selection.GetFiltered<Object>(SelectionMode.Assets);

		if (arr.Length == 1)
		{
			CollectModel<Object>(arr[0]);
		}

		for (var i = 0; i < allControlls.Count; ++i)
		{
			var imgObject = allControlls[i];
			onPreprocessTexture(AssetDatabase.GetAssetPath(imgObject), textureImportData,"IOS");
		}
	}

	[MenuItem("Assets/TextureTool/设置图集")]
	public static void SetPackerWithFloder()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered<Object>(SelectionMode.Assets);
		string tagName = "";

		if (arr.Length == 1)
		{
			string relatepath = AssetDatabase.GetAssetPath(arr[0]);
			string outrelatepath = Path.GetFileName(relatepath);
			tagName = $"tag_{outrelatepath}";
			CollectModel<Object>(arr[0]);
		}

		for (var i = 0; i < allControlls.Count; ++i)
		{
			var imgObject = allControlls[i];
			onSetPackerTexture(AssetDatabase.GetAssetPath(imgObject), tagName);
		}
	}

	[MenuItem("Assets/TextureTool/清理图集")]
	public static void ClearPackerWithFloder()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered<Object>(SelectionMode.Assets);
		string tagName = "";

		if (arr.Length == 1)
		{
			tagName = "";
			CollectModel<Object>(arr[0]);
		}

		for (var i = 0; i < allControlls.Count; ++i)
		{
			var imgObject = allControlls[i];
			onSetPackerTexture(AssetDatabase.GetAssetPath(imgObject), tagName);
		}
	}

	[MenuItem("Assets/TextureTool/打图集引用")]
	public static void ClearPackerRefence()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered<Object>(SelectionMode.Assets);
		if (arr.Length == 1)
		{
			CollectModel<Sprite>(arr[0]);

			string relatepath = AssetDatabase.GetAssetPath(arr[0]);
			var newGame = new GameObject("sprite_package");
			var packSprite = newGame.AddComponent<ParkSpriteData>();
			packSprite.Clear();

			for (var i = 0; i < allControlls.Count; ++i)
			{
				var imgObject = allControlls[i];
				var spriteData = (imgObject as Sprite);

				if (spriteData != null)
				{
					var imageGameObject = new GameObject(spriteData.name);
					imageGameObject.transform.SetParent(newGame.transform);
					var image = imageGameObject.AddComponent<Image>();
					image.overrideSprite = spriteData;
					image.sprite = image.overrideSprite;
					packSprite.AddNewData(spriteData.name, image);
				}
			}

			var _target = MonoBehaviour.Instantiate(newGame, newGame.transform.position, newGame.transform.rotation);
			bool isSuccess = false;
			PrefabUtility.SaveAsPrefabAsset(_target, relatepath.Replace("\\","/") + $"/{ newGame.name }.prefab", out isSuccess);
			MonoBehaviour.DestroyImmediate(_target);
		}
	}
}

public class TextureImportData
{
	public TextureImporterType textureImporterType = TextureImporterType.Default;
	public bool isReadable = false;
	public bool mipmapEnabled = false;
	public TextureImporterShape ShapeType = TextureImporterShape.Texture2D;
	/// <summary>
	/// 图片压缩格式
	/// androdi TextureImporterFormat.ETC2_RGB4;
	/// ios TextureImporterFormat.PVRTC_RGB4
	/// </summary>
	public TextureImporterFormat textureImporterFormat = TextureImporterFormat.ETC2_RGB4;
}
