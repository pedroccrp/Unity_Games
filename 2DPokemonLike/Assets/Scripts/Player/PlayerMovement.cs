using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float runSpeed;

    private Directions dir;

    public CharacterStatus status;

    private bool wantsToMove;

    void Start () {
        status = new CharacterStatus();
        status.moving = false;
        status.occupied = false;

        dir = Directions.RIGHT;

        wantsToMove = false;
    }

    void Update () {

        GetInput();

        Move();		
	}

    private void GetInput ()
    {
        wantsToMove = false;

        //Move Direction
        if (Input.GetKey(Buttons.right))
        {
            dir = Directions.RIGHT;
            wantsToMove = true;
        }
        else if (Input.GetKey(Buttons.left))
        {
            dir = Directions.LEFT;
            wantsToMove = true;
        }
        else if (Input.GetKey(Buttons.up))
        {
            dir = Directions.UP;
            wantsToMove = true;
        }
        else if (Input.GetKey(Buttons.down))
        {
            dir = Directions.DOWN;
            wantsToMove = true;
        }

        //Checks if running
        status.running = Input.GetKey(Buttons.run);
    }

    private void Move()
    {
        if (wantsToMove && !status.moving && !status.occupied)
        {
            Vector2 moveDir = GetLookVector();
            Vector2 newPos = (Vector2)transform.position + moveDir;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, 0.2f);

            bool canWalkToNextTile = true;

            foreach (Collider2D col in colliders)
            {
                if (col.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    canWalkToNextTile = false;
                    break;
                }
            }

            if (canWalkToNextTile)
            {
                status.moving = true;
                StartCoroutine(SmoothMove((Vector2)transform.position, 
                    moveDir, 
                    status.running ? runSpeed : moveSpeed));                
            }

            wantsToMove = false;
        }
    }

    private IEnumerator SmoothMove (Vector2 initPos, Vector2 moveDir, float speed)
    {
        float increment = 0;

        while (increment < 1f)
        {
            increment += (1f / speed) * Time.deltaTime;

            if (increment > 1f)
            {
                increment = 1f;
            }

            transform.position = initPos + (moveDir * increment);

            yield return null;
        }

        status.moving = false;
    }

    private Vector2 GetLookVector ()
    {
        if (dir == Directions.RIGHT)
        {
            return Vector2.right;
        } else if (dir == Directions.UP)
        {
            return Vector2.up;
        }
        else if (dir == Directions.LEFT)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.down;
        }
    }
}

[System.Serializable]
public class CharacterStatus
{
    public bool occupied = false;
    public bool moving   = false;
    public bool still     = true;
    public bool running  = false;
    public bool surfing  = false;
    public bool bike     = false;
    public bool strength = false;
}

public enum Directions
{
    RIGHT,
    UP,
    LEFT,
    DOWN
}
