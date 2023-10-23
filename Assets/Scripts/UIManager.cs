using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtBest;
    [SerializeField] private TextMeshProUGUI timeLeft;
    [SerializeField] private TextMeshProUGUI timeRecord;
    [SerializeField] private TextMeshProUGUI gameOvertext;
    [SerializeField] private Button casualButton;
    [SerializeField] private Button timeButton;
    [SerializeField] public Button playAgainButton;
    public string mode;
    public int maxBalls;

    void Update()
    {
        if (mode == "casual")
        {
            txtBest.text = "Best: " + GameManager.singleton.best;
            txtScore.text = "Score: " + GameManager.singleton.score;
        }
        
        if(mode == "time")
        {
            timeLeft.text = "Time Left: " + (60 - GameManager.singleton.numOfSeconds).ToString();
            timeRecord.text = "Time Record: " + GameManager.singleton.bestTime;
        }

        if(GameManager.singleton.gameOver)
        {
            gameOvertext.text = "Game Over";
            gameOvertext.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
        }
    }

    public void timeMode()
    {
        mode = "time";
        txtBest.gameObject.SetActive(false);
        txtScore.gameObject.SetActive(false);
        casualButton.gameObject.SetActive(false);
        timeButton.gameObject.SetActive(false);
        GameManager.singleton.countDown();
        FindObjectOfType<BallController>().gameObject.GetComponent<Rigidbody>().useGravity = true;
        maxBalls = 1;
    }

    public void casualMode()
    {
        mode = "casual";
        timeLeft.gameObject.SetActive(false);
        timeRecord.gameObject.SetActive(false);
        casualButton.gameObject.SetActive(false);
        timeButton.gameObject.SetActive(false);
        FindObjectOfType<BallController>().gameObject.GetComponent<Rigidbody>().useGravity = true;
        maxBalls = 2;
    }

    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
