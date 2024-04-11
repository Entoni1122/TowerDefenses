using UnityEngine;

namespace TowerDefense
{
    public class EnemyStats : MonoBehaviour
    {
        public float HP;
        public float MaxHP;

        public int DMG;
        public float CoinOnDead;

        public GameObject ParticleImpact;

        public void OnTakeDMG(float b)
        {
            HP -= b;
            if (HP <= 0)
            {
                Dead();
            }
        }
        void Dead()
        {
            PoolingMethod.SpawnObject(ParticleImpact, transform.position, Quaternion.identity, PoolingMethod.PoolType.ParticleSystem);
            HP = MaxHP;
            PlayerStats.Instance.AddMoney(CoinOnDead);
            PoolingMethod.ReturnObjectToPool(gameObject);
        }
    }
}
