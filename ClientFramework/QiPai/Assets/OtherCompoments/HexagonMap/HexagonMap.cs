using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lantis
{
    public class HexagonInt2
    {
        public int value_1;
        public int value_2;

        public HexagonInt2(int setValue_1, int setValue_2)
        {
            value_1 = setValue_1;
            value_2 = setValue_2;
        }
    }

    public class HexagonMap
    {
        public HexagonGrid[][] mapDatas;
        public void CreateHexagomMap(float _width, float _height, float _diam)
        {
            int xCount = (int)Math.Ceiling(_width / _diam);
            int yCount = (int)Math.Ceiling(_width / _diam * 1.15f);

            mapDatas = new HexagonGrid[xCount][];
            for (var x = 0; x < xCount; ++x)
            {
                mapDatas[x] = new HexagonGrid[yCount];
            }

            for (var x = 0; x < xCount; ++x)
            {
                for (var y = 0; y < yCount; ++y)
                {
                    HexagonGrid grid = new HexagonGrid();
                    grid.Init(x, y, _diam);
                    mapDatas[x][y] = grid;
                }
            }
        }

        public void SpawnBuild(int count)
        {
            int spawnCount = count;
            List<HexagonGrid> cantSpawnList = new List<HexagonGrid>();
            
            Action<HexagonGrid[][], bool> setArrayAllObs = (dataArray, isObs) =>
            {
                for (var i = 0; i < dataArray.Length; ++i)
                {
                    for (var j = 0; j < dataArray[i].Length; ++j)
                    {
                        dataArray[i][j].isObs = isObs;
                    }
                }
            };

            Action<List<List<HexagonGrid>>, bool> setList2AllObs = (dataArray, isObs) =>
            {
                for (var i = 0; i < dataArray.Count; ++i)
                {
                    for (var j = 0; j < dataArray[i].Count; ++j)
                    {
                        dataArray[i][j].isObs = isObs;
                    }
                }
            };

            Action<List<HexagonGrid>, bool> setList1AllObs = (nodes, isObs) =>
            {
                for (var i = 0; i < nodes.Count; ++i)
                {
                    nodes[i].isObs = isObs;
                }
            };

            Action<List<HexagonGrid>, List<HexagonGrid>> addToListNotHas = (toList, dataList) =>
            {
                for (var i = 0; i < dataList.Count; ++i)
                {
                    if (!toList.Exists(item => item == dataList[i]))
                    {
                        toList.Add(dataList[i]);
                    }
                }
            };

            Action<List<HexagonGrid>, List<HexagonGrid>> removeListFromData = (toList, dataList) =>
            {
                for (var i = 0; i < dataList.Count; ++i)
                {
                    if (toList.Exists(item => item == dataList[i]))
                    {
                        toList.Remove(dataList[i]);
                    }
                }
            };

            Action<HexagonFindPath, List<HexagonGrid>, List<HexagonGrid>> addToListFromDataNear = (_fp, toList, dataList) =>
            {
                for (var i = 0; i < dataList.Count; ++i)
                {
                    addToListNotHas(toList, _fp.GetNearHexagon(dataList[i], mapDatas));
                }
            };

            Func<HexagonFindPath, List<List<HexagonGrid>>, List<List<HexagonGrid>>> combinePath_1 = (_fp, hexagonListGroup) =>
            {
                List<List<HexagonGrid>> allPathNodes = new List<List<HexagonGrid>>();
                for (var i = 0; i < hexagonListGroup.Count; ++i)
                {
                    var pathSe = hexagonListGroup[i];
                    var first = pathSe[0];
                    var last = pathSe[pathSe.Count - 1];
                    first.isObs = false;
                    last.isObs = false;
                    removeListFromData(cantSpawnList, _fp.GetNearHexagon(first, mapDatas));
                    removeListFromData(cantSpawnList, _fp.GetNearHexagon(last, mapDatas));

                    var pathNodes = _fp.FindPathToNodes(first, last, mapDatas, cantSpawnList);
                    if (pathNodes == null && pathNodes.Exists(item => item == last))
                    {
                        continue;
                    }
                    setList1AllObs(pathNodes, true);
                    allPathNodes.Add(pathNodes);
                    addToListFromDataNear(_fp, cantSpawnList, pathNodes);
                }
                return allPathNodes;
            };

            Func<HexagonFindPath, List<List<HexagonGrid>>, List<List<HexagonGrid>>> combinePath_2 = (_fp, hexagonListGroup) =>
            {
                while (hexagonListGroup.Count > 1)
                {
                    setArrayAllObs(mapDatas, false);
                    setList2AllObs(hexagonListGroup, true);

                    var curList = hexagonListGroup[0];
                    float recordMinDistance = float.MaxValue;
                    List<HexagonGrid> recordList = null;
                    HexagonGrid firstNode = null;
                    HexagonGrid lastNode = null;
                    for (var i = 1; i < hexagonListGroup.Count; ++i)
                    {
                        var compareList = hexagonListGroup[i];
                        float distance_1 = HexagonFindPath.GetDistance(curList[0].basePos, compareList[0].basePos);
                        firstNode = curList[0];
                        lastNode = compareList[0];

                        float distance_2 = HexagonFindPath.GetDistance(curList[0].basePos, compareList[compareList.Count - 1].basePos);
                        if (distance_2 < distance_1)
                        {
                            distance_1 = distance_2;
                            firstNode = curList[0];
                            lastNode = compareList[compareList.Count - 1];
                        }

                        distance_2 = HexagonFindPath.GetDistance(curList[curList.Count - 1].basePos, compareList[0].basePos);
                        if (distance_2 < distance_1)
                        {
                            distance_1 = distance_2;
                            firstNode = curList[curList.Count - 1];
                            lastNode = compareList[0];
                        }

                        distance_2 = HexagonFindPath.GetDistance(curList[curList.Count - 1].basePos, compareList[compareList.Count - 1].basePos);
                        if (distance_2 < distance_1)
                        {
                            distance_1 = distance_2;
                            firstNode = curList[curList.Count - 1];
                            lastNode = compareList[compareList.Count - 1];
                        }

                        if (distance_1 < recordMinDistance)
                        {
                            recordMinDistance = distance_1;
                            recordList = compareList;
                        }
                    }

                    hexagonListGroup.RemoveAt(0);
                    firstNode.isObs = false;
                    lastNode.isObs = false;
                    removeListFromData(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                    removeListFromData(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                    var pathNodes = _fp.FindPathToNodes(firstNode, lastNode, mapDatas, cantSpawnList);
                    if (pathNodes == null || pathNodes.Exists(item => item == lastNode))
                    {
                        firstNode.isObs = true;
                        lastNode.isObs = true;
                        addToListNotHas(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                        addToListNotHas(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                        firstNode = curList[0];
                        lastNode = recordList[0];
                        firstNode.isObs = false;
                        lastNode.isObs = false;
                        removeListFromData(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                        removeListFromData(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                        pathNodes = _fp.FindPathToNodes(firstNode, lastNode, mapDatas, cantSpawnList);
                        if (pathNodes == null || pathNodes.Exists(item => item == lastNode))
                        {
                            firstNode.isObs = true;
                            lastNode.isObs = true;
                            addToListNotHas(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                            addToListNotHas(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                            firstNode = curList[0];
                            lastNode = recordList[recordList.Count - 1];
                            firstNode.isObs = false;
                            lastNode.isObs = false;
                            removeListFromData(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                            removeListFromData(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                            pathNodes = _fp.FindPathToNodes(firstNode, lastNode, mapDatas, cantSpawnList);
                            if (pathNodes == null || pathNodes.Exists(item => item == lastNode))
                            {
                                firstNode.isObs = true;
                                lastNode.isObs = true;
                                addToListNotHas(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                                addToListNotHas(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                                firstNode = curList[curList.Count - 1];
                                lastNode = recordList[recordList.Count - 1];
                                firstNode.isObs = false;
                                lastNode.isObs = false;
                                removeListFromData(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                                removeListFromData(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                                pathNodes = _fp.FindPathToNodes(firstNode, lastNode, mapDatas, cantSpawnList);
                                if (pathNodes == null || pathNodes.Exists(item => item == lastNode))
                                {
                                    firstNode.isObs = true;
                                    lastNode.isObs = true;
                                    addToListNotHas(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                                    addToListNotHas(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                                    firstNode = curList[curList.Count - 1];
                                    lastNode = recordList[0];
                                    firstNode.isObs = false;
                                    lastNode.isObs = false;
                                    removeListFromData(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                                    removeListFromData(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));

                                    pathNodes = _fp.FindPathToNodes(firstNode, lastNode, mapDatas, cantSpawnList);
                                }
                            }
                        }
                    }

                    if (pathNodes == null || !pathNodes.Exists(item => item == lastNode))
                    {
                        firstNode.isObs = true;
                        lastNode.isObs = true;
                        addToListNotHas(cantSpawnList, _fp.GetNearHexagon(firstNode, mapDatas));
                        addToListNotHas(cantSpawnList, _fp.GetNearHexagon(lastNode, mapDatas));
                        removeListFromData(cantSpawnList, curList);
                        if (pathNodes == null)
                        {
                            Debug.Log("pathNodes not font");
                        }
                        Debug.LogError("合并时候有路径无法联通");
                        return null;
                    }


                    recordList.AddRange(curList);
                    for (var i = 0; i < pathNodes.Count; ++i)
                    {
                        if (!recordList.Exists(item => item == pathNodes[i]))
                        {
                            recordList.Add(pathNodes[i]);
                        }
                    }
                    setList1AllObs(recordList, true);
                }

                return hexagonListGroup;
            };

            if (spawnCount % 2 == 1)
            {
                spawnCount++;
            }

            int xCount = mapDatas.Length;
            int yCount = mapDatas[0].Length;
            if (spawnCount > xCount * yCount)
            {
                Debug.LogError($"生成路点数量超出 xCount * yCount = {  xCount * yCount }!");
                return;
            }

            HexagonFindPath fp = new HexagonFindPath();

        _ReBuild:
            Debug.Log("执行生成!");
            count = spawnCount;
            setArrayAllObs(mapDatas, false);
            cantSpawnList.Clear();
            //随机出来的路点
            List<HexagonGrid> hexagonPathNodes = new List<HexagonGrid>();
            while (count > 0)
            {
                int x = UnityEngine.Random.Range(0, xCount);
                int y = UnityEngine.Random.Range(0, yCount);
                var hexagon = mapDatas[x][y];
                if (!hexagonPathNodes.Exists(item => item == hexagon) && !cantSpawnList.Exists(item => item == hexagon))
                {
                    addToListNotHas(cantSpawnList, fp.GetNearHexagon(hexagon, mapDatas));
                    hexagonPathNodes.Add(hexagon);
                    count--;
                }
            }

            ///多个点组合路径
            List<List<HexagonGrid>> hexagonMaps = new List<List<HexagonGrid>>();
            while (hexagonPathNodes.Count > 1)
            {
                var firstNode = hexagonPathNodes[0];
                float recordDis = float.MaxValue;
                HexagonGrid mustDistanceNode = null;
                for (var i = 1; i < hexagonPathNodes.Count; ++i)
                {
                    var thisNode = hexagonPathNodes[i];
                    float curDis = Vector3.Distance(firstNode.basePos, thisNode.basePos);
                    if (curDis < recordDis)
                    {
                        mustDistanceNode = thisNode;
                        recordDis = curDis;
                    }
                }
                hexagonPathNodes.RemoveAt(0);
                hexagonPathNodes.Remove(mustDistanceNode);
                List<HexagonGrid> kp = new List<HexagonGrid>();
                kp.Add(firstNode);
                kp.Add(mustDistanceNode);
                hexagonMaps.Add(kp);
            }

            setArrayAllObs(mapDatas, false);
            setList2AllObs(hexagonMaps, true);

            hexagonMaps = combinePath_1(fp, hexagonMaps);

            //多段路径都是障碍了
            if (hexagonMaps.Count > 1)
            {
                hexagonMaps = combinePath_2(fp, hexagonMaps);
                if (hexagonMaps == null)
                {
                    goto _ReBuild;
                }
            }

            List<HexagonGrid> listAllNodexToPath = new List<HexagonGrid>();
            for (var i = 0; i < hexagonMaps.Count; ++i)
            {
                var curNodes = hexagonMaps[i];
                for (var j = 0; j < curNodes.Count; ++j)
                {
                    listAllNodexToPath.Add(curNodes[j]);
                }
            }

            for (var x = 0; x < mapDatas.Length; ++x)
            {
                for (var y = 0; y < mapDatas[x].Length; ++y)
                {
                    mapDatas[x][y].isObs = true;
                }
            }
            for (var i = 0; i < listAllNodexToPath.Count; ++i)
            {
                listAllNodexToPath[i].isObs = false;
            }
        }

        public void DrawBuild(List<GameObject> walkBuilds,List<GameObject> builds,float scale)
        {
            for (var x = 0; x < mapDatas.Length; ++x)
            {
                for (var y = 0; y < mapDatas[x].Length; ++y)
                {
                    var curData = mapDatas[x][y];
                    GameObject sourceTarget = null;
                    if (!curData.isObs)
                    {
                        sourceTarget = walkBuilds[UnityEngine.Random.Range(0, walkBuilds.Count)];
                    }
                    else
                    {
                        sourceTarget = builds[UnityEngine.Random.Range(0, builds.Count)];
                    }
                    curData.Draw(sourceTarget);
                    curData.SetScaleBig(scale);
                }
            }
        }

        public void TestDistance()
        {
            var endNode = mapDatas[7][7];
            var first = mapDatas[5][5];
            var last = mapDatas[6][5];

            HexagonFindPath ps = new HexagonFindPath();
            float dis_1 = HexagonFindPath.GetDistance(first.basePos, endNode.basePos);
            float dis_2 = HexagonFindPath.GetDistance(last.basePos, endNode.basePos);
        }

        public void FindPathNodeShow(int startX, int startY, int endX, int endY)
        {
            HexagonFindPath fp = new HexagonFindPath();
            var listNodes = fp.FindPathToNodes(mapDatas[startX][startY], mapDatas[endX][endY], mapDatas,null);
            for (var x = 0; x < mapDatas.Length; ++x)
            {
                for (var y = 0; y < mapDatas[x].Length; ++y)
                {
                    mapDatas[x][y].node.SetActive(false);
                }
            }

            for (var i = 0; i < listNodes.Count; ++i)
            {
                listNodes[i].node.SetActive(true);
            }
        }

        public void HiddentRound(int x, int z)
        {
            HexagonFindPath fp = new HexagonFindPath();
            var hexagonList = fp.GetNearHexagon(mapDatas[x][z], mapDatas);

            for (var i = 0; i < mapDatas.Length; ++i)
            {
                for (var y = 0; y < mapDatas[x].Length; ++y)
                {
                    var thishNode = mapDatas[i][y];

                    bool has = false;
                    for (var f = 0; f < hexagonList.Count; ++f)
                    {
                        var curNode = hexagonList[f];
                        if (thishNode == curNode)
                        {
                            has = true;
                            break;
                        }
                    }
                    if (!has)
                    {
                        thishNode.node.SetActive(false);
                    }
                }
            }

            mapDatas[x][z].node.SetActive(true);
            mapDatas[x][z].SetScaleBig(1);
        }
    }

    public class HexagonGrid
    {
        public const float hexagonDiamLongScale = 1.15f;

        public int x, y;
        private float diam;
        public Vector3 basePos;

        public List<Vector3> posList = new List<Vector3>();
        public HexagonGrid father;
        public bool isObs;
        public GameObject node;
        public void Init(int _x, int _y, float _diam)
        {
            x = _x;
            y = _y;
            diam = _diam;

            posList.Add(new Vector3(0.0f, 0.0f, diam / 2 * hexagonDiamLongScale));
            posList.Add(GetRoadAnglePos(posList[0], Vector3.zero, 60.0f));
            posList.Add(GetRoadAnglePos(posList[1], Vector3.zero, 60.0f));
            posList.Add(GetRoadAnglePos(posList[2], Vector3.zero, 60.0f));
            posList.Add(GetRoadAnglePos(posList[3], Vector3.zero, 60.0f));
            posList.Add(GetRoadAnglePos(posList[4], Vector3.zero, 60.0f));

            int yMod = y % 2;
            float outX = 0;
            float outY = 0;
            if (yMod == 0)
            {
                outX = _x * _diam;
            }
            else
            {
                outX = _x * _diam + _diam * 0.5f;
            }

            outY = _y * _diam * (1.0f / hexagonDiamLongScale);

            basePos = new Vector3(outX, 0.0f, outY);

            for (var i = 0; i < posList.Count; ++i)
            {
                posList[i] += basePos;
            }
        }

        public Vector3 GetRoadAnglePos(Vector3 targetPos, Vector3 centerPos, float angle)
        {
            float l = (float)((angle * Math.PI) / 180);
            //sin/cos value 
            float cosv = (float)Math.Cos(l);
            float sinv = (float)Math.Sin(l);
            float newX = (float)((targetPos.x - centerPos.x) * cosv - (targetPos.z - centerPos.z) * sinv + centerPos.x);
            float newY = (float)((targetPos.x - centerPos.x) * sinv + (targetPos.z - centerPos.z) * cosv + centerPos.z);

            return new Vector3(newX, 0, newY);
        }

        public void DrawOne()
        {
            var showObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            showObj.transform.position = basePos;
            showObj.transform.localScale = Vector3.one;
        }

        public void Draw(GameObject source)
        {
            node = GameObject.Instantiate(source);
            node.name = $"{x}_{y}";
            node.transform.position = basePos;
        }

        public void SetScaleBig(float scale)
        {
            node.transform.localScale = Vector3.one * scale;
        }
    }

    public class HexagonFindPath
    {
        private List<HexagonGrid> closeList = new List<HexagonGrid>();
        private List<HexagonGrid> openList = new List<HexagonGrid>();

        public HexagonGrid FindPathWithOpenAndClose(HexagonGrid startNode, HexagonGrid endNode, HexagonGrid[][] mapData, List<HexagonGrid> excludeList = null)
        {
            openList.Clear();
            closeList.Clear();
            startNode.father = null;
            openList.Add(startNode);

            if (startNode == endNode || (startNode.x == endNode.x && startNode.y == endNode.y))
            {
                return endNode;
            }

            while (openList.Count > 0)
            {
                var currentNode = GetMinDistanceFromOpenList(endNode);
                if (currentNode != null)
                {
                    openList.Remove(currentNode);
                    closeList.Add(currentNode);

                    List<HexagonGrid> hexagonGrids = GetNearHexagon(currentNode, mapData);
                    for (int i = 0; i < hexagonGrids.Count; i++)
                    {
                        if (excludeList != null && excludeList.Exists(item => item == hexagonGrids[i]))
                        {
                            continue;
                        }

                        if (!closeList.Exists(item => item == hexagonGrids[i]) && !hexagonGrids[i].isObs)
                        {
                            if (openList.Exists(item => item == hexagonGrids[i]))
                            {
                            }
                            else
                            {
                                hexagonGrids[i].father = currentNode;
                                openList.Add(hexagonGrids[i]);
                            }
                        }
                    }
                }
            }

            var returnNode = OpenListGetEnd(endNode);
            if (returnNode != null)
            {
                return returnNode;
            }

            return GetMinDistanceFromCloseList(endNode);
        }

        public List<HexagonGrid> FindPathToNodes(HexagonGrid startNode, HexagonGrid endNode, HexagonGrid[][] mapData,List<HexagonGrid> excludeList)
        {
            //这个是Line是节点的X Y的值
            List<HexagonGrid> linePositions = new List<HexagonGrid>();
            List<HexagonGrid> pathPostions = new List<HexagonGrid>();
            linePositions.Clear();
            pathPostions.Clear();

            if (startNode != null && endNode != null)
            {
                var findNode = FindPathWithOpenAndClose(startNode, endNode, mapData, excludeList);

                if (findNode != null)
                {
                    Node2Path(findNode, linePositions);

                    for (int indexLine = 0; indexLine < linePositions.Count; indexLine++)
                    {
                        pathPostions.Add(mapData[(int)linePositions[indexLine].x][(int)linePositions[indexLine].y]);
                    }

                    if (pathPostions.Count == 1 && startNode != endNode)
                    {
                        pathPostions[0] = startNode;

                        if (findNode == endNode)
                        {
                            pathPostions.Add(endNode);
                        }
                    }
                    else
                    {
                        pathPostions[0] = startNode;

                        if (findNode == endNode)
                        {
                            pathPostions[pathPostions.Count - 1] = endNode;
                        }
                    }
                }
            }

            if (!pathPostions.Exists(item => item == startNode))
            {
                Debug.LogError("寻路结果不包含开始结果!");
            }

            if (!pathPostions.Exists(item => item == startNode))
            {
                Debug.LogError("寻路结果不包含结束结果!");
            }

            return pathPostions;
        }


        public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos, HexagonGrid[][] mapData)
        {
            HexagonGrid startNode = GetHexagon(startPos, mapData);
            HexagonGrid endNode = GetHexagon(endPos, mapData);

            //这个是Line是节点的X Y的值
            List<Vector3> linePositions = new List<Vector3>();
            List<Vector3> pathPostions = new List<Vector3>();
            linePositions.Clear();
            pathPostions.Clear();

            if (startNode != null && endNode != null)
            {
                var findNode = FindPathWithOpenAndClose(startNode, endNode, mapData);

                if (findNode != null)
                {
                    Node2Path(findNode, linePositions);

                    for (int indexLine = 0; indexLine < linePositions.Count; indexLine++)
                    {
                        pathPostions.Add(mapData[(int)linePositions[indexLine].x][(int)linePositions[indexLine].z].basePos);
                    }

                    if (pathPostions.Count == 1 && startPos != endPos)
                    {
                        pathPostions[0] = startPos;

                        if (findNode == endNode)
                        {
                            pathPostions.Add(endPos);
                        }
                    }
                    else
                    {
                        pathPostions[0] = startPos;

                        if (findNode == endNode)
                        {
                            pathPostions[pathPostions.Count - 1] = endPos;
                        }
                    }
                }
            }

            return pathPostions;
        }



        public HexagonGrid GetMinDistanceFromOpenList(HexagonGrid endNode)
        {
            float minDistance = float.MaxValue;
            HexagonGrid minNode = null;
            for (int index = 0; index < openList.Count; index++)
            {
                var node = openList[index];
                float distance = GetDistance(node.basePos, endNode.basePos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minNode = node;
                }
            }

            return minNode;
        }

        public HexagonGrid GetMinDistanceFromCloseList(HexagonGrid endNode)
        {
            float minDistance = float.MaxValue;
            HexagonGrid minNode = null;
            for (int index = 0; index < closeList.Count; index++)
            {
                var node = closeList[index];
                float distance = GetDistance(node.basePos, endNode.basePos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minNode = node;
                }
            }

            return minNode;
        }

        public List<HexagonGrid> GetNearHexagon(HexagonGrid node, HexagonGrid[][] mapData)
        {
            List<HexagonGrid> nearHexagonNodes = new List<HexagonGrid>();

            List<HexagonInt2> hexagonInt2s = new List<HexagonInt2>();
            if (node.y % 2 == 1)
            {
                hexagonInt2s.Add(new HexagonInt2(node.x, node.y + 1));
                hexagonInt2s.Add(new HexagonInt2(node.x + 1, node.y + 1));
                hexagonInt2s.Add(new HexagonInt2(node.x + 1, node.y));
                hexagonInt2s.Add(new HexagonInt2(node.x, node.y));
                hexagonInt2s.Add(new HexagonInt2(node.x + 1, node.y - 1));
                hexagonInt2s.Add(new HexagonInt2(node.x, node.y - 1));
            }
            else
            {
                hexagonInt2s.Add(new HexagonInt2(node.x - 1, node.y + 1));
                hexagonInt2s.Add(new HexagonInt2(node.x, node.y + 1));
                hexagonInt2s.Add(new HexagonInt2(node.x + 1, node.y));
                hexagonInt2s.Add(new HexagonInt2(node.x - 1, node.y));
                hexagonInt2s.Add(new HexagonInt2(node.x, node.y - 1));
                hexagonInt2s.Add(new HexagonInt2(node.x - 1, node.y - 1));
            }

            for (var i = 0; i < hexagonInt2s.Count; ++i)
            {
                var int2Value = hexagonInt2s[i];
                if (int2Value.value_1 >= 0 && int2Value.value_2 >= 0 &&
                    int2Value.value_1 < mapData.Length && int2Value.value_2 < mapData[int2Value.value_1].Length)
                {
                    if (!mapData[int2Value.value_1][int2Value.value_2].isObs)
                    {
                        nearHexagonNodes.Add(mapData[int2Value.value_1][int2Value.value_2]);
                    }
                }
            }

            return nearHexagonNodes;
        }

        //从Open中找结束的点
        private HexagonGrid OpenListGetEnd(HexagonGrid end)
        {
            HexagonGrid endNode = openList.Find(item => item == end);

            return endNode;
        }
        /// <summary>
        /// 把Node 返回到path
        /// </summary>
        /// <param name="node"></param>
        /// <param name="linePosition"></param>
        public void Node2Path(HexagonGrid node, List<Vector3> linePosition)
        {
            if (node.father != null)
            {

                Node2Path(node.father, linePosition);
            }

            linePosition.Add(new Vector3(node.x, 0, node.y));
        }

        /// <summary>
        /// 把Node 返回到path
        /// </summary>
        /// <param name="node"></param>
        /// <param name="lineNodes"></param>
        public void Node2Path(HexagonGrid node, List<HexagonGrid> lineNodes)
        {
            if (node.father != null)
            {

                Node2Path(node.father, lineNodes);
            }

            lineNodes.Add(node);
        }


        public HexagonGrid GetHexagon(Vector3 pos, HexagonGrid[][] mapData)
        {
            HexagonGrid selectNode = null;
            for (var x = 0; x < mapData.Length; ++x)
            {
                for (var y = 0; y < mapData[x].Length; ++y)
                {
                    HexagonGrid currentNode = mapData[x][y];

                    if (PointinTriangle(currentNode.posList[0], currentNode.posList[1], currentNode.posList[5], pos))
                    {
                        selectNode = currentNode;
                        break;
                    }

                    if (PointinTriangle(currentNode.posList[1], currentNode.posList[2], currentNode.posList[3], pos))
                    {
                        selectNode = currentNode;
                        break;
                    }

                    if (PointinTriangle(currentNode.posList[3], currentNode.posList[4], currentNode.posList[5], pos))
                    {
                        selectNode = currentNode;
                        break;
                    }

                    if (PointinTriangle(currentNode.posList[1], currentNode.posList[3], currentNode.posList[5], pos))
                    {
                        selectNode = currentNode;
                        break;
                    }
                }
            }

            return selectNode;
        }



        /// <summary>
        /// 点是否在三角形内
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static bool PointinTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
        {
            Vector3 v0 = C - A;
            Vector3 v1 = B - A;
            Vector3 v2 = P - A;

            float dot00 = Vector3.Dot(v0, v0);
            float dot01 = Vector3.Dot(v0, v1);
            float dot02 = Vector3.Dot(v0, v2);
            float dot11 = Vector3.Dot(v1, v1);
            float dot12 = Vector3.Dot(v1, v2);
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

        public static float GetDistance(Vector3 first, Vector3 last)
        {
            float xDistance = Mathf.Abs(first.x - last.x);
            float zDistance = Mathf.Abs(first.z - last.z);
            return Mathf.Sqrt(xDistance * xDistance + zDistance * zDistance);
        }

        public static HexagonGrid GetMinDistanceFromList(HexagonGrid tarNode, List<HexagonGrid> listData)
        {
            float minDistance = float.MaxValue;
            HexagonGrid minNode = null;
            for (int index = 0; index < listData.Count; index++)
            {
                var node = listData[index];
                float distance = GetDistance(node.basePos, tarNode.basePos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minNode = node;
                }
            }

            return minNode;
        }
    }
}