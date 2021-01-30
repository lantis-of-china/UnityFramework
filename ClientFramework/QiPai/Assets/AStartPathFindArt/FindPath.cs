using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindPath : MonoBehaviour {

    private static FindPath _instance;

    public static FindPath Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new FindPath();
            }

            return _instance;
        }
    }


    Node[][] Map;
    Transform[,] TransformList;

    public Transform prefab;
    PathManager pm = new PathManager();

     Vector3 startNode=new Vector3(-1,-1,-1);
    Vector3 findNode;


    List<Vector3> LinePosition = new List<Vector3>();

    Material lineMaterial;
	// Use this for initialization
	void Start () 
    {
        int count = 100;
        lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));

        float pathLenght;
        Vector2 v2 = MapEditorNodeHelp.InstanceMapDate(@"E:\WorkProject\MapEditor\Assets\TestMapInfor.AS", out Map, out pathLenght);

        //Map = new Node[count, count];
        TransformList = new Transform[(int)v2.x, (int)v2.y];     

        for (int a = 0; a < (int)v2.x; a++)
        {
            for (int b = 0; b < (int)v2.y; b++)
            {
                //Node n=new Node();

                //n.X = a;
                //n.Y = b;
                //Map[a,b] = n; 
                Node n = Map[a][b];

                Transform t=(Instantiate(prefab.gameObject) as GameObject).transform;

                t.localScale = new Vector3(10, 0.1f, 10);
                t.position = n.NodePosition;


                if (n.State == 1)
                {
                    //n.State = 1;
                    t.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    //n.State = 0;
                    t.gameObject.GetComponent<Renderer>().material.color = Color.red;
                }

                TransformList[a,b] = t;
            }
        }

	}


    void DebugNode(Node node)
    {

        if (node.father != null)
        {

            DebugNode(node.father);
        }
        

        LinePosition.Add(new Vector3(node.X,1, node.Y));    
    }


    /// <summary>
    /// 三点共线
    /// </summary>
    /// <param name="linePosition"></param>
    void RemoveMorePoint(List<Vector3> linePosition)
    {
        Debug.Log("Start Count " + linePosition.Count);
        if (LinePosition.Count >= 3)
        {
            for (int index = linePosition.Count-1; index >=2; index--)
            {
                if ((linePosition[index - 1].x - linePosition[index].x) == (linePosition[index - 2].x - linePosition[index - 1].x) && (linePosition[index - 1].z - linePosition[index].z) == (linePosition[index - 2].z - linePosition[index - 1].z))
                {
                    //三点共线
                    linePosition.RemoveAt(index - 1);
                } 
            }            
        }



        int len = linePosition.Count;
        for (int i = len - 1; i >= 0; i--)
        {
            for (int j = 0; j <= i; j++)
            {
                //可不可合并
                if (floydCrossAble(new Vector2(linePosition[i].x, linePosition[i].z), new Vector2(linePosition[j].x, linePosition[j].z)))
                {
                    for (int k = i - 1; k > j; k--)
                    {
                        linePosition.RemoveAt(k);
                    }
                    i = j;
                    len = linePosition.Count;
                    break;


                }
            }
        }
        Debug.Log("End Count " + linePosition.Count);

    }

    //布雷森汉姆直线演算法  猜测为要走的路径 暴露在列表里返回 
   List<Vector2>  bresenhamNodes(Vector2 p1,Vector2 p2)
    {  
        bool steep = System.Math.Abs(p2.y - p1.y) > System.Math.Abs(p2.x - p1.x);  
        if (steep){  
            int temp = (int)p1.x;  
            p1.x = p1.y;  
            p1.y = temp;  
            temp = (int)p2.x;  
            p2.x = p2.y;  
            p2.y = temp;  
        }  

        int stepX = p2.x > p1.x ? 1 : (p2.x < p1.x ? -1 : 0);  
        int stepY = p2.y > p1.y ? 1 : (p2.y < p1.y ? -1 : 0);  
        float deltay = (p2.y - p1.y) / System.Math.Abs(p2.x - p1.x);  
        List<Vector2> ret = new List<Vector2>();  
        float nowX = p1.x + stepX;  
        float nowY = p1.y + deltay;  

        if (steep){  
            ret.Add(new Vector2(p1.y, p1.x));  
        } else 
        {  
            ret.Add(new Vector2(p1.x, p1.y));  
        }  
        while (nowX != p2.x){  
            int fy = (int)System.Math.Floor((double)nowY);
            int cy = (int)System.Math.Ceiling((double)nowY);
            if (steep)
            {  
                ret.Add(new Vector2(fy, nowX));  
            } else {  
                ret.Add(new Vector2(nowX, fy));  
            }  
            if (fy != cy)
            {  
                if (steep)
                {  
                    ret.Add(new Vector2(cy, nowX));  
                } else {  
                    ret.Add(new Vector2(nowX, cy));  
                }  
            }  
            nowX += stepX;  
            nowY += deltay;  
        }  
        if (steep){  
            ret.Add(new Vector2(p2.y, p2.x));  
        } else {  
            ret.Add(new Vector2(p2.x, p2.y));  
        }  
        return ret;  
    }

    //是否能合并
    bool floydCrossAble(Vector2 n1, Vector2 n2)
    {
        List<Vector2> ps = bresenhamNodes(new Vector2(n1.x, n1.y), new Vector2(n2.x, n2.y));

        for (int i = ps.Count - 2; i > 0; i--)
        {
            if (Map[(int)ps[i].x][(int)ps[i].y].State != 1)
            {
                return false;
            }
        }

        return true;
    }


    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray r = camera.ScreenPointToRay(Input.mousePosition);

        //    RaycastHit rayHit;
        //    if (Physics.Raycast(r, out rayHit))
        //    {
        //        if(rayHit.collider.tag == "terrar")
        //        {
        //            if (startNode ==new Vector3(-1,-1,-1))
        //            {
        //                startNode = rayHit.collider.transform.position;
        //            }
        //            else
        //            {
        //                if (findNode == null)
        //                {
        //                    findNode = rayHit.collider.transform.position;
        //                }
        //                else
        //                {
        //                    startNode = findNode;
        //                    findNode = rayHit.collider.transform.position; 
        //                }

        //                Debug.Log("start "+startNode.x+" "+startNode.z+"  end "+ findNode.x + " " + findNode.z);

        //                LinePosition.Clear();

        //                System.DateTime front=System.DateTime.Now;
        //                System.DateTime finish = System.DateTime.Now;

        //                Debug.Log("Find Front " + front);

        //                LinePosition=pm.GetPath(startNode, findNode, Map);
        //                //Node no = pm.FindPath(Map[(int)startNode.x, (int)startNode.z], Map[(int)findNode.x, (int)findNode.z], Map);

        //                //finish=System.DateTime.Now;

        //                //Debug.Log("Find Finish useTime " + (finish-front).TotalMilliseconds + " ms");
        //                //if (no != null)
        //                //{
        //                //    DebugNode(no);
        //                //    Debug.Log("Optimize finish  ustTime " + (System.DateTime.Now-finish).TotalMilliseconds + " ms");
        //                //    //最后一次递归  直接调用
        //                //    RemoveMorePoint(LinePosition);     
        //                //}
        //                Debug.Log("LinePositionCount " + LinePosition.Count);

        //                for (int index = 0; index < LinePosition.Count; index++)
        //                {
        //                    Debug.Log("Vector3 " + LinePosition[index]);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    void OnPostRender()
    {
        if (LinePosition.Count > 0)
        {
            
            GL.PushMatrix();

            lineMaterial.SetPass(0);
            GL.Color(Color.red);
            GL.Begin(GL.LINES);
            
            for (int iCount = 0; iCount < LinePosition.Count; iCount++)
            {

                if (iCount == 0)
                { 
                }
                else
                {
                    GL.Vertex(LinePosition[iCount - 1]);
                    GL.Vertex(LinePosition[iCount]);
                }

                
            }

            GL.End();
            GL.PopMatrix();
        }
    }
}
