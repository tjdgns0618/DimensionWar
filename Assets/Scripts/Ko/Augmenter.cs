using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Augmenter : MonoBehaviour
{
    public int number;
    public Text Name;
    public Text Effet;
    public Image Image;
    public GameObject button;
    public Augmenter_Data[] augmenter_Datas;
    public int r;
   
    // Start is called before the first frame update

    private void Start()
    {
        //randomAugmenter();
       


    }
    public void AugmentUpdate()
    {
        Name.text = augmenter_Datas[r].Augmenter_Name;
        Effet.text = augmenter_Datas[r].Augmenter_Desc;
        Image.sprite = augmenter_Datas[r].image;
    }
    public void Button()
    {
        switch(number)
        {
            case 0:
                HealthUP();
                break;
            case 1:
                AttackUp();
                break;
            case 2:
                SpeedUp();
                break;
        }
    }
    public void HealthUP()
    {
        button.SetActive(false);
        Time.timeScale = 1;

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().health += 100;
        }
    }

    public void AttackUp()
    {
        button.SetActive(false);

        Time.timeScale = 1;
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().Damage += 100;
        }
    }
    public void SpeedUp()
    {
        button.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().AttackDel /=1.5f;

        }
    }

    //public void randomAugmenter()
    //{
    //    r.Add(Random.Range(0, 3));
        
    //}

    // 랜덤 생성 (중복 배제)
   
}
