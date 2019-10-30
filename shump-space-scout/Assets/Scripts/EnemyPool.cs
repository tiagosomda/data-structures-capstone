using UnityEngine;

public class EnemyPool : ObjectPool
{
    private static EnemyPool singleton;
    private static EnemyPool Instance
    {
        get 
        {
            if(singleton == null)
            {
                var prefab = Resources.Load<GameObject>("Enemy");
                singleton = new EnemyPool();
                singleton.Initialize(10, prefab);
            }

            return singleton;
        }
    }

    public static GameObject Retrieve()
    {
        return Instance.RetrieveFromPool();
    }
    public static void Return(GameObject bullet)
    {
        Instance.ReturnToPool(bullet);
    }
    protected override GameObject InstantiatePooledObject()
    {
        var origin = new Vector3(10000, 10000, 10000);
        return GameObject.Instantiate(poolPrefab, origin, Quaternion.identity);
    }
}
