using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [11/21/2023]
 * codes for the UI, such as the player and enemy health on screen
 */

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text playerHealth;

    public TMP_Text easyE1H;
    public EnemyL1D1 enemyL1D1;

    public TMP_Text easyE3H;
    public EnemyL3D1 enemyL3D1;

    public TMP_Text easyE2H;
    public EnemyL1D2 enemyL1D2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
  
    // Update is called once per frame
    void Update() 
    {
        playerHealth.text = "Player HP: " + playerController.healthPoints.ToString();
        easyE1H.text = "Enemy 1 HP: " + enemyL1D1.health.ToString();
        easyE2H.text = "Enemy 3 HP: " + enemyL1D2.health.ToString();
        easyE3H.text = "Enemy 2 HP: " + enemyL3D1.health.ToString();
    }
}
