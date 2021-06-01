using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThoughtManager : MonoBehaviour
{
    public GameObject m_thoughtPref;

    public float anxietyLevel = 0;

    public GameObject aBar;
    public GameObject aLevel;

    public Canvas canvas;

    public List<GameObject> m_thoughts = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
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
        anxietyLevel += 10 * Time.deltaTime;

        float r = Random.Range(0, 1000.0f);

        if (r < anxietyLevel && m_thoughts.Count < 20 && ((int)Time.time) % 1 == 0 )
        {
            GameObject t = Instantiate(m_thoughtPref, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity, canvas.transform);
            m_thoughts.Add(t);
        }
      
        RectTransform bar = aLevel.GetComponent<RectTransform>();
        bar.sizeDelta = new Vector2(anxietyLevel, bar.rect.height);
    }
}
