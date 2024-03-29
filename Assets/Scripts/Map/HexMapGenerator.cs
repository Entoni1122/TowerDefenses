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

        [SerializeField] GameObject Node;


        [SerializeField] int randomSeed = 1;
        //Noise diopo
        [SerializeField] float noiseFrequency = 10f;
        [SerializeField] float deepWaterPropability;
        [SerializeField] float waterPropability;
        [SerializeField] float treePropability;
        [SerializeField] float groundProb;
        [SerializeField] float SnowPropability;


        public Gradient test;
        public AnimationCurve animationCurve;
        public float yNoise;


        private void Start()
        {
            CreateMap();
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
                    }
                    GameObject node = Instantiate(tileToPlace, transform.position, Quaternion.identity, transform);
                    node.GetComponent<Nodes>().Init(new Vector3(hexcod.x, 0, hexcod.y), UpScale, test.Evaluate(noiseValue));
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
