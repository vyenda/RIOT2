using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Authors: Arellano, Angeleen; Strong, Hannah
 * Last Modified: 11/30/2023
 * Controls the Level 2 Difficulty 1 Enemy
 */

public class L2D1Enemy : MonoBehaviour
{

    public GameObject playerTarget;
    public float speed;
    public float enemyHealthPoints = 100f;

    public float playerDamage = 15f;
    public float swordDamage = 20f;

    public bool attackUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackTwo")
        {
            StartCoroutine(AttackTwo(10));
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// ups the enemy damage for a limited amount of time
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    IEnumerator AttackTwo(float secondsToWait)
    {
        attackUp = true;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enemyL2D1Damage = 25f;
            yield return new WaitForSeconds(secondsToWait);
            player.enemyL2D1Damage = 20f;
        }

        attackUp = false;
    }
}