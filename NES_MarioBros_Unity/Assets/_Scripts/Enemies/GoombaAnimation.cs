using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Goomba
{
    public class GoombaAnimation : MonoBehaviour
    {
        //Sprite Change
        [HideInInspector]
        public static float movingTime = 0;

        public void ChangeSprite(Status state, SpriteRenderer spriteRenderer, BP_Enemies Goomba)
        {
            if (state != Status.Dead)
            {
                spriteRenderer.sprite = Goomba.sprites.moving[(int)(movingTime) % Goomba.sprites.moving.Length];
            }
            else
            {
                spriteRenderer.sprite = Goomba.sprites.dead;

                StartCoroutine(DeathAnimation(spriteRenderer.gameObject, Goomba.deathAnimTime));
            }

            movingTime += Goomba.changeSpriteSpeed;                        
        }

        private IEnumerator DeathAnimation (GameObject enemyObj, float animTime)
        {
            yield return new WaitForSeconds(animTime);

            Destroy(enemyObj);
        }
    }
}


