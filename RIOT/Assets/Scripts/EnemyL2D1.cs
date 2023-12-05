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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
    }
}