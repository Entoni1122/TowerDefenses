using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

namespace TowerDefense
{
    public class Pathfinding
    {
        //List<PathNode> toSearch = new List<PathNode>();
        //List<PathNode> processed = new List<PathNode>();



        //List<PathNode> FindPath(PathNode start, PathNode end)
        //{
        //    toSearch.Add(start);
        //    List<PathNode> truePath = new List<PathNode>();
        //    while (toSearch.Count > 0)
        //    {
        //        PathNode current = toSearch[0];
        //        for (int i = 0; i < toSearch.Count; i++)
        //        {
        //            if (toSearch[i].Fcost < current.Fcost || toSearch[i].Fcost == current.Fcost && toSearch[i].Hcost < current.Hcost)
        //            {
        //                current = toSearch[i];
        //            }
        //        }

        //        toSearch.Remove(current);
        //        processed.Add(current);

        //        if (current == end)
        //        {
        //            return GetTruePath(start, end);
        //        }

        //        foreach (PathNode node in GetNeighbors(toSearch))
        //        {

        //        }




        //    }

        //}
        //List<PathNode> GetTruePath(PathNode start, PathNode end)
        //{
        //    List<PathNode> node = new List<PathNode>();

        //    PathNode current = end;

        //    while (current != start)
        //    {
        //        node.Add(current);
        //        current = current.cameFromNode;
        //    }

        //    node.Reverse();
        //    return node;
        //}

        //List<PathNode> GetNeighbors(PathNode node)
        //{
        //    List<PathNode> ndoe = new List<PathNode>();

        //    for (int i = -1; i <= 1; i++)
        //    {
        //        for (int z = -1; z <= 1; z++)
        //        {
        //            if (i == 0 && z == 0)
        //            {
        //                continue;
        //            }

        //            int checkX = node.xPos + i;
        //            int checkY = node.yPos + z;

        //            if (checkX >= 0 && checkX < grid.GetLength(0) && checkY >= 0 && checkY < grid.GetLength(1))
        //            {
        //                ndoe.Add(InstancePath.n[checkX, checkY]);
        //            }


        //        }
        //    }
        //}


    }
}
