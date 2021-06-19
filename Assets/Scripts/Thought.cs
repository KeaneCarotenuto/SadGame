using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : MonoBehaviour
{
    public ThoughtManager m_thoughtManager;
    public GameObject text;
    public Camera cam;

    public bool onMouse = false;
    bool beenTouched = false;

    public GameManager m_gameManager;

    GameObject target = null;

    public GameObject bird;

    private void Awake()
    {
        m_thoughtManager = GameObject.Find("ThoughtManager").GetComponent<ThoughtManager>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        cam = GameObject.Find("Cam").GetComponent<Camera>(); ;

        if (!m_thoughtManager)
        {
            Debug.Log("Cant find TM");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

        if (Input.GetMouseButton(0))
        {
            Debug.Log("down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                Debug.Log("on obj");
                beenTouched = true;
                onMouse = true;
            }
        }
        else
        {
            onMouse = false;
        }

        if (onMouse)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.velocity = Vector2.ClampMagnitude((pos - (Vector2)transform.position) * 10,10);
        }
        else
        {
            if (!beenTouched)
            {
                if (!target)
                {
                    float dist = Mathf.Infinity;

                    foreach (GameObject _ring in m_gameManager.rings)
                    {
                        if (Vector2.Distance(_ring.transform.position, gameObject.transform.position) <= dist)
                        {
                            target = _ring;
                        }

                    }
                }

                if (target)
                {
                    transform.position += ((target.transform.position) - transform.position).normalized * Time.deltaTime;
                    transform.position -= new Vector3(bird.GetComponent<BirdMovement>().velocity.x, 0, 0) * Time.deltaTime;
                }
            }
            
            
        }


        if (screenPos.x < 0 ||
        screenPos.x > Screen.width ||
        screenPos.y < 0 ||
        screenPos.y > Screen.height)
        {

        }

        //transform.position -= new Vector3(1,0,0) * Time.deltaTime;

        if (screenPos.x < 0 ||
            screenPos.x > Screen.width ||
            screenPos.y < 0 ||
            screenPos.y > Screen.height)
        {
            Destroy(gameObject);
            m_thoughtManager.anxietyLevel -= 5;
            if (m_thoughtManager.anxietyLevel < 0) m_thoughtManager.anxietyLevel = 0;
        }
    }
}
