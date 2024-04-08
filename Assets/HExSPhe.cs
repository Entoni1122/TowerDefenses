using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class HExSPhe : MonoBehaviour
    {
        public int gridSize = 10; // Numero di cerchi per latitudine e longitudine
        public float radius = 1f; // Raggio della sfera

        public GameObject He;
        void Start()
        {
            CreateHexasphereGrid();
        }

        void CreateHexasphereGrid()
        {
            for (int lat = 0; lat < gridSize; lat++)
            {
                for (int lon = 0; lon < gridSize * 2; lon++)
                {
                    Vector3 pos = CalculatePosition(lat, lon);
                    CreateCircle(pos);
                }
            }
        }
        Vector3 CalculatePosition(int lat, int lon)
        {
            float latAngle = Mathf.PI * (float)lat / gridSize;
            float lonAngle = 2 * Mathf.PI * (float)lon / (gridSize * 2);

            float x = radius * Mathf.Sin(latAngle) * Mathf.Cos(lonAngle);
            float y = radius * Mathf.Sin(latAngle) * Mathf.Sin(lonAngle);
            float z = radius * Mathf.Cos(latAngle);

            return new Vector3(x, y, z);
        }

        void CreateCircle(Vector3 position)
        {
            GameObject circle = Instantiate(He);
            circle.transform.position = position;
            circle.transform.LookAt(transform.position); // Orienta il cerchio verso il centro della sfera
            circle.transform.SetParent(transform);
        }
    }
}
