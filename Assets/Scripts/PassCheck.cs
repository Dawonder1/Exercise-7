using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.AddScore(2);

        other.GetComponent<BallController>().perfectPass++;

        //add new ball when perfect score reaches 4.
        if(other.GetComponent<BallController>().perfectPass == 4 && FindObjectsOfType<BallController>().Length < FindObjectOfType<UIManager>().maxBalls)
        {
            Instantiate(other.gameObject, other.transform.position, other.transform.rotation);
        }
    }
}
