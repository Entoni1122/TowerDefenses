using UnityEngine;

namespace TowerDefense
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float speed;
        public Vector3 nodeToGO;
        public float disappearDistance;
        private void Start()
        {
            nodeToGO = transform.position + Vector3.down * 24;
        }
        private void Update()
        {
            Vector3 direction = (nodeToGO - transform.position).normalized * speed * Time.deltaTime;

            transform.Translate(direction, Space.World);

            if (Vector3.Distance(transform.position, nodeToGO) < disappearDistance)
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
