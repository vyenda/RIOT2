using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/07/2023]
 * Codes for the player movement, and more.
 */

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbodyRef;

    public float speed = 1f;
    private Vector3 startPos;
    public float jumpForce = 10f;
    public float health = 100f;
    public float healthPoints = 100f;

    public bool facingRight = true;

    public bool shield = false;
    public bool recharge = false;

    public bool attackUp = false;

    public float enemyL1D1Damage = 5f;
    public float enemyL2D1Damage = 20f;
    public float enemyL3D1Damage = 15f;
    public GameObject effect;

    public GameObject bulletPrefab;
    public bool shootRight = false;

    //these make it so the player can't go off the screen
    public float minX = -18f;
    public float maxX = 18f;
    private Vector3 temp;

    //for animations
    public Animation armAnimation;
    public Animation swordAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        //stores the rigidbody of the player as a reference
        rigidbodyRef = GetComponent<Rigidbody>();

        //stores the players current position in the level
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if A is pressed
        if (Input.GetKey(KeyCode.A))
        {
            //the player will move left
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            facingRight = false;
        }

        //if D is pressed
        if (Input.GetKey(KeyCode.D))
        {
            //the player will move right
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            facingRight = true;
        }

        //if space bar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            //the player will jump
            HandleJump();
        }

        //if S is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            //the player will have the shield for 3 seconds
            StartCoroutine(Shield(3));
            //after the 3 seconds, shield will recharge for 5 before use again
            //StartCoroutine(Recharge(13));
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootBullet();
        }*/

        //makes it so player can't heal past 100
        if (healthPoints > health)
        {
            healthPoints = health;
        }

        //lets the player do a punch attack
        if (Input.GetKeyDown(KeyCode.K))
        {
            armAnimation.Play("ArmAnimation");
        }

        //lets the player do a sword attack
        if (Input.GetKeyDown(KeyCode.L))
        {
            swordAnimation.Play("SwordAnimation");
        }

        //tracks the player's health to see if they are dead or not
        GameOver();
    }

    /// <summary>
    /// codes for what happens when the player touches a tagged game object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!shield)
        {
            if (other.gameObject.tag == "EnemyArm")
            {
                health -= enemyL1D1Damage;
            }

            if (other.tag == "L2D1Enemy")
            {
                // Instantiate(effect, transform.position, Quaternion.identity);
                healthPoints -= 20f;
            }

            if (other.gameObject.tag == "EnemySword")
            {
                health -= enemyL3D1Damage;
            }

            
        }

        

        if (other.gameObject.tag == "HealthPickup")
        {
            healthPoints += other.gameObject.GetComponent<Pickup>().playerHeal;
        }

        if (other.gameObject.tag == "AttackUp")
        {
            StartCoroutine(AttackUp(10));
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AttackTwo")
        {
            StartCoroutine(AttackTwo(10));
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AttackThree")
        {
            StartCoroutine(AttackThree(10));
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// ups the player's attack on the enemy for the specified amount of time when called
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackUp(float secondsToWait)
    {
        attackUp = true;

        //this will need to be done here for each different enemy type
        //this is a local variable that references the enemy script
        EnemyL1D1 enemy1 = FindObjectOfType<EnemyL1D1>();
        //it will tell the computer to search the entire scene for the enemy
        //if it finds it, then it will run the code inside the if statement
        if (enemy1 != null)
        {
            enemy1.playerDamage = 20f;
            enemy1.swordDamage = 25f;
            yield return new WaitForSeconds(secondsToWait);
            enemy1.playerDamage = 15f;
            enemy1.swordDamage = 20f;
        }
        attackUp = false;
    }

    /// <summary>
    /// this will make the attack up work for the second enemy
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackTwo(float secondsToWait)
    {
        attackUp = true;

        EnemyL1D2 enemy2 = FindObjectOfType<EnemyL1D2>();
        if (enemy2 != null)
        {
            enemy2.playerDamage = 20f;
            enemy2.swordDamage = 25f;
            yield return new WaitForSeconds(secondsToWait);
            enemy2.playerDamage = 15f;
            enemy2.swordDamage = 20f;
        }

        attackUp = false;
    }

    /// <summary>
    /// this will make the attack up work for the third enemy
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackThree(float secondsToWait)
    {
        attackUp = true;

        EnemyL3D1 enemy3 = FindObjectOfType<EnemyL3D1>();
        if (enemy3 != null)
        {
            enemy3.playerDamage = 20f;
            enemy3.swordDamage = 25f;
            yield return new WaitForSeconds(secondsToWait);
            enemy3.playerDamage = 15f;
            enemy3.swordDamage = 20f;
        }

        attackUp = false;
    }

    /*private void AttackUp()
    {
        GetComponent<EnemyL1D1>().playerDamage = 20f;
        GetComponent<EnemyL1D1>().swordDamage = 25f;
    }*/

    /// <summary>
    /// codes for the player jumping; they have to touch the ground before jumping
    /// </summary>
    private void HandleJump()
    {
        //Raycasts from the bottom of the player and detects if the ground is there
        RaycastHit hit;

        //if the ground is there
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.8f))
        {
            //they can jump
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("The player can jump.");
        }
        else
        {
            //if the ground is not there, then they can't
            Debug.Log("The player can't jump.");
        }
    }

    /*private void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletInstance.GetComponent<Bullet>().goingRight = shootRight;
    }*/

    /// <summary>
    /// allows the player to use the sheild to not take damage for 3 seconds
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator Shield(float secondsToWait)
    {
        if (!recharge)
        {
            shield = true;
            yield return new WaitForSeconds(secondsToWait);
            shield = false;
            StartCoroutine(Recharge(13));
        }
        
    }

    /// <summary>
    /// "recharges" the sheild before it can be used again
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator Recharge(float secondsToWait)
    {
        recharge = true;
        yield return new WaitForSeconds(secondsToWait);
        recharge = false;
    }

    /// <summary>
    /// if the player's health reaches 0 or below, the game will end
    /// </summary>
    private void GameOver()
    {
        if (healthPoints <= 0)
        {
            SceneManager.LoadScene(8);
            Debug.Log("The player died. Game over.");
        }
    }
}
