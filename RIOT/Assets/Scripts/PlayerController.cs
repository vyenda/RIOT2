using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author(s): [Strong, Hannah]; [Arellano, Angeleen]
 * Date Last Modified: [11/16/2023]
 * Codes for the player movement
 */

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbodyRef;

    public float speed = 1f;
    private Vector3 startPos;
    public float jumpForce = 10f;
    public float health = 100f;

    //these make it so the player can't go off the screen
    public float minX = -18f;
    public float maxX = 18f;
    private Vector3 temp;
    
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
            HandleJump();
        }

        //tracks the player's health to see if they are dead or not
        GameOver();
    }

    /// <summary>
    /// codes for the player jumping; they have to touch the ground before jumping
    /// </summary>
    private void HandleJump()
    {
        //Raycasts from the bottom of the player and detects if the ground is there
        RaycastHit hit;

        //if the ground is there
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f))
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

    /// <summary>
    /// if the player's health reaches 0 or below, the game will end
    /// </summary>
    private void GameOver()
    {
        if (health <= 0)
        {
            //add a scene transition here when UI has been created
            Debug.Log("The player died. Game over.");
        }
    }
}
