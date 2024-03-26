using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;

        [Serializable]
        public class ObjectPool
        {
            public string poolName;
            public GameObject prefab;
            public int poolSize = 10;
        }

        public List<ObjectPool> objectPools;

        private Dictionary<string, List<GameObject>> poolDictionary;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            InitializeObjectPools();
        }
        
        void InitializeObjectPools()
        {
            poolDictionary = new Dictionary<string, List<GameObject>>();

            foreach (ObjectPool pool in objectPools)
            {
                List<GameObject> objectList = new List<GameObject>();

                for (int i = 0; i < pool.poolSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);
                    obj.SetActive(false);
                    objectList.Add(obj);
                }

                poolDictionary.Add(pool.poolName, objectList);
            }
        }

        public GameObject GetPooledObject(string poolName)
        {
            if (poolDictionary.TryGetValue(poolName, out List<GameObject> objectList))
            {
                for (int i = 0; i < objectList.Count; i++)
                {
                    if (!objectList[i].activeInHierarchy)
                    {
                        return objectList[i];
                    }
                }

                // If no inactive object is found, expand the pool by creating a new object
                GameObject newObj = Instantiate(objectPools.Find(pool => pool.poolName == poolName).prefab, transform);
                newObj.SetActive(false);
                objectList.Add(newObj);

                return newObj;
            }

            return null;
        }
    }
}
