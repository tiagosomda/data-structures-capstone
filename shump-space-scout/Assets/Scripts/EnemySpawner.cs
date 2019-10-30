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
    private CameraBound cameraBound;
    void Start()
    {
        cameraBound = GameObject.FindObjectOfType<CameraBound>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while(true) 
        {
            var verticalPosition = Random.Range(cameraBound.MinY+verticalBuffer, cameraBound.MaxY-verticalBuffer);
            var enemy = EnemyPool.Retrieve();
            enemy.transform.position = new Vector3(cameraBound.MaxX, verticalPosition, 0);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
