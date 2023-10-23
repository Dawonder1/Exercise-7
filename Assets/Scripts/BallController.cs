using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public Rigidbody rb;
    public float impulseForce = 5f;

    private Vector3 resetPos;
    public int perfectPass = 0;
    private bool ignoreNextCollision;
    public bool isSuperSpeedActive;

    private void Awake()
    {
        resetPos = new Vector3(0, 3, -1.8f);
        //set color for each ball on creation
        GetComponent<Renderer>().material.color = FindObjectOfType<HelixController>().allStages[GameManager.singleton.currentStage].stageBallColor;
    }



    private void OnCollisionEnter(Collision other)
    {
        if (GameManager.singleton.gameOver)
        {
            return;
        }
        if (ignoreNextCollision)
            return;
        if (isSuperSpeedActive)
        {
            if (!other.transform.GetComponent<Goal>())
            {
                /*foreach (Transform t in other.transform.parent)
                {
                    gameObject.AddComponent<TriangleExplosion>();

                    StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
                    //Destroy(other.gameObject);
                    Debug.Log("exploding - exploding - exploding - exploding");
                }*/
                Destroy(other.transform.parent.gameObject);

            }

        }
        // If super speed is not active and a death part git hit -> restart game
        else
        {
            DeathPart deathPart = other.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                //destroy extra balls
                if (FindObjectsOfType<BallController>().Length > 1)
                {
                    DeleteAllBallsExceptOne();
                    Destroy(deathPart.gameObject);
                }
                else
                {
                    deathPart.HittedDeathPart();
                }
            }
        }

        rb.velocity = Vector3.zero; // Remove velocity to not make the ball jump higher after falling done a greater distance
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);



        // Safety check
        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        // Handling super speed
        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        // activate super speed
        if (perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    public void ResetBall()
    {
        transform.position = resetPos;
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    public void DeleteAllBallsExceptOne()
    {
        if (FindObjectsOfType<BallController>().Length > 1)
        {
            Destroy(gameObject);
            Debug.Log(FindObjectsOfType<BallController>().Length + " balls are left");
        }
        FindObjectOfType<BallController>().perfectPass = 0;
    }
}
