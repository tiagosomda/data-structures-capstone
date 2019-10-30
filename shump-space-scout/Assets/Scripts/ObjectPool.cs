using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> pool;
    protected GameObject poolPrefab;

    public ObjectPool(int initialCapacity, GameObject prefab)
    {
        poolPrefab = prefab;

        pool = new List<GameObject>(initialCapacity);
        for(int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(InstantiatePooledObject());
        }
    }

    public GameObject RetrieveFromPool()
    {
        var obj = RetrieveOrCreate();
        ActivatePooledObject(obj);
        return obj;
    }

    private GameObject RetrieveOrCreate()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return obj;
        }
        else
        {
            // pool empty, so expand pool and return new object
            pool.Capacity++;
            return InstantiatePooledObject();
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        DeactivatePooledObject(obj);
        pool.Add(obj);
    }

    protected virtual void ActivatePooledObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    protected virtual void DeactivatePooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected virtual GameObject InstantiatePooledObject()
    {
        var test = GameObject.Instantiate(poolPrefab, Vector3.zero, Quaternion.identity);
        test.SetActive(false);
        return test;
    }
}
