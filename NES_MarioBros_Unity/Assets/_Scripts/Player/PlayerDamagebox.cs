using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerDamagebox : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitbox"))
            {
                if (PlayerController.instance.state == Status.Dead)
                {
                    return;
                }

                collision.transform.parent.gameObject.GetComponent<EnemyManager>().Kill();

                transform.parent.gameObject.GetComponent<PlayerController>().KillJump();        
            }            
        }
    }
}
