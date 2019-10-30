using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float Speed;

    static Vector3 moveDirection = new Vector3(-1,0,0);

    private CameraBound cameraBound;

    private void Start() 
    {
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
    }

    void Update()
    {
        transform.position += moveDirection * Speed * Time.deltaTime;

        if(transform.position.x < cameraBound.MinX)
        {
            EnemyPool.Return(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if(bullet)
        {
            BulletPool.Return(bullet);
            EnemyPool.Return(this.gameObject);
        }
    }
}
