using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] DotManager dotManager;
    [SerializeField] TimeManager timeManager;
    [SerializeField] float maxTime;
    [SerializeField] GameObject canvasGameOver;
    private void Start()
    {
        timeManager.SetUp(maxTime);
        timeManager.OnTimerFinish += Failure;
    }

    public void OnSuccessfulConnection(Dot dot1, Dot dot2) 
    {
        scoreManager.AddScore();
        timeManager.AddTime(5);
        dotManager.FreePointFromDot(dot1);
        dotManager.FreePointFromDot(dot2);
    }
    public void OnFailedConnection() 
    {
        timeManager.RemoveTime(5);
    }

    private void Failure()
    {

        canvasGameOver.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("GameOver!");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
