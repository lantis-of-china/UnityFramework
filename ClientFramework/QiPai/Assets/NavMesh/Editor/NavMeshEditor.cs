using UnityEngine;
using System.Collections;

using UnityEditor;
using System.Collections.Generic;
using System;

public class NavMeshEditor : EditorWindow
{
    private static NavMeshEditor windowInstance;
    private SceneView.OnSceneFunc _delegate;

    private GameObject meshRoot;
    private Mesh MESH;
    private MeshFilter meshFilter;

    private static Material mat_1;
    private Material Mat_1
    {
        get
        {
            if (mat_1 == null)
            {
                mat_1 = new Material(Shader.Find("Sprites/Default"));
            }

            return mat_1;
        }
    }


    private NavTriangl recordLastTriangl;
    private List<NavMeshVector3> createTriang = new List<NavMeshVector3>();
    private List<NavTriangl> navTrianglList = new List<NavTriangl>();
    public bool isEditOpen = false;
    public bool isQuickOpen = true;
    string[] layerArray = null;
    string layer = "";
    int selectLayer = 0;
    [UnityEditor.MenuItem("NavSeeker/Editor")]
    static void OpenNavEditor()
    {
        mat_1 = new Material(Shader.Find("Sprites/Default"));
        Rect wr = new Rect(0, 0, 200, 300);

        if (windowInstance == null)
        {
            windowInstance = (NavMeshEditor)UnityEditor.EditorWindow.GetWindow(typeof(NavMeshEditor));
            windowInstance.Show();

            windowInstance._delegate = OnSceneFunc;
            SceneView.onSceneGUIDelegate += windowInstance._delegate;
        }
    }

    void OnDestroy()
    {
        if (_delegate != null)
        {
            SceneView.onSceneGUIDelegate -= _delegate;
        }

        if(meshRoot != null)
        {
            GameObject.DestroyImmediate(meshRoot);
        }
    }

    static public void OnSceneFunc(SceneView sceneView)
    {
        windowInstance.UpdateSence(sceneView);
    }

    void UpdateSence(SceneView sceneView)
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        if (Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(controlID);
        }
        if (isEditOpen)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                Vector3 touchPos = Event.current.mousePosition;
                Ray touchRay = HandleUtility.GUIPointToWorldRay(touchPos);
                RaycastHit hitInfo;
                if (Physics.Raycast(touchRay, out hitInfo, 10000,  1 << LayerMask.NameToLayer(layer)))
                {

                    if (hitInfo.collider != null)
                    {
                        NavMeshVector3 navMeshPos = new NavMeshVector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);

                        if (isQuickOpen && navTrianglList.Count > 0 && recordLastTriangl == null)
                        {
                            ///选择一个快捷关联三角
                            recordLastTriangl = NavSeeker.GetPosAtNavTriangl(navTrianglList, navMeshPos);
                            return;
                        }

                        if(isQuickOpen && recordLastTriangl != null)
                        {
                            ///加入一条边的点 量
                            NavMeshEdge addEdge = recordLastTriangl.GetNearEdge(navMeshPos);
                            createTriang.Add(addEdge.pos1);
                            createTriang.Add(addEdge.pos2);
                        }


                        NavTriangl touchAtNav = NavSeeker.GetPosAtNavTriangl(navTrianglList, navMeshPos);
                        if (touchAtNav == null)
                        {
                            createTriang.Add(navMeshPos);
                        }

                        if (createTriang.Count >= 3)
                        {
                            recordLastTriangl = NavTriangl.CreateNavTriangl(createTriang[0], createTriang[1], createTriang[2]);

                            navTrianglList.Add(recordLastTriangl);
                            ///创建三角
                            createTriang.Clear();
                        }
                    }
                }

                windowInstance.Repaint();
            }
        }

        SceneView.RepaintAll();
    }


    void Update()
    {
        if(meshRoot == null)
        {
            meshRoot = new GameObject("meshRoot");//UnityEditor.EditorUtility.CreateGameObjectWithHideFlags("meshRoot", HideFlags.HideAndDontSave);
            meshFilter = meshRoot.AddComponent<MeshFilter>();
            MeshRenderer mr = meshRoot.AddComponent<MeshRenderer>();
            mr.material = Mat_1;
        }
        
        UpDrawMeshPos();
    }

    static int recordCount = 0;
    public void UpDrawMeshPos()
    {
        if (MESH == null)
        {
            MESH = new Mesh();
            MESH.name = "Triangl";
        }

        if (recordCount != navTrianglList.Count)
        {
            recordCount = navTrianglList.Count;

            Vector3[] vertices = new Vector3[navTrianglList.Count * 3];
            int[] triangles = new int[navTrianglList.Count * 3];
            Vector3[] normals = new Vector3[vertices.Length];
            List<Vector3> posList = new List<Vector3>();
            posList.Clear();
            for (int loop = 0; loop < navTrianglList.Count; ++loop)
            {
                Vector3 v1 = new Vector3(navTrianglList[loop].pos1.x, navTrianglList[loop].pos1.y + 0.1f, navTrianglList[loop].pos1.z);
                Vector3 v2 = new Vector3(navTrianglList[loop].pos2.x, navTrianglList[loop].pos2.y + 0.1f, navTrianglList[loop].pos2.z);
                Vector3 v3 = new Vector3(navTrianglList[loop].pos3.x, navTrianglList[loop].pos3.y + 0.1f, navTrianglList[loop].pos3.z);
                posList.Add(v1);
                posList.Add(v2);
                posList.Add(v3);
                Vector3 vDir = Vector3.up;
                
                int useIndex = loop * 3;
                vertices[useIndex] = v1;
                triangles[useIndex] = useIndex;
                normals[useIndex] = vDir;

                useIndex++;
                vertices[useIndex] = v2;
                triangles[useIndex] = useIndex;
                normals[useIndex] = vDir;

                useIndex++;
                vertices[useIndex] = v3;
                triangles[useIndex] = useIndex;
                normals[useIndex] = vDir;
            }

            if (vertices.Length > 0)
            {               
                MESH.vertices = vertices;
                MESH.triangles = triangles;
                MESH.normals = normals;
                meshFilter.sharedMesh = MESH;
            }
            else
            {
                MESH.triangles = triangles;
                MESH.vertices = vertices;
                meshFilter.sharedMesh = null;
            }

        }


        GL.PushMatrix ();    
        GL.LoadOrtho ();
        GL.Begin( GL.LINES );    
        GL.Color( new Color(1,1,1,0.5f) ); 
        for (int loop = 0; loop < navTrianglList.Count; ++loop)
        {
            GL.Vertex3(navTrianglList[loop].pos1.x, navTrianglList[loop].pos1.y, navTrianglList[loop].pos1.z);    
            GL.Vertex3(navTrianglList[loop].pos2.x, navTrianglList[loop].pos2.y, navTrianglList[loop].pos2.z);  
            GL.Vertex3(navTrianglList[loop].pos3.x, navTrianglList[loop].pos3.y, navTrianglList[loop].pos3.z);            
        }
        GL.End();
        GL.PopMatrix(); 
    }


    /// 判断多边形是顺时针还是逆时针.  
    /// </summary>  
    /// <param name="points">所有的点</param>  
    /// <param name="isYAxixToDown">true:Y轴向下为正(屏幕坐标系),false:Y轴向上为正(一般的坐标系)</param>  
    /// <returns></returns>  
    public static bool IsClockwise(List<Vector3> points)
    {
        Vector2 dirAb = new Vector2(points[1].x - points[0].x, points[1].z - points[0].z);
        Vector2 dirAc = new Vector2(points[1].x - points[2].x, points[1].z - points[2].z);

        float clockwiseDir = dirAb.x * dirAc.y - dirAb.y * dirAc.x;

        if (clockwiseDir > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }  



    string[] GetLayerArray()
    {
        List<string> layerList = new List<string>();
        layerList.Add("Default");
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty it = tagManager.GetIterator();
        while (it.NextVisible(true))
        {
            if (it.name.StartsWith("User Layer") || it.name.StartsWith("Builtin Layer")|| it.name.StartsWith("layers"))
            {
                ////if (!string.IsNullOrEmpty(it.stringValue))
                ////{
                ////    layerList.Add(it.stringValue);
                ////}
            }
        }

        return layerList.ToArray();
    }

    void OnGUI()
    {
        SetColor(false);
        EditorGUILayout.LabelField("当前三角形数量：" + navTrianglList.Count);
        EditorGUILayout.LabelField("下一个三角形建立点数量：" + createTriang.Count);

        SetColor(true);
        if (layerArray == null)
        {
            layerArray = GetLayerArray();
        }
        selectLayer = EditorGUILayout.Popup("选择地形层", selectLayer, layerArray);
        layer = layerArray[selectLayer];


        SetColor(!isEditOpen);
        if (GUILayout.Button(isEditOpen ? "关闭编辑" : "开启编辑",GUILayout.Height(40)))
        {
            isEditOpen = !isEditOpen;
        }
        if(isEditOpen)
        {
            SetColor(!isQuickOpen);
            if (GUILayout.Button(isQuickOpen?"关闭关联":"开启关联", GUILayout.Height(40)))
            {
                isQuickOpen = !isQuickOpen;

                if(!isQuickOpen)
                {
                    recordLastTriangl = null;
                }
            }
        }

        SetColor(true);
        if (GUILayout.Button("关联三角形", GUILayout.Height(40)))
        {

        }

        SetColor(true);
        if (GUILayout.Button("导出数据", GUILayout.Height(40)))
        {

        }

        SetColor(false);
        if (GUILayout.Button("清除数据", GUILayout.Height(40)))
        {
            recordLastTriangl = null;
            navTrianglList.Clear();
            createTriang.Clear();
        }
    }


    private void SetColor(bool general)
    {
        if (general)
        {
            GUI.color = Color.white;
            GUI.contentColor = Color.white;
        }
        else
        {
            GUI.color = Color.red;
            GUI.contentColor = Color.white;
        }
    }
}

