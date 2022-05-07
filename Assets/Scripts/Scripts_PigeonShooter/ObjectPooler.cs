using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    public class ObjectPooler : MonoBehaviour
    {
        //public static ObjectPooler Instance { get; private set; }
        public static ObjectPooler Instance;

        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        

        private void Awake()
        {
            //if (Instance == null)
            //{
            //    DontDestroyOnLoad(gameObject);
            //    Instance = this;
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}
            Instance = this;
        }

        void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for(int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (! poolDictionary.ContainsKey(tag))
            {
                Debug.LogError("Pool with tag " + tag + " doesn't exist");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            poolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }


    }
}

