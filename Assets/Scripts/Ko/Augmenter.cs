using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Augmenter : MonoBehaviour
{
    public int number;
    public Text Name;
    public Text Effet;
    public Image Image;
    public GameObject button;
    public GameObject[] allBt;
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
        if (splitString != null)
        {
            Effet.text = string.Format(splitString[0] + augmenter_Datas[r].num[n] + splitString[1]);
        }
        else
        {
            Effet.text = augmenter_Datas[r].Augmenter_Desc;
        }

        Image.sprite = augmenter_Datas[r].image;
        number = augmenter_Datas[r].number;
        
    }
    public void fullAugment()
    {
        Name.text ="더이상 강화할 증강이 없습니다.";
        n = 0;
        Effet.text = "더이상 강화할 증강이 없습니다.";
        Image.sprite = null;
        //number = augmenter_Datas[r].number;
    }
    public void Button()
    {
        foreach(GameObject AllBt in allBt)
        {
            AllBt.gameObject.SetActive(false);
        }
        button.SetActive(false);
        Time.timeScale = PlayerPrefs.GetInt("Timescale");
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
            case 11:
                RangeUp();
                break;
        }
    }
    public void HealthUP()
    {
        GameManager.Instance.towerHp += augmenter_Datas[r].num[n] * 0.01f;

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().health += GameManager.Instance.towers[i].GetComponent<Tower>().tempHealth * augmenter_Datas[r].num[n]*0.01f;
        }
    }

    public void AttackUp()
    {
        GameManager.Instance.towerDamage += augmenter_Datas[r].num[n] * 0.01f; 

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().Damage += GameManager.Instance.towers[i].GetComponent<Tower>().TempDamage * augmenter_Datas[r].num[n] * 0.01f;
        }
    }
    public void SpeedUp()
    {
        GameManager.Instance.towerSpeed += augmenter_Datas[r].num[n] * 0.01f;
        
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().AttackDel -= GameManager.Instance.towers[i].GetComponent<Tower>().AttackDel * augmenter_Datas[r].num[n] * 0.01f;
            GameManager.Instance.towers[i].GetComponent<Tower>().anim.speed += augmenter_Datas[r].num[n] * 0.01f;
        }
    }

    public void BonusDamage()
    {
        GameManager.Instance.BonusDamage += augmenter_Datas[r].num[n] * 0.01f;
    }
    public void Skill_DamageUp()
    {
        GameManager.Instance.SkillDamage += augmenter_Datas[r].num[n] * 0.01f; 
    }
    public void Pixel_HealthUP()
    {

        GameManager.Instance.Pixel_tower_Hp += augmenter_Datas[r].num[n] * 0.01f; 
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.Pixel)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += GameManager.Instance.towers[i].GetComponent<Tower>().tempHealth * augmenter_Datas[r].num[n] * 0.01f;
        }
    }

    public void Pixel_AttackUp()
    {
        GameManager.Instance.Pixel_tower_damage += augmenter_Datas[r].num[n] * 0.01f; 

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.Pixel)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += GameManager.Instance.towers[i].GetComponent<Tower>().TempDamage * augmenter_Datas[r].num[n] * 0.01f;
        }
    }
    public void LowPoly_HealthUP()
    {
        GameManager.Instance.LowPoly_tower_Hp+= augmenter_Datas[r].num[n] * 0.01f;

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += GameManager.Instance.towers[i].GetComponent<Tower>().tempHealth * augmenter_Datas[r].num[n] * 0.01f;
        }
    }

    public void LowPoly_AttackUp()
    {
        GameManager.Instance.LowPoly_tower_damage += augmenter_Datas[r].num[n] * 0.01f;

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += GameManager.Instance.towers[i].GetComponent<Tower>().TempDamage * augmenter_Datas[r].num[n] * 0.01f;
        }
    }
    public void _3D_HealthUP()
    {

        GameManager.Instance._3D_tower_damage += augmenter_Datas[r].num[n] * 0.01f; 
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().health += GameManager.Instance.towers[i].GetComponent<Tower>().tempHealth * augmenter_Datas[r].num[n] * 0.01f;
        }
    }
    public void _3D_AttackUp()
    {
        GameManager.Instance._3D_tower_damage += augmenter_Datas[r].num[n] * 0.01f; 
        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            if (GameManager.Instance.towers[i].GetComponent<Tower>().tower_class == Tower.Tower_Class.RowPoly)
                GameManager.Instance.towers[i].GetComponent<Tower>().Damage += GameManager.Instance.towers[i].GetComponent<Tower>().TempDamage * augmenter_Datas[r].num[n] * 0.01f;
        }
    }
    public void RangeUp()
    {

        for (int i = 0; i < GameManager.Instance.towers.Count; i++)
        {
            GameManager.Instance.towers[i].GetComponent<Tower>().AttackRange += augmenter_Datas[r].num[n];
        }
    }
}
