using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    Transform bulletOrigin;

    [SerializeField]
    float attackCooldown;

    float cooldown;

    public bool CanAttack
    {
        get 
        {
            return cooldown <= 0;
        }
    }

    void Update()
    {
        if(cooldown > 0) 
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        BulletPool.Retrieve(BulletType.Blue, bulletOrigin.position);
        cooldown = attackCooldown;
    }
}
