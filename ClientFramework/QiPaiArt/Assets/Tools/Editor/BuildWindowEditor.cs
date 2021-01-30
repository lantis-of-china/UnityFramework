#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;

public enum 运营商 : int
{
    内部网 = 0,
    外部网,
    腾讯,
    网易,
    豌豆荚
}

public enum 系统平台 : int
{
    IOS = 0,
    Android
}

public class BuildWindowEditor : EditorWindow
{
    private static BuildWindowEditor thisWindow;

    private Rect LoadMoudleWindowRect = new  Rect (0,0,200,200);
    private bool IsFoldout = true;
    private Vector2 scrollPos = Vector2.zero;


    private Rect SceneRect = new Rect(200, 0, 200, 200);

    private GameObject mSelectObj = null;
    private GameObject mCurrentObj = null;
    private GameObject mDrawObj = null;

    private Rect EditorMoudleWindowRect = new Rect(400, 0, 200, 200);

    private int colorMode = 0;

    int[] frameArray =
    {
        10,
        20,
        30,
        40,
        50,
        60,
        10,
        80,
        90,
        100
    };

    string[] frameName =
    {
        "10 FPS",
        "20 FPS",
        "30 FPS",
        "40 FPS",
        "50 FPS",
        "60 FPS",
        "70 FPS",
        "80 FPS",
        "90 FPS",
        "100 FPS",
    };


    int[] colorAry =
    {
        0,
        1,
        2
    };

    string[] colorModeName = 
    {
        "无彩",
        "红绿真彩",
        "绿红真彩"
    };

    public void SetInfoColor(int infoState)
    {
        if(colorMode == 0)
        {
            GUI.color = Color.white;
        }
        else if (colorMode == 1)
        {
            if (infoState == 0)
            {
                GUI.color = Color.white;
            }
            else if (infoState == 1)
            {
                GUI.color = Color.red;
            }
            else if (infoState == 2)
            {
                GUI.color = Color.green;
            }
        }
        else if (colorMode == 2)
        {
            if (infoState == 0)
            {
                GUI.color = Color.white;
            }
            else if (infoState == 1)
            {
                GUI.color = Color.green;
            }
            else if (infoState == 2)
            {
                GUI.color = Color.red;
            }
        }

       
    }


    [MenuItem ("BuildEditor/BuildWindow")]
    public static void InstanceEditor()
    {
        ReadGameDataInfo();

        if (thisWindow == null)
        {
            thisWindow = EditorWindow.GetWindow(typeof(BuildWindowEditor)) as BuildWindowEditor;
            thisWindow.minSize = new Vector2(630,500);
            thisWindow.maxSize = new Vector2(630, 500);
        }
        
        thisWindow.Show();        
    }


    void OnGUI()
    {
        GetSetting();

        GUI.BeginGroup(new Rect(0, 0, 100, 80));
        {
            SetInfoColor(0);
            EditorGUI.HelpBox(new Rect(0, 0, 100, 80), "", MessageType.None);
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 0, 100, 20), "运营选择", MessageType.None);
            SetInfoColor(2);
            运营商 platformSelect = (运营商)EditorGUI.EnumPopup(new Rect(0, 23, 100, 20), GameBuildSettingSelect.platformSelect);
            if(platformSelect != GameBuildSettingSelect.platformSelect)
            {
                GameBuildSettingSelect.platformSelect = platformSelect;
                ReadGameDataInfo();
            }
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 40, 100, 20), "当前版本：" + GameBuildSettingSelect.currentVersion, MessageType.None);
            SetInfoColor(2);
            EditorGUI.HelpBox(new Rect(0, 60, 50, 20), "输出版本：", MessageType.None);
            SetInfoColor(2);
            GameBuildSettingSelect.outVersion = EditorGUI.TextField(new Rect(50, 60, 50, 19), GameBuildSettingSelect.outVersion);
            SetInfoColor(0);
        }
        GUI.EndGroup();


        GUI.BeginGroup(new Rect(110, 0, 200, 80));
        {
            SetInfoColor(0);
            EditorGUI.HelpBox(new Rect(0, 0, 200, 80), "", MessageType.None);
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 0, 200, 20), "打包流程设定", MessageType.None);


            colorMode = EditorGUI.IntPopup(new Rect(120, 2, 80, 23), colorMode, colorModeName, colorAry);
            SetInfoColor(2);
            EditorGUI.HelpBox(new Rect(0, 20, 50, 20), "编译输出", MessageType.None);
            GameBuildSettingSelect.needBuild = EditorGUI.Toggle(new Rect(50, 20, 10, 20), GameBuildSettingSelect.needBuild);
            EditorGUI.HelpBox(new Rect(70, 20, 50, 20), "拷贝资源", MessageType.None);
            bool needUpAsset = EditorGUI.Toggle(new Rect(120, 20, 10, 20), GameBuildSettingSelect.needUpAsset);
            if(needUpAsset != GameBuildSettingSelect.needUpAsset)
            {
                GameBuildSettingSelect.needUpAsset = needUpAsset;
                if (needUpAsset == true)
                {
                    GameBuildSettingSelect.onlyPatch = !needUpAsset;
                }
            }

            EditorGUI.HelpBox(new Rect(140, 20, 40, 20), "补丁包", MessageType.None);
            bool onlyPatch = EditorGUI.Toggle(new Rect(180, 20, 10, 20), GameBuildSettingSelect.onlyPatch);
            if (onlyPatch != GameBuildSettingSelect.onlyPatch)
            {
                GameBuildSettingSelect.onlyPatch = onlyPatch;                

                if(onlyPatch)
                {
                    GameBuildSettingSelect.needUpAsset = false;
                    GameBuildSettingSelect.needBuild = false;
                }
            }

            SetInfoColor(2);
            EditorGUI.HelpBox(new Rect(0, 40, 40, 20), "签名", MessageType.None);
            GameBuildSettingSelect.bundleIdentifier = EditorGUI.TextField(new Rect(40, 40, 160, 20), GameBuildSettingSelect.bundleIdentifier);

            if (GUI.Button(new Rect(0, 60, 60, 20), "资源目录"))
            {
                string assetsFoloder = EditorUtility.OpenFolderPanel("选择需要拷贝的资源目录", "", "");

                if (!string.IsNullOrEmpty(assetsFoloder))
                {
                    GameBuildSettingSelect.assetsFoloder = assetsFoloder;
                    EditorPrefs.SetString("assetsFoloder", GameBuildSettingSelect.assetsFoloder);
                }            
            }
            GameBuildSettingSelect.assetsFoloder = EditorGUI.TextField(new Rect(60, 60, 120, 20), GameBuildSettingSelect.assetsFoloder);

            SetInfoColor(1);
            if (GUI.Button(new Rect(180, 60, 20, 20), ""))
            {
                EditorUtility.OpenWithDefaultApp(GameBuildSettingSelect.assetsFoloder);
                //BuildResource.ShowAndSelectFileInExplorer(GameBuildSettingSelect.assetsFoloder);
            }
        }
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, 90, 310, 50));
        {
            SetInfoColor(0);
            EditorGUI.HelpBox(new Rect(0, 0, 310, 50), "", MessageType.None);
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 0, 310, 20), "系统平台", MessageType.None);
            SetInfoColor(2);
            系统平台 systemPlatform = (系统平台)EditorGUI.EnumPopup(new Rect(0, 23, 100, 20), GameBuildSettingSelect.systemPlatform);
            if(systemPlatform != GameBuildSettingSelect.systemPlatform)
            {
                GameBuildSettingSelect.systemPlatform = systemPlatform;
                ReadGameDataInfo();
            }

            if (GUI.Button(new Rect(110, 20, 60, 20), "输出目录"))
            {
                string outFoloder = EditorUtility.OpenFolderPanel("选择需要输出根目录 各运营各独立一个目录", "", "");

                if (!string.IsNullOrEmpty(outFoloder))
                {
                    GameBuildSettingSelect.outFoloder = outFoloder;
                    EditorPrefs.SetString("outFoloder", GameBuildSettingSelect.outFoloder);
                    ReadGameDataInfo();
                }
            }
            GameBuildSettingSelect.outFoloder = EditorGUI.TextField(new Rect(170, 20, 120, 19), GameBuildSettingSelect.outFoloder);

            SetInfoColor(1);
            if (GUI.Button(new Rect(290, 20, 20, 20), ""))
            {
                EditorUtility.OpenWithDefaultApp(GameBuildSettingSelect.outFoloder);
                //BuildResource.ShowAndSelectFileInExplorer(GameBuildSettingSelect.outFoloder);
            }
            SetInfoColor(2);
            EditorGUI.HelpBox(new Rect(0, 39, 310, 10), "", MessageType.None);
        }
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, 160, 310, 200));
        {
            SetInfoColor(0);
            EditorGUI.HelpBox(new Rect(0, 0, 310, 200), "", MessageType.None);
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 0, 310, 20), "地址文件", MessageType.None);
            GameBuildSettingSelect.assetsServerAddr = EditorGUI.TextArea(new Rect(0, 20, 310, 180), GameBuildSettingSelect.assetsServerAddr);
        }
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(320, 0, 310, 139));
        {
            SetInfoColor(0);
            EditorGUI.HelpBox(new Rect(0, 0, 310, 139), "", MessageType.None);
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 0, 310, 20), "通用设置", MessageType.None);
            SetInfoColor(2);
            EditorGUI.HelpBox(new Rect(0, 20, 120, 18), "ApiCompatibilityLevel", MessageType.None);
            ApiCompatibilityLevel apiCompatibilityLev = (ApiCompatibilityLevel)EditorGUI.EnumPopup(new Rect(120, 20, 120, 23), GameBuildSettingSelect.apiCompatibilityLevel);
            if(apiCompatibilityLev != GameBuildSettingSelect.apiCompatibilityLevel)
            {
                GameBuildSettingSelect.apiCompatibilityLevel = apiCompatibilityLev;
                PlayerSettings.apiCompatibilityLevel = apiCompatibilityLev;
            }

            EditorGUI.HelpBox(new Rect(0, 40, 120, 18), "StrippingLevel", MessageType.None);
            StrippingLevel stripLevel = (StrippingLevel)EditorGUI.EnumPopup(new Rect(120, 40, 120, 23), GameBuildSettingSelect.strippingLevel);
            if (stripLevel != GameBuildSettingSelect.strippingLevel)
            {
                GameBuildSettingSelect.strippingLevel = stripLevel;
                PlayerSettings.strippingLevel = stripLevel;
            }

            EditorGUI.HelpBox(new Rect(0, 60, 120, 18), "BundleVersion", MessageType.None);
            string bundleVer = EditorGUI.TextField(new Rect(120, 60, 120, 19), GameBuildSettingSelect.bundleVersion);
            if (bundleVer != GameBuildSettingSelect.bundleVersion)
            {
                GameBuildSettingSelect.bundleVersion = bundleVer;
                PlayerSettings.bundleVersion = bundleVer;
            }

            EditorGUI.HelpBox(new Rect(0, 80, 120, 18), "ShortBundleVersion", MessageType.None);
            string shortbundleVer = EditorGUI.TextField(new Rect(120, 80, 120, 19), GameBuildSettingSelect.shortBundleVersion);
            if (bundleVer != GameBuildSettingSelect.bundleVersion)
            {
                GameBuildSettingSelect.shortBundleVersion = shortbundleVer;
                PlayerSettings.bundleVersion = shortbundleVer;
            }

            EditorGUI.HelpBox(new Rect(0, 100, 120, 18), "AccelerometerFrequency", MessageType.None);
            int accelerometerFrequency = EditorGUI.IntPopup(new Rect(120, 100, 120, 23), GameBuildSettingSelect.accelerometerFrequency, frameName,frameArray);
            if (accelerometerFrequency != GameBuildSettingSelect.accelerometerFrequency)
            {
                GameBuildSettingSelect.accelerometerFrequency = accelerometerFrequency;
                PlayerSettings.accelerometerFrequency = accelerometerFrequency;
            }


            EditorGUI.HelpBox(new Rect(0, 120, 120, 18), "AotOptions", MessageType.None);
            string aotOptions = EditorGUI.TextField(new Rect(120, 120, 120, 19), GameBuildSettingSelect.aotOptions);
            if (aotOptions != GameBuildSettingSelect.aotOptions)
            {
                GameBuildSettingSelect.aotOptions = aotOptions;
                PlayerSettings.aotOptions = aotOptions;
            }

            EditorGUI.HelpBox(new Rect(245, 20, 65, 118), "RunInBack", MessageType.None);
            bool runInBackGround = EditorGUI.Toggle(new Rect(270, 70, 20, 20), GameBuildSettingSelect.runInBackGround);
            if(runInBackGround != GameBuildSettingSelect.runInBackGround)
            {
                GameBuildSettingSelect.runInBackGround = runInBackGround;
                PlayerSettings.runInBackground = runInBackGround;
            }
        }
        GUI.EndGroup();


        if(GameBuildSettingSelect.systemPlatform == 系统平台.Android)
        {
            GUI.BeginGroup(new Rect(320, 160, 310, 200));
            {
                SetInfoColor(0);
                EditorGUI.HelpBox(new Rect(0, 0, 310, 200), "", MessageType.None);
                SetInfoColor(1);
                EditorGUI.HelpBox(new Rect(0, 0, 310, 20), "Android设置", MessageType.None);
                SetInfoColor(2);

                EditorGUI.HelpBox(new Rect(0, 20, 120, 18), "BundleVersionCode", MessageType.None);
                int bundleVersionCode_Android = EditorGUI.IntField(new Rect(120, 20, 120, 18), GameBuildSettingSelect.bundleVersionCode_Android);
                if(bundleVersionCode_Android != GameBuildSettingSelect.bundleVersionCode_Android)
                {
                    GameBuildSettingSelect.bundleVersionCode_Android = bundleVersionCode_Android;
                    PlayerSettings.Android.bundleVersionCode = bundleVersionCode_Android;
                }

                EditorGUI.HelpBox(new Rect(0, 40, 120, 18), "MinSdkVersion", MessageType.None);
                AndroidSdkVersions androidSdkVer = (AndroidSdkVersions)EditorGUI.EnumPopup(new Rect(120, 40, 120, 23), GameBuildSettingSelect.androidSdkVersion_Android);
                if(androidSdkVer != GameBuildSettingSelect.androidSdkVersion_Android)
                {
                    GameBuildSettingSelect.androidSdkVersion_Android = androidSdkVer;
                    PlayerSettings.Android.minSdkVersion = androidSdkVer;
                }

                EditorGUI.HelpBox(new Rect(0, 60, 120, 18), "TargetDevice", MessageType.None);
                AndroidArchitecture androidTarget = (AndroidArchitecture)EditorGUI.EnumPopup(new Rect(120, 60, 120, 23), GameBuildSettingSelect.targetDevice_Android);
                if (androidTarget != GameBuildSettingSelect.targetDevice_Android)
                {
                    GameBuildSettingSelect.targetDevice_Android = androidTarget;
                    PlayerSettings.Android.targetArchitectures = androidTarget;
                }

                EditorGUI.HelpBox(new Rect(0, 80, 120, 18), "TargetGlesGraphics", MessageType.None);
                TargetGlesGraphics androidGraphics = (TargetGlesGraphics)EditorGUI.EnumPopup(new Rect(120, 80, 120, 23), GameBuildSettingSelect.targetGlesGraphics_Android);
                if (androidGraphics != GameBuildSettingSelect.targetGlesGraphics_Android)
                {
                    GameBuildSettingSelect.targetGlesGraphics_Android = androidGraphics;
                    PlayerSettings.targetGlesGraphics = androidGraphics;
                }

                EditorGUI.HelpBox(new Rect(0, 100, 120, 18), "AndroidPreferredInstallLocation", MessageType.None);
                AndroidPreferredInstallLocation androidPreferredInstall = (AndroidPreferredInstallLocation)EditorGUI.EnumPopup(new Rect(120, 100, 120, 23), GameBuildSettingSelect.androidPreferredInstallLocation_Android);
                if (androidPreferredInstall != GameBuildSettingSelect.androidPreferredInstallLocation_Android)
                {
                    GameBuildSettingSelect.androidPreferredInstallLocation_Android = androidPreferredInstall;
                    PlayerSettings.Android.preferredInstallLocation = androidPreferredInstall;
                }

                EditorGUI.HelpBox(new Rect(0, 120, 120, 18), "forceSDCardPermission", MessageType.None);
                bool forceSdCardPermission = EditorGUI.Toggle(new Rect(120, 120, 120, 23), GameBuildSettingSelect.forceSDCardPermission_Android);
                if (forceSdCardPermission != GameBuildSettingSelect.forceSDCardPermission_Android)
                {
                    GameBuildSettingSelect.forceSDCardPermission_Android = forceSdCardPermission;
                    PlayerSettings.Android.forceSDCardPermission = forceSdCardPermission;
                }

                EditorGUI.HelpBox(new Rect(0, 140, 120, 18), "ScriptingDefineSymbols", MessageType.None);
                string scriptingDefineSymbols = EditorGUI.TextField(new Rect(120, 140, 120, 19), GameBuildSettingSelect.scriptingDefineSymbols_Android);
                if (scriptingDefineSymbols != GameBuildSettingSelect.scriptingDefineSymbols_Android)
                {
                    GameBuildSettingSelect.scriptingDefineSymbols_Android = scriptingDefineSymbols;
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, scriptingDefineSymbols);
                }
                SetInfoColor(1);
                EditorGUI.HelpBox(new Rect(0, 160, 310, 40), "", MessageType.None);
            }
            GUI.EndGroup();
        }
        else if(GameBuildSettingSelect.systemPlatform == 系统平台.IOS)
        {
            GUI.BeginGroup(new Rect(320, 160, 310, 200));
            {
                SetInfoColor(0);
                EditorGUI.HelpBox(new Rect(0, 0, 310, 200), "", MessageType.None);
                SetInfoColor(1);
                EditorGUI.HelpBox(new Rect(0, 0, 310, 20), "IOS设置", MessageType.None);

                SetInfoColor(2);
                EditorGUI.HelpBox(new Rect(0, 20, 120, 18), "ScriptingBackend", MessageType.None);
                ScriptingImplementation scriptBanked = (ScriptingImplementation)EditorGUI.EnumPopup(new Rect(120, 20, 120, 23), GameBuildSettingSelect.scriptingBackend);
                if(scriptBanked != GameBuildSettingSelect.scriptingBackend)
                {
                    GameBuildSettingSelect.scriptingBackend = scriptBanked;
                    PlayerSettings.SetPropertyInt("ScriptingBackend", (int)scriptBanked, BuildTarget.iOS);
                }

                EditorGUI.HelpBox(new Rect(0, 40, 120, 18), "Architecture", MessageType.None);
                //iPhoneArchitecture iosArchitecture = (iPhoneArchitecture)
                    EditorGUI.EnumPopup(new Rect(120, 40, 120, 23), GameBuildSettingSelect.iosTargetDevice_Ios);
                //if (iosArchitecture != GameBuildSettingSelect.architecture_Ios)
                //{
                //    GameBuildSettingSelect.architecture_Ios = iosArchitecture;
                //    PlayerSettings.SetPropertyInt("Architecture", (int)iosArchitecture, BuildTarget.iOS);
                //}

                EditorGUI.HelpBox(new Rect(0, 60, 120, 18), "IosTargetVersion", MessageType.None);
                //iOSTargetOSVersion iosTargetOSVersion = (iOSTargetOSVersion)EditorGUI.EnumPopup(new Rect(120, 60, 120, 23), GameBuildSettingSelect.iosTargetVersion_Ios);
                //if (iosTargetOSVersion != GameBuildSettingSelect.iosTargetVersion_Ios)
                //{
                //    GameBuildSettingSelect.iosTargetVersion_Ios = iosTargetOSVersion;
                //    PlayerSettings.iOS.targetOSVersion = iosTargetOSVersion;
                //}

                EditorGUI.HelpBox(new Rect(0, 80, 120, 18), "TargetDevice", MessageType.None);
                iOSTargetDevice iosTargetDevice = (iOSTargetDevice)EditorGUI.EnumPopup(new Rect(120, 80, 120, 23), GameBuildSettingSelect.iosTargetDevice_Ios);
                if (iosTargetDevice != GameBuildSettingSelect.iosTargetDevice_Ios)
                {
                    GameBuildSettingSelect.iosTargetDevice_Ios = iosTargetDevice;
                    PlayerSettings.iOS.targetDevice = iosTargetDevice;
                }

                EditorGUI.HelpBox(new Rect(0, 100, 120, 18), "TargetResolution", MessageType.None);
                //iOSTargetResolution iosTargetResoulution = (iOSTargetResolution)EditorGUI.EnumPopup(new Rect(120, 100, 120, 23), GameBuildSettingSelect.iosTargetResoulution_Ios);
                //if (iosTargetResoulution != GameBuildSettingSelect.iosTargetResoulution_Ios)
                //{
                //    GameBuildSettingSelect.iosTargetResoulution_Ios = iosTargetResoulution;
                //    //PlayerSettings.iOS.targetResolution = iosTargetResoulution;
                //}

                EditorGUI.HelpBox(new Rect(0, 120, 120, 18), "ScriptCallOptimizationLevel", MessageType.None);
                ScriptCallOptimizationLevel scriptCallOptimizationLevel = (ScriptCallOptimizationLevel)EditorGUI.EnumPopup(new Rect(120, 120, 120, 23), GameBuildSettingSelect.ScriptCallOptimization_Ios);
                if (scriptCallOptimizationLevel != GameBuildSettingSelect.ScriptCallOptimization_Ios)
                {
                    GameBuildSettingSelect.ScriptCallOptimization_Ios = scriptCallOptimizationLevel;
                    PlayerSettings.iOS.scriptCallOptimization = scriptCallOptimizationLevel;
                }

                EditorGUI.HelpBox(new Rect(0, 140, 120, 18), "TargetIOSGraphics", MessageType.None);
                TargetIOSGraphics targetIOSGraphics = (TargetIOSGraphics)EditorGUI.EnumPopup(new Rect(120, 140, 120, 23), GameBuildSettingSelect.targetIOSGraphics_Ios);
                if (targetIOSGraphics != GameBuildSettingSelect.targetIOSGraphics_Ios)
                {
                    GameBuildSettingSelect.targetIOSGraphics_Ios = targetIOSGraphics;
                    PlayerSettings.targetIOSGraphics = targetIOSGraphics;
                }


                EditorGUI.HelpBox(new Rect(0, 160, 120, 18), "RequiresPersistentWiFi", MessageType.None);
                bool requiresPersistentWiFi = EditorGUI.Toggle(new Rect(120, 160, 120, 23), GameBuildSettingSelect.requiresPersistentWiFi_Ios);
                if (requiresPersistentWiFi != GameBuildSettingSelect.requiresPersistentWiFi_Ios)
                {
                    GameBuildSettingSelect.requiresPersistentWiFi_Ios = requiresPersistentWiFi;
                    PlayerSettings.iOS.requiresPersistentWiFi = requiresPersistentWiFi;
                }

                EditorGUI.HelpBox(new Rect(0, 180, 120, 18), "ScriptingDefineSymbols", MessageType.None);
                string scriptingDefineSymbols = EditorGUI.TextField(new Rect(120, 180, 120, 19), GameBuildSettingSelect.scriptingDefineSymbols_Ios);
                if (scriptingDefineSymbols != GameBuildSettingSelect.scriptingDefineSymbols_Ios)
                {
                    GameBuildSettingSelect.scriptingDefineSymbols_Ios = scriptingDefineSymbols;
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, scriptingDefineSymbols);
                }
            }
            GUI.EndGroup();
        }


        GUI.BeginGroup(new Rect(0, 380, 630, 60));
        {
            SetInfoColor(0);
            EditorGUI.HelpBox(new Rect(0, 0, 630, 60), "", MessageType.None);
            SetInfoColor(1);
            EditorGUI.HelpBox(new Rect(0, 0, 630, 20), "编译", MessageType.None);
            SetInfoColor(2);

            EditorGUI.HelpBox(new Rect(0, 30, 110, 18), "Development Build", MessageType.None);
            bool development = EditorGUI.Toggle(new Rect(110, 30, 20, 20), GameBuildSettingSelect.development);
            if(development != GameBuildSettingSelect.development)
            {
                GameBuildSettingSelect.development = development;
                EditorUserBuildSettings.development = development;
            }

            if(GameBuildSettingSelect.development)
            {
                EditorGUI.HelpBox(new Rect(170, 30, 110, 18), "Connect Profiler", MessageType.None);
                bool connectProfiler = EditorGUI.Toggle(new Rect(280, 30, 20, 20), GameBuildSettingSelect.connectProfiler);
                if (connectProfiler != GameBuildSettingSelect.connectProfiler)
                {
                    GameBuildSettingSelect.connectProfiler = connectProfiler;
                    EditorUserBuildSettings.connectProfiler = connectProfiler;
                }
            }

            EditorGUI.HelpBox(new Rect(340, 30, 110, 18), "Debugging", MessageType.None);
            bool allowDebugging = EditorGUI.Toggle(new Rect(450, 30, 20, 20), GameBuildSettingSelect.allowDebugging);
            if (allowDebugging != GameBuildSettingSelect.allowDebugging)
            {
                GameBuildSettingSelect.allowDebugging = allowDebugging;
                EditorUserBuildSettings.allowDebugging = allowDebugging;
            }
            SetInfoColor(1);
            if (GUI.Button(new Rect(500, 30, 130, 20),"开始打包"))
            {
                OutBuild();
            }
        }
        GUI.EndGroup();

        //所有GUI.Window 或 GUILayout.Window 必须在这里面
        //LoadMoudleWindowRect = GUI.Window(1, LoadMoudleWindowRect, DrawLoadMoudleWindow, "载入模型");    
        //window.Show();
        //EditorMoudleWindowRect = GUILayout.Window(2, EditorMoudleWindowRect, DrawSkillMoudleWindow, "编辑技能");
        //SceneRect = GUILayout.Window(3, SceneRect, DrawSceneWindow, "展示场景");
    }


    public static void ReadGameDataInfo()
    {
        GameBuildSettingSelect.assetsFoloder = EditorPrefs.GetString("assetsFoloder");
        GameBuildSettingSelect.outFoloder = EditorPrefs.GetString("outFoloder");
        GameBuildSettingSelect.platformSelectFoloder = GameBuildSettingSelect.outFoloder + "/" + GameBuildSettingSelect.platformSelect.ToString();

        if(GameBuildSettingSelect.systemPlatform == 系统平台.Android)
        {
            GameBuildSettingSelect.platformSelectOutSystemFoloder = GameBuildSettingSelect.platformSelectFoloder + "/AndroidOut";
        }
        else if(GameBuildSettingSelect.systemPlatform == 系统平台.IOS)
        {
            GameBuildSettingSelect.platformSelectOutSystemFoloder = GameBuildSettingSelect.platformSelectFoloder + "/IOSOut";
        }
        


        if (string.IsNullOrEmpty(GameBuildSettingSelect.assetsFoloder))
        {
            return;
        }

        if (string.IsNullOrEmpty(GameBuildSettingSelect.outFoloder))
        {
            return;
        }

        if(!Directory.Exists(GameBuildSettingSelect.platformSelectFoloder))
        {
            Directory.CreateDirectory(GameBuildSettingSelect.platformSelectFoloder);
        }

        if (!Directory.Exists(GameBuildSettingSelect.platformSelectFoloder + "/AndroidOut"))
        {
            Directory.CreateDirectory(GameBuildSettingSelect.platformSelectFoloder + "/AndroidOut");
        }

        if (!Directory.Exists(GameBuildSettingSelect.platformSelectFoloder + "/IOSOut"))
        {
            Directory.CreateDirectory(GameBuildSettingSelect.platformSelectFoloder + "/IOSOut");
        }


        GameBuildSettingSelect.serverAddrPath = GameBuildSettingSelect.platformSelectOutSystemFoloder + "/ServerAddress.text";
        GameBuildSettingSelect.versionPath = GameBuildSettingSelect.platformSelectOutSystemFoloder + "/version.text";
        GameBuildSettingSelect.resourceVersionPath = GameBuildSettingSelect.platformSelectOutSystemFoloder + "/ResourceVersion.text";

        if (!File.Exists(GameBuildSettingSelect.serverAddrPath))
        {
            File.WriteAllBytes(GameBuildSettingSelect.serverAddrPath, Encoding.UTF8.GetBytes(""));
        }

        if (!File.Exists(GameBuildSettingSelect.versionPath))
        {
            File.WriteAllBytes(GameBuildSettingSelect.versionPath, Encoding.UTF8.GetBytes("0.0.0"));
        }

        byte[] versionBuf = File.ReadAllBytes(GameBuildSettingSelect.versionPath);
        GameBuildSettingSelect.currentVersion = System.Text.Encoding.UTF8.GetString(versionBuf);
        if(string.IsNullOrEmpty(GameBuildSettingSelect.currentVersion))
        {
            GameBuildSettingSelect.currentVersion = "0.0.0";
        }
        GameBuildSettingSelect.outVersion = GameBuildSettingSelect.currentVersion;
    
        byte[] serAddrBuf = File.ReadAllBytes(GameBuildSettingSelect.serverAddrPath);
        GameBuildSettingSelect.assetsServerAddr = System.Text.Encoding.UTF8.GetString(CompressEncryption.UnEncryption(serAddrBuf));
    }

    public static void GetSetting()
    {
        //GameBuildSettingSelect.bundleIdentifier = PlayerSettings.bundleIdentifier;

        GameBuildSettingSelect.apiCompatibilityLevel = PlayerSettings.apiCompatibilityLevel;

        GameBuildSettingSelect.strippingLevel = PlayerSettings.strippingLevel;

        GameBuildSettingSelect.bundleVersion = PlayerSettings.bundleVersion;

        //GameBuildSettingSelect.shortBundleVersion = PlayerSettings.shortBundleVersion;

        GameBuildSettingSelect.runInBackGround = PlayerSettings.runInBackground;

        GameBuildSettingSelect.aotOptions = PlayerSettings.aotOptions;

        GameBuildSettingSelect.accelerometerFrequency = PlayerSettings.accelerometerFrequency;


        //////////Android
        GameBuildSettingSelect.bundleVersionCode_Android = PlayerSettings.Android.bundleVersionCode;
        GameBuildSettingSelect.androidSdkVersion_Android = PlayerSettings.Android.minSdkVersion;
        GameBuildSettingSelect.targetDevice_Android = PlayerSettings.Android.targetArchitectures;
        GameBuildSettingSelect.targetGlesGraphics_Android = PlayerSettings.targetGlesGraphics;
        GameBuildSettingSelect.androidPreferredInstallLocation_Android = PlayerSettings.Android.preferredInstallLocation;
        GameBuildSettingSelect.forceSDCardPermission_Android = PlayerSettings.Android.forceSDCardPermission;
        GameBuildSettingSelect.scriptingDefineSymbols_Android = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

        //////////Ios
        GameBuildSettingSelect.targetIOSGraphics_Ios = PlayerSettings.targetIOSGraphics;
        GameBuildSettingSelect.ScriptCallOptimization_Ios = PlayerSettings.iOS.scriptCallOptimization;
        GameBuildSettingSelect.requiresPersistentWiFi_Ios = PlayerSettings.iOS.requiresPersistentWiFi;
        GameBuildSettingSelect.iosTargetDevice_Ios = PlayerSettings.iOS.targetDevice;
        //GameBuildSettingSelect.iosTargetVersion_Ios = PlayerSettings.iOS.targetOSVersion;
        //GameBuildSettingSelect.iosTargetResoulution_Ios = PlayerSettings.iOS.targetResolution;
        GameBuildSettingSelect.scriptingBackend = (ScriptingImplementation)PlayerSettings.GetPropertyInt("ScriptingBackend",BuildTargetGroup.iOS);
        //GameBuildSettingSelect.architecture_Ios = (iPhoneArchitecture)PlayerSettings.GetPropertyInt("Architecture", BuildTargetGroup.IOS);
        //GameBuildSettingSelect.scriptingDefineSymbols_Ios = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone);

        GameBuildSettingSelect.development = EditorUserBuildSettings.development;
        GameBuildSettingSelect.connectProfiler = EditorUserBuildSettings.connectProfiler;        
        GameBuildSettingSelect.allowDebugging = EditorUserBuildSettings.allowDebugging;
    }


    public static void SaveInfor()
    {
        File.WriteAllBytes(GameBuildSettingSelect.serverAddrPath, CompressEncryption.Encryption(Encoding.UTF8.GetBytes(GameBuildSettingSelect.assetsServerAddr)));
        File.WriteAllBytes(GameBuildSettingSelect.versionPath, Encoding.UTF8.GetBytes(GameBuildSettingSelect.outVersion));
    }

    public static void OutBuild()
    {
        SaveInfor();

        string PlatformFloder = "";
        if (GameBuildSettingSelect.systemPlatform == 系统平台.Android)
        {
            PlatformFloder = "/android";
        }
        else if (GameBuildSettingSelect.systemPlatform == 系统平台.IOS)
        {
            PlatformFloder = "/ios";
        }

        //拷贝资源
        if (GameBuildSettingSelect.needUpAsset)
        {
            //bool sucess = ResourceBuild.AutoCopyAllResource(GameBuildSettingSelect.versionPath, GameBuildSettingSelect.serverAddrPath, GameBuildSettingSelect.resourceVersionPath, GameBuildSettingSelect.assetsFoloder, Application.streamingAssetsPath);
            //if(!sucess)
            //{
            //    return;
            //}
        }


        if(GameBuildSettingSelect.onlyPatch)
        {
            string patchPath = GameBuildSettingSelect.platformSelectFoloder + PlatformFloder + "Patch";
            //BuildResource.MakePatchResource(GameBuildSettingSelect.versionPath, GameBuildSettingSelect.resourceVersionPath, GameBuildSettingSelect.assetsFoloder, patchPath, GameBuildSettingSelect.platformSelectFoloder + "/SpawnPatchGeneral");


            string scriptSrc = patchPath + "/Script";
            if (Directory.Exists(scriptSrc))
            {
                //这里copy
                CopyDirectory(scriptSrc,GameBuildSettingSelect.platformSelectOutSystemFoloder);
            }

            string platformSrc = patchPath + PlatformFloder;
            if (Directory.Exists(platformSrc))
            {
                //这里copy
                CopyDirectory(platformSrc, GameBuildSettingSelect.platformSelectOutSystemFoloder);
            }
        }


        if (GameBuildSettingSelect.needBuild)
        {
            Build();
        }


        if (Directory.Exists(GameBuildSettingSelect.platformSelectOutSystemFoloder + "/Script"))
        {
            Directory.Delete(GameBuildSettingSelect.platformSelectOutSystemFoloder + "/Script", true);            
        }

        if (Directory.Exists(GameBuildSettingSelect.platformSelectOutSystemFoloder + PlatformFloder))
        {
            Directory.Delete(GameBuildSettingSelect.platformSelectOutSystemFoloder + PlatformFloder, true);
        }

        EditorUtility.DisplayProgressBar("输出/Script更新资源配置", "正在拷贝大概需要等待1分钟", 1.0f);
        CopyDirectory(Application.streamingAssetsPath + "/Script", GameBuildSettingSelect.platformSelectOutSystemFoloder);
        EditorUtility.DisplayProgressBar("输出"+ PlatformFloder + "更新资源配置", "正在拷贝大概需要等待2-3分钟", 1.0f);
        CopyDirectory(Application.streamingAssetsPath + PlatformFloder, GameBuildSettingSelect.platformSelectOutSystemFoloder);
        EditorUtility.ClearProgressBar();
        EditorUtility.OpenWithDefaultApp(GameBuildSettingSelect.platformSelectOutSystemFoloder);
    }



    public static void Build()
    {
        List<string> names = new List<string>();
        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;
            if (e.enabled)
                names.Add(e.path);
        }

        if (GameBuildSettingSelect.systemPlatform == 系统平台.Android)
        {
            BuildPipeline.BuildPlayer(names.ToArray(), GameBuildSettingSelect.platformSelectOutSystemFoloder + "/" + PlayerSettings.productName + GameBuildSettingSelect.outVersion + ".apk", BuildTarget.Android, BuildOptions.None);
        }
        else if(GameBuildSettingSelect.systemPlatform == 系统平台.IOS)
        {
            if (GameBuildSettingSelect.scriptingBackend == ScriptingImplementation.IL2CPP)
            {
                BuildPipeline.BuildPlayer(names.ToArray(), GameBuildSettingSelect.platformSelectOutSystemFoloder + "/" + PlayerSettings.productName + GameBuildSettingSelect.outVersion, BuildTarget.iOS, BuildOptions.None);
            }
            else
            {
                BuildPipeline.BuildPlayer(names.ToArray(), GameBuildSettingSelect.platformSelectOutSystemFoloder + "/" + PlayerSettings.productName + GameBuildSettingSelect.outVersion, BuildTarget.iOS, BuildOptions.None);
            }            
        }
    }


    /// <summary>
    /// 拷贝文件夹
    /// </summary>
    /// <param name="srcdir"></param>
    /// <param name="desdir"></param>
    private static void CopyDirectory(string srcdir, string desdir)
    {
        string folderName = srcdir.Substring(srcdir.LastIndexOf("/") + 1);

        string desfolderdir = desdir + "/" + folderName;

        if (desdir.LastIndexOf("/") == (desdir.Length - 1))
        {
            desfolderdir = desdir + folderName;
        }
        string[] filenames = Directory.GetFileSystemEntries(srcdir);
        int loopCount = 0;
        foreach (string file in filenames)// 遍历所有的文件和目录
        {
            string filePath = file.Replace("\\", "/");
            if (Directory.Exists(filePath))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
            {

                string currentdir = desfolderdir + "/" + filePath.Substring(filePath.LastIndexOf("/") + 1);
                if (!Directory.Exists(currentdir))
                {
                    Directory.CreateDirectory(currentdir);
                }

                CopyDirectory(filePath, desfolderdir);
            }

            else // 否则直接copy文件
            {
                string srcfileName = filePath.Substring(filePath.LastIndexOf("/") + 1);

                srcfileName = desfolderdir + "/" + srcfileName;


                if (!Directory.Exists(desfolderdir))
                {
                    Directory.CreateDirectory(desfolderdir);
                }


                File.Copy(filePath, srcfileName,true);
            }
        }
    }
}



public class GameBuildSettingSelect
{
    /// <summary>
    /// 编辑器中的须臾
    /// </summary>
    public  static 运营商 platformSelect = 运营商.内部网;
    public static 系统平台 systemPlatform = 系统平台.Android;
    public static string currentVersion = "0.0.0";
    public static string outVersion = "0.0.0";
    public static string assetsServerAddr = "";
    public static string assetsFoloder = "";
    public static string outFoloder = "";
    public static bool needBuild = true;
    public static bool needUpAsset = true;
    public static bool onlyPatch = false;

    public static string bundleIdentifier = "xx.xx.xx";
    public static ApiCompatibilityLevel apiCompatibilityLevel;
    public static StrippingLevel strippingLevel;
    public static string bundleVersion;
    public static string shortBundleVersion;
    public static bool runInBackGround;
    public static string aotOptions;
    public static int accelerometerFrequency;

    ///Android
    public static int bundleVersionCode_Android;
    public static AndroidSdkVersions androidSdkVersion_Android;
    public static AndroidArchitecture targetDevice_Android;
    public static TargetGlesGraphics targetGlesGraphics_Android;
    public static AndroidPreferredInstallLocation androidPreferredInstallLocation_Android;
    public static bool forceSDCardPermission_Android;
    public static string scriptingDefineSymbols_Android;
    /// IOS
    public static TargetIOSGraphics targetIOSGraphics_Ios;
    public static ScriptCallOptimizationLevel ScriptCallOptimization_Ios;
    public static bool requiresPersistentWiFi_Ios;
    public static iOSTargetDevice iosTargetDevice_Ios;
    //public static iOSTargetOSVersion iosTargetVersion_Ios;
    //public static iOSTargetResolution iosTargetResoulution_Ios;
    public static ScriptingImplementation scriptingBackend;
    public static string scriptingDefineSymbols_Ios;


    public static bool development;
    public static bool connectProfiler;
    public static bool allowDebugging;

    /// <summary>
    /// 平台的对应输出目录
    /// </summary>
    public static string platformSelectFoloder;
    public static string platformSelectOutSystemFoloder;

    public static string serverAddrPath;
    public static string versionPath;
    public static string resourceVersionPath;
}

#endif