using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;
    public int best;
    public int score;
    public int numOfSeconds;
    public int bestTime;
    public bool gameOver = false;
    public int currentStage = 0;
    

    private void Awake()
    {
        string gameId = "5453013";
        Advertisement.Initialize(gameId, testMode : true);
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        // Load the saved highscore
        best = PlayerPrefs.GetInt("Highscore");
        bestTime = PlayerPrefs.GetInt("TimeRecord");
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().DeleteAllBallsExceptOne();
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);

        //reset the timer if in timer mode;
        if (FindObjectOfType<UIManager>().mode == "time")
        {
            //set latest time record
            if(numOfSeconds < bestTime || bestTime == 0)
            {
                PlayerPrefs.SetInt("TimeRecord", numOfSeconds);
                bestTime = numOfSeconds;
            }
            GameManager.singleton.resetTimer();
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        // Show Adds Advertisement.Show();
        Advertisement.Show("Interstitial_Android");
        singleton.score = 0;
        BallController[] balls = FindObjectsOfType<BallController>();
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            PlayerPrefs.SetInt("Highscore", score);
            best = score;
        }
    }

    IEnumerator startTimer()
    {
        numOfSeconds = 0;
        
        //check or set record time when we complete the stage.
        while(numOfSeconds < 60)
        {
            yield return new WaitForSeconds(1);
            numOfSeconds++;
        }
        gameOver = true;
        FindObjectOfType<UIManager>().playAgainButton.gameObject.SetActive(true);
    }

    public void countDown()
    {
        StartCoroutine(startTimer());
    }

    public void resetTimer()
    {
        StopCoroutine(startTimer());
        countDown();
    }

}
