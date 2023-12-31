using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/07/2023]
 * codes for the UI, such as the player and enemy health on screen
 */

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
  
    // Update is called once per frame
    void Update() 
    {
        playerHealth.text = "Player HP: " + playerController.healthPoints.ToString();
    }
}
