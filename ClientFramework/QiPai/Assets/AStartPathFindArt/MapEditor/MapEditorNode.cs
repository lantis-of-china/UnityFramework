using UnityEngine;
using System.Collections;
using System.Collections.Generic;




[ExecuteInEditMode]
public class MapEditorNode : MonoBehaviour 
{
    public int Id;
    public int CountryFightId;
    public int X;
    public int Y;
    public int TargetX = -1;
    public int TargetY = -1;

    /// <summary>
    /// 单位
    /// </summary>
    public float Distance = 1.0f;
    private float RecordDistance;
    public Transform UpLeft;
    public Transform UpRight;
    public Transform DownLeft;
    public Transform DownRight;
    public bool IsObseale;


    /// <summary>
    /// 拥有的类型Object
    /// </summary>
    public GameObject hasTypeObject;
    /// <summary>
    /// 路径
    /// </summary>
    public string hasObjectPath;
    /// <summary>
    /// 标记 城池
    /// </summary>
    public eTag Tag = eTag.None;

    /// <summary>
    /// 拥有的类型
    /// </summary>
    public eHasType hasType = eHasType.None;

    /// <summary>
    /// 建筑物的方向
    /// </summary>
    public eBuildDirection buildDirection = eBuildDirection.Forward;

    /// <summary>
    /// 设置动态加载之后动态障碍
    /// </summary>
    public bool dynamicObseale = true;

    /// <summary>
    /// 设置规模
    /// </summary>
    public eBuildTagType cityType = eBuildTagType.Tag_0;

    /// <summary>
    /// 组ID
    /// </summary>
    public int groupId = 0;

    /// <summary>
    /// 战斗点位置
    /// </summary>
    public Transform battleDir;
    public Transform battleDirOther;

    /// <summary>
    /// 可以创建的类型
    /// </summary>
    public List<eHasType> CanCreateList;

    /// <summary>
    /// 建筑物体
    /// </summary>
    private GameObject objectBuild;

    private List<GameObject> battleList = new List<GameObject>();

    

    private Material Mat_1
    {
        get
        {
            if (mat_1 == null)
            {
                mat_1 = new Material(Shader.Find("Lines/Colored Blended1"));
            }

            return mat_1;
        }
    }

    private Material Mat_2_None 
    {
        get
        {
            if (mat_2_none == null)
            {
                mat_2_none = new Material(Shader.Find("Lines/Colored Blended2"));
            }

            return mat_2_none;
        }
    }

    private Material Mat_2_Wei
    {
        get
        {
            if (mat_2_wei == null)
            {
                mat_2_wei = new Material("Shader \"Lines/Colored Blended2\" {" +
                    "Properties { _AColor (\"Main Color\", Color) = (0.7,1,0,0.5)  _BackColor(\"BackColor\",Color)=(0.7,1,0,0.5)   } SubShader { " +
                    "Tags { \"Queue\" = \"Transparent\" \"RenderType\"=\"Opaque\" } " +
                    "Pass {  " +
                    "ZTest LEqual  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_AColor] }  " +
                    "Lighting On   }  " +
                    "Pass {  " +
                    "ZTest Greater  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_BackColor] } Lighting On   "
                    + "} } }");
            }

            return mat_2_wei;
        }
    }

    private Material Mat_2_Shu
    {
        get
        {
            if (mat_2_shu == null)
            {
                mat_2_shu = new Material("Shader \"Lines/Colored Blended2\" {" +
                    "Properties { _AColor (\"Main Color\", Color) = (0,0,1,0.5)  _BackColor(\"BackColor\",Color)=(0,0,1,0.5)   } SubShader { " +
                    "Tags { \"Queue\" = \"Transparent\" \"RenderType\"=\"Opaque\" } " +
                    "Pass {  " +
                    "ZTest LEqual  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_AColor] }  " +
                    "Lighting On   }  " +
                    "Pass {  " +
                    "ZTest Greater  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_BackColor] } Lighting On   "
                    + "} } }");
            }

            return mat_2_shu;
        }
    }

    private Material Mat_2_Wu
    {
        get
        {
            if (mat_2_wu == null)
            {
                mat_2_wu = new Material("Shader \"Lines/Colored Blended2\" {" +
                    "Properties { _AColor (\"Main Color\", Color) = (0.5,0,1,0.5)  _BackColor(\"BackColor\",Color)=(0.5,0,1,0.5)   } SubShader { " +
                    "Tags { \"Queue\" = \"Transparent\" \"RenderType\"=\"Opaque\" } " +
                    "Pass {  " +
                    "ZTest LEqual  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_AColor] }  " +
                    "Lighting On   }  " +
                    "Pass {  " +
                    "ZTest Greater  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_BackColor] } Lighting On   "
                    + "} } }");
            }

            return mat_2_wu;
        }
    }

    public static Material Mat_3
    {
        get
        {
            if (mat_3 == null)
            {
                mat_3 = new Material("Shader \"Lines/Colored Blended2\" {" +
                    "Properties { _AColor (\"Main Color\", Color) = (1,1,1,0.5)  _BackColor(\"BackColor\",Color)=(1,1,1,0.7)   } SubShader { " +
                    "Tags { \"Queue\" = \"Transparent\" \"RenderType\"=\"Opaque\" } " +
                    "Pass {  " +
                    "ZTest LEqual  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_AColor] }  " +
                    "Lighting On   }  " +
                    "Pass {  " +
                    "ZTest Greater  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_BackColor] } Lighting On   "
                    + "} } }");
            }

            return mat_3;
        }
    }

    public static Material Mat_4
    {
        get
        {
            if (mat_4 == null)
            {
                mat_4 = new Material("Shader \"Lines/Colored Blended2\" {" +
                    "Properties { _AColor (\"Main Color\", Color) = (1,1,1,0.5)  _BackColor(\"BackColor\",Color)=(1,1,1,0.7)   } SubShader { " +
                    "Tags { \"Queue\" = \"Transparent\" \"RenderType\"=\"Opaque\" } " +
                    "Pass {  " +
                    "ZTest LEqual  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_AColor] }  " +
                    "Lighting On   }  " +
                    "Pass {  " +
                    "ZTest Greater  " +
                    "Blend SrcAlpha OneMinusSrcAlpha  " +
                    "Material { Diffuse [_BackColor] } Lighting On   "
                    + "} } }");
            }

            return mat_4;
        }
    }

     static Material mat_1;
     static Material mat_2_none;
     static Material mat_2_wei;
     static Material mat_2_shu;
     static Material mat_2_wu;
     static Material mat_3;
     static Material mat_4;
     private Mesh MESH;


    /// <summary>
    /// 是否包含在Grid中
    /// </summary>
    /// <param name="_pos"></param>
    /// <returns></returns>
    public bool IsRangInRound(Vector3 _pos)
     {
        if(_pos.x>=UpLeft.position.x&&
            _pos.z<=UpLeft.position.z&&
            _pos.x>=DownLeft.position.x&&
            _pos.z>=DownLeft.position.z&&
            _pos.x<=UpRight.position.x&&
            _pos.z<=UpRight.position.z&&
            _pos.x<=DownRight.position.x&&
            _pos.z >= DownRight.position.z)
        {
            return true;
        }

        return false;
     }

    /// <summary>
    /// 根据Node数据
    /// </summary>
    /// <param name="nodeInfor"></param>
    public void SetDataWithNode(Node nodeInfor)
     {
         Id = nodeInfor.Id;
         CountryFightId = nodeInfor.CountryFightId;
         groupId = nodeInfor.groupId;
         X = nodeInfor.X;
         Y = nodeInfor.Y;
         TargetX = nodeInfor.TargetX;
         TargetY = nodeInfor.TargetY;
         Distance = nodeInfor.Distance;
         UpLeft.position = nodeInfor.UpLeft;
         UpRight.position = nodeInfor.UpRight;
         DownLeft.position = nodeInfor.DownLeft;
         DownRight.position = nodeInfor.DownRight;
         IsObseale = nodeInfor.State == 1 ? false : true;
         Tag = (eTag)nodeInfor.Tag;
         hasType = (eHasType)nodeInfor.hasType;
         cityType = (eBuildTagType)nodeInfor.createType;
         hasObjectPath = nodeInfor.hasBuildPath;
         dynamicObseale = nodeInfor.dynamicObseale;


        if (!string.IsNullOrEmpty(hasObjectPath))
        {
            hasTypeObject = GameObject.Find(hasObjectPath);
        }


         if (CanCreateList == null)
         {
             CanCreateList = new List<eHasType>();
         }

         for (int loop = 0; loop < nodeInfor.canCreateList.Count; ++loop)
         {
             int createTypeValue = nodeInfor.canCreateList[loop];

             CanCreateList.Add((eHasType)createTypeValue);
         }

         transform.position = nodeInfor.NodePosition;


         if (!nodeInfor.battleDir.Equals(Vector3.zero))
         {            
             if (MapEditorNodeRoot.Instance.battleRoot == null)
             {
                MapEditorNodeRoot.Instance.battleRoot = new GameObject("Abattle_dir");
             }

             GameObject dir = new GameObject(X + "_" + Y);
             dir.AddComponent<BoxCollider>();
             MapEditorNodeChild editor = dir.AddComponent<MapEditorNodeChild>();
             editor.ParentRoot = this;
             dir.transform.parent = MapEditorNodeRoot.Instance.battleRoot.transform;
             dir.transform.position = nodeInfor.battleDir;
             battleDir = dir.transform;
             
            MapEditorNodeRoot.Instance.battleRoot.name = "ABattle_dir_" + MapEditorNodeRoot.Instance.battleRoot.transform.childCount;
        }

         if (!nodeInfor.battleDirOther.Equals(Vector3.zero))
         {
             GameObject dirOther = new GameObject(X + "_" + Y + "_Other");
             dirOther.AddComponent<BoxCollider>();
             MapEditorNodeChild editor = dirOther.AddComponent<MapEditorNodeChild>();
             editor.ParentRoot = this;
             dirOther.transform.parent = MapEditorNodeRoot.Instance.battleRoot.transform;
             dirOther.transform.position = nodeInfor.battleDirOther;
             battleDirOther = dirOther.transform;
             MapEditorNodeRoot.Instance.battleRoot.name = "ABattle_dir_" + MapEditorNodeRoot.Instance.battleRoot.transform.childCount;
        }
     }

    /// <summary>
    /// 改变目标
    /// </summary>
    void CheckTarget()
    {
        if (RecordDistance != Distance)
        {
            int boxDistance = Mathf.CeilToInt(RecordDistance);
            int defultTarget = -1;
            ///前置修改
            for (int loop = 1; loop < boxDistance; ++loop)
            {
                int minX = X - loop;
                int maxX = X + loop;
                int minY = Y - loop;
                int maxY = Y + loop;

                if (minX < 0)
                {
                    minX = 0;
                }

                if (maxX >= MapEditorNodeRoot.Instance.XCount)
                {
                    maxX = MapEditorNodeRoot.Instance.XCount - 1;
                }

                if (minY < 0)
                {
                    minY = 0;
                }

                if (maxY >= MapEditorNodeRoot.Instance.YCount)
                {
                    maxY = MapEditorNodeRoot.Instance.YCount - 1;
                }

                ///左下角扩展
                MapEditorNode currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[minX].Node[minY].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///右下角
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[maxX].Node[minY].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///左上角
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[minX].Node[maxY].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///右上角
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[maxX].Node[maxY].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///左
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[minX].Node[Y].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///右
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[maxX].Node[Y].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///上
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[X].Node[maxY].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;

                ///下
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[X].Node[minY].GetComponent<MapEditorNode>();
                currentNode.TargetX = defultTarget;
                currentNode.TargetY = defultTarget;
            }


            ///保留2位小数
            int valueDistance = (int)(Distance * 100);

            Distance = (float)valueDistance / 100.0f;

            RecordDistance = Distance;

            boxDistance = Mathf.CeilToInt(RecordDistance);

            ///后置修改
            for (int loop = 1; loop < boxDistance; ++loop)
            {
                int minX = X - loop;
                int maxX = X + loop;
                int minY = Y - loop;
                int maxY = Y + loop;

                if (minX < 0)
                {
                    minX = 0;
                }

                if (maxX >= MapEditorNodeRoot.Instance.XCount)
                {
                    maxX = MapEditorNodeRoot.Instance.XCount - 1;
                }

                if (minY < 0)
                {
                    minY = 0;
                }

                if (maxY >= MapEditorNodeRoot.Instance.YCount)
                {
                    maxY = MapEditorNodeRoot.Instance.YCount - 1;
                }

                ///左下角扩展
                MapEditorNode currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[minX].Node[minY].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///右下角
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[maxX].Node[minY].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///左上角
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[minX].Node[maxY].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///右上角
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[maxX].Node[maxY].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///左
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[minX].Node[Y].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///右
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[maxX].Node[Y].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///上
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[X].Node[maxY].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;

                ///下
                currentNode = MapEditorNodeRoot.Instance.MapNode2DArray[X].Node[minY].GetComponent<MapEditorNode>();
                currentNode.TargetX = X;
                currentNode.TargetY = Y;
            }

        }
    }

    /// <summary>
    /// 更新Update
    /// </summary>
    void Update()
    {
        CheckTarget();
        if (MapEditorNodeRoot.Instance.DrawMesh)
        {
            if (MESH == null) MESH = new Mesh();

            //if (IsObseale)
            //{
            //    mat_1.SetColor("_Color", Color.red);
            //}
            //else
            //{
            //    mat_1.SetColor("_Color", Color.blue);
            //}
            //if (MESH.vertices == null || MESH.vertices.Length <= 0)
            {
                UpDrawMeshPos();
            }


            if (IsObseale)
            {
                Graphics.DrawMesh(MESH, Vector3.zero, transform.rotation, Mat_1, 0);
            }
            else if (Tag == eTag.Wei)
            {
                Graphics.DrawMesh(MESH, Vector3.zero, transform.rotation, Mat_2_Wei, 0);
            }
            else if (Tag == eTag.Shu)
            {
                Graphics.DrawMesh(MESH, Vector3.zero, transform.rotation, Mat_2_Shu, 0);
            }
            else if (Tag == eTag.Wu)
            {
                Graphics.DrawMesh(MESH, Vector3.zero, transform.rotation, Mat_2_Wu, 0);
            }
            else
            {
                Graphics.DrawMesh(MESH, Vector3.zero, transform.rotation, Mat_2_None, 0);
            }
        }
    }

    /// <summary>
    /// 更新绘制模型
    /// </summary>
    public void UpDrawMeshPos()
    {
        if (MESH == null) MESH = new Mesh();
        MESH.vertices = new Vector3[5] { transform.position, DownLeft.position, UpLeft.position, UpRight.position, DownRight.position };
        MESH.triangles = new int[12] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 1 };
        MESH.RecalculateNormals();
    }
    
    /// <summary>
    /// 设置模型缩放
    /// </summary>
    /// <param name="MainNodeScale"></param>
    /// <param name="OtherNodeScale"></param>
    public void SetNodeScale(float MainNodeScale, float OtherNodeScale)
    {
        transform.localScale = new Vector3(MainNodeScale, MainNodeScale, MainNodeScale);

        if (UpLeft != null)
        {
            UpLeft.localScale = new Vector3(OtherNodeScale, OtherNodeScale, OtherNodeScale);
        }

        if (UpRight != null)
        {
            UpRight.localScale = new Vector3(OtherNodeScale, OtherNodeScale, OtherNodeScale);
        }

        if (DownLeft != null)
        {
            DownLeft.localScale = new Vector3(OtherNodeScale, OtherNodeScale, OtherNodeScale);
        }

        if (DownRight != null)
        {
            DownRight.localScale = new Vector3(OtherNodeScale, OtherNodeScale, OtherNodeScale);
        }
    }

    /// <summary>
    /// 通过路径设置对象
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    public void SetHasTypeObjectPath(string _path)
    {
        hasTypeObject = GameObject.Find(_path);
    }

    /// <summary>
    /// 获取拥有类型Object的路径
    /// </summary>
    public string GetHasTypeObjectPath()
    {
        string path = "";

        if (hasTypeObject == null)
        {
            return path;
        }

        List<GameObject> gameObjectList = GetParentGame(hasTypeObject);

        for (short loop = 0; loop < gameObjectList.Count; ++loop)
        {
            GameObject objectInstance = gameObjectList[loop];

            if (loop > 0)
            {
                path += "/" + objectInstance.name;
            }
            else
            {
                path = objectInstance.name;
            }
        }

        hasObjectPath = path;

        return path;
    }

    /// <summary>
    /// 获取父物体列表
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public List<GameObject> GetParentGame(GameObject gameObject)
    {
        List<GameObject> gameObjectList = new List<GameObject>();

        if (gameObject == null)
        {
            return gameObjectList;
        }

        if (gameObject.transform.parent != null)
        {
            ///先找父节点
            List<GameObject> parentList = GetParentGame(gameObject.transform.parent.gameObject);

            for (short loop = 0; loop < parentList.Count; ++loop)
            {
                gameObjectList.Add(parentList[loop]);
            }
        }
        ///再存自己的节点
        gameObjectList.Add(gameObject);

        return gameObjectList;
    }
    //void OnGUI()
    //{
    //    if (!mat)
    //    {
    //        Debug.LogError("Please Assign a material on the inspector");
    //        return;
    //    }
    //    GL.PushMatrix();
    //    mat.SetPass(0);
    //    GL.Color(Color.yellow);
    //    //GL.LoadOrtho();
    //    GL.Begin(GL.TRIANGLES);

    //    GL.Vertex(transform.position);
    //    GL.Vertex(DownLeft.position);
    //    GL.Vertex(UpLeft.position);

    //    GL.Vertex(transform.position);
    //    GL.Vertex(UpLeft.position);
    //    GL.Vertex(UpRight.position);


    //    GL.Vertex(transform.position);
    //    GL.Vertex(UpRight.position);
    //    GL.Vertex(DownRight.position);

    //    GL.Vertex(transform.position);
    //    GL.Vertex(DownRight.position);
    //    GL.Vertex(DownLeft.position);

    //    GL.End();
    //    GL.PopMatrix();
    //}



    /// <summary>
    /// 更新建筑物
    /// </summary>
    public void UpHasBuild()
    {
        if(hasType == eHasType.None)
        {
            return;
        }



        if (battleList != null && battleList.Count > 0 && MapEditorNodeRoot.Instance.battle2 != null && MapEditorNodeRoot.Instance.battle3 != null)
        {
            for (int loopInt = 0; loopInt < battleList.Count; ++loopInt)
            {
                GameObject battleObject = battleList[loopInt];

                Object.DestroyImmediate(battleObject);
            }
        }


        if (hasType == eHasType.MainCity)
        {
            for (int loop = 0; loop < 3; ++loop)
            {
                for (int loopBattle = 0; loopBattle < 4; ++loopBattle)
                {
                    GameObject posObject = MapEditorNodeRoot.Instance.battle3.transform.Find("demolisher/" + loop + "/" + loopBattle).gameObject;

                    if (battleDir != null)
                    {
                        SetBattleMoudle(MapEditorNodeRoot.Instance.battle3, battleDir, posObject, loopBattle);
                    }

                    if (battleDirOther != null)
                    {
                        SetBattleMoudle(MapEditorNodeRoot.Instance.battle3, battleDirOther, posObject, loopBattle);
                    }
                }
            }
        }
        else if (hasType == eHasType.Fortress_L || hasType == eHasType.Fortress_K || hasType == eHasType.Fortress_M /*|| hasType == eHasType.Gate*/)
        {
            for (int loopBattle = 0; loopBattle < 4; ++loopBattle)
            {
                GameObject posObject = MapEditorNodeRoot.Instance.battle3.transform.Find("demolisher/0" + "/" + loopBattle).gameObject;

                if (battleDir != null)
                {
                    SetBattleMoudle(MapEditorNodeRoot.Instance.battle3, battleDir, posObject, loopBattle);
                }

                if (battleDirOther != null)
                {
                    SetBattleMoudle(MapEditorNodeRoot.Instance.battle3, battleDirOther, posObject, loopBattle);
                }
            }
        }
        else
        {
            for (int loop = 0; loop < 2; ++loop)
            {
                for (int loopBattle = 0; loopBattle < 4; ++loopBattle)
                {
                    GameObject posObject = MapEditorNodeRoot.Instance.battle2.transform.Find("demolisher/" + loop + "/" + loopBattle).gameObject;

                    if (battleDir != null)
                    {
                        SetBattleMoudle(MapEditorNodeRoot.Instance.battle2, battleDir, posObject, loopBattle);
                    }

                    if (battleDirOther != null)
                    {
                        SetBattleMoudle(MapEditorNodeRoot.Instance.battle2, battleDirOther, posObject, loopBattle);
                    }
                }
            }
        }



        ///有路径就返回
        if (!string.IsNullOrEmpty(hasObjectPath))
        {
            return;
        }

        ///通过配置来
        int configId = System.Convert.ToInt32(hasType);

        if (hasType == eHasType.MainCity)
        {
           
            configId += System.Convert.ToInt32(Tag) + System.Convert.ToInt32(cityType);            
        }

        string moudleName = GetMoudleNameWithConfigId(configId);        

        GameObject findObject = null;

        for (int loop = 0; loop < MapEditorNodeRoot.Instance.GameList.Count;++loop)
        {
            findObject = MapEditorNodeRoot.Instance.GameList[loop];

            if(findObject.name== moudleName)
            {
                break;
            }

            findObject = null;
        }

        if(findObject==null)
        {
            Debug.LogError("not find moudle id is " + configId + " moudleName " + moudleName + " pos " + X + "_" + Y);
            return;
        }

        findObject = GameObject.Instantiate(findObject) as GameObject;

        if(objectBuild != null && findObject!=null)
        {
            Object.DestroyImmediate(objectBuild);
        }

        objectBuild = findObject;

        objectBuild.transform.position = transform.position;

        if (buildDirection == eBuildDirection.Forward)
        {
            objectBuild.transform.forward = Vector3.forward;
        }
        else if (buildDirection == eBuildDirection.Back)
        {
            objectBuild.transform.forward = Vector3.back;
        }
        else if (buildDirection == eBuildDirection.Left)
        {
            objectBuild.transform.forward = Vector3.left;
        }
        else if (buildDirection == eBuildDirection.Right)
        {
            objectBuild.transform.forward = Vector3.right;
        }

        objectBuild.transform.parent = MapEditorNodeRoot.Instance.gameAssetsRoot.transform;
    }

    public void SetBattleMoudle(GameObject battleNodeRoot,Transform battleDir, GameObject posObject,int index)
    {
        battleDir.transform.forward = (transform.position - battleDir.transform.position).normalized;

        Vector3 offsetPos = posObject.transform.localPosition + posObject.transform.parent.localPosition + posObject.transform.parent.parent.localPosition;

        Vector3 setDir = -(offsetPos).normalized;

        //offsetPos = battleDir.transform.position + battleDir.transform.rotation * new Vector3(0, 0, Vector3.Distance(offsetPos, Vector3.zero));

        //setDir = (setDir + battleDir.transform.forward).normalized;


        float offsetAngle = Mathf.Atan2(setDir.x, setDir.z) * 180.0f / Mathf.PI;

        float rootAngle = Mathf.Atan2(battleDir.transform.forward.x, battleDir.transform.forward.z) * 180.0f / Mathf.PI;

        float spawnAngle = (rootAngle + offsetAngle) * Mathf.PI / 180;

        setDir = new Vector3(Mathf.Sin(spawnAngle), 0, Mathf.Cos(spawnAngle));

        offsetPos = Vector3.Distance(offsetPos, Vector3.zero) * setDir;

        offsetPos = offsetPos + battleDir.transform.position;

        string moudleName = "";

        if(index == 0)
        {
            moudleName = "W_qb01";
            return;
        }
        else if(index == 1)
        {
            moudleName = "W_gcwq05";
        }
        else if (index == 2)
        {
            moudleName = "W_gcwq06";
        }
        else if (index == 3)
        {
            moudleName = "W_gcwq04";
        }

        GameObject gameAssets = GetMoudleWithName(moudleName);

        if (gameAssets == null)
        {
            Debug.LogError("Game Assets Is Null");
        }

        

        //battleNodeRoot.transform.parent = battleDir.transform;

        //battleNodeRoot.transform.localEulerAngles = new Vector3(0, 180.0f, 0);

        //battleNodeRoot.transform.localPosition = Vector3.zero;

        gameAssets = GameObject.Instantiate(gameAssets) as GameObject;

        gameAssets.transform.SetParent(MapEditorNodeRoot.Instance.gameAssetsRoot.transform);

        gameAssets.transform.position = offsetPos;// posObject.transform.position;        

        gameAssets.transform.forward = transform.position - gameAssets.transform.position;//posObject.transform.forward;

        battleList.Add(gameAssets);        
    }

    public GameObject GetMoudleWithName(string moudleName)
    {
        GameObject findObject = null;

        for (int loop = 0; loop < MapEditorNodeRoot.Instance.GameList.Count; ++loop)
        {
            findObject = MapEditorNodeRoot.Instance.GameList[loop];

            if (findObject.name == moudleName)
            {
                break;
            }

            findObject = null;
        }

        return findObject;
    }

    /// <summary>
    /// 通过ID来获取模型名字
    /// </summary>
    /// <param name="configId"></param>
    /// <returns></returns>
    public string GetMoudleNameWithConfigId(int configId)
    {
        if (configId == 101000)
        {
            return "W_hc_01";

        }
        else
               if (configId == 101001)
        {
            return "W_cc_01";
        }
        else
               if (configId == 101300)
        {
            return "W_hc_01";
        }
        else
               if (configId == 101301)
        {
            return "W_cc_02";
        }
        else
               if (configId == 101200)
        {
            return "W_hc_01";
        }
        else
               if (configId == 101201)
        {
            return "W_cc_03";
        }
        else
               if (configId == 101100)
        {
            return "W_hc_01";
        }
        else
               if (configId == 101101)
        {
            return "W_cc_04";
        }
        else
               if (configId == 103000)
        {
            return "W_C_yd_01";
        }
        else
               if (configId == 103001)
        {
            return "W_C_yd_01";
        }
        else
               if (configId == 103003)
        {
            return "W_C_yd_01";
        }
        else
               if (configId == 103004)
        {
            return "W_C_yd_01";
        }
        else
               if (configId == 20013)
        {
            return "W_gcwq_07";
        }
        else
               if (configId == 20014)
        {
            return "W_gcwq_07";
        }
        else
               if (configId == 20015)
        {
            return "W_gcwq_07";
        }


        return "";
    }
}

/// <summary>
/// 建筑物方向
/// </summary>
public enum eBuildDirection
{
    Forward = 0,
    Back = 1,
    Left = 2,
    Right = 3
}


/// <summary>
/// 障碍物标志
/// </summary>
public enum eState
{
    Obstacle = 0,
    None = 1
}

/// <summary>
/// 势力范围
/// </summary>
public enum eTag
{
    None = 0,
    /// <summary>
    /// 魏
    /// </summary>
    Wei = 100,
    /// <summary>
    /// 蜀
    /// </summary>
    Shu = 200,
    /// <summary>
    /// 吴
    /// </summary>
    Wu = 300
}

/// 类型,资源配置ID
/// </summary>
public enum eHasType
{
    None = 0,
    /// <summary>
    /// 主城
    /// </summary>
    MainCity = 101000,
    /// </summary>
    /// 关隘
    /// </summary>
    Gate = 107000,
    /// <summary>
    /// 渡口
    /// </summary>
    Ferry = 108000,
    /// <summary>
    /// 要塞(粮)
    /// </summary>
    Fortress_L = 20013,
    /// <summary>
    /// 要塞(木)
    /// </summary>
    Fortress_M = 20014,
    /// <summary>
    /// 要塞(矿)
    /// </summary>
    Fortress_K = 20015,
    /// <summary>
    /// 农田
    /// </summary>
    Cropland = 102100,
    /// <summary>
    /// 林地
    /// </summary>
    Bush = 102200,
    /// <summary>
    /// 矿脉
    /// </summary>
    Mine = 102300,
    /// <summary>
    /// 黄巾驻地
    /// </summary>
    YellowArmy = 103000,
    /// <summary>
    /// 匈奴
    /// </summary>
    XiongnuCampsite = 103001,
    /// <summary>
    /// 羌族
    /// </summary>
    QiangCampsite = 103002,
    /// <summary>
    /// 山越
    /// </summary>
    ShanyueCampsite = 103003,
    /// <summary>
    /// 南蛮
    /// </summary>
    NanmanCampsite = 103004,

}

///<summary>
///城池规模规模
///</summary>
public enum eBuildTagType
{
    /// <summary>
    /// 皇城
    /// </summary>
    Tag_0 = 0,
    /// <summary>
    /// 小型城市
    /// </summary>
    Tag_1 = 1,
    /// <summary>
    /// 中型城市
    /// </summary>
    Tag_2 = 2,
    /// <summary>
    /// 大城市
    /// </summary>
    Tag_4 = 3,
    /// <summary>
    /// 
    /// </summary>
    Tag_5 = 4,
    /// <summary>
    /// 
    /// </summary>
    Tag_6 = 5,
    /// <summary>
    /// 
    /// </summary>
    Tag_7 = 6,
    /// <summary>
    /// 
    /// </summary>
    Tag_8 = 7,
    /// <summary>
    /// 
    /// </summary>
    Tag_9 = 8,
    /// <summary>
    /// 
    /// </summary>
    Tag_10 = 9
}