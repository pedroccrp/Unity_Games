using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    public bool isPlayer1;

    public float maxPositionY;
    
	void FixedUpdate () {

        if (isPlayer1)
        {
            transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0));            
        }
        else
        {
            transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical1") * speed * Time.deltaTime, 0));
        }


        if (transform.position.y > maxPositionY)
        {
            transform.position = new Vector3(transform.position.x, maxPositionY, 0);
        }
        else if (transform.position.y < -maxPositionY)
        {
            transform.position = new Vector3(transform.position.x, -maxPositionY, 0);
        }

    }
}
