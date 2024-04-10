using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace TowerDefense
{
    public class Buildable : MonoBehaviour
    {
        Vector3 PositionToLand;
        public bool isTurret;

        [Header("UI Stuff")]
        public float UpgradeCost;
        public float SellCost;
        public float costToBuild;
        public int Level;

        bool isLanded;
        // Update is called once per frame
        public virtual void Start()
        {
            AnimationOnSpawn();
        }

        public virtual void Update()
        {
            float dist = Vector3.Distance(transform.position,PositionToLand);

            if (!isLanded && dist > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, PositionToLand, 20f * Time.deltaTime);
            }
            else
            {
                isLanded = true;
            }
        }

        void AnimationOnSpawn()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * 200f, out hit))
            {
                PositionToLand = hit.point;
            }
        }

        public virtual void UpgradeBuilding()
        {
            Level += 1;

            print("Called Upgrade Building");
        }
    }
}
