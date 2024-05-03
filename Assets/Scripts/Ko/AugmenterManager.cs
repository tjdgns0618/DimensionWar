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
            r = new List<int>();
            Augmeneter();
            CreateUnDuplicateRandom();
            
        }

    }
    public void Augmeneter()
    {
        Time.timeScale = 0;
        btt.SetActive(true);

        GameManager.Instance.blockClicked = false;
        if (GameManager.Instance.SelectBlock)
        {
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.clip =
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().audio[1];
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.Play();
            Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);
            GameManager.Instance.SelectBlock = null;
        }
        if (GameManager.Instance.tower)
        {
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            GameManager.Instance.tower = null;
        }
    }
    public void reroll(int num)
    {
        
        int currentNumber = Random.Range(0, max);
        if (r.Contains(currentNumber))
        {
            currentNumber = Random.Range(0, max);
        }
        else
        {
            r.RemoveAt(num);
            r.Insert(num, currentNumber);
        }
        bt[num].GetComponent<Augmenter>().r = r[num];
        bt[num].GetComponent<Augmenter>().AugmentUpdate();
    }
    void CreateUnDuplicateRandom()
    {
        int currentNumber = Random.Range(0, max);
        
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
            bt[i].GetComponent<Augmenter>().r = r[i];
            bt[i].GetComponent<Augmenter>().AugmentUpdate();
        }
        count++;
    }
}
