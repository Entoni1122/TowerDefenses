using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BulletBase : MonoBehaviour
    {
        public Transform target;
        public float speed;

        public float bulletDMG;
        public virtual void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 directionToEnemy = target.position - transform.position;
            float distance = speed * Time.deltaTime;

            if (directionToEnemy.magnitude <= distance )
            {
                HitTarget();
                return;
            }
            transform.Translate(directionToEnemy.normalized * distance, Space.World);
        }
        public virtual void HitTarget()
        {
            EnemyStats enmey = target.transform.gameObject.GetComponent<EnemyStats>();
            enmey.OnTakeDMG(bulletDMG);

            PoolingMethod.ReturnObjectToPool(gameObject);
        }
        public void ReachTarget(Transform _target)
        {
            target = _target;
        }
    }
}
