using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [HideInInspector] public int score = 0;
    private int highScore;

    private int collectibleCount = 0;
    //public TextMeshProUGUI gemCount; //temp


    public void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        //gemCount.text = PlayerPrefs.GetInt("CollectibleCount").ToString();
        collectibleCount = PlayerPrefs.GetInt("CollectibleCount");
    }

    public void ScoreCounter()
    {
        score += 4;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = score.ToString();
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore",score);
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        
        highScoreText.text =  highScore.ToString(); //updates the highscore immediately

    }

    public void ScoreCounterBig()
    {
        score += (int)FindObjectOfType<RandomFoodObstacle>().initBigFoodValue;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = score.ToString();
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        highScoreText.text = highScore.ToString();

    }

    public void CollectibleCounter()
    {
        collectibleCount += 1;
        PlayerPrefs.SetInt("CollectibleCount", collectibleCount);
    }    
    
}
