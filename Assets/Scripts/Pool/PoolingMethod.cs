using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;


namespace TowerDefense
{
    public class PoolingMethod : MonoBehaviour
    {
        public static List<PooledObjectInfo> ObjectPool = new List<PooledObjectInfo>();
        private GameObject _objectPoolEmptyHolder;
        private static GameObject _particleSystemEmpty;
        private static GameObject _bulletsEmpty;
        private static GameObject _enemyEmpty;

        public enum PoolType
        {
            ParticleSystem,
            Bullets,
            Enemy,
            None
        }

        public static PoolType PoolingType;

        private void Awake()
        {
            
            SetupEmpties();
        }

        private void SetupEmpties()
        {
            print("Pooling objct");
            _objectPoolEmptyHolder = new GameObject("Pool Object");

            _particleSystemEmpty = new GameObject("Particle");
            _particleSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

            _enemyEmpty = new GameObject("Enemies");
            _enemyEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

            _bulletsEmpty = new GameObject("Bullets");
            _bulletsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
        }

        public static GameObject SpawnObject(GameObject objToSpawn, Vector3 SpawnPosition, Quaternion SpawnRotation,PoolType poolType = PoolType.None)
        {
            PooledObjectInfo pool = ObjectPool.Find(p => p.LookUpString == objToSpawn.name);

            if (pool == null)
            {
                pool = new PooledObjectInfo() { LookUpString = objToSpawn.name };
                ObjectPool.Add(pool);
            }

            GameObject spawnableOBJ = pool.InactiveObject.FirstOrDefault();

            if (spawnableOBJ == null)
            {
                GameObject parent = SetParentObject(poolType);

                spawnableOBJ = Instantiate(objToSpawn, SpawnPosition, SpawnRotation);

                if (parent != null)
                {
                    spawnableOBJ.transform.SetParent(parent.transform);
                }

            }
            else
            {
                spawnableOBJ.transform.position = SpawnPosition;
                spawnableOBJ.transform.rotation = SpawnRotation;
                pool.InactiveObject.Remove(spawnableOBJ);
                spawnableOBJ.SetActive(true);
            }
            return spawnableOBJ;
        }

        public static void ReturnObjectToPool(GameObject bjToReturn)
        {
            string goname = bjToReturn.name.Substring(0, bjToReturn.name .Length - 7);

            PooledObjectInfo pool = ObjectPool.Find(p => p.LookUpString == goname);

            if (pool == null)
            {
                Debug.Log("<color=red>Error: </color> Trying to destroy an obj not pooled");
            }
            else 
            {
                bjToReturn.SetActive(false);
                pool.InactiveObject.Add(bjToReturn);
            }
        }

        private static GameObject SetParentObject(PoolType poolType)
        {
            switch (poolType)
            {
                case PoolType.ParticleSystem:
                    return _particleSystemEmpty;
                case PoolType.Bullets:
                    return _bulletsEmpty;
                case PoolType.Enemy:
                    return _enemyEmpty;
                case PoolType.None:
                    return null;
                default:
                    return null;
            }

        }


    }

    public class PooledObjectInfo
    {
        public string LookUpString;
        public List<GameObject> InactiveObject = new List<GameObject>();
    }
}
