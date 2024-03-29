using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace TowerDefense
{
    public class HexMapGenerator : MonoBehaviour
    {
        public int Row;
        public int Column;
        public float xOffset, yOffset;

        [SerializeField] GameObject WaterNode;
        [SerializeField] GameObject GroundNode;
        [SerializeField] GameObject TreeNode;

        //Noise diopo
        [SerializeField] float noiseFrequency = 10f;
        [SerializeField] float waterPropability;
        [SerializeField] float treePropability;


        private void Start()
        {
            CreateMap();
        }

        void CreateMap()
        {
            for (int i = 0; i < Column; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Vector2 hexcod = GetHexPosition(i, j);

                    float noiseValue = Mathf.PerlinNoise(hexcod.x / noiseFrequency, hexcod.y / noiseFrequency);

                    float gg = (j * i) + Column;

                    if (noiseValue < waterPropability)
                    {
                        GameObject node = Instantiate(WaterNode, transform.position, Quaternion.identity, transform);
                        node.GetComponent<Nodes>().Init(new Vector3(hexcod.x, 0, hexcod.y));
                    }
                    else if (noiseValue > treePropability)
                    {
                        GameObject waterNode = Instantiate(GroundNode, transform.position, Quaternion.identity, transform);
                        waterNode.GetComponent<Nodes>().Init(new Vector3(hexcod.x, noiseValue * 2, hexcod.y));
                    }
                    else
                    {
                        GameObject node = Instantiate(TreeNode, transform.position, Quaternion.identity, transform);
                        node.GetComponent<Nodes>().Init(new Vector3(hexcod.x, noiseValue  *2, hexcod.y));
                    }
                }
            }
        }

        Vector2 GetHexPosition(int x, int z)
        {
            float xPos = x * xOffset * Mathf.Cos(Mathf.Deg2Rad * 30);
            float yPos = z * yOffset + ((x % 2 == 1) ? yOffset * 0.5f : 0);
            return new Vector2(xPos, yPos);
        }
    }
}
