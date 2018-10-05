using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Rigidbody BallRb;
    public float ballSpeed;

    public Transform Bump1, Bump2;

    public Text pText, eText;

    private int playerScore, enemyScore;

    private void LateUpdate()
    {
        if (BallRb.position.x < -18)
        {
            enemyScore++;
            ReStartGame();
            eText.text = enemyScore.ToString();
        }
        else if (BallRb.position.x > 18)
        {
            playerScore++;
            ReStartGame();
            pText.text = playerScore.ToString();
        }
    }

    private void ReStartGame ()
    {
        BallRb.position = Vector3.zero;

        float hs = Random.Range(0, 2) == 0 ? -1 : 1;
        float vs = Random.Range(0, 2) == 0 ? -1 : 1;

        BallRb.velocity = new Vector3(hs * ballSpeed, vs * ballSpeed, 0);
    }
}
