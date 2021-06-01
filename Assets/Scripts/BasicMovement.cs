using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float moveSpeed = 200;

        Vector3 vel = new Vector3();

        if (Input.GetKey(KeyCode.S))
        {
            vel -= new Vector3(1,0,1).normalized * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            vel += new Vector3(1, 0, 1).normalized * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel += new Vector3(-1, 0, 1).normalized * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel -= new Vector3(-1, 0, 1).normalized * Time.deltaTime * moveSpeed;
        }

        GetComponent<Rigidbody>().velocity = vel;

    }
}
