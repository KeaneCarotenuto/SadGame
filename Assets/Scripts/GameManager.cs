using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bird;
    public BirdMovement birdScript;

    public GameObject ringPrefab;
    public List<GameObject> rings;

    public float playerScore = 0;

    public float spawnTime ;
    float lastSpawnTime = 0;

    public TextMeshProUGUI scoreText;

    public GameObject background1;
    public GameObject background2;
    public GameObject background3;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        background1.transform.position -= new Vector3(Time.deltaTime * birdScript.velocity.x, 0,0);
        background2.transform.position -= new Vector3(Time.deltaTime * birdScript.velocity.x, 0,0);
        background3.transform.position -= new Vector3(Time.deltaTime * birdScript.velocity.x, 0,0);

        if (background1.transform.position.x < -36) background1.transform.position = new Vector3(background3.transform.position.x + 36, 0, 0);
        if (background2.transform.position.x < -36) background2.transform.position = new Vector3(background1.transform.position.x + 36, 0, 0);
        if (background3.transform.position.x < -36) background3.transform.position = new Vector3(background2.transform.position.x + 36, 0, 0);

        for (int i = rings.Count - 1; i >= 0; i--)
        {
            if (!rings[i]) rings.RemoveAt(i);
        }

        if (Time.time - lastSpawnTime > spawnTime)
        {
            lastSpawnTime = Time.time;
            GameObject t = Instantiate(ringPrefab, new Vector3(10, Random.Range(-5, 5), 0), Quaternion.identity, null);
            rings.Add(t);
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject _ring in rings)
        {

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (screenPos.x < 0)
            {
                Destroy(gameObject);
            }

            if (_ring.GetComponent<Rigidbody2D>().gravityScale == 0 && Vector3.Distance(_ring.transform.position, bird.transform.position) < 0.5f)
            {
                _ring.GetComponent<Rigidbody2D>().gravityScale = 1;
                playerScore += 1;

                scoreText.text = "SCORE\n" + playerScore;
            }
            else if (_ring.GetComponent<Rigidbody2D>().gravityScale == 0)
            {
                _ring.GetComponent<Rigidbody2D>().velocity = new Vector3(-birdScript.velocity.x, 0, 0);
            }
        }
    }
}
