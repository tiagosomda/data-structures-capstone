using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletOrigin;

    [SerializeField]
    float attackCooldown;

    float cooldown;

    void Update()
    {
        if(cooldown <= 0 && Input.GetKey(KeyCode.Space))
        {
            Attack();
            cooldown = attackCooldown;
        }
        else if(cooldown > 0) 
        {
            cooldown -= Time.deltaTime;
        }
    }

    void Attack()
    {
        BulletPool.Retrieve(bulletOrigin.position);
    }
}
