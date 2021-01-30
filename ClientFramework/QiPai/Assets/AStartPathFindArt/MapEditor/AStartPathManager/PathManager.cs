using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager
{

    List<Node> CloseList =new List<Node>();
    List<Node> OpenList  =new List<Node>();

    /// <summary>
    /// 是否是障碍物
    /// </summary>
    /// <returns></returns>
    public bool IsObstacle(Vector3 pos, Node[][] map)
    {
        Node StartNode = GetNode(pos, map);

        if (StartNode.State == 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    //计算某个点的G值
    private int GetG(Node p)
    {
        if (p.father == null) return 0;
        if (p.X == p.father.X || p.Y == p.father.Y) return p.father.G + 10;
        else return p.father.G + 14;
    }

    //计算某个点的H值
    private int GetH(Node p, Node pb)
    {
        int dis = 0;
        if (p.father != null)
        {
            if (System.Math.Abs(p.X - pb.X) > System.Math.Abs(p.father.X - pb.X))
            {
                dis++;
            }
            else
            if (System.Math.Abs(p.X - pb.X) < System.Math.Abs(p.father.X - pb.X))
            {
                dis--;
            }


            if (System.Math.Abs(p.Y - pb.Y) > System.Math.Abs(p.father.Y - pb.Y))
            {
                dis++;
            }
            else
            if (System.Math.Abs(p.Y - pb.Y) > System.Math.Abs(p.father.Y - pb.Y))
            {
                dis--;
            }
        }
        return (System.Math.Abs(p.X - pb.X) + System.Math.Abs(p.Y - pb.Y)+dis);
    }

    /// <summary>
    /// 获取最小F节点从Close
    /// </summary>
    /// <param name="EndNode"></param>
    /// <returns></returns>
    private Node GetMinNodeFormClose(Node EndNode)
    {
        int minH = -1;
        Node minNode = null;

        for (int index = 0; index < CloseList.Count; index++)
        {
            Node node = CloseList[index];

            int G = GetG(node);
            int H = GetH(node, EndNode);
            int f = G + H;

            if (node != null)
                if (minNode == null || minH < 0 || minH > H) { minH = H; minNode = node; node.F = f; node.G = G; node.H = H; }

        }

        return minNode;

    }

    //获取OpenList中 F最小的点
    private Node GetMinNode(Node EndNode)
    {
        int minH = -1;
        Node minNode = null;

        for (int index = 0; index < OpenList.Count; index++)
        {
            Node node = OpenList[index];

            int G= GetG(node);
            int H= GetH(node, EndNode);
            int f = G + H;

            if (node!=null)
                if (minNode == null || minH < 0 || minH > H) { minH = H; minNode = node; node.F = f; node.G = G; node.H = H; }

        }

        return minNode;
 
    }

    //找周围的点
    private List<Node> SurrroundNodes(Node CurrentNode,Node[][] Map)
    {
        List<Node> canMoveList = new List<Node>();

        int Left=(CurrentNode.X - 1);
        int Right=(CurrentNode.X + 1);
        int Top=(CurrentNode.Y - 1);
        int Bottom=(CurrentNode.Y + 1);

        if (Right < Map.GetLength(0))//X没有超出Map
        {
            Node rightNode = Map[Right][CurrentNode.Y];

            if (rightNode.State == 1) { canMoveList.Add(rightNode); }
        }

        if (Left >= 0)//左边没有超出
        {
            Node leftNode = Map[Left][CurrentNode.Y];

            if (leftNode.State == 1) { canMoveList.Add(leftNode); }
        }

        if (Top >= 0)//上边没有超出
        {
            Node topNode = Map[CurrentNode.X][Top];

            if (topNode.State == 1) { canMoveList.Add(topNode); }
        }

        if (Bottom < Map.GetLength(1))//下边没有超出
        {
            Node bottomNode = Map[CurrentNode.X][Bottom];

            if (bottomNode.State == 1) { canMoveList.Add(bottomNode); }
        }

        if (Left >= 0 && Top >= 0)//左上角那个没超出
        {
             //左边 上边可走
            if (Map[CurrentNode.X][Top].State == 1 && Map[Left][CurrentNode.Y].State == 1)
            {
                Node leftTopNode = Map[Left][Top];

                if (leftTopNode.State == 1) { canMoveList.Add(leftTopNode); }
            }
        }

        if (Right < Map.GetLength(0) && Top >= 0)//右上角那个没超出
        {
             //右边 上边可走
            if (Map[CurrentNode.X][Top].State == 1 && Map[Right][CurrentNode.Y].State == 1)
            {
                Node rightTopNode = Map[Right][Top];

                if (rightTopNode.State == 1) { canMoveList.Add(rightTopNode); }
            }
        }

        if (Left >=0)//坐下角那个没超出
        {
            if (Bottom < Map.GetLength(1))
            {

                //左边 下边可走
                if (Map[CurrentNode.X][Bottom].State == 1&& Map[Left][CurrentNode.Y].State == 1)
                {
                    Node leftBottomNode = Map[Left][Bottom];

                    if (leftBottomNode.State == 1) { canMoveList.Add(leftBottomNode); }
                }
            }
        }

        if (Right < Map.GetLength(0))//右下角那个没超出
        {
            if (Bottom < Map.GetLength(1))
            {
                if (Map[CurrentNode.X][Bottom].State == 1 && Map[Right][CurrentNode.Y].State == 1)
                {
                    Node rightBottomNode = Map[Right][Bottom];

                    if (rightBottomNode.State == 1) { canMoveList.Add(rightBottomNode); }
                }
            }
        }



        return canMoveList;
    }

    //从Open中找结束的点
    private Node OpenListGetEnd(Node end)
    {
        Node endNode = OpenList.Find(item => item == end);

        return endNode;
    }
    
    /// <summary>
    /// 寻路
    /// </summary>
    /// <param name="startNode"></param>
    /// <param name="EndNode"></param>
    /// <param name="Map"></param>
    /// <returns></returns>
    public Node FindPath(Node startNode,Node EndNode,Node[][] Map)
    {
        OpenList.Clear();
        CloseList.Clear();

        startNode.father = null;

        OpenList.Add(startNode);

        if (startNode == EndNode || (startNode.X == EndNode.X&&startNode.Y == EndNode.Y))
        {
           return EndNode;
        }

        while (OpenList.Count > 0)
        {
            //F最小的
            Node currentNode = GetMinNode(EndNode);

            if (currentNode != null)
            {
                OpenList.Remove(currentNode);

                CloseList.Add(currentNode);

                List<Node> SurrroundList = SurrroundNodes(currentNode, Map);

                for (int i = 0; i < SurrroundList.Count; i++)
                {
                    if (!CloseList.Exists(item => item == SurrroundList[i]) && SurrroundList[i].State == 1)
                    {

                        if (OpenList.Exists(item => item == SurrroundList[i]))
                        {  
                            // G值 到目的点的距离  越小越好
                            int G = GetG(SurrroundList[i]);

                            int H = GetH(SurrroundList[i], EndNode);

                            int F = G + H;

                            if (G <= SurrroundList[i].G)
                            {
                                SurrroundList[i].F = F;
                                SurrroundList[i].G = G;
                                SurrroundList[i].H = H;

                                SurrroundList[i].father = currentNode;
                            }
                        }
                        else
                        {
                            //加入进去 设置父节点为当前节点
                            OpenList.Add(SurrroundList[i]);

                            int G = GetG(SurrroundList[i]);
                            int H = GetH(SurrroundList[i], EndNode);
                            int F = G + H;

                            SurrroundList[i].F = F;
                            SurrroundList[i].G = G;
                            SurrroundList[i].H = H;

                            SurrroundList[i].father = currentNode;
                        }
                    }
                }
            }

            Node ReturnNode=OpenListGetEnd(EndNode);

            if (ReturnNode != null)
            {
                return ReturnNode;
            }

        }

        //return currentNode;
        return GetMinNodeFormClose(EndNode);
    }

    /// <summary>
    /// 获取路径
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="Map"></param>
    public List<Vector3> GetPath(Vector3 startPosition, Vector3 endPosition, Node[][] Map)
    {
        Node StartNode = GetNode(startPosition, Map);
        Node EndNode = GetNode(endPosition, Map);

        //这个是Line是节点的X Y的值
        List<Vector3> LinePosition = new List<Vector3>();
        List<Vector3> PathPostion = new List<Vector3>();
        LinePosition.Clear();
        PathPostion.Clear();

        if (StartNode != null && EndNode != null)
        {
            Node FindNode = FindPath(StartNode, EndNode, Map);

            if (FindNode != null)
            {
                Node2Path(FindNode,LinePosition);

                //RemoveMorePoint(LinePosition, Map);

                for (int indexLine = 0; indexLine < LinePosition.Count; indexLine++)
                {
                    PathPostion.Add(Map[(int)LinePosition[indexLine].x][(int)LinePosition[indexLine].z].NodePosition);
                }

                if (PathPostion.Count == 1 && startPosition != endPosition)
                {
                    PathPostion[0] = startPosition;

                    if (FindNode == EndNode)
                    {
                        PathPostion.Add(endPosition);
                    }
                }
                else
                {
                    PathPostion[0] = startPosition;

                    if (FindNode == EndNode)
                    {
                        PathPostion[PathPostion.Count - 1] = endPosition;
                    }
                }
            }
        }

        return PathPostion;
    }
    
    /// <summary>
    /// 把Node 返回到path
    /// </summary>
    /// <param name="node"></param>
    /// <param name="linePosition"></param>
    void Node2Path(Node node,List<Vector3> linePosition)
    {       

        if (node.father != null)
        {

            Node2Path(node.father,linePosition);
        }
        

        linePosition.Add(new Vector3(node.X,0,node.Y));
    }
    
    //获取拥有该点的节点
    public Node GetNode(Vector3 position, Node[][] map)
    {
        Node selectNode = null;
        for (int indexX = 0; indexX < map.Length; indexX++)
        {
            for (int indexY = 0; indexY < map[indexX].Length; indexY++)
            {
                Node CurrentNode = map[indexX][indexY];

                if (PointinTriangle(new Vector3(CurrentNode.DownLeft.x,0,CurrentNode.DownLeft.z),new Vector3(CurrentNode.DownRight.x,0,CurrentNode.DownRight.z),new Vector3(CurrentNode.UpRight.x,0,CurrentNode.UpRight.z),new Vector3(position.x,0,position.z)))
                {
                    selectNode = CurrentNode;
                    break;
                }

                if (PointinTriangle(new Vector3(CurrentNode.DownLeft.x, 0, CurrentNode.DownLeft.z), new Vector3(CurrentNode.UpLeft.x, 0, CurrentNode.UpLeft.z), new Vector3(CurrentNode.UpRight.x, 0, CurrentNode.UpRight.z), new Vector3(position.x, 0, position.z)))
                {
                    selectNode = CurrentNode;
                    break;
                }
            }

            if (selectNode!=null)
            {
                break;
            }
 
        }

        return selectNode;
    }

    /// <summary>
    /// 获取拥有该点的节点
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public Node GetNode(int x,int y,Node[,] map)
    {
        return map[x, y];
    }

    /// <summary>
    /// 点是否在三角形内
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="C"></param>
    /// <param name="P"></param>
    /// <returns></returns>
    bool PointinTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
    {    
        Vector3 v0 = C - A ;   
        Vector3 v1 = B - A ;    
        Vector3 v2 = P - A ;    

        float dot00 =Vector3.Dot(v0,v0);
        float dot01 = Vector3.Dot(v0,v1);
        float dot02 = Vector3.Dot(v0,v2);
        float dot11 = Vector3.Dot(v1,v1);
        float dot12 = Vector3.Dot(v1,v2);    
        float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01) ;    
        float u = (dot11 * dot02 - dot01 * dot12) * inverDeno ;    
        if (u < 0 || u > 1) // if u out of range, return directly    
        {        
            return false ;    
        }    
        float v = (dot00 * dot12 - dot01 * dot02) * inverDeno ;    
        if (v < 0 || v > 1) // if v out of range, return directly    
        {        
            return false ;    
        }    
        return u + v <= 1 ;
    }
    
    /// <summary>
    /// 三点共线
    /// </summary>
    /// <param name="linePosition"></param>
    void RemoveMorePoint(List<Vector3> linePosition,Node[,] map)
    {
        if (linePosition.Count >= 3)
        {
            for (int index = linePosition.Count - 1; index >= 2; index--)
            {
                if ((linePosition[index - 1].x - linePosition[index].x) == (linePosition[index - 2].x - linePosition[index - 1].x) && (linePosition[index - 1].z - linePosition[index].z) == (linePosition[index - 2].z - linePosition[index - 1].z))
                {
                    //三点共线
                    linePosition.RemoveAt(index - 1);
                }
            }
        }

        for (int removeLoop = linePosition.Count - 1; removeLoop >= 0; --removeLoop)
        {
            for (int curLoop = removeLoop - 2; curLoop >= 0; --curLoop)
            {
                //可不可合并
                if (FloydCrossAble(new Vector2(linePosition[removeLoop].x, linePosition[removeLoop].z), new Vector2(linePosition[curLoop].x, linePosition[curLoop].z), map))
                {
                    for (int k = removeLoop - 1; k > curLoop; --k)
                    {
                        linePosition.RemoveAt(k);

                        removeLoop--;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 布雷森汉姆直线演算法  猜测为要走的路径 暴露在列表里返回 
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private List<Vector2> BresenhamNodes(Vector2 p1, Vector2 p2)
    {        
        List<Vector2> retList = new List<Vector2>();

        bool steep = System.Math.Abs(p2.y - p1.y) > System.Math.Abs(p2.x - p1.x);

        if (steep)
        {
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

        
        float nowX = p1.x + stepX;
        float nowY = p1.y + deltay;

        if (steep)
        {
            retList.Add(new Vector2(p1.y, p1.x));
                       
        }
        else
        {
            retList.Add(new Vector2(p1.x, p1.y)); 
        }

        while (nowX != p2.x)
        {
            int fy = (int)System.Math.Floor((double)nowY);
            int cy = (int)System.Math.Ceiling((double)nowY);

            if (steep)
            {
                retList.Add(new Vector2(fy, nowX));
            }
            else
            {
                retList.Add(new Vector2(nowX, fy));
            }

            if (fy != cy)
            {
                if (steep)
                {
                    retList.Add(new Vector2(cy, nowX));
                }
                else
                {
                    retList.Add(new Vector2(nowX, cy));
                }
            }
            
            nowX += stepX;
            nowY += deltay;

        }

        if (steep)
        {
            retList.Add(new Vector2(p2.y, p2.x));
        }
        else
        {
            retList.Add(new Vector2(p2.x, p2.y));
        }

        return retList;
    }

    /// <summary>
    /// 是否能合并拐点
    /// </summary>
    /// <param name="n1"></param>
    /// <param name="n2"></param>
    /// <param name="Map"></param>
    /// <returns></returns>
    private bool FloydCrossAble(Vector2 n1, Vector2 n2,Node[,] Map)
    {
        //bool xDif = false;
        //bool yDif = false;

        //if (Mathf.Abs(n1.x - n2.x) == 1)
        //{
        //    xDif = true;
        //}

        //if (Mathf.Abs(n1.y - n2.y) == 1)
        //{
        //    yDif = true;
        //}

        //if (xDif && yDif)
        //{
        //    return false;
        //}
        
        List<Vector2> ps = BresenhamNodes(n1, n2);

        if (ps.Count <= 0)
        {
            return false;
        }
        
        for (int i = 1; i < ps.Count; ++i)
        {
            if (ps[i].x < 0 || ps[i].x >= Map.GetLength(0) || ps[i].y < 0 || ps[i].y >= Map.GetLength(1))
            {
                continue;
            }           

            Node curNode = Map[(int)ps[i].x, (int)ps[i].y];

            if (curNode.State != 1)
            {
                return false;
            }

            ///这里需要增加一个判断 判断完全直线的情况下是否过中心点 需要判断格子
            Node upNode = Map[(int)ps[i - 1].x, (int)ps[i - 1].y];

            int differentX = upNode.X - curNode.X;
            int differentY = upNode.Y - curNode.Y;

            if (Mathf.Abs(differentX) == 1 && Mathf.Abs(differentY) == 1)
            {
                int firstX = curNode.X + differentX;
                int secondY = curNode.Y + differentY;

                Node nodeFirst = null;

                Node nodeSecond = null;

                if (firstX >= 0 && firstX < Map.GetLength(0) && curNode.Y >= 0 && curNode.Y < Map.GetLength(1))
                {
                    nodeFirst = Map[firstX, curNode.Y];
                }

                if (secondY >= 0 && secondY < Map.GetLength(1) &&curNode.X >= 0 && curNode.X < Map.GetLength(0))
                {
                    nodeSecond = Map[curNode.X, secondY];
                }

                if ((nodeFirst == null || nodeSecond == null) || (nodeFirst.State != 1 || nodeSecond.State != 1))
                {                    
                    return false;
                }
            }

        }



        return true;
    } 
}

//它们通过公式 F=G+H 来计算
//G 表示从起点 A 移动到网格上指定方格的移动耗费 (可沿斜方向移动)
//H 表示从指定的方格移动到终点的预计耗费 (H 有很多计算方法, 这里我们设定只可以上下左右移动)

public class Node
{
    public int Id;
    public int CountryFightId;
    public Node father;
    public int X;
    public int Y;
    public int TargetX;
    public int TargetY;
    public float Distance;
    /// <summary>
    /// 1 可走  0障碍
    /// </summary>
    public int State;
    public int Tag;
    /// <summary>
    /// 建筑物的方向
    /// </summary>
    public int buildDirection;
    /// <summary>
    /// 拥有的类型
    /// </summary>
    public int hasType;
    /// <summary>
    /// 动态加载障碍物
    /// </summary>
    public bool dynamicObseale;
    /// <summary>
    /// 拥有的建筑路径
    /// </summary>
    public string hasBuildPath;
    /// <summary>
    /// 穿件类型
    /// </summary>
    public int createType;

    /// <summary>
    /// 可以创建的类型
    /// </summary>
    public List<int> canCreateList = new List<int>();

    /// <summary>
    /// 组ID
    /// </summary>
    public int groupId = 0;

    //从起点到方格的移动耗费
    public int G;
    //从本节点移动到终点的耗费
    public int H;

    //
    public int F;

    public Vector3 UpLeft;
    public Vector3 UpRight;
    public Vector3 DownLeft;
    public Vector3 DownRight;
    public Vector3 NodePosition;
    // 用于战斗的点定位 //
    public Vector3 battleDir;
    // 关隘的另一一个战斗点
	public Vector3 battleDirOther;
}


