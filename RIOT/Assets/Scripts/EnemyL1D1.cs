using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/04/2023]
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

    public bool guard = false;
    public bool recharge = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Recharge(13));
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
        if (!guard)
        {
            if (other.gameObject.tag == "PlayerArm")
            {
                health -= playerDamage;
            }

            if (other.gameObject.tag == "PlayerSword")
            {
                health -= swordDamage;
            }
        }

        /*if (other.gameObject.tag == "Bullet")
        {
            health -= gunDamage;
        }*/
    }

    /// <summary>
    /// Codes for the enemy's random movement
    /// </summary>
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

    /// <summary>
    /// Makes the enemy's movements more random
    /// </summary>
    private void SetRandomDirectionSwitch()
    {
        dist = Random.Range(minX, maxX);
    }

    /// <summary>
    /// codes for the enemy's guarding ability
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator Guard(float secondsToWait)
    {
        guard = true;
        yield return new WaitForSeconds(secondsToWait);
        guard = false;
        StartCoroutine(Recharge(13));
    }

    /// <summary>
    /// pauses the enemy's guarding
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator Recharge(float secondsToWait)
    {
        recharge = true;
        yield return new WaitForSeconds(secondsToWait);
        recharge = false;
        StartCoroutine(Guard(3));
    }

    /// <summary>
    /// tracks the enemy's health. If it gets at or below 0, it will die
    /// </summary>
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
