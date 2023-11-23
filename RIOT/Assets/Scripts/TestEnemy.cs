using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    private float dist;

    //makes sure tree dosen't go off screen
    public float minX = -26f;
    public float maxX = 26f;
    private Vector3 temp;

    //shortend version of moving code in PlayerController script
    //private Vector3 moveDirection = Vector3.right;

    //the second the game starts, tree will always go right
    public bool goingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (goingRight)
        {
            if (transform.position.x >= -dist)
            {
                temp = Vector3.left;
                SetRandomDirectionSwitch();
                goingRight = false;
            }
        }
        else
        {
            if (transform.position.x <= dist)
            {
                temp = Vector3.right;
                SetRandomDirectionSwitch();
                goingRight = true;
            }
        }

        

        transform.position += temp * Time.deltaTime * speed;

    }

    private void SetRandomDirectionSwitch()
    {
        dist = Random.Range(minX, maxX);
    }

}
