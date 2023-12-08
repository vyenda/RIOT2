using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/07/2023]
 * Codes for the hard difficulty, level 2 enemy.
 */

public class HardEnemy2 : MonoBehaviour
{
    //variables for movement
    public float speed;
    private float dist;
    public float minX = -26f;
    public float maxX = 26f;
    public bool goingRight = true;
    private Vector3 temp;

    //variables for enemy health
    public float health = 150f;
    public float playerDamage = 15f;
    public float swordDamage = 20f;

    //variables for pick up items
    public bool attackUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        EnemyHealth();
        NextLevel();
    }

    /// <summary>
    /// codes for events that happen when this object interacts with other objects
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //the enemy will get hurt if they hit the player arm
        if (other.gameObject.tag == "PlayerArm")
        {
            health -= playerDamage;
        }

        //the enemy will get hurt if they hit the player sword
        if (other.gameObject.tag == "PlayerSword")
        {
            health -= swordDamage;
        }

        //the enemy's attack will go up if they touch this item
        if (other.gameObject.tag == "AttackThree")
        {
            StartCoroutine(AttackThree(10));
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// ups the enemy's attack for a limited amount of time
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackThree(float secondsToWait)
    {
        attackUp = true;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enemyL3D1Damage = 20f;
            yield return new WaitForSeconds(secondsToWait);
            player.enemyL3D1Damage = 15f;
        }

        attackUp = false;
    }

    /// <summary>
    /// tracks the enemy's health and kills it if it reaches 0 or below
    /// </summary>
    private void EnemyHealth()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("You killed the last enemy.");
        }
    }

    /// <summary>
    /// makes the enemy move
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
    /// makes the enemy move randomly
    /// </summary>
    private void SetRandomDirectionSwitch()
    {
        dist = Random.Range(minX, maxX);
    }

    /// <summary>
    /// Moves onto the next level if the enemy dies.
    /// </summary>
    private void NextLevel()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(7);
            Debug.Log("Enemy dead, next level.");
        }
    }

}
