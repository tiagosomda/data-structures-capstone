using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    BulletType bulletType;
    
    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float Speed;

    private CameraBound cameraBound;
    private Coroutine offscreenCheckRoutine;
    
    void Update()
    {   
        transform.position += direction * Speed * Time.deltaTime;
    }

    public void Initiliaze()
    {
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
        offscreenCheckRoutine = StartCoroutine(DestroyIfOutsideScreen());
    }

    public void Deactivate()
    {
        if(offscreenCheckRoutine != null)
        {
            StopCoroutine(offscreenCheckRoutine);
            offscreenCheckRoutine = null;
        }
    }

    public BulletType GetBulletType()
    {
        return bulletType;
    }

    IEnumerator DestroyIfOutsideScreen()
    {
        while(transform.position.x <= cameraBound.MaxX && transform.position.x >= cameraBound.MinX)
        {
            yield return new WaitForEndOfFrame();
        }

        BulletPool.Return(this);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if(bullet)
        {
            BulletPool.Return(this);
        }
    }
}
