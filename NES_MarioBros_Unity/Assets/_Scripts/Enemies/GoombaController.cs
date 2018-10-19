using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Goomba
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GoombaController : MonoBehaviour
    {
        //Components
        public Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private GoombaAnimation animationScript;

        //Scriptable Object
        public BP_Enemies Goomba;

        //Character Status
        private Status state;

        //Speed Variables
        [HideInInspector]
        public float moveDir;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();            
            animationScript = GetComponent<GoombaAnimation>();
            
            //Starts going towards the player
            moveDir = -1;
            rb.velocity = new Vector2(Goomba.moveSpeed * moveDir, rb.velocity.y);
        }

        private void FixedUpdate()
        {
            if (!GameManager.isRunning && state != Status.Dead)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            CheckDeathPlane();

            Move();
        }

        private void Move ()
        {
            if (rb.velocity.x == 0)
            {
                moveDir *= -1;

                rb.velocity = new Vector2(Goomba.moveSpeed * moveDir, rb.velocity.y);
            }

            rb.velocity = new Vector2(Goomba.moveSpeed * moveDir, rb.velocity.y);

            animationScript.ChangeSprite(state, spriteRenderer, Goomba);
        }        

        private void CheckDeathPlane ()
        {
            if (rb.position.y <= GameManager.deathPlaneY)
            {
                Destroy(gameObject);
            }
        }

        public void Die ()
        {
            //Zero the speed multiplier
            moveDir = 0;

            state = Status.Dead;

            animationScript.ChangeSprite(state, spriteRenderer, Goomba);
        }
    }
}