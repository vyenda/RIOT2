using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [11/20/2023]
 * Codes for the first level, first difficulty enemy
 */

public class EnemyL1D1 : MonoBehaviour
{
    public float speed;
    private float dist;
    public float health = 100f;

    public float playerDamage = 15f;

    //makes it so the enemy can't go off screen
    public float minX = -17f;
    public float maxX = 17f;
    private Vector3 temp;

    public bool goingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        EnemyHealth();
    }

    /// <summary>
    /// codes for what happens when the enemy interacts with game objects
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            health -= playerDamage;
        }
    }

    /// <summary>
    /// codes for enemy movement
    /// </summary>
    /*private void Move()
    {
        if (goingLeft)
        {
            if (transform.position.x >= -dist)
            {
                temp = Vector3.right;
                SetRandomDirectionSwitch();
                goingLeft = false;
            }
        }
        else
        {
            if (transform.position.x <= dist)
            {
                temp = Vector3.left;
                SetRandomDirectionSwitch();
                goingLeft = true;
            }
        }

        transform.position += temp * Time.deltaTime * speed;
    }*/

    /// <summary>
    /// makes the enemy move randomly
    /// </summary>
    /*private void SetRandomDirectionSwitch()
    {
        dist = Random.Range(minX, maxX);
    }*/

    private void EnemyHealth()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("You killed the first enemy.");
            //maybe add a waiting IEnumerator for 5 seconds
            //before moving to the next screen/level?
            //code for the scene transition here
        }
    }
}
