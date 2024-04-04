using UnityEngine;

namespace TowerDefense
{
    public class EnemyStats : MonoBehaviour
    {
        public float HP;
        public float MaxHP;

        public int DMG;
        public float CoinOnDead;

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
            GetComponent<EnemyBehaviour>().nodeIndex = 0;
            HP = MaxHP;
            PlayerStats.Instance.AddMoney(CoinOnDead);
            PoolingMethod.ReturnObjectToPool(gameObject);
        }
    }
}
