using UnityEngine;
using System.Collections;

public class MapEditorNodeRoot : MonoBehaviour
{
    public float MainNodeScale;
    public float OtherNodeScale;

    public float PathLenght;
    public int XCount;
    public int YCount;
    public static MapEditorNodeRoot _Instance;
    public static MapEditorNodeRoot Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<MapEditorNodeRoot>();
            }

            return _Instance;
        }
    }

    public bool DrawMesh = true;

    public GameObject battle2;
    public GameObject battle3;
    //[System.NonSerialized]
    //public GameObject instanceBattle2;
    //[System.NonSerialized]
    //public GameObject instanceBattle3;

    public System.Collections.Generic.List<GameObject> GameList;

    public System.Collections.Generic.List<MapNodeRow> MapNode2DArray = new System.Collections.Generic.List<MapNodeRow>();


    
    public GameObject gameAssetsRoot;

    public GameObject battleRoot;

    /// <summary>
    /// 获取Map中对应位置的节点
    /// </summary>
    /// <param name="_pos"></param>
    /// <returns></returns>
    public MapEditorNode GetMapEditorNode(Vector3 _pos)
    {
        for (int loopX = 0; loopX < MapNode2DArray.Count; ++loopX)
        {
            for (int loopY = 0; loopY < MapNode2DArray[loopX].Node.Count; ++loopY)
            {
                MapEditorNode mapEditorNode = MapNode2DArray[loopX].Node[loopY].transform.GetComponent<MapEditorNode>();

                if (mapEditorNode.IsRangInRound(_pos))
                {
                    return mapEditorNode;
                }
            }
        }

        return null;
    }


    void Awake()
    {
        _Instance = this;
    }

    public void CreateRoot()
    {
        if (gameAssetsRoot == null)
        {
            gameAssetsRoot = new GameObject("AssetsRoot");

            gameAssetsRoot.transform.parent = transform;
        }       

        if (battleRoot == null)
        {
            battleRoot = new GameObject("ABattle_dir");

            battleRoot.transform.parent = transform;
        }

        //if (instanceBattle2 == null)
        //{
        //    instanceBattle2 = GameObject.Instantiate(battle2) as GameObject;
        //    instanceBattle2.transform.parent = transform;

        //}

        //if (instanceBattle3 == null)
        //{
        //    instanceBattle3 = GameObject.Instantiate(battle3) as GameObject;
        //    instanceBattle3.transform.parent = transform;
        //}
    }

    
    public string SaveMapInfor()
    {
        string Infor="";

        //路径间中心点的距离
        Infor += PathLenght.ToString() + "|";
        //X上面的数量
        Infor += XCount.ToString() + "|";
        //Y上面的数量
        Infor += YCount.ToString() + "|";

        for (int IndexX = 0; IndexX < XCount; IndexX++)
        {
            //if (IndexX > 0)
            //{
            //    Infor += "++";
            //}
            for (int IndexY = 0; IndexY < YCount; IndexY++)
            {                
                MapEditorNode  MapNode= MapNode2DArray[IndexX].Node[IndexY].GetComponent<MapEditorNode>();
                //if (IndexY > 0)
                //{
                //    Infor += "--";
                //}
                //Id
                Infor += MapNode.Id.ToString() + "|";
                //CountryFightId
                Infor += MapNode.CountryFightId.ToString() + "|";
                //X int类型
                Infor +=MapNode.X.ToString()+"|";
                //Y int 类型
                Infor += MapNode.Y.ToString() + "|";
                ///目标X
                Infor += MapNode.TargetX.ToString() + "|";
                ///目标Y
                Infor += MapNode.TargetY.ToString() + "|";
                ///距离
                Infor += MapNode.Distance.ToString("f2") + "|";
                //是否障碍物
                Infor += MapNode.IsObseale.ToString() + "|";
                //标志Tag
                Infor += System.Convert.ToInt32(MapNode.Tag).ToString() + "|";
                //拥有建筑的方向
                Infor += System.Convert.ToInt32(MapNode.buildDirection).ToString() + "|";
                //拥有建筑hasType
                Infor += System.Convert.ToInt32(MapNode.hasType).ToString() + "|";
                //拥有的城池类型
                Infor += MapNode.dynamicObseale.ToString() + "|";
                //固定建筑的路径
                Infor += MapNode.GetHasTypeObjectPath() + "|";
                //设置规模
                Infor += System.Convert.ToInt32(MapNode.cityType).ToString() + "|";
                //组ID
                Infor += MapNode.groupId.ToString() + "|";
                //可以创建的
                for (int loop = 0; loop < MapNode.CanCreateList.Count; ++loop)
                {
                    if (loop == 0)
                    {
                        Infor += System.Convert.ToInt32(MapNode.CanCreateList[loop]).ToString();
                    }
                    else
                    {
                        Infor +=  "," + System.Convert.ToInt32(MapNode.CanCreateList[loop]).ToString();
                    }
                }
                Infor += "|";
                //范围 下左
                Infor += MapNode.DownLeft.position.x.ToString() + "|";
                Infor += MapNode.DownLeft.position.y.ToString() + "|";
                Infor += MapNode.DownLeft.position.z.ToString() + "|";
                //范围 下右
                Infor += MapNode.DownRight.position.x.ToString() + "|";
                Infor += MapNode.DownRight.position.y.ToString() + "|";
                Infor += MapNode.DownRight.position.z.ToString() + "|";
                //范围 上左
                Infor += MapNode.UpLeft.position.x.ToString() + "|";
                Infor += MapNode.UpLeft.position.y.ToString() + "|";
                Infor += MapNode.UpLeft.position.z.ToString() + "|";
                //范围 上右
                Infor += MapNode.UpRight.position.x.ToString() + "|";
                Infor += MapNode.UpRight.position.y.ToString() + "|";
                Infor += MapNode.UpRight.position.z.ToString() + "|";
                //节点位置
                Infor += MapNode.transform.position.x.ToString() + "|";
                Infor += MapNode.transform.position.y.ToString() + "|";
                Infor += MapNode.transform.position.z.ToString() + "|";

                // 战斗点位置 //
                if (MapNode.battleDir != null)
                {
                    Infor += MapNode.battleDir.position.x.ToString() + "|";
                    Infor += MapNode.battleDir.position.y.ToString() + "|";
                    Infor += MapNode.battleDir.position.z.ToString() + "|";

                    if (MapNode.battleDirOther != null)
                    {
                        Infor += MapNode.battleDirOther.position.x.ToString() + "|";
                        Infor += MapNode.battleDirOther.position.y.ToString() + "|";
                        Infor += MapNode.battleDirOther.position.z.ToString() + "|";
                    }
					else
					{
						Infor += Vector3.zero.x.ToString() + "|";
						Infor += Vector3.zero.y.ToString() + "|";
						Infor += Vector3.zero.z.ToString() + "|";   
					}
                }
                else
                {
                    Infor += Vector3.zero.x.ToString() + "|";
                    Infor += Vector3.zero.y.ToString() + "|";
                    Infor += Vector3.zero.z.ToString() + "|";   
                }
  
            }
        }


        Debug.Log(Infor);
        return Infor;
    }


    public void OnDispos()
    {
        GameObject.DestroyImmediate(gameObject);
    }
}

[System.Serializable]
public class MapNodeRow
{
    public System.Collections.Generic.List<Transform> Node = new System.Collections.Generic.List<Transform>();
}