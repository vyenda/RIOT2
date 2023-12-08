using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/07/2023]
 * Codes for the first level, first difficulty enemy
 */

public class EnemyL1D1 : MonoBehaviour
{
    public float speed = 5.5f;
    private float dist;
    public float health = 100f;

    public float playerDamage = 15f;
    public float swordDamage = 20f;

    //makes it so the enemy can't go off screen
    public float minX = -26f;
    public float maxX = 26f;
    private Vector3 temp;

    public bool goingRight = true;

    public bool guard = false;
    public bool recharge = false;

    public bool attack = false;
    public bool pause = false;

    public bool attackUp = false;

    public Animation enemyArm;
    internal string text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Recharge(13));
        StartCoroutine(Pause(1));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        EnemyHealth();
        NextLevel();
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

        if (other.gameObject.tag == "AttackUp")
        {
            StartCoroutine(AttackUp(10));
            Destroy(other.gameObject);
        }

        /*if (other.gameObject.tag == "Bullet")
        {
            health -= gunDamage;
        }*/
    }

    /// <summary>
    /// ups the attack of the enemy for the specified period of time when called
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackUp(float secondsToWait)
    {
        attackUp = true;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enemyL1D1Damage = 15f;
            yield return new WaitForSeconds(secondsToWait);
            player.enemyL1D1Damage = 5f;
        }

        attackUp = false;
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
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                goingRight = false;
            }
        }
        else
        {
            if (transform.position.x <= dist)
            {
                temp = Vector3.right;
                SetRandomDirectionSwitch();
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
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
    /// causes the enemy to periodically attack the player
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator Attack(float secondsToWait)
    {
        attack = true;
        HitPlayer();
        yield return new WaitForSeconds(secondsToWait);
        attack = false;
        StartCoroutine(Pause(1));
    }

    /// <summary>
    /// causes the enemy to pause from attacking the player
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator Pause(float secondsToWait)
    {
        pause = true;
        yield return new WaitForSeconds(secondsToWait);
        pause = false;
        StartCoroutine(Attack(1));
    }
    //maybe in the future we can try a raycast so that the enemy doesn't start the
    //attack coroutine unless the player game object is nearby

    /// <summary>
    /// starts the enemy attack animation 
    /// </summary>
    private void HitPlayer()
    {
        if (attack)
        {
            enemyArm.Play("EnemyArm");
        }
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

    /// <summary>
    /// Moves onto the next level if enemy dies.
    /// </summary>
    private void NextLevel()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(3);
            Debug.Log("Enemy dead, next level.");
        }
    }
}
