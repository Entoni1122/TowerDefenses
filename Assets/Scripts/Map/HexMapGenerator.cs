using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TowerDefense
{
    public class HexMapGenerator : MonoBehaviour
    {
        [Header("MapInfo")]
        public int Row;
        public int Column;
        public float xOffset, yOffset;
        [SerializeField] GameObject Node;

        [Space]

        [Header("NoiseInfo")]
        [SerializeField] int randomSeed = 1;
        [SerializeField] float noiseFrequency = 10f;
        [SerializeField] float deepWaterPropability;
        [SerializeField] float waterPropability;
        [SerializeField] float treePropability;
        [SerializeField] float groundProb;
        [SerializeField] float SnowPropability;
        [SerializeField] GameObject enemyPath;

        [Space]

        [Header("ColorsInfo")]
        public Gradient test;
        public AnimationCurve animationCurve;

        List<PathNode> nodeList = new List<PathNode>();


        [SerializeField] Material _material;

        private void Start()
        {
            CreateMap();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                int RandmoEnd = Random.Range((Row * Column) / 2, nodeList.Count);
                int randomStart = Random.Range(1, 2);
                Testing(nodeList[randomStart], nodeList[RandmoEnd]);
            }
        }
        void Testing(PathNode start, PathNode end)
        {
            List<PathNode> find = FindPath(start, end);
            List<Transform> enemy = new List<Transform>();
            if (find != null)
            {
                if (find.Count <= 0)
                {
                    return;
                }
                for (int i = 0; i < find.Count - 1; i++)
                {
                    transform.GetChild(find[i].yPos * Row + find[i].xPos).GetComponent<MeshRenderer>().material = _material;
                    enemy.Add(transform.GetChild(find[i].yPos * Row + find[i].xPos));
                    transform.GetChild(find[i].yPos * Row + find[i].xPos).gameObject.layer = 1;
                }
                PathManager.nodes = enemy;
            }
            print("Find Path is null");
        }


        void CreateMap()
        {
            if (randomSeed == 1)
            {
                randomSeed = Random.Range(1, 10000);
            }

            for (int i = 0; i < Column; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Vector2 hexcod = GetHexPosition(i, j);

                    float noiseValue = Mathf.PerlinNoise((hexcod.x + randomSeed) / noiseFrequency, (hexcod.y + randomSeed) / noiseFrequency);

                    float UpScale = 0;
                    bool walkbale = true;
                    GameObject tileToPlace = null;
                    if (noiseValue < deepWaterPropability)
                    {
                        tileToPlace = Node;
                        UpScale = animationCurve.Evaluate(noiseValue);
                    }
                    else if (noiseValue <= waterPropability)
                    {
                        tileToPlace = Node;
                        UpScale = animationCurve.Evaluate(noiseValue);
                    }
                    else if (noiseValue <= treePropability)
                    {
                        tileToPlace = Node;
                        UpScale = animationCurve.Evaluate(noiseValue);
                    }
                    else if (noiseValue <= groundProb)
                    {
                        UpScale = animationCurve.Evaluate(noiseValue);
                        tileToPlace = Node;
                    }
                    else
                    {
                        UpScale = animationCurve.Evaluate(noiseValue);
                        tileToPlace = Node;
                        walkbale = false;
                    }
                    GameObject node = Instantiate(tileToPlace, transform.position, Quaternion.identity, transform);
                    node.GetComponent<Nodes>().Init(new Vector3(hexcod.x, 0, hexcod.y), UpScale, test.Evaluate(noiseValue), walkbale);
                    node.GetComponent<Nodes>().pathNode.xPos = j;
                    node.GetComponent<Nodes>().pathNode.yPos = i;
                    node.GetComponent<Nodes>().pathNode.Gcost = int.MaxValue;
                    node.GetComponent<Nodes>().pathNode.Hcost = 0;
                    int parentIndex = i * Row + j;
                    if (parentIndex == 0)
                    {
                        node.GetComponent<Nodes>().pathNode.cameFromNode = null;
                        nodeList.Add(node.GetComponent<Nodes>().pathNode);
                        continue;
                    }
                    node.GetComponent<Nodes>().pathNode.cameFromNode = nodeList[parentIndex - 1];
                    nodeList.Add(node.GetComponent<Nodes>().pathNode);
                }
            }
        }

        Vector2 GetHexPosition(int x, int z)
        {
            float xPos = x * xOffset * Mathf.Cos(Mathf.Deg2Rad * 30);
            float yPos = z * yOffset + ((x % 2 == 1) ? yOffset * 0.5f : 0);
            return new Vector2(xPos, yPos);
        }

        //------------------------------------PathFinding-----------------------------------------

        List<PathNode> toSearch = new List<PathNode>();
        List<PathNode> processed = new List<PathNode>();

        List<PathNode> FindPath(PathNode start, PathNode end)
        {
            toSearch.Add(start);

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
                    print("Find Path");
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

                    int checkX = node.xPos + i + 1;
                    int checkY = node.yPos + z;

                    if (checkX >= 0 && checkX < Row && checkY >= 0 && checkY < Column)
                    {
                        ndoe.Add(nodeList[checkY * Row + checkX]);
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
