using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/07/2023]
 * Codes for the hard difficulty, level 3 enemy.
 */

public class HardEnemy3 : MonoBehaviour
{

    public float health = 150f;
    public float speed = 5.5f;
    public float enemyDamage = 20f;

    public float playerDamage = 15f;
    public float swordDamage = 20f;

    public GameObject playerTarget;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
        NextLevel();
    }

    private void NextLevel()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("Enemy dead, next level.");
        }
    }
}
