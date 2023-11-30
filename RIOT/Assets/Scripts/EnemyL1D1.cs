using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [11/30/2023]
 * Codes for the first level, first difficulty enemy
 */

public class EnemyL1D1 : MonoBehaviour
{
    public float speed;
    private float dist;
    public float health = 100f;

    public float playerDamage = 15f;
    public float swordDamage = 20f;
    public float gunDamage = 25f;

    //makes it so the enemy can't go off screen
    public float minX = -26f;
    public float maxX = 26f;
    private Vector3 temp;

    public bool goingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        EnemyHealth();
    }

    /// <summary>
    /// codes for what happens when the enemy interacts with game objects
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerArm")
        {
            health -= playerDamage;
        }

        if (other.gameObject.tag == "PlayerSword")
        {
            health -= swordDamage;
        }

        /*if (other.gameObject.tag == "Bullet")
        {
            health -= gunDamage;
        }*/
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
