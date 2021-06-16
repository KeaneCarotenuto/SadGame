using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThoughtManager : MonoBehaviour
{
    public GameObject m_thoughtPref;

    public float anxietyLevel = 0;

    public float spawnTime = 2;
    float lastSpawnTime;

    public GameObject aBar;
    public GameObject aLevel;

    public Canvas canvas;

    public List<GameObject> m_thoughts = new List<GameObject>();

    public GameObject bird;


    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;

        m_thoughts.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = m_thoughts.Count-1; i >= 0; i--)
        {
            GameObject t = m_thoughts[i];

            if (t == null)
            {
                m_thoughts.RemoveAt(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Room");
        }
    }

    private void FixedUpdate()
    {
        anxietyLevel += 5 * Time.deltaTime;
        anxietyLevel = Mathf.Clamp(anxietyLevel, 0, 100);

        if (Time.time - lastSpawnTime > spawnTime)
        {
            lastSpawnTime = Time.time;
            GameObject t = Instantiate(m_thoughtPref, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity, canvas.transform);
            m_thoughts.Add(t);

            t.GetComponent<Thought>().bird = bird;
        }
      
        RectTransform bar = aLevel.GetComponent<RectTransform>();
        bar.sizeDelta = new Vector2(anxietyLevel * 5, bar.rect.height);
    }
}
