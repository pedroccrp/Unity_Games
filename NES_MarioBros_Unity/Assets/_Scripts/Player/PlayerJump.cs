using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        public static void CheckGround()
        {
            if (PlayerController.instance.rb.velocity.y != 0)
            {
                PlayerController.instance.isGrounded = false;
                return;
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(PlayerController.instance.rb.position.x, PlayerController.instance.rb.position.y - 1f), 0.7f);

            foreach (Collider2D hit in hits)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    PlayerController.instance.isGrounded = true;
                    return;
                }
            }

            PlayerController.instance.isGrounded = false;
        }

        public static void Jump()
        {
            if (PlayerController.instance.isGrounded)
            {
                PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, PlayerManager.instance.Character.jumpSpeed);
                PlayerController.instance.isGrounded = false;
            }

            if (PlayerController.instance.rb.velocity.y < 0)
            {
                PlayerController.instance.rb.gravityScale = PlayerManager.instance.Character.fallMultiplier;
            }
            else if (PlayerController.instance.rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                PlayerController.instance.rb.gravityScale = PlayerManager.instance.Character.lowJumpMultiplier;
            }
            else
            {
                PlayerController.instance.rb.gravityScale = PlayerManager.instance.Character.normalMultiplier;
            }
        }

        public static void Jump(float jumpSpeed)
        {
            PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, jumpSpeed);

            if (PlayerController.instance.rb.velocity.y < 0)
            {
                PlayerController.instance.rb.gravityScale = PlayerManager.instance.Character.fallMultiplier;
            }
            else if (PlayerController.instance.rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                PlayerController.instance.rb.gravityScale = PlayerManager.instance.Character.lowJumpMultiplier;
            }
            else
            {
                PlayerController.instance.rb.gravityScale = PlayerManager.instance.Character.normalMultiplier;
            }
        }
    }
}
