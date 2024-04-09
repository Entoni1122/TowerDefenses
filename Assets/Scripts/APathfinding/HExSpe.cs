using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class HExSpe : MonoBehaviour
    {
        public int numDivisions = 10;
        public float radius = 1f;
        public GameObject hexagonPrefab;

        void Start()
        {
            CreateHexSphere();
        }

        void CreateHexSphere()
        {
            for (int i = 0; i < numDivisions; i++)
            {
                float lat = Mathf.PI * (i + 0.5f) / numDivisions;
                float y = Mathf.Cos(lat) * radius;
                float ringRadius = Mathf.Sin(lat) * radius;

                int numInRing = Mathf.CeilToInt(Mathf.PI * 2 * ringRadius / radius);

                for (int j = 0; j < numInRing; j++)
                {
                    float lon = (Mathf.PI * 2 * j) / numInRing;
                    Vector3 position = new Vector3(Mathf.Cos(lon) * ringRadius, Mathf.Sin(lat) * radius, Mathf.Sin(lon) * ringRadius);
                    GameObject hexagon = Instantiate(hexagonPrefab, position, Quaternion.identity, transform);
                    hexagon.transform.LookAt(transform.position);
                }
            }
        }
    }
}
