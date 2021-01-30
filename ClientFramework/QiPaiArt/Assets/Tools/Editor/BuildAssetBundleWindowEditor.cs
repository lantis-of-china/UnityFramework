using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildProjectSumName
{
    /// <summary>
    /// int 0-原封不动拷贝 1-每个都单独导出为Ab 2-全部合并导出为1个Ab 
    /// </summary>
    public static Dictionary<string, KeyValuePair<string, int>> sumRule = new Dictionary<string, KeyValuePair<string, int>>()
        {
            { "Config",new KeyValuePair<string,int>("*.xml", 0 ) },
            { "Font",new KeyValuePair<string,int>("*.ttf|*.TTF", 1 ) },
            { "Items",new KeyValuePair<string,int>("*.prefab", 1 ) },
            { "Sences",new KeyValuePair<string,int>("*.unity", 1 ) },
            { "UI",new KeyValuePair<string,int>("*.prefab", 1 ) },
            { "Sound",new KeyValuePair<string,int>("*.mp3|*.wav", 2 ) },
            { "ThingIcons",new KeyValuePair<string,int>("*.jpg|*.png", 2 ) },
        };

    public static bool HasRule(string num)
    {
        return sumRule.ContainsKey(num);
    }

    public static int GetSumState(string num)
    {
        return sumRule[num].Value;
    }

    public static KeyValuePair<string, int> GetSumData(string num)
    {
        return sumRule[num];
    }

    public static void ExportBuildProjectAll(List<BuildProjectSetting> buildSettingList)
    {
        for (var i = 0; i < buildSettingList.Count; ++i)
        {
            ExportBuildProject(buildSettingList[i]);
        }
    }

    private static void ExportBuildProject(BuildProjectSetting projectSetting)
    {
        if (projectSetting.drawSelect)
        {
            for (var i = 0; i < projectSetting.selectDictionary.Count; ++i)
            {
                var selectSumState = projectSetting.selectDictionary.ElementAt(i);

                if (selectSumState.Value)
                {
                    if (HasRule(selectSumState.Key))
                    {
                        var state = GetSumData(selectSumState.Key);

                        var sum = selectSumState.Key;

                        switch (state.Value)
                        {
                            case 0:
                                {
                                    //拷贝
                                    ResourceBuild.CopyToAll(projectSetting.projectName, projectSetting.projectPath, sum, state.Key);
                                    break;
                                }
                            case 1:
                                {
                                    //每一个单独导出
                                    ResourceBuild.ExporeAssetBundle(projectSetting.projectName, projectSetting.projectPath, sum, state.Key);
                                    break;
                                }
                            case 2:
                                {
                                    //全部合并导出
                                    ResourceBuild.ExporeAssetBundleAllToOne(projectSetting.projectName, projectSetting.projectPath, sum, state.Key);
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }    
}

public class BuildProjectSetting
{
    public string name;

    public string projectName;

    public string projectPath;

    public Dictionary<string, bool> selectDictionary = new System.Collections.Generic.Dictionary<string, bool>();

    public bool drawSelect;
}

public class BuildAssetBundleEditor : EditorWindow
{
    private static BuildAssetBundleEditor thisWindow;

    [MenuItem("Tools/BuildAssetBundleWindow")]
    public static void InstanceEditor()
    {
        if (thisWindow == null)
        {
            thisWindow = EditorWindow.GetWindow(typeof(BuildAssetBundleEditor)) as BuildAssetBundleEditor;

            thisWindow.minSize = new Vector2(520, 320);

            thisWindow.maxSize = new Vector2(520, 320);
        }

        thisWindow.Show();
    }

    #region 编辑器参数
    private Rect mainGroupRect = new Rect(10, 10, 500, 300);

    private Vector2 mainViewScrollPos = new Vector2(0, 30);

    private float spaceSize = 20.0f;

    private bool selectAll = false;
    #endregion 编辑器参数

    public List<BuildProjectSetting> buildSettingList = new List<BuildProjectSetting>();

    private void OnEnable()
    {
        buildSettingList = GetAllProjectSetting();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(mainGroupRect, "");
        {
            GUILayout.Box("LantisFramework导出框架 Version 1.0");

            GUILayout.Space(5.0f);

            if (GUILayout.Toggle(selectAll, "全选 项目详情") != selectAll)
            {
                selectAll = !selectAll;

                SetAllSelectState(selectAll);
            }

            mainViewScrollPos = GUILayout.BeginScrollView(mainViewScrollPos);

            for (var i = 0; i < buildSettingList.Count; ++i)
            {
                DrawProjectItem(buildSettingList[i]);

                GUILayout.Space(5.0f);
            }

            GUILayout.EndScrollView();

            GUILayout.Space(spaceSize);

            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(420);

                if (GUILayout.Button("开始导出"))
                {
                    BuildProjectSumName.ExportBuildProjectAll(buildSettingList);
                }

                GUILayout.Space(20);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
    }

    #region 编辑器相关
    private void DrawProjectItem(BuildProjectSetting projectSetting)
    {
        GUILayout.Box(projectSetting.name);

        if (GUILayout.Toggle(projectSetting.drawSelect, "选择") != projectSetting.drawSelect)
        {
            SetBuildProjectSettingSelectState(projectSetting, !projectSetting.drawSelect);
        }

        if (projectSetting.drawSelect)
        {
            for (var sum = 0; sum < projectSetting.selectDictionary.Count; ++sum)
            {
                var kvData = projectSetting.selectDictionary.ElementAt(sum);

                GUILayout.BeginHorizontal();

                GUILayout.Space(spaceSize);

                projectSetting.selectDictionary[kvData.Key] = GUILayout.Toggle(kvData.Value, kvData.Key);

                GUILayout.EndHorizontal();
            }
        }
    }


    #endregion 编辑器相关

    private void SetAllSelectState(bool select)
    {
        for (var i = 0; i < buildSettingList.Count; ++i)
        {
            SetBuildProjectSettingSelectState(buildSettingList[i], select);
        }
    }

    private void SetBuildProjectSettingSelectState(BuildProjectSetting buildSetting, bool select)
    {
        buildSetting.drawSelect = select;

        for (var sum = 0; sum < buildSetting.selectDictionary.Count; ++sum)
        {
            buildSetting.selectDictionary[buildSetting.selectDictionary.ElementAt(sum).Key] = select;
        }
    }

    private static List<BuildProjectSetting> GetAllProjectSetting()
    {
        var gameRootPath = Application.dataPath + "/Games";

        var gameFloders = System.IO.Directory.GetDirectories(gameRootPath);

        List<BuildProjectSetting> buildSettings = new List<BuildProjectSetting>();

        for (var i = 0; i < gameFloders.Length; ++i)
        {
            var buildProjectSetting = new BuildProjectSetting();

            buildSettings.Add(buildProjectSetting);

            buildProjectSetting.projectName = System.IO.Path.GetFileNameWithoutExtension(gameFloders[i]);

            buildProjectSetting.projectPath = gameFloders[i];

            buildProjectSetting.drawSelect = false;

            var prefabPath = gameFloders[i] + "/GamePrefab";

            if (Directory.Exists(prefabPath))
            {
                var fileName = $"{prefabPath}/name.txt";

                if (File.Exists(fileName))
                {
                    buildProjectSetting.name = File.ReadAllText(fileName);
                }
                else
                {
                    buildProjectSetting.name = buildProjectSetting.projectName;

                    File.WriteAllText(fileName,buildProjectSetting.name,System.Text.Encoding.UTF8);
                }

                var sumFloders = System.IO.Directory.GetDirectories(prefabPath);

                for (var sum = 0; sum < sumFloders.Length; ++sum)
                {
                    var sumName = System.IO.Path.GetFileNameWithoutExtension(sumFloders[sum]);

                    buildProjectSetting.selectDictionary.Add(sumName, false);
                }
            }
        }

        return buildSettings;
    }
}