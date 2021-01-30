using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lantis;
using System;

public class HexagonRuner : MonoBehaviour
{    
    public float width;
    public float height;
    public float diam;
    public int spawnWalkPosCount;
    public List<GameObject> spawnWalkBuild;
    public List<GameObject> spawnMapBuilds;
    private HexagonMap hexagonMap;

    void Start()
    {
        hexagonMap = new HexagonMap();
        hexagonMap.CreateHexagomMap(width, height, diam);
        hexagonMap.SpawnBuild(spawnWalkPosCount);
        hexagonMap.DrawBuild(spawnWalkBuild, spawnMapBuilds,0.755f);
    }
}
