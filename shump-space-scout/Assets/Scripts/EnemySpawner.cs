using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float spawnRate;
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float verticalBuffer;
    CameraBound cameraBound;
    float countdown;
    void Start()
    {
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            countdown = spawnRate;
            var verticalPosition = Random.Range(cameraBound.MinY+verticalBuffer, cameraBound.MaxY-verticalBuffer);
            var enemy = EnemyPool.Retrieve();
            enemy.transform.position = new Vector3(cameraBound.MaxX, verticalPosition, 0);
            enemy.Initialize();
        }
    }
}
