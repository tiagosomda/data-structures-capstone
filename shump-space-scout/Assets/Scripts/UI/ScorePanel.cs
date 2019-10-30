using UnityEngine;
using UnityEngine.UI;
public class ScorePanel : MonoBehaviour
{
    private static ScorePanel instance;

    [SerializeField]
    private Text ScoreText;

    [SerializeField]
    private Text TimeAliveText;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one ScorePanel found ["+instance.gameObject.name+","+gameObject.name+"]");
            Destroy(gameObject);
        }
    }

    public static void SetTimeAlive(int value)
    {
        instance.TimeAliveText.text = value.ToString();
    }

    public static void SetScore(int value)
    {
        instance.ScoreText.text = value.ToString();
    }
}
