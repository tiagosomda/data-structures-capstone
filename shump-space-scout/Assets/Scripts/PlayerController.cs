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

    void Start()
    {        
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
    }

    void Update()
    {
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
    }
}
