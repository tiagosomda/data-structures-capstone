using UnityEngine;

public class EnemyPool
{
    private static EnemyPool singleton;
    private static EnemyPool Instance
    {
        get 
        {
            if(singleton == null)
            {
                singleton = new EnemyPool();
                singleton.Initialize();
            }

            return singleton;
        }
    }

    private ObjectPool pool;

    public void Initialize()
    {
        var prefab = Resources.Load<GameObject>("Enemy");
        pool = new ObjectPool(10, prefab);
    }

    public static EnemyController Retrieve()
    {
        var obj = Instance.pool.RetrieveFromPool();
        return obj.GetComponent<EnemyController>();
    }
    public static void Return(EnemyController obj)
    {
        Instance.pool.ReturnToPool(obj.gameObject);
    }
}
