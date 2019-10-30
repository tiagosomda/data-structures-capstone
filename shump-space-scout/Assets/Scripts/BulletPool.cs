using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool
{
    private static BulletPool singleton;
    private static BulletPool Instance
    {
        get 
        {
            if(singleton == null)
            {
                var prefab = Resources.Load<GameObject>("Bullet");
                singleton = new BulletPool();
                singleton.Initialize(10, prefab);
            }

            return singleton;
        }
    }

    public static Bullet Retrieve(Vector3 position)
    {
        var obj = Instance.RetrieveFromPool();
        var bullet = obj.GetComponent<Bullet>();
        bullet.transform.position = position;
        bullet.Initiliaze();
        return bullet;
    }
    public static void Return(Bullet bullet)
    {
        bullet.Deactivate();
        Instance.ReturnToPool(bullet.gameObject);
    }
    protected override GameObject InstantiatePooledObject()
    {
        var origin = new Vector3(10000, 10000, 10000);
        return GameObject.Instantiate(poolPrefab, origin, Quaternion.identity);
    }
}
