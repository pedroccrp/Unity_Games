using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton        
        public static PlayerManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion
        
        //Scriptable Object
        public BP_Character Character;

        public Vector2 startPosition;

        public static GameStatus gameStatus;

        private void Start()
        {
            gameStatus.lifes = 3;
        }

        public static void ReSpawn ()
        {
            if (gameStatus.lifes <= 0)
            {
                GameManager.GameOver();
            }
            else
            {
                GameManager.SoftReset();
            }
        }

        public static void Die()
        {
            gameStatus.lifes--;

            PlayerController.instance.state = Status.Dead;

            PlayerController.instance.GetComponent<BoxCollider2D>().enabled = false;

            //Stops game
            GameManager.isRunning = false;

            PlayerAnimation.instance.DeathAnimation();
        }

    }

    public struct GameStatus
    {
        public float lifes;
        public float coins;
    }
}