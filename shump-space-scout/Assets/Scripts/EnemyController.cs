using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float SpeedRange;

    [SerializeField]
    Transform bulletOrigin;

    [SerializeField]
    float attackCooldown;

    float cooldown;
    float actualSpeed;

    static Vector3 moveDirection = new Vector3(-1,0,0);

    private CameraBound cameraBound;

    private void Start() 
    {
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
        cooldown = attackCooldown;
    }

    public void Initialize()
    {
        var range = SpeedRange*0.25f;
        actualSpeed = Random.Range(SpeedRange-range, SpeedRange+range);
    }

    void Update()
    {
        if(!isActiveAndEnabled)
        {
            return;
        }

        transform.position += moveDirection * actualSpeed * Time.deltaTime;

        if(transform.position.x < cameraBound.MinX)
        {
            EnemyPool.Return(this);
        }

        if(actualSpeed <= SpeedRange)
        {
            if(cooldown <= 0)
            {
                Attack();
                cooldown = attackCooldown;
            }
            else if(cooldown > 0) 
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    void Attack()
    {
        BulletPool.Retrieve(BulletType.Yellow, bulletOrigin.position);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if(bullet)
        {
            if(bullet.GetBulletType() != BulletType.Yellow)
            {
                BulletPool.Return(bullet);
                EnemyPool.Return(this);
                ScoreKeeper.AddScore(1);
            } 
            else if(bullet.GetBulletType() == BulletType.Yellow)
            {
                actualSpeed *= 1.5f;
                BulletPool.Return(bullet);
            }
        }    
    }
}
