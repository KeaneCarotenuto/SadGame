using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PC : MonoBehaviour
{
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (text.enabled && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Starting");
            SceneManager.LoadScene("Game");
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            text.enabled = true;
            Debug.Log("In");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            text.enabled = false;
            Debug.Log("Out");
        }
    }
}
