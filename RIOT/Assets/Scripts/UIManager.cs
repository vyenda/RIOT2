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
    public L2D1Enemy easyE2;
    public TMP_Text easyE2H;
    public TMP_Text easyE3H;
    public TMP_Text hardE1H;
    public TMP_Text hardE2H;
    public TMP_Text hardE3H;
    
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
