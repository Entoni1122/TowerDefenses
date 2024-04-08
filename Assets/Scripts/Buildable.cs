using System.Collections;
using System.Collections.Generic;
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

        // Update is called once per frame
        public virtual void Start()
        {
            AnimationOnSpawn();
        }

        public virtual void Update()
        {
            transform.position = Vector3.Lerp(transform.position,PositionToLand,10f * Time.deltaTime);
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
