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

    /// <summary>
    /// Moves onto the next level if enemy dies.
    /// </summary>
    private void NextLevel()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(4);
            Debug.Log("Enemy dead, next level.");
        }
    }
}
