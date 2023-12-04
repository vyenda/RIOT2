using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [11/28/2023]
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

    public bool shield = false;
    public bool recharge = false;

    public float enemyL1D1Damage = 5f;
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
        }

        //if D is pressed
        if (Input.GetKey(KeyCode.D))
        {
            //the player will move right
            transform.position += Vector3.right * speed * Time.deltaTime;
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            armAnimation.Play("ArmAnimation");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
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
        }

        if (other.tag == "L2D1Enemy")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            healthPoints -= 15f;
        }

        if (other.gameObject.tag == "HealthPickup")
        {
            healthPoints += other.gameObject.GetComponent<Pickup>().playerHeal;
        }
    }

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
            SceneManager.LoadScene(3);
            Debug.Log("The player died. Game over.");
        }
    }
}
