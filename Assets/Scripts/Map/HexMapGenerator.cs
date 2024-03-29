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

        [SerializeField] GameObject DeepWater;
        [SerializeField] GameObject WaterNode;
        [SerializeField] GameObject GroundNode;
        [SerializeField] GameObject TreeNode;
        [SerializeField] GameObject SnowNode;


        [SerializeField] int randomSeed = 1;
        //Noise diopo
        [SerializeField] float noiseFrequency = 10f;
        [SerializeField] float deepWaterPropability;
        [SerializeField] float waterPropability;
        [SerializeField] float treePropability;
        [SerializeField] float groundProb;
        [SerializeField] float SnowPropability;


        private void Start()
        {
            CreateMap();
        }

        void CreateMap()
        {
            if (randomSeed == 1)
            {
                randomSeed = Random.Range(1,10000);
            }

            for (int i = 0; i < Column; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Vector2 hexcod = GetHexPosition(i, j);
                    
                    float noiseValue = Mathf.PerlinNoise((hexcod.x + randomSeed) / noiseFrequency, (hexcod.y + randomSeed) / noiseFrequency);

                    float UpScale = 0;
                    GameObject tileToPlace = null;
                    if (noiseValue < deepWaterPropability)
                    {
                        tileToPlace = DeepWater;
                        UpScale = noiseValue * 8;
                    }
                    else if (noiseValue < waterPropability)
                    {
                        tileToPlace = WaterNode;
                        UpScale = noiseValue * 10;
                    }
                    else if (noiseValue < treePropability)
                    {
                        tileToPlace = TreeNode;
                        UpScale = noiseValue * 12;
                    }
                    else if (noiseValue < groundProb)
                    {
                        UpScale = noiseValue * 15;
                        tileToPlace = GroundNode;
                    }
                    else 
                    {
                        UpScale = noiseValue * 18;
                        tileToPlace = SnowNode;
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
