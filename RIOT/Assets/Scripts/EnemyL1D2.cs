using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/04/2023]
 * Codes for the first level, first difficulty enemy
 */

public class EnemyL1D2 : MonoBehaviour
{
    public float health = 100f;
    public float speed = 5.5f;
    public float enemyDamage = 20f;

    public float playerDamage = 15f;
    public float swordDamage = 20f;

    public GameObject playerTarget;

    public bool attackThree = false;

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
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
        NextLevel();
    }
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

            if (other.gameObject.tag == "AttackThree")
        {
            StartCoroutine(AttackThree(10));
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// ups the attack of the enemy for the specified period of time when called
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackThree(float secondsToWait)
    {
        attackThree = true;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enemyL2D1Damage = 25f;
            yield return new WaitForSeconds(secondsToWait);
            player.enemyL2D1Damage = 20f;
        }

        attackThree = false;
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
    /// Moves onto the next level if enemy dies.
    /// </summary>
    private void NextLevel()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("Enemy dead, next level.");
        }
    }
}
