using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
   private static GameOverPanel instance;

    [SerializeField]
    private Text scorePointsText;

    [SerializeField]
    private Text timeAlivePointsText;

    [SerializeField]
    private Text totalCalculationText;

    [SerializeField]
    private Text totalPointsText;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one GameOverPanel found ["+instance.gameObject.name+","+gameObject.name+"]");
            Destroy(gameObject);
        }
    }

    public static void ShowScore(int score, int timeAlive)
    {
        instance.scorePointsText.text = score.ToString();
        instance.timeAlivePointsText.text = timeAlive.ToString();
        instance.totalCalculationText.text = score + " x " + timeAlive;
        instance.totalPointsText.text = "   = " + (score*timeAlive);
    }

    public void RestartGame()
    {
        GameManager.RestartGame();
    }
}
