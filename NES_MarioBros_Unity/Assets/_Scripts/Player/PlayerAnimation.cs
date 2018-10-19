using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class PlayerAnimation : MonoBehaviour {

        #region Singleton        
        public static PlayerAnimation instance;

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
        private static SpriteRenderer spriteRenderer;

        //Sprite Change
        [HideInInspector]
        public static float movingTime = 0;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public static void ChangeSprite()
        {
            spriteRenderer.flipX = (PlayerController.instance.dir == Directions.Left);

            if (PlayerController.instance.state == Status.Idle)
            {
                spriteRenderer.sprite = PlayerManager.instance.Character.sprites.idle;
            }
            else if (PlayerController.instance.state == Status.Moving)
            {
                spriteRenderer.sprite = PlayerManager.instance.Character.sprites.moving[(int)(movingTime) % PlayerManager.instance.Character.sprites.moving.Length];
            }
            else if (PlayerController.instance.state == Status.Running)
            {
                spriteRenderer.sprite = PlayerManager.instance.Character.sprites.moving[(int)(movingTime) % PlayerManager.instance.Character.sprites.moving.Length];
            }

            if (!PlayerController.instance.isGrounded)
            {
                spriteRenderer.sprite = PlayerManager.instance.Character.sprites.jumping;
            }


            movingTime += PlayerManager.instance.Character.changeSpriteSpeed;
        }

        public void DeathAnimation()
        {
            spriteRenderer.flipX = false;

            spriteRenderer.sprite = PlayerManager.instance.Character.sprites.dead;

            StartCoroutine(SmoothDeath());
        }

        private IEnumerator SmoothDeath()
        {
            if (PlayerController.instance.rb.position.y < GameManager.deathPlaneY)
            {
                PlayerController.instance.rb.position = new Vector2(PlayerController.instance.rb.position.x, GameManager.deathPlaneY);
            }

            PlayerController.instance.rb.velocity = new Vector2(0, PlayerController.instance.rb.velocity.y);

            PlayerJump.Jump(PlayerManager.instance.Character.deathJumpSpeed);

            while (PlayerController.instance.rb.position.y >= GameManager.deathPlaneY)
            {
                yield return null;
            }

            PlayerManager.ReSpawn();
        }
    }
}