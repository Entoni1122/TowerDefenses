using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyHexagon : MonoBehaviour
    {
        Vector3 PositionToLand;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, PositionToLand, 10f * Time.deltaTime);
            AnimationOnSpawn();
        }

        void AnimationOnSpawn()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * 200f, out hit, 200f))
            {
                PositionToLand = hit.point;
            }
        }
    }
}
