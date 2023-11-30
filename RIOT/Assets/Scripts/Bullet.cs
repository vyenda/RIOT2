using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [11/30/2023]
 * Codes for the bullets
 */

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool goingRight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
