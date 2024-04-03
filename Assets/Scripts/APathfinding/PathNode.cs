using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class PathNode
    {
        public int xPos;
        public int yPos;

        public int Gcost;
        public int Hcost;
        public int Fcost => Gcost + Hcost;

        public PathNode cameFromNode;

        public bool isWalkable = true;
    }
}
