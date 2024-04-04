using UnityEngine;
namespace TowerDefense
{
    public class BulletMortaio : BulletBase
    {
        public float CheckEnemy;
        public LayerMask enmeyMask;
        public override void Update()
        {
            base.Update();
        }
        public override void HitTarget()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, CheckEnemy, enmeyMask);
            foreach (var enemy in enemies)
            {
                EnemyStats enemyStatsRef = enemy.transform.gameObject.GetComponent<EnemyStats>();
                enemyStatsRef.OnTakeDMG(bulletDMG);
            }
            PoolingMethod.SpawnObject(ParticleImpact, transform.position, Quaternion.identity,PoolingMethod.PoolType.ParticleSystem);
            
            PoolingMethod.ReturnObjectToPool(gameObject);
        }
    }
}
