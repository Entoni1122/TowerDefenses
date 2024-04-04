using UnityEngine;

namespace TowerDefense
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float speed;
        public Vector3 nodeToGO;
        public int nodeIndex = 0;

        public float switchTargetDistance;
        private void Start()
        {
            nodeToGO = PathManager.enemyTarget.position;
        }
        private void Update()
        {
            print(nodeToGO);
            Vector3 direction = ((nodeToGO + Vector3.up * 6f) - transform.position).normalized * speed * Time.deltaTime;

            transform.Translate(direction, Space.World);

            if (Vector3.Distance(transform.position, nodeToGO + Vector3.up * 6f) < switchTargetDistance)
            {
                Reachedtarget();
            }
        }
        void Reachedtarget()
        {
            PoolingMethod.ReturnObjectToPool(gameObject);
        }
    }
}
