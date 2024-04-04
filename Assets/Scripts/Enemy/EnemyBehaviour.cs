using UnityEngine;

namespace TowerDefense
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float speed;
        private Transform nodeToGO;
        public int nodeIndex = 0;

        public float switchTargetDistance;

        private void Start()
        {
            nodeToGO = PathManager.HexagonEnemyDIOPO[nodeIndex].transform;
        }

        private void Update()
        {
            Vector3 direction = ((nodeToGO.transform.position + Vector3.up * 5f) - transform.position).normalized * speed * Time.deltaTime;

            transform.Translate(direction, Space.World);

            if (Vector3.Distance(transform.position, nodeToGO.position + Vector3.up * 5f) < switchTargetDistance)
            {
                Reachedtarget();
            }
        }

        void Reachedtarget()
        {
            if (nodeIndex >= PathManager.HexagonEnemyDIOPO.Count - 1)
            {
                PoolingMethod.ReturnObjectToPool(gameObject);
                return;
            }
            nodeIndex++;
            nodeToGO = PathManager.HexagonEnemyDIOPO[nodeIndex].transform;
        }
    }
}
