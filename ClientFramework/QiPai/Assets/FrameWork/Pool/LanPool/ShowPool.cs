using UnityEngine;
using System.Collections;
using LanPool;
using System.Collections.Generic;

public class ShowPool : MonoBehaviour
{

    public Transform t;
    public string st = "PooA";
    List<Transform> ti = new List<Transform>();
    // Use this for initialization
    void OnGUI()
    {

        if (GUI.Button(new Rect(0, 0, 100, 30), "Create"))
        {

            PrefabPool pp = new PrefabPool();

            pp.preloadAmount = 4;

            pp.prefab = t;

            pp.cullAbove = 5;


            if (pp.InistancePrefabPool(PoolManager.Pools[st].transform, PoolManager.Pools[st]))
                PoolManager.Pools[st]._perPrefabPoolOptions.Add(pp);
        }

        if (GUI.Button(new Rect(120, 0, 100, 30), "Clean"))
        {
            PoolManager.Pools[st].CleanAll();
        }

        if (GUI.Button(new Rect(0, 40, 100, 30), "Spawn"))
        {
            Transform tt = PoolManager.Pools[st].Spawn(t, new Vector3(0, 0, 0), Quaternion.identity);
            ti.Add(tt);
        }

        if (GUI.Button(new Rect(120, 40, 100, 30), "Despawn"))
        {
            if (ti.Count > 0)
            {
                PoolManager.Pools[st].Despawn(ti[0]);

                ti.RemoveAt(0);
            }
        }

        if (GUI.Button(new Rect(0, 80, 100, 30), "DespawnAll"))
        {
            if (ti.Count > 0)
            {
                PoolManager.Pools[st].DespawnAll();

                ti.Clear();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
