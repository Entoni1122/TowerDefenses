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

                    float UpScale = 0;
                    GameObject tileToPlace = null;

                    if (noiseValue < waterPropability)
                    {
                        tileToPlace = WaterNode;
                    }
                    else if (noiseValue > treePropability)
                    {
                        tileToPlace = GroundNode;
                        UpScale = noiseValue * 4;
                    }
                    else
                    {
                        UpScale = noiseValue * 4;
                        tileToPlace = TreeNode;
                    }
                    GameObject node = Instantiate(tileToPlace, transform.position, Quaternion.identity, transform);
                    node.GetComponent<Nodes>().Init(new Vector3(hexcod.x, 0, hexcod.y),UpScale);
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
