using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        #region Singleton        
        public static PlayerController instance;

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

        //Components
        public Rigidbody2D rb;        

        //Character Status
        [HideInInspector]
        public Directions dir;
        [HideInInspector]
        public Status state;

        //Jump Related Variables
        [HideInInspector]
        public bool isGrounded = true;
        private bool wantsToJump = false;
        private bool canJump = true;

        //Movement Related Variables
        private float currentSpeed = 0;
        private float speedDir = 0;
        [Header("Movement")]
        [Range(0, 10)]
        public float moveSpeed;
        [Range(0, 10)]
        public float runningSpeed;

        private void Start()
        {
            dir = Directions.Right;
            state = Status.Idle;

            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!GameManager.isRunning)
            {
                return;
            }

            GetInput();
        }

        private void FixedUpdate()
        {
            if (!GameManager.isRunning)
            {
                return;
            }

            PlayerJump.CheckGround();
            Move();
        }

        private void GetInput()
        {
            if (state == Status.Dead)
            {
                return;
            }

            speedDir = Input.GetAxisRaw("Horizontal");

            if (speedDir != 0)
            {
                dir = speedDir > 0 ? Directions.Right : Directions.Left;

                if (Input.GetKey(KeyCode.X))
                {
                    state = Status.Running;
                }
                else
                {
                    state = Status.Moving;
                }
            }
            else
            {
                state = Status.Idle;
                PlayerAnimation.movingTime = 0;
            }

            wantsToJump = Input.GetKey(KeyCode.Space);
        }
        
        private void Move()
        {
            float speed = (state == Status.Running) ? runningSpeed : moveSpeed;

            rb.velocity = new Vector2(speedDir * speed, rb.velocity.y);

            if (canJump && wantsToJump)
            {
                PlayerJump.Jump();
            }

            PlayerAnimation.ChangeSprite();
        }   

        public void KillJump ()
        {
            float jSpeed = Input.GetKey(KeyCode.Space) ? PlayerManager.instance.Character.jumpSpeed : PlayerManager.instance.Character.enemtKillJumpSpeed;

            PlayerJump.Jump(jSpeed);
        }        
    }
}

public enum Directions
{
    Left,
    Right
}

public enum Status
{
    Idle,
    Moving,
    Running,
    Dead
}