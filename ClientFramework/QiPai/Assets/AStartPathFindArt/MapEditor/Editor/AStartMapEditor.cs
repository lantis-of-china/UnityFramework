using UnityEngine;
using System.Collections;
using UnityEditor;

public class AStartMapEditor : UnityEditor.EditorWindow
{
    private static float PathLenght= 10.0f;
    private static float PositionX = 0;
    private static float PositionY = 0;
    private static float PositionZ = 100.0f;
    private static int XCount = 10;
    private static int YCount = 10;
    private static bool Obselale = false;
    public static float MainNodeScale=10.0f;
    public static float OtherNodeScale = 10.0f;

    private static float SenceNearClipValue = 0.0f;
    private static float SenceFarClipValue = 3000.0f;
    private static MapEditorNodeRoot MapRootScript;


    [UnityEditor.MenuItem("FindPathEditor/CreateOrRefenceMap")]    
    /// <summary>
    /// 创建一组新的地图间隔数据
    /// </summary>
    static void CreateOrRefenceMap()
    {
        MapRootScript = GameObject.FindObjectOfType<MapEditorNodeRoot>() as MapEditorNodeRoot;

        if (MapRootScript == null)
        {
            OtherNodeScale = MainNodeScale = PathLenght / 100;
        }
        else
        {
            OtherNodeScale = MapRootScript.OtherNodeScale;

            MainNodeScale = MapRootScript.MainNodeScale;

            PathLenght = MapRootScript.PathLenght;

            XCount = MapRootScript.XCount;

            YCount = MapRootScript.YCount;
        }
        

        Rect wr = new Rect (0,0,200,300);


        AStartMapEditor window = (AStartMapEditor)UnityEditor.EditorWindow.GetWindowWithRect(typeof(AStartMapEditor), wr, true, "寻路地图创建");

        window.Show();

        //UnityEditor.EditorUtility.DisplayDialog("title", "messsage", "ok");
    }    

    [UnityEditor.MenuItem("FindPathEditor/SaveMapInfor")]
    static void SaveMapInfor()
    {
        //在场景中寻找MapEditorNodeRoot
       MapRootScript= GameObject.FindObjectOfType<MapEditorNodeRoot>() as MapEditorNodeRoot;

        if (MapRootScript == null) { Debug.Log("MapNull"); return; }

        string path = UnityEditor.EditorUtility.SaveFilePanel("Save AStartFindInfor", "", "New Infor", "AS"); 

        string ContenxtInfor=MapRootScript.SaveMapInfor();

        System.IO.FileStream  FileAs= new System.IO.FileStream(path, System.IO.FileMode.Create);
        
        System.IO.StreamWriter sw = new System.IO.StreamWriter(FileAs);
        //开始写入
        sw.Write(ContenxtInfor);
        //清空缓冲区
        //sw.Flush();
        //关闭流
        sw.Close();

        FileAs.Dispose();
        FileAs.Close();
    }

    /// <summary>
    /// 设置选中物体到对应的grid
    /// </summary>
    void SetSelectBuildToNode(bool isLink)
    {
        if (MapRootScript == null)
        {
            return;
        }

        for (int setLoop = 0; setLoop < UnityEditor.Selection.gameObjects.Length; ++setLoop)
        {
             GameObject selectObject = UnityEditor.Selection.gameObjects[setLoop];

             MapEditorNode mapEditorNode = MapRootScript.GetMapEditorNode(selectObject.transform.position);

             if (mapEditorNode != null)
             {
                if (isLink)
                {
                    selectObject.name = "staticBuild_" + mapEditorNode.X + "_" + mapEditorNode.Y;

                    mapEditorNode.hasTypeObject = selectObject;
                }
                else
                {
                    string[] spArray = selectObject.name.Split('_');

                    if(spArray.Length<2)
                    {
                        return;
                    }

                    string objectName = "staticBuild";

                    //for(int loop= spArray.Length-1;loop>=0;--loop)
                    //{
                    //    if(loop< spArray.Length-2)
                    //    {  
                    //        objectName = spArray[loop] + objectName;
                    //    }
                    //}

                    selectObject.name = objectName;

                    mapEditorNode.hasTypeObject = null;

                    mapEditorNode.hasObjectPath = string.Empty;
                }
             }
        }
    }

    /// <summary>
    /// 通过As文件数据初始化现有的MapEditor
    /// </summary>
    void ReadAsDataToMap()
    {
        MapRootScript = GameObject.FindObjectOfType<MapEditorNodeRoot>() as MapEditorNodeRoot;

        if (MapRootScript == null) { Debug.LogError("场景中找不到指定的 MapEditorNodeRoot"); }

        string path = UnityEditor.EditorUtility.OpenFilePanel("Open AStartFindInfor", "", "AS");

        string sources = System.IO.File.ReadAllText(path);

        Node[][] outMapInfor;

        float pathLenght;

        Vector2 v2f = MapEditorNodeHelp.InstanceMapDate(sources, out outMapInfor, out pathLenght);

        if (v2f.x <= 0 || v2f.y<=0 || outMapInfor == null || outMapInfor.GetLength(0) == 0 || outMapInfor.GetLength(1) == 0)
        {
            Debug.LogError("node 数据不正确");

            return;
        }

        PositionX = outMapInfor[0][0].NodePosition.x;
        PositionY = outMapInfor[0][0].NodePosition.y;
        PositionZ = outMapInfor[0][0].NodePosition.z;

        XCount = (int)v2f.x;

        YCount = (int)v2f.y;

        PathLenght = pathLenght;

        if (MapRootScript==null || MapRootScript.XCount != v2f.x || MapRootScript.YCount != v2f.y)
        {
            Debug.LogError("MapRootSctipt node 网格数量不一致 创建新的");

            CreateMap();
        }

        UpMapHeight();

        for (int loopX = 0; loopX < MapRootScript.XCount; ++loopX)
        {
            for (int loopY = 0; loopY < MapRootScript.YCount; ++loopY)
            {
                Node mapNode = outMapInfor[loopX][loopY];

                MapEditorNode mapEditorNode = MapRootScript.MapNode2DArray[loopX].Node[loopY].GetComponent<MapEditorNode>();

                mapEditorNode.SetDataWithNode(mapNode);
            }
        }

        UpInfor();
    }
    
    /// <summary>
    /// 绘制界面
    /// </summary>
    void OnGUI()
    {
        if (PathLenght < 0)
        {
            PathLenght = 0;
        }

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("√建筑关联", GUILayout.Width(94)))
        {
            SetSelectBuildToNode(true);
        }       
       

        if (GUILayout.Button("×取消关联",GUILayout.Width(94)))
        {
            SetSelectBuildToNode(false);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        PositionX = UnityEditor.EditorGUILayout.FloatField("节点X", PositionX);
        PositionY = UnityEditor.EditorGUILayout.FloatField("节点Y", PositionY);
        PositionZ = UnityEditor.EditorGUILayout.FloatField("节点Y", PositionZ);
        PathLenght = UnityEditor.EditorGUILayout.FloatField("路径间隔", PathLenght);
        XCount = UnityEditor.EditorGUILayout.IntField("X路径点数量",XCount);
        YCount = UnityEditor.EditorGUILayout.IntField("Y路径点数量", YCount);
        Obselale = UnityEditor.EditorGUILayout.Toggle("创建障碍物", Obselale);
        MainNodeScale = UnityEditor.EditorGUILayout.FloatField("主节点缩放", MainNodeScale);
        OtherNodeScale = UnityEditor.EditorGUILayout.FloatField("副点缩放", OtherNodeScale);
        SenceNearClipValue = UnityEditor.EditorGUILayout.FloatField("Scene相机近剪裁", SenceNearClipValue);
        SenceFarClipValue = UnityEditor.EditorGUILayout.FloatField("Scene相机远剪裁", SenceFarClipValue);

        if (GUILayout.Button("创建/刷新地图从现有AS文件"))
        {
            ReadAsDataToMap();
        }
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("创建地图"))
        {
            CreateMap();
        }
        
        if (GUILayout.Button("刷新格局"))
        {
            UpInfor();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        if (GUILayout.Button("调整SceneCameraClip"))
        {
           UpSenceView();
        }
        GUILayout.EndVertical();
    }

    public void CreateMapCall()
    {
        CreateMap();

        UpInfor();
    }

    ///更新SenceView
    public void UpSenceView()
    {
        if (UnityEditor.SceneView.currentDrawingSceneView != null)
        {
            SenceNearClipValue = UnityEditor.SceneView.currentDrawingSceneView.camera.nearClipPlane = SenceNearClipValue;

            SenceFarClipValue = UnityEditor.SceneView.currentDrawingSceneView.camera.farClipPlane = SenceFarClipValue;

            UnityEditor.SceneView.currentDrawingSceneView.title = "地图编辑器模式";

            for (int loop = 0; loop < UnityEditor.SceneView.currentDrawingSceneView.camera.layerCullDistances.Length; ++loop)
            {
                UnityEditor.SceneView.currentDrawingSceneView.camera.layerCullDistances[loop] = SenceFarClipValue;
            }

            UnityEditor.SceneView.currentDrawingSceneView.Repaint();           
        }
    }
    
    /// <summary>
    /// 获取Map中的Root数据
    /// </summary>
    /// <returns></returns>
    public static bool GetMapNodeRoot()
    {
        MapRootScript = GameObject.FindObjectOfType<MapEditorNodeRoot>();

        if (MapRootScript == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 创建地图数据
    /// </summary>
    void CreateMap()
    {
        if (MapRootScript != null)
        {
            MapRootScript.OnDispos();
        }

        MapEditorNodeRoot[] findTypeArray = GameObject.FindObjectsOfType<MapEditorNodeRoot>();

        for (int loop = 0; loop < findTypeArray.Length; ++loop)
        {
            findTypeArray[loop].OnDispos();
        }

        //string LocaPath = "Assets/MapEditor/Resources/";
        string NoteRootPath = "MapNodeRoot";
        string NotePath = "MapNode";

        Object RootAss = Resources.Load(NoteRootPath, typeof(MapEditorNodeRoot));
        Object nodeAss = Resources.Load(NotePath, typeof(MapEditorNode));

        GameObject nodeRoot = (Instantiate(RootAss) as MapEditorNodeRoot).gameObject;//new GameObject(NoteRootPath);


        nodeRoot.name = "MapNodeRoot";

        nodeRoot.transform.localScale = new Vector3(1, 1, 1);

        nodeRoot.transform.position = new Vector3(PositionX, PositionY, PositionZ);

        MapRootScript = nodeRoot.GetComponent<MapEditorNodeRoot>();

        MapRootScript.CreateRoot();

        UpMapHeight();

        //MapRootScript.MapNode2DArray = new Transform[XCount, YCount];

        MapRootScript.XCount = XCount;
        MapRootScript.YCount = YCount;

        MapRootScript.PathLenght = PathLenght;

        int idAdd = 0;

        for (int Xindex = 0; Xindex < XCount; Xindex++)
        {
            MapNodeRow mMapNodeRow = new MapNodeRow();
            MapRootScript.MapNode2DArray.Add(mMapNodeRow);

            for (int Yindex = 0; Yindex < YCount; Yindex++)
            {
                GameObject node = (Instantiate(nodeAss) as MapEditorNode).gameObject;//new GameObject(NotePath);
                //加入行
                mMapNodeRow.Node.Add(node.transform);


                node.name = string.Format("Node_{0}_{1}", Xindex, Yindex);

                MeshRenderer rd = node.GetComponent<MeshRenderer>();

                if (rd != null)
                {
                    rd.sharedMaterial = MapEditorNode.Mat_3;
                }

                node.transform.parent = nodeRoot.transform;
                node.transform.localScale = new Vector3(1, 1, 1);
                

                node.transform.localPosition = new Vector3(Xindex * PathLenght, 0, Yindex * PathLenght);


                MapEditorNode NodeScript = node.GetComponent<MapEditorNode>();
                NodeScript.Id = idAdd++;
                NodeScript.X = Xindex;
                NodeScript.Y = Yindex;
                NodeScript.IsObseale = Obselale;




                if (Xindex > 0 && Yindex > 0)
                {
                    MapEditorNode mapEditorNode = node.GetComponent<MapEditorNode>();

                    DestroyImmediate(mapEditorNode.UpLeft.gameObject);
                    DestroyImmediate(mapEditorNode.DownLeft.gameObject);
                    DestroyImmediate(mapEditorNode.DownRight.gameObject);

                    mapEditorNode.DownLeft = MapRootScript.MapNode2DArray[Xindex - 1].Node[Yindex].GetComponent<MapEditorNode>().DownRight;
                    mapEditorNode.UpLeft = MapRootScript.MapNode2DArray[Xindex - 1].Node[Yindex].GetComponent<MapEditorNode>().UpRight;
                    mapEditorNode.DownRight = MapRootScript.MapNode2DArray[Xindex].Node[Yindex - 1].GetComponent<MapEditorNode>().UpRight;
                }
                else
                    if (Xindex > 0)
                    {
                        MapEditorNode mapEditorNode = node.GetComponent<MapEditorNode>();

                        DestroyImmediate(mapEditorNode.DownLeft.gameObject);
                        DestroyImmediate(mapEditorNode.UpLeft.gameObject);

                        mapEditorNode.DownLeft = MapRootScript.MapNode2DArray[Xindex - 1].Node[Yindex].GetComponent<MapEditorNode>().DownRight;
                        mapEditorNode.UpLeft = MapRootScript.MapNode2DArray[Xindex - 1].Node[Yindex].GetComponent<MapEditorNode>().UpRight;
                    }
                    else
                        if (Yindex > 0)
                        {
                            MapEditorNode mapEditorNode = node.GetComponent<MapEditorNode>();

                            DestroyImmediate(mapEditorNode.DownLeft.gameObject);
                            DestroyImmediate(mapEditorNode.DownRight.gameObject);

                            mapEditorNode.DownLeft = MapRootScript.MapNode2DArray[Xindex].Node[Yindex - 1].GetComponent<MapEditorNode>().UpLeft;
                            mapEditorNode.DownRight = MapRootScript.MapNode2DArray[Xindex].Node[Yindex - 1].GetComponent<MapEditorNode>().UpRight;
                        }



                //MapRootScript.MapNode2DArray[Xindex, Yindex] = node.transform;


                node.GetComponent<MapEditorNode>().DownLeft.parent = nodeRoot.transform;
                node.GetComponent<MapEditorNode>().DownRight.parent = nodeRoot.transform;
                node.GetComponent<MapEditorNode>().UpLeft.parent = nodeRoot.transform;
                node.GetComponent<MapEditorNode>().UpRight.parent = nodeRoot.transform;
            }


        }        
    }

    /// <summary>
    /// 刷新高度
    /// </summary>
    void UpMapHeight()
    {
        if (MapRootScript == null)
        {
            return;
        }

        MapRootScript.transform.position = new Vector3(PositionX, PositionY, PositionZ);
    }

    /// <summary>
    /// 更新信息
    /// </summary>
    void UpInfor()
    {
        if (!GetMapNodeRoot())
        {
            Debug.LogError("场景中不存在 MapRootScript 请先创建MapRootScript");

            return;
        }

        ///比例
        float multValue = PathLenght / MapRootScript.PathLenght;

        for (int Xindex = 0; Xindex < MapRootScript.MapNode2DArray.Count; Xindex++)
        {
            MapNodeRow mMapNodeRow = MapRootScript.MapNode2DArray[Xindex];

            for (int Yindex = 0; Yindex < mMapNodeRow.Node.Count; Yindex++)
            {
                Transform node = mMapNodeRow.Node[Yindex];

                MapEditorNode NodeScript = node.GetComponent<MapEditorNode>();

                NodeScript.SetNodeScale(MainNodeScale, OtherNodeScale);

                ///位置
                node.transform.localPosition = new Vector3(Xindex * PathLenght, 0, Yindex * PathLenght);



                NodeScript.UpLeft.localPosition = new Vector3(node.transform.localPosition.x + -PathLenght / 2, 0, node.transform.localPosition.z + PathLenght / 2);
                NodeScript.DownLeft.localPosition = new Vector3(node.transform.localPosition.x + -PathLenght / 2, 0, node.transform.localPosition.z + -PathLenght / 2);

                NodeScript.UpRight.localPosition = new Vector3(node.transform.localPosition.x + PathLenght / 2, 0, node.transform.localPosition.z + PathLenght / 2);
                NodeScript.DownRight.localPosition = new Vector3(node.transform.localPosition.x + PathLenght / 2, 0, node.transform.localPosition.z + -PathLenght / 2);

                if (Xindex <= 0)
                {
                    NodeScript.UpLeft.localPosition = new Vector3(node.transform.localPosition.x + -PathLenght / 2, 0, NodeScript.UpLeft.localPosition.z);

                    NodeScript.DownLeft.localPosition = new Vector3(node.transform.localPosition.x + -PathLenght / 2, 0, NodeScript.DownLeft.localPosition.z);
                }
                if (Yindex <= 0)
                {
                    NodeScript.DownLeft.localPosition = new Vector3(NodeScript.DownLeft.localPosition.x, 0, node.transform.localPosition.z + -PathLenght / 2);

                    NodeScript.DownRight.localPosition = new Vector3(NodeScript.DownRight.localPosition.x, 0, node.transform.localPosition.z + -PathLenght / 2);
                }
                if (Xindex >= XCount - 1)
                {
                    NodeScript.UpRight.localPosition = new Vector3(node.transform.localPosition.x + PathLenght / 2, 0, NodeScript.UpRight.localPosition.z);

                    NodeScript.DownRight.localPosition = new Vector3(node.transform.localPosition.x + PathLenght / 2, 0, NodeScript.DownRight.localPosition.z);
                }
                if (Yindex >= YCount - 1)
                {
                    NodeScript.UpRight.localPosition = new Vector3(NodeScript.UpRight.localPosition.x, 0, node.transform.localPosition.z + PathLenght / 2);

                    NodeScript.UpLeft.localPosition = new Vector3(NodeScript.UpLeft.localPosition.x, 0, node.transform.localPosition.z + PathLenght / 2);
                }


                NodeScript.UpDrawMeshPos();

                NodeScript.UpHasBuild();
            }
        }        

        MapRootScript.PathLenght = PathLenght;
        MapRootScript.OtherNodeScale = OtherNodeScale;
        MapRootScript.MainNodeScale = MainNodeScale;
    }

    /// <summary>
    /// 获取MapEditorRoot
    /// </summary>
    /// <returns></returns>
    public static MapEditorNodeRoot GetMapEditorRootNodeInstance()
    {
        return MapRootScript;
    }

    /// <summary>
    /// 获取焦点
    /// </summary>
    void OnFocus()
    { }
}



[CustomEditor(typeof(MapEditorNodeRoot))]
public class MapEditorNodeRootEditor : Editor
{
    private MapEditorNodeRoot mapEditorNode;

    private float width = 18.0f;

    private MapEditorNode currentSelect;

    private Vector2 recordPos;
    private Vector2 inputPos;

    private bool mouseDown = false;

    private Transform curentSelectTrans;

    void OnSceneGUI()
    {
        if (Selection.activeTransform == null)
        {
            return;
        }
        if (SceneView.currentDrawingSceneView == null)
        {
            return;
        }

        if(mapEditorNode==null)
        {        
            mapEditorNode = AStartMapEditor.GetMapEditorRootNodeInstance();

            if (mapEditorNode == null)
            {
                if (!AStartMapEditor.GetMapNodeRoot())
                {
                    return;
                }
                mapEditorNode = AStartMapEditor.GetMapEditorRootNodeInstance();
            }
        }        

        //if (Event.current.type == EventType.mouseDown && Event.current.keyCode == KeyCode.None)
        //{            
        //    mouseDown = true;
        //    recordPos = Event.current.mousePosition;
        //}

        //if (Event.current.type == EventType.mouseUp && Event.current.keyCode == KeyCode.None)
        //{
        //    mouseDown = false;
        //    curentSelectTrans = null;
        //}

        inputPos = Event.current.mousePosition;  

        //开始绘制GUI
        Handles.BeginGUI();
        //GUILayout.BeginArea(new Rect(0, 0, SceneView.currentDrawingSceneView.size, SceneView.currentDrawingSceneView.size));

        for (int loopX = 0; loopX < mapEditorNode.MapNode2DArray.Count; ++loopX)
        {
            for (int loopY = 0; loopY < mapEditorNode.MapNode2DArray[loopX].Node.Count; ++loopY)
            {
                Transform currentTransform=mapEditorNode.MapNode2DArray[loopX].Node[loopY];

                if (currentTransform != null)
                {
                    MapEditorNode mapNode = currentTransform.GetComponent<MapEditorNode>();

                    if (mapNode != null)
                    {
                        CallFun(mapNode);
                    }
                }
            }
        }
        //GUILayout.EndArea();
        Handles.EndGUI();

        recordPos = inputPos;
    }

    /// <summary>
    /// CallFun
    /// </summary>
    /// <param name="_mapNode"></param>
    private void CallFun(MapEditorNode _mapNode)
    {
        //SceneView.currentDrawingSceneView.camera.WorldToScreenPoint(_mapNode.transform.position);

        GUI.contentColor = Color.black;

        Vector3 sencePos = HandleUtility.WorldToGUIPoint(_mapNode.transform.position); 
        Rect nodeRect=new Rect(sencePos.x - width / 2, sencePos.y - width / 2, width, width);
        if (GUI.Button(nodeRect, "■"))
        {
            currentSelect = _mapNode;

            Selection.activeTransform = _mapNode.transform;
        }

        GUI.contentColor = Color.red;
        sencePos = HandleUtility.WorldToGUIPoint(_mapNode.UpLeft.position);
        Rect upLeftRect=new Rect(sencePos.x - width / 2, sencePos.y - width / 2, width, width);        
        GUI.Button(upLeftRect, "●");

        sencePos = HandleUtility.WorldToGUIPoint(_mapNode.DownLeft.position);
        Rect downLeftRect = new Rect(sencePos.x - width / 2, sencePos.y - width / 2, width, width);
        GUI.Button(downLeftRect, "●");

        sencePos = HandleUtility.WorldToGUIPoint(_mapNode.DownRight.position);
        Rect downRightRect = new Rect(sencePos.x - width / 2, sencePos.y - width / 2, width, width);
        GUI.Button(downRightRect, "●");
        sencePos = HandleUtility.WorldToGUIPoint(_mapNode.UpRight.position);
        Rect upRightRect = new Rect(sencePos.x - width / 2, sencePos.y - width / 2, width, width);
        GUI.Button(upRightRect, "●");

        if (mouseDown)
        {       
            if (curentSelectTrans == null && IsRang(inputPos, nodeRect))
            {
                currentSelect = _mapNode;
                curentSelectTrans = _mapNode.transform;
            }

            if (curentSelectTrans == null && IsRang(inputPos, upLeftRect))
            {
                currentSelect = _mapNode;
                curentSelectTrans = _mapNode.UpLeft;
            }

            if (curentSelectTrans == null && IsRang(inputPos, downLeftRect))
            {
                currentSelect = _mapNode;
                curentSelectTrans = _mapNode.DownLeft;
            }

            if (curentSelectTrans == null && IsRang(inputPos, downRightRect))
            {
                currentSelect = _mapNode;
                curentSelectTrans = _mapNode.DownRight;
            }

            if (curentSelectTrans == null && IsRang(inputPos, upRightRect))
            {
                currentSelect = _mapNode;
                curentSelectTrans = _mapNode.UpRight;
            }

            if (curentSelectTrans != null && currentSelect == _mapNode)
            {
                Vector2 dir = (inputPos - recordPos).normalized;

                float moveDistance = Vector2.Distance(inputPos,recordPos);

                float angle = Mathf.Atan2(dir.x, -dir.y) * 180 / Mathf.PI;

                float b = (SceneView.currentDrawingSceneView.camera.transform.eulerAngles.y + angle) * Mathf.PI / 180;

                Vector3 offsetDir = new Vector3(Mathf.Sin(b), 0, Mathf.Cos(b)).normalized;

                curentSelectTrans.position += offsetDir * moveDistance*0.15f;
            }
        }
    }

    public bool IsRang(Vector3 pos, Rect rect)
    {
        if (pos.x >= rect.xMin && pos.x <= rect.xMax && pos.y >= rect.yMin && pos.y <= rect.yMax)
        {
            return true;
        }

        return false;
    }
}
