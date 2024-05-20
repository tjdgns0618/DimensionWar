using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using DG.Tweening;
public class AugmenterManager : MonoBehaviour
{
    public GameObject btt;
    public GameObject[] bt;
    public List<int> r = new List<int>();
    public GameObject[] reRollBt;
    
    public int max;

    [SerializeField]
    int Upgradecount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Killcount >= 10&& max - GameManager.Instance.FullUpAugm.Count!=0)
        {
            GameManager.Instance.Killcount = 0;            
            Augmeneter();
            CreateUnDuplicateRandom();            
        }
    }

    public void Augmeneter()
    {
        r = new List<int>();
        Time.timeScale = 0;
        btt.SetActive(true);
        GameManager.Instance.blockClicked = false;
        if (GameManager.Instance.SelectBlock)
        {
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
            //GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.clip =
            //GameManager.Instance.SelectBlock.GetComponent<Blocks>().audio[1];
            //GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.Play();
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource[1].Play();
            GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
            Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);
            GameManager.Instance.SelectBlock = null;
        }
        if (GameManager.Instance.tower)
        {
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
            GameManager.Instance.uiManager.UpgradePanel.GetComponent<DOTweenAnimation>().DORewind();
            GameManager.Instance.tower = null;
        }
    }

    public void reroll(int num)
    {
        if (GameManager.Instance.gold <=10|| max - GameManager.Instance.FullUpAugm.Count <= 2)
        {
            //gameObject.SetActive(false);
            return;
        }
        GameManager.Instance.gold -= 10;
        int currentNumber = Random.Range(0, max);
        while (true)
        {
            if (r.Contains(currentNumber)|| GameManager.Instance.FullUpAugm.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, max);
               
            }
            else
            {
                r.RemoveAt(num);
                r.Insert(num, currentNumber);
                break;
            }
        }
        bt[num].GetComponent<Augmenter>().r = r[num];
        bt[num].GetComponent<Augmenter>().AugmentUpdate();
    }
    public void CreateUnDuplicateRandom()
    {
        
        int currentNumber = Random.Range(0, max);
        if (max - GameManager.Instance.FullUpAugm.Count <= 2)
        {
            for (int i = 0; i < max - GameManager.Instance.FullUpAugm.Count;)
            {
                while (r.Contains(currentNumber) || GameManager.Instance.FullUpAugm.Contains(currentNumber))
                {
                    currentNumber = Random.Range(0, max);
                }
                r.Add(currentNumber);
                i++;
            }
            for (int i = 0; i < max - GameManager.Instance.FullUpAugm.Count; i++)
            {
                bt[i].GetComponent<Augmenter>().r = r[i];
                bt[i].GetComponent<Augmenter>().AugmentUpdate();
                bt[i].SetActive(true);
            }
            return;
        }
        for (int i = 0; i < bt.Length;)
        {
            while(r.Contains(currentNumber)|| GameManager.Instance.FullUpAugm.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, max);
            }
                r.Add(currentNumber);
                i++;
        }
        for (int i = 0; i < bt.Length; i++)
        {
            bt[i].GetComponent<Augmenter>().r = r[i];
            bt[i].GetComponent<Augmenter>().AugmentUpdate();
            bt[i].SetActive(true);
        }
    }
    public void AugmenterBuy()
    {
        if(GameManager.Instance.gold <100)
        {
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            return;
        }
        GameManager.Instance.gold -= 100;
       
        Augmeneter();
        CreateUnDuplicateRandom();
    }
}
