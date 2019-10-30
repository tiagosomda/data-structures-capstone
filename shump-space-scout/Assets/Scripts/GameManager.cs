using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    float playerEasiInDuration;
    [SerializeField]
    float playerBorderPadding;
    [SerializeField]
    AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    [SerializeField]
    EnemySpawner enemySpawner;

    [SerializeField]
    GameObject scorePanel;

    [SerializeField]
    GameObject gameOverPanel;

    PlayerController player;
    bool isGameOver;
    public static bool IsGameOver
    {
        get 
        {
            return instance.isGameOver;
        }
    }

    private static GameManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one GameManager found ["+instance.gameObject.name+","+gameObject.name+"]");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartGameAnimation();
        scorePanel.SetActive(false);
        isGameOver = true;
    }

    private void StartGameAnimation()
    {
        StartCoroutine(EaseInPlayer());
    }

    private IEnumerator EaseInPlayer()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        player.transform.position = new Vector3(-10000, 0, 0);

        yield return new WaitForSeconds(0.5f);

        var cameraBound = GameObject.FindObjectOfType<CameraBound>();
        player.transform.position = new Vector3(cameraBound.MinX-1,0,0);

        float elapsed = 0;
        float rate = (1.0f/playerEasiInDuration);
        var target = new Vector3(cameraBound.MinX + playerBorderPadding,0,0);
        var start = player.transform.position;
        while(elapsed < 1)
        {
            elapsed += rate*Time.deltaTime;
            yield return new WaitForEndOfFrame();
            player.transform.position = Vector3.Lerp(start, target, curve.Evaluate(elapsed));
        }

        enemySpawner.gameObject.SetActive(true);
        ScoreKeeper.SetIsRunning(true);
        instance.scorePanel.SetActive(true);
        isGameOver = false;
    }

    private void ReturnAllBullets()
    {
        var bullets = GameObject.FindObjectsOfType<Bullet>();
        foreach(var bullet in bullets)
        {
            BulletPool.Return(bullet);
        }
    }

    private void ReturnAllEnemies()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyController>();
        foreach(var enemy in enemies)
        {
            EnemyPool.Return(enemy);
        }
    }

    public static void EndGame()
    {
        instance.isGameOver = true;
        ScoreKeeper.SetIsRunning(false);
        instance.enemySpawner.gameObject.SetActive(false);
        instance.scorePanel.SetActive(false);
        instance.gameOverPanel.SetActive(true);
        instance.ReturnAllBullets();
        instance.ReturnAllEnemies();
        GameOverPanel.ShowScore(ScoreKeeper.GetScore(), ScoreKeeper.GetTimeAlive());
    }

    public static void RestartGame()
    {
        ScoreKeeper.SetScore(0);
        ScoreKeeper.SetTimeAlive(0);
        instance.gameOverPanel.SetActive(false);
        instance.StartGameAnimation();
    }
}
