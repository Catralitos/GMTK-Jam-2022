using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Bullets.Spawners
{
    public class BulletPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        #region SingleTon
        public static BulletPooler Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion
        
        public List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;
        
        private void Start()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for(int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool (string poolTag, Vector2 spawnPos, Quaternion rotation, float angle = -1.0f, float maxAngleStep = -1.0f)
        {
            if (!_poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogWarning("Pool with tag " + poolTag + " doesn't exist");
                return null;
            }
            GameObject objToSpawn = _poolDictionary[poolTag].Dequeue();

            objToSpawn.SetActive(true);
            objToSpawn.transform.position = spawnPos;
            objToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objToSpawn.GetComponent<IPooledObject>();
            if(angle == -1.0f && maxAngleStep == -1.0f)
                pooledObj.OnObjectSpawn();
            else if(maxAngleStep == -1.0f)
                pooledObj.OnObjectSpawn(angle);
            else
                pooledObj.OnObjectSpawn(angle, maxAngleStep);

            _poolDictionary[poolTag].Enqueue(objToSpawn);

            return objToSpawn;
        }
    }
}
