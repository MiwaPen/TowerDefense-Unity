using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake(){
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictinary;
    void Start() {
        poolDictinary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj =  Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictinary.Add(pool.tag,objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag,Vector3 position, Quaternion rotation) {

        if (!poolDictinary.ContainsKey(tag)) return null;

        GameObject objectToSpawn = poolDictinary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null) pooledObject.OnObjectSpawn();

        poolDictinary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
