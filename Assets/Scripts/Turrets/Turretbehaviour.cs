using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense
{
    public class Turretbehaviour : MonoBehaviour
    {
        [SerializeField] List<GameObject> Enemies;
        [SerializeField] GameObject RotatingBase;
        [SerializeField] Transform Target;

        [Header("Utilities")]
        public float CheckForEnemiesRadius;
        public float TurnSpeed;
        public LayerMask Enemy;
        Vector3 PositionToLand;

        [Header("FireGunVariable")]
        [SerializeField] Transform Muzzle;
        [SerializeField] GameObject Bullet;
        public float FireRate;
        private float FireTimer = 0;
        public float costToBuild;
        public int DMG;

        [Header("UI Stuff")]
        public float UpgradeCost;
        public float SellCost;

        private void Start()
        {
            AnimationOnSpawn();
        }
        void Update()
        {
            transform.position = Vector3.Lerp(transform.position,PositionToLand,10f * Time.deltaTime);
            CheckForNearestEnemy();
            RotateTowardsNearestEnemy();
            CheckToShoot();
        }

        void AnimationOnSpawn()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,Vector3.down * 200f,out hit))
            {
                PositionToLand = hit.point;
            }
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

                RotatingBase.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
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
        void Shoot()
        {
            GameObject bulletSpawned = Instantiate(Bullet, Muzzle.position, Quaternion.identity);
            Bullet bullet = bulletSpawned.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.ReachTarget(Target);
                bullet.bulletDMG = DMG;
            }
        }

        public void UpgradeTurret()
        {
            if (PlayerStats.Instance.Coin >= UpgradeCost)
            {
                PlayerStats.Instance.Coin -= UpgradeCost;
                PlayerStats.Instance.UdpateStats();
                UpgradeCost += 50;
                SellCost += UpgradeCost * 0.5f;
                DMG += 1;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, CheckForEnemiesRadius);
        }
    }
}

