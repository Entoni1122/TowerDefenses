using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.Playables;

namespace TowerDefense
{
    public class PathfindingTesting : MonoBehaviour
    {
        const int MOVE_COST = 10;

        public int width = 3;
        public int heihgt = 3;

        List<PathNode> nodeList = new List<PathNode>();


        void Start()
        {
            for (int y = 0; y < heihgt; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    PathNode node = new PathNode();
                    node.xPos = x;
                    node.yPos = y;
                    node.Gcost = 0;
                    node.Hcost = 0;
                    int parentIndex = y * width + x;
                    if (parentIndex == 0)
                    {
                        node.cameFromNode = null;
                        nodeList.Add(node);
                        continue;
                    }
                    if (parentIndex == 3||parentIndex == 4)
                    {
                        node.isWalkable = false;
                        node.cameFromNode = nodeList[parentIndex - 1];
                        nodeList.Add(node);
                        continue;
                    }
                    node.cameFromNode = nodeList[parentIndex - 1];
                    nodeList.Add(node);
                }
            }

            print(nodeList.Count);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Testing(nodeList[0], nodeList[nodeList.Count - 1]);
            }
        }
        int CalculateHeuristicCost(PathNode start, PathNode target)
        {
            int x = Mathf.Abs(start.xPos - target.xPos);
            int y = Mathf.Abs(start.yPos - target.yPos);

            return x + y;
        }
        void Testing(PathNode start, PathNode end)
        {
            FindPath(start, end);
        }


        //---------------------------PathFinding---------------------------------------------

         
        List<PathNode> toSearch = new List<PathNode>();
        List<PathNode> processed = new List<PathNode>();

        List<PathNode> FindPath(PathNode start, PathNode end)
        {
            toSearch.Add(start);
            List<PathNode> truePath = new List<PathNode>();
            while (toSearch.Count > 0)
            {
                PathNode current = toSearch[0];
                for (int i = 0; i < toSearch.Count; i++)
                {
                    if (toSearch[i].Fcost < current.Fcost || toSearch[i].Fcost == current.Fcost && toSearch[i].Hcost < current.Hcost)
                    {
                        current = toSearch[i];
                    }
                }

                toSearch.Remove(current);
                processed.Add(current);

                if (current == end)
                {
                    return GetTruePath(start, end);
                }

                foreach (PathNode neighbor in GetNeighbors(current))
                {
                    if (!neighbor.isWalkable || processed.Contains(neighbor))
                    {
                        continue;
                    }
                    int newCostToNeighbor = current.Gcost + GetDistance(current, neighbor);
                    if (newCostToNeighbor < neighbor.Gcost || !toSearch.Contains(neighbor))
                    {
                        neighbor.Gcost = newCostToNeighbor;
                        neighbor.Hcost = GetDistance(neighbor, end);
                        neighbor.cameFromNode = current;

                        if (!toSearch.Contains(neighbor))
                        {
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }
        List<PathNode> GetTruePath(PathNode start, PathNode end)
        {
            List<PathNode> node = new List<PathNode>();

            PathNode current = end;

            while (current != start)
            {
                node.Add(current);
                current = current.cameFromNode;
                
            }

            node.Reverse();

            for (int i = 0; i < node.Count; i++)
            {
                print(node[i].xPos + " " + node[i].yPos);
            }
            return node;
        }

        List<PathNode> GetNeighbors(PathNode node)
        {
            List<PathNode> ndoe = new List<PathNode>();

            for (int i = -1; i <= 1; i++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (i == 0 && z == 0)
                    {
                        continue;
                    }

                    int checkX = node.xPos + i;
                    int checkY = node.yPos + z;

                    if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < heihgt)
                    {
                        ndoe.Add(nodeList[checkY * width + checkX]);
                    }


                }
            }
            return ndoe;
        }


        private int GetDistance(PathNode nodeA, PathNode nodeB)
        {
            int dstX = Mathf.Abs(nodeA.xPos - nodeB.xPos);
            int dstY = Mathf.Abs(nodeA.yPos - nodeB.yPos);

            return dstX + dstY;
        }

    }
}
