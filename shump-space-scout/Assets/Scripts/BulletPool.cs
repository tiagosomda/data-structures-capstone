using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Blue, Yellow }
public class BulletPool
{
    private static BulletPool singleton;
    private static BulletPool Instance
    {
        get 
        {
            if(singleton == null)
            {
                singleton = new BulletPool();
                singleton.InitializeBullets();
            }

            return singleton;
        }
    }

    private Dictionary<BulletType, ObjectPool> pools;

    private void InitializeBullets()
    {
        pools = new Dictionary<BulletType, ObjectPool>();

        var blueBulletprefab = Resources.Load<GameObject>("Bullets/BlueBullet");
        pools.Add(BulletType.Blue, new ObjectPool(10, blueBulletprefab));

        var yellowBulletprefab = Resources.Load<GameObject>("Bullets/YellowBullet");
        pools.Add(BulletType.Yellow, new ObjectPool(10, yellowBulletprefab));
    }

    public static Bullet Retrieve(BulletType type, Vector3 position)
    {
        var obj = Instance.pools[type].RetrieveFromPool();
        obj.transform.position = position;
        var bullet = obj.GetComponent<Bullet>();
        bullet.Initiliaze();
        return bullet;
    }
    public static void Return(Bullet bullet)
    {
        bullet.Deactivate();
        Instance.pools[bullet.GetBulletType()].ReturnToPool(bullet.gameObject);
    }
}
