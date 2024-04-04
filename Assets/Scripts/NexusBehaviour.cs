using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class NexusBehaviour : MonoBehaviour
    {
        Vector3 PositionToLand;
        private void Update()
        {
            AnimationOnSpawn();
            transform.position = Vector3.Lerp(transform.position, PositionToLand, 10f * Time.deltaTime);
        }

        void AnimationOnSpawn()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * 200f, out hit))
            {
                PositionToLand = hit.point;
            }
        }
    }
}
