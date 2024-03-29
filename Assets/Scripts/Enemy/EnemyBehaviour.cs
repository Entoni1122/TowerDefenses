using UnityEngine;

namespace TowerDefense
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float speed;
        private Transform nodeToGO;
        private int nodeIndex = 0;

        public float switchTargetDistance;

        private void Start()
        {
            nodeToGO = PathManager.nodes[nodeIndex];
        }

        private void Update()
        {
            Vector3 direction = (nodeToGO.transform.position - transform.position).normalized * speed * Time.deltaTime;

            transform.Translate(direction, Space.World);

            if (Vector3.Distance(transform.position, nodeToGO.position) < switchTargetDistance)
            {
                Reachedtarget();
            }
        }

        void Reachedtarget()
        {
            if (nodeIndex >= PathManager.nodes.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            nodeIndex++;
            nodeToGO = PathManager.nodes[nodeIndex];
        }
    }
}
