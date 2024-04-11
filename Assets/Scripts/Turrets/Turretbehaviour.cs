using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense
{
    public class Turretbehaviour : Buildable
    {
        [SerializeField] List<GameObject> Enemies;
        [SerializeField] GameObject RotatingBase;
        [SerializeField] Transform Target;

        [Header("Utilities")]
        public float CheckForEnemiesRadius;
        public float TurnSpeed;
        public LayerMask Enemy;

        [Header("FireGunVariable")]
        [SerializeField] Transform Muzzle;
        [SerializeField] Transform SecondMuzzle;
        [SerializeField] GameObject Bullet;
        private float FireTimer = 0;
        public float FireRate;
        public int DMG;

        [Header("Upgrade Stats")]
        public float UpgradeFireRate;
        public int UpgradeDMG;
        public float UpgradeCheckForEnemiesRadius;
        public int MaxLevel;


        public override void Start()
        {
            base.Start();
            isTurret = true;
        }
        public override void Update()
        {
            base.Update();
            CheckForNearestEnemy();
            RotateTowardsNearestEnemy();
            CheckToShoot();
        }

        void CheckForNearestEnemy()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, CheckForEnemiesRadius, Enemy);
            float nearestEnemy = Mathf.Infinity;
            Collider target = null;
            foreach (var enemy in enemies)
            {
                Debug.Log("Enemeis found " + enemies.Length);
                if (!Enemies.Contains(enemy.gameObject))
                {
                    Enemies.Add(enemy.gameObject);
                }
                Vector3 directionToEnemy = enemy.transform.position - transform.position;

                if (directionToEnemy.magnitude < nearestEnemy)
                {
                    nearestEnemy = directionToEnemy.magnitude;
                    target = enemy;
                }
            }
            if (target != null && nearestEnemy <= CheckForEnemiesRadius)
            {
                Target = target.transform;
            }
            else
            {
                Target = null;
            }
        }
        void RotateTowardsNearestEnemy()
        {
            if (Target != null)
            {
                Vector3 Dire = Target.transform.position - transform.position;
                Quaternion lookRtation = Quaternion.LookRotation(Dire);
                Vector3 rotation = Quaternion.Lerp(RotatingBase.transform.rotation, lookRtation, Time.deltaTime * TurnSpeed).eulerAngles;

                RotatingBase.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
            }
        }
        void CheckToShoot()
        {
            if (Target != null)
            {
                Vector3 forward = RotatingBase.transform.TransformDirection(Vector3.forward);
                Vector3 toOther = Target.position - RotatingBase.transform.position;

                if (Vector3.Dot(forward, toOther) > 0.9)
                {
                    FireTimer -= Time.deltaTime;
                    if (FireTimer <= 0)
                    {
                        Shoot();

                        FireTimer = FireRate;
                    }
                }
            }
        }

        bool bShouldSwitchMuzzle;
        void Shoot()
        {
            Vector3 position = Muzzle.position;
            if (bShouldSwitchMuzzle)
            {
                position = SecondMuzzle.position;
            }
            GameObject bulletSpawned = PoolingMethod.SpawnObject(Bullet, position, Quaternion.identity, PoolingMethod.PoolType.Bullets);
            bulletSpawned.GetComponent<BulletBase>().ReachTarget(Target);
            bulletSpawned.GetComponent<BulletBase>().bulletDMG = DMG;
            bShouldSwitchMuzzle = !bShouldSwitchMuzzle;
        }
        public override void UpgradeBuilding()
        {
            if (Level < MaxLevel)
            {
                if (PlayerStats.Instance.Coin >= UpgradeCost)
                {
                    base.UpgradeBuilding();
                    PlayerStats.Instance.Coin -= UpgradeCost;
                    DMG += UpgradeDMG;
                    CheckForEnemiesRadius += UpgradeCheckForEnemiesRadius;
                    FireRate -= UpgradeFireRate;
                    UpgradeCost += 50;
                    SellCost += UpgradeCost * 0.5f;
                    PlayerStats.Instance.UdpateStats();
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, CheckForEnemiesRadius);
        }
    }
}

