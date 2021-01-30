using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 描述导航网格的3D顶点
/// </summary>
public class NavMeshVector3
{
    public float x;

    public float y;

    public float z;

    public NavMeshVector3 normals;

    public NavMeshVector3(float dx,float dy,float dz)
    {
        x = dx;
        y = dy;
        z = dz;
    }

    public static bool operator ==(NavMeshVector3 lhs, NavMeshVector3 rhs)
    {
        if (lhs == null || rhs == null)
            return false;

        if (lhs.GetType() != rhs.GetType())
            return false;

        if (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(NavMeshVector3 lhs, NavMeshVector3 rhs)
    {
        if (lhs == null || rhs == null)
            return true;

        if (lhs.GetType() != rhs.GetType())
            return true;

        if (lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z)
        {
            return true;
        }

        return false;
    }

    public static NavMeshVector3 operator +(NavMeshVector3 lhs, NavMeshVector3 rhs)
    {
        return new NavMeshVector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }

    public static NavMeshVector3 operator -(NavMeshVector3 lhs, NavMeshVector3 rhs)
    {
        return new NavMeshVector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        NavMeshVector3 other = obj as NavMeshVector3;

        if (x == other.x && y == other.y && z == other.z)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public static float GetDistance(NavMeshVector3 sourceV3, NavMeshVector3 subV3)
    {
        float xDistance = Mathf.Abs((sourceV3.x - subV3.x));
        float yDistance = Mathf.Abs((sourceV3.y - subV3.y));
        float zDistance = Mathf.Abs((sourceV3.z - subV3.z));

        float distance =Mathf.Sqrt(xDistance  * xDistance + yDistance * yDistance + zDistance * zDistance);

        return distance;
    }


    public static NavMeshVector3 GetDir(NavMeshVector3 sourceV3, NavMeshVector3 subV3)
    {
        return new NavMeshVector3(sourceV3.x - subV3.x, sourceV3.y - subV3.y, sourceV3.z - subV3.z);
    }

    public float Dot(NavMeshVector3 rhs)
    {
        return x * rhs.x + y * rhs.y + z * rhs.z;
    }

    public float DotNoY(NavMeshVector3 rhs)
    {
        return x * rhs.x + z * rhs.z;
    }
}

public class NavMeshEdge
{
    public NavMeshVector3 pos1;
    public NavMeshVector3 pos2;

    public NavTriangl relevanceTriangl;

    public static NavMeshEdge CreateNavMeshLine(NavMeshVector3 navMeshPos_1, NavMeshVector3 navMeshPos_2)
    {
        NavMeshEdge outLine = new NavMeshEdge();
        outLine.pos1 = navMeshPos_1;
        outLine.pos2 = navMeshPos_2;

        return outLine;
    }

    public static bool EdgeEquals(NavMeshEdge lhs, NavMeshEdge rhs)
    {
        if (lhs == null || rhs == null)
            return false;

        if (lhs.GetType() != rhs.GetType())
            return false;

        if ((lhs.pos1 == rhs.pos1 && lhs.pos2 == rhs.pos2) || (lhs.pos1 == rhs.pos2 && lhs.pos2 == rhs.pos1))
        {
            return true;
        }

        return false;
    }

    public static bool operator ==(NavMeshEdge lhs, NavMeshEdge rhs)
    {
        if (lhs == null || rhs == null)
            return false;

        if (lhs.GetType() != rhs.GetType())
            return false;

        if (lhs.pos1 == rhs.pos1 && lhs.pos2 == rhs.pos2)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(NavMeshEdge lhs, NavMeshEdge rhs)
    {
        if (lhs == null || rhs == null)
            return true;

        if (lhs.GetType() != rhs.GetType())
            return true;

        if (lhs.pos1 != rhs.pos1 || lhs.pos2 != rhs.pos2)
        {
            return true;
        }

        return false;
    }


    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        NavMeshEdge other = obj as NavMeshEdge;

        if (pos1 == other.pos1 && pos2 == other.pos2)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

public class NavTriangl
{
    /// <summary>
    /// 独立的id
    /// </summary>
    public int hashCode;

    public NavMeshVector3 pos1;
    public NavMeshVector3 pos2;
    public NavMeshVector3 pos3;

    public NavMeshEdge edge1;
    public NavMeshEdge edge2;
    public NavMeshEdge edge3;

    public NavMeshVector3 centerPos;

    public bool canWolk;


    /// <summary>
    /// 开始寻路网格记录的hasCode
    /// </summary>
    public int fromPathHashCode;
    /// <summary>
    /// 结束寻路网格记录的hasCode
    /// </summary>
    public int toPathHashCode;
    /// <summary>
    /// 能够寻路
    /// </summary>
    public bool canPath;
    /// <summary>
    /// 记录的寻路父节点
    /// </summary>
    public NavTriangl parent;


    public static NavTriangl CreateNavTriangl(NavMeshVector3 pos_1, NavMeshVector3 pos_2, NavMeshVector3 pos_3)
    {
        NavTriangl navTriangl = new NavTriangl();
        navTriangl.pos1 = pos_1;
        navTriangl.pos2 = pos_2;
        navTriangl.pos3 = pos_3;

        navTriangl.edge1 = NavMeshEdge.CreateNavMeshLine(pos_1, pos_2);
        navTriangl.edge2 = NavMeshEdge.CreateNavMeshLine(pos_1, pos_3);
        navTriangl.edge3 = NavMeshEdge.CreateNavMeshLine(pos_2, pos_3);

        navTriangl.UpCenterPos();

        return navTriangl;
    }

    public NavMeshEdge GetNearEdge(NavMeshVector3 pos)
    {
        float edge1Dis = NavMeshVector3.GetDistance(edge1.pos1, pos) + NavMeshVector3.GetDistance(edge1.pos2, pos);
        float edge2Dis = NavMeshVector3.GetDistance(edge2.pos1, pos) + NavMeshVector3.GetDistance(edge2.pos2, pos);
        float edge3Dis = NavMeshVector3.GetDistance(edge3.pos1, pos) + NavMeshVector3.GetDistance(edge3.pos2, pos);

        if (edge1Dis < edge2Dis && edge1Dis < edge3Dis)
        {
            return edge1;
        }

        if (edge2Dis < edge1Dis && edge2Dis < edge3Dis)
        {
            return edge2;
        }

        return edge3;

    }

    public NavMeshVector3[] GetPosArray()
    {
        return new NavMeshVector3[] { pos1, pos2, pos3 };
    }

    private void UpCenterPos()
    {
        centerPos = new NavMeshVector3((pos1.x + pos2.x + pos3.x)/3, (pos1.y + pos2.y + pos3.y) / 3,(pos1.z + pos2.z + pos3.z) / 3);
    }

    public NavMeshVector3 GetCenterPos()
    {
        return centerPos;
    }

    public NavMeshVector3 GetBestHeightPos()
    {
        if(pos1.y >= pos2.y && pos1.y >= pos3.y)
        {
            return pos1;
        }
        else
        if (pos2.y >= pos1.y && pos2.y >= pos3.y)
        {
            return pos2;
        }
        else
        {
            return pos3;
        }
    }

    public NavMeshVector3 GetBestLowPos()
    {
        if (pos1.y < pos2.y && pos1.y < pos3.y)
        {
            return pos1;
        }
        else
        if (pos2.y < pos1.y && pos2.y < pos3.y)
        {
            return pos2;
        }
        else
        {
            return pos3;
        }
    }

    /// <summary>
    /// 判断点在三角形内部
    /// http://www.cnblogs.com/graphics/archive/2010/08/05/1793393.html
    /// P = A +  u * (C – A) + v * (B - A) // 方程1
    /// u >= 0
    /// v >= 0
    /// u + v <= 1
    /// 
    /// u = ((v1•v1)(v2•v0)-(v1•v0)(v2•v1)) / ((v0•v0)(v1•v1) - (v0•v1)(v1•v0))
    /// v = ((v0•v0)(v2•v1)-(v0•v1)(v2•v0)) / ((v0•v0)(v1•v1) - (v0•v1)(v1•v0))
    /// </summary>
    /// <returns></returns>
    /// 
    public bool PosInTriangl(NavMeshVector3 pos)
    {
        NavMeshVector3 v0 = pos1 - pos;
        NavMeshVector3 v1 = pos2 - pos;
        NavMeshVector3 v2 = pos3 - pos;

        float dot00 = v0.Dot(v0);
        float dot01 = v0.Dot(v1);
        float dot02 = v0.Dot(v2);
        float dot11 = v1.Dot(v1);
        float dot12 = v1.Dot(v2);

        float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01);

        float u = (dot11 * dot02 - dot01 * dot12) * inverDeno;
        if (u < 0 || u > 1) // if u out of range, return directly
        {
            return false;
        }

        float v = (dot00 * dot12 - dot01 * dot02) * inverDeno;
        if (v < 0 || v > 1) // if v out of range, return directly
        {
            return false;
        }

        return u + v <= 1;
    }

    public bool PosInTriangl2D(NavMeshVector3 pos)
    {
        NavMeshVector3 v0 = pos1 - pos;
        NavMeshVector3 v1 = pos2 - pos;
        NavMeshVector3 v2 = pos3 - pos;

        float dot00 = v0.DotNoY(v0);
        float dot01 = v0.DotNoY(v1);
        float dot02 = v0.DotNoY(v2);
        float dot11 = v1.DotNoY(v1);
        float dot12 = v1.DotNoY(v2);

        float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01);

        float u = (dot11 * dot02 - dot01 * dot12) * inverDeno;
        if (u < 0 || u > 1) // if u out of range, return directly
        {
            return false;
        }

        float v = (dot00 * dot12 - dot01 * dot02) * inverDeno;
        if (v < 0 || v > 1) // if v out of range, return directly
        {
            return false;
        }

        return u + v <= 1;
    }

    public bool PosBelongNav(NavMeshVector3 pos)
    {
        if(PosInTriangl2D(pos))
        {
            NavMeshVector3 navHeightPos = GetBestHeightPos();
            NavMeshVector3 navLowPos = GetBestLowPos();

            if (pos.y <= navHeightPos.y && pos.y >= navLowPos.y)
            {
                return true;
            }
        }

        return false;
    }

    public float GetTrianglAngle()
    {
        NavMeshVector3 lowVector = null;
        NavMeshVector3 heightVector = null;
        if (pos1.y <= pos2.y && pos1.y <= pos3.y)
        {
            lowVector = pos1;
        }
        else
        if (pos2.y <= pos1.y && pos2.y <= pos3.y)
        {
            lowVector = pos2;
        }
        else
        {
            lowVector = pos3;
        }

        if (pos1.y >= pos2.y && pos1.y >= pos3.y)
        {
            heightVector = pos1;
        }
        else
        if (pos2.y >= pos1.y && pos2.y >= pos3.y)
        {
            heightVector = pos2;
        }
        else
        {
            heightVector = pos3;
        }

        NavMeshVector3 dir = NavMeshVector3.GetDir(heightVector, lowVector);

        float dirX = Mathf.Sqrt(dir.x * dir.x + dir.z * dir.z);

        float angle = Mathf.Atan2(dir.y, dirX) * 180 / Mathf.PI;

        return angle;
    }
}

public class NavSeeker
{
    public List<NavTriangl> NavTrianglList = new List<NavTriangl>();

    public NavTriangl[][][] NavTriangGrid3D;

    public NavMeshVector3 starOffsetPos;

    public NavMeshVector3 endOffsetPos;

    public NavMeshVector3 sizeDistance;

    int length;

    public float unitScale;

    /// <summary>
    /// 获取所有三维三角形网格的最小方向的边界
    /// </summary>
    /// <returns></returns>
    public NavMeshVector3 GetMinMeshBoundXYZ()
    {
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float minZ = float.MaxValue;

        for (int loop=0;loop< NavTrianglList.Count;++loop)
        {
            NavTriangl curNavPos = NavTrianglList[loop];
            NavMeshVector3[] trianglArray = curNavPos.GetPosArray();
            for (int indexPos = 0; indexPos < trianglArray.Length; ++indexPos)
            {
                NavMeshVector3 compartPos = trianglArray[indexPos];
                if(compartPos.x<minX)
                {
                    minX = compartPos.x;
                }

                if (compartPos.y < minY)
                {
                    minY = compartPos.y;
                }

                if (compartPos.z < minZ)
                {
                    minZ = compartPos.z;
                }
            }
        }

        return new NavMeshVector3(minX, minY, minZ);
    }

    public NavMeshVector3 GetMaxMeshBoundXYZ()
    {
        float maxX = float.MinValue;
        float maxY = float.MinValue;
        float maxZ = float.MinValue;

        for (int loop = 0; loop < NavTrianglList.Count; ++loop)
        {
            NavTriangl curNavPos = NavTrianglList[loop];
            NavMeshVector3[] trianglArray = curNavPos.GetPosArray();
            for (int indexPos = 0; indexPos < trianglArray.Length; ++indexPos)
            {
                NavMeshVector3 compartPos = trianglArray[indexPos];
                if (compartPos.x > maxX)
                {
                    maxX = compartPos.x;
                }

                if (compartPos.y > maxY)
                {
                    maxY = compartPos.y;
                }

                if (compartPos.z > maxZ)
                {
                    maxZ = compartPos.z;
                }
            }
        }

        return new NavMeshVector3(maxX, maxY, maxZ);
    }

    public float GetMaxDistanceAxisValue()
    {
        
        if(Mathf.Abs(sizeDistance.x)> Mathf.Abs(sizeDistance.y)&& Mathf.Abs(sizeDistance.x) > Mathf.Abs(sizeDistance.z))
        {
            return Mathf.Abs(sizeDistance.x);
        }
        else
        if (Mathf.Abs(sizeDistance.y) > Mathf.Abs(sizeDistance.x)&& Mathf.Abs(sizeDistance.y) > Mathf.Abs(sizeDistance.z))
        {
            return Mathf.Abs(sizeDistance.y);
        }
        else
        {
            return Mathf.Abs(sizeDistance.z);
        }
    }

    public void InitGridInfo()
    {
        int navCount = NavTrianglList.Count;
        NavTriangGrid3D = new NavTriangl[navCount][][];
        starOffsetPos = GetMinMeshBoundXYZ();
        endOffsetPos = GetMaxMeshBoundXYZ();
        sizeDistance = endOffsetPos - starOffsetPos;

        length = (int)NavTriangGrid3D.GetLongLength(0);
        float maxBoundDistance = GetMaxDistanceAxisValue();
        unitScale = maxBoundDistance / length;

        for (int loopX = 0; loopX < length; ++loopX)
        {
            for (int loopY = 0; loopY < length; ++loopY)
            {
                for (int loopZ = 0; loopZ < length; ++loopZ)
                {
                    NavTriangl curNavTriangl = NavTriangGrid3D[loopX][loopY][loopZ];

                    if(curNavTriangl == null)
                    {
                        curNavTriangl = GetPosAtNavTriangl(NavTrianglList, new NavMeshVector3(loopX* unitScale, loopY* unitScale, loopZ* unitScale) + starOffsetPos);

                        if(curNavTriangl != null)
                        {
                            NavTriangGrid3D[loopX][loopY][loopZ] = curNavTriangl;
                        }
                    }
                }
            }
        }
    }
    
    public NavTriangl GetPosAtNavTrianglQuick(NavMeshVector3 pos)
    {
        NavMeshVector3 realPos = pos - starOffsetPos;
        int x = Mathf.RoundToInt(realPos.x / unitScale);
        int y = Mathf.RoundToInt(realPos.y / unitScale);
        int z = Mathf.RoundToInt(realPos.z / unitScale);

        if (x >= 0 && x < length && x >= 0 && x < length && z >= 0 && z < length)
        {
            NavTriangl getNavTriangl = NavTriangGrid3D[x][y][z];

            if (getNavTriangl != null)
            {
                return getNavTriangl;
            }
        }

        return null;
    }

    public static NavTriangl GetPosAtNavTriangl(List<NavTriangl> navTrianglList,NavMeshVector3 pos)
    {
        for(int loop=0;loop< navTrianglList.Count;++loop)
        {
            NavTriangl curNavTriangl = navTrianglList[loop];

            if(curNavTriangl.PosBelongNav(pos))
            {
                return curNavTriangl;
            }
        }
        return null;
    }

    List<NavTriangl> OpenGoList = new List<NavTriangl>();
    public List<NavMeshVector3> FindPath(NavMeshVector3 starPos, NavMeshVector3 endPos)
    {
        NavTriangl starNavTriangl = GetPosAtNavTrianglQuick(starPos);
        NavTriangl endNavTriangl = GetPosAtNavTrianglQuick(endPos);
        if(starNavTriangl == null)
        {
            return null;
        }

        OpenGoList.Clear();
        starNavTriangl.parent = null;
        int fromHashCode = starNavTriangl.hashCode;
        int endHashCode = endNavTriangl.hashCode;

        NavTriangl curNavTriangl = starNavTriangl;
        OpenGoList.Add(curNavTriangl);
        while (curNavTriangl != endNavTriangl)
        {
            NavTriangl bestNavTriangl = GetNearBestGoodNavTriangl(curNavTriangl, endPos, fromHashCode, endHashCode);
            if(bestNavTriangl == null)
            {
                if (curNavTriangl == starNavTriangl)
                {
                    break;
                }
                curNavTriangl.canPath = false;
                curNavTriangl = curNavTriangl.parent;
            }
            else
            {
                bestNavTriangl.parent = curNavTriangl;
                curNavTriangl = bestNavTriangl;
                OpenGoList.Add(curNavTriangl);
            }
        }

        NavTriangl  pathNav = GetBestNearPosFromOpenList(endPos);

        if(pathNav != null)
        {
            return GetNavPath(pathNav);
        }

        return null;
    }

    public NavTriangl GetBestNearPosFromOpenList(NavMeshVector3 pos)
    {
        NavTriangl navRecord = null;
        float recordBestDistance = float.MaxValue;
        for(int loop = 0;loop< OpenGoList.Count;++loop)
        {
            NavTriangl navTriangl = OpenGoList[loop];
            float distance = NavMeshVector3.GetDistance(navTriangl.centerPos, pos);

            if(distance < recordBestDistance)
            {
                navRecord = navTriangl;
                recordBestDistance = distance;
            }
        }

        return navRecord;
    }

    private List<NavTriangl> canBestGoodChooseList = new List<NavTriangl>();
    public NavTriangl GetNearBestGoodNavTriangl(NavTriangl fromTriangl,NavMeshVector3 toPos,int fromHashCode,int endHashCode)
    {
        canBestGoodChooseList.Clear();
        
        if ((((fromTriangl.edge1.relevanceTriangl.fromPathHashCode == fromHashCode && fromTriangl.edge1.relevanceTriangl.toPathHashCode == endHashCode) 
            && fromTriangl.edge1.relevanceTriangl.canPath) 
            || (fromTriangl.edge1.relevanceTriangl.fromPathHashCode != fromHashCode || fromTriangl.edge1.relevanceTriangl.fromPathHashCode != endHashCode))
            && fromTriangl.edge1.relevanceTriangl.hashCode != fromHashCode && fromTriangl.edge1.relevanceTriangl.canWolk)
        {
            canBestGoodChooseList.Add(fromTriangl.edge1.relevanceTriangl);
        }

        if ((((fromTriangl.edge2.relevanceTriangl.fromPathHashCode == fromHashCode && fromTriangl.edge2.relevanceTriangl.toPathHashCode == endHashCode)
            && fromTriangl.edge2.relevanceTriangl.canPath)
            || (fromTriangl.edge2.relevanceTriangl.fromPathHashCode != fromHashCode || fromTriangl.edge2.relevanceTriangl.fromPathHashCode != endHashCode))
            && fromTriangl.edge2.relevanceTriangl.hashCode != fromHashCode && fromTriangl.edge2.relevanceTriangl.canWolk)
        {
            canBestGoodChooseList.Add(fromTriangl.edge2.relevanceTriangl);
        }

        if ((((fromTriangl.edge3.relevanceTriangl.fromPathHashCode == fromHashCode && fromTriangl.edge3.relevanceTriangl.toPathHashCode == endHashCode)
            && fromTriangl.edge3.relevanceTriangl.canPath)
            || (fromTriangl.edge3.relevanceTriangl.fromPathHashCode != fromHashCode || fromTriangl.edge3.relevanceTriangl.fromPathHashCode != endHashCode))
            && fromTriangl.edge3.relevanceTriangl.hashCode != fromHashCode && fromTriangl.edge3.relevanceTriangl.canWolk)
        {
            canBestGoodChooseList.Add(fromTriangl.edge3.relevanceTriangl);
        }

        NavTriangl bestNav = null;
        float distanceRecord = float.MaxValue;
        for(int loop = 0;loop<canBestGoodChooseList.Count;++loop)
        {
            NavTriangl navTriangl = canBestGoodChooseList[loop];
            float distance = NavMeshVector3.GetDistance(navTriangl.centerPos, toPos);

            if(distance < distanceRecord)
            {
                bestNav = navTriangl;
                distanceRecord = distance;
            }
        }

        if(bestNav != null)
        {
            bestNav.canPath = true;
            bestNav.fromPathHashCode = fromHashCode;
            bestNav.toPathHashCode = endHashCode;
            return bestNav;
        }
        else
        {
            return null;
        }

    }
    
    public List<NavMeshVector3> GetNavPath(NavTriangl navTriangl)
    {
        NavTriangl navlCurrent = navTriangl;
        List<NavTriangl> navLinkList = new List<NavTriangl>();        
        navLinkList.Add(navlCurrent);
        while (navlCurrent.parent!=null)
        {
            navLinkList.Add(navlCurrent.parent);
            navlCurrent = navlCurrent.parent;
        }

        List<NavMeshVector3> navPath = new List<NavMeshVector3>();

        for(int loop= navLinkList.Count-1;loop>=0;--loop)
        {
            NavTriangl navTrianglOut = navLinkList[loop];
            navPath.Add(navTrianglOut.centerPos);
        }

        return navPath;
    }
}
