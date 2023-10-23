using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.singleton.gameOver)
        {
            GameManager.singleton.NextLevel();
        }
    }
}
