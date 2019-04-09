using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singelton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    [Serializable]
    public struct Pool
    {
        public GameObject prefab;
        [SerializeField]public int size;
        public bool spawnMoreWhenEmpty;
        public bool reUseObjects;
    }
    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.prefab.name, objectPool);
        }
    }

    //will Instantiate a game object from the pool
    public GameObject InstantiateFromPool(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        Pool pool = pools.Find((x) => { return (x.prefab == gameObject); });
        Queue<GameObject> queue = poolDictionary[gameObject.name];
        GameObject objectToSpawn = null;
        if (queue.Count != 0)
        {
            objectToSpawn = poolDictionary[gameObject.name].Dequeue();
        }
        else
        {
            if (pool.spawnMoreWhenEmpty)
            {
                objectToSpawn = InstantiateNewForPool(gameObject);
            }
            else
            {
                return null;
            }
        }
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(null);
        objectToSpawn.GetComponent<IpooledObject>()?.OnObjectSpawn();
       
        if (pool.reUseObjects == true) poolDictionary[gameObject.name].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public GameObject InstantiateFromPool(GameObject gameObject, Transform transform)
    {
        return InstantiateFromPool(gameObject, transform.position, transform.rotation);
    }
    private GameObject InstantiateNewForPool(GameObject gameObject)
    {
        GameObject obj = Instantiate(gameObject);
        obj.SetActive(false);
        obj.transform.parent = this.transform;
        poolDictionary[gameObject.name].Enqueue(obj);
        return obj;
    }
}
