using UnityEngine;
namespace TowerDefense
{
    public class Bullet : MonoBehaviour
    {
        public Transform target;
        public GameObject ParticleImpact;
        public float speed;

        public float bulletDMG;

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 directionToEnemy = target.position - transform.position;
            float distance = speed * Time.deltaTime;

            if (directionToEnemy.magnitude <= distance)
            {
                HitTarget();
                return;
            }
            transform.Translate(directionToEnemy.normalized * distance, Space.World);

        }
        void HitTarget()
        {
            GameObject part = Instantiate(ParticleImpact, transform.position, Quaternion.identity);
            EnemyStats enmey = target.transform.gameObject.GetComponent<EnemyStats>();
            enmey.OnTakeDMG(bulletDMG);

            Destroy(part, 2f);

            Destroy(gameObject);
        }
        public void ReachTarget(Transform _target)
        {
            target = _target;
        }
    }
}
