using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    IEnumerator DestroyIfOutsideScreen()
    {
        while(transform.position.x <= cameraBound.MaxX)
        {
            yield return new WaitForEndOfFrame();
        }

        BulletPool.Return(this);
    }
}
