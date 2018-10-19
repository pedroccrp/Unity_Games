using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class PlayerCheckDeath : MonoBehaviour {

        private void FixedUpdate()
        {
            if (PlayerController.instance.state == Status.Dead)
            {
                return;
            }

            if (PlayerController.instance.rb.position.y <= GameManager.deathPlaneY)
            {
                PlayerManager.Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitbox"))
            {
                if (PlayerController.instance.state != Status.Dead)
                {
                    PlayerManager.Die();
                }
            }
        }
    }
}