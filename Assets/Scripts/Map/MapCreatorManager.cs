using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.Mathematics;
using UnityEngine;

namespace TowerDefense
{
    public class MapCreatorManager : MonoBehaviour
    {
        public int Row;
        public int Column;
        public float xOffset, yOffset;

        [SerializeField] GameObject Node;

        private void Start()
        {
            CreateMap();
        }

        void CreateMap()
        {
            for (int i = 0; i < Column * Row; i++)
            {
                Vector3 posToGo = new Vector3(xOffset * (i / Column), 0, yOffset * (i % Row));
                GameObject node = Instantiate(Node, transform.position, Quaternion.identity, transform);
                node.GetComponent<Nodes>().Init(posToGo);

            }
        }
    }
}
