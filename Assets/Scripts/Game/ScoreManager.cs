using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int boostScore = 0;

    public float boostTime = 0f;

    public bool isBoost = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI boostScoreText;
    public TextMeshProUGUI boostTimeText;

    public TextMeshProUGUI gameOverTextScore;
    private int gameOverScore;

    void Start()
    {
        boostTimeText.maxVisibleCharacters = 16;
    }

    void Update()
    {
        DisplayScore();

        if (isBoost == true)
        {
            boostTime += Time.deltaTime;
        }
        gameOverScore = score;
        BoostTimeFalse();
    }

    public void AddScore()
    {
        isBoost = true;     
        score += 1 + boostScore; 
        BoostTimeTrue();
    }

    private void DisplayScore() 
    {
        scoreText.text = "Score: " + score;
        boostScoreText.text = "Boost score: " + boostScore;
        boostTimeText.text = "Boost time: " + boostTime;
        gameOverTextScore.text = "Your Score: " + gameOverScore;
    }


    private void BoostTimeTrue()
    {
        if (isBoost == true && boostTime <= 3f)
        {
            boostScore++;
            boostTime = 0f;
        }
    }

    private void BoostTimeFalse()
    {
        if (boostTime >= 3f)
        {
            isBoost = false;
            boostScore = 0;
            boostTime = 0f;
        }
    }
}
