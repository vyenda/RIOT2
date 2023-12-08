using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }

    IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(7f);
        Destroy(this.gameObject);
    }
}
