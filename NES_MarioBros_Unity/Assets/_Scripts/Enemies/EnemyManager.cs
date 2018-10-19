using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Goomba;

namespace Enemy {

    public class EnemyManager : MonoBehaviour {

        public Vector2 startPos;

        public EnemyNames enemyName;

        private void Awake()
        {
            if (enemyName == EnemyNames.Goomba)
            {
                GameManager.AddEnemy((GameObject)Resources.Load("Prefabs/Enemies/Goomba"), startPos);
            }            
        }

        public void Spawn (Vector2 pos)
        {
            GetComponent<GoombaController>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().position = pos;
        }

        public void Kill ()
        {
            if (enemyName == EnemyNames.Goomba)
            {
                transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<GoombaController>().Die();
            }
        }
    }

    [System.Serializable]
    public enum EnemyNames
    {
        Goomba
    }
}
