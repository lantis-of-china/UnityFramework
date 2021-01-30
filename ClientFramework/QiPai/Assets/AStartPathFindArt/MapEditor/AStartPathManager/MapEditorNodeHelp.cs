using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapEditorNodeHelp 
{

    public static Vector2 InstanceMapDate(string mapSources,out Node[][] Map,out float pathPointLenght)
    {
        int CurrentP = 0;
        string[] inforArray = mapSources.Split('|');

        float PathPointLenght = Convert.ToSingle(inforArray[CurrentP++]);

        pathPointLenght = PathPointLenght;

        int XCount = Convert.ToInt32(inforArray[CurrentP++]);
        int YCount = Convert.ToInt32(inforArray[CurrentP++]);

        Map = new Node[XCount][];
        for (var i = 0; i < Map.Length; ++i)
        {
            Map[i] = new Node[YCount];
        }

        for (int indexX = 0; indexX < XCount; indexX++)
        {
            for (int indexY = 0; indexY < YCount; indexY++)
            {
                Node aNode = new Node();
                aNode.Id = Convert.ToInt32(inforArray[CurrentP++]);
                aNode.CountryFightId = Convert.ToInt32(inforArray[CurrentP++]);
                aNode.X = Convert.ToInt32(inforArray[CurrentP++]);
                aNode.Y = Convert.ToInt32(inforArray[CurrentP++]);
                aNode.TargetX = Convert.ToInt32(inforArray[CurrentP++]);
                aNode.TargetY = Convert.ToInt32(inforArray[CurrentP++]);
                aNode.Distance =(float)Convert.ToDouble(inforArray[CurrentP++]);

                if (inforArray[CurrentP++] == "False")
                {
                    aNode.State = 1;
                }
                else
                {
                    aNode.State = 0;
                }

                ///Tag读取
                aNode.Tag = Convert.ToInt32(inforArray[CurrentP++]);

                aNode.buildDirection = Convert.ToInt32(inforArray[CurrentP++]);

                aNode.hasType = Convert.ToInt32(inforArray[CurrentP++]);
                
                aNode.dynamicObseale = bool.Parse(inforArray[CurrentP++]);

                aNode.hasBuildPath = inforArray[CurrentP++];

                aNode.createType = Convert.ToInt32(inforArray[CurrentP++]);

                aNode.groupId = Convert.ToInt32(inforArray[CurrentP++]);

                string canCreateStr = inforArray[CurrentP++];

                if (!string.IsNullOrEmpty(canCreateStr))
                {
                    string[] nodeCanCreateList = canCreateStr.Split(',');

                    for (int loopInfor = 0; loopInfor < nodeCanCreateList.Length; ++loopInfor)
                    {
                        string sInfor = nodeCanCreateList[loopInfor];

                        if (aNode.canCreateList == null)
                        {
                            aNode.canCreateList = new List<int>();
                        }

                        aNode.canCreateList.Add(Convert.ToInt32(sInfor));
                    }
                }


                aNode.DownLeft = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));
                aNode.DownRight = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));
                aNode.UpLeft = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));
                aNode.UpRight = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));
                aNode.NodePosition = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));
                aNode.battleDir = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));

                // 读取另一个战斗配点
                if (!aNode.battleDir.Equals(Vector3.zero))
                {
                    aNode.battleDirOther = new Vector3(Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]), Convert.ToSingle(inforArray[CurrentP++]));
                }

                Map[indexX][indexY] = aNode;
            }
        }
        return new Vector2(XCount, YCount);
    }
}
