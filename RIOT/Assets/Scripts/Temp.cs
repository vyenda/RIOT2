using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    public float speed = 1f;
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
