using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [12/07/2023]
 * Codes for pickup items.
 */

public class Pickup : MonoBehaviour
{
    public float playerHeal = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Controls the interactions with player collision
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
