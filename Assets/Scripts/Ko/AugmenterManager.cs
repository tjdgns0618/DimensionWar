using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AugmenterManager : MonoBehaviour
{
    public GameObject btt;
    public GameObject[] bt;
    public List<int> r = new List<int>();
    public int count=0;
    public int max;
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
            //r = new List<int>();
            if (max - r.Count <= 2)
                return;
            Augmeneter();
            CreateUnDuplicateRandom();
            
        }

    }
    public void Augmeneter()
    {
        Time.timeScale = 0;
        btt.SetActive(true);
    }
    public void CreateUnDuplicateRandom()
    {
        int currentNumber = Random.Range(0, 3);
        
        for (int i = 0; i < bt.Length;)
        {
            if (r.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, max);
            }
            else
            {
                r.Add(currentNumber);
                i++;
            }
        }
       
        for (int i = 0; i < bt.Length; i++)
        {
            bt[i].GetComponent<Augmenter>().r = r[i+(count * 3)];
            bt[i].GetComponent<Augmenter>().AugmentUpdate();
        }
        count++;
    }
}
