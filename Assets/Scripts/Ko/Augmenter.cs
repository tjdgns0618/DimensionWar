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
    public List<int> nonag;
    public int r;
    public int n;
    string[] splitString;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach(Augmenter_Data _Data in augmenter_Datas)
        {
            _Data.count = 0;
        }
    }
    public void AugmentUpdate()
    {
        Name.text = augmenter_Datas[r].Augmenter_Name;
        splitString = augmenter_Datas[r].Augmenter_Desc.Split("0");
        n = augmenter_Datas[r].count;
        Effet.text = string.Format(splitString[0]+ augmenter_Datas[r].num[n] + splitString[1]);
        Image.sprite = augmenter_Datas[r].image;
        number = augmenter_Datas[r].number;
        
    }
    public void Button()
    {
        button.SetActive(false);
        Time.timeScale = PlayerPrefs.GetInt("Timescale");
        //if(augmenter_Datas[r].count<3)
            augmenter_Datas[r].count++;
        if (augmenter_Datas[r].count >= augmenter_Datas[r].num.Count)
        {
                GameManager.Instance.FullUpAugm.Add(number);
        }
        switch (number)
        {
            case 0:
                AttackUp();
                break;
            case 1: 
                HealthUP();
                break;
            case 2:
                SpeedUp();
                break;
            case 3:
                BonusDamage();
                break;
            case 4:
                Skill_DamageUp();
                break;
            case 5:
                Pixel_HealthUP();
                break;
            case 6:
                Pixel_AttackUp();
                break;
            case 7:
                LowPoly_HealthUP();
                break;
            case 8:
                LowPoly_AttackUp();
                break;
            case 9:
                _3D_HealthUP();
                break;
            case 10:
                _3D_AttackUp();
                break;
        }
    }
    public void HealthUP()
    {
        GameManager.Instance.towerHp += augmenter_Datas[r].num[n];

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().health += augmenter_Datas[r].num[n];
        }
    }

    public void AttackUp()
    {
        GameManager.Instance.towerDamage += augmenter_Datas[r].num[n];

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().Damage += augmenter_Datas[r].num[n];
        }
    }
    public void SpeedUp()
    {
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().AttackDel /= 1.5f;

        }
    }

    public void BonusDamage()
    {
        GameManager.Instance.BonusDamage += augmenter_Datas[r].num[n];
    }
    public void Skill_DamageUp()
    {
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower_Skill>().SkillDmg += augmenter_Datas[r].num[n];
        }
    }
    public void Pixel_HealthUP()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.Pixel)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += augmenter_Datas[r].num[n];
        }
    }

    public void Pixel_AttackUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.Pixel)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += augmenter_Datas[r].num[n];
        }
    }
    public void LowPoly_HealthUP()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += augmenter_Datas[r].num[n];
        }
    }

    public void LowPoly_AttackUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += augmenter_Datas[r].num[n];
        }
    }
    public void _3D_HealthUP()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += augmenter_Datas[r].num[n];
        }
    }
    public void _3D_AttackUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += augmenter_Datas[r].num[n];
        }
    }
}
