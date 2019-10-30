using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Speed;
    
    [SerializeField]
    float verticalBuffer;

    Vector3 moveDirection = new Vector3(0, 1, 0);
    private CameraBound cameraBound;
    private PlayerAttack gun;

    void Start()
    {        
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
        gun = GetComponentInChildren<PlayerAttack>();
    }

    void Update()
    {
        if(GameManager.IsGameOver)
        {
            return;
        }

        var vertical = Input.GetAxis("Vertical");

        if(vertical > 0 && transform.position.y+verticalBuffer > cameraBound.MaxY)
        {
            vertical = 0;
        }
        else if(vertical < 0 && transform.position.y-verticalBuffer < cameraBound.MinY)
        {
            vertical = 0;
        }

        if(vertical != 0)
        {
            transform.position += moveDirection * vertical * Speed * Time.deltaTime;
        }

        if(gun.CanAttack && Input.GetKey(KeyCode.Space))
        {
            gun.Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if(bullet && bullet.GetBulletType() != BulletType.Blue)
        {
            BulletPool.Return(bullet);
            GameManager.EndGame();
            return;
        }

        var enemy = other.gameObject.GetComponent<EnemyController>();
        if(enemy)
        {
            GameManager.EndGame();
            return;
        }
    }
}
