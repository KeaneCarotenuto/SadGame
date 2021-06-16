using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float gravity = 5.0f;
    public float flapForce = 5.0f;

    public Vector2 velocity = new Vector2(1,0);

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            velocity.y = flapForce;
            if (!started) started = true;
        }

        if (started)
        {
            velocity.y -= gravity * Time.deltaTime;

            transform.position += new Vector3(0, velocity.y, 0) * Time.deltaTime;
        }
    }
}
