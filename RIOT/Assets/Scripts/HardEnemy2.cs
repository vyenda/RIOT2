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
    //ideas for this enemy
    //have them jump from time to time
    //have the random movement deactivate if player is detected in raycast
    //have the enemy only be able to take damage in one area (back, head, etc)
    //will have a sword (maybe a gun too)?
    //limit guarding ability compared to other enemies?

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

    //public GameObject bulletsPrefab;
    public float spawnrate = 1f;

    //public bool shootRight = false;

    public Animation enemySword;
    public bool attack = false;
    public bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("ShootBullets", 0, spawnrate);
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

    /// <summary>
    ///  starts the enemy attack animation 
    /// </summary>
    private void HitPlayer()
    {
        if (attack)
        {
            enemySword.Play("EnemySword");
        }
    }

    /*private void ShootBullets()
    {
        GameObject bulletsInstance = Instantiate(bulletsPrefab, transform.position, transform.rotation);
        bulletsInstance.GetComponent<Bullets>().goingRight = shootRight;
    }*/

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
