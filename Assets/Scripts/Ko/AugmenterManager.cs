using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AugmenterManager : MonoBehaviour
{
    public GameObject btt;
    public GameObject[] bt;
    public List<int> r = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Killcount >= 10)
        {
            GameManager.Instance.Killcount = 0;
            r = new List<int>();
            Augmeneter();
            CreateUnDuplicateRandom();
            
        }

    }
    public void Augmeneter()
    {
        Time.timeScale = 0;
        btt.SetActive(true);
    }
    void CreateUnDuplicateRandom()
    {
        int currentNumber = Random.Range(0, 3);
        
        for (int i = 0; i < 3;)
        {
            if (r.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, 3);
            }
            else
            {
                r.Add(currentNumber);
                i++;
            }
        }

        for (int i = 0; i < bt.Length; i++)
        {
            bt[i].GetComponent<Augmenter>().r = r[i];
            bt[i].GetComponent<Augmenter>().AugmentUpdate();
        }
    }
}
