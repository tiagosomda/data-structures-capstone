using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    private float updateInterval = 0.25f;

    private static ScoreKeeper instance;
    private int score;
    private int timeAlive;
    private bool isRunning = true;
    private float counter;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            score = 0;
            timeAlive = 0;
        }
        else
        {
            Debug.LogError("More than one ScoreKeeper found ["+instance.gameObject.name+","+gameObject.name+"]");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(!isRunning)
        {
            return;
        }

        timeAlive += (int)(Time.deltaTime*100);
        ScorePanel.SetTimeAlive(timeAlive);
        counter -= Time.deltaTime;
        if(counter < 0)
        {
            counter = updateInterval;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        ScorePanel.SetScore(score);
        ScorePanel.SetTimeAlive((int)timeAlive);
    }

    public static void SetIsRunning(bool value)
    {
        instance.isRunning = value;
    }
    public static void SetTimeAlive(int value)
    {
        instance.timeAlive = value;
    }
    public static void SetScore(int value)
    {
        instance.score = value;
    }
    public static void AddScore(int value)
    {
        instance.score += value;
    }

    public static int GetScore()
    {
        return instance.score;
    }

    public static int GetTimeAlive()
    {
        return instance.timeAlive;
    }
}
